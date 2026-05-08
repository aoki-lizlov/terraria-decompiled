using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001E RID: 30
	public class GameComponent : IGameComponent, IUpdateable, IComparable<GameComponent>, IDisposable
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0001314D File Offset: 0x0001134D
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00013155 File Offset: 0x00011355
		public Game Game
		{
			[CompilerGenerated]
			get
			{
				return this.<Game>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Game>k__BackingField = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0001315E File Offset: 0x0001135E
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00013166 File Offset: 0x00011366
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (this._enabled != value)
				{
					this._enabled = value;
					if (this.EnabledChanged != null)
					{
						this.EnabledChanged(this, EventArgs.Empty);
					}
					this.OnEnabledChanged(this, null);
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00013199 File Offset: 0x00011399
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x000131A1 File Offset: 0x000113A1
		public int UpdateOrder
		{
			get
			{
				return this._updateOrder;
			}
			set
			{
				if (this._updateOrder != value)
				{
					this._updateOrder = value;
					if (this.UpdateOrderChanged != null)
					{
						this.UpdateOrderChanged(this, EventArgs.Empty);
					}
					this.OnUpdateOrderChanged(this, null);
				}
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000BAC RID: 2988 RVA: 0x000131D4 File Offset: 0x000113D4
		// (remove) Token: 0x06000BAD RID: 2989 RVA: 0x0001320C File Offset: 0x0001140C
		public event EventHandler<EventArgs> Disposed
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.Disposed;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposed, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.Disposed;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposed, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000BAE RID: 2990 RVA: 0x00013244 File Offset: 0x00011444
		// (remove) Token: 0x06000BAF RID: 2991 RVA: 0x0001327C File Offset: 0x0001147C
		public event EventHandler<EventArgs> EnabledChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.EnabledChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.EnabledChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.EnabledChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.EnabledChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000BB0 RID: 2992 RVA: 0x000132B4 File Offset: 0x000114B4
		// (remove) Token: 0x06000BB1 RID: 2993 RVA: 0x000132EC File Offset: 0x000114EC
		public event EventHandler<EventArgs> UpdateOrderChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.UpdateOrderChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.UpdateOrderChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.UpdateOrderChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.UpdateOrderChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00013321 File Offset: 0x00011521
		public GameComponent(Game game)
		{
			this.Game = game;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00013338 File Offset: 0x00011538
		~GameComponent()
		{
			this.Dispose(false);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00013368 File Offset: 0x00011568
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00009E6B File Offset: 0x0000806B
		public virtual void Initialize()
		{
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00009E6B File Offset: 0x0000806B
		public virtual void Update(GameTime gameTime)
		{
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void OnUpdateOrderChanged(object sender, EventArgs args)
		{
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void OnEnabledChanged(object sender, EventArgs args)
		{
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00013377 File Offset: 0x00011577
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.Disposed != null)
			{
				this.Disposed(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00013395 File Offset: 0x00011595
		int IComparable<GameComponent>.CompareTo(GameComponent other)
		{
			return other.UpdateOrder - this.UpdateOrder;
		}

		// Token: 0x04000559 RID: 1369
		[CompilerGenerated]
		private Game <Game>k__BackingField;

		// Token: 0x0400055A RID: 1370
		private bool _enabled = true;

		// Token: 0x0400055B RID: 1371
		private int _updateOrder;

		// Token: 0x0400055C RID: 1372
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposed;

		// Token: 0x0400055D RID: 1373
		[CompilerGenerated]
		private EventHandler<EventArgs> EnabledChanged;

		// Token: 0x0400055E RID: 1374
		[CompilerGenerated]
		private EventHandler<EventArgs> UpdateOrderChanged;
	}
}
