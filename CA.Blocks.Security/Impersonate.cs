using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace CA.Blocks.Security
{
    /// <summary>
    /// ImpersonateException is the exception which is raised in the event the Impersonate class fails to Impersonate
    /// </summary>
    public class ImpersonateException : Exception
    {
        /// <summary>
        /// Public Constructor which simply accepts a message
        /// </summary>
        /// <param name="message"></param>
        public ImpersonateException(string message) : base(message)
        {

        }
    }

    /// <summary>
    /// This impersonate class has been designed to enable the running of code segment a user under different security context to 
    /// that which is the norm.   The typical example is allowing the ASP.NET account to have access to network drives 
    /// while the running in the context of the ASP.NET account.   This class by design implements the IDisposable 
    /// interface allowing the context to automatically revert with a using statement. 
    /// </summary>
    /// <remarks>
    /// Using the dispose method makes it possible to use the Impersonate class with a using class in c#
    /// <code lang="cs" title="Quick Example of using Impersonate class using the C# using statement">
    /// using (new Impersonate(User, Domain, Password))
    /// { 
    ///  //.. do work with new security context
    /// }  
    /// </code>  
    /// </remarks>
	public class Impersonate : IDisposable
    {
        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;
        #region windows API calls
        [DllImport("advapi32.dll")]
        private static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);
        #endregion

        readonly WindowsImpersonationContext _impersonationContext;

        /// <summary>
        /// This is the only constructor to the class and by design implements the IDisposable  interface allowing the context to 
        /// automatically revert with a using statement. The Constructor take the username password and a domain in. 
        /// </summary>
        /// <remarks>
        /// Using the dispose method makes it possible to use the Impersonate class with a using class in c#
        /// <code lang="cs" title="Quick Example of using Impersonate class using the C# using statement">
        /// using (new Impersonate(User, Domain, Password))
        /// { 
        ///  //.. do work with new security context
        /// }    
        /// </code>
        /// </remarks>
        /// <param name="userName">The username</param>
        /// <param name="domain"> the Domain to authenticate against</param>
        /// <param name="password">the password</param>
        /// <exception cref="ImpersonateException"> will throw an ImpersonateException if Impersonate fails</exception>
        public Impersonate(string userName, string domain, string password)
        {
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;
            // firstly force any current Impersonation to revert to the default security context  
            if (RevertToSelf())
            {
                // Call the LogonUserA to authenticate the user and get the users token
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {

                    //The DuplicateToken function creates an impersonation token which can be used when creating a new windows identity 
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        var tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        _impersonationContext = tempWindowsIdentity.Impersonate();
                        // we have the impersonationContext so we can close the handles to the tokens
                        if (_impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                        }
                    }
                    else
                    {
                        if (token != IntPtr.Zero)
                            CloseHandle(token);
                        if (tokenDuplicate != IntPtr.Zero)
                            CloseHandle(tokenDuplicate);
                        throw new ImpersonateException(string.Format("Impersonation Failed - DuplicateToken ({0})", userName));
                    }
                }
                else
                {
                    if (token != IntPtr.Zero)
                        CloseHandle(token);
                    throw new ImpersonateException(string.Format("Impersonation Failed - LogonUserA ({0})", userName));
                }
            }
            else
            {
                throw new ImpersonateException("Impersonation Failed - RevertToSelf");
            }
        }

        /// <summary>
        /// On Dispose the Context of the Impersonate is undone by calling the Undo method.. 
        /// </summary>
        /// <remarks>
        /// Using the dispose method makes it possible to use the Impersonate class with a using class in c#
        /// <code lang="cs" title="Quick Example of using Impersonate class using the C# using statement">
        /// using (new Impersonate(User, Domain, Password))
        /// { 
        ///  //.. do work with new security context
        /// }    
        /// </code>
        /// </remarks>
		public void Dispose()
        {
            _impersonationContext.Undo();
        }
    }
}

