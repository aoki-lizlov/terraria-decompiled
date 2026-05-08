using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A29 RID: 2601
	public sealed class ContractFailedEventArgs : EventArgs
	{
		// Token: 0x06006033 RID: 24627 RVA: 0x0014D130 File Offset: 0x0014B330
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public ContractFailedEventArgs(ContractFailureKind failureKind, string message, string condition, Exception originalException)
		{
			this._failureKind = failureKind;
			this._message = message;
			this._condition = condition;
			this._originalException = originalException;
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06006034 RID: 24628 RVA: 0x0014D155 File Offset: 0x0014B355
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06006035 RID: 24629 RVA: 0x0014D15D File Offset: 0x0014B35D
		public string Condition
		{
			get
			{
				return this._condition;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06006036 RID: 24630 RVA: 0x0014D165 File Offset: 0x0014B365
		public ContractFailureKind FailureKind
		{
			get
			{
				return this._failureKind;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06006037 RID: 24631 RVA: 0x0014D16D File Offset: 0x0014B36D
		public Exception OriginalException
		{
			get
			{
				return this._originalException;
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06006038 RID: 24632 RVA: 0x0014D175 File Offset: 0x0014B375
		public bool Handled
		{
			get
			{
				return this._handled;
			}
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x0014D17D File Offset: 0x0014B37D
		[SecurityCritical]
		public void SetHandled()
		{
			this._handled = true;
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x0014D186 File Offset: 0x0014B386
		public bool Unwind
		{
			get
			{
				return this._unwind;
			}
		}

		// Token: 0x0600603B RID: 24635 RVA: 0x0014D18E File Offset: 0x0014B38E
		[SecurityCritical]
		public void SetUnwind()
		{
			this._unwind = true;
		}

		// Token: 0x040039E0 RID: 14816
		private ContractFailureKind _failureKind;

		// Token: 0x040039E1 RID: 14817
		private string _message;

		// Token: 0x040039E2 RID: 14818
		private string _condition;

		// Token: 0x040039E3 RID: 14819
		private Exception _originalException;

		// Token: 0x040039E4 RID: 14820
		private bool _handled;

		// Token: 0x040039E5 RID: 14821
		private bool _unwind;

		// Token: 0x040039E6 RID: 14822
		internal Exception thrownDuringHandler;
	}
}
