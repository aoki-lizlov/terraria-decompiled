using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000298 RID: 664
	public struct AsyncFlowControl : IDisposable
	{
		// Token: 0x06001E99 RID: 7833 RVA: 0x00072D80 File Offset: 0x00070F80
		[SecurityCritical]
		internal void Setup()
		{
			this.useEC = true;
			Thread currentThread = Thread.CurrentThread;
			this._ec = currentThread.GetMutableExecutionContext();
			this._ec.isFlowSuppressed = true;
			this._thread = currentThread;
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x00072DB9 File Offset: 0x00070FB9
		public void Dispose()
		{
			this.Undo();
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00072DC4 File Offset: 0x00070FC4
		[SecuritySafeCritical]
		public void Undo()
		{
			if (this._thread == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl object can be used only once to call Undo()."));
			}
			if (this._thread != Thread.CurrentThread)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl object must be used on the thread where it was created."));
			}
			if (this.useEC)
			{
				if (Thread.CurrentThread.GetMutableExecutionContext() != this._ec)
				{
					throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl objects can be used to restore flow only on the Context that had its flow suppressed."));
				}
				ExecutionContext.RestoreFlow();
			}
			this._thread = null;
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x00072E3C File Offset: 0x0007103C
		public override int GetHashCode()
		{
			if (this._thread != null)
			{
				return this._thread.GetHashCode();
			}
			return this.ToString().GetHashCode();
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00072E63 File Offset: 0x00071063
		public override bool Equals(object obj)
		{
			return obj is AsyncFlowControl && this.Equals((AsyncFlowControl)obj);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00072E7B File Offset: 0x0007107B
		public bool Equals(AsyncFlowControl obj)
		{
			return obj.useEC == this.useEC && obj._ec == this._ec && obj._thread == this._thread;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00072EA9 File Offset: 0x000710A9
		public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00072EB3 File Offset: 0x000710B3
		public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
		{
			return !(a == b);
		}

		// Token: 0x040019BD RID: 6589
		private bool useEC;

		// Token: 0x040019BE RID: 6590
		private ExecutionContext _ec;

		// Token: 0x040019BF RID: 6591
		private Thread _thread;
	}
}
