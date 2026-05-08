using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000255 RID: 597
	[Serializable]
	public class AbandonedMutexException : SystemException
	{
		// Token: 0x06001D2C RID: 7468 RVA: 0x0006E4C3 File Offset: 0x0006C6C3
		public AbandonedMutexException()
			: base("The wait completed due to an abandoned mutex.")
		{
			base.HResult = -2146233043;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0006E4E2 File Offset: 0x0006C6E2
		public AbandonedMutexException(string message)
			: base(message)
		{
			base.HResult = -2146233043;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0006E4FD File Offset: 0x0006C6FD
		public AbandonedMutexException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233043;
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0006E519 File Offset: 0x0006C719
		public AbandonedMutexException(int location, WaitHandle handle)
			: base("The wait completed due to an abandoned mutex.")
		{
			base.HResult = -2146233043;
			this.SetupException(location, handle);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0006E540 File Offset: 0x0006C740
		public AbandonedMutexException(string message, int location, WaitHandle handle)
			: base(message)
		{
			base.HResult = -2146233043;
			this.SetupException(location, handle);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0006E563 File Offset: 0x0006C763
		public AbandonedMutexException(string message, Exception inner, int location, WaitHandle handle)
			: base(message, inner)
		{
			base.HResult = -2146233043;
			this.SetupException(location, handle);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0006E588 File Offset: 0x0006C788
		protected AbandonedMutexException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0006E599 File Offset: 0x0006C799
		private void SetupException(int location, WaitHandle handle)
		{
			this._mutexIndex = location;
			if (handle != null)
			{
				this._mutex = handle as Mutex;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0006E5B1 File Offset: 0x0006C7B1
		public Mutex Mutex
		{
			get
			{
				return this._mutex;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0006E5B9 File Offset: 0x0006C7B9
		public int MutexIndex
		{
			get
			{
				return this._mutexIndex;
			}
		}

		// Token: 0x04001903 RID: 6403
		private int _mutexIndex = -1;

		// Token: 0x04001904 RID: 6404
		private Mutex _mutex;
	}
}
