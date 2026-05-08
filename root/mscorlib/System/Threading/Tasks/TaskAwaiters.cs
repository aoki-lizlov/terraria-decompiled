using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002E7 RID: 743
	internal static class TaskAwaiters
	{
		// Token: 0x0600217C RID: 8572 RVA: 0x000792DD File Offset: 0x000774DD
		public static ForceAsyncAwaiter ForceAsync(this Task task)
		{
			return new ForceAsyncAwaiter(task);
		}
	}
}
