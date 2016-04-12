using CA.Blocks.Security.Cryptography;
using NUnit.Framework;

namespace CA.Blocks.SecurityTests.Cryptography
{
    [TestFixture]
    public class AesCryptoUnitTest : CryptoBaseUnitTest
    {
        [Test]
        public void EncryptDecryptTest()
        {
            EncryptDecryptTest(new AesCrypto());
        }

        [Test]
        public void GenerateRandomKeyAsStringTest()
        {
             GenerateRandomKeyAsStringTest(new AesCrypto(), 64);
        }

        
    }
}
