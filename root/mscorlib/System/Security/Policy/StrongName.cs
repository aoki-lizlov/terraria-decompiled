using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003F0 RID: 1008
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongName : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		// Token: 0x06002AD8 RID: 10968 RVA: 0x0009C768 File Offset: 0x0009A968
		public StrongName(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Empty"), "name");
			}
			this.publickey = blob;
			this.name = name;
			this.version = version;
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x0009C7DD File Offset: 0x0009A9DD
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x0009C7E5 File Offset: 0x0009A9E5
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.publickey;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x0009C7ED File Offset: 0x0009A9ED
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0009C7F5 File Offset: 0x0009A9F5
		public object Copy()
		{
			return new StrongName(this.publickey, this.name, this.version);
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0009C80E File Offset: 0x0009AA0E
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new StrongNameIdentityPermission(this.publickey, this.name, this.version);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x0009C828 File Offset: 0x0009AA28
		public override bool Equals(object o)
		{
			StrongName strongName = o as StrongName;
			return strongName != null && !(this.name != strongName.Name) && this.Version.Equals(strongName.Version) && this.PublicKey.Equals(strongName.PublicKey);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0009C87C File Offset: 0x0009AA7C
		public override int GetHashCode()
		{
			return this.publickey.GetHashCode();
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0009C88C File Offset: 0x0009AA8C
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(typeof(StrongName).Name);
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Key", this.publickey.ToString());
			securityElement.AddAttribute("Name", this.name);
			securityElement.AddAttribute("Version", this.version.ToString());
			return securityElement.ToString();
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0009C8FF File Offset: 0x0009AAFF
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 5 : 1) + this.name.Length;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x04001E80 RID: 7808
		private StrongNamePublicKeyBlob publickey;

		// Token: 0x04001E81 RID: 7809
		private string name;

		// Token: 0x04001E82 RID: 7810
		private Version version;
	}
}
