using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000443 RID: 1091
	public sealed class RSAEncryptionPadding : IEquatable<RSAEncryptionPadding>
	{
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x000A5FA0 File Offset: 0x000A41A0
		public static RSAEncryptionPadding Pkcs1
		{
			get
			{
				return RSAEncryptionPadding.s_pkcs1;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000A5FA7 File Offset: 0x000A41A7
		public static RSAEncryptionPadding OaepSHA1
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA1;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x000A5FAE File Offset: 0x000A41AE
		public static RSAEncryptionPadding OaepSHA256
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA256;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000A5FB5 File Offset: 0x000A41B5
		public static RSAEncryptionPadding OaepSHA384
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA384;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x000A5FBC File Offset: 0x000A41BC
		public static RSAEncryptionPadding OaepSHA512
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA512;
			}
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000A5FC3 File Offset: 0x000A41C3
		private RSAEncryptionPadding(RSAEncryptionPaddingMode mode, HashAlgorithmName oaepHashAlgorithm)
		{
			this._mode = mode;
			this._oaepHashAlgorithm = oaepHashAlgorithm;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000A5FD9 File Offset: 0x000A41D9
		public static RSAEncryptionPadding CreateOaep(HashAlgorithmName hashAlgorithm)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(Environment.GetResourceString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
			}
			return new RSAEncryptionPadding(RSAEncryptionPaddingMode.Oaep, hashAlgorithm);
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000A6005 File Offset: 0x000A4205
		public RSAEncryptionPaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x000A600D File Offset: 0x000A420D
		public HashAlgorithmName OaepHashAlgorithm
		{
			get
			{
				return this._oaepHashAlgorithm;
			}
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000A6015 File Offset: 0x000A4215
		public override int GetHashCode()
		{
			return RSAEncryptionPadding.CombineHashCodes(this._mode.GetHashCode(), this._oaepHashAlgorithm.GetHashCode());
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x0002B7DF File Offset: 0x000299DF
		private static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000A603E File Offset: 0x000A423E
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSAEncryptionPadding);
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000A604C File Offset: 0x000A424C
		public bool Equals(RSAEncryptionPadding other)
		{
			return other != null && this._mode == other._mode && this._oaepHashAlgorithm == other._oaepHashAlgorithm;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000A6078 File Offset: 0x000A4278
		public static bool operator ==(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000A6089 File Offset: 0x000A4289
		public static bool operator !=(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			return !(left == right);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000A6095 File Offset: 0x000A4295
		public override string ToString()
		{
			return this._mode.ToString() + this._oaepHashAlgorithm.Name;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000A60B8 File Offset: 0x000A42B8
		// Note: this type is marked as 'beforefieldinit'.
		static RSAEncryptionPadding()
		{
		}

		// Token: 0x04001FE4 RID: 8164
		private static readonly RSAEncryptionPadding s_pkcs1 = new RSAEncryptionPadding(RSAEncryptionPaddingMode.Pkcs1, default(HashAlgorithmName));

		// Token: 0x04001FE5 RID: 8165
		private static readonly RSAEncryptionPadding s_oaepSHA1 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA1);

		// Token: 0x04001FE6 RID: 8166
		private static readonly RSAEncryptionPadding s_oaepSHA256 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA256);

		// Token: 0x04001FE7 RID: 8167
		private static readonly RSAEncryptionPadding s_oaepSHA384 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA384);

		// Token: 0x04001FE8 RID: 8168
		private static readonly RSAEncryptionPadding s_oaepSHA512 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA512);

		// Token: 0x04001FE9 RID: 8169
		private RSAEncryptionPaddingMode _mode;

		// Token: 0x04001FEA RID: 8170
		private HashAlgorithmName _oaepHashAlgorithm;
	}
}
