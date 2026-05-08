using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001D RID: 29
	public class Game : IDisposable
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00011F80 File Offset: 0x00010180
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x00011F88 File Offset: 0x00010188
		public GameComponentCollection Components
		{
			[CompilerGenerated]
			get
			{
				return this.<Components>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Components>k__BackingField = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00011F91 File Offset: 0x00010191
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x00011F99 File Offset: 0x00010199
		public ContentManager Content
		{
			get
			{
				return this.INTERNAL_content;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.INTERNAL_content = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00011FAC File Offset: 0x000101AC
		public GraphicsDevice GraphicsDevice
		{
			get
			{
				if (this.graphicsDeviceService == null)
				{
					this.graphicsDeviceService = (IGraphicsDeviceService)this.Services.GetService(typeof(IGraphicsDeviceService));
					if (this.graphicsDeviceService == null)
					{
						throw new InvalidOperationException("No Graphics Device Service");
					}
				}
				return this.graphicsDeviceService.GraphicsDevice;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00011FFF File Offset: 0x000101FF
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x00012007 File Offset: 0x00010207
		public TimeSpan InactiveSleepTime
		{
			get
			{
				return this.INTERNAL_inactiveSleepTime;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("The time must be positive.", null);
				}
				if (this.INTERNAL_inactiveSleepTime != value)
				{
					this.INTERNAL_inactiveSleepTime = value;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00012037 File Offset: 0x00010237
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x0001203F File Offset: 0x0001023F
		public bool IsActive
		{
			get
			{
				return this.INTERNAL_isActive;
			}
			internal set
			{
				if (this.INTERNAL_isActive != value)
				{
					this.INTERNAL_isActive = value;
					if (this.INTERNAL_isActive)
					{
						this.OnActivated(this, EventArgs.Empty);
						return;
					}
					this.OnDeactivated(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00012072 File Offset: 0x00010272
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x0001207A File Offset: 0x0001027A
		public bool IsFixedTimeStep
		{
			[CompilerGenerated]
			get
			{
				return this.<IsFixedTimeStep>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsFixedTimeStep>k__BackingField = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00012083 File Offset: 0x00010283
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0001208B File Offset: 0x0001028B
		public bool IsMouseVisible
		{
			get
			{
				return this.INTERNAL_isMouseVisible;
			}
			set
			{
				if (this.INTERNAL_isMouseVisible != value)
				{
					this.INTERNAL_isMouseVisible = value;
					FNAPlatform.OnIsMouseVisibleChanged(value);
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x000120A8 File Offset: 0x000102A8
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x000120B0 File Offset: 0x000102B0
		public LaunchParameters LaunchParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<LaunchParameters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LaunchParameters>k__BackingField = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x000120B9 File Offset: 0x000102B9
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x000120C1 File Offset: 0x000102C1
		public TimeSpan TargetElapsedTime
		{
			get
			{
				return this.INTERNAL_targetElapsedTime;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("The time must be positive and non-zero.", null);
				}
				this.INTERNAL_targetElapsedTime = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x000120E3 File Offset: 0x000102E3
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x000120EB File Offset: 0x000102EB
		public GameServiceContainer Services
		{
			[CompilerGenerated]
			get
			{
				return this.<Services>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Services>k__BackingField = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x000120F4 File Offset: 0x000102F4
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x000120FC File Offset: 0x000102FC
		public GameWindow Window
		{
			[CompilerGenerated]
			get
			{
				return this.<Window>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Window>k__BackingField = value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000B74 RID: 2932 RVA: 0x00012108 File Offset: 0x00010308
		// (remove) Token: 0x06000B75 RID: 2933 RVA: 0x00012140 File Offset: 0x00010340
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

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000B76 RID: 2934 RVA: 0x00012178 File Offset: 0x00010378
		// (remove) Token: 0x06000B77 RID: 2935 RVA: 0x000121B0 File Offset: 0x000103B0
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

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000B78 RID: 2936 RVA: 0x000121E8 File Offset: 0x000103E8
		// (remove) Token: 0x06000B79 RID: 2937 RVA: 0x00012220 File Offset: 0x00010420
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

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000B7A RID: 2938 RVA: 0x00012258 File Offset: 0x00010458
		// (remove) Token: 0x06000B7B RID: 2939 RVA: 0x00012290 File Offset: 0x00010490
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

		// Token: 0x06000B7C RID: 2940 RVA: 0x000122C8 File Offset: 0x000104C8
		public Game()
		{
			AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;
			this.LaunchParameters = new LaunchParameters();
			this.Components = new GameComponentCollection();
			this.Services = new GameServiceContainer();
			this.Content = new ContentManager(this.Services);
			this.updateableComponents = new List<IUpdateable>();
			this.currentlyUpdatingComponents = new List<IUpdateable>();
			this.drawableComponents = new List<IDrawable>();
			this.currentlyDrawingComponents = new List<IDrawable>();
			this.IsMouseVisible = false;
			this.IsFixedTimeStep = true;
			this.TargetElapsedTime = TimeSpan.FromTicks(166667L);
			this.InactiveSleepTime = TimeSpan.FromSeconds(0.02);
			for (int i = 0; i < this.previousSleepTimes.Length; i++)
			{
				this.previousSleepTimes[i] = TimeSpan.FromMilliseconds(1.0);
			}
			this.textInputControlDown = new bool[FNAPlatform.TextInputCharacters.Length];
			this.hasInitialized = false;
			this.suppressDraw = false;
			this.isDisposed = false;
			this.gameTime = new GameTime();
			this.Window = FNAPlatform.CreateWindow();
			Mouse.WindowHandle = this.Window.Handle;
			TouchPanel.WindowHandle = this.Window.Handle;
			TextInputEXT.WindowHandle = this.Window.Handle;
			FrameworkDispatcher.Update();
			this.RunApplication = true;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00012454 File Offset: 0x00010654
		~Game()
		{
			this.Dispose(false);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00012484 File Offset: 0x00010684
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			if (this.Disposed != null)
			{
				this.Disposed(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000124AC File Offset: 0x000106AC
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					for (int i = 0; i < this.Components.Count; i++)
					{
						IDisposable disposable = this.Components[i] as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					if (this.Content != null)
					{
						this.Content.Dispose();
					}
					if (this.graphicsDeviceService != null)
					{
						(this.graphicsDeviceService as IDisposable).Dispose();
					}
					if (this.Window != null)
					{
						FNAPlatform.DisposeWindow(this.Window);
					}
					ContentTypeReaderManager.ClearTypeCreators();
				}
				AppDomain.CurrentDomain.UnhandledException -= this.OnUnhandledException;
				this.isDisposed = true;
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00012560 File Offset: 0x00010760
		[DebuggerNonUserCode]
		private void AssertNotDisposed()
		{
			if (this.isDisposed)
			{
				string name = base.GetType().Name;
				throw new ObjectDisposedException(name, string.Format("The {0} object was used after being Disposed.", name));
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00012593 File Offset: 0x00010793
		public void Exit()
		{
			this.RunApplication = false;
			this.suppressDraw = true;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000125A3 File Offset: 0x000107A3
		public void ResetElapsedTime()
		{
			if (!this.IsFixedTimeStep)
			{
				this.forceElapsedTimeToZero = true;
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000125B4 File Offset: 0x000107B4
		public void SuppressDraw()
		{
			this.suppressDraw = true;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000125BD File Offset: 0x000107BD
		public void RunOneFrame()
		{
			if (!this.hasInitialized)
			{
				this.DoInitialize();
				this.gameTimer = Stopwatch.StartNew();
				this.hasInitialized = true;
			}
			this.Tick();
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000125E8 File Offset: 0x000107E8
		public void Run()
		{
			this.AssertNotDisposed();
			if (!this.hasInitialized)
			{
				this.DoInitialize();
				this.hasInitialized = true;
			}
			this.BeginRun();
			this.BeforeLoop();
			this.gameTimer = Stopwatch.StartNew();
			this.RunLoop();
			this.EndRun();
			this.AfterLoop();
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0001263C File Offset: 0x0001083C
		public void Tick()
		{
			this.AdvanceElapsedTime();
			if (this.IsFixedTimeStep)
			{
				while (this.accumulatedElapsedTime + this.worstCaseSleepPrecision < this.TargetElapsedTime)
				{
					Thread.Sleep(1);
					TimeSpan timeSpan = this.AdvanceElapsedTime();
					this.UpdateEstimatedSleepPrecision(timeSpan);
				}
				while (this.accumulatedElapsedTime < this.TargetElapsedTime)
				{
					Thread.SpinWait(1);
					this.AdvanceElapsedTime();
				}
			}
			FNAPlatform.PollEvents(this, ref this.currentAdapter, this.textInputControlDown, ref this.textInputSuppress);
			if (this.accumulatedElapsedTime > Game.MaxElapsedTime)
			{
				this.accumulatedElapsedTime = Game.MaxElapsedTime;
			}
			if (this.IsFixedTimeStep)
			{
				this.gameTime.ElapsedGameTime = this.TargetElapsedTime;
				int num = 0;
				while (this.accumulatedElapsedTime >= this.TargetElapsedTime)
				{
					this.gameTime.TotalGameTime += this.TargetElapsedTime;
					this.accumulatedElapsedTime -= this.TargetElapsedTime;
					num++;
					this.AssertNotDisposed();
					this.Update(this.gameTime);
				}
				this.updateFrameLag += Math.Max(0, num - 1);
				if (this.gameTime.IsRunningSlowly)
				{
					if (this.updateFrameLag == 0)
					{
						this.gameTime.IsRunningSlowly = false;
					}
				}
				else if (this.updateFrameLag >= 5)
				{
					this.gameTime.IsRunningSlowly = true;
				}
				if (num == 1 && this.updateFrameLag > 0)
				{
					this.updateFrameLag--;
				}
				this.gameTime.ElapsedGameTime = TimeSpan.FromTicks(this.TargetElapsedTime.Ticks * (long)num);
			}
			else
			{
				if (this.forceElapsedTimeToZero)
				{
					this.gameTime.ElapsedGameTime = TimeSpan.Zero;
					this.forceElapsedTimeToZero = false;
				}
				else
				{
					this.gameTime.ElapsedGameTime = this.accumulatedElapsedTime;
					this.gameTime.TotalGameTime += this.gameTime.ElapsedGameTime;
				}
				this.accumulatedElapsedTime = TimeSpan.Zero;
				this.AssertNotDisposed();
				this.Update(this.gameTime);
			}
			if (this.suppressDraw)
			{
				this.suppressDraw = false;
				return;
			}
			if (this.BeginDraw())
			{
				this.Draw(this.gameTime);
				this.EndDraw();
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0001288C File Offset: 0x00010A8C
		internal void RedrawWindow()
		{
			if (this.gameTime.TotalGameTime != TimeSpan.Zero && this.BeginDraw())
			{
				this.Draw(new GameTime(this.gameTime.TotalGameTime, TimeSpan.Zero));
				this.EndDraw();
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000128D9 File Offset: 0x00010AD9
		protected virtual bool BeginDraw()
		{
			return this.graphicsDeviceManager == null || this.graphicsDeviceManager.BeginDraw();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x000128F0 File Offset: 0x00010AF0
		protected virtual void EndDraw()
		{
			if (this.graphicsDeviceManager != null)
			{
				this.graphicsDeviceManager.EndDraw();
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void BeginRun()
		{
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void EndRun()
		{
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void LoadContent()
		{
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00009E6B File Offset: 0x0000806B
		protected virtual void UnloadContent()
		{
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00012908 File Offset: 0x00010B08
		protected virtual void Initialize()
		{
			for (int i = 0; i < this.Components.Count; i++)
			{
				this.Components[i].Initialize();
			}
			this.graphicsDeviceService = (IGraphicsDeviceService)this.Services.GetService(typeof(IGraphicsDeviceService));
			if (this.graphicsDeviceService != null)
			{
				this.graphicsDeviceService.DeviceDisposing += delegate(object o, EventArgs e)
				{
					this.UnloadContent();
				};
				if (this.graphicsDeviceService.GraphicsDevice != null)
				{
					this.LoadContent();
					return;
				}
				this.graphicsDeviceService.DeviceCreated += delegate(object o, EventArgs e)
				{
					this.LoadContent();
				};
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000129A8 File Offset: 0x00010BA8
		protected virtual void Draw(GameTime gameTime)
		{
			List<IDrawable> list = this.drawableComponents;
			lock (list)
			{
				for (int i = 0; i < this.drawableComponents.Count; i++)
				{
					this.currentlyDrawingComponents.Add(this.drawableComponents[i]);
				}
			}
			foreach (IDrawable drawable in this.currentlyDrawingComponents)
			{
				if (drawable.Visible)
				{
					drawable.Draw(gameTime);
				}
			}
			this.currentlyDrawingComponents.Clear();
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00012A68 File Offset: 0x00010C68
		protected virtual void Update(GameTime gameTime)
		{
			List<IUpdateable> list = this.updateableComponents;
			lock (list)
			{
				for (int i = 0; i < this.updateableComponents.Count; i++)
				{
					this.currentlyUpdatingComponents.Add(this.updateableComponents[i]);
				}
			}
			foreach (IUpdateable updateable in this.currentlyUpdatingComponents)
			{
				if (updateable.Enabled)
				{
					updateable.Update(gameTime);
				}
			}
			this.currentlyUpdatingComponents.Clear();
			FrameworkDispatcher.Update();
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00012B2C File Offset: 0x00010D2C
		protected virtual void OnExiting(object sender, EventArgs args)
		{
			if (this.Exiting != null)
			{
				this.Exiting(this, args);
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00012B43 File Offset: 0x00010D43
		protected virtual void OnActivated(object sender, EventArgs args)
		{
			this.AssertNotDisposed();
			if (this.Activated != null)
			{
				this.Activated(this, args);
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00012B60 File Offset: 0x00010D60
		protected virtual void OnDeactivated(object sender, EventArgs args)
		{
			this.AssertNotDisposed();
			if (this.Deactivated != null)
			{
				this.Deactivated(this, args);
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00012B80 File Offset: 0x00010D80
		protected virtual bool ShowMissingRequirementMessage(Exception exception)
		{
			if (exception is NoAudioHardwareException)
			{
				FNAPlatform.ShowRuntimeError(this.Window.Title, "Could not find a suitable audio device.  Verify that a sound card is\ninstalled, and check the driver properties to make sure it is not disabled.");
				return true;
			}
			if (exception is NoSuitableGraphicsDeviceException)
			{
				FNAPlatform.ShowRuntimeError(this.Window.Title, "Could not find a suitable graphics device. More information:\n\n" + exception.Message);
				return true;
			}
			return false;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00012BE4 File Offset: 0x00010DE4
		private void DoInitialize()
		{
			this.AssertNotDisposed();
			this.graphicsDeviceManager = (IGraphicsDeviceManager)this.Services.GetService(typeof(IGraphicsDeviceManager));
			if (this.graphicsDeviceManager != null)
			{
				this.graphicsDeviceManager.CreateDevice();
			}
			this.Initialize();
			this.updateableComponents.Clear();
			this.drawableComponents.Clear();
			for (int i = 0; i < this.Components.Count; i++)
			{
				this.CategorizeComponent(this.Components[i]);
			}
			this.Components.ComponentAdded += this.OnComponentAdded;
			this.Components.ComponentRemoved += this.OnComponentRemoved;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00012C9C File Offset: 0x00010E9C
		private void CategorizeComponent(IGameComponent component)
		{
			IUpdateable updateable = component as IUpdateable;
			if (updateable != null)
			{
				List<IUpdateable> list = this.updateableComponents;
				lock (list)
				{
					this.SortUpdateable(updateable);
				}
				updateable.UpdateOrderChanged += this.OnUpdateOrderChanged;
			}
			IDrawable drawable = component as IDrawable;
			if (drawable != null)
			{
				List<IDrawable> list2 = this.drawableComponents;
				lock (list2)
				{
					this.SortDrawable(drawable);
				}
				drawable.DrawOrderChanged += this.OnDrawOrderChanged;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00012D48 File Offset: 0x00010F48
		private void SortUpdateable(IUpdateable updateable)
		{
			for (int i = 0; i < this.updateableComponents.Count; i++)
			{
				if (updateable.UpdateOrder < this.updateableComponents[i].UpdateOrder)
				{
					this.updateableComponents.Insert(i, updateable);
					return;
				}
			}
			this.updateableComponents.Add(updateable);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00012DA0 File Offset: 0x00010FA0
		private void SortDrawable(IDrawable drawable)
		{
			for (int i = 0; i < this.drawableComponents.Count; i++)
			{
				if (drawable.DrawOrder < this.drawableComponents[i].DrawOrder)
				{
					this.drawableComponents.Insert(i, drawable);
					return;
				}
			}
			this.drawableComponents.Add(drawable);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00012DF8 File Offset: 0x00010FF8
		private void BeforeLoop()
		{
			this.currentAdapter = FNAPlatform.RegisterGame(this);
			this.IsActive = true;
			TouchPanel.TouchDeviceExists = FNAPlatform.GetTouchCapabilities().IsConnected;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00012E34 File Offset: 0x00011034
		private void AfterLoop()
		{
			FNAPlatform.UnregisterGame(this);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00012E41 File Offset: 0x00011041
		private void RunLoop()
		{
			if (FNAPlatform.NeedsPlatformMainLoop())
			{
				FNAPlatform.RunPlatformMainLoop(this);
			}
			while (this.RunApplication)
			{
				this.Tick();
			}
			this.OnExiting(this, EventArgs.Empty);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00012E78 File Offset: 0x00011078
		private TimeSpan AdvanceElapsedTime()
		{
			long ticks = this.gameTimer.Elapsed.Ticks;
			TimeSpan timeSpan = TimeSpan.FromTicks(ticks - this.previousTicks);
			this.accumulatedElapsedTime += timeSpan;
			this.previousTicks = ticks;
			return timeSpan;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00012EC4 File Offset: 0x000110C4
		private void UpdateEstimatedSleepPrecision(TimeSpan timeSpentSleeping)
		{
			TimeSpan timeSpan = TimeSpan.FromMilliseconds(4.0);
			if (timeSpentSleeping > timeSpan)
			{
				timeSpentSleeping = timeSpan;
			}
			if (timeSpentSleeping >= this.worstCaseSleepPrecision)
			{
				this.worstCaseSleepPrecision = timeSpentSleeping;
			}
			else if (this.previousSleepTimes[this.sleepTimeIndex] == this.worstCaseSleepPrecision)
			{
				TimeSpan timeSpan2 = TimeSpan.MinValue;
				for (int i = 0; i < this.previousSleepTimes.Length; i++)
				{
					if (this.previousSleepTimes[i] > timeSpan2)
					{
						timeSpan2 = this.previousSleepTimes[i];
					}
				}
				this.worstCaseSleepPrecision = timeSpan2;
			}
			this.previousSleepTimes[this.sleepTimeIndex] = timeSpentSleeping;
			this.sleepTimeIndex = (this.sleepTimeIndex + 1) & 127;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00012F85 File Offset: 0x00011185
		private void OnComponentAdded(object sender, GameComponentCollectionEventArgs e)
		{
			e.GameComponent.Initialize();
			this.CategorizeComponent(e.GameComponent);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00012FA0 File Offset: 0x000111A0
		private void OnComponentRemoved(object sender, GameComponentCollectionEventArgs e)
		{
			IUpdateable updateable = e.GameComponent as IUpdateable;
			if (updateable != null)
			{
				List<IUpdateable> list = this.updateableComponents;
				lock (list)
				{
					this.updateableComponents.Remove(updateable);
				}
				updateable.UpdateOrderChanged -= this.OnUpdateOrderChanged;
			}
			IDrawable drawable = e.GameComponent as IDrawable;
			if (drawable != null)
			{
				List<IDrawable> list2 = this.drawableComponents;
				lock (list2)
				{
					this.drawableComponents.Remove(drawable);
				}
				drawable.DrawOrderChanged -= this.OnDrawOrderChanged;
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00013064 File Offset: 0x00011264
		private void OnUpdateOrderChanged(object sender, EventArgs e)
		{
			IUpdateable updateable = sender as IUpdateable;
			List<IUpdateable> list = this.updateableComponents;
			lock (list)
			{
				this.updateableComponents.Remove(updateable);
				this.SortUpdateable(updateable);
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000130BC File Offset: 0x000112BC
		private void OnDrawOrderChanged(object sender, EventArgs e)
		{
			IDrawable drawable = sender as IDrawable;
			List<IDrawable> list = this.drawableComponents;
			lock (list)
			{
				this.drawableComponents.Remove(drawable);
				this.SortDrawable(drawable);
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00013114 File Offset: 0x00011314
		private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
		{
			this.ShowMissingRequirementMessage(args.ExceptionObject as Exception);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00013128 File Offset: 0x00011328
		// Note: this type is marked as 'beforefieldinit'.
		static Game()
		{
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001313D File Offset: 0x0001133D
		[CompilerGenerated]
		private void <Initialize>b__97_0(object o, EventArgs e)
		{
			this.UnloadContent();
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00013145 File Offset: 0x00011345
		[CompilerGenerated]
		private void <Initialize>b__97_1(object o, EventArgs e)
		{
			this.LoadContent();
		}

		// Token: 0x04000532 RID: 1330
		[CompilerGenerated]
		private GameComponentCollection <Components>k__BackingField;

		// Token: 0x04000533 RID: 1331
		private ContentManager INTERNAL_content;

		// Token: 0x04000534 RID: 1332
		private TimeSpan INTERNAL_inactiveSleepTime;

		// Token: 0x04000535 RID: 1333
		private bool INTERNAL_isActive;

		// Token: 0x04000536 RID: 1334
		[CompilerGenerated]
		private bool <IsFixedTimeStep>k__BackingField;

		// Token: 0x04000537 RID: 1335
		private bool INTERNAL_isMouseVisible;

		// Token: 0x04000538 RID: 1336
		[CompilerGenerated]
		private LaunchParameters <LaunchParameters>k__BackingField;

		// Token: 0x04000539 RID: 1337
		private TimeSpan INTERNAL_targetElapsedTime;

		// Token: 0x0400053A RID: 1338
		[CompilerGenerated]
		private GameServiceContainer <Services>k__BackingField;

		// Token: 0x0400053B RID: 1339
		[CompilerGenerated]
		private GameWindow <Window>k__BackingField;

		// Token: 0x0400053C RID: 1340
		internal bool RunApplication;

		// Token: 0x0400053D RID: 1341
		private List<IUpdateable> updateableComponents;

		// Token: 0x0400053E RID: 1342
		private List<IUpdateable> currentlyUpdatingComponents;

		// Token: 0x0400053F RID: 1343
		private List<IDrawable> drawableComponents;

		// Token: 0x04000540 RID: 1344
		private List<IDrawable> currentlyDrawingComponents;

		// Token: 0x04000541 RID: 1345
		private IGraphicsDeviceService graphicsDeviceService;

		// Token: 0x04000542 RID: 1346
		private IGraphicsDeviceManager graphicsDeviceManager;

		// Token: 0x04000543 RID: 1347
		private GraphicsAdapter currentAdapter;

		// Token: 0x04000544 RID: 1348
		private bool hasInitialized;

		// Token: 0x04000545 RID: 1349
		private bool suppressDraw;

		// Token: 0x04000546 RID: 1350
		private bool isDisposed;

		// Token: 0x04000547 RID: 1351
		private readonly GameTime gameTime;

		// Token: 0x04000548 RID: 1352
		private Stopwatch gameTimer;

		// Token: 0x04000549 RID: 1353
		private TimeSpan accumulatedElapsedTime;

		// Token: 0x0400054A RID: 1354
		private long previousTicks;

		// Token: 0x0400054B RID: 1355
		private int updateFrameLag;

		// Token: 0x0400054C RID: 1356
		private bool forceElapsedTimeToZero;

		// Token: 0x0400054D RID: 1357
		private const int PREVIOUS_SLEEP_TIME_COUNT = 128;

		// Token: 0x0400054E RID: 1358
		private const int SLEEP_TIME_MASK = 127;

		// Token: 0x0400054F RID: 1359
		private TimeSpan[] previousSleepTimes = new TimeSpan[128];

		// Token: 0x04000550 RID: 1360
		private int sleepTimeIndex;

		// Token: 0x04000551 RID: 1361
		private TimeSpan worstCaseSleepPrecision = TimeSpan.FromMilliseconds(1.0);

		// Token: 0x04000552 RID: 1362
		private static readonly TimeSpan MaxElapsedTime = TimeSpan.FromMilliseconds(500.0);

		// Token: 0x04000553 RID: 1363
		private bool[] textInputControlDown;

		// Token: 0x04000554 RID: 1364
		private bool textInputSuppress;

		// Token: 0x04000555 RID: 1365
		[CompilerGenerated]
		private EventHandler<EventArgs> Activated;

		// Token: 0x04000556 RID: 1366
		[CompilerGenerated]
		private EventHandler<EventArgs> Deactivated;

		// Token: 0x04000557 RID: 1367
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposed;

		// Token: 0x04000558 RID: 1368
		[CompilerGenerated]
		private EventHandler<EventArgs> Exiting;
	}
}
