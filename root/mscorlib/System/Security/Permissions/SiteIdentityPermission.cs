using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200042A RID: 1066
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002CE5 RID: 11493 RVA: 0x0009ED64 File Offset: 0x0009CF64
		public SiteIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000A2202 File Offset: 0x000A0402
		public SiteIdentityPermission(string site)
		{
			this.Site = site;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000A2211 File Offset: 0x000A0411
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x000A222C File Offset: 0x000A042C
		public string Site
		{
			get
			{
				if (this.IsEmpty())
				{
					throw new NullReferenceException("No site.");
				}
				return this._site;
			}
			set
			{
				if (!this.IsValid(value))
				{
					throw new ArgumentException("Invalid site.");
				}
				this._site = value;
			}
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000A2249 File Offset: 0x000A0449
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return new SiteIdentityPermission(PermissionState.None);
			}
			return new SiteIdentityPermission(this._site);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000A2268 File Offset: 0x000A0468
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Site");
			if (text != null)
			{
				this.Site = text;
			}
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000A229C File Offset: 0x000A049C
		public override IPermission Intersect(IPermission target)
		{
			SiteIdentityPermission siteIdentityPermission = this.Cast(target);
			if (siteIdentityPermission == null || this.IsEmpty())
			{
				return null;
			}
			if (this.Match(siteIdentityPermission._site))
			{
				return new SiteIdentityPermission((this._site.Length > siteIdentityPermission._site.Length) ? this._site : siteIdentityPermission._site);
			}
			return null;
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000A22FC File Offset: 0x000A04FC
		public override bool IsSubsetOf(IPermission target)
		{
			SiteIdentityPermission siteIdentityPermission = this.Cast(target);
			if (siteIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this._site == null && siteIdentityPermission._site == null)
			{
				return true;
			}
			if (this._site == null || siteIdentityPermission._site == null)
			{
				return false;
			}
			int num = siteIdentityPermission._site.IndexOf('*');
			if (num == -1)
			{
				return this._site == siteIdentityPermission._site;
			}
			return this._site.EndsWith(siteIdentityPermission._site.Substring(num + 1));
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000A237C File Offset: 0x000A057C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._site != null)
			{
				securityElement.AddAttribute("Site", this._site);
			}
			return securityElement;
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000A23AC File Offset: 0x000A05AC
		public override IPermission Union(IPermission target)
		{
			SiteIdentityPermission siteIdentityPermission = this.Cast(target);
			if (siteIdentityPermission == null || siteIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return siteIdentityPermission.Copy();
			}
			if (this.Match(siteIdentityPermission._site))
			{
				return new SiteIdentityPermission((this._site.Length < siteIdentityPermission._site.Length) ? this._site : siteIdentityPermission._site);
			}
			throw new ArgumentException(Locale.GetText("Cannot union two different sites."), "target");
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x0002A0C4 File Offset: 0x000282C4
		int IBuiltInPermission.GetTokenIndex()
		{
			return 11;
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000A2430 File Offset: 0x000A0630
		private bool IsEmpty()
		{
			return this._site == null;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000A243B File Offset: 0x000A063B
		private SiteIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(SiteIdentityPermission));
			}
			return siteIdentityPermission;
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000A245C File Offset: 0x000A065C
		private bool IsValid(string s)
		{
			if (s == null || s.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				ushort num = (ushort)s[i];
				if (num < 33 || num > 126)
				{
					return false;
				}
				if (num == 42 && s.Length > 1 && (i > 0 || s[i + 1] != '.'))
				{
					return false;
				}
				if (!SiteIdentityPermission.valid[(int)(num - 33)])
				{
					return false;
				}
			}
			return s.Length != 1 || s[0] != '.';
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000A24E4 File Offset: 0x000A06E4
		private bool Match(string target)
		{
			if (this._site == null || target == null)
			{
				return false;
			}
			int num = this._site.IndexOf('*');
			int num2 = target.IndexOf('*');
			if (num == -1 && num2 == -1)
			{
				return this._site == target;
			}
			if (num == -1)
			{
				return this._site.EndsWith(target.Substring(num2 + 1));
			}
			if (num2 == -1)
			{
				return target.EndsWith(this._site.Substring(num + 1));
			}
			string text = this._site.Substring(num + 1);
			target = target.Substring(num2 + 1);
			if (text.Length > target.Length)
			{
				return text.EndsWith(target);
			}
			return target.EndsWith(text);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000A2593 File Offset: 0x000A0793
		// Note: this type is marked as 'beforefieldinit'.
		static SiteIdentityPermission()
		{
		}

		// Token: 0x04001F74 RID: 8052
		private const int version = 1;

		// Token: 0x04001F75 RID: 8053
		private string _site;

		// Token: 0x04001F76 RID: 8054
		private static bool[] valid = new bool[]
		{
			true, false, true, true, true, true, true, true, true, true,
			false, false, true, true, false, true, true, true, true, true,
			true, true, true, true, false, false, false, false, false, false,
			false, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, false, false,
			false, true, true, false, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, false, true, true
		};
	}
}
