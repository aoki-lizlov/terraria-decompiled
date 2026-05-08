using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Server
{
	// Token: 0x0200006A RID: 106
	public class Game : IDisposable
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x00076333 File Offset: 0x00074533
		public GameComponentCollection Components
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00076333 File Offset: 0x00074533
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x00009E46 File Offset: 0x00008046
		public ContentManager Content
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00076333 File Offset: 0x00074533
		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x004BB77D File Offset: 0x004B997D
		// (set) Token: 0x0600146C RID: 5228 RVA: 0x00009E46 File Offset: 0x00008046
		public TimeSpan InactiveSleepTime
		{
			get
			{
				return TimeSpan.Zero;
			}
			set
			{
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x000379E9 File Offset: 0x00035BE9
		public bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x000379E9 File Offset: 0x00035BE9
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00009E46 File Offset: 0x00008046
		public bool IsFixedTimeStep
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00009E46 File Offset: 0x00008046
		public bool IsMouseVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00076333 File Offset: 0x00074533
		public LaunchParameters LaunchParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00076333 File Offset: 0x00074533
		public GameServiceContainer Services
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x004BB77D File Offset: 0x004B997D
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00009E46 File Offset: 0x00008046
		public TimeSpan TargetElapsedTime
		{
			get
			{
				return TimeSpan.Zero;
			}
			set
			{
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00076333 File Offset: 0x00074533
		public GameWindow Window
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001477 RID: 5239 RVA: 0x004BB784 File Offset: 0x004B9984
		// (remove) Token: 0x06001478 RID: 5240 RVA: 0x004BB7BC File Offset: 0x004B99BC
		public event EventHandler<EventArgs> Activated
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.Activated;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Activated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.Activated;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Activated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001479 RID: 5241 RVA: 0x004BB7F4 File Offset: 0x004B99F4
		// (remove) Token: 0x0600147A RID: 5242 RVA: 0x004BB82C File Offset: 0x004B9A2C
		public event EventHandler<EventArgs> Deactivated
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.Deactivated;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Deactivated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.Deactivated;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Deactivated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600147B RID: 5243 RVA: 0x004BB864 File Offset: 0x004B9A64
		// (remove) Token: 0x0600147C RID: 5244 RVA: 0x004BB89C File Offset: 0x004B9A9C
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

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600147D RID: 5245 RVA: 0x004BB8D4 File Offset: 0x004B9AD4
		// (remove) Token: 0x0600147E RID: 5246 RVA: 0x004BB90C File Offset: 0x004B9B0C
		public event EventHandler<EventArgs> Exiting
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.Exiting;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Exiting, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.Exiting;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Exiting, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000379E9 File Offset: 0x00035BE9
		protected virtual bool BeginDraw()
		{
			return true;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void BeginRun()
		{
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00009E46 File Offset: 0x00008046
		public void Dispose()
		{
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void Draw(GameTime gameTime)
		{
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void EndDraw()
		{
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void EndRun()
		{
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00009E46 File Offset: 0x00008046
		public void Exit()
		{
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void Initialize()
		{
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void LoadContent()
		{
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void OnActivated(object sender, EventArgs args)
		{
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void OnDeactivated(object sender, EventArgs args)
		{
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void OnExiting(object sender, EventArgs args)
		{
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00009E46 File Offset: 0x00008046
		public void ResetElapsedTime()
		{
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00009E46 File Offset: 0x00008046
		public void Run()
		{
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00009E46 File Offset: 0x00008046
		public void RunOneFrame()
		{
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x000379E9 File Offset: 0x00035BE9
		protected virtual bool ShowMissingRequirementMessage(Exception exception)
		{
			return true;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00009E46 File Offset: 0x00008046
		public void SuppressDraw()
		{
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00009E46 File Offset: 0x00008046
		public void Tick()
		{
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void UnloadContent()
		{
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void Update(GameTime gameTime)
		{
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0000357B File Offset: 0x0000177B
		public Game()
		{
		}

		// Token: 0x0400107E RID: 4222
		[CompilerGenerated]
		private EventHandler<EventArgs> Activated;

		// Token: 0x0400107F RID: 4223
		[CompilerGenerated]
		private EventHandler<EventArgs> Deactivated;

		// Token: 0x04001080 RID: 4224
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposed;

		// Token: 0x04001081 RID: 4225
		[CompilerGenerated]
		private EventHandler<EventArgs> Exiting;
	}
}
