using System;

namespace System.Security.Permissions
{
	// Token: 0x0200040E RID: 1038
	[Serializable]
	internal sealed class HostProtectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002BC9 RID: 11209 RVA: 0x0009EFFC File Offset: 0x0009D1FC
		public HostProtectionPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._resources = HostProtectionResource.All;
				return;
			}
			this._resources = HostProtectionResource.None;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x0009F021 File Offset: 0x0009D221
		public HostProtectionPermission(HostProtectionResource resources)
		{
			this.Resources = this._resources;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x0009F035 File Offset: 0x0009D235
		// (set) Token: 0x06002BCC RID: 11212 RVA: 0x0009F03D File Offset: 0x0009D23D
		public HostProtectionResource Resources
		{
			get
			{
				return this._resources;
			}
			set
			{
				if (!Enum.IsDefined(typeof(HostProtectionResource), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "HostProtectionResource");
				}
				this._resources = value;
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x0009F07D File Offset: 0x0009D27D
		public override IPermission Copy()
		{
			return new HostProtectionPermission(this._resources);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x0009F08C File Offset: 0x0009D28C
		public override IPermission Intersect(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && hostProtectionPermission.IsUnrestricted())
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return hostProtectionPermission.Copy();
			}
			if (hostProtectionPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			return new HostProtectionPermission(this._resources & hostProtectionPermission._resources);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x0009F0F0 File Offset: 0x0009D2F0
		public override IPermission Union(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || hostProtectionPermission.IsUnrestricted())
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			return new HostProtectionPermission(this._resources | hostProtectionPermission._resources);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0009F138 File Offset: 0x0009D338
		public override bool IsSubsetOf(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return this._resources == HostProtectionResource.None;
			}
			return hostProtectionPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this._resources & ~hostProtectionPermission._resources) == HostProtectionResource.None);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0009F17F File Offset: 0x0009D37F
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._resources = (HostProtectionResource)Enum.Parse(typeof(HostProtectionResource), e.Attribute("Resources"));
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0009F1B4 File Offset: 0x0009D3B4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			securityElement.AddAttribute("Resources", this._resources.ToString());
			return securityElement;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x0009F1D9 File Offset: 0x0009D3D9
		public bool IsUnrestricted()
		{
			return this._resources == HostProtectionResource.All;
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x00029E66 File Offset: 0x00028066
		int IBuiltInPermission.GetTokenIndex()
		{
			return 9;
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x0009F1E8 File Offset: 0x0009D3E8
		private HostProtectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			HostProtectionPermission hostProtectionPermission = target as HostProtectionPermission;
			if (hostProtectionPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(HostProtectionPermission));
			}
			return hostProtectionPermission;
		}

		// Token: 0x04001EF9 RID: 7929
		private const int version = 1;

		// Token: 0x04001EFA RID: 7930
		private HostProtectionResource _resources;
	}
}
