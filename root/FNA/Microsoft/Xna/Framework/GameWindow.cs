using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000023 RID: 35
	public abstract class GameWindow
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000BD6 RID: 3030
		// (set) Token: 0x06000BD7 RID: 3031
		[DefaultValue(false)]
		public abstract bool AllowUserResizing { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000BD8 RID: 3032
		public abstract Rectangle ClientBounds { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000BD9 RID: 3033
		// (set) Token: 0x06000BDA RID: 3034
		public abstract DisplayOrientation CurrentOrientation { get; internal set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000BDB RID: 3035
		public abstract IntPtr Handle { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000BDC RID: 3036
		public abstract string ScreenDeviceName { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x000136C5 File Offset: 0x000118C5
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x000136CD File Offset: 0x000118CD
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (this._title != value)
				{
					this.SetTitle(value);
					this._title = value;
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000136EB File Offset: 0x000118EB
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x000136EE File Offset: 0x000118EE
		public virtual bool IsBorderlessEXT
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000136F5 File Offset: 0x000118F5
		protected GameWindow()
		{
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000BE2 RID: 3042 RVA: 0x00013700 File Offset: 0x00011900
		// (remove) Token: 0x06000BE3 RID: 3043 RVA: 0x00013738 File Offset: 0x00011938
		public event EventHandler<EventArgs> ClientSizeChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.ClientSizeChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.ClientSizeChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.ClientSizeChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.ClientSizeChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000BE4 RID: 3044 RVA: 0x00013770 File Offset: 0x00011970
		// (remove) Token: 0x06000BE5 RID: 3045 RVA: 0x000137A8 File Offset: 0x000119A8
		public event EventHandler<EventArgs> OrientationChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.OrientationChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.OrientationChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.OrientationChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.OrientationChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000BE6 RID: 3046 RVA: 0x000137E0 File Offset: 0x000119E0
		// (remove) Token: 0x06000BE7 RID: 3047 RVA: 0x00013818 File Offset: 0x00011A18
		public event EventHandler<EventArgs> ScreenDeviceNameChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.ScreenDeviceNameChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.ScreenDeviceNameChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.ScreenDeviceNameChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.ScreenDeviceNameChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06000BE8 RID: 3048
		public abstract void BeginScreenDeviceChange(bool willBeFullScreen);

		// Token: 0x06000BE9 RID: 3049
		public abstract void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight);

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001384D File Offset: 0x00011A4D
		public void EndScreenDeviceChange(string screenDeviceName)
		{
			this.EndScreenDeviceChange(screenDeviceName, this.ClientBounds.Width, this.ClientBounds.Height);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00009E6B File Offset: 0x0000806B
		protected void OnActivated()
		{
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0001386C File Offset: 0x00011A6C
		protected void OnClientSizeChanged()
		{
			if (this.ClientSizeChanged != null)
			{
				this.ClientSizeChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00009E6B File Offset: 0x0000806B
		protected void OnDeactivated()
		{
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00013887 File Offset: 0x00011A87
		protected void OnOrientationChanged()
		{
			if (this.OrientationChanged != null)
			{
				this.OrientationChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00009E6B File Offset: 0x0000806B
		protected void OnPaint()
		{
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000138A2 File Offset: 0x00011AA2
		protected void OnScreenDeviceNameChanged()
		{
			if (this.ScreenDeviceNameChanged != null)
			{
				this.ScreenDeviceNameChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000BF1 RID: 3057
		protected internal abstract void SetSupportedOrientations(DisplayOrientation orientations);

		// Token: 0x06000BF2 RID: 3058
		protected abstract void SetTitle(string title);

		// Token: 0x04000566 RID: 1382
		private string _title;

		// Token: 0x04000567 RID: 1383
		[CompilerGenerated]
		private EventHandler<EventArgs> ClientSizeChanged;

		// Token: 0x04000568 RID: 1384
		[CompilerGenerated]
		private EventHandler<EventArgs> OrientationChanged;

		// Token: 0x04000569 RID: 1385
		[CompilerGenerated]
		private EventHandler<EventArgs> ScreenDeviceNameChanged;
	}
}
