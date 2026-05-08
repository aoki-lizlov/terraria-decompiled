using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ReLogic.OS.Windows
{
	// Token: 0x02000068 RID: 104
	internal class STATaskInvoker : IDisposable
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00009B94 File Offset: 0x00007D94
		private STATaskInvoker()
		{
			this._shouldThreadContinue = true;
			this._staThread = new Thread(new ThreadStart(this.TaskThreadStart));
			this._staThread.Name = "STA Invoker Thread";
			this._staThread.SetApartmentState(0);
			this._staThread.Start();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009C0F File Offset: 0x00007E0F
		public static void Invoke(Action action)
		{
			if (STATaskInvoker.Instance == null)
			{
				STATaskInvoker.Instance = new STATaskInvoker();
			}
			STATaskInvoker.Instance.InvokeAndWait(action);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009C30 File Offset: 0x00007E30
		public static T Invoke<T>(Func<T> action)
		{
			if (STATaskInvoker.Instance == null)
			{
				STATaskInvoker.Instance = new STATaskInvoker();
			}
			T output = default(T);
			STATaskInvoker.Instance.InvokeAndWait(delegate
			{
				output = action.Invoke();
			});
			return output;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009C84 File Offset: 0x00007E84
		private void InvokeAndWait(Action action)
		{
			object taskInvokeLock = this._taskInvokeLock;
			lock (taskInvokeLock)
			{
				object taskCompletionLock = this._taskCompletionLock;
				lock (taskCompletionLock)
				{
					this._staTasks.Add(action);
					Monitor.Wait(this._taskCompletionLock);
				}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009D00 File Offset: 0x00007F00
		private void TaskThreadStart()
		{
			while (this._shouldThreadContinue)
			{
				Action action = this._staTasks.Take();
				object taskCompletionLock = this._taskCompletionLock;
				lock (taskCompletionLock)
				{
					action.Invoke();
					Monitor.Pulse(this._taskCompletionLock);
				}
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009D64 File Offset: 0x00007F64
		private void Shutdown()
		{
			this.InvokeAndWait(delegate
			{
				this._shouldThreadContinue = false;
			});
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009D78 File Offset: 0x00007F78
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					this.Shutdown();
					if (this._staTasks != null)
					{
						this._staTasks.Dispose();
						this._staTasks = null;
					}
				}
				this.disposedValue = true;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009DAC File Offset: 0x00007FAC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009DB5 File Offset: 0x00007FB5
		[CompilerGenerated]
		private void <Shutdown>b__11_0()
		{
			this._shouldThreadContinue = false;
		}

		// Token: 0x04000306 RID: 774
		private static STATaskInvoker Instance;

		// Token: 0x04000307 RID: 775
		private Thread _staThread;

		// Token: 0x04000308 RID: 776
		private volatile bool _shouldThreadContinue;

		// Token: 0x04000309 RID: 777
		private BlockingCollection<Action> _staTasks = new BlockingCollection<Action>();

		// Token: 0x0400030A RID: 778
		private object _taskInvokeLock = new object();

		// Token: 0x0400030B RID: 779
		private object _taskCompletionLock = new object();

		// Token: 0x0400030C RID: 780
		private bool disposedValue;

		// Token: 0x020000D3 RID: 211
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0<T>
		{
			// Token: 0x0600045E RID: 1118 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x0600045F RID: 1119 RVA: 0x0000E4DA File Offset: 0x0000C6DA
			internal void <Invoke>b__0()
			{
				this.output = this.action.Invoke();
			}

			// Token: 0x040005C8 RID: 1480
			public T output;

			// Token: 0x040005C9 RID: 1481
			public Func<T> action;
		}
	}
}
