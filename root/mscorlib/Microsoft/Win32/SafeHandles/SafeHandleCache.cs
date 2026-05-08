using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000096 RID: 150
	internal static class SafeHandleCache<T> where T : SafeHandle
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x000180E8 File Offset: 0x000162E8
		internal static T GetInvalidHandle(Func<T> invalidHandleFactory)
		{
			T t = Volatile.Read<T>(ref SafeHandleCache<T>.s_invalidHandle);
			if (t == null)
			{
				T t2 = invalidHandleFactory();
				t = Interlocked.CompareExchange<T>(ref SafeHandleCache<T>.s_invalidHandle, t2, default(T));
				if (t == null)
				{
					GC.SuppressFinalize(t2);
					t = t2;
				}
				else
				{
					t2.Dispose();
				}
			}
			return t;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00018147 File Offset: 0x00016347
		internal static bool IsCachedInvalidHandle(SafeHandle handle)
		{
			return handle == Volatile.Read<T>(ref SafeHandleCache<T>.s_invalidHandle);
		}

		// Token: 0x04000EC4 RID: 3780
		private static T s_invalidHandle;
	}
}
