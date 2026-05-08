using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000137 RID: 311
	public class Progress<T> : IProgress<T>
	{
		// Token: 0x06000CBF RID: 3263 RVA: 0x00033529 File Offset: 0x00031729
		public Progress()
		{
			this._synchronizationContext = SynchronizationContext.Current ?? ProgressStatics.DefaultContext;
			this._invokeHandlers = new SendOrPostCallback(this.InvokeHandlers);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00033557 File Offset: 0x00031757
		public Progress(Action<T> handler)
			: this()
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this._handler = handler;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000CC1 RID: 3265 RVA: 0x00033574 File Offset: 0x00031774
		// (remove) Token: 0x06000CC2 RID: 3266 RVA: 0x000335AC File Offset: 0x000317AC
		public event EventHandler<T> ProgressChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<T> eventHandler = this.ProgressChanged;
				EventHandler<T> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<T> eventHandler3 = (EventHandler<T>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<T>>(ref this.ProgressChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<T> eventHandler = this.ProgressChanged;
				EventHandler<T> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<T> eventHandler3 = (EventHandler<T>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<T>>(ref this.ProgressChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000335E4 File Offset: 0x000317E4
		protected virtual void OnReport(T value)
		{
			bool handler = this._handler != null;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler || progressChanged != null)
			{
				this._synchronizationContext.Post(this._invokeHandlers, value);
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0003361A File Offset: 0x0003181A
		void IProgress<T>.Report(T value)
		{
			this.OnReport(value);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00033624 File Offset: 0x00031824
		private void InvokeHandlers(object state)
		{
			T t = (T)((object)state);
			Action<T> handler = this._handler;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler != null)
			{
				handler(t);
			}
			if (progressChanged != null)
			{
				progressChanged(this, t);
			}
		}

		// Token: 0x04001143 RID: 4419
		private readonly SynchronizationContext _synchronizationContext;

		// Token: 0x04001144 RID: 4420
		private readonly Action<T> _handler;

		// Token: 0x04001145 RID: 4421
		private readonly SendOrPostCallback _invokeHandlers;

		// Token: 0x04001146 RID: 4422
		[CompilerGenerated]
		private EventHandler<T> ProgressChanged;
	}
}
