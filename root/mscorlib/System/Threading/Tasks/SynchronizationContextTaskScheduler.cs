using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200034B RID: 843
	internal sealed class SynchronizationContextTaskScheduler : TaskScheduler
	{
		// Token: 0x060024E0 RID: 9440 RVA: 0x00084570 File Offset: 0x00082770
		internal SynchronizationContextTaskScheduler()
		{
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext == null)
			{
				throw new InvalidOperationException("The current SynchronizationContext may not be used as a TaskScheduler.");
			}
			this.m_synchronizationContext = synchronizationContext;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0008459E File Offset: 0x0008279E
		protected internal override void QueueTask(Task task)
		{
			this.m_synchronizationContext.Post(SynchronizationContextTaskScheduler.s_postCallback, task);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000845B1 File Offset: 0x000827B1
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return SynchronizationContext.Current == this.m_synchronizationContext && base.TryExecuteTask(task);
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return null;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override int MaximumConcurrencyLevel
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000845C9 File Offset: 0x000827C9
		// Note: this type is marked as 'beforefieldinit'.
		static SynchronizationContextTaskScheduler()
		{
		}

		// Token: 0x04001C1F RID: 7199
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x04001C20 RID: 7200
		private static readonly SendOrPostCallback s_postCallback = delegate(object s)
		{
			((Task)s).ExecuteEntry(true);
		};

		// Token: 0x0200034C RID: 844
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060024E6 RID: 9446 RVA: 0x000845E0 File Offset: 0x000827E0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060024E7 RID: 9447 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x060024E8 RID: 9448 RVA: 0x000845EC File Offset: 0x000827EC
			internal void <.cctor>b__8_0(object s)
			{
				((Task)s).ExecuteEntry(true);
			}

			// Token: 0x04001C21 RID: 7201
			public static readonly SynchronizationContextTaskScheduler.<>c <>9 = new SynchronizationContextTaskScheduler.<>c();
		}
	}
}
