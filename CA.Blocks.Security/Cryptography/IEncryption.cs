namespace CA.Blocks.Security.Cryptography
{
    /// <summary>
    /// A simple interface to allow switching of encryption algorithms 
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        /// Will Encrypt the plaintext and return a base64econded string
        /// </summary>
        /// <param name="plaintext"> the string to encrypt</param>
        /// <param name="key">The password used to encrypt the string.</param>
        /// <returns>a base 64 encoded string </returns>
        string Encrypt(string plaintext, string key);

        /// <summary>
        /// Will Decrypt the base64 econded Text using the provide key. 
        /// The Encrypt and Decrypt must use the same algorithm to work.  
        /// </summary>
        /// <param name="base64Text"> the encoded Test which was generated from the Encrypt method</param>
        /// <param name="key">The password used to encrypt the string.</param>
        /// <returns>The plain Decrypted text</returns>
        string Decrypt(string base64Text, string key);



        void EncryptFile(string sourceFilePath, string encryptedFilePathName, string key);


        void DecryptFile(string sourceFilePath, string decryptFilePathName, string key);


        /// <summary>
        /// Will Generate a Random hex string version Key 
        /// </summary>
        /// <returns></returns>
        string GenerateRandomKeyAsString();
    }
}
