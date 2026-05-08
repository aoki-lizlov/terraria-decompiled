using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002B2 RID: 690
	internal sealed class QueueUserWorkItemCallback : IThreadPoolWorkItem
	{
		// Token: 0x06001FDA RID: 8154 RVA: 0x00075747 File Offset: 0x00073947
		[SecurityCritical]
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, bool compressStack, ref StackCrawlMark stackMark)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this.context = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00075775 File Offset: 0x00073975
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, ExecutionContext ec)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			this.context = ec;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x00075792 File Offset: 0x00073992
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.context == null)
			{
				WaitCallback waitCallback = this.callback;
				this.callback = null;
				waitCallback(this.state);
				return;
			}
			ExecutionContext.Run(this.context, QueueUserWorkItemCallback.ccb, this, true);
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x00004088 File Offset: 0x00002288
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x000757C8 File Offset: 0x000739C8
		[SecurityCritical]
		private static void WaitCallback_Context(object state)
		{
			QueueUserWorkItemCallback queueUserWorkItemCallback = (QueueUserWorkItemCallback)state;
			queueUserWorkItemCallback.callback(queueUserWorkItemCallback.state);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000757ED File Offset: 0x000739ED
		// Note: this type is marked as 'beforefieldinit'.
		static QueueUserWorkItemCallback()
		{
		}

		// Token: 0x04001A0E RID: 6670
		private WaitCallback callback;

		// Token: 0x04001A0F RID: 6671
		private ExecutionContext context;

		// Token: 0x04001A10 RID: 6672
		private object state;

		// Token: 0x04001A11 RID: 6673
		[SecurityCritical]
		internal static ContextCallback ccb = new ContextCallback(QueueUserWorkItemCallback.WaitCallback_Context);
	}
}
