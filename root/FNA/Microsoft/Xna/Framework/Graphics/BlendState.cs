using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000BF RID: 191
	public class BlendState : GraphicsResource
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00031C75 File Offset: 0x0002FE75
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x00031C82 File Offset: 0x0002FE82
		public BlendFunction AlphaBlendFunction
		{
			get
			{
				return this.state.alphaBlendFunction;
			}
			set
			{
				this.state.alphaBlendFunction = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00031C90 File Offset: 0x0002FE90
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x00031C9D File Offset: 0x0002FE9D
		public Blend AlphaDestinationBlend
		{
			get
			{
				return this.state.alphaDestinationBlend;
			}
			set
			{
				this.state.alphaDestinationBlend = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00031CAB File Offset: 0x0002FEAB
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x00031CB8 File Offset: 0x0002FEB8
		public Blend AlphaSourceBlend
		{
			get
			{
				return this.state.alphaSourceBlend;
			}
			set
			{
				this.state.alphaSourceBlend = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00031CC6 File Offset: 0x0002FEC6
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x00031CD3 File Offset: 0x0002FED3
		public BlendFunction ColorBlendFunction
		{
			get
			{
				return this.state.colorBlendFunction;
			}
			set
			{
				this.state.colorBlendFunction = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00031CE1 File Offset: 0x0002FEE1
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00031CEE File Offset: 0x0002FEEE
		public Blend ColorDestinationBlend
		{
			get
			{
				return this.state.colorDestinationBlend;
			}
			set
			{
				this.state.colorDestinationBlend = value;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00031CFC File Offset: 0x0002FEFC
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00031D09 File Offset: 0x0002FF09
		public Blend ColorSourceBlend
		{
			get
			{
				return this.state.colorSourceBlend;
			}
			set
			{
				this.state.colorSourceBlend = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00031D17 File Offset: 0x0002FF17
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00031D24 File Offset: 0x0002FF24
		public ColorWriteChannels ColorWriteChannels
		{
			get
			{
				return this.state.colorWriteEnable;
			}
			set
			{
				this.state.colorWriteEnable = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00031D32 File Offset: 0x0002FF32
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x00031D3F File Offset: 0x0002FF3F
		public ColorWriteChannels ColorWriteChannels1
		{
			get
			{
				return this.state.colorWriteEnable1;
			}
			set
			{
				this.state.colorWriteEnable1 = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00031D4D File Offset: 0x0002FF4D
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x00031D5A File Offset: 0x0002FF5A
		public ColorWriteChannels ColorWriteChannels2
		{
			get
			{
				return this.state.colorWriteEnable2;
			}
			set
			{
				this.state.colorWriteEnable2 = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00031D68 File Offset: 0x0002FF68
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x00031D75 File Offset: 0x0002FF75
		public ColorWriteChannels ColorWriteChannels3
		{
			get
			{
				return this.state.colorWriteEnable3;
			}
			set
			{
				this.state.colorWriteEnable3 = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00031D83 File Offset: 0x0002FF83
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x00031D90 File Offset: 0x0002FF90
		public Color BlendFactor
		{
			get
			{
				return this.state.blendFactor;
			}
			set
			{
				this.state.blendFactor = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00031D9E File Offset: 0x0002FF9E
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x00031DAB File Offset: 0x0002FFAB
		public int MultiSampleMask
		{
			get
			{
				return this.state.multiSampleMask;
			}
			set
			{
				this.state.multiSampleMask = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		protected internal override bool IsHarmlessToLeakInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00031DBC File Offset: 0x0002FFBC
		public BlendState()
		{
			this.AlphaBlendFunction = BlendFunction.Add;
			this.AlphaDestinationBlend = Blend.Zero;
			this.AlphaSourceBlend = Blend.One;
			this.ColorBlendFunction = BlendFunction.Add;
			this.ColorDestinationBlend = Blend.Zero;
			this.ColorSourceBlend = Blend.One;
			this.ColorWriteChannels = ColorWriteChannels.All;
			this.ColorWriteChannels1 = ColorWriteChannels.All;
			this.ColorWriteChannels2 = ColorWriteChannels.All;
			this.ColorWriteChannels3 = ColorWriteChannels.All;
			this.BlendFactor = Color.White;
			this.MultiSampleMask = -1;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00031E2B File Offset: 0x0003002B
		private BlendState(string name, Blend colorSourceBlend, Blend alphaSourceBlend, Blend colorDestBlend, Blend alphaDestBlend)
			: this()
		{
			this.Name = name;
			this.ColorSourceBlend = colorSourceBlend;
			this.AlphaSourceBlend = alphaSourceBlend;
			this.ColorDestinationBlend = colorDestBlend;
			this.AlphaDestinationBlend = alphaDestBlend;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00031E58 File Offset: 0x00030058
		// Note: this type is marked as 'beforefieldinit'.
		static BlendState()
		{
		}

		// Token: 0x040009DE RID: 2526
		public static readonly BlendState Additive = new BlendState("BlendState.Additive", Blend.SourceAlpha, Blend.SourceAlpha, Blend.One, Blend.One);

		// Token: 0x040009DF RID: 2527
		public static readonly BlendState AlphaBlend = new BlendState("BlendState.AlphaBlend", Blend.One, Blend.One, Blend.InverseSourceAlpha, Blend.InverseSourceAlpha);

		// Token: 0x040009E0 RID: 2528
		public static readonly BlendState NonPremultiplied = new BlendState("BlendState.NonPremultiplied", Blend.SourceAlpha, Blend.SourceAlpha, Blend.InverseSourceAlpha, Blend.InverseSourceAlpha);

		// Token: 0x040009E1 RID: 2529
		public static readonly BlendState Opaque = new BlendState("BlendState.Opaque", Blend.One, Blend.One, Blend.Zero, Blend.Zero);

		// Token: 0x040009E2 RID: 2530
		internal FNA3D.FNA3D_BlendState state;
	}
}
