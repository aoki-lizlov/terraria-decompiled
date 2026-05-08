using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002E8 RID: 744
	internal readonly struct ForceAsyncAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x0600217D RID: 8573 RVA: 0x000792E5 File Offset: 0x000774E5
		internal ForceAsyncAwaiter(Task task)
		{
			this._task = task;
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000792EE File Offset: 0x000774EE
		public ForceAsyncAwaiter GetAwaiter()
		{
			return this;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsCompleted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000792F8 File Offset: 0x000774F8
		public void GetResult()
		{
			this._task.GetAwaiter().GetResult();
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00079318 File Offset: 0x00077518
		public void OnCompleted(Action action)
		{
			this._task.ConfigureAwait(false).GetAwaiter().OnCompleted(action);
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00079344 File Offset: 0x00077544
		public void UnsafeOnCompleted(Action action)
		{
			this._task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(action);
		}

		// Token: 0x04001AAC RID: 6828
		private readonly Task _task;
	}
}
