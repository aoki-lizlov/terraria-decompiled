using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000C2 RID: 194
	public class DepthStencilState : GraphicsResource
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00031EB1 File Offset: 0x000300B1
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x00031EC1 File Offset: 0x000300C1
		public bool DepthBufferEnable
		{
			get
			{
				return this.state.depthBufferEnable == 1;
			}
			set
			{
				this.state.depthBufferEnable = ((value > false) ? 1 : 0);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x00031ED3 File Offset: 0x000300D3
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x00031EE3 File Offset: 0x000300E3
		public bool DepthBufferWriteEnable
		{
			get
			{
				return this.state.depthBufferWriteEnable == 1;
			}
			set
			{
				this.state.depthBufferWriteEnable = ((value > false) ? 1 : 0);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00031EF5 File Offset: 0x000300F5
		// (set) Token: 0x060014A9 RID: 5289 RVA: 0x00031F02 File Offset: 0x00030102
		public StencilOperation CounterClockwiseStencilDepthBufferFail
		{
			get
			{
				return this.state.ccwStencilDepthBufferFail;
			}
			set
			{
				this.state.ccwStencilDepthBufferFail = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x00031F10 File Offset: 0x00030110
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x00031F1D File Offset: 0x0003011D
		public StencilOperation CounterClockwiseStencilFail
		{
			get
			{
				return this.state.ccwStencilFail;
			}
			set
			{
				this.state.ccwStencilFail = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x00031F2B File Offset: 0x0003012B
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x00031F38 File Offset: 0x00030138
		public CompareFunction CounterClockwiseStencilFunction
		{
			get
			{
				return this.state.ccwStencilFunction;
			}
			set
			{
				this.state.ccwStencilFunction = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00031F46 File Offset: 0x00030146
		// (set) Token: 0x060014AF RID: 5295 RVA: 0x00031F53 File Offset: 0x00030153
		public StencilOperation CounterClockwiseStencilPass
		{
			get
			{
				return this.state.ccwStencilPass;
			}
			set
			{
				this.state.ccwStencilPass = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00031F61 File Offset: 0x00030161
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x00031F6E File Offset: 0x0003016E
		public CompareFunction DepthBufferFunction
		{
			get
			{
				return this.state.depthBufferFunction;
			}
			set
			{
				this.state.depthBufferFunction = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x00031F7C File Offset: 0x0003017C
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x00031F89 File Offset: 0x00030189
		public int ReferenceStencil
		{
			get
			{
				return this.state.referenceStencil;
			}
			set
			{
				this.state.referenceStencil = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00031F97 File Offset: 0x00030197
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00031FA4 File Offset: 0x000301A4
		public StencilOperation StencilDepthBufferFail
		{
			get
			{
				return this.state.stencilDepthBufferFail;
			}
			set
			{
				this.state.stencilDepthBufferFail = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00031FB2 File Offset: 0x000301B2
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x00031FC2 File Offset: 0x000301C2
		public bool StencilEnable
		{
			get
			{
				return this.state.stencilEnable == 1;
			}
			set
			{
				this.state.stencilEnable = ((value > false) ? 1 : 0);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00031FD4 File Offset: 0x000301D4
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x00031FE1 File Offset: 0x000301E1
		public StencilOperation StencilFail
		{
			get
			{
				return this.state.stencilFail;
			}
			set
			{
				this.state.stencilFail = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00031FEF File Offset: 0x000301EF
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x00031FFC File Offset: 0x000301FC
		public CompareFunction StencilFunction
		{
			get
			{
				return this.state.stencilFunction;
			}
			set
			{
				this.state.stencilFunction = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0003200A File Offset: 0x0003020A
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x00032017 File Offset: 0x00030217
		public int StencilMask
		{
			get
			{
				return this.state.stencilMask;
			}
			set
			{
				this.state.stencilMask = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00032025 File Offset: 0x00030225
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x00032032 File Offset: 0x00030232
		public StencilOperation StencilPass
		{
			get
			{
				return this.state.stencilPass;
			}
			set
			{
				this.state.stencilPass = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00032040 File Offset: 0x00030240
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x0003204D File Offset: 0x0003024D
		public int StencilWriteMask
		{
			get
			{
				return this.state.stencilWriteMask;
			}
			set
			{
				this.state.stencilWriteMask = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0003205B File Offset: 0x0003025B
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x0003206B File Offset: 0x0003026B
		public bool TwoSidedStencilMode
		{
			get
			{
				return this.state.twoSidedStencilMode == 1;
			}
			set
			{
				this.state.twoSidedStencilMode = ((value > false) ? 1 : 0);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		protected internal override bool IsHarmlessToLeakInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00032080 File Offset: 0x00030280
		public DepthStencilState()
		{
			this.DepthBufferEnable = true;
			this.DepthBufferWriteEnable = true;
			this.DepthBufferFunction = CompareFunction.LessEqual;
			this.StencilEnable = false;
			this.StencilFunction = CompareFunction.Always;
			this.StencilPass = StencilOperation.Keep;
			this.StencilFail = StencilOperation.Keep;
			this.StencilDepthBufferFail = StencilOperation.Keep;
			this.TwoSidedStencilMode = false;
			this.CounterClockwiseStencilFunction = CompareFunction.Always;
			this.CounterClockwiseStencilFail = StencilOperation.Keep;
			this.CounterClockwiseStencilPass = StencilOperation.Keep;
			this.CounterClockwiseStencilDepthBufferFail = StencilOperation.Keep;
			this.StencilMask = int.MaxValue;
			this.StencilWriteMask = int.MaxValue;
			this.ReferenceStencil = 0;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0003210B File Offset: 0x0003030B
		private DepthStencilState(string name, bool depthBufferEnable, bool depthBufferWriteEnable)
			: this()
		{
			this.Name = name;
			this.DepthBufferEnable = depthBufferEnable;
			this.DepthBufferWriteEnable = depthBufferWriteEnable;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00032128 File Offset: 0x00030328
		// Note: this type is marked as 'beforefieldinit'.
		static DepthStencilState()
		{
		}

		// Token: 0x040009F0 RID: 2544
		public static readonly DepthStencilState Default = new DepthStencilState("DepthStencilState.Default", true, true);

		// Token: 0x040009F1 RID: 2545
		public static readonly DepthStencilState DepthRead = new DepthStencilState("DepthStencilState.DepthRead", true, false);

		// Token: 0x040009F2 RID: 2546
		public static readonly DepthStencilState None = new DepthStencilState("DepthStencilState.None", false, false);

		// Token: 0x040009F3 RID: 2547
		internal FNA3D.FNA3D_DepthStencilState state;
	}
}
