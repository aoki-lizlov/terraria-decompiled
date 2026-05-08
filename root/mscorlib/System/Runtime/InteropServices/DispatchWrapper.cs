using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006E6 RID: 1766
	[ComVisible(true)]
	[Serializable]
	public sealed class DispatchWrapper
	{
		// Token: 0x0600406A RID: 16490 RVA: 0x000E114D File Offset: 0x000DF34D
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public DispatchWrapper(object obj)
		{
			if (obj != null)
			{
				Marshal.Release(Marshal.GetIDispatchForObject(obj));
			}
			this.m_WrappedObject = obj;
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x000E116B File Offset: 0x000DF36B
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002A7C RID: 10876
		private object m_WrappedObject;
	}
}
