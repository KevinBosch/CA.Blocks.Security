using System;
using System.Security.Cryptography;
using System.Text;

namespace CA.Blocks.Security.Cryptography
{
	/// <summary>
	/// This class is a wrapper class for working with the MD5CryptoServiceProvider, it contains a single static method called GenerateHash
	/// </summary>
	/// <remarks>
	/// <para>A simple wrapper function to the MD5CryptoServiceProvider.</para> 
	/// <code outlining="true" source="..\CANC\Common\Security\Cryptography\MD5.cs" lang="cs" 
	/// title="Full Source Code for the MD5 class"/>
	/// </remarks>
	/// <seealso cref="CA.Common"> CA.Common Namespace </seealso>
	public class MD5
	{
		/// <summary>
		/// This function will calculate an MD5 hash for an input string and return the MD5 result as a ToBase64encoded string.
		/// This function is usefull for storing hashes for passwords rather than the actual password it self. This function is case sensitive
		/// </summary>
		/// <remarks>
		/// <para>A simple wrapper function to the MD5CryptoServiceProvider example below</para> 
		/// <code outlining="true" source="..\CANC\Common\UnitTest\Security\Cryptography\MD5_UnitTests.cs" lang="cs" 
		/// title="Example of using the GenerateHash to get a base64 encoded hash result from a string" region="Test_generateMd5Hash" />
		/// </remarks>
		/// <param name="input"> This is a string on which to generate the MD5 hash, 
		/// to get the same hash result the string has to be the same byte for byte, 
		/// even a subtle change will result in a different hash result.</param>
		/// <returns>Will return a string which represents the MD5 hash result.</returns>
		public static byte[] GenerateHash(string input)
		{
			MD5CryptoServiceProvider oMD5 = new MD5CryptoServiceProvider();
			Byte[] bByte = new Byte[input.Length];

			for (int i = 0; i < input.Length; i++)
			{
				bByte[i] = Convert.ToByte(input[i]);
			}
			return oMD5.ComputeHash(bByte);
		}

		public static string GenerateHashAsBase64String(string input)
		{
			return Convert.ToBase64String(GenerateHash(input));
		}


		public static string GenerateHashAsHexadecimalString(string input)
		{
			StringBuilder sb = new StringBuilder();
			byte[] bytes = GenerateHash(input);
			foreach (byte num in bytes)
			{
				sb.Append(num.ToString("x2"));
			}
			return sb.ToString();
		}

		
		/// <summary>
		/// This function will generate an MD5 hash which is insensitive towards the input value. 
		/// This works by calling first converting the input string to lower case then running the 
		/// standard GenerateHash function.
		/// </summary>
		/// <remarks>
		/// <para>A simple wrapper function to the MD5CryptoServiceProvider providing CaseInsensitive hashs</para> 
		/// <code outlining="true" source="..\CANC\Common\UnitTest\Security\Cryptography\MD5_UnitTests.cs" lang="cs" 
		/// title="Example of using the GenerateCaseInsensitiveHash function to get a base64 encoded hash result from a string with is CaseInsensitive" region="Test_GenerateMd5CaseInsensitiveHash" />
		/// </remarks>
		/// <param name="input"> the string on which to generate the the hash result</param>
		/// <returns> Will return a string which represents the MD5 hash result as a base64 encoded string</returns>
		public static string GenerateCaseInsensitiveHashAsBase64String(string input)
		{
			return GenerateHashAsBase64String(input.ToLower());
		}

		public static string GenerateCaseInsensitiveHashAsHexadecimalString(string input)
		{
			return GenerateHashAsHexadecimalString(input.ToLower());
		}


		


	}
}
