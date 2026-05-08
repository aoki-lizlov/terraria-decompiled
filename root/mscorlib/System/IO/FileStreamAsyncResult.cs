using System;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000981 RID: 2433
	internal class FileStreamAsyncResult : IAsyncResult
	{
		// Token: 0x0600588C RID: 22668 RVA: 0x0012BEB7 File Offset: 0x0012A0B7
		public FileStreamAsyncResult(AsyncCallback cb, object state)
		{
			this.state = state;
			this.realcb = cb;
			if (this.realcb != null)
			{
				this.cb = new AsyncCallback(FileStreamAsyncResult.CBWrapper);
			}
			this.wh = new ManualResetEvent(false);
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x0012BEF3 File Offset: 0x0012A0F3
		private static void CBWrapper(IAsyncResult ares)
		{
			((FileStreamAsyncResult)ares).realcb.BeginInvoke(ares, null, null);
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x0012BF09 File Offset: 0x0012A109
		public void SetComplete(Exception e)
		{
			this.exc = e;
			this.completed = true;
			this.wh.Set();
			if (this.cb != null)
			{
				this.cb(this);
			}
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x0012BF39 File Offset: 0x0012A139
		public void SetComplete(Exception e, int nbytes)
		{
			this.BytesRead = nbytes;
			this.SetComplete(e);
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x0012BF49 File Offset: 0x0012A149
		public void SetComplete(Exception e, int nbytes, bool synch)
		{
			this.completedSynch = synch;
			this.SetComplete(e, nbytes);
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06005891 RID: 22673 RVA: 0x0012BF5A File Offset: 0x0012A15A
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06005892 RID: 22674 RVA: 0x0012BF62 File Offset: 0x0012A162
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynch;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06005893 RID: 22675 RVA: 0x0012BF6A File Offset: 0x0012A16A
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.wh;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06005894 RID: 22676 RVA: 0x0012BF72 File Offset: 0x0012A172
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06005895 RID: 22677 RVA: 0x0012BF7A File Offset: 0x0012A17A
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06005896 RID: 22678 RVA: 0x0012BF82 File Offset: 0x0012A182
		// (set) Token: 0x06005897 RID: 22679 RVA: 0x0012BF8A File Offset: 0x0012A18A
		public bool Done
		{
			get
			{
				return this.done;
			}
			set
			{
				this.done = value;
			}
		}

		// Token: 0x04003523 RID: 13603
		private object state;

		// Token: 0x04003524 RID: 13604
		private bool completed;

		// Token: 0x04003525 RID: 13605
		private bool done;

		// Token: 0x04003526 RID: 13606
		private Exception exc;

		// Token: 0x04003527 RID: 13607
		private ManualResetEvent wh;

		// Token: 0x04003528 RID: 13608
		private AsyncCallback cb;

		// Token: 0x04003529 RID: 13609
		private bool completedSynch;

		// Token: 0x0400352A RID: 13610
		public byte[] Buffer;

		// Token: 0x0400352B RID: 13611
		public int Offset;

		// Token: 0x0400352C RID: 13612
		public int Count;

		// Token: 0x0400352D RID: 13613
		public int OriginalCount;

		// Token: 0x0400352E RID: 13614
		public int BytesRead;

		// Token: 0x0400352F RID: 13615
		private AsyncCallback realcb;
	}
}
