using CA.Blocks.Security.Cryptography;
using CA.Blocks.SecurityTests.Cryptography;
using NUnit.Framework;

namespace CA.Common.SecurityTests.Cryptography
{
    [TestFixture]
    public class RC2CryptoUnitTest : CryptoBaseUnitTest
    {

        [Test]
        public void EncryptDecryptTest()
        {
            EncryptDecryptTest(new RC2Crypto());
        }

        [Test]
        public void GenerateRandomKeyAsStringTest()
        {
            GenerateRandomKeyAsStringTest(new RC2Crypto(), 32);
        }

        
    }
}
