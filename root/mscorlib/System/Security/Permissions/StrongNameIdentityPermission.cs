using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200042C RID: 1068
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002CF9 RID: 11513 RVA: 0x000A25FE File Offset: 0x000A07FE
		public StrongNameIdentityPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this._list = new ArrayList();
			this._list.Add(StrongNameIdentityPermission.SNIP.CreateDefault());
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000A2634 File Offset: 0x000A0834
		public StrongNameIdentityPermission(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name != null && name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this._state = PermissionState.None;
			this._list = new ArrayList();
			this._list.Add(new StrongNameIdentityPermission.SNIP(blob, name, version));
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000A2698 File Offset: 0x000A0898
		internal StrongNameIdentityPermission(StrongNameIdentityPermission snip)
		{
			this._state = snip._state;
			this._list = new ArrayList(snip._list.Count);
			foreach (object obj in snip._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				this._list.Add(new StrongNameIdentityPermission.SNIP(snip2.PublicKey, snip2.Name, snip2.AssemblyVersion));
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x000A273C File Offset: 0x000A093C
		// (set) Token: 0x06002CFD RID: 11517 RVA: 0x000A2768 File Offset: 0x000A0968
		public string Name
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).Name;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					throw new ArgumentException("name");
				}
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.Name = value;
				this._list[0] = snip;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x000A27CB File Offset: 0x000A09CB
		// (set) Token: 0x06002CFF RID: 11519 RVA: 0x000A27F8 File Offset: 0x000A09F8
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).PublicKey;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.PublicKey = value;
				this._list[0] = snip;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x000A2853 File Offset: 0x000A0A53
		// (set) Token: 0x06002D01 RID: 11521 RVA: 0x000A2880 File Offset: 0x000A0A80
		public Version Version
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).AssemblyVersion;
			}
			set
			{
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.AssemblyVersion = value;
				this._list[0] = snip;
			}
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000A28CD File Offset: 0x000A0ACD
		internal void ResetToDefault()
		{
			this._list.Clear();
			this._list.Add(StrongNameIdentityPermission.SNIP.CreateDefault());
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000A28F0 File Offset: 0x000A0AF0
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			return new StrongNameIdentityPermission(this);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000A2908 File Offset: 0x000A0B08
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._list.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				using (IEnumerator enumerator = e.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SecurityElement securityElement = (SecurityElement)obj;
						this._list.Add(this.FromSecurityElement(securityElement));
					}
					return;
				}
			}
			this._list.Add(this.FromSecurityElement(e));
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000A29BC File Offset: 0x000A0BBC
		private StrongNameIdentityPermission.SNIP FromSecurityElement(SecurityElement se)
		{
			string text = se.Attribute("Name");
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = StrongNamePublicKeyBlob.FromString(se.Attribute("PublicKeyBlob"));
			string text2 = se.Attribute("AssemblyVersion");
			Version version = ((text2 == null) ? null : new Version(text2));
			return new StrongNameIdentityPermission.SNIP(strongNamePublicKeyBlob, text, version);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000A2A08 File Offset: 0x000A0C08
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				throw new ArgumentException(Locale.GetText("Wrong permission type."));
			}
			if (this.IsEmpty() || strongNameIdentityPermission.IsEmpty())
			{
				return null;
			}
			if (!this.Match(strongNameIdentityPermission.Name))
			{
				return null;
			}
			string text = ((this.Name.Length < strongNameIdentityPermission.Name.Length) ? this.Name : strongNameIdentityPermission.Name);
			if (!this.Version.Equals(strongNameIdentityPermission.Version))
			{
				return null;
			}
			if (!this.PublicKey.Equals(strongNameIdentityPermission.PublicKey))
			{
				return null;
			}
			return new StrongNameIdentityPermission(this.PublicKey, text, this.Version);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000A2ABC File Offset: 0x000A0CBC
		public override bool IsSubsetOf(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsEmpty())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return strongNameIdentityPermission.IsUnrestricted();
			}
			if (strongNameIdentityPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				foreach (object obj2 in strongNameIdentityPermission._list)
				{
					StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj2;
					if (!snip.IsSubsetOf(snip2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000A2BA0 File Offset: 0x000A0DA0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._list.Count > 1)
			{
				using (IEnumerator enumerator = this._list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
						SecurityElement securityElement2 = new SecurityElement("StrongName");
						this.ToSecurityElement(securityElement2, snip);
						securityElement.AddChild(securityElement2);
					}
					return securityElement;
				}
			}
			if (this._list.Count == 1)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)this._list[0];
				if (!this.IsEmpty(snip2))
				{
					this.ToSecurityElement(securityElement, snip2);
				}
			}
			return securityElement;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000A2C5C File Offset: 0x000A0E5C
		private void ToSecurityElement(SecurityElement se, StrongNameIdentityPermission.SNIP snip)
		{
			if (snip.PublicKey != null)
			{
				se.AddAttribute("PublicKeyBlob", snip.PublicKey.ToString());
			}
			if (snip.Name != null)
			{
				se.AddAttribute("Name", snip.Name);
			}
			if (snip.AssemblyVersion != null)
			{
				se.AddAttribute("AssemblyVersion", snip.AssemblyVersion.ToString());
			}
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000A2CC4 File Offset: 0x000A0EC4
		public override IPermission Union(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null || strongNameIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return strongNameIdentityPermission.Copy();
			}
			StrongNameIdentityPermission strongNameIdentityPermission2 = (StrongNameIdentityPermission)this.Copy();
			foreach (object obj in strongNameIdentityPermission._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				if (!this.IsEmpty(snip) && !this.Contains(snip))
				{
					strongNameIdentityPermission2._list.Add(snip);
				}
			}
			return strongNameIdentityPermission2;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x00048F49 File Offset: 0x00047149
		int IBuiltInPermission.GetTokenIndex()
		{
			return 12;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000A2D74 File Offset: 0x000A0F74
		private bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000A2D80 File Offset: 0x000A0F80
		private bool Contains(StrongNameIdentityPermission.SNIP snip)
		{
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				bool flag = (snip2.PublicKey == null && snip.PublicKey == null) || (snip2.PublicKey != null && snip2.PublicKey.Equals(snip.PublicKey));
				bool flag2 = snip2.IsNameSubsetOf(snip.Name);
				bool flag3 = (snip2.AssemblyVersion == null && snip.AssemblyVersion == null) || (snip2.AssemblyVersion != null && snip2.AssemblyVersion.Equals(snip.AssemblyVersion));
				if (flag && flag2 && flag3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000A2E6C File Offset: 0x000A106C
		private bool IsEmpty(StrongNameIdentityPermission.SNIP snip)
		{
			return this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000A2EBC File Offset: 0x000A10BC
		private bool IsEmpty()
		{
			return !this.IsUnrestricted() && this._list.Count <= 1 && this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000A2F23 File Offset: 0x000A1123
		private StrongNameIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(StrongNameIdentityPermission));
			}
			return strongNameIdentityPermission;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000A2F44 File Offset: 0x000A1144
		private bool Match(string target)
		{
			if (this.Name == null || target == null)
			{
				return false;
			}
			int num = this.Name.LastIndexOf('*');
			int num2 = target.LastIndexOf('*');
			int num3;
			if (num == -1 && num2 == -1)
			{
				num3 = Math.Max(this.Name.Length, target.Length);
			}
			else if (num == -1)
			{
				num3 = num2;
			}
			else if (num2 == -1)
			{
				num3 = num;
			}
			else
			{
				num3 = Math.Min(num, num2);
			}
			return string.Compare(this.Name, 0, target, 0, num3, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000A2FCC File Offset: 0x000A11CC
		// Note: this type is marked as 'beforefieldinit'.
		static StrongNameIdentityPermission()
		{
		}

		// Token: 0x04001F78 RID: 8056
		private const int version = 1;

		// Token: 0x04001F79 RID: 8057
		private static Version defaultVersion = new Version(0, 0);

		// Token: 0x04001F7A RID: 8058
		private PermissionState _state;

		// Token: 0x04001F7B RID: 8059
		private ArrayList _list;

		// Token: 0x0200042D RID: 1069
		private struct SNIP
		{
			// Token: 0x06002D13 RID: 11539 RVA: 0x000A2FDA File Offset: 0x000A11DA
			internal SNIP(StrongNamePublicKeyBlob pk, string name, Version version)
			{
				this.PublicKey = pk;
				this.Name = name;
				this.AssemblyVersion = version;
			}

			// Token: 0x06002D14 RID: 11540 RVA: 0x000A2FF1 File Offset: 0x000A11F1
			internal static StrongNameIdentityPermission.SNIP CreateDefault()
			{
				return new StrongNameIdentityPermission.SNIP(null, string.Empty, (Version)StrongNameIdentityPermission.defaultVersion.Clone());
			}

			// Token: 0x06002D15 RID: 11541 RVA: 0x000A3010 File Offset: 0x000A1210
			internal bool IsNameSubsetOf(string target)
			{
				if (this.Name == null)
				{
					return target == null;
				}
				if (target == null)
				{
					return true;
				}
				int num = this.Name.LastIndexOf('*');
				if (num == 0)
				{
					return true;
				}
				if (num == -1)
				{
					num = this.Name.Length;
				}
				return string.Compare(this.Name, 0, target, 0, num, true, CultureInfo.InvariantCulture) == 0;
			}

			// Token: 0x06002D16 RID: 11542 RVA: 0x000A306C File Offset: 0x000A126C
			internal bool IsSubsetOf(StrongNameIdentityPermission.SNIP target)
			{
				return (this.PublicKey != null && this.PublicKey.Equals(target.PublicKey)) || (this.IsNameSubsetOf(target.Name) && (!(this.AssemblyVersion != null) || this.AssemblyVersion.Equals(target.AssemblyVersion)) && this.PublicKey == null && target.PublicKey == null);
			}

			// Token: 0x04001F7C RID: 8060
			public StrongNamePublicKeyBlob PublicKey;

			// Token: 0x04001F7D RID: 8061
			public string Name;

			// Token: 0x04001F7E RID: 8062
			public Version AssemblyVersion;
		}
	}
}
