using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000427 RID: 1063
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002CB7 RID: 11447 RVA: 0x000A1C50 File Offset: 0x0009FE50
		public SecurityPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.flags = SecurityPermissionFlag.AllFlags;
				return;
			}
			this.flags = SecurityPermissionFlag.NoFlags;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000A1C75 File Offset: 0x0009FE75
		public SecurityPermission(SecurityPermissionFlag flag)
		{
			this.Flags = flag;
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000A1C84 File Offset: 0x0009FE84
		// (set) Token: 0x06002CBA RID: 11450 RVA: 0x000A1C8C File Offset: 0x0009FE8C
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				if ((value & SecurityPermissionFlag.AllFlags) != value)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid flags {0}"), value), "SecurityPermissionFlag");
				}
				this.flags = value;
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000A1CBF File Offset: 0x0009FEBF
		public bool IsUnrestricted()
		{
			return this.flags == SecurityPermissionFlag.AllFlags;
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000A1CCE File Offset: 0x0009FECE
		public override IPermission Copy()
		{
			return new SecurityPermission(this.flags);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000A1CDC File Offset: 0x0009FEDC
		public override IPermission Intersect(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return null;
			}
			if (this.IsEmpty() || securityPermission.IsEmpty())
			{
				return null;
			}
			if (this.IsUnrestricted() && securityPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return securityPermission.Copy();
			}
			if (securityPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			SecurityPermissionFlag securityPermissionFlag = this.flags & securityPermission.flags;
			if (securityPermissionFlag == SecurityPermissionFlag.NoFlags)
			{
				return null;
			}
			return new SecurityPermission(securityPermissionFlag);
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000A1D58 File Offset: 0x0009FF58
		public override IPermission Union(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || securityPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.flags | securityPermission.flags);
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000A1DA0 File Offset: 0x0009FFA0
		public override bool IsSubsetOf(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return this.IsEmpty();
			}
			return securityPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this.flags & ~securityPermission.flags) == SecurityPermissionFlag.NoFlags);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000A1DE4 File Offset: 0x0009FFE4
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.flags = SecurityPermissionFlag.AllFlags;
				return;
			}
			string text = esd.Attribute("Flags");
			if (text == null)
			{
				this.flags = SecurityPermissionFlag.NoFlags;
				return;
			}
			this.flags = (SecurityPermissionFlag)Enum.Parse(typeof(SecurityPermissionFlag), text);
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000A1E48 File Offset: 0x000A0048
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Flags", this.flags.ToString());
			}
			return securityElement;
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x00019E33 File Offset: 0x00018033
		int IBuiltInPermission.GetTokenIndex()
		{
			return 6;
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000A1E94 File Offset: 0x000A0094
		private bool IsEmpty()
		{
			return this.flags == SecurityPermissionFlag.NoFlags;
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000A1E9F File Offset: 0x000A009F
		private SecurityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SecurityPermission securityPermission = target as SecurityPermission;
			if (securityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(SecurityPermission));
			}
			return securityPermission;
		}

		// Token: 0x04001F60 RID: 8032
		private const int version = 1;

		// Token: 0x04001F61 RID: 8033
		private SecurityPermissionFlag flags;
	}
}
