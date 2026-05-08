using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000432 RID: 1074
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002D3D RID: 11581 RVA: 0x000A36FF File Offset: 0x000A18FF
		public UrlIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
			this.url = string.Empty;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000A371A File Offset: 0x000A191A
		public UrlIdentityPermission(string site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			this.url = site;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000A3737 File Offset: 0x000A1937
		// (set) Token: 0x06002D40 RID: 11584 RVA: 0x000A373F File Offset: 0x000A193F
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000A3752 File Offset: 0x000A1952
		public override IPermission Copy()
		{
			if (this.url == null)
			{
				return new UrlIdentityPermission(PermissionState.None);
			}
			return new UrlIdentityPermission(this.url);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000A3770 File Offset: 0x000A1970
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Url");
			if (text == null)
			{
				this.url = string.Empty;
				return;
			}
			this.Url = text;
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000A37B0 File Offset: 0x000A19B0
		public override IPermission Intersect(IPermission target)
		{
			UrlIdentityPermission urlIdentityPermission = this.Cast(target);
			if (urlIdentityPermission == null || this.IsEmpty())
			{
				return null;
			}
			if (!this.Match(urlIdentityPermission.url))
			{
				return null;
			}
			if (this.url.Length > urlIdentityPermission.url.Length)
			{
				return this.Copy();
			}
			return urlIdentityPermission.Copy();
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000A3808 File Offset: 0x000A1A08
		public override bool IsSubsetOf(IPermission target)
		{
			UrlIdentityPermission urlIdentityPermission = this.Cast(target);
			if (urlIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsEmpty())
			{
				return true;
			}
			if (urlIdentityPermission.url == null)
			{
				return false;
			}
			int num = urlIdentityPermission.url.LastIndexOf('*');
			if (num == -1)
			{
				num = urlIdentityPermission.url.Length;
			}
			return string.Compare(this.url, 0, urlIdentityPermission.url, 0, num, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000A3878 File Offset: 0x000A1A78
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (!this.IsEmpty())
			{
				securityElement.AddAttribute("Url", this.url);
			}
			return securityElement;
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000A38A8 File Offset: 0x000A1AA8
		public override IPermission Union(IPermission target)
		{
			UrlIdentityPermission urlIdentityPermission = this.Cast(target);
			if (urlIdentityPermission == null)
			{
				return this.Copy();
			}
			if (this.IsEmpty() && urlIdentityPermission.IsEmpty())
			{
				return null;
			}
			if (urlIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return urlIdentityPermission.Copy();
			}
			if (!this.Match(urlIdentityPermission.url))
			{
				throw new ArgumentException(Locale.GetText("Cannot union two different urls."), "target");
			}
			if (this.url.Length < urlIdentityPermission.url.Length)
			{
				return this.Copy();
			}
			return urlIdentityPermission.Copy();
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x00034CEE File Offset: 0x00032EEE
		int IBuiltInPermission.GetTokenIndex()
		{
			return 13;
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000A393F File Offset: 0x000A1B3F
		private bool IsEmpty()
		{
			return this.url == null || this.url.Length == 0;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000A3959 File Offset: 0x000A1B59
		private UrlIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
			if (urlIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(UrlIdentityPermission));
			}
			return urlIdentityPermission;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000A397C File Offset: 0x000A1B7C
		private bool Match(string target)
		{
			if (this.url == null || target == null)
			{
				return false;
			}
			int num = this.url.LastIndexOf('*');
			int num2 = target.LastIndexOf('*');
			int num3;
			if (num == -1 && num2 == -1)
			{
				num3 = Math.Max(this.url.Length, target.Length);
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
			return string.Compare(this.url, 0, target, 0, num3, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x04001F88 RID: 8072
		private const int version = 1;

		// Token: 0x04001F89 RID: 8073
		private string url;
	}
}
