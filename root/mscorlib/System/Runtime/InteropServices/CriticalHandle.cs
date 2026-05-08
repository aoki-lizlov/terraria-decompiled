using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006E5 RID: 1765
	[SecurityCritical]
	public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x0600405E RID: 16478 RVA: 0x000E109C File Offset: 0x000DF29C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandle(IntPtr invalidHandleValue)
		{
			this.handle = invalidHandleValue;
			this._isClosed = false;
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000E10B4 File Offset: 0x000DF2B4
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000E10E4 File Offset: 0x000DF2E4
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Cleanup()
		{
			if (this.IsClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this.IsInvalid)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!this.ReleaseHandle())
			{
				CriticalHandle.FireCustomerDebugProbe();
			}
			Marshal.SetLastWin32Error(lastWin32Error);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x00004088 File Offset: 0x00002288
		private static void FireCustomerDebugProbe()
		{
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000E111C File Offset: 0x000DF31C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x000E1125 File Offset: 0x000DF325
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06004064 RID: 16484
		public abstract bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get;
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000E112D File Offset: 0x000DF32D
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000E112D File Offset: 0x000DF32D
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000E1136 File Offset: 0x000DF336
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Dispose(bool disposing)
		{
			this.Cleanup();
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000E113E File Offset: 0x000DF33E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void SetHandleAsInvalid()
		{
			this._isClosed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004069 RID: 16489
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		// Token: 0x04002A7A RID: 10874
		protected IntPtr handle;

		// Token: 0x04002A7B RID: 10875
		private bool _isClosed;
	}
}
