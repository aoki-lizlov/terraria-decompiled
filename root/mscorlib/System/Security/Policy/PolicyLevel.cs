using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Xml;

namespace System.Security.Policy
{
	// Token: 0x020003EC RID: 1004
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyLevel
	{
		// Token: 0x06002A8E RID: 10894 RVA: 0x0009B00E File Offset: 0x0009920E
		internal PolicyLevel(string label, PolicyLevelType type)
		{
			this.label = label;
			this._type = type;
			this.full_trust_assemblies = new ArrayList();
			this.named_permission_sets = new ArrayList();
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x0009B03C File Offset: 0x0009923C
		internal void LoadFromFile(string filename)
		{
			try
			{
				if (!File.Exists(filename))
				{
					string text = filename + ".default";
					if (File.Exists(text))
					{
						File.Copy(text, filename);
					}
				}
				if (File.Exists(filename))
				{
					using (StreamReader streamReader = File.OpenText(filename))
					{
						this.xml = this.FromString(streamReader.ReadToEnd());
					}
					try
					{
						SecurityManager.ResolvingPolicyLevel = this;
						this.FromXml(this.xml);
						goto IL_008A;
					}
					finally
					{
						SecurityManager.ResolvingPolicyLevel = this;
					}
				}
				this.CreateDefaultFullTrustAssemblies();
				this.CreateDefaultNamedPermissionSets();
				this.CreateDefaultLevel(this._type);
				this.Save();
				IL_008A:;
			}
			catch
			{
			}
			finally
			{
				this._location = filename;
			}
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x0009B114 File Offset: 0x00099314
		internal void LoadFromString(string xml)
		{
			this.FromXml(this.FromString(xml));
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x0009B124 File Offset: 0x00099324
		private SecurityElement FromString(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			securityParser.LoadXml(xml);
			SecurityElement securityElement = securityParser.ToXml();
			if (securityElement.Tag != "configuration")
			{
				throw new ArgumentException(Locale.GetText("missing <configuration> root element"));
			}
			SecurityElement securityElement2 = (SecurityElement)securityElement.Children[0];
			if (securityElement2.Tag != "mscorlib")
			{
				throw new ArgumentException(Locale.GetText("missing <mscorlib> tag"));
			}
			SecurityElement securityElement3 = (SecurityElement)securityElement2.Children[0];
			if (securityElement3.Tag != "security")
			{
				throw new ArgumentException(Locale.GetText("missing <security> tag"));
			}
			SecurityElement securityElement4 = (SecurityElement)securityElement3.Children[0];
			if (securityElement4.Tag != "policy")
			{
				throw new ArgumentException(Locale.GetText("missing <policy> tag"));
			}
			return (SecurityElement)securityElement4.Children[0];
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x0009B20A File Offset: 0x0009940A
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public IList FullTrustAssemblies
		{
			get
			{
				return this.full_trust_assemblies;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x0009B212 File Offset: 0x00099412
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x0009B21A File Offset: 0x0009941A
		public IList NamedPermissionSets
		{
			get
			{
				return this.named_permission_sets;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x0009B222 File Offset: 0x00099422
		// (set) Token: 0x06002A96 RID: 10902 RVA: 0x0009B22A File Offset: 0x0009942A
		public CodeGroup RootCodeGroup
		{
			get
			{
				return this.root_code_group;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.root_code_group = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x0009B241 File Offset: 0x00099441
		public string StoreLocation
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x0009B249 File Offset: 0x00099449
		[ComVisible(false)]
		public PolicyLevelType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x0009B254 File Offset: 0x00099454
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void AddFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("sn");
			}
			StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version);
			this.AddFullTrustAssembly(strongNameMembershipCondition);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x0009B290 File Offset: 0x00099490
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void AddFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			using (IEnumerator enumerator = this.full_trust_assemblies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((StrongNameMembershipCondition)enumerator.Current).Equals(snMC))
					{
						throw new ArgumentException(Locale.GetText("sn already has full trust."));
					}
				}
			}
			this.full_trust_assemblies.Add(snMC);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x0009B314 File Offset: 0x00099514
		public void AddNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (permSet.Name == namedPermissionSet.Name)
				{
					throw new ArgumentException(Locale.GetText("This NamedPermissionSet is the same an existing NamedPermissionSet."));
				}
			}
			this.named_permission_sets.Add(permSet.Copy());
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x0009B3AC File Offset: 0x000995AC
		public NamedPermissionSet ChangeNamedPermissionSet(string name, PermissionSet pSet)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (pSet == null)
			{
				throw new ArgumentNullException("pSet");
			}
			if (DefaultPolicies.ReservedNames.IsReserved(name))
			{
				throw new ArgumentException(Locale.GetText("Reserved name"));
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (name == namedPermissionSet.Name)
				{
					this.named_permission_sets.Remove(namedPermissionSet);
					this.AddNamedPermissionSet(new NamedPermissionSet(name, pSet));
					return namedPermissionSet;
				}
			}
			throw new ArgumentException(Locale.GetText("PermissionSet not found"));
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x0009B470 File Offset: 0x00099670
		public static PolicyLevel CreateAppDomainLevel()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup(new AllMembershipCondition(), new PolicyStatement(DefaultPolicies.FullTrust));
			unionCodeGroup.Name = "All_Code";
			PolicyLevel policyLevel = new PolicyLevel("AppDomain", PolicyLevelType.AppDomain);
			policyLevel.RootCodeGroup = unionCodeGroup;
			policyLevel.Reset();
			return policyLevel;
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x0009B4B8 File Offset: 0x000996B8
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			SecurityElement securityElement = e.SearchForChildByTag("SecurityClasses");
			if (securityElement != null && securityElement.Children != null && securityElement.Children.Count > 0)
			{
				this.fullNames = new Hashtable(securityElement.Children.Count);
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					this.fullNames.Add(securityElement2.Attributes["Name"], securityElement2.Attributes["Description"]);
				}
			}
			SecurityElement securityElement3 = e.SearchForChildByTag("FullTrustAssemblies");
			if (securityElement3 != null && securityElement3.Children != null && securityElement3.Children.Count > 0)
			{
				this.full_trust_assemblies.Clear();
				foreach (object obj2 in securityElement3.Children)
				{
					SecurityElement securityElement4 = (SecurityElement)obj2;
					if (securityElement4.Tag != "IMembershipCondition")
					{
						throw new ArgumentException(Locale.GetText("Invalid XML"));
					}
					if (securityElement4.Attribute("class").IndexOf("StrongNameMembershipCondition") < 0)
					{
						throw new ArgumentException(Locale.GetText("Invalid XML - must be StrongNameMembershipCondition"));
					}
					this.full_trust_assemblies.Add(new StrongNameMembershipCondition(securityElement4));
				}
			}
			SecurityElement securityElement5 = e.SearchForChildByTag("CodeGroup");
			if (securityElement5 != null && securityElement5.Children != null && securityElement5.Children.Count > 0)
			{
				this.root_code_group = CodeGroup.CreateFromXml(securityElement5, this);
				SecurityElement securityElement6 = e.SearchForChildByTag("NamedPermissionSets");
				if (securityElement6 != null && securityElement6.Children != null && securityElement6.Children.Count > 0)
				{
					this.named_permission_sets.Clear();
					foreach (object obj3 in securityElement6.Children)
					{
						SecurityElement securityElement7 = (SecurityElement)obj3;
						NamedPermissionSet namedPermissionSet = new NamedPermissionSet();
						namedPermissionSet.Resolver = this;
						namedPermissionSet.FromXml(securityElement7);
						this.named_permission_sets.Add(namedPermissionSet);
					}
				}
				return;
			}
			throw new ArgumentException(Locale.GetText("Missing Root CodeGroup"));
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0009B758 File Offset: 0x00099958
		public NamedPermissionSet GetNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (namedPermissionSet.Name == name)
				{
					return (NamedPermissionSet)namedPermissionSet.Copy();
				}
			}
			return null;
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0009B7D8 File Offset: 0x000999D8
		public void Recover()
		{
			if (this._location == null)
			{
				throw new PolicyException(Locale.GetText("Only file based policies may be recovered."));
			}
			string text = this._location + ".backup";
			if (!File.Exists(text))
			{
				throw new PolicyException(Locale.GetText("No policy backup exists."));
			}
			try
			{
				File.Copy(text, this._location, true);
			}
			catch (Exception ex)
			{
				throw new PolicyException(Locale.GetText("Couldn't replace the policy file with it's backup."), ex);
			}
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x0009B858 File Offset: 0x00099A58
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void RemoveFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("sn");
			}
			StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version);
			this.RemoveFullTrustAssembly(strongNameMembershipCondition);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0009B892 File Offset: 0x00099A92
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void RemoveFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			if (((IList)this.full_trust_assemblies).Contains(snMC))
			{
				((IList)this.full_trust_assemblies).Remove(snMC);
				return;
			}
			throw new ArgumentException(Locale.GetText("sn does not have full trust."));
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x0009B8CC File Offset: 0x00099ACC
		public NamedPermissionSet RemoveNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			return this.RemoveNamedPermissionSet(permSet.Name);
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x0009B8E8 File Offset: 0x00099AE8
		public NamedPermissionSet RemoveNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (DefaultPolicies.ReservedNames.IsReserved(name))
			{
				throw new ArgumentException(Locale.GetText("Reserved name"));
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (name == namedPermissionSet.Name)
				{
					this.named_permission_sets.Remove(namedPermissionSet);
					return namedPermissionSet;
				}
			}
			throw new ArgumentException(string.Format(Locale.GetText("Name '{0}' cannot be found."), name), "name");
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0009B99C File Offset: 0x00099B9C
		public void Reset()
		{
			if (this.fullNames != null)
			{
				this.fullNames.Clear();
			}
			if (this._type != PolicyLevelType.AppDomain)
			{
				this.full_trust_assemblies.Clear();
				this.named_permission_sets.Clear();
				if (this._location != null && File.Exists(this._location))
				{
					try
					{
						File.Delete(this._location);
					}
					catch
					{
					}
				}
				this.LoadFromFile(this._location);
				return;
			}
			this.CreateDefaultFullTrustAssemblies();
			this.CreateDefaultNamedPermissionSets();
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0009BA2C File Offset: 0x00099C2C
		public PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			PolicyStatement policyStatement = this.root_code_group.Resolve(evidence);
			if (policyStatement == null)
			{
				return PolicyStatement.Empty();
			}
			return policyStatement;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x0009BA60 File Offset: 0x00099C60
		public CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			CodeGroup codeGroup = this.root_code_group.ResolveMatchingCodeGroups(evidence);
			if (codeGroup == null)
			{
				return null;
			}
			return codeGroup;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x0009BA90 File Offset: 0x00099C90
		public SecurityElement ToXml()
		{
			Hashtable hashtable = new Hashtable();
			if (this.full_trust_assemblies.Count > 0 && !hashtable.Contains("StrongNameMembershipCondition"))
			{
				hashtable.Add("StrongNameMembershipCondition", typeof(StrongNameMembershipCondition).FullName);
			}
			SecurityElement securityElement = new SecurityElement("NamedPermissionSets");
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				SecurityElement securityElement2 = namedPermissionSet.ToXml();
				object obj2 = securityElement2.Attributes["class"];
				if (!hashtable.Contains(obj2))
				{
					hashtable.Add(obj2, namedPermissionSet.GetType().FullName);
				}
				securityElement.AddChild(securityElement2);
			}
			SecurityElement securityElement3 = new SecurityElement("FullTrustAssemblies");
			foreach (object obj3 in this.full_trust_assemblies)
			{
				StrongNameMembershipCondition strongNameMembershipCondition = (StrongNameMembershipCondition)obj3;
				securityElement3.AddChild(strongNameMembershipCondition.ToXml(this));
			}
			SecurityElement securityElement4 = new SecurityElement("SecurityClasses");
			if (hashtable.Count > 0)
			{
				foreach (object obj4 in hashtable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj4;
					SecurityElement securityElement5 = new SecurityElement("SecurityClass");
					securityElement5.AddAttribute("Name", (string)dictionaryEntry.Key);
					securityElement5.AddAttribute("Description", (string)dictionaryEntry.Value);
					securityElement4.AddChild(securityElement5);
				}
			}
			SecurityElement securityElement6 = new SecurityElement(typeof(PolicyLevel).Name);
			securityElement6.AddAttribute("version", "1");
			securityElement6.AddChild(securityElement4);
			securityElement6.AddChild(securityElement);
			if (this.root_code_group != null)
			{
				securityElement6.AddChild(this.root_code_group.ToXml(this));
			}
			securityElement6.AddChild(securityElement3);
			return securityElement6;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x0009BCCC File Offset: 0x00099ECC
		internal void Save()
		{
			if (this._type == PolicyLevelType.AppDomain)
			{
				throw new PolicyException(Locale.GetText("Can't save AppDomain PolicyLevel"));
			}
			if (this._location != null)
			{
				try
				{
					if (File.Exists(this._location))
					{
						File.Copy(this._location, this._location + ".backup", true);
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					using (StreamWriter streamWriter = new StreamWriter(this._location))
					{
						streamWriter.Write(this.ToXml().ToString());
						streamWriter.Close();
					}
				}
			}
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x0009BD80 File Offset: 0x00099F80
		internal void CreateDefaultLevel(PolicyLevelType type)
		{
			PolicyStatement policyStatement = new PolicyStatement(DefaultPolicies.FullTrust);
			switch (type)
			{
			case PolicyLevelType.User:
			case PolicyLevelType.Enterprise:
			case PolicyLevelType.AppDomain:
				this.root_code_group = new UnionCodeGroup(new AllMembershipCondition(), policyStatement);
				this.root_code_group.Name = "All_Code";
				return;
			case PolicyLevelType.Machine:
			{
				PolicyStatement policyStatement2 = new PolicyStatement(DefaultPolicies.Nothing);
				this.root_code_group = new UnionCodeGroup(new AllMembershipCondition(), policyStatement2);
				this.root_code_group.Name = "All_Code";
				UnionCodeGroup unionCodeGroup = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.MyComputer), policyStatement);
				unionCodeGroup.Name = "My_Computer_Zone";
				this.root_code_group.AddChild(unionCodeGroup);
				UnionCodeGroup unionCodeGroup2 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Intranet), new PolicyStatement(DefaultPolicies.LocalIntranet));
				unionCodeGroup2.Name = "LocalIntranet_Zone";
				this.root_code_group.AddChild(unionCodeGroup2);
				PolicyStatement policyStatement3 = new PolicyStatement(DefaultPolicies.Internet);
				UnionCodeGroup unionCodeGroup3 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Internet), policyStatement3);
				unionCodeGroup3.Name = "Internet_Zone";
				this.root_code_group.AddChild(unionCodeGroup3);
				UnionCodeGroup unionCodeGroup4 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Untrusted), policyStatement2);
				unionCodeGroup4.Name = "Restricted_Zone";
				this.root_code_group.AddChild(unionCodeGroup4);
				UnionCodeGroup unionCodeGroup5 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Trusted), policyStatement3);
				unionCodeGroup5.Name = "Trusted_Zone";
				this.root_code_group.AddChild(unionCodeGroup5);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0009BED4 File Offset: 0x0009A0D4
		internal void CreateDefaultFullTrustAssemblies()
		{
			this.full_trust_assemblies.Clear();
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("mscorlib", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Data", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.DirectoryServices", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Drawing", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Messaging", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.ServiceProcess", DefaultPolicies.Key.MsFinal));
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0009BF90 File Offset: 0x0009A190
		internal void CreateDefaultNamedPermissionSets()
		{
			this.named_permission_sets.Clear();
			try
			{
				SecurityManager.ResolvingPolicyLevel = this;
				this.named_permission_sets.Add(DefaultPolicies.LocalIntranet);
				this.named_permission_sets.Add(DefaultPolicies.Internet);
				this.named_permission_sets.Add(DefaultPolicies.SkipVerification);
				this.named_permission_sets.Add(DefaultPolicies.Execution);
				this.named_permission_sets.Add(DefaultPolicies.Nothing);
				this.named_permission_sets.Add(DefaultPolicies.Everything);
				this.named_permission_sets.Add(DefaultPolicies.FullTrust);
			}
			finally
			{
				SecurityManager.ResolvingPolicyLevel = null;
			}
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x0009C040 File Offset: 0x0009A240
		internal string ResolveClassName(string className)
		{
			if (this.fullNames != null)
			{
				object obj = this.fullNames[className];
				if (obj != null)
				{
					return (string)obj;
				}
			}
			return className;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x0009C070 File Offset: 0x0009A270
		internal bool IsFullTrustAssembly(Assembly a)
		{
			AssemblyName name = a.GetName();
			StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition(new StrongNamePublicKeyBlob(name.GetPublicKey()), name.Name, name.Version);
			using (IEnumerator enumerator = this.full_trust_assemblies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((StrongNameMembershipCondition)enumerator.Current).Equals(strongNameMembershipCondition))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04001E73 RID: 7795
		private string label;

		// Token: 0x04001E74 RID: 7796
		private CodeGroup root_code_group;

		// Token: 0x04001E75 RID: 7797
		private ArrayList full_trust_assemblies;

		// Token: 0x04001E76 RID: 7798
		private ArrayList named_permission_sets;

		// Token: 0x04001E77 RID: 7799
		private string _location;

		// Token: 0x04001E78 RID: 7800
		private PolicyLevelType _type;

		// Token: 0x04001E79 RID: 7801
		private Hashtable fullNames;

		// Token: 0x04001E7A RID: 7802
		private SecurityElement xml;
	}
}
