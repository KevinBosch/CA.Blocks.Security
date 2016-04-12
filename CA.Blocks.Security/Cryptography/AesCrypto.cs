using System.Security.Cryptography;

namespace CA.Blocks.Security.Cryptography
{
    /// <summary>
    /// Implements the CryptoBase using the AesCryptoServiceProvider as the SymmetricAlgorithm
    /// </summary>
    public class AesCrypto : CryptoBase, IEncryption 
    {
        private readonly AesCryptoServiceProvider _algorithm = new AesCryptoServiceProvider();

        /// <summary>
        /// AES requires a 256 bit key (64 hexadecimal characters 32 bytes).
        /// </summary>
        public AesCrypto()
        {
            _keySizeInBytes = 32;
        }

        /// <summary>
        /// Gets Algorithm as a AesCryptoServiceProvider
        /// </summary>
        public override SymmetricAlgorithm Algorithm
        {
            get { return _algorithm; }
        }

    }
}
