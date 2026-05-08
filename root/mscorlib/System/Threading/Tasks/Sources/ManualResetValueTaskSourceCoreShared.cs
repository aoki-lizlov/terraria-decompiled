using System;
using System.Diagnostics;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200035D RID: 861
	internal static class ManualResetValueTaskSourceCoreShared
	{
		// Token: 0x06002524 RID: 9508 RVA: 0x00084CDD File Offset: 0x00082EDD
		[StackTraceHidden]
		internal static void ThrowInvalidOperationException()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x00084CE4 File Offset: 0x00082EE4
		private static void CompletionSentinel(object _)
		{
			ManualResetValueTaskSourceCoreShared.ThrowInvalidOperationException();
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x00084CEB File Offset: 0x00082EEB
		// Note: this type is marked as 'beforefieldinit'.
		static ManualResetValueTaskSourceCoreShared()
		{
		}

		// Token: 0x04001C55 RID: 7253
		internal static readonly Action<object> s_sentinel = new Action<object>(ManualResetValueTaskSourceCoreShared.CompletionSentinel);
	}
}
