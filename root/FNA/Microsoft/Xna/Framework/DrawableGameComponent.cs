using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000016 RID: 22
	public class DrawableGameComponent : GameComponent, IDrawable
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00009C79 File Offset: 0x00007E79
		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return base.Game.GraphicsDevice;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00009C86 File Offset: 0x00007E86
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00009C8E File Offset: 0x00007E8E
		public int DrawOrder
		{
			get
			{
				return this._drawOrder;
			}
			set
			{
				if (this._drawOrder != value)
				{
					this._drawOrder = value;
					if (this.DrawOrderChanged != null)
					{
						this.DrawOrderChanged(this, null);
					}
					this.OnDrawOrderChanged(this, null);
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00009CBD File Offset: 0x00007EBD
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x00009CC5 File Offset: 0x00007EC5
		public bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				if (this._visible != value)
				{
					this._visible = value;
					if (this.VisibleChanged != null)
					{
						this.VisibleChanged(this, EventArgs.Empty);
					}
					this.OnVisibleChanged(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00009CFC File Offset: 0x00007EFC
		public DrawableGameComponent(Game game)
			: base(game)
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000AA7 RID: 2727 RVA: 0x00009D0C File Offset: 0x00007F0C
		// (remove) Token: 0x06000AA8 RID: 2728 RVA: 0x00009D44 File Offset: 0x00007F44
		public event EventHandler<EventArgs> DrawOrderChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.DrawOrderChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DrawOrderChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.DrawOrderChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DrawOrderChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000AA9 RID: 2729 RVA: 0x00009D7C File Offset: 0x00007F7C
		// (remove) Token: 0x06000AAA RID: 2730 RVA: 0x00009DB4 File Offset: 0x00007FB4
		public event EventHandler<EventArgs> VisibleChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.VisibleChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.VisibleChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.VisibleChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.VisibleChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00009DEC File Offset: 0x00007FEC
		public override void Initialize()
		{
			if (!this._initialized)
			{
				this._initialized = true;
				IGraphicsDeviceService graphicsDeviceService = (IGraphicsDeviceService)base.Game.Services.GetService(typeof(IGraphicsDeviceService));
				if (graphicsDeviceService != null)
				{
					if (graphicsDeviceService.GraphicsDevice != null)
					{
						this.LoadContent();
						return;
					}
					graphicsDeviceService.DeviceCreated += this.OnDeviceCreated;
				}
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00009E4C File Offset: 0x0000804C
		protected override void Dispose(bool disposing)
		{
			if (this._initialized)
			{
				this.UnloadContent();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00009E63 File Offset: 0x00008063
		private void OnDeviceCreated(object sender, EventArgs e)
		{
			this.LoadContent();
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00009E6B File Offset: 0x0000806B
		public virtual void Draw(GameTime gameTime)
		{
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void LoadContent()
		{
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void UnloadContent()
		{
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void OnVisibleChanged(object sender, EventArgs args)
		{
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void OnDrawOrderChanged(object sender, EventArgs args)
		{
		}

		// Token: 0x040004C1 RID: 1217
		private bool _initialized;

		// Token: 0x040004C2 RID: 1218
		private int _drawOrder;

		// Token: 0x040004C3 RID: 1219
		private bool _visible = true;

		// Token: 0x040004C4 RID: 1220
		[CompilerGenerated]
		private EventHandler<EventArgs> DrawOrderChanged;

		// Token: 0x040004C5 RID: 1221
		[CompilerGenerated]
		private EventHandler<EventArgs> VisibleChanged;
	}
}
