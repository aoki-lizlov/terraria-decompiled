using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using Mono.Security;

namespace System.Security.Policy
{
	// Token: 0x020003F6 RID: 1014
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		// Token: 0x06002B17 RID: 11031 RVA: 0x0009D0B0 File Offset: 0x0009B2B0
		public UrlMembershipCondition(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			this.CheckUrl(url);
			this.userUrl = url;
			this.url = new Url(url);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x0009D0E7 File Offset: 0x0009B2E7
		internal UrlMembershipCondition(Url url, string userUrl)
		{
			this.url = (Url)url.Copy();
			this.userUrl = userUrl;
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0009D10E File Offset: 0x0009B30E
		// (set) Token: 0x06002B1A RID: 11034 RVA: 0x0009D12F File Offset: 0x0009B32F
		public string Url
		{
			get
			{
				if (this.userUrl == null)
				{
					this.userUrl = this.url.Value;
				}
				return this.userUrl;
			}
			set
			{
				this.url = new Url(value);
			}
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0009D140 File Offset: 0x0009B340
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			string value = this.url.Value;
			int num = value.LastIndexOf("*");
			if (num == -1)
			{
				num = value.Length;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is Url && string.Compare(value, 0, (hostEnumerator.Current as Url).Value, 0, num, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x0009D1B7 File Offset: 0x0009B3B7
		public IMembershipCondition Copy()
		{
			return new UrlMembershipCondition(this.url, this.userUrl);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x0009D1CC File Offset: 0x0009B3CC
		public override bool Equals(object o)
		{
			UrlMembershipCondition urlMembershipCondition = o as UrlMembershipCondition;
			if (o == null)
			{
				return false;
			}
			string value = this.url.Value;
			int num = value.Length;
			if (value[num - 1] == '*')
			{
				num--;
				if (value[num - 1] == '/')
				{
					num--;
				}
			}
			return string.Compare(value, 0, urlMembershipCondition.Url, 0, num, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0009D232 File Offset: 0x0009B432
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x0009D23C File Offset: 0x0009B43C
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			string text = e.Attribute("Url");
			if (text != null)
			{
				this.CheckUrl(text);
				this.url = new Url(text);
			}
			else
			{
				this.url = null;
			}
			this.userUrl = text;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x0009D293 File Offset: 0x0009B493
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0009D2A0 File Offset: 0x0009B4A0
		public override string ToString()
		{
			return "Url - " + this.Url;
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x0009D2B2 File Offset: 0x0009B4B2
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x0009D2BB File Offset: 0x0009B4BB
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(UrlMembershipCondition), this.version);
			securityElement.AddAttribute("Url", this.userUrl);
			return securityElement;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x0009D2E4 File Offset: 0x0009B4E4
		internal void CheckUrl(string url)
		{
			if (new Uri((url.IndexOf(Uri.SchemeDelimiter) < 0) ? ("file://" + url) : url, false, false).Host.IndexOf('*') >= 1)
			{
				throw new ArgumentException(Locale.GetText("Invalid * character in url"), "name");
			}
		}

		// Token: 0x04001E92 RID: 7826
		private readonly int version = 1;

		// Token: 0x04001E93 RID: 7827
		private Url url;

		// Token: 0x04001E94 RID: 7828
		private string userUrl;
	}
}
