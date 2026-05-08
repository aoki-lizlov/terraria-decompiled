using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200033F RID: 831
	internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x0600243A RID: 9274 RVA: 0x00082DB3 File Offset: 0x00080FB3
		internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext)
			: base(action, flowExecutionContext)
		{
			this.m_syncContext = context;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00082DC4 File Offset: 0x00080FC4
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.Current)
			{
				base.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			base.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), this, ref Task.t_currentTask);
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00082E00 File Offset: 0x00081000
		private static void PostAction(object state)
		{
			SynchronizationContextAwaitTaskContinuation synchronizationContextAwaitTaskContinuation = (SynchronizationContextAwaitTaskContinuation)state;
			synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, synchronizationContextAwaitTaskContinuation.m_action);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00082E2C File Offset: 0x0008102C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ContextCallback GetPostActionCallback()
		{
			ContextCallback contextCallback = SynchronizationContextAwaitTaskContinuation.s_postActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (SynchronizationContextAwaitTaskContinuation.s_postActionCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction));
			}
			return contextCallback;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00082E56 File Offset: 0x00081056
		// Note: this type is marked as 'beforefieldinit'.
		static SynchronizationContextAwaitTaskContinuation()
		{
		}

		// Token: 0x04001C00 RID: 7168
		private static readonly SendOrPostCallback s_postCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04001C01 RID: 7169
		private static ContextCallback s_postActionCallback;

		// Token: 0x04001C02 RID: 7170
		private readonly SynchronizationContext m_syncContext;

		// Token: 0x02000340 RID: 832
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600243F RID: 9279 RVA: 0x00082E6D File Offset: 0x0008106D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002440 RID: 9280 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002441 RID: 9281 RVA: 0x0006F828 File Offset: 0x0006DA28
			internal void <.cctor>b__7_0(object state)
			{
				((Action)state)();
			}

			// Token: 0x04001C03 RID: 7171
			public static readonly SynchronizationContextAwaitTaskContinuation.<>c <>9 = new SynchronizationContextAwaitTaskContinuation.<>c();
		}
	}
}
