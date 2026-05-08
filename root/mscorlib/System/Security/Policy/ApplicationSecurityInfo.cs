using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003D1 RID: 977
	[ComVisible(true)]
	public sealed class ApplicationSecurityInfo
	{
		// Token: 0x06002991 RID: 10641 RVA: 0x00098258 File Offset: 0x00096458
		public ApplicationSecurityInfo(ActivationContext activationContext)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x0009826E File Offset: 0x0009646E
		// (set) Token: 0x06002993 RID: 10643 RVA: 0x00098276 File Offset: 0x00096476
		public Evidence ApplicationEvidence
		{
			get
			{
				return this._evidence;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationEvidence");
				}
				this._evidence = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x0009828D File Offset: 0x0009648D
		// (set) Token: 0x06002995 RID: 10645 RVA: 0x00098295 File Offset: 0x00096495
		public ApplicationId ApplicationId
		{
			get
			{
				return this._appid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationId");
				}
				this._appid = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x000982AC File Offset: 0x000964AC
		// (set) Token: 0x06002997 RID: 10647 RVA: 0x000982C3 File Offset: 0x000964C3
		public PermissionSet DefaultRequestSet
		{
			get
			{
				if (this._defaultSet == null)
				{
					return new PermissionSet(PermissionState.None);
				}
				return this._defaultSet;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DefaultRequestSet");
				}
				this._defaultSet = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x000982DA File Offset: 0x000964DA
		// (set) Token: 0x06002999 RID: 10649 RVA: 0x000982E2 File Offset: 0x000964E2
		public ApplicationId DeploymentId
		{
			get
			{
				return this._deployid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DeploymentId");
				}
				this._deployid = value;
			}
		}

		// Token: 0x04001E20 RID: 7712
		private Evidence _evidence;

		// Token: 0x04001E21 RID: 7713
		private ApplicationId _appid;

		// Token: 0x04001E22 RID: 7714
		private PermissionSet _defaultSet;

		// Token: 0x04001E23 RID: 7715
		private ApplicationId _deployid;
	}
}
