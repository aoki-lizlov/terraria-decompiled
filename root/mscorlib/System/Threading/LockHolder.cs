using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000287 RID: 647
	[ReflectionBlocked]
	public struct LockHolder : IDisposable
	{
		// Token: 0x06001E26 RID: 7718 RVA: 0x00070EF8 File Offset: 0x0006F0F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static LockHolder Hold(Lock l)
		{
			l.Acquire();
			LockHolder lockHolder;
			lockHolder._lock = l;
			return lockHolder;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00070F14 File Offset: 0x0006F114
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			this._lock.Release();
		}

		// Token: 0x04001982 RID: 6530
		private Lock _lock;
	}
}
