using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CA.Blocks.Security.Cryptography
{
    /// <summary>
    /// Is abstract class used to encapsulate the inner workings of Encrypt and Decrypt methods amongst mutiple 
    /// SymmetricAlgorithms. This class provides the guts which can then simply be inherited by specilised 
    /// SymmetricAlgorithms such as DES, triple DES, RC2.
    /// </summary>
    public abstract class CryptoBase 
    {
        protected int _keySizeInBytes = -1; // this must be set by the inheriting class

        /// <summary>
        /// This is a abstract pointer to the CryptoServiceProvider which implements the SymmetricAlgorithm
        /// </summary>
        public abstract SymmetricAlgorithm Algorithm { get; }


        /// <summary>
        /// This function will generate a Crypto key from the hash of the text key aka password. 
        /// We use Sha256 hash in order to generate a 256bit hash so there are no repeating groups up to 256 bits
        /// if an Algorithm which requires more than 256bits is used we start to reuse the bits from the beginning
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] GenerateCryptoKey(string key)
        {
            byte[] result = new byte[_keySizeInBytes];
            SHA256CryptoServiceProvider hashSha256 = new SHA256CryptoServiceProvider();
            byte[] hashKey = hashSha256.ComputeHash(Encoding.ASCII.GetBytes(key));

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = hashKey[i % hashKey.Length];
            }
            return result;
        }

        /// <summary>
        /// Will generate a random key to be used with the Algorithm
        /// <ol>
        /// <li>AES requires a 256 bit key (64 hexadecimal characters, 32 bytes).</li>
        /// <li>MD5 requires a 128 bit key (32 hexadecimal characters, 16 bytes).</li>
        /// <li>SHA1 requires a 160 bit key (40 hexadecimal characters, 20 bytes).</li>
        /// <li>DES requires a 64 bit key (16 hexadecimal characters, 8 bytes).</li>
        /// <li>3DES requires a 192 bit key (48 hexadecimal characters, 24 bytes).</li>
        /// <li>RC2 requires a 128 bit key (32 hexadecimal characters 16 bytes).</li>
        /// <li>HMACSHA256 requires a 256 bit key (64 hexadecimal characters, 32 bytes).</li>
        /// <li>HMACSHA384 requires a 384 bit key (96 hexadecimal characters, 48 bytes).</li>
        /// <li>HMACSHA512 requires a 512 bit key (128 hexadecimal characters, 64 bytes).</li>
        /// </ol>
        /// </summary>
        /// <returns> the key as a byte array</returns>
        public byte[] GenerateRandomKey()
        {
            byte[] buff = new byte[_keySizeInBytes];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            return buff;
        }

        /// <summary>
        /// Will generate a random key to be used with the Algorithm
        /// <ol>
        /// <li>AES requires a 256 bit key (64 hexadecimal characters, 32 bytes).</li>
        /// <li>MD5 requires a 128 bit key (32 hexadecimal characters, 16 bytes).</li>
        /// <li>SHA1 requires a 160 bit key (40 hexadecimal characters, 20 bytes).</li>
        /// <li>DES requires a 64 bit key (16 hexadecimal characters, 8 bytes).</li>
        /// <li>3DES requires a 192 bit key (48 hexadecimal characters, 24 bytes).</li>
        /// <li>RC2 requires a 128 bit key (32 hexadecimal characters 16 bytes).</li>
        /// <li>HMACSHA256 requires a 256 bit key (64 hexadecimal characters, 32 bytes).</li>
        /// <li>HMACSHA384 requires a 384 bit key (96 hexadecimal characters, 48 bytes).</li>
        /// <li>HMACSHA512 requires a 512 bit key (128 hexadecimal characters, 64 bytes).</li>
        /// </ol>
        /// </summary>
        /// <returns> the key as a hexadecimal string</returns>
        public string GenerateRandomKeyAsString()
        {
            byte[] keyAsBytes = GenerateRandomKey();
            StringBuilder sb = new StringBuilder(_keySizeInBytes * 2);
            for (int i = 0; i < keyAsBytes.Length; i++)
                sb.Append(string.Format("{0:X2}", keyAsBytes[i]));
            return sb.ToString();
        }


        /// <summary>
        /// Encrypts text with SymmetricAlgorithm provided using the supplied key
        /// </summary>
        /// <param name="plaintext">The text to encrypt</param>
        /// <param name="key">Key to use for encryption</param>
        /// <returns>The encrypted string represented as base 64 text</returns>
        public  string Encrypt(string plaintext, string key)
        {
            Algorithm.Key = GenerateCryptoKey(key);
            Algorithm.Mode = CipherMode.ECB;
            ICryptoTransform transformer = Algorithm.CreateEncryptor();
            byte[] buffer = Encoding.ASCII.GetBytes(plaintext);
            return Convert.ToBase64String(transformer.TransformFinalBlock(buffer, 0, buffer.Length));
        }


        /// <summary>
        /// Encrypts text with SymmetricAlgorithm provided using the supplied key
        /// </summary>
        /// <param name="sourceFilePath">Fie full file path for the file to encrypt</param>
        /// <param name="encryptedFilePathName">Fie full file path for the file to encrypt</param>
        /// <param name="key">Key to use for encryption</param>
        /// <returns>The file path of the encrypted file string represented as base 64 text</returns>
        public void EncryptFile(string sourceFilePath, string encryptedFilePathName, string key)
        {
            const int chunkSize = 1024;
            Algorithm.Key = GenerateCryptoKey(key);
            Algorithm.Mode = CipherMode.ECB;
            ICryptoTransform transformer = Algorithm.CreateEncryptor();

            using (FileStream fsOutput = File.OpenWrite(encryptedFilePathName))
            {
                using (FileStream fsInput = File.OpenRead(sourceFilePath))
                {         
                    using (CryptoStream cryptoStream = new CryptoStream(fsOutput, transformer, CryptoStreamMode.Write))
                    {
          
                        for (long i = 0; i < fsInput.Length; i += chunkSize)
                        {
                            byte[] chunkData = new byte[chunkSize];
                            int bytesRead = 0;
                            while ((bytesRead = fsInput.Read(chunkData, 0, chunkSize)) > 0)
                            {
                                // go a null out the last bytes ready from the chunk
                                for (int x = bytesRead; x < chunkSize; x++)
                                    chunkData[x] = 0;
     
                                cryptoStream.Write(chunkData, 0, bytesRead);
                            }
                        }
                        cryptoStream.FlushFinalBlock();
                    }
                }
            }
        }

        public void DecryptFile(string sourceFilePath, string decryptFilePathName, string key)
        {
            const int chunkSize = 1024;
            Algorithm.Key = GenerateCryptoKey(key);
            Algorithm.Mode = CipherMode.ECB;
            ICryptoTransform transformer = Algorithm.CreateDecryptor();



            using (FileStream fsInput = File.OpenRead(sourceFilePath))
            {
                using (FileStream fsOutput = File.OpenWrite(decryptFilePathName))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(fsOutput, transformer, CryptoStreamMode.Write))
                    {
        
                        for (long i = 0; i < fsInput.Length; i += chunkSize)
                        {
                            byte[] chunkData = new byte[chunkSize];
                            int bytesRead = 0;
                            while ((bytesRead = fsInput.Read(chunkData, 0, chunkSize)) > 0)
                            {
                                cryptoStream.Write(chunkData, 0, bytesRead);
                            }
                        }
                        cryptoStream.FlushFinalBlock();
                    }
                }
            }
        }


        /// <summary>
        /// Decrypts the base 64 Encoded  string with SymmetricAlgorithm provided using the supplied key
        /// </summary>
        /// <param name="base64Text">encrypted base64 text</param>
        /// <param name="key">Decryption Key</param>
        /// <returns>The decrypted string</returns>
        public string Decrypt(string base64Text, string key)
        {
            Algorithm.Key = GenerateCryptoKey(key);
            Algorithm.Mode = CipherMode.ECB;
            ICryptoTransform transformer = Algorithm.CreateDecryptor();
            byte[] buffer = Convert.FromBase64String(base64Text);
            return Encoding.ASCII.GetString( transformer.TransformFinalBlock(buffer, 0, buffer.Length));
        }

    }
}
