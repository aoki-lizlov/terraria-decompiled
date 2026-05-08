using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006ED RID: 1773
	[SecurityCritical]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x06004082 RID: 16514 RVA: 0x000E1214 File Offset: 0x000DF414
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
		{
			this.handle = invalidHandleValue;
			this._state = 4;
			this._ownsHandle = ownsHandle;
			if (!ownsHandle)
			{
				GC.SuppressFinalize(this);
			}
			this._fullyInitialized = true;
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000E1244 File Offset: 0x000DF444
		[SecuritySafeCritical]
		~SafeHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000E1274 File Offset: 0x000DF474
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x000E127D File Offset: 0x000DF47D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000E1285 File Offset: 0x000DF485
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return (this._state & 1) == 1;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06004087 RID: 16519
		public abstract bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get;
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x000E1292 File Offset: 0x000DF492
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x000E1292 File Offset: 0x000DF492
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x000E129B File Offset: 0x000DF49B
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalDispose();
				return;
			}
			this.InternalFinalize();
		}

		// Token: 0x0600408B RID: 16523
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		// Token: 0x0600408C RID: 16524 RVA: 0x000E12B0 File Offset: 0x000DF4B0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void SetHandleAsInvalid()
		{
			try
			{
			}
			finally
			{
				int state;
				int num;
				do
				{
					state = this._state;
					num = state | 1;
				}
				while (Interlocked.CompareExchange(ref this._state, num, state) != state);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x000E12F4 File Offset: 0x000DF4F4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void DangerousAddRef(ref bool success)
		{
			try
			{
			}
			finally
			{
				if (!this._fullyInitialized)
				{
					throw new InvalidOperationException();
				}
				for (;;)
				{
					int state = this._state;
					if ((state & 1) != 0)
					{
						break;
					}
					int num = state + 4;
					if (Interlocked.CompareExchange(ref this._state, num, state) == state)
					{
						goto Block_5;
					}
				}
				throw new ObjectDisposedException(null, "Safe handle has been closed");
				Block_5:
				success = true;
			}
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x000E1354 File Offset: 0x000DF554
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void DangerousRelease()
		{
			this.DangerousReleaseInternal(false);
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x000E135D File Offset: 0x000DF55D
		private void InternalDispose()
		{
			if (!this._fullyInitialized)
			{
				throw new InvalidOperationException();
			}
			this.DangerousReleaseInternal(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x000E137A File Offset: 0x000DF57A
		private void InternalFinalize()
		{
			if (this._fullyInitialized)
			{
				this.DangerousReleaseInternal(true);
			}
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x000E138C File Offset: 0x000DF58C
		private void DangerousReleaseInternal(bool dispose)
		{
			try
			{
			}
			finally
			{
				if (!this._fullyInitialized)
				{
					throw new InvalidOperationException();
				}
				bool flag;
				for (;;)
				{
					int state = this._state;
					if (dispose && (state & 2) != 0)
					{
						break;
					}
					if ((state & 2147483644) == 0)
					{
						goto Block_6;
					}
					flag = (state & 2147483644) == 4 && (state & 1) == 0 && this._ownsHandle && !this.IsInvalid;
					int num = state - 4;
					if ((state & 2147483644) == 4)
					{
						num |= 1;
					}
					if (dispose)
					{
						num |= 2;
					}
					if (Interlocked.CompareExchange(ref this._state, num, state) == state)
					{
						goto IL_009A;
					}
				}
				flag = false;
				goto IL_009A;
				Block_6:
				throw new ObjectDisposedException(null, "Safe handle has been closed");
				IL_009A:
				if (flag)
				{
					this.ReleaseHandle();
				}
			}
		}

		// Token: 0x04002A84 RID: 10884
		protected IntPtr handle;

		// Token: 0x04002A85 RID: 10885
		private int _state;

		// Token: 0x04002A86 RID: 10886
		private bool _ownsHandle;

		// Token: 0x04002A87 RID: 10887
		private bool _fullyInitialized;

		// Token: 0x04002A88 RID: 10888
		private const int RefCount_Mask = 2147483644;

		// Token: 0x04002A89 RID: 10889
		private const int RefCount_One = 4;

		// Token: 0x020006EE RID: 1774
		private enum State
		{
			// Token: 0x04002A8B RID: 10891
			Closed = 1,
			// Token: 0x04002A8C RID: 10892
			Disposed
		}
	}
}
