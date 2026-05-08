using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000355 RID: 853
	[FriendAccessAllowed]
	internal static class AsyncCausalityTracer
	{
		// Token: 0x06002502 RID: 9474 RVA: 0x00004088 File Offset: 0x00002288
		internal static void EnableToETW(bool enabled)
		{
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x0000408A File Offset: 0x0000228A
		[FriendAccessAllowed]
		internal static bool LoggingOn
		{
			[FriendAccessAllowed]
			get
			{
				return false;
			}
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00004088 File Offset: 0x00002288
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCreation(CausalityTraceLevel traceLevel, int taskId, string operationName, ulong relatedContext)
		{
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x00004088 File Offset: 0x00002288
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCompletion(CausalityTraceLevel traceLevel, int taskId, AsyncCausalityStatus status)
		{
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x00004088 File Offset: 0x00002288
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationRelation(CausalityTraceLevel traceLevel, int taskId, CausalityRelation relation)
		{
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x00004088 File Offset: 0x00002288
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, int taskId, CausalitySynchronousWork work)
		{
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00004088 File Offset: 0x00002288
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
		{
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000848C6 File Offset: 0x00082AC6
		private static ulong GetOperationId(uint taskId)
		{
			return (ulong)(((long)AppDomain.CurrentDomain.Id << 32) + (long)((ulong)taskId));
		}
	}
}
