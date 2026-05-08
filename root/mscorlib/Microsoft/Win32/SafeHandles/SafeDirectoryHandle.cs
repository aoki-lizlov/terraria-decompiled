using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000095 RID: 149
	internal sealed class SafeDirectoryHandle : SafeHandle
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x000180B6 File Offset: 0x000162B6
		private SafeDirectoryHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000180C4 File Offset: 0x000162C4
		protected override bool ReleaseHandle()
		{
			return Interop.Sys.CloseDir(this.handle) == 0;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000180D4 File Offset: 0x000162D4
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
