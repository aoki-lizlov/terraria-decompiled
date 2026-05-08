using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000297 RID: 663
	internal struct ExecutionContextSwitcher
	{
		// Token: 0x06001E97 RID: 7831 RVA: 0x00072CFC File Offset: 0x00070EFC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			try
			{
				this.Undo();
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x00072D2C File Offset: 0x00070F2C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void Undo()
		{
			if (this.thread == null)
			{
				return;
			}
			Thread thread = this.thread;
			ExecutionContext.Reader executionContextReader = thread.GetExecutionContextReader();
			thread.SetExecutionContext(this.outerEC, this.outerECBelongsToScope);
			this.thread = null;
			ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), this.outerEC.DangerousGetRawExecutionContext());
		}

		// Token: 0x040019B9 RID: 6585
		internal ExecutionContext.Reader outerEC;

		// Token: 0x040019BA RID: 6586
		internal bool outerECBelongsToScope;

		// Token: 0x040019BB RID: 6587
		internal object hecsw;

		// Token: 0x040019BC RID: 6588
		internal Thread thread;
	}
}
