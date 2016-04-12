using System.Security.Cryptography;

namespace CA.Blocks.Security.Cryptography
{
    /// <summary>
    /// Implements the CryptoBase using the TripleDESCryptoServiceProvider as the SymmetricAlgorithm
    /// </summary>
    public class TripleDESCrypto : CryptoBase, IEncryption
    {
        private readonly TripleDESCryptoServiceProvider _algorithm = new TripleDESCryptoServiceProvider();

        /// <summary>
        /// 3DES requires a 192 bit key (48 hexadecimal characters, 24 bytes).
        /// </summary>
        public TripleDESCrypto()
        {
            _keySizeInBytes = 24;
        }

        /// <summary>
        /// Gets Algorithm as a TripleDESCryptoServiceProvider
        /// </summary>
        public override SymmetricAlgorithm Algorithm
        {
            get { return _algorithm; }
        }
    }
}
