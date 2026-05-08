using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	public sealed class ApplicationId
	{
		// Token: 0x06001392 RID: 5010 RVA: 0x0004EF68 File Offset: 0x0004D168
		public ApplicationId(byte[] publicKeyToken, string name, Version version, string processorArchitecture, string culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("ApplicationId cannot have an empty string for the name.");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			if (publicKeyToken == null)
			{
				throw new ArgumentNullException("publicKeyToken");
			}
			this._publicKeyToken = (byte[])publicKeyToken.Clone();
			this.Name = name;
			this.Version = version;
			this.ProcessorArchitecture = processorArchitecture;
			this.Culture = culture;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0004EFED File Offset: 0x0004D1ED
		public string Culture
		{
			[CompilerGenerated]
			get
			{
				return this.<Culture>k__BackingField;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0004EFF5 File Offset: 0x0004D1F5
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0004EFFD File Offset: 0x0004D1FD
		public string ProcessorArchitecture
		{
			[CompilerGenerated]
			get
			{
				return this.<ProcessorArchitecture>k__BackingField;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0004F005 File Offset: 0x0004D205
		public Version Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0004F00D File Offset: 0x0004D20D
		public byte[] PublicKeyToken
		{
			get
			{
				return (byte[])this._publicKeyToken.Clone();
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004F01F File Offset: 0x0004D21F
		public ApplicationId Copy()
		{
			return new ApplicationId(this._publicKeyToken, this.Name, this.Version, this.ProcessorArchitecture, this.Culture);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004F044 File Offset: 0x0004D244
		public override string ToString()
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append(this.Name);
			if (this.Culture != null)
			{
				stringBuilder.Append(", culture=\"");
				stringBuilder.Append(this.Culture);
				stringBuilder.Append('"');
			}
			stringBuilder.Append(", version=\"");
			stringBuilder.Append(this.Version.ToString());
			stringBuilder.Append('"');
			if (this._publicKeyToken != null)
			{
				stringBuilder.Append(", publicKeyToken=\"");
				stringBuilder.Append(ApplicationId.EncodeHexString(this._publicKeyToken));
				stringBuilder.Append('"');
			}
			if (this.ProcessorArchitecture != null)
			{
				stringBuilder.Append(", processorArchitecture =\"");
				stringBuilder.Append(this.ProcessorArchitecture);
				stringBuilder.Append('"');
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004F116 File Offset: 0x0004D316
		private static char HexDigit(int num)
		{
			return (char)((num < 10) ? (num + 48) : (num + 55));
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004F128 File Offset: 0x0004D328
		private static string EncodeHexString(byte[] sArray)
		{
			string text = null;
			if (sArray != null)
			{
				char[] array = new char[sArray.Length * 2];
				int i = 0;
				int num = 0;
				while (i < sArray.Length)
				{
					int num2 = (sArray[i] & 240) >> 4;
					array[num++] = ApplicationId.HexDigit(num2);
					num2 = (int)(sArray[i] & 15);
					array[num++] = ApplicationId.HexDigit(num2);
					i++;
				}
				text = new string(array);
			}
			return text;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004F190 File Offset: 0x0004D390
		public override bool Equals(object o)
		{
			ApplicationId applicationId = o as ApplicationId;
			if (applicationId == null)
			{
				return false;
			}
			if (!object.Equals(this.Name, applicationId.Name) || !object.Equals(this.Version, applicationId.Version) || !object.Equals(this.ProcessorArchitecture, applicationId.ProcessorArchitecture) || !object.Equals(this.Culture, applicationId.Culture))
			{
				return false;
			}
			if (this._publicKeyToken.Length != applicationId._publicKeyToken.Length)
			{
				return false;
			}
			for (int i = 0; i < this._publicKeyToken.Length; i++)
			{
				if (this._publicKeyToken[i] != applicationId._publicKeyToken[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004F233 File Offset: 0x0004D433
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Version.GetHashCode();
		}

		// Token: 0x0400132F RID: 4911
		private readonly byte[] _publicKeyToken;

		// Token: 0x04001330 RID: 4912
		[CompilerGenerated]
		private readonly string <Culture>k__BackingField;

		// Token: 0x04001331 RID: 4913
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x04001332 RID: 4914
		[CompilerGenerated]
		private readonly string <ProcessorArchitecture>k__BackingField;

		// Token: 0x04001333 RID: 4915
		[CompilerGenerated]
		private readonly Version <Version>k__BackingField;
	}
}
