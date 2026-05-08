using System;

namespace System
{
	// Token: 0x020001F6 RID: 502
	internal struct RuntimeMethodHandleInternal
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0005E27C File Offset: 0x0005C47C
		internal static RuntimeMethodHandleInternal EmptyHandle
		{
			get
			{
				return default(RuntimeMethodHandleInternal);
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0005E292 File Offset: 0x0005C492
		internal bool IsNullHandle()
		{
			return this.m_handle.IsNull();
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0005E29F File Offset: 0x0005C49F
		internal IntPtr Value
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0005E2A7 File Offset: 0x0005C4A7
		internal RuntimeMethodHandleInternal(IntPtr value)
		{
			this.m_handle = value;
		}

		// Token: 0x04001598 RID: 5528
		internal IntPtr m_handle;
	}
}
