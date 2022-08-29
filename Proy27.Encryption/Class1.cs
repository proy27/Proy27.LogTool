using System;
using System.Security.Cryptography;
using System.Text;

namespace Proy27.Encryption
{
	public static class Encryption
	{
		/// <summary>
		/// SHA256 encrypt
		/// </summary>
		/// <param name="srcString">The string to be encrypted</param>
		/// <returns></returns>
		public static string Sha256(this string srcString)
		{
			SHA256 sha256 = SHA256.Create();
			byte[] bytes_sha256_in = Encoding.UTF8.GetBytes(srcString);
			byte[] bytes_sha256_out = sha256.ComputeHash(bytes_sha256_in);
			string str_sha256_out = BitConverter.ToString(bytes_sha256_out);
			str_sha256_out = str_sha256_out.Replace("-", "");
			return str_sha256_out;
		}

		/// <summary>
		/// SHA512 encrypt
		/// </summary>
		/// <param name="srcString">The string to be encrypted</param>
		/// <returns></returns>
		public static string Sha512(this string srcString)
		{
			SHA512 sha512 = SHA512.Create();
			byte[] bytes_sha512_in = Encoding.UTF8.GetBytes(srcString);
			byte[] bytes_sha512_out = sha512.ComputeHash(bytes_sha512_in);
			string str_sha512_out = BitConverter.ToString(bytes_sha512_out);
			str_sha512_out = str_sha512_out.Replace("-", "");
			return str_sha512_out;
		}

		public static string AesDescString(this string str, string pass)
		{
			// var bytes = Convert.FromBase64String(str);
			var bytes = FromBase32String(str);
			var resS = AesDec(bytes, pass);
			return Encoding.UTF8.GetString(resS);
		}

		public static string AesEncString(this string str, string pass)
		{
			var bytes = AesEnc(Encoding.UTF8.GetBytes(str), pass);
			// return Convert.ToBase64String(bytes);
			return ToBase32String(bytes);
		}

		private static byte[] AesEnc(this byte[] bsFile, string pass)
		{
			RijndaelManaged aes = new RijndaelManaged();

			aes.KeySize = 256;
			aes.Padding = PaddingMode.PKCS7;
			aes.Key = Encoding.UTF8.GetBytes(pass);
			aes.IV = Encoding.UTF8.GetBytes(pass.Substring(8, 16));

			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;

			ICryptoTransform transform = aes.CreateEncryptor();
			return transform.TransformFinalBlock(bsFile, 0, bsFile.Length);
		}

		private static byte[] AesDec(this byte[] bsFile, string pass)
		{
			RijndaelManaged aes = new RijndaelManaged();
			aes.KeySize = 256;
			aes.Padding = PaddingMode.PKCS7;
			aes.Key = Encoding.UTF8.GetBytes(pass);
			aes.IV = Encoding.UTF8.GetBytes(pass.Substring(8, 16));
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;

			ICryptoTransform transform = aes.CreateDecryptor();
			return transform.TransformFinalBlock(bsFile, 0, bsFile.Length);
		}


		// the valid chars for the encoding
		private const string ValidChars = "QAZ2WSX3" + "EDC4RFV5" + "TGB6YHN7" + "UJM8K9LP";

		/// <summary>
		/// Converts an array of bytes to a Base32-k string.
		/// </summary>
		private static string ToBase32String(byte[] bytes)
		{
			StringBuilder sb = new StringBuilder();         // holds the base32 chars
			byte index;
			int hi = 5;
			int currentByte = 0;

			while (currentByte < bytes.Length)
			{
				// do we need to use the next byte?
				if (hi > 8)
				{
					// get the last piece from the current byte, shift it to the right
					// and increment the byte counter
					index = (byte)(bytes[currentByte++] >> (hi - 5));
					if (currentByte != bytes.Length)
					{
						// if we are not at the end, get the first piece from
						// the next byte, clear it and shift it to the left
						index = (byte)(((byte)(bytes[currentByte] << (16 - hi)) >> 3) | index);
					}

					hi -= 3;
				}
				else if (hi == 8)
				{
					index = (byte)(bytes[currentByte++] >> 3);
					hi -= 3;
				}
				else
				{

					// simply get the stuff from the current byte
					index = (byte)((byte)(bytes[currentByte] << (8 - hi)) >> 3);
					hi += 5;
				}

				sb.Append(ValidChars[index]);
			}

			return sb.ToString();
		}


		/// <summary>
		/// Converts a Base32-k string into an array of bytes.
		/// </summary>
		/// <exception cref="System.ArgumentException">
		/// Input string <paramref name="s">s</paramref> contains invalid Base32-k characters.
		/// </exception>
		private static byte[] FromBase32String(string str)
		{
			int numBytes = str.Length * 5 / 8;
			byte[] bytes = new Byte[numBytes];

			// all UPPERCASE chars
			str = str.ToUpper();

			int bit_buffer;
			int currentCharIndex;
			int bits_in_buffer;

			if (str.Length < 3)
			{
				bytes[0] = (byte)(ValidChars.IndexOf(str[0]) | ValidChars.IndexOf(str[1]) << 5);
				return bytes;
			}

			bit_buffer = (ValidChars.IndexOf(str[0]) | ValidChars.IndexOf(str[1]) << 5);
			bits_in_buffer = 10;
			currentCharIndex = 2;
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = (byte)bit_buffer;
				bit_buffer >>= 8;
				bits_in_buffer -= 8;
				while (bits_in_buffer < 8 && currentCharIndex < str.Length)
				{
					bit_buffer |= ValidChars.IndexOf(str[currentCharIndex++]) << bits_in_buffer;
					bits_in_buffer += 5;
				}
			}

			return bytes;
		}
	}
}
