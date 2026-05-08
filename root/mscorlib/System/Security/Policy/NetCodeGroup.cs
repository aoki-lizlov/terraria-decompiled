using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003E9 RID: 1001
	[ComVisible(true)]
	[Serializable]
	public sealed class NetCodeGroup : CodeGroup
	{
		// Token: 0x06002A70 RID: 10864 RVA: 0x0009A96E File Offset: 0x00098B6E
		public NetCodeGroup(IMembershipCondition membershipCondition)
			: base(membershipCondition, null)
		{
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0009A983 File Offset: 0x00098B83
		internal NetCodeGroup(SecurityElement e, PolicyLevel level)
			: base(e, level)
		{
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override string AttributeString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x00099DA0 File Offset: 0x00097FA0
		public override string MergeLogic
		{
			get
			{
				return "Union";
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x0009A998 File Offset: 0x00098B98
		public override string PermissionSetName
		{
			get
			{
				return "Same site Web";
			}
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0009A9A0 File Offset: 0x00098BA0
		[MonoTODO("(2.0) missing validations")]
		public void AddConnectAccess(string originScheme, CodeConnectAccess connectAccess)
		{
			if (originScheme == null)
			{
				throw new ArgumentException("originScheme");
			}
			if (originScheme == NetCodeGroup.AbsentOriginScheme && connectAccess.Scheme == CodeConnectAccess.OriginScheme)
			{
				throw new ArgumentOutOfRangeException("connectAccess", Locale.GetText("Schema == CodeConnectAccess.OriginScheme"));
			}
			if (this._rules.ContainsKey(originScheme))
			{
				if (connectAccess != null)
				{
					CodeConnectAccess[] array = (CodeConnectAccess[])this._rules[originScheme];
					CodeConnectAccess[] array2 = new CodeConnectAccess[array.Length + 1];
					Array.Copy(array, 0, array2, 0, array.Length);
					array2[array.Length] = connectAccess;
					this._rules[originScheme] = array2;
					return;
				}
			}
			else
			{
				CodeConnectAccess[] array3 = new CodeConnectAccess[] { connectAccess };
				this._rules.Add(originScheme, array3);
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0009AA58 File Offset: 0x00098C58
		public override CodeGroup Copy()
		{
			NetCodeGroup netCodeGroup = new NetCodeGroup(base.MembershipCondition);
			netCodeGroup.Name = base.Name;
			netCodeGroup.Description = base.Description;
			netCodeGroup.PolicyStatement = base.PolicyStatement;
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				netCodeGroup.AddChild(codeGroup.Copy());
			}
			return netCodeGroup;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x0009AAE8 File Offset: 0x00098CE8
		private bool Equals(CodeConnectAccess[] rules1, CodeConnectAccess[] rules2)
		{
			for (int i = 0; i < rules1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < rules2.Length; j++)
				{
					if (rules1[i].Equals(rules2[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x0009AB2C File Offset: 0x00098D2C
		public override bool Equals(object o)
		{
			if (!base.Equals(o))
			{
				return false;
			}
			NetCodeGroup netCodeGroup = o as NetCodeGroup;
			if (netCodeGroup == null)
			{
				return false;
			}
			foreach (object obj in this._rules)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				CodeConnectAccess[] array = (CodeConnectAccess[])netCodeGroup._rules[dictionaryEntry.Key];
				bool flag;
				if (array != null)
				{
					flag = this.Equals((CodeConnectAccess[])dictionaryEntry.Value, array);
				}
				else
				{
					flag = dictionaryEntry.Value == null;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0009ABE8 File Offset: 0x00098DE8
		public DictionaryEntry[] GetConnectAccessRules()
		{
			DictionaryEntry[] array = new DictionaryEntry[this._rules.Count];
			this._rules.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x0009AC14 File Offset: 0x00098E14
		public override int GetHashCode()
		{
			if (this._hashcode == 0)
			{
				this._hashcode = base.GetHashCode();
				foreach (object obj in this._rules)
				{
					CodeConnectAccess[] array = (CodeConnectAccess[])((DictionaryEntry)obj).Value;
					if (array != null)
					{
						foreach (CodeConnectAccess codeConnectAccess in array)
						{
							this._hashcode ^= codeConnectAccess.GetHashCode();
						}
					}
				}
			}
			return this._hashcode;
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x0009ACC4 File Offset: 0x00098EC4
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
			PolicyStatement policyStatement2 = base.PolicyStatement.Copy();
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x0009AD90 File Offset: 0x00098F90
		public void ResetConnectAccess()
		{
			this._rules.Clear();
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x0009ADA0 File Offset: 0x00098FA0
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			CodeGroup codeGroup = null;
			if (base.MembershipCondition.Check(evidence))
			{
				codeGroup = this.Copy();
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup2 = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
					if (codeGroup2 != null)
					{
						codeGroup.AddChild(codeGroup2);
					}
				}
			}
			return codeGroup;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x0009AE28 File Offset: 0x00099028
		[MonoTODO("(2.0) Add new stuff (CodeConnectAccess) into XML")]
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			base.CreateXml(element, level);
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x0009AE32 File Offset: 0x00099032
		[MonoTODO("(2.0) Parse new stuff (CodeConnectAccess) from XML")]
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			base.ParseXml(e, level);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x0009AE3C File Offset: 0x0009903C
		// Note: this type is marked as 'beforefieldinit'.
		static NetCodeGroup()
		{
		}

		// Token: 0x04001E6C RID: 7788
		public static readonly string AbsentOriginScheme = string.Empty;

		// Token: 0x04001E6D RID: 7789
		public static readonly string AnyOtherOriginScheme = "*";

		// Token: 0x04001E6E RID: 7790
		private Hashtable _rules = new Hashtable();

		// Token: 0x04001E6F RID: 7791
		private int _hashcode;
	}
}
