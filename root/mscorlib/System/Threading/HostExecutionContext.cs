using System;

namespace System.Threading
{
	// Token: 0x020002BC RID: 700
	[MonoTODO("Useless until the runtime supports it")]
	public class HostExecutionContext : IDisposable
	{
		// Token: 0x06002058 RID: 8280 RVA: 0x00076AD5 File Offset: 0x00074CD5
		public HostExecutionContext()
		{
			this._state = null;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x00076AE4 File Offset: 0x00074CE4
		public HostExecutionContext(object state)
		{
			this._state = state;
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00076AF3 File Offset: 0x00074CF3
		public virtual HostExecutionContext CreateCopy()
		{
			return new HostExecutionContext(this._state);
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00076B00 File Offset: 0x00074D00
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x00076B08 File Offset: 0x00074D08
		protected internal object State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00076B11 File Offset: 0x00074D11
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x04001A37 RID: 6711
		private object _state;
	}
}
