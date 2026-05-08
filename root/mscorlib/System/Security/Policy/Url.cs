using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	// Token: 0x020003F5 RID: 1013
	[ComVisible(true)]
	[Serializable]
	public sealed class Url : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		// Token: 0x06002B0B RID: 11019 RVA: 0x0009CEE8 File Offset: 0x0009B0E8
		public Url(string name)
			: this(name, false)
		{
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x0009CEF2 File Offset: 0x0009B0F2
		internal Url(string name, bool validated)
		{
			this.origin_url = (validated ? name : this.Prepare(name));
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x0009CF0D File Offset: 0x0009B10D
		public object Copy()
		{
			return new Url(this.origin_url, true);
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x0009CF1B File Offset: 0x0009B11B
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new UrlIdentityPermission(this.origin_url);
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x0009CF28 File Offset: 0x0009B128
		public override bool Equals(object o)
		{
			Url url = o as Url;
			if (url == null)
			{
				return false;
			}
			string text = url.Value;
			string text2 = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			if (text2.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text2 = "file://" + text2;
			}
			return string.Compare(text, text2, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x0009CF94 File Offset: 0x0009B194
		public override int GetHashCode()
		{
			string text = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			return text.GetHashCode();
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x0009CFC8 File Offset: 0x0009B1C8
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Url", this.origin_url));
			return securityElement.ToString();
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x0009CFFF File Offset: 0x0009B1FF
		public string Value
		{
			get
			{
				return this.origin_url;
			}
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0009D007 File Offset: 0x0009B207
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.origin_url.Length;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x0009D01C File Offset: 0x0009B21C
		private string Prepare(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("Url");
			}
			if (url == string.Empty)
			{
				throw new FormatException(Locale.GetText("Invalid (empty) Url"));
			}
			if (url.IndexOf(Uri.SchemeDelimiter) > 0)
			{
				if (url.StartsWith("file://"))
				{
					url = "file://" + url.Substring(7);
				}
				url = new Uri(url, false, false).ToString();
			}
			int num = url.Length - 1;
			if (url[num] == '/')
			{
				url = url.Substring(0, num);
			}
			return url;
		}

		// Token: 0x04001E91 RID: 7825
		private string origin_url;
	}
}
