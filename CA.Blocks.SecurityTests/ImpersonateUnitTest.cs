using System;
using System.Configuration;
using System.Diagnostics;
using System.Security.Principal;
using CA.Blocks.Security;
using NUnit.Framework;

namespace CA.Blocks.SecurityTests
{
    [TestFixture]
    public class ImpersonateUnitTest
    {

        [Test]
        public void TestValidImpersonate()
        {
            // get the user name password and domain from the config file
            string user = ConfigurationManager.AppSettings["TestUser"];
            string password = ConfigurationManager.AppSettings["TestPassword"];
            string domain = ConfigurationManager.AppSettings["TestDomain"];

            if (String.IsNullOrWhiteSpace(user) || String.IsNullOrWhiteSpace(password))
            {
                Assert.Inconclusive("No User Name or password set cannot run Tests on local machine");
            }
            else
            {
                // Now get the  user name currently running
                string beforeImpersonateorIdentityName = WindowsIdentity.GetCurrent().Name;
                Trace.WriteLine(beforeImpersonateorIdentityName);
                // Now begin the Impersonate under a new account
                using (new Impersonate(user, domain, password))
                {
                    // this is is expected name after the Impersonate
                    string expectedIdentityName = domain + "\\" + user;
                    // Now get the current WindowsIdentity name inside the Impersonate
                    string actualIdentityNameInImpersonate = WindowsIdentity.GetCurrent().Name;
                    Trace.WriteLine(actualIdentityNameInImpersonate);
                    // test that the expected is the same as the ActualIdentityNameInImpersonate do ToLower as names and domains are case insensitive 
                    Assert.AreEqual(expectedIdentityName.ToLower(), actualIdentityNameInImpersonate.ToLower());
                }
                // get the Name after the Impersonate section is done.. this should revert the identity 
                string afterImpersonateorIdentityName = WindowsIdentity.GetCurrent().Name;
                Trace.WriteLine(afterImpersonateorIdentityName);
                // do the test the the BeforeImpersonateorIdentityName is the same as the AfterImpersonateorIdentityName
                Assert.AreEqual(beforeImpersonateorIdentityName.ToLower(), afterImpersonateorIdentityName.ToLower());
            }
           
        }


        [Test]
        public void TestInvalidImpersonate()
        {
            try
            {
                using (new Impersonate("baduser", "workgroup", "badPassword"))
                {
                    // This should should never get executed as Impersonate should fail
                    // and raise the expected ImpersonateException unless you have the user password domain combo as hard coded

                }
            }
            catch (System.Exception ex)
            {
                Assert.IsInstanceOf<ImpersonateException>(ex);
            }
        } 
    }
}
