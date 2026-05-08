using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	// Token: 0x020003EE RID: 1006
	[ComVisible(true)]
	[Serializable]
	public sealed class Site : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		// Token: 0x06002ABE RID: 10942 RVA: 0x0009C324 File Offset: 0x0009A524
		public Site(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("url");
			}
			if (!Site.IsValid(name))
			{
				throw new ArgumentException(Locale.GetText("name is not valid"));
			}
			this.origin_site = name;
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x0009C35C File Offset: 0x0009A55C
		public static Site CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new FormatException(Locale.GetText("Empty URL."));
			}
			string text = Site.UrlToSite(url);
			if (text == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid URL '{0}'."), url), "url");
			}
			return new Site(text);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x0009C3B8 File Offset: 0x0009A5B8
		public object Copy()
		{
			return new Site(this.origin_site);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0009C3C5 File Offset: 0x0009A5C5
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new SiteIdentityPermission(this.origin_site);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0009C3D4 File Offset: 0x0009A5D4
		public override bool Equals(object o)
		{
			Site site = o as Site;
			return site != null && string.Compare(site.Name, this.origin_site, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x0009C407 File Offset: 0x0009A607
		public override int GetHashCode()
		{
			return this.origin_site.GetHashCode();
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x0009C414 File Offset: 0x0009A614
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Site");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Name", this.origin_site));
			return securityElement.ToString();
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x0009C44B File Offset: 0x0009A64B
		public string Name
		{
			get
			{
				return this.origin_site;
			}
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0009C453 File Offset: 0x0009A653
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.origin_site.Length;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x0009C468 File Offset: 0x0009A668
		internal static bool IsValid(string name)
		{
			if (name == string.Empty)
			{
				return false;
			}
			if (name.Length == 1 && name == ".")
			{
				return false;
			}
			string[] array = name.Split('.', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (i != 0 || !(text == "*"))
				{
					string text2 = text;
					for (int j = 0; j < text2.Length; j++)
					{
						int num = Convert.ToInt32(text2[j]);
						if (num != 33 && num != 45 && (num < 35 || num > 41) && (num < 48 || num > 57) && (num < 64 || num > 90) && (num < 94 || num > 95) && (num < 97 || num > 123) && (num < 125 || num > 126))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x0009C55C File Offset: 0x0009A75C
		internal static string UrlToSite(string url)
		{
			if (url == null)
			{
				return null;
			}
			Uri uri = new Uri(url);
			if (uri.Scheme == Uri.UriSchemeFile)
			{
				return null;
			}
			string host = uri.Host;
			if (!Site.IsValid(host))
			{
				return null;
			}
			return host;
		}

		// Token: 0x04001E7D RID: 7805
		internal string origin_site;
	}
}
