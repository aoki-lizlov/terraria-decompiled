using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	public class PresentationParameters
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0002ED37 File Offset: 0x0002CF37
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x0002ED44 File Offset: 0x0002CF44
		public SurfaceFormat BackBufferFormat
		{
			get
			{
				return this.parameters.backBufferFormat;
			}
			set
			{
				this.parameters.backBufferFormat = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0002ED52 File Offset: 0x0002CF52
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x0002ED5F File Offset: 0x0002CF5F
		public int BackBufferHeight
		{
			get
			{
				return this.parameters.backBufferHeight;
			}
			set
			{
				this.parameters.backBufferHeight = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0002ED6D File Offset: 0x0002CF6D
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x0002ED7A File Offset: 0x0002CF7A
		public int BackBufferWidth
		{
			get
			{
				return this.parameters.backBufferWidth;
			}
			set
			{
				this.parameters.backBufferWidth = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0002ED88 File Offset: 0x0002CF88
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(0, 0, this.BackBufferWidth, this.BackBufferHeight);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0002ED9D File Offset: 0x0002CF9D
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x0002EDAA File Offset: 0x0002CFAA
		public IntPtr DeviceWindowHandle
		{
			get
			{
				return this.parameters.deviceWindowHandle;
			}
			set
			{
				this.parameters.deviceWindowHandle = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0002EDB8 File Offset: 0x0002CFB8
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x0002EDC5 File Offset: 0x0002CFC5
		public DepthFormat DepthStencilFormat
		{
			get
			{
				return this.parameters.depthStencilFormat;
			}
			set
			{
				this.parameters.depthStencilFormat = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0002EDD3 File Offset: 0x0002CFD3
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x0002EDE3 File Offset: 0x0002CFE3
		public bool IsFullScreen
		{
			get
			{
				return this.parameters.isFullScreen == 1;
			}
			set
			{
				this.parameters.isFullScreen = ((value > false) ? 1 : 0);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0002EDF5 File Offset: 0x0002CFF5
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x0002EE02 File Offset: 0x0002D002
		public int MultiSampleCount
		{
			get
			{
				return this.parameters.multiSampleCount;
			}
			set
			{
				this.parameters.multiSampleCount = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0002EE10 File Offset: 0x0002D010
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x0002EE1D File Offset: 0x0002D01D
		public PresentInterval PresentationInterval
		{
			get
			{
				return this.parameters.presentationInterval;
			}
			set
			{
				this.parameters.presentationInterval = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0002EE2B File Offset: 0x0002D02B
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x0002EE38 File Offset: 0x0002D038
		public DisplayOrientation DisplayOrientation
		{
			get
			{
				return this.parameters.displayOrientation;
			}
			set
			{
				this.parameters.displayOrientation = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0002EE46 File Offset: 0x0002D046
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x0002EE53 File Offset: 0x0002D053
		public RenderTargetUsage RenderTargetUsage
		{
			get
			{
				return this.parameters.renderTargetUsage;
			}
			set
			{
				this.parameters.renderTargetUsage = value;
			}
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0002EE64 File Offset: 0x0002D064
		public PresentationParameters()
		{
			this.BackBufferFormat = SurfaceFormat.Color;
			this.BackBufferWidth = GraphicsDeviceManager.DefaultBackBufferWidth;
			this.BackBufferHeight = GraphicsDeviceManager.DefaultBackBufferHeight;
			this.DeviceWindowHandle = IntPtr.Zero;
			this.IsFullScreen = false;
			this.DepthStencilFormat = DepthFormat.None;
			this.MultiSampleCount = 0;
			this.PresentationInterval = PresentInterval.Default;
			this.DisplayOrientation = DisplayOrientation.Default;
			this.RenderTargetUsage = RenderTargetUsage.DiscardContents;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0002EECC File Offset: 0x0002D0CC
		public PresentationParameters Clone()
		{
			return new PresentationParameters
			{
				BackBufferFormat = this.BackBufferFormat,
				BackBufferHeight = this.BackBufferHeight,
				BackBufferWidth = this.BackBufferWidth,
				DeviceWindowHandle = this.DeviceWindowHandle,
				IsFullScreen = this.IsFullScreen,
				DepthStencilFormat = this.DepthStencilFormat,
				MultiSampleCount = this.MultiSampleCount,
				PresentationInterval = this.PresentationInterval,
				DisplayOrientation = this.DisplayOrientation,
				RenderTargetUsage = this.RenderTargetUsage
			};
		}

		// Token: 0x0400094A RID: 2378
		internal FNA3D.FNA3D_PresentationParameters parameters;
	}
}
