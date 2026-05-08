using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200040D RID: 1037
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002BB2 RID: 11186 RVA: 0x0009EDDC File Offset: 0x0009CFDC
		public HostProtectionAttribute()
			: base(SecurityAction.LinkDemand)
		{
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x0009EDE5 File Offset: 0x0009CFE5
		public HostProtectionAttribute(SecurityAction action)
			: base(action)
		{
			if (action != SecurityAction.LinkDemand)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Only {0} is accepted."), SecurityAction.LinkDemand), "action");
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x0009EE12 File Offset: 0x0009D012
		// (set) Token: 0x06002BB5 RID: 11189 RVA: 0x0009EE1F File Offset: 0x0009D01F
		public bool ExternalProcessMgmt
		{
			get
			{
				return (this._resources & HostProtectionResource.ExternalProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.ExternalProcessMgmt;
					return;
				}
				this._resources &= ~HostProtectionResource.ExternalProcessMgmt;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x0009EE42 File Offset: 0x0009D042
		// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x0009EE50 File Offset: 0x0009D050
		public bool ExternalThreading
		{
			get
			{
				return (this._resources & HostProtectionResource.ExternalThreading) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.ExternalThreading;
					return;
				}
				this._resources &= ~HostProtectionResource.ExternalThreading;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x0009EE74 File Offset: 0x0009D074
		// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x0009EE85 File Offset: 0x0009D085
		public bool MayLeakOnAbort
		{
			get
			{
				return (this._resources & HostProtectionResource.MayLeakOnAbort) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.MayLeakOnAbort;
					return;
				}
				this._resources &= ~HostProtectionResource.MayLeakOnAbort;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x0009EEAF File Offset: 0x0009D0AF
		// (set) Token: 0x06002BBB RID: 11195 RVA: 0x0009EEBD File Offset: 0x0009D0BD
		[ComVisible(true)]
		public bool SecurityInfrastructure
		{
			get
			{
				return (this._resources & HostProtectionResource.SecurityInfrastructure) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SecurityInfrastructure;
					return;
				}
				this._resources &= ~HostProtectionResource.SecurityInfrastructure;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x0009EEE1 File Offset: 0x0009D0E1
		// (set) Token: 0x06002BBD RID: 11197 RVA: 0x0009EEEE File Offset: 0x0009D0EE
		public bool SelfAffectingProcessMgmt
		{
			get
			{
				return (this._resources & HostProtectionResource.SelfAffectingProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SelfAffectingProcessMgmt;
					return;
				}
				this._resources &= ~HostProtectionResource.SelfAffectingProcessMgmt;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x0009EF11 File Offset: 0x0009D111
		// (set) Token: 0x06002BBF RID: 11199 RVA: 0x0009EF1F File Offset: 0x0009D11F
		public bool SelfAffectingThreading
		{
			get
			{
				return (this._resources & HostProtectionResource.SelfAffectingThreading) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SelfAffectingThreading;
					return;
				}
				this._resources &= ~HostProtectionResource.SelfAffectingThreading;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x0009EF43 File Offset: 0x0009D143
		// (set) Token: 0x06002BC1 RID: 11201 RVA: 0x0009EF50 File Offset: 0x0009D150
		public bool SharedState
		{
			get
			{
				return (this._resources & HostProtectionResource.SharedState) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SharedState;
					return;
				}
				this._resources &= ~HostProtectionResource.SharedState;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x0009EF73 File Offset: 0x0009D173
		// (set) Token: 0x06002BC3 RID: 11203 RVA: 0x0009EF80 File Offset: 0x0009D180
		public bool Synchronization
		{
			get
			{
				return (this._resources & HostProtectionResource.Synchronization) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.Synchronization;
					return;
				}
				this._resources &= ~HostProtectionResource.Synchronization;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x0009EFA3 File Offset: 0x0009D1A3
		// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x0009EFB4 File Offset: 0x0009D1B4
		public bool UI
		{
			get
			{
				return (this._resources & HostProtectionResource.UI) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.UI;
					return;
				}
				this._resources &= ~HostProtectionResource.UI;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x0009EFDE File Offset: 0x0009D1DE
		// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x0009EFE6 File Offset: 0x0009D1E6
		public HostProtectionResource Resources
		{
			get
			{
				return this._resources;
			}
			set
			{
				this._resources = value;
			}
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x0009EFEF File Offset: 0x0009D1EF
		public override IPermission CreatePermission()
		{
			return new HostProtectionPermission(this._resources);
		}

		// Token: 0x04001EF8 RID: 7928
		private HostProtectionResource _resources;
	}
}
