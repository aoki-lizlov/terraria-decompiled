using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007ED RID: 2029
	internal static class AsyncTaskCache
	{
		// Token: 0x06004618 RID: 17944 RVA: 0x000E65A0 File Offset: 0x000E47A0
		private static Task<int>[] CreateInt32Tasks()
		{
			Task<int>[] array = new Task<int>[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AsyncTaskCache.CreateCacheableTask<int>(i + -1);
			}
			return array;
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x000E65D0 File Offset: 0x000E47D0
		internal static Task<TResult> CreateCacheableTask<TResult>(TResult result)
		{
			return new Task<TResult>(false, result, (TaskCreationOptions)16384, default(CancellationToken));
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x000E65F2 File Offset: 0x000E47F2
		// Note: this type is marked as 'beforefieldinit'.
		static AsyncTaskCache()
		{
		}

		// Token: 0x04002CDF RID: 11487
		internal static readonly Task<bool> TrueTask = AsyncTaskCache.CreateCacheableTask<bool>(true);

		// Token: 0x04002CE0 RID: 11488
		internal static readonly Task<bool> FalseTask = AsyncTaskCache.CreateCacheableTask<bool>(false);

		// Token: 0x04002CE1 RID: 11489
		internal static readonly Task<int>[] Int32Tasks = AsyncTaskCache.CreateInt32Tasks();

		// Token: 0x04002CE2 RID: 11490
		internal const int INCLUSIVE_INT32_MIN = -1;

		// Token: 0x04002CE3 RID: 11491
		internal const int EXCLUSIVE_INT32_MAX = 9;
	}
}
