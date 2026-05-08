using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200034D RID: 845
	public class UnobservedTaskExceptionEventArgs : EventArgs
	{
		// Token: 0x060024E9 RID: 9449 RVA: 0x000845FB File Offset: 0x000827FB
		public UnobservedTaskExceptionEventArgs(AggregateException exception)
		{
			this.m_exception = exception;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0008460A File Offset: 0x0008280A
		public void SetObserved()
		{
			this.m_observed = true;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x00084613 File Offset: 0x00082813
		public bool Observed
		{
			get
			{
				return this.m_observed;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x0008461B File Offset: 0x0008281B
		public AggregateException Exception
		{
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x04001C22 RID: 7202
		private AggregateException m_exception;

		// Token: 0x04001C23 RID: 7203
		internal bool m_observed;
	}
}
