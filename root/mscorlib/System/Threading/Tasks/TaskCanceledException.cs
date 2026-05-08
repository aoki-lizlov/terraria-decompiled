using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	// Token: 0x020002D9 RID: 729
	[Serializable]
	public class TaskCanceledException : OperationCanceledException
	{
		// Token: 0x0600211F RID: 8479 RVA: 0x000785C6 File Offset: 0x000767C6
		public TaskCanceledException()
			: base("A task was canceled.")
		{
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000785D3 File Offset: 0x000767D3
		public TaskCanceledException(string message)
			: base(message)
		{
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000785DC File Offset: 0x000767DC
		public TaskCanceledException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000785E6 File Offset: 0x000767E6
		public TaskCanceledException(string message, Exception innerException, CancellationToken token)
			: base(message, innerException, token)
		{
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000785F4 File Offset: 0x000767F4
		public TaskCanceledException(Task task)
			: base("A task was canceled.", (task != null) ? task.CancellationToken : default(CancellationToken))
		{
			this._canceledTask = task;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00078627 File Offset: 0x00076827
		protected TaskCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x00078631 File Offset: 0x00076831
		public Task Task
		{
			get
			{
				return this._canceledTask;
			}
		}

		// Token: 0x04001A92 RID: 6802
		[NonSerialized]
		private readonly Task _canceledTask;
	}
}
