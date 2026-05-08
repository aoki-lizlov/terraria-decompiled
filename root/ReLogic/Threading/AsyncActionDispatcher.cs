using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ReLogic.Threading
{
	// Token: 0x0200000B RID: 11
	public class AsyncActionDispatcher : IDisposable
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000038AD File Offset: 0x00001AAD
		public int ActionsRemaining
		{
			get
			{
				return this._actionQueue.Count;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000038BA File Offset: 0x00001ABA
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000038C2 File Offset: 0x00001AC2
		public bool IsDisposed
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDisposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsDisposed>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000038CB File Offset: 0x00001ACB
		public bool IsRunning
		{
			get
			{
				return this._isRunning;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000038D5 File Offset: 0x00001AD5
		public void Queue(Action action)
		{
			this._actionQueue.Add(action);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000038E4 File Offset: 0x00001AE4
		public void Start()
		{
			if (this.IsRunning)
			{
				throw new InvalidOperationException("AsyncActionDispatcher is already started.");
			}
			this._isRunning = true;
			this._actionThread = new Thread(new ThreadStart(this.LoaderThreadStart))
			{
				IsBackground = true,
				Name = "AsyncActionDispatcher Thread"
			};
			this._actionThread.Start();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003941 File Offset: 0x00001B41
		public void Stop()
		{
			if (!this.IsRunning)
			{
				throw new InvalidOperationException("AsyncActionDispatcher is already stopped.");
			}
			this._isRunning = false;
			this._threadCancellation.Cancel();
			this._actionThread.Join();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003978 File Offset: 0x00001B78
		[DebuggerNonUserCode]
		private void LoaderThreadStart()
		{
			while (this._isRunning)
			{
				try
				{
					this._actionQueue.Take(this._threadCancellation.Token).Invoke();
				}
				catch (OperationCanceledException)
				{
					break;
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000039C4 File Offset: 0x00001BC4
		protected virtual void Dispose(bool disposing)
		{
			if (this.IsDisposed)
			{
				return;
			}
			if (disposing)
			{
				if (this.IsRunning)
				{
					this.Stop();
				}
				this._actionQueue.Dispose();
				this._threadCancellation.Dispose();
			}
			this.IsDisposed = true;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000039FD File Offset: 0x00001BFD
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003A06 File Offset: 0x00001C06
		public AsyncActionDispatcher()
		{
		}

		// Token: 0x0400001A RID: 26
		private Thread _actionThread;

		// Token: 0x0400001B RID: 27
		private readonly BlockingCollection<Action> _actionQueue = new BlockingCollection<Action>();

		// Token: 0x0400001C RID: 28
		private readonly CancellationTokenSource _threadCancellation = new CancellationTokenSource();

		// Token: 0x0400001D RID: 29
		private volatile bool _isRunning;

		// Token: 0x0400001E RID: 30
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;
	}
}
