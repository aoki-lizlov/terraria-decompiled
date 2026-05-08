using System;
using System.Buffers;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200047A RID: 1146
	[ComVisible(true)]
	public abstract class RSA : AsymmetricAlgorithm
	{
		// Token: 0x06002F53 RID: 12115 RVA: 0x000A7047 File Offset: 0x000A5247
		protected RSA()
		{
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000AD590 File Offset: 0x000AB790
		public new static RSA Create()
		{
			return RSA.Create("System.Security.Cryptography.RSA");
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000AD59C File Offset: 0x000AB79C
		public new static RSA Create(string algName)
		{
			return (RSA)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000AD5A9 File Offset: 0x000AB7A9
		public virtual byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000AD5A9 File Offset: 0x000AB7A9
		public virtual byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000AD5A9 File Offset: 0x000AB7A9
		public virtual byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000AD5A9 File Offset: 0x000AB7A9
		public virtual bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000AD5A9 File Offset: 0x000AB7A9
		protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000AD5A9 File Offset: 0x000AB7A9
		protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000AD5B0 File Offset: 0x000AB7B0
		public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.SignData(data, 0, data.Length, hashAlgorithm, padding);
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000AD5D0 File Offset: 0x000AB7D0
		public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.SignHash(array, hashAlgorithm, padding);
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000AD658 File Offset: 0x000AB858
		public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.SignHash(array, hashAlgorithm, padding);
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000AD6AD File Offset: 0x000AB8AD
		public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm, padding);
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000AD6CC File Offset: 0x000AB8CC
		public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.VerifyHash(array, signature, hashAlgorithm, padding);
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000AD764 File Offset: 0x000AB964
		public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.VerifyHash(array, signature, hashAlgorithm, padding);
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000A75C9 File Offset: 0x000A57C9
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000A75DA File Offset: 0x000A57DA
		internal static Exception HashAlgorithmNameNullOrEmpty()
		{
			return new ArgumentException(Environment.GetResourceString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000AD7CA File Offset: 0x000AB9CA
		public virtual byte[] DecryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("Method is not supported."));
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000AD7CA File Offset: 0x000AB9CA
		public virtual byte[] EncryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("Method is not supported."));
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x000AD7DB File Offset: 0x000AB9DB
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x000AD7DB File Offset: 0x000AB9DB
		public override string SignatureAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000AD7E4 File Offset: 0x000AB9E4
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			RSAParameters rsaparameters = default(RSAParameters);
			SecurityElement topElement = new Parser(xmlString).GetTopElement();
			string text = topElement.SearchForTextOfLocalName("Modulus");
			if (text == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "RSA", "Modulus" }));
			}
			rsaparameters.Modulus = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text));
			string text2 = topElement.SearchForTextOfLocalName("Exponent");
			if (text2 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "RSA", "Exponent" }));
			}
			rsaparameters.Exponent = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text2));
			string text3 = topElement.SearchForTextOfLocalName("P");
			if (text3 != null)
			{
				rsaparameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text3));
			}
			string text4 = topElement.SearchForTextOfLocalName("Q");
			if (text4 != null)
			{
				rsaparameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text4));
			}
			string text5 = topElement.SearchForTextOfLocalName("DP");
			if (text5 != null)
			{
				rsaparameters.DP = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text5));
			}
			string text6 = topElement.SearchForTextOfLocalName("DQ");
			if (text6 != null)
			{
				rsaparameters.DQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text6));
			}
			string text7 = topElement.SearchForTextOfLocalName("InverseQ");
			if (text7 != null)
			{
				rsaparameters.InverseQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text7));
			}
			string text8 = topElement.SearchForTextOfLocalName("D");
			if (text8 != null)
			{
				rsaparameters.D = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text8));
			}
			this.ImportParameters(rsaparameters);
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000AD980 File Offset: 0x000ABB80
		public override string ToXmlString(bool includePrivateParameters)
		{
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<RSAKeyValue>");
			stringBuilder.Append("<Modulus>" + Convert.ToBase64String(rsaparameters.Modulus) + "</Modulus>");
			stringBuilder.Append("<Exponent>" + Convert.ToBase64String(rsaparameters.Exponent) + "</Exponent>");
			if (includePrivateParameters)
			{
				stringBuilder.Append("<P>" + Convert.ToBase64String(rsaparameters.P) + "</P>");
				stringBuilder.Append("<Q>" + Convert.ToBase64String(rsaparameters.Q) + "</Q>");
				stringBuilder.Append("<DP>" + Convert.ToBase64String(rsaparameters.DP) + "</DP>");
				stringBuilder.Append("<DQ>" + Convert.ToBase64String(rsaparameters.DQ) + "</DQ>");
				stringBuilder.Append("<InverseQ>" + Convert.ToBase64String(rsaparameters.InverseQ) + "</InverseQ>");
				stringBuilder.Append("<D>" + Convert.ToBase64String(rsaparameters.D) + "</D>");
			}
			stringBuilder.Append("</RSAKeyValue>");
			return stringBuilder.ToString();
		}

		// Token: 0x06002F6A RID: 12138
		public abstract RSAParameters ExportParameters(bool includePrivateParameters);

		// Token: 0x06002F6B RID: 12139
		public abstract void ImportParameters(RSAParameters parameters);

		// Token: 0x06002F6C RID: 12140 RVA: 0x000ADAC8 File Offset: 0x000ABCC8
		public static RSA Create(int keySizeInBits)
		{
			RSA rsa = RSA.Create();
			RSA rsa2;
			try
			{
				rsa.KeySize = keySizeInBits;
				rsa2 = rsa;
			}
			catch
			{
				rsa.Dispose();
				throw;
			}
			return rsa2;
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x000ADB00 File Offset: 0x000ABD00
		public static RSA Create(RSAParameters parameters)
		{
			RSA rsa = RSA.Create();
			RSA rsa2;
			try
			{
				rsa.ImportParameters(parameters);
				rsa2 = rsa;
			}
			catch
			{
				rsa.Dispose();
				throw;
			}
			return rsa2;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x000ADB38 File Offset: 0x000ABD38
		public virtual bool TryDecrypt(ReadOnlySpan<byte> data, Span<byte> destination, RSAEncryptionPadding padding, out int bytesWritten)
		{
			byte[] array = this.Decrypt(data.ToArray(), padding);
			if (destination.Length >= array.Length)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000ADB7C File Offset: 0x000ABD7C
		public virtual bool TryEncrypt(ReadOnlySpan<byte> data, Span<byte> destination, RSAEncryptionPadding padding, out int bytesWritten)
		{
			byte[] array = this.Encrypt(data.ToArray(), padding);
			if (destination.Length >= array.Length)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000ADBC0 File Offset: 0x000ABDC0
		protected virtual bool TryHashData(ReadOnlySpan<byte> data, Span<byte> destination, HashAlgorithmName hashAlgorithm, out int bytesWritten)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(data.Length);
			byte[] array2;
			try
			{
				data.CopyTo(array);
				array2 = this.HashData(array, 0, data.Length, hashAlgorithm);
			}
			finally
			{
				Array.Clear(array, 0, data.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
			if (destination.Length >= array2.Length)
			{
				new ReadOnlySpan<byte>(array2).CopyTo(destination);
				bytesWritten = array2.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000ADC54 File Offset: 0x000ABE54
		public virtual bool TrySignHash(ReadOnlySpan<byte> hash, Span<byte> destination, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding, out int bytesWritten)
		{
			byte[] array = this.SignHash(hash.ToArray(), hashAlgorithm, padding);
			if (destination.Length >= array.Length)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000ADC9C File Offset: 0x000ABE9C
		public virtual bool TrySignData(ReadOnlySpan<byte> data, Span<byte> destination, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding, out int bytesWritten)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			int num;
			if (this.TryHashData(data, destination, hashAlgorithm, out num) && this.TrySignHash(destination.Slice(0, num), destination, hashAlgorithm, padding, out bytesWritten))
			{
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000ADD04 File Offset: 0x000ABF04
		public virtual bool VerifyData(ReadOnlySpan<byte> data, ReadOnlySpan<byte> signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			int num = 256;
			checked
			{
				bool flag;
				for (;;)
				{
					int num2 = 0;
					byte[] array = ArrayPool<byte>.Shared.Rent(num);
					try
					{
						if (this.TryHashData(data, array, hashAlgorithm, out num2))
						{
							flag = this.VerifyHash(new ReadOnlySpan<byte>(array, 0, num2), signature, hashAlgorithm, padding);
							break;
						}
					}
					finally
					{
						Array.Clear(array, 0, num2);
						ArrayPool<byte>.Shared.Return(array, false);
					}
					num *= 2;
				}
				return flag;
			}
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000ADDA4 File Offset: 0x000ABFA4
		public virtual bool VerifyHash(ReadOnlySpan<byte> hash, ReadOnlySpan<byte> signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			return this.VerifyHash(hash.ToArray(), signature.ToArray(), hashAlgorithm, padding);
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual byte[] ExportRSAPrivateKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual byte[] ExportRSAPublicKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual void ImportRSAPrivateKey(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual void ImportRSAPublicKey(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual bool TryExportRSAPrivateKey(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual bool TryExportRSAPublicKey(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
