using System.Security.Cryptography;

namespace CA.Blocks.Security.Cryptography
{
    /// <summary>
    /// Implements the CryptoBase using the RC2CryptoServiceProvider as the SymmetricAlgorithm
    /// </summary>
    public class RC2Crypto : CryptoBase, IEncryption
    {
        private readonly RC2CryptoServiceProvider _algorithm = new RC2CryptoServiceProvider();

        /// <summary>
        /// RC2 requires a 128 bit key (32 hexadecimal characters 16 bytes).
        /// </summary>
        public RC2Crypto ()
        {
            _keySizeInBytes = 16;
        }

        /// <summary>
        /// Gets Algorithm as a RC2CryptoServiceProvider
        /// </summary>
        public override SymmetricAlgorithm Algorithm
        {
            get { return _algorithm; }
        }

    }
}
