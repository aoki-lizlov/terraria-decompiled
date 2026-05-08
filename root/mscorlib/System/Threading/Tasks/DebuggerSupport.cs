using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	// Token: 0x0200030E RID: 782
	internal static class DebuggerSupport
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x0000408A File Offset: 0x0000228A
		public static bool LoggingOn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00004088 File Offset: 0x00002288
		public static void TraceOperationCreation(CausalityTraceLevel traceLevel, Task task, string operationName, ulong relatedContext)
		{
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00004088 File Offset: 0x00002288
		public static void TraceOperationCompletion(CausalityTraceLevel traceLevel, Task task, AsyncStatus status)
		{
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00004088 File Offset: 0x00002288
		public static void TraceOperationRelation(CausalityTraceLevel traceLevel, Task task, CausalityRelation relation)
		{
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00004088 File Offset: 0x00002288
		public static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, Task task, CausalitySynchronousWork work)
		{
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x00004088 File Offset: 0x00002288
		public static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
		{
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0007CE15 File Offset: 0x0007B015
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddToActiveTasks(Task task)
		{
			if (Task.s_asyncDebuggingEnabled)
			{
				DebuggerSupport.AddToActiveTasksNonInlined(task);
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0007CE24 File Offset: 0x0007B024
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void AddToActiveTasksNonInlined(Task task)
		{
			int id = task.Id;
			object obj = DebuggerSupport.s_activeTasksLock;
			lock (obj)
			{
				DebuggerSupport.s_activeTasks[id] = task;
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0007CE70 File Offset: 0x0007B070
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveFromActiveTasks(Task task)
		{
			if (Task.s_asyncDebuggingEnabled)
			{
				DebuggerSupport.RemoveFromActiveTasksNonInlined(task);
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0007CE80 File Offset: 0x0007B080
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void RemoveFromActiveTasksNonInlined(Task task)
		{
			int id = task.Id;
			object obj = DebuggerSupport.s_activeTasksLock;
			lock (obj)
			{
				DebuggerSupport.s_activeTasks.Remove(id);
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0007CECC File Offset: 0x0007B0CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task GetActiveTaskFromId(int taskId)
		{
			Task task = null;
			DebuggerSupport.s_activeTasks.TryGetValue(taskId, out task);
			return task;
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x0007CEEA File Offset: 0x0007B0EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task GetTaskIfDebuggingEnabled(this AsyncVoidMethodBuilder builder)
		{
			if (DebuggerSupport.LoggingOn || Task.s_asyncDebuggingEnabled)
			{
				return builder.Task;
			}
			return null;
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0007CF03 File Offset: 0x0007B103
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task GetTaskIfDebuggingEnabled(this AsyncTaskMethodBuilder builder)
		{
			if (DebuggerSupport.LoggingOn || Task.s_asyncDebuggingEnabled)
			{
				return builder.Task;
			}
			return null;
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x0007CF1C File Offset: 0x0007B11C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task GetTaskIfDebuggingEnabled<TResult>(this AsyncTaskMethodBuilder<TResult> builder)
		{
			if (DebuggerSupport.LoggingOn || Task.s_asyncDebuggingEnabled)
			{
				return builder.Task;
			}
			return null;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0007CF35 File Offset: 0x0007B135
		// Note: this type is marked as 'beforefieldinit'.
		static DebuggerSupport()
		{
		}

		// Token: 0x04001B4C RID: 6988
		private static readonly LowLevelDictionary<int, Task> s_activeTasks = new LowLevelDictionary<int, Task>();

		// Token: 0x04001B4D RID: 6989
		private static readonly object s_activeTasksLock = new object();
	}
}
