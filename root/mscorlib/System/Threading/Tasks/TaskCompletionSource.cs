using System;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x020002DA RID: 730
	public class TaskCompletionSource<TResult>
	{
		// Token: 0x06002126 RID: 8486 RVA: 0x00078639 File Offset: 0x00076839
		public TaskCompletionSource()
		{
			this._task = new Task<TResult>();
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0007864C File Offset: 0x0007684C
		public TaskCompletionSource(TaskCreationOptions creationOptions)
			: this(null, creationOptions)
		{
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00078656 File Offset: 0x00076856
		public TaskCompletionSource(object state)
			: this(state, TaskCreationOptions.None)
		{
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00078660 File Offset: 0x00076860
		public TaskCompletionSource(object state, TaskCreationOptions creationOptions)
		{
			this._task = new Task<TResult>(state, creationOptions);
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x00078675 File Offset: 0x00076875
		public Task<TResult> Task
		{
			get
			{
				return this._task;
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00078680 File Offset: 0x00076880
		private void SpinUntilCompleted()
		{
			SpinWait spinWait = default(SpinWait);
			while (!this._task.IsCompleted)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000786AB File Offset: 0x000768AB
		public bool TrySetException(Exception exception)
		{
			if (exception == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exception);
			}
			bool flag = this._task.TrySetException(exception);
			if (!flag && !this._task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000786DC File Offset: 0x000768DC
		public bool TrySetException(IEnumerable<Exception> exceptions)
		{
			if (exceptions == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exceptions);
			}
			List<Exception> list = new List<Exception>();
			foreach (Exception ex in exceptions)
			{
				if (ex == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.TaskCompletionSourceT_TrySetException_NullException, ExceptionArgument.exceptions);
				}
				list.Add(ex);
			}
			if (list.Count == 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.TaskCompletionSourceT_TrySetException_NoExceptions, ExceptionArgument.exceptions);
			}
			bool flag = this._task.TrySetException(list);
			if (!flag && !this._task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00078774 File Offset: 0x00076974
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.exception);
			}
			if (!this.TrySetException(exception))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00078790 File Offset: 0x00076990
		public void SetException(IEnumerable<Exception> exceptions)
		{
			if (!this.TrySetException(exceptions))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000787A2 File Offset: 0x000769A2
		public bool TrySetResult(TResult result)
		{
			bool flag = this._task.TrySetResult(result);
			if (!flag)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000787B9 File Offset: 0x000769B9
		public void SetResult(TResult result)
		{
			if (!this.TrySetResult(result))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000787CC File Offset: 0x000769CC
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(default(CancellationToken));
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000787E8 File Offset: 0x000769E8
		public bool TrySetCanceled(CancellationToken cancellationToken)
		{
			bool flag = this._task.TrySetCanceled(cancellationToken);
			if (!flag && !this._task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0007880C File Offset: 0x00076A0C
		public void SetCanceled()
		{
			if (!this.TrySetCanceled())
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted);
			}
		}

		// Token: 0x04001A93 RID: 6803
		private readonly Task<TResult> _task;
	}
}
