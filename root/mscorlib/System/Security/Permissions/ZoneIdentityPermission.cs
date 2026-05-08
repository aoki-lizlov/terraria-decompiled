using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000434 RID: 1076
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002D4F RID: 11599 RVA: 0x000A3A40 File Offset: 0x000A1C40
		public ZoneIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
			this.zone = SecurityZone.NoZone;
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000A3A57 File Offset: 0x000A1C57
		public ZoneIdentityPermission(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000A3A66 File Offset: 0x000A1C66
		public override IPermission Copy()
		{
			return new ZoneIdentityPermission(this.zone);
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000A3A74 File Offset: 0x000A1C74
		public override bool IsSubsetOf(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null)
			{
				return this.zone == SecurityZone.NoZone;
			}
			return this.zone == SecurityZone.NoZone || this.zone == zoneIdentityPermission.zone;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000A3AB0 File Offset: 0x000A1CB0
		public override IPermission Union(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null)
			{
				if (this.zone != SecurityZone.NoZone)
				{
					return this.Copy();
				}
				return null;
			}
			else
			{
				if (this.zone == zoneIdentityPermission.zone || zoneIdentityPermission.zone == SecurityZone.NoZone)
				{
					return this.Copy();
				}
				if (this.zone == SecurityZone.NoZone)
				{
					return zoneIdentityPermission.Copy();
				}
				throw new ArgumentException(Locale.GetText("Union impossible"));
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000A3B18 File Offset: 0x000A1D18
		public override IPermission Intersect(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null || this.zone == SecurityZone.NoZone)
			{
				return null;
			}
			if (this.zone == zoneIdentityPermission.zone)
			{
				return this.Copy();
			}
			return null;
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000A3B54 File Offset: 0x000A1D54
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Zone");
			if (text == null)
			{
				this.zone = SecurityZone.NoZone;
				return;
			}
			this.zone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000A3BA4 File Offset: 0x000A1DA4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.zone != SecurityZone.NoZone)
			{
				securityElement.AddAttribute("Zone", this.zone.ToString());
			}
			return securityElement;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000A3BDF File Offset: 0x000A1DDF
		// (set) Token: 0x06002D58 RID: 11608 RVA: 0x000A3BE7 File Offset: 0x000A1DE7
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SecurityZone), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "SecurityZone");
				}
				this.zone = value;
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x00020036 File Offset: 0x0001E236
		int IBuiltInPermission.GetTokenIndex()
		{
			return 14;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000A3C27 File Offset: 0x000A1E27
		private ZoneIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
			if (zoneIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(ZoneIdentityPermission));
			}
			return zoneIdentityPermission;
		}

		// Token: 0x04001F8B RID: 8075
		private const int version = 1;

		// Token: 0x04001F8C RID: 8076
		private SecurityZone zone;
	}
}
