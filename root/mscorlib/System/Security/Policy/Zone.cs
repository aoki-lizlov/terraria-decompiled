using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	// Token: 0x020003F7 RID: 1015
	[ComVisible(true)]
	[Serializable]
	public sealed class Zone : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		// Token: 0x06002B25 RID: 11045 RVA: 0x0009D338 File Offset: 0x0009B538
		public Zone(SecurityZone zone)
		{
			if (!Enum.IsDefined(typeof(SecurityZone), zone))
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid zone {0}."), zone), "zone");
			}
			this.zone = zone;
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x0009D389 File Offset: 0x0009B589
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x0009D391 File Offset: 0x0009B591
		public object Copy()
		{
			return new Zone(this.zone);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0009D39E File Offset: 0x0009B59E
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new ZoneIdentityPermission(this.zone);
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x0009D3AC File Offset: 0x0009B5AC
		[MonoTODO("Not user configurable yet")]
		public static Zone CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			SecurityZone securityZone = SecurityZone.NoZone;
			if (url.Length == 0)
			{
				return new Zone(securityZone);
			}
			Uri uri = null;
			try
			{
				uri = new Uri(url);
			}
			catch
			{
				return new Zone(securityZone);
			}
			if (securityZone == SecurityZone.NoZone)
			{
				if (uri.IsFile)
				{
					if (File.Exists(uri.LocalPath))
					{
						securityZone = SecurityZone.MyComputer;
					}
					else if (string.Compare("FILE://", 0, url, 0, 7, true, CultureInfo.InvariantCulture) == 0)
					{
						securityZone = SecurityZone.Intranet;
					}
					else
					{
						securityZone = SecurityZone.Internet;
					}
				}
				else if (uri.IsLoopback)
				{
					securityZone = SecurityZone.Intranet;
				}
				else
				{
					securityZone = SecurityZone.Internet;
				}
			}
			return new Zone(securityZone);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x0009D450 File Offset: 0x0009B650
		public override bool Equals(object o)
		{
			Zone zone = o as Zone;
			return zone != null && zone.zone == this.zone;
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x0009D389 File Offset: 0x0009B589
		public override int GetHashCode()
		{
			return (int)this.zone;
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x0009D478 File Offset: 0x0009B678
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Zone", this.zone.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x00019B62 File Offset: 0x00017D62
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 3;
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x0009D4C5 File Offset: 0x0009B6C5
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			char c = buffer[position++];
			char c2 = buffer[position++];
			return position;
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x0009D4DA File Offset: 0x0009B6DA
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position++] = '\u0003';
			buffer[position++] = (char)(this.zone >> 16);
			buffer[position++] = (char)(this.zone & (SecurityZone)65535);
			return position;
		}

		// Token: 0x04001E95 RID: 7829
		private SecurityZone zone;
	}
}
