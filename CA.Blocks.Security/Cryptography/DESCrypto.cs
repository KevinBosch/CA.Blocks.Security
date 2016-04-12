using System.Security.Cryptography;

namespace CA.Blocks.Security.Cryptography
{
    /// <summary>
    /// Implements the CryptoBase using the DESCryptoServiceProvider as the SymmetricAlgorithm
    /// </summary>
    public class DESCrypto : CryptoBase, IEncryption
    {
        private readonly DESCryptoServiceProvider _algorithm = new DESCryptoServiceProvider();

        public DESCrypto()
        {
            _keySizeInBytes = 8;
        }

        /// <summary>
        /// Gets Algorithm as a DESCryptoServiceProvider
        /// </summary>
        public override SymmetricAlgorithm Algorithm
        {
            get { return _algorithm; }
        }
    }
}
