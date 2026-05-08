using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000448 RID: 1096
	[ComVisible(true)]
	public abstract class AsymmetricAlgorithm : IDisposable
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x000025BE File Offset: 0x000007BE
		protected AsymmetricAlgorithm()
		{
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000A6273 File Offset: 0x000A4473
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000A627B File Offset: 0x000A447B
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000A628A File Offset: 0x000A448A
		// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x000A6294 File Offset: 0x000A4494
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalKeySizesValue.Length; i++)
				{
					if (this.LegalKeySizesValue[i].SkipSize == 0)
					{
						if (this.LegalKeySizesValue[i].MinSize == value)
						{
							this.KeySizeValue = value;
							return;
						}
					}
					else
					{
						for (int j = this.LegalKeySizesValue[i].MinSize; j <= this.LegalKeySizesValue[i].MaxSize; j += this.LegalKeySizesValue[i].SkipSize)
						{
							if (j == value)
							{
								this.KeySizeValue = value;
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000A6326 File Offset: 0x000A4526
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual string SignatureAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual string KeyExchangeAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000A6338 File Offset: 0x000A4538
		public static AsymmetricAlgorithm Create()
		{
			return AsymmetricAlgorithm.Create("System.Security.Cryptography.AsymmetricAlgorithm");
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000A6344 File Offset: 0x000A4544
		public static AsymmetricAlgorithm Create(string algName)
		{
			return (AsymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual void FromXmlString(string xmlString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual string ToXmlString(bool includePrivateParameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual byte[] ExportEncryptedPkcs8PrivateKey(ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual byte[] ExportEncryptedPkcs8PrivateKey(ReadOnlySpan<char> password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual byte[] ExportPkcs8PrivateKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual byte[] ExportSubjectPublicKeyInfo()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual void ImportEncryptedPkcs8PrivateKey(ReadOnlySpan<byte> passwordBytes, ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual void ImportEncryptedPkcs8PrivateKey(ReadOnlySpan<char> password, ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual void ImportPkcs8PrivateKey(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual void ImportSubjectPublicKeyInfo(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual bool TryExportEncryptedPkcs8PrivateKey(ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters, Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual bool TryExportEncryptedPkcs8PrivateKey(ReadOnlySpan<char> password, PbeParameters pbeParameters, Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual bool TryExportPkcs8PrivateKey(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual bool TryExportSubjectPublicKeyInfo(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x04001FF6 RID: 8182
		protected int KeySizeValue;

		// Token: 0x04001FF7 RID: 8183
		protected KeySizes[] LegalKeySizesValue;
	}
}
