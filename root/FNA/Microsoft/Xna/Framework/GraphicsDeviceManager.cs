using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000025 RID: 37
	public class GraphicsDeviceManager : IGraphicsDeviceService, IDisposable, IGraphicsDeviceManager
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00013920 File Offset: 0x00011B20
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00013928 File Offset: 0x00011B28
		public GraphicsProfile GraphicsProfile
		{
			[CompilerGenerated]
			get
			{
				return this.<GraphicsProfile>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GraphicsProfile>k__BackingField = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00013931 File Offset: 0x00011B31
		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return this.graphicsDevice;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00013939 File Offset: 0x00011B39
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00013941 File Offset: 0x00011B41
		public bool IsFullScreen
		{
			get
			{
				return this.INTERNAL_isFullScreen;
			}
			set
			{
				this.INTERNAL_isFullScreen = value;
				this.prefsChanged = true;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00013951 File Offset: 0x00011B51
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00013959 File Offset: 0x00011B59
		public bool PreferMultiSampling
		{
			get
			{
				return this.INTERNAL_preferMultiSampling;
			}
			set
			{
				this.INTERNAL_preferMultiSampling = value;
				this.prefsChanged = true;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00013969 File Offset: 0x00011B69
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00013971 File Offset: 0x00011B71
		public SurfaceFormat PreferredBackBufferFormat
		{
			get
			{
				return this.INTERNAL_preferredBackBufferFormat;
			}
			set
			{
				this.INTERNAL_preferredBackBufferFormat = value;
				this.prefsChanged = true;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00013981 File Offset: 0x00011B81
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00013989 File Offset: 0x00011B89
		public int PreferredBackBufferHeight
		{
			get
			{
				return this.INTERNAL_preferredBackBufferHeight;
			}
			set
			{
				this.INTERNAL_preferredBackBufferHeight = value;
				this.prefsChanged = true;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00013999 File Offset: 0x00011B99
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x000139A1 File Offset: 0x00011BA1
		public int PreferredBackBufferWidth
		{
			get
			{
				return this.INTERNAL_preferredBackBufferWidth;
			}
			set
			{
				this.INTERNAL_preferredBackBufferWidth = value;
				this.prefsChanged = true;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x000139B1 File Offset: 0x00011BB1
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x000139B9 File Offset: 0x00011BB9
		public DepthFormat PreferredDepthStencilFormat
		{
			get
			{
				return this.INTERNAL_preferredDepthStencilFormat;
			}
			set
			{
				this.INTERNAL_preferredDepthStencilFormat = value;
				this.prefsChanged = true;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x000139C9 File Offset: 0x00011BC9
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x000139D1 File Offset: 0x00011BD1
		public bool SynchronizeWithVerticalRetrace
		{
			get
			{
				return this.INTERNAL_synchronizeWithVerticalRetrace;
			}
			set
			{
				this.INTERNAL_synchronizeWithVerticalRetrace = value;
				this.prefsChanged = true;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000139E1 File Offset: 0x00011BE1
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x000139E9 File Offset: 0x00011BE9
		public DisplayOrientation SupportedOrientations
		{
			get
			{
				return this.INTERNAL_supportedOrientations;
			}
			set
			{
				this.INTERNAL_supportedOrientations = value;
				this.prefsChanged = true;
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000C0E RID: 3086 RVA: 0x000139FC File Offset: 0x00011BFC
		// (remove) Token: 0x06000C0F RID: 3087 RVA: 0x00013A34 File Offset: 0x00011C34
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

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000C10 RID: 3088 RVA: 0x00013A6C File Offset: 0x00011C6C
		// (remove) Token: 0x06000C11 RID: 3089 RVA: 0x00013AA4 File Offset: 0x00011CA4
		public event EventHandler<EventArgs> DeviceCreated
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.DeviceCreated;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceCreated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.DeviceCreated;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceCreated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000C12 RID: 3090 RVA: 0x00013ADC File Offset: 0x00011CDC
		// (remove) Token: 0x06000C13 RID: 3091 RVA: 0x00013B14 File Offset: 0x00011D14
		public event EventHandler<EventArgs> DeviceDisposing
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.DeviceDisposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceDisposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.DeviceDisposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceDisposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000C14 RID: 3092 RVA: 0x00013B4C File Offset: 0x00011D4C
		// (remove) Token: 0x06000C15 RID: 3093 RVA: 0x00013B84 File Offset: 0x00011D84
		public event EventHandler<EventArgs> DeviceReset
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.DeviceReset;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceReset, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.DeviceReset;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceReset, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000C16 RID: 3094 RVA: 0x00013BBC File Offset: 0x00011DBC
		// (remove) Token: 0x06000C17 RID: 3095 RVA: 0x00013BF4 File Offset: 0x00011DF4
		public event EventHandler<EventArgs> DeviceResetting
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.DeviceResetting;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceResetting, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.DeviceResetting;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceResetting, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000C18 RID: 3096 RVA: 0x00013C2C File Offset: 0x00011E2C
		// (remove) Token: 0x06000C19 RID: 3097 RVA: 0x00013C64 File Offset: 0x00011E64
		public event EventHandler<PreparingDeviceSettingsEventArgs> PreparingDeviceSettings
		{
			[CompilerGenerated]
			add
			{
				EventHandler<PreparingDeviceSettingsEventArgs> eventHandler = this.PreparingDeviceSettings;
				EventHandler<PreparingDeviceSettingsEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<PreparingDeviceSettingsEventArgs> eventHandler3 = (EventHandler<PreparingDeviceSettingsEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<PreparingDeviceSettingsEventArgs>>(ref this.PreparingDeviceSettings, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<PreparingDeviceSettingsEventArgs> eventHandler = this.PreparingDeviceSettings;
				EventHandler<PreparingDeviceSettingsEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<PreparingDeviceSettingsEventArgs> eventHandler3 = (EventHandler<PreparingDeviceSettingsEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<PreparingDeviceSettingsEventArgs>>(ref this.PreparingDeviceSettings, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00013C9C File Offset: 0x00011E9C
		public GraphicsDeviceManager(Game game)
		{
			if (game == null)
			{
				throw new ArgumentNullException("The game cannot be null!");
			}
			this.game = game;
			this.INTERNAL_supportedOrientations = DisplayOrientation.Default;
			this.INTERNAL_preferredBackBufferHeight = GraphicsDeviceManager.DefaultBackBufferHeight;
			this.INTERNAL_preferredBackBufferWidth = GraphicsDeviceManager.DefaultBackBufferWidth;
			this.INTERNAL_preferredBackBufferFormat = SurfaceFormat.Color;
			this.INTERNAL_preferredDepthStencilFormat = DepthFormat.Depth24;
			this.INTERNAL_synchronizeWithVerticalRetrace = true;
			this.INTERNAL_preferMultiSampling = false;
			if (game.Services.GetService(typeof(IGraphicsDeviceManager)) != null)
			{
				throw new ArgumentException("Graphics Device Manager Already Present");
			}
			game.Services.AddService(typeof(IGraphicsDeviceManager), this);
			game.Services.AddService(typeof(IGraphicsDeviceService), this);
			this.prefsChanged = true;
			this.useResizedBackBuffer = false;
			this.supportsOrientations = FNAPlatform.SupportsOrientationChanges();
			game.Window.ClientSizeChanged += this.INTERNAL_OnClientSizeChanged;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00013D80 File Offset: 0x00011F80
		~GraphicsDeviceManager()
		{
			this.Dispose(false);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00013DB0 File Offset: 0x00011FB0
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.game.Services.RemoveService(typeof(IGraphicsDeviceManager));
				this.game.Services.RemoveService(typeof(IGraphicsDeviceService));
				if (disposing && this.graphicsDevice != null)
				{
					this.graphicsDevice.Dispose();
					this.graphicsDevice = null;
				}
				if (this.Disposed != null)
				{
					this.Disposed(this, EventArgs.Empty);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00013E36 File Offset: 0x00012036
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00013E48 File Offset: 0x00012048
		public void ApplyChanges()
		{
			if (this.graphicsDevice == null)
			{
				((IGraphicsDeviceManager)this).CreateDevice();
				return;
			}
			if (!this.prefsChanged && !this.useResizedBackBuffer)
			{
				return;
			}
			GraphicsDeviceInformation graphicsDeviceInformation = new GraphicsDeviceInformation();
			graphicsDeviceInformation.Adapter = this.graphicsDevice.Adapter;
			graphicsDeviceInformation.PresentationParameters = this.graphicsDevice.PresentationParameters.Clone();
			this.INTERNAL_CreateGraphicsDeviceInformation(graphicsDeviceInformation);
			if (this.supportsOrientations)
			{
				this.game.Window.SetSupportedOrientations(this.INTERNAL_supportedOrientations);
			}
			this.game.Window.BeginScreenDeviceChange(graphicsDeviceInformation.PresentationParameters.IsFullScreen);
			this.game.Window.EndScreenDeviceChange(graphicsDeviceInformation.Adapter.DeviceName, graphicsDeviceInformation.PresentationParameters.BackBufferWidth, graphicsDeviceInformation.PresentationParameters.BackBufferHeight);
			this.graphicsDevice.Reset(graphicsDeviceInformation.PresentationParameters, graphicsDeviceInformation.Adapter);
			this.prefsChanged = false;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00013F31 File Offset: 0x00012131
		public void ToggleFullScreen()
		{
			this.IsFullScreen = !this.IsFullScreen;
			this.ApplyChanges();
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00013F48 File Offset: 0x00012148
		protected virtual void OnDeviceCreated(object sender, EventArgs args)
		{
			if (this.DeviceCreated != null)
			{
				this.DeviceCreated(sender, args);
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00013F5F File Offset: 0x0001215F
		protected virtual void OnDeviceDisposing(object sender, EventArgs args)
		{
			if (this.DeviceDisposing != null)
			{
				this.DeviceDisposing(this, args);
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00013F76 File Offset: 0x00012176
		protected virtual void OnDeviceReset(object sender, EventArgs args)
		{
			if (this.DeviceReset != null)
			{
				this.DeviceReset(this, args);
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00013F8D File Offset: 0x0001218D
		protected virtual void OnDeviceResetting(object sender, EventArgs args)
		{
			if (this.DeviceResetting != null)
			{
				this.DeviceResetting(this, args);
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00013FA4 File Offset: 0x000121A4
		protected virtual void OnPreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs args)
		{
			if (this.PreparingDeviceSettings != null)
			{
				this.PreparingDeviceSettings(sender, args);
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x000136EE File Offset: 0x000118EE
		protected virtual bool CanResetDevice(GraphicsDeviceInformation newDeviceInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000136EE File Offset: 0x000118EE
		protected virtual GraphicsDeviceInformation FindBestDevice(bool anySuitableDevice)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000136EE File Offset: 0x000118EE
		protected virtual void RankDevices(List<GraphicsDeviceInformation> foundDevices)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00013FBC File Offset: 0x000121BC
		private void INTERNAL_OnClientSizeChanged(object sender, EventArgs e)
		{
			GameWindow gameWindow = sender as GameWindow;
			Rectangle clientBounds = gameWindow.ClientBounds;
			this.resizedBackBufferWidth = clientBounds.Width;
			this.resizedBackBufferHeight = clientBounds.Height;
			FNAPlatform.ScaleForWindow(gameWindow.Handle, true, ref this.resizedBackBufferWidth, ref this.resizedBackBufferHeight);
			this.useResizedBackBuffer = true;
			this.ApplyChanges();
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0001401C File Offset: 0x0001221C
		private void INTERNAL_CreateGraphicsDeviceInformation(GraphicsDeviceInformation gdi)
		{
			if (this.useResizedBackBuffer)
			{
				gdi.PresentationParameters.BackBufferWidth = this.resizedBackBufferWidth;
				gdi.PresentationParameters.BackBufferHeight = this.resizedBackBufferHeight;
				this.useResizedBackBuffer = false;
			}
			else if (!this.supportsOrientations)
			{
				gdi.PresentationParameters.BackBufferWidth = this.PreferredBackBufferWidth;
				gdi.PresentationParameters.BackBufferHeight = this.PreferredBackBufferHeight;
			}
			else
			{
				int num = Math.Min(this.PreferredBackBufferWidth, this.PreferredBackBufferHeight);
				int num2 = Math.Max(this.PreferredBackBufferWidth, this.PreferredBackBufferHeight);
				if (gdi.PresentationParameters.DisplayOrientation == DisplayOrientation.Portrait)
				{
					gdi.PresentationParameters.BackBufferWidth = num;
					gdi.PresentationParameters.BackBufferHeight = num2;
				}
				else
				{
					gdi.PresentationParameters.BackBufferWidth = num2;
					gdi.PresentationParameters.BackBufferHeight = num;
				}
			}
			gdi.PresentationParameters.BackBufferFormat = this.PreferredBackBufferFormat;
			gdi.PresentationParameters.DepthStencilFormat = this.PreferredDepthStencilFormat;
			gdi.PresentationParameters.IsFullScreen = this.IsFullScreen;
			gdi.PresentationParameters.PresentationInterval = (this.SynchronizeWithVerticalRetrace ? PresentInterval.One : PresentInterval.Immediate);
			if (!this.PreferMultiSampling)
			{
				gdi.PresentationParameters.MultiSampleCount = 0;
			}
			else if (gdi.PresentationParameters.MultiSampleCount == 0)
			{
				int num3 = 0;
				if (this.graphicsDevice != null)
				{
					num3 = FNA3D.FNA3D_GetMaxMultiSampleCount(this.graphicsDevice.GLDevice, gdi.PresentationParameters.BackBufferFormat, 8);
				}
				gdi.PresentationParameters.MultiSampleCount = Math.Min(num3, 8);
			}
			gdi.GraphicsProfile = this.GraphicsProfile;
			this.OnPreparingDeviceSettings(this, new PreparingDeviceSettingsEventArgs(gdi));
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000141B0 File Offset: 0x000123B0
		void IGraphicsDeviceManager.CreateDevice()
		{
			if (this.graphicsDevice != null)
			{
				this.graphicsDevice.Dispose();
				this.graphicsDevice = null;
			}
			GraphicsDeviceInformation graphicsDeviceInformation = new GraphicsDeviceInformation();
			graphicsDeviceInformation.Adapter = GraphicsAdapter.DefaultAdapter;
			graphicsDeviceInformation.PresentationParameters = new PresentationParameters();
			graphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = this.game.Window.Handle;
			this.INTERNAL_CreateGraphicsDeviceInformation(graphicsDeviceInformation);
			if (this.supportsOrientations)
			{
				this.game.Window.SetSupportedOrientations(this.INTERNAL_supportedOrientations);
			}
			this.game.Window.BeginScreenDeviceChange(graphicsDeviceInformation.PresentationParameters.IsFullScreen);
			this.game.Window.EndScreenDeviceChange(graphicsDeviceInformation.Adapter.DeviceName, graphicsDeviceInformation.PresentationParameters.BackBufferWidth, graphicsDeviceInformation.PresentationParameters.BackBufferHeight);
			this.graphicsDevice = new GraphicsDevice(graphicsDeviceInformation.Adapter, graphicsDeviceInformation.GraphicsProfile, graphicsDeviceInformation.PresentationParameters);
			this.graphicsDevice.Disposing += this.OnDeviceDisposing;
			this.graphicsDevice.DeviceResetting += this.OnDeviceResetting;
			this.graphicsDevice.DeviceReset += this.OnDeviceReset;
			this.OnDeviceCreated(this, EventArgs.Empty);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000142F0 File Offset: 0x000124F0
		bool IGraphicsDeviceManager.BeginDraw()
		{
			if (this.graphicsDevice == null)
			{
				return false;
			}
			this.drawBegun = true;
			return true;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00014304 File Offset: 0x00012504
		void IGraphicsDeviceManager.EndDraw()
		{
			if (this.graphicsDevice != null && this.drawBegun)
			{
				this.drawBegun = false;
				this.graphicsDevice.Present();
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00014328 File Offset: 0x00012528
		// Note: this type is marked as 'beforefieldinit'.
		static GraphicsDeviceManager()
		{
		}

		// Token: 0x0400056D RID: 1389
		[CompilerGenerated]
		private GraphicsProfile <GraphicsProfile>k__BackingField;

		// Token: 0x0400056E RID: 1390
		private bool INTERNAL_isFullScreen;

		// Token: 0x0400056F RID: 1391
		private bool INTERNAL_preferMultiSampling;

		// Token: 0x04000570 RID: 1392
		private SurfaceFormat INTERNAL_preferredBackBufferFormat;

		// Token: 0x04000571 RID: 1393
		private int INTERNAL_preferredBackBufferHeight;

		// Token: 0x04000572 RID: 1394
		private int INTERNAL_preferredBackBufferWidth;

		// Token: 0x04000573 RID: 1395
		private DepthFormat INTERNAL_preferredDepthStencilFormat;

		// Token: 0x04000574 RID: 1396
		private bool INTERNAL_synchronizeWithVerticalRetrace;

		// Token: 0x04000575 RID: 1397
		private DisplayOrientation INTERNAL_supportedOrientations;

		// Token: 0x04000576 RID: 1398
		private Game game;

		// Token: 0x04000577 RID: 1399
		private GraphicsDevice graphicsDevice;

		// Token: 0x04000578 RID: 1400
		private bool drawBegun;

		// Token: 0x04000579 RID: 1401
		private bool disposed;

		// Token: 0x0400057A RID: 1402
		private bool prefsChanged;

		// Token: 0x0400057B RID: 1403
		private bool supportsOrientations;

		// Token: 0x0400057C RID: 1404
		private bool useResizedBackBuffer;

		// Token: 0x0400057D RID: 1405
		private int resizedBackBufferWidth;

		// Token: 0x0400057E RID: 1406
		private int resizedBackBufferHeight;

		// Token: 0x0400057F RID: 1407
		public static readonly int DefaultBackBufferWidth = 800;

		// Token: 0x04000580 RID: 1408
		public static readonly int DefaultBackBufferHeight = 480;

		// Token: 0x04000581 RID: 1409
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposed;

		// Token: 0x04000582 RID: 1410
		[CompilerGenerated]
		private EventHandler<EventArgs> DeviceCreated;

		// Token: 0x04000583 RID: 1411
		[CompilerGenerated]
		private EventHandler<EventArgs> DeviceDisposing;

		// Token: 0x04000584 RID: 1412
		[CompilerGenerated]
		private EventHandler<EventArgs> DeviceReset;

		// Token: 0x04000585 RID: 1413
		[CompilerGenerated]
		private EventHandler<EventArgs> DeviceResetting;

		// Token: 0x04000586 RID: 1414
		[CompilerGenerated]
		private EventHandler<PreparingDeviceSettingsEventArgs> PreparingDeviceSettings;
	}
}
