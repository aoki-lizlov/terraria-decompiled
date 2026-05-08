using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x0200049A RID: 1178
	[ComVisible(true)]
	public sealed class DSACryptoServiceProvider : DSA, ICspAsymmetricAlgorithm
	{
		// Token: 0x060030A6 RID: 12454 RVA: 0x000B352A File Offset: 0x000B172A
		public DSACryptoServiceProvider()
			: this(1024)
		{
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000B3537 File Offset: 0x000B1737
		public DSACryptoServiceProvider(CspParameters parameters)
			: this(1024, parameters)
		{
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000B3545 File Offset: 0x000B1745
		public DSACryptoServiceProvider(int dwKeySize)
		{
			this.privateKeyExportable = true;
			base..ctor();
			this.Common(dwKeySize, false);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000B355C File Offset: 0x000B175C
		public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
		{
			this.privateKeyExportable = true;
			base..ctor();
			bool flag = parameters != null;
			this.Common(dwKeySize, flag);
			if (flag)
			{
				this.Common(parameters);
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000B3590 File Offset: 0x000B1790
		private void Common(int dwKeySize, bool parameters)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(512, 1024, 64);
			this.KeySize = dwKeySize;
			this.dsa = new DSAManaged(dwKeySize);
			this.dsa.KeyGenerated += this.OnKeyGenerated;
			this.persistKey = parameters;
			if (parameters)
			{
				return;
			}
			CspParameters cspParameters = new CspParameters(13);
			if (DSACryptoServiceProvider.useMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			this.store = new KeyPairPersistence(cspParameters);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000B3620 File Offset: 0x000B1820
		private void Common(CspParameters parameters)
		{
			this.store = new KeyPairPersistence(parameters);
			this.store.Load();
			if (this.store.KeyValue != null)
			{
				this.persisted = true;
				this.FromXmlString(this.store.KeyValue);
			}
			this.privateKeyExportable = (parameters.Flags & CspProviderFlags.UseNonExportableKey) == CspProviderFlags.NoFlags;
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000B367C File Offset: 0x000B187C
		~DSACryptoServiceProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000B36AC File Offset: 0x000B18AC
		public override int KeySize
		{
			get
			{
				return this.dsa.KeySize;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x000B36B9 File Offset: 0x000B18B9
		// (set) Token: 0x060030B0 RID: 12464 RVA: 0x000B36C1 File Offset: 0x000B18C1
		public bool PersistKeyInCsp
		{
			get
			{
				return this.persistKey;
			}
			set
			{
				this.persistKey = value;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x000B36CA File Offset: 0x000B18CA
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				return this.dsa.PublicOnly;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000124CD File Offset: 0x000106CD
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x000B36D7 File Offset: 0x000B18D7
		// (set) Token: 0x060030B4 RID: 12468 RVA: 0x000B36DE File Offset: 0x000B18DE
		public static bool UseMachineKeyStore
		{
			get
			{
				return DSACryptoServiceProvider.useMachineKeyStore;
			}
			set
			{
				DSACryptoServiceProvider.useMachineKeyStore = value;
			}
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000B36E6 File Offset: 0x000B18E6
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters && !this.privateKeyExportable)
			{
				throw new CryptographicException(Locale.GetText("Cannot export private key"));
			}
			return this.dsa.ExportParameters(includePrivateParameters);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000B370F File Offset: 0x000B190F
		public override void ImportParameters(DSAParameters parameters)
		{
			this.dsa.ImportParameters(parameters);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000B371D File Offset: 0x000B191D
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			return this.dsa.CreateSignature(rgbHash);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000B372C File Offset: 0x000B192C
		public byte[] SignData(byte[] buffer)
		{
			byte[] array = SHA1.Create().ComputeHash(buffer);
			return this.dsa.CreateSignature(array);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000B3754 File Offset: 0x000B1954
		public byte[] SignData(byte[] buffer, int offset, int count)
		{
			byte[] array = SHA1.Create().ComputeHash(buffer, offset, count);
			return this.dsa.CreateSignature(array);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000B377C File Offset: 0x000B197C
		public byte[] SignData(Stream inputStream)
		{
			byte[] array = SHA1.Create().ComputeHash(inputStream);
			return this.dsa.CreateSignature(array);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000B37A1 File Offset: 0x000B19A1
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (string.Compare(str, "SHA1", true, CultureInfo.InvariantCulture) != 0)
			{
				throw new CryptographicException(Locale.GetText("Only SHA1 is supported."));
			}
			return this.dsa.CreateSignature(rgbHash);
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000B37D4 File Offset: 0x000B19D4
		public bool VerifyData(byte[] rgbData, byte[] rgbSignature)
		{
			byte[] array = SHA1.Create().ComputeHash(rgbData);
			return this.dsa.VerifySignature(array, rgbSignature);
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000B37FA File Offset: 0x000B19FA
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (str == null)
			{
				str = "SHA1";
			}
			if (string.Compare(str, "SHA1", true, CultureInfo.InvariantCulture) != 0)
			{
				throw new CryptographicException(Locale.GetText("Only SHA1 is supported."));
			}
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000B3836 File Offset: 0x000B1A36
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000B3848 File Offset: 0x000B1A48
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm != HashAlgorithmName.SHA1)
			{
				throw new CryptographicException(Environment.GetResourceString("'{0}' is not a known hash algorithm.", new object[] { hashAlgorithm.Name }));
			}
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data, offset, count);
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000B3898 File Offset: 0x000B1A98
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm != HashAlgorithmName.SHA1)
			{
				throw new CryptographicException(Environment.GetResourceString("'{0}' is not a known hash algorithm.", new object[] { hashAlgorithm.Name }));
			}
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000B38E4 File Offset: 0x000B1AE4
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.persisted && !this.persistKey)
				{
					this.store.Remove();
				}
				if (this.dsa != null)
				{
					this.dsa.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000B3924 File Offset: 0x000B1B24
		private void OnKeyGenerated(object sender, EventArgs e)
		{
			if (this.persistKey && !this.persisted)
			{
				this.store.KeyValue = this.ToXmlString(!this.dsa.PublicOnly);
				this.store.Save();
				this.persisted = true;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		[MonoTODO("call into KeyPairPersistence to get details")]
		[ComVisible(false)]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000B3974 File Offset: 0x000B1B74
		[ComVisible(false)]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			byte[] array;
			if (includePrivateParameters)
			{
				array = CryptoConvert.ToCapiPrivateKeyBlob(this);
			}
			else
			{
				array = CryptoConvert.ToCapiPublicKeyBlob(this);
			}
			return array;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000B3998 File Offset: 0x000B1B98
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			DSA dsa = CryptoConvert.FromCapiKeyBlobDSA(keyBlob);
			if (dsa is DSACryptoServiceProvider)
			{
				DSAParameters dsaparameters = dsa.ExportParameters(!(dsa as DSACryptoServiceProvider).PublicOnly);
				this.ImportParameters(dsaparameters);
				return;
			}
			try
			{
				DSAParameters dsaparameters2 = dsa.ExportParameters(true);
				this.ImportParameters(dsaparameters2);
			}
			catch
			{
				DSAParameters dsaparameters3 = dsa.ExportParameters(false);
				this.ImportParameters(dsaparameters3);
			}
		}

		// Token: 0x040021DA RID: 8666
		private const int PROV_DSS_DH = 13;

		// Token: 0x040021DB RID: 8667
		private KeyPairPersistence store;

		// Token: 0x040021DC RID: 8668
		private bool persistKey;

		// Token: 0x040021DD RID: 8669
		private bool persisted;

		// Token: 0x040021DE RID: 8670
		private bool privateKeyExportable;

		// Token: 0x040021DF RID: 8671
		private bool m_disposed;

		// Token: 0x040021E0 RID: 8672
		private DSAManaged dsa;

		// Token: 0x040021E1 RID: 8673
		private static bool useMachineKeyStore;
	}
}
