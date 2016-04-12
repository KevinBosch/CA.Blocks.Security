using CA.Blocks.Security.Cryptography;
using CA.Blocks.SecurityTests.Cryptography;
using NUnit.Framework;

namespace CA.Common.SecurityTests.Cryptography
{
    [TestFixture]
    public class TripleDESCryptoUnitTest : CryptoBaseUnitTest
    {

        [Test]
        public void EncryptDecryptTest()
        {
            EncryptDecryptTest(new TripleDESCrypto());
        }

        [Test]
        public void GenerateRandomKeyAsStringTest()
        {
            GenerateRandomKeyAsStringTest(new TripleDESCrypto(), 48);
        }

        [Test]
        public void EncryptDecryptFileTest()
        {
            EncryptDecryptFileTest(new TripleDESCrypto());
        }


    }

  
}
