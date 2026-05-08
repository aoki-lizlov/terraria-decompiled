using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000414 RID: 1044
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class IsolatedStoragePermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002BEA RID: 11242 RVA: 0x0009DE0C File Offset: 0x0009C00C
		protected IsolatedStoragePermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x0009F6E8 File Offset: 0x0009D8E8
		// (set) Token: 0x06002BEC RID: 11244 RVA: 0x0009F6F0 File Offset: 0x0009D8F0
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.usage_allowed;
			}
			set
			{
				this.usage_allowed = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x0009F6F9 File Offset: 0x0009D8F9
		// (set) Token: 0x06002BEE RID: 11246 RVA: 0x0009F701 File Offset: 0x0009D901
		public long UserQuota
		{
			get
			{
				return this.user_quota;
			}
			set
			{
				this.user_quota = value;
			}
		}

		// Token: 0x04001F13 RID: 7955
		private IsolatedStorageContainment usage_allowed;

		// Token: 0x04001F14 RID: 7956
		private long user_quota;
	}
}
