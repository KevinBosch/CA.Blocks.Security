using CA.Blocks.Security.Cryptography;
using CA.Blocks.SecurityTests.Cryptography;
using NUnit.Framework;

namespace CA.Common.SecurityTests.Cryptography
{
    [TestFixture]
    public class DESCryptoUnitTest : CryptoBaseUnitTest
    {

        [Test]
        public void EncryptDecryptTest()
        {
            EncryptDecryptTest(new DESCrypto());
        }

        [Test]
        public void GenerateRandomKeyAsStringTest()
        {
            GenerateRandomKeyAsStringTest(new DESCrypto(), 16);
        }
    }
}
