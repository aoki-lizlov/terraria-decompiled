using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005DB RID: 1499
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncResult : IAsyncResult, IMessageSink, IThreadPoolWorkItem
	{
		// Token: 0x06003A15 RID: 14869 RVA: 0x000025BE File Offset: 0x000007BE
		internal AsyncResult()
		{
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000CCA34 File Offset: 0x000CAC34
		public virtual object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003A17 RID: 14871 RVA: 0x000CCA3C File Offset: 0x000CAC3C
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle waitHandle;
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
					waitHandle = this.handle;
				}
				return waitHandle;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003A18 RID: 14872 RVA: 0x000CCA94 File Offset: 0x000CAC94
		public virtual bool CompletedSynchronously
		{
			get
			{
				return this.sync_completed;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x000CCA9C File Offset: 0x000CAC9C
		public virtual bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003A1A RID: 14874 RVA: 0x000CCAA4 File Offset: 0x000CACA4
		// (set) Token: 0x06003A1B RID: 14875 RVA: 0x000CCAAC File Offset: 0x000CACAC
		public bool EndInvokeCalled
		{
			get
			{
				return this.endinvoke_called;
			}
			set
			{
				this.endinvoke_called = value;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000CCAB5 File Offset: 0x000CACB5
		public virtual object AsyncDelegate
		{
			get
			{
				return this.async_delegate;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003A1D RID: 14877 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000CCABD File Offset: 0x000CACBD
		public virtual IMessage GetReplyMessage()
		{
			return this.reply_message;
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000CCAC5 File Offset: 0x000CACC5
		public virtual void SetMessageCtrl(IMessageCtrl mc)
		{
			this.message_ctrl = mc;
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000CCACE File Offset: 0x000CACCE
		internal void SetCompletedSynchronously(bool completed)
		{
			this.sync_completed = completed;
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000CCAD8 File Offset: 0x000CACD8
		internal IMessage EndInvoke()
		{
			lock (this)
			{
				if (this.completed)
				{
					return this.reply_message;
				}
			}
			this.AsyncWaitHandle.WaitOne();
			return this.reply_message;
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000CCB34 File Offset: 0x000CAD34
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			this.reply_message = msg;
			lock (this)
			{
				this.completed = true;
				if (this.handle != null)
				{
					((ManualResetEvent)this.AsyncWaitHandle).Set();
				}
			}
			if (this.async_callback != null)
			{
				((AsyncCallback)this.async_callback)(this);
			}
			return null;
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x000CCBAC File Offset: 0x000CADAC
		// (set) Token: 0x06003A25 RID: 14885 RVA: 0x000CCBB4 File Offset: 0x000CADB4
		internal MonoMethodMessage CallMessage
		{
			get
			{
				return this.call_message;
			}
			set
			{
				this.call_message = value;
			}
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000CCBBD File Offset: 0x000CADBD
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.Invoke();
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x00004088 File Offset: 0x00002288
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06003A28 RID: 14888
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object Invoke();

		// Token: 0x040025E3 RID: 9699
		private object async_state;

		// Token: 0x040025E4 RID: 9700
		private WaitHandle handle;

		// Token: 0x040025E5 RID: 9701
		private object async_delegate;

		// Token: 0x040025E6 RID: 9702
		private IntPtr data;

		// Token: 0x040025E7 RID: 9703
		private object object_data;

		// Token: 0x040025E8 RID: 9704
		private bool sync_completed;

		// Token: 0x040025E9 RID: 9705
		private bool completed;

		// Token: 0x040025EA RID: 9706
		private bool endinvoke_called;

		// Token: 0x040025EB RID: 9707
		private object async_callback;

		// Token: 0x040025EC RID: 9708
		private ExecutionContext current;

		// Token: 0x040025ED RID: 9709
		private ExecutionContext original;

		// Token: 0x040025EE RID: 9710
		private long add_time;

		// Token: 0x040025EF RID: 9711
		private MonoMethodMessage call_message;

		// Token: 0x040025F0 RID: 9712
		private IMessageCtrl message_ctrl;

		// Token: 0x040025F1 RID: 9713
		private IMessage reply_message;

		// Token: 0x040025F2 RID: 9714
		private WaitCallback orig_cb;
	}
}
