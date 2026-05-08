using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200009D RID: 157
	public abstract class GraphicsResource : IDisposable
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0002D74F File Offset: 0x0002B94F
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x0002D758 File Offset: 0x0002B958
		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return this.graphicsDevice;
			}
			internal set
			{
				if (this.graphicsDevice == value)
				{
					return;
				}
				if (this.graphicsDevice != null && this.selfReference.IsAllocated && this.graphicsDevice.RemoveResourceReference(this.selfReference))
				{
					this.selfReference.Free();
				}
				this.graphicsDevice = value;
				this.selfReference = GCHandle.Alloc(this, GCHandleType.Weak);
				this.graphicsDevice.AddResourceReference(this.selfReference);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0002D7C7 File Offset: 0x0002B9C7
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x0002D7CF File Offset: 0x0002B9CF
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

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0002D7D8 File Offset: 0x0002B9D8
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x0002D7E0 File Offset: 0x0002B9E0
		public virtual string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0002D7E9 File Offset: 0x0002B9E9
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x0002D7F1 File Offset: 0x0002B9F1
		public object Tag
		{
			[CompilerGenerated]
			get
			{
				return this.<Tag>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tag>k__BackingField = value;
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600138A RID: 5002 RVA: 0x0002D7FC File Offset: 0x0002B9FC
		// (remove) Token: 0x0600138B RID: 5003 RVA: 0x0002D834 File Offset: 0x0002BA34
		public event EventHandler<EventArgs> Disposing
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.Disposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.Disposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000136F5 File Offset: 0x000118F5
		internal GraphicsResource()
		{
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0002D86C File Offset: 0x0002BA6C
		~GraphicsResource()
		{
			if (!this.IsDisposed && this.graphicsDevice != null && !this.graphicsDevice.IsDisposed)
			{
				this.Dispose(false);
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0002D8B8 File Offset: 0x0002BAB8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0002D8C7 File Offset: 0x0002BAC7
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return base.ToString();
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00009E6B File Offset: 0x0000806B
		protected internal virtual void GraphicsDeviceResetting()
		{
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x000136EB File Offset: 0x000118EB
		protected internal virtual bool IsHarmlessToLeakInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				if (disposing && this.Disposing != null)
				{
					this.Disposing(this, EventArgs.Empty);
				}
				if (this.graphicsDevice != null && this.selfReference.IsAllocated && this.graphicsDevice.RemoveResourceReference(this.selfReference))
				{
					this.selfReference.Free();
				}
				this.IsDisposed = true;
			}
		}

		// Token: 0x040008F4 RID: 2292
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x040008F5 RID: 2293
		protected string _Name;

		// Token: 0x040008F6 RID: 2294
		[CompilerGenerated]
		private object <Tag>k__BackingField;

		// Token: 0x040008F7 RID: 2295
		private GCHandle selfReference;

		// Token: 0x040008F8 RID: 2296
		private GraphicsDevice graphicsDevice;

		// Token: 0x040008F9 RID: 2297
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposing;
	}
}
