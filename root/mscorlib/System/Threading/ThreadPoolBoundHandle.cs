using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020002D8 RID: 728
	public sealed class ThreadPoolBoundHandle : IDisposable
	{
		// Token: 0x06002117 RID: 8471 RVA: 0x000025BE File Offset: 0x000007BE
		internal ThreadPoolBoundHandle()
		{
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public SafeHandle Handle
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0003CB93 File Offset: 0x0003AD93
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0003CB93 File Offset: 0x0003AD93
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped(PreAllocatedOverlapped preAllocated)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public static ThreadPoolBoundHandle BindHandle(SafeHandle handle)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00004088 File Offset: 0x00002288
		public void Dispose()
		{
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0003CB93 File Offset: 0x0003AD93
		[CLSCompliant(false)]
		public unsafe void FreeNativeOverlapped(NativeOverlapped* overlapped)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0003CB93 File Offset: 0x0003AD93
		[CLSCompliant(false)]
		public unsafe static object GetNativeOverlappedState(NativeOverlapped* overlapped)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
