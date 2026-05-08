using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003D7 RID: 983
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeGroup
	{
		// Token: 0x060029CF RID: 10703 RVA: 0x00098BD2 File Offset: 0x00096DD2
		protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
		{
			if (membershipCondition == null)
			{
				throw new ArgumentNullException("membershipCondition");
			}
			if (policy != null)
			{
				this.m_policy = policy.Copy();
			}
			this.m_membershipCondition = membershipCondition.Copy();
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x00098C0E File Offset: 0x00096E0E
		internal CodeGroup(SecurityElement e, PolicyLevel level)
		{
			this.FromXml(e, level);
		}

		// Token: 0x060029D1 RID: 10705
		public abstract CodeGroup Copy();

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060029D2 RID: 10706
		public abstract string MergeLogic { get; }

		// Token: 0x060029D3 RID: 10707
		public abstract PolicyStatement Resolve(Evidence evidence);

		// Token: 0x060029D4 RID: 10708
		public abstract CodeGroup ResolveMatchingCodeGroups(Evidence evidence);

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x00098C29 File Offset: 0x00096E29
		// (set) Token: 0x060029D6 RID: 10710 RVA: 0x00098C31 File Offset: 0x00096E31
		public PolicyStatement PolicyStatement
		{
			get
			{
				return this.m_policy;
			}
			set
			{
				this.m_policy = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x00098C3A File Offset: 0x00096E3A
		// (set) Token: 0x060029D8 RID: 10712 RVA: 0x00098C42 File Offset: 0x00096E42
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x00098C4B File Offset: 0x00096E4B
		// (set) Token: 0x060029DA RID: 10714 RVA: 0x00098C53 File Offset: 0x00096E53
		public IMembershipCondition MembershipCondition
		{
			get
			{
				return this.m_membershipCondition;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("value");
				}
				this.m_membershipCondition = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x00098C6A File Offset: 0x00096E6A
		// (set) Token: 0x060029DC RID: 10716 RVA: 0x00098C72 File Offset: 0x00096E72
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x00098C7B File Offset: 0x00096E7B
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x00098C83 File Offset: 0x00096E83
		public IList Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_children = new ArrayList(value);
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x00098C9F File Offset: 0x00096E9F
		public virtual string AttributeString
		{
			get
			{
				if (this.m_policy != null)
				{
					return this.m_policy.AttributeString;
				}
				return null;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x00098CB6 File Offset: 0x00096EB6
		public virtual string PermissionSetName
		{
			get
			{
				if (this.m_policy == null)
				{
					return null;
				}
				if (this.m_policy.PermissionSet is NamedPermissionSet)
				{
					return ((NamedPermissionSet)this.m_policy.PermissionSet).Name;
				}
				return null;
			}
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x00098CEB File Offset: 0x00096EEB
		public void AddChild(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.m_children.Add(group.Copy());
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00098D10 File Offset: 0x00096F10
		public override bool Equals(object o)
		{
			CodeGroup codeGroup = o as CodeGroup;
			return codeGroup != null && this.Equals(codeGroup, false);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x00098D34 File Offset: 0x00096F34
		public bool Equals(CodeGroup cg, bool compareChildren)
		{
			if (cg.Name != this.Name)
			{
				return false;
			}
			if (cg.Description != this.Description)
			{
				return false;
			}
			if (!cg.MembershipCondition.Equals(this.m_membershipCondition))
			{
				return false;
			}
			if (compareChildren)
			{
				int count = cg.Children.Count;
				if (this.Children.Count != count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					if (!((CodeGroup)this.Children[i]).Equals((CodeGroup)cg.Children[i], false))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x00098DD8 File Offset: 0x00096FD8
		public void RemoveChild(CodeGroup group)
		{
			if (group != null)
			{
				this.m_children.Remove(group);
			}
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x00098DEC File Offset: 0x00096FEC
		public override int GetHashCode()
		{
			int num = this.m_membershipCondition.GetHashCode();
			if (this.m_policy != null)
			{
				num += this.m_policy.GetHashCode();
			}
			return num;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x00098E1C File Offset: 0x0009701C
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x00098E28 File Offset: 0x00097028
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			string text = e.Attribute("PermissionSetName");
			PermissionSet permissionSet;
			if (text != null && level != null)
			{
				permissionSet = level.GetNamedPermissionSet(text);
			}
			else
			{
				SecurityElement securityElement = e.SearchForChildByTag("PermissionSet");
				if (securityElement != null)
				{
					permissionSet = (PermissionSet)Activator.CreateInstance(Type.GetType(securityElement.Attribute("class")), true);
					permissionSet.FromXml(securityElement);
				}
				else
				{
					permissionSet = new PermissionSet(new PermissionSet(PermissionState.None));
				}
			}
			this.m_policy = new PolicyStatement(permissionSet);
			this.m_children.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				foreach (object obj in e.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2.Tag == "CodeGroup")
					{
						this.AddChild(CodeGroup.CreateFromXml(securityElement2, level));
					}
				}
			}
			this.m_membershipCondition = null;
			SecurityElement securityElement3 = e.SearchForChildByTag("IMembershipCondition");
			if (securityElement3 != null)
			{
				string text2 = securityElement3.Attribute("class");
				Type type = Type.GetType(text2);
				if (type == null)
				{
					type = Type.GetType("System.Security.Policy." + text2);
				}
				this.m_membershipCondition = (IMembershipCondition)Activator.CreateInstance(type, true);
				this.m_membershipCondition.FromXml(securityElement3, level);
			}
			this.m_name = e.Attribute("Name");
			this.m_description = e.Attribute("Description");
			this.ParseXml(e, level);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
		{
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00098FD0 File Offset: 0x000971D0
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x00098FDC File Offset: 0x000971DC
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.Name != null)
			{
				securityElement.AddAttribute("Name", this.Name);
			}
			if (this.Description != null)
			{
				securityElement.AddAttribute("Description", this.Description);
			}
			if (this.MembershipCondition != null)
			{
				securityElement.AddChild(this.MembershipCondition.ToXml());
			}
			if (this.PolicyStatement != null && this.PolicyStatement.PermissionSet != null)
			{
				securityElement.AddChild(this.PolicyStatement.PermissionSet.ToXml());
			}
			foreach (object obj in this.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				securityElement.AddChild(codeGroup.ToXml());
			}
			this.CreateXml(securityElement, level);
			return securityElement;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
		{
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000990EC File Offset: 0x000972EC
		internal static CodeGroup CreateFromXml(SecurityElement se, PolicyLevel level)
		{
			string text = se.Attribute("class");
			string text2 = text;
			int num = text2.IndexOf(",");
			if (num > 0)
			{
				text2 = text2.Substring(0, num);
			}
			num = text2.LastIndexOf(".");
			if (num > 0)
			{
				text2 = text2.Substring(num + 1);
			}
			if (text2 == "FileCodeGroup")
			{
				return new FileCodeGroup(se, level);
			}
			if (text2 == "FirstMatchCodeGroup")
			{
				return new FirstMatchCodeGroup(se, level);
			}
			if (text2 == "NetCodeGroup")
			{
				return new NetCodeGroup(se, level);
			}
			if (!(text2 == "UnionCodeGroup"))
			{
				CodeGroup codeGroup = (CodeGroup)Activator.CreateInstance(Type.GetType(text), true);
				codeGroup.FromXml(se, level);
				return codeGroup;
			}
			return new UnionCodeGroup(se, level);
		}

		// Token: 0x04001E35 RID: 7733
		private PolicyStatement m_policy;

		// Token: 0x04001E36 RID: 7734
		private IMembershipCondition m_membershipCondition;

		// Token: 0x04001E37 RID: 7735
		private string m_description;

		// Token: 0x04001E38 RID: 7736
		private string m_name;

		// Token: 0x04001E39 RID: 7737
		private ArrayList m_children = new ArrayList();
	}
}
