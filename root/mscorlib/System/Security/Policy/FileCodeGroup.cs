using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003DE RID: 990
	[ComVisible(true)]
	[Serializable]
	public sealed class FileCodeGroup : CodeGroup
	{
		// Token: 0x06002A1E RID: 10782 RVA: 0x00099CF6 File Offset: 0x00097EF6
		public FileCodeGroup(IMembershipCondition membershipCondition, FileIOPermissionAccess access)
			: base(membershipCondition, null)
		{
			this.m_access = access;
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x00099D07 File Offset: 0x00097F07
		internal FileCodeGroup(SecurityElement e, PolicyLevel level)
			: base(e, level)
		{
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00099D14 File Offset: 0x00097F14
		public override CodeGroup Copy()
		{
			FileCodeGroup fileCodeGroup = new FileCodeGroup(base.MembershipCondition, this.m_access);
			fileCodeGroup.Name = base.Name;
			fileCodeGroup.Description = base.Description;
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				fileCodeGroup.AddChild(codeGroup.Copy());
			}
			return fileCodeGroup;
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x00099DA0 File Offset: 0x00097FA0
		public override string MergeLogic
		{
			get
			{
				return "Union";
			}
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00099DA8 File Offset: 0x00097FA8
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			PermissionSet permissionSet = null;
			if (base.PolicyStatement == null)
			{
				permissionSet = new PermissionSet(PermissionState.None);
			}
			else
			{
				permissionSet = base.PolicyStatement.PermissionSet.Copy();
			}
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					PolicyStatement policyStatement = ((CodeGroup)obj).Resolve(evidence);
					if (policyStatement != null)
					{
						permissionSet = permissionSet.Union(policyStatement.PermissionSet);
					}
				}
			}
			PolicyStatement policyStatement2;
			if (base.PolicyStatement != null)
			{
				policyStatement2 = base.PolicyStatement.Copy();
			}
			else
			{
				policyStatement2 = PolicyStatement.Empty();
			}
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x00099E8C File Offset: 0x0009808C
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			FileCodeGroup fileCodeGroup = new FileCodeGroup(base.MembershipCondition, this.m_access);
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
				if (codeGroup != null)
				{
					fileCodeGroup.AddChild(codeGroup);
				}
			}
			return fileCodeGroup;
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override string AttributeString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x00099F20 File Offset: 0x00098120
		public override string PermissionSetName
		{
			get
			{
				return "Same directory FileIO - " + this.m_access.ToString();
			}
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x00099F3D File Offset: 0x0009813D
		public override bool Equals(object o)
		{
			return o is FileCodeGroup && this.m_access == ((FileCodeGroup)o).m_access && base.Equals((CodeGroup)o, false);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x00099F6B File Offset: 0x0009816B
		public override int GetHashCode()
		{
			return this.m_access.GetHashCode();
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00099F80 File Offset: 0x00098180
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			string text = e.Attribute("Access");
			if (text != null)
			{
				this.m_access = (FileIOPermissionAccess)Enum.Parse(typeof(FileIOPermissionAccess), text, true);
				return;
			}
			this.m_access = FileIOPermissionAccess.NoAccess;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00099FC0 File Offset: 0x000981C0
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			element.AddAttribute("Access", this.m_access.ToString());
		}

		// Token: 0x04001E61 RID: 7777
		private FileIOPermissionAccess m_access;
	}
}
