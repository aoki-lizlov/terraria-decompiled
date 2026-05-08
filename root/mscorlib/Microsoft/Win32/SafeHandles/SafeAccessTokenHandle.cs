using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020000A1 RID: 161
	[SecurityCritical]
	public sealed class SafeAccessTokenHandle : SafeHandle
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x000180B6 File Offset: 0x000162B6
		private SafeAccessTokenHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001836C File Offset: 0x0001656C
		public SafeAccessTokenHandle(IntPtr handle)
			: base(IntPtr.Zero, true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00018381 File Offset: 0x00016581
		public static SafeAccessTokenHandle InvalidHandle
		{
			[SecurityCritical]
			get
			{
				return new SafeAccessTokenHandle(IntPtr.Zero);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001838D File Offset: 0x0001658D
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero || this.handle == new IntPtr(-1);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00003FB7 File Offset: 0x000021B7
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return true;
		}
	}
}
