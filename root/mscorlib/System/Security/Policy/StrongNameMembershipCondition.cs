using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Policy
{
	// Token: 0x020003F1 RID: 1009
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		// Token: 0x06002AE4 RID: 10980 RVA: 0x0009C914 File Offset: 0x0009AB14
		public StrongNameMembershipCondition(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			this.blob = blob;
			this.name = name;
			if (version != null)
			{
				this.assemblyVersion = (Version)version.Clone();
			}
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x0009C964 File Offset: 0x0009AB64
		internal StrongNameMembershipCondition(SecurityElement e)
		{
			this.FromXml(e);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0009C97A File Offset: 0x0009AB7A
		internal StrongNameMembershipCondition()
		{
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x0009C989 File Offset: 0x0009AB89
		// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x0009C991 File Offset: 0x0009AB91
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x0009C99A File Offset: 0x0009AB9A
		// (set) Token: 0x06002AEA RID: 10986 RVA: 0x0009C9A2 File Offset: 0x0009ABA2
		public Version Version
		{
			get
			{
				return this.assemblyVersion;
			}
			set
			{
				this.assemblyVersion = value;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x0009C9AB File Offset: 0x0009ABAB
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x0009C9B3 File Offset: 0x0009ABB3
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.blob;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PublicKey");
				}
				this.blob = value;
			}
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0009C9CC File Offset: 0x0009ABCC
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				StrongName strongName = obj as StrongName;
				if (strongName != null)
				{
					return strongName.PublicKey.Equals(this.blob) && (this.name == null || !(this.name != strongName.Name)) && (!(this.assemblyVersion != null) || this.assemblyVersion.Equals(strongName.Version));
				}
			}
			return false;
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x0009CA56 File Offset: 0x0009AC56
		public IMembershipCondition Copy()
		{
			return new StrongNameMembershipCondition(this.blob, this.name, this.assemblyVersion);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x0009CA70 File Offset: 0x0009AC70
		public override bool Equals(object o)
		{
			StrongNameMembershipCondition strongNameMembershipCondition = o as StrongNameMembershipCondition;
			if (strongNameMembershipCondition == null)
			{
				return false;
			}
			if (!strongNameMembershipCondition.PublicKey.Equals(this.PublicKey))
			{
				return false;
			}
			if (this.name != strongNameMembershipCondition.Name)
			{
				return false;
			}
			if (this.assemblyVersion != null)
			{
				return this.assemblyVersion.Equals(strongNameMembershipCondition.Version);
			}
			return strongNameMembershipCondition.Version == null;
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0009CADF File Offset: 0x0009ACDF
		public override int GetHashCode()
		{
			return this.blob.GetHashCode();
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0009CAEC File Offset: 0x0009ACEC
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0009CAF8 File Offset: 0x0009ACF8
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this.blob = StrongNamePublicKeyBlob.FromString(e.Attribute("PublicKeyBlob"));
			this.name = e.Attribute("Name");
			string text = e.Attribute("AssemblyVersion");
			if (text == null)
			{
				this.assemblyVersion = null;
				return;
			}
			this.assemblyVersion = new Version(text);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0009CB68 File Offset: 0x0009AD68
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("StrongName - ");
			stringBuilder.Append(this.blob);
			if (this.name != null)
			{
				stringBuilder.AppendFormat(" name = {0}", this.name);
			}
			if (this.assemblyVersion != null)
			{
				stringBuilder.AppendFormat(" version = {0}", this.assemblyVersion);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0009CBCD File Offset: 0x0009ADCD
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x0009CBD8 File Offset: 0x0009ADD8
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(StrongNameMembershipCondition), this.version);
			if (this.blob != null)
			{
				securityElement.AddAttribute("PublicKeyBlob", this.blob.ToString());
			}
			if (this.name != null)
			{
				securityElement.AddAttribute("Name", this.name);
			}
			if (this.assemblyVersion != null)
			{
				string text = this.assemblyVersion.ToString();
				if (text != "0.0")
				{
					securityElement.AddAttribute("AssemblyVersion", text);
				}
			}
			return securityElement;
		}

		// Token: 0x04001E83 RID: 7811
		private readonly int version = 1;

		// Token: 0x04001E84 RID: 7812
		private StrongNamePublicKeyBlob blob;

		// Token: 0x04001E85 RID: 7813
		private string name;

		// Token: 0x04001E86 RID: 7814
		private Version assemblyVersion;
	}
}
