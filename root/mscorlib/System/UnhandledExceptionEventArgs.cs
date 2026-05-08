using System;

namespace System
{
	// Token: 0x02000181 RID: 385
	[Serializable]
	public class UnhandledExceptionEventArgs : EventArgs
	{
		// Token: 0x06001250 RID: 4688 RVA: 0x00049025 File Offset: 0x00047225
		public UnhandledExceptionEventArgs(object exception, bool isTerminating)
		{
			this._exception = exception;
			this._isTerminating = isTerminating;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0004903B File Offset: 0x0004723B
		public object ExceptionObject
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00049043 File Offset: 0x00047243
		public bool IsTerminating
		{
			get
			{
				return this._isTerminating;
			}
		}

		// Token: 0x0400124C RID: 4684
		private object _exception;

		// Token: 0x0400124D RID: 4685
		private bool _isTerminating;
	}
}
