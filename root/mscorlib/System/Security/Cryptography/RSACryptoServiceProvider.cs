using System;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x0200047B RID: 1147
	[ComVisible(true)]
	public sealed class RSACryptoServiceProvider : RSA, ICspAsymmetricAlgorithm
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x00010B05 File Offset: 0x0000ED05
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000ADDBD File Offset: 0x000ABFBD
		// (set) Token: 0x06002F7D RID: 12157 RVA: 0x000ADDC9 File Offset: 0x000ABFC9
		public static bool UseMachineKeyStore
		{
			get
			{
				return RSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
			}
			set
			{
				RSACryptoServiceProvider.s_UseMachineKeyStore = (value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
			}
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000ADDD9 File Offset: 0x000ABFD9
		[SecuritySafeCritical]
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data, offset, count);
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000ADDEF File Offset: 0x000ABFEF
		[SecuritySafeCritical]
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data);
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000ADE04 File Offset: 0x000AC004
		private static int GetAlgorithmId(HashAlgorithmName hashAlgorithm)
		{
			string name = hashAlgorithm.Name;
			if (name == "MD5")
			{
				return 32771;
			}
			if (name == "SHA1")
			{
				return 32772;
			}
			if (name == "SHA256")
			{
				return 32780;
			}
			if (name == "SHA384")
			{
				return 32781;
			}
			if (!(name == "SHA512"))
			{
				throw new CryptographicException(Environment.GetResourceString("'{0}' is not a known hash algorithm.", new object[] { hashAlgorithm.Name }));
			}
			return 32782;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000ADE9C File Offset: 0x000AC09C
		public override byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return this.Encrypt(data, false);
			}
			if (padding == RSAEncryptionPadding.OaepSHA1)
			{
				return this.Encrypt(data, true);
			}
			throw RSACryptoServiceProvider.PaddingModeNotSupported();
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000ADEFC File Offset: 0x000AC0FC
		public override byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return this.Decrypt(data, false);
			}
			if (padding == RSAEncryptionPadding.OaepSHA1)
			{
				return this.Decrypt(data, true);
			}
			throw RSACryptoServiceProvider.PaddingModeNotSupported();
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000ADF5C File Offset: 0x000AC15C
		public override byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding != RSASignaturePadding.Pkcs1)
			{
				throw RSACryptoServiceProvider.PaddingModeNotSupported();
			}
			return this.SignHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm));
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000ADFC0 File Offset: 0x000AC1C0
		public override bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
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
			if (padding != RSASignaturePadding.Pkcs1)
			{
				throw RSACryptoServiceProvider.PaddingModeNotSupported();
			}
			return this.VerifyHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm), signature);
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000AE034 File Offset: 0x000AC234
		private static Exception PaddingModeNotSupported()
		{
			return new CryptographicException(Environment.GetResourceString("Specified padding mode is not valid for this algorithm."));
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000AE045 File Offset: 0x000AC245
		public RSACryptoServiceProvider()
			: this(1024)
		{
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000AE052 File Offset: 0x000AC252
		public RSACryptoServiceProvider(CspParameters parameters)
			: this(1024, parameters)
		{
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000AE060 File Offset: 0x000AC260
		public RSACryptoServiceProvider(int dwKeySize)
		{
			this.privateKeyExportable = true;
			base..ctor();
			this.Common(dwKeySize, false);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000AE078 File Offset: 0x000AC278
		public RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
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

		// Token: 0x06002F8A RID: 12170 RVA: 0x000AE0AC File Offset: 0x000AC2AC
		private void Common(int dwKeySize, bool parameters)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = dwKeySize;
			this.rsa = new RSAManaged(this.KeySize);
			this.rsa.KeyGenerated += this.OnKeyGenerated;
			this.persistKey = parameters;
			if (parameters)
			{
				return;
			}
			CspParameters cspParameters = new CspParameters(1);
			if (RSACryptoServiceProvider.UseMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			this.store = new KeyPairPersistence(cspParameters);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000AE140 File Offset: 0x000AC340
		private void Common(CspParameters p)
		{
			this.store = new KeyPairPersistence(p);
			bool flag = this.store.Load();
			bool flag2 = (p.Flags & CspProviderFlags.UseExistingKey) > CspProviderFlags.NoFlags;
			this.privateKeyExportable = (p.Flags & CspProviderFlags.UseNonExportableKey) == CspProviderFlags.NoFlags;
			if (flag2 && !flag)
			{
				throw new CryptographicException("Keyset does not exist");
			}
			if (this.store.KeyValue != null)
			{
				this.persisted = true;
				this.FromXmlString(this.store.KeyValue);
			}
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000AE1B8 File Offset: 0x000AC3B8
		~RSACryptoServiceProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x00010AD6 File Offset: 0x0000ECD6
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA-PKCS1-KeyEx";
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x000AE1E8 File Offset: 0x000AC3E8
		public override int KeySize
		{
			get
			{
				if (this.rsa == null)
				{
					return this.KeySizeValue;
				}
				return this.rsa.KeySize;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x000AE204 File Offset: 0x000AC404
		// (set) Token: 0x06002F90 RID: 12176 RVA: 0x000AE20C File Offset: 0x000AC40C
		public bool PersistKeyInCsp
		{
			get
			{
				return this.persistKey;
			}
			set
			{
				this.persistKey = value;
				if (this.persistKey)
				{
					this.OnKeyGenerated(this.rsa, null);
				}
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x000AE22A File Offset: 0x000AC42A
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				return this.rsa.PublicOnly;
			}
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000AE238 File Offset: 0x000AC438
		public byte[] Decrypt(byte[] rgb, bool fOAEP)
		{
			if (rgb == null)
			{
				throw new ArgumentNullException("rgb");
			}
			if (rgb.Length > this.KeySize / 8)
			{
				throw new CryptographicException(Environment.GetResourceString("The data to be decrypted exceeds the maximum for this modulus of {0} bytes.", new object[] { this.KeySize / 8 }));
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("rsa");
			}
			AsymmetricKeyExchangeDeformatter asymmetricKeyExchangeDeformatter;
			if (fOAEP)
			{
				asymmetricKeyExchangeDeformatter = new RSAOAEPKeyExchangeDeformatter(this.rsa);
			}
			else
			{
				asymmetricKeyExchangeDeformatter = new RSAPKCS1KeyExchangeDeformatter(this.rsa);
			}
			return asymmetricKeyExchangeDeformatter.DecryptKeyExchange(rgb);
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000AE2BF File Offset: 0x000AC4BF
		public override byte[] DecryptValue(byte[] rgb)
		{
			if (!this.rsa.IsCrtPossible)
			{
				throw new CryptographicException("Incomplete private key - missing CRT.");
			}
			return this.rsa.DecryptValue(rgb);
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000AE2E8 File Offset: 0x000AC4E8
		public byte[] Encrypt(byte[] rgb, bool fOAEP)
		{
			AsymmetricKeyExchangeFormatter asymmetricKeyExchangeFormatter;
			if (fOAEP)
			{
				asymmetricKeyExchangeFormatter = new RSAOAEPKeyExchangeFormatter(this.rsa);
			}
			else
			{
				asymmetricKeyExchangeFormatter = new RSAPKCS1KeyExchangeFormatter(this.rsa);
			}
			return asymmetricKeyExchangeFormatter.CreateKeyExchange(rgb);
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000AE31B File Offset: 0x000AC51B
		public override byte[] EncryptValue(byte[] rgb)
		{
			return this.rsa.EncryptValue(rgb);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000AE32C File Offset: 0x000AC52C
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters && !this.privateKeyExportable)
			{
				throw new CryptographicException("cannot export private key");
			}
			RSAParameters rsaparameters = this.rsa.ExportParameters(includePrivateParameters);
			if (includePrivateParameters)
			{
				if (rsaparameters.D == null)
				{
					throw new ArgumentNullException("Missing D parameter for the private key.");
				}
				if (rsaparameters.P == null || rsaparameters.Q == null || rsaparameters.DP == null || rsaparameters.DQ == null || rsaparameters.InverseQ == null)
				{
					throw new CryptographicException("Missing some CRT parameters for the private key.");
				}
			}
			return rsaparameters;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000AE3A6 File Offset: 0x000AC5A6
		public override void ImportParameters(RSAParameters parameters)
		{
			this.rsa.ImportParameters(parameters);
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000AE3B4 File Offset: 0x000AC5B4
		private HashAlgorithm GetHash(object halg)
		{
			if (halg == null)
			{
				throw new ArgumentNullException("halg");
			}
			HashAlgorithm hashAlgorithm;
			if (halg is string)
			{
				hashAlgorithm = this.GetHashFromString((string)halg);
			}
			else if (halg is HashAlgorithm)
			{
				hashAlgorithm = (HashAlgorithm)halg;
			}
			else
			{
				if (!(halg is Type))
				{
					throw new ArgumentException("halg");
				}
				hashAlgorithm = (HashAlgorithm)Activator.CreateInstance((Type)halg);
			}
			if (hashAlgorithm == null)
			{
				throw new ArgumentException("Could not find provider for halg='" + ((halg != null) ? halg.ToString() : null) + "'.", "halg");
			}
			return hashAlgorithm;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000AE44C File Offset: 0x000AC64C
		private HashAlgorithm GetHashFromString(string name)
		{
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(name);
			if (hashAlgorithm != null)
			{
				return hashAlgorithm;
			}
			HashAlgorithm hashAlgorithm2;
			try
			{
				hashAlgorithm2 = HashAlgorithm.Create(this.GetHashNameFromOID(name));
			}
			catch (CryptographicException ex)
			{
				throw new ArgumentException(ex.Message, "halg", ex);
			}
			return hashAlgorithm2;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000AE49C File Offset: 0x000AC69C
		public byte[] SignData(byte[] buffer, object halg)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.SignData(buffer, 0, buffer.Length, halg);
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000AE4B8 File Offset: 0x000AC6B8
		public byte[] SignData(Stream inputStream, object halg)
		{
			HashAlgorithm hash = this.GetHash(halg);
			byte[] array = hash.ComputeHash(inputStream);
			return PKCS1.Sign_v15(this, hash, array);
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000AE4E0 File Offset: 0x000AC6E0
		public byte[] SignData(byte[] buffer, int offset, int count, object halg)
		{
			HashAlgorithm hash = this.GetHash(halg);
			byte[] array = hash.ComputeHash(buffer, offset, count);
			return PKCS1.Sign_v15(this, hash, array);
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000AE508 File Offset: 0x000AC708
		private string GetHashNameFromOID(string oid)
		{
			if (oid == "1.3.14.3.2.26")
			{
				return "SHA1";
			}
			if (oid == "1.2.840.113549.2.5")
			{
				return "MD5";
			}
			if (oid == "2.16.840.1.101.3.4.2.1")
			{
				return "SHA256";
			}
			if (oid == "2.16.840.1.101.3.4.2.2")
			{
				return "SHA384";
			}
			if (!(oid == "2.16.840.1.101.3.4.2.3"))
			{
				throw new CryptographicException(oid + " is an unsupported hash algorithm for RSA signing");
			}
			return "SHA512";
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000AE588 File Offset: 0x000AC788
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create((str == null) ? "SHA1" : this.GetHashNameFromOID(str));
			return PKCS1.Sign_v15(this, hashAlgorithm, rgbHash);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000AE5C2 File Offset: 0x000AC7C2
		private byte[] SignHash(byte[] rgbHash, int calgHash)
		{
			return PKCS1.Sign_v15(this, RSACryptoServiceProvider.InternalHashToHashAlgorithm(calgHash), rgbHash);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000AE5D4 File Offset: 0x000AC7D4
		private static HashAlgorithm InternalHashToHashAlgorithm(int calgHash)
		{
			if (calgHash == 32771)
			{
				return MD5.Create();
			}
			if (calgHash == 32772)
			{
				return SHA1.Create();
			}
			switch (calgHash)
			{
			case 32780:
				return SHA256.Create();
			case 32781:
				return SHA384.Create();
			case 32782:
				return SHA512.Create();
			default:
				throw new NotImplementedException(calgHash.ToString());
			}
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000AE638 File Offset: 0x000AC838
		public bool VerifyData(byte[] buffer, object halg, byte[] signature)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			HashAlgorithm hash = this.GetHash(halg);
			byte[] array = hash.ComputeHash(buffer);
			return PKCS1.Verify_v15(this, hash, array, signature);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000AE67C File Offset: 0x000AC87C
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create((str == null) ? "SHA1" : this.GetHashNameFromOID(str));
			return PKCS1.Verify_v15(this, hashAlgorithm, rgbHash, rgbSignature);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000AE6C5 File Offset: 0x000AC8C5
		private bool VerifyHash(byte[] rgbHash, int calgHash, byte[] rgbSignature)
		{
			return PKCS1.Verify_v15(this, RSACryptoServiceProvider.InternalHashToHashAlgorithm(calgHash), rgbHash, rgbSignature);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000AE6D5 File Offset: 0x000AC8D5
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.persisted && !this.persistKey)
				{
					this.store.Remove();
				}
				if (this.rsa != null)
				{
					this.rsa.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000AE714 File Offset: 0x000AC914
		private void OnKeyGenerated(object sender, EventArgs e)
		{
			if (this.persistKey && !this.persisted)
			{
				this.store.KeyValue = this.ToXmlString(!this.rsa.PublicOnly);
				this.store.Save();
				this.persisted = true;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000AE762 File Offset: 0x000AC962
		[ComVisible(false)]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			get
			{
				return new CspKeyContainerInfo(this.store.Parameters);
			}
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000AE774 File Offset: 0x000AC974
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
			array[5] = ((this.store != null && this.store.Parameters.KeyNumber == 2) ? 36 : 164);
			return array;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000AE7C0 File Offset: 0x000AC9C0
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			RSA rsa = CryptoConvert.FromCapiKeyBlob(keyBlob);
			if (rsa is RSACryptoServiceProvider)
			{
				RSAParameters rsaparameters = rsa.ExportParameters(!(rsa as RSACryptoServiceProvider).PublicOnly);
				this.ImportParameters(rsaparameters);
			}
			else
			{
				try
				{
					RSAParameters rsaparameters2 = rsa.ExportParameters(true);
					this.ImportParameters(rsaparameters2);
				}
				catch
				{
					RSAParameters rsaparameters3 = rsa.ExportParameters(false);
					this.ImportParameters(rsaparameters3);
				}
			}
			CspParameters cspParameters = new CspParameters(1);
			cspParameters.KeyNumber = ((keyBlob[5] == 36) ? 2 : 1);
			if (RSACryptoServiceProvider.UseMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			this.store = new KeyPairPersistence(cspParameters);
		}

		// Token: 0x0400208B RID: 8331
		private static volatile CspProviderFlags s_UseMachineKeyStore;

		// Token: 0x0400208C RID: 8332
		private const int PROV_RSA_FULL = 1;

		// Token: 0x0400208D RID: 8333
		private const int AT_KEYEXCHANGE = 1;

		// Token: 0x0400208E RID: 8334
		private const int AT_SIGNATURE = 2;

		// Token: 0x0400208F RID: 8335
		private KeyPairPersistence store;

		// Token: 0x04002090 RID: 8336
		private bool persistKey;

		// Token: 0x04002091 RID: 8337
		private bool persisted;

		// Token: 0x04002092 RID: 8338
		private bool privateKeyExportable;

		// Token: 0x04002093 RID: 8339
		private bool m_disposed;

		// Token: 0x04002094 RID: 8340
		private RSAManaged rsa;
	}
}
