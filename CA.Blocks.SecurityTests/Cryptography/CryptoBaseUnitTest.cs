using System.Diagnostics;
using System.IO;
using CA.Blocks.Security;
using CA.Blocks.Security.Cryptography;
using NUnit.Framework;

namespace CA.Blocks.SecurityTests.Cryptography
{
    public class CryptoBaseUnitTest
    {

        private string GenerateRandomText(int length)
        {
            RandomTextGenerator.RandomTextGeneratorSettings Settings =
                new RandomTextGenerator.RandomTextGeneratorSettings {Length = length};
            RandomTextGenerator rtGenerator = new RandomTextGenerator(Settings);
            return rtGenerator.Generate();
        }

        protected void EncryptDecryptTest(IEncryption crypto)
        {
            string plainText = GenerateRandomText(1000);
            string key = GenerateRandomText(5);

            Trace.WriteLine(string.Format("keyUsed={0}", key));
            Trace.WriteLine(string.Format("plainText={0}",plainText));
            string encryptedText = crypto.Encrypt(plainText, key);
            Trace.WriteLine(string.Format("encryptedText={0}", encryptedText));
            string unEncryptedText = crypto.Decrypt(encryptedText, key);
            Assert.AreEqual(plainText, unEncryptedText);
        }

        protected void EncryptDecryptFileTest(IEncryption crypto)
        {
            string tempPath = Path.GetTempPath();
            string testOrginalFileName = Path.Combine(tempPath, "Test.txt");
            string encrypttestFileName = Path.Combine(tempPath, "Test.txt.enc");
            string testDecryptedFileName = Path.Combine(tempPath, "Test1.txt");

            string plainText = GenerateRandomText(2000);
            string key = GenerateRandomText(5);
            Trace.WriteLine(string.Format("keyUsed={0}", key));
            Trace.WriteLine(string.Format("plainText={0}", plainText));

            File.WriteAllText(testOrginalFileName, plainText);
            crypto.EncryptFile(testOrginalFileName, encrypttestFileName , key);

            crypto.DecryptFile(encrypttestFileName, testDecryptedFileName, key);

            string unEncryptedText = File.ReadAllText(testDecryptedFileName);

            Assert.AreEqual(plainText, unEncryptedText);

            File.Delete(testOrginalFileName);
            File.Delete(encrypttestFileName);
            File.Delete(testDecryptedFileName);

        }


        protected void GenerateRandomKeyAsStringTest(IEncryption crypto, int expectedSize)
        {
            string key = crypto.GenerateRandomKeyAsString();
            Trace.WriteLine(key);
            Assert.AreEqual(expectedSize, key.Length );
        }
    }
}
