using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000442 RID: 1090
	public readonly struct HashAlgorithmName : IEquatable<HashAlgorithmName>
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000A5EEA File Offset: 0x000A40EA
		public static HashAlgorithmName MD5
		{
			get
			{
				return new HashAlgorithmName("MD5");
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x000A5EF6 File Offset: 0x000A40F6
		public static HashAlgorithmName SHA1
		{
			get
			{
				return new HashAlgorithmName("SHA1");
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x000A5F02 File Offset: 0x000A4102
		public static HashAlgorithmName SHA256
		{
			get
			{
				return new HashAlgorithmName("SHA256");
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x000A5F0E File Offset: 0x000A410E
		public static HashAlgorithmName SHA384
		{
			get
			{
				return new HashAlgorithmName("SHA384");
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000A5F1A File Offset: 0x000A411A
		public static HashAlgorithmName SHA512
		{
			get
			{
				return new HashAlgorithmName("SHA512");
			}
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000A5F26 File Offset: 0x000A4126
		public HashAlgorithmName(string name)
		{
			this._name = name;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000A5F2F File Offset: 0x000A412F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000A5F37 File Offset: 0x000A4137
		public override string ToString()
		{
			return this._name ?? string.Empty;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000A5F48 File Offset: 0x000A4148
		public override bool Equals(object obj)
		{
			return obj is HashAlgorithmName && this.Equals((HashAlgorithmName)obj);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000A5F60 File Offset: 0x000A4160
		public bool Equals(HashAlgorithmName other)
		{
			return this._name == other._name;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000A5F73 File Offset: 0x000A4173
		public override int GetHashCode()
		{
			if (this._name != null)
			{
				return this._name.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000A5F8A File Offset: 0x000A418A
		public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000A5F94 File Offset: 0x000A4194
		public static bool operator !=(HashAlgorithmName left, HashAlgorithmName right)
		{
			return !(left == right);
		}

		// Token: 0x04001FE3 RID: 8163
		private readonly string _name;
	}
}
