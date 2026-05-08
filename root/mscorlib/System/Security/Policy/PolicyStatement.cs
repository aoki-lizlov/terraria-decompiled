using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003ED RID: 1005
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyStatement : ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002AAF RID: 10927 RVA: 0x0009C0F8 File Offset: 0x0009A2F8
		public PolicyStatement(PermissionSet permSet)
			: this(permSet, PolicyStatementAttribute.Nothing)
		{
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0009C102 File Offset: 0x0009A302
		public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
		{
			if (permSet != null)
			{
				this.perms = permSet.Copy();
				this.perms.SetReadOnly(true);
			}
			this.attrs = attributes;
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x0009C12C File Offset: 0x0009A32C
		// (set) Token: 0x06002AB2 RID: 10930 RVA: 0x0009C154 File Offset: 0x0009A354
		public PermissionSet PermissionSet
		{
			get
			{
				if (this.perms == null)
				{
					this.perms = new PermissionSet(PermissionState.None);
					this.perms.SetReadOnly(true);
				}
				return this.perms;
			}
			set
			{
				this.perms = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x0009C15D File Offset: 0x0009A35D
		// (set) Token: 0x06002AB4 RID: 10932 RVA: 0x0009C165 File Offset: 0x0009A365
		public PolicyStatementAttribute Attributes
		{
			get
			{
				return this.attrs;
			}
			set
			{
				if (value <= PolicyStatementAttribute.All)
				{
					this.attrs = value;
					return;
				}
				throw new ArgumentException(string.Format(Locale.GetText("Invalid value for {0}."), "PolicyStatementAttribute"));
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x0009C18C File Offset: 0x0009A38C
		public string AttributeString
		{
			get
			{
				switch (this.attrs)
				{
				case PolicyStatementAttribute.Exclusive:
					return "Exclusive";
				case PolicyStatementAttribute.LevelFinal:
					return "LevelFinal";
				case PolicyStatementAttribute.All:
					return "Exclusive LevelFinal";
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x0009C1CD File Offset: 0x0009A3CD
		public PolicyStatement Copy()
		{
			return new PolicyStatement(this.perms, this.attrs);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x0009C1E0 File Offset: 0x0009A3E0
		public void FromXml(SecurityElement et)
		{
			this.FromXml(et, null);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x0009C1EC File Offset: 0x0009A3EC
		public void FromXml(SecurityElement et, PolicyLevel level)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PolicyStatement")
			{
				throw new ArgumentException(Locale.GetText("Invalid tag."));
			}
			string text = et.Attribute("Attributes");
			if (text != null)
			{
				this.attrs = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), text);
			}
			SecurityElement securityElement = et.SearchForChildByTag("PermissionSet");
			this.PermissionSet.FromXml(securityElement);
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x0009C26B File Offset: 0x0009A46B
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x0009C274 File Offset: 0x0009A474
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("PolicyStatement");
			securityElement.AddAttribute("version", "1");
			if (this.attrs != PolicyStatementAttribute.Nothing)
			{
				securityElement.AddAttribute("Attributes", this.attrs.ToString());
			}
			securityElement.AddChild(this.PermissionSet.ToXml());
			return securityElement;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0009C2D4 File Offset: 0x0009A4D4
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PolicyStatement policyStatement = obj as PolicyStatement;
			return policyStatement != null && this.PermissionSet.Equals(obj) && this.attrs == policyStatement.attrs;
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x0009C310 File Offset: 0x0009A510
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.PermissionSet.GetHashCode() ^ (int)this.attrs;
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x00098708 File Offset: 0x00096908
		internal static PolicyStatement Empty()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04001E7B RID: 7803
		private PermissionSet perms;

		// Token: 0x04001E7C RID: 7804
		private PolicyStatementAttribute attrs;
	}
}
