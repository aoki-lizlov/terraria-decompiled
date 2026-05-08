using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000C4 RID: 196
	public class RasterizerState : GraphicsResource
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0003215D File Offset: 0x0003035D
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x0003216A File Offset: 0x0003036A
		public CullMode CullMode
		{
			get
			{
				return this.state.cullMode;
			}
			set
			{
				this.state.cullMode = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00032178 File Offset: 0x00030378
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x00032185 File Offset: 0x00030385
		public float DepthBias
		{
			get
			{
				return this.state.depthBias;
			}
			set
			{
				this.state.depthBias = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00032193 File Offset: 0x00030393
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x000321A0 File Offset: 0x000303A0
		public FillMode FillMode
		{
			get
			{
				return this.state.fillMode;
			}
			set
			{
				this.state.fillMode = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x000321AE File Offset: 0x000303AE
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x000321BE File Offset: 0x000303BE
		public bool MultiSampleAntiAlias
		{
			get
			{
				return this.state.multiSampleAntiAlias == 1;
			}
			set
			{
				this.state.multiSampleAntiAlias = ((value > false) ? 1 : 0);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x000321D0 File Offset: 0x000303D0
		// (set) Token: 0x060014D1 RID: 5329 RVA: 0x000321E0 File Offset: 0x000303E0
		public bool ScissorTestEnable
		{
			get
			{
				return this.state.scissorTestEnable == 1;
			}
			set
			{
				this.state.scissorTestEnable = ((value > false) ? 1 : 0);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x000321F2 File Offset: 0x000303F2
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x000321FF File Offset: 0x000303FF
		public float SlopeScaleDepthBias
		{
			get
			{
				return this.state.slopeScaleDepthBias;
			}
			set
			{
				this.state.slopeScaleDepthBias = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		protected internal override bool IsHarmlessToLeakInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0003220D File Offset: 0x0003040D
		public RasterizerState()
		{
			this.CullMode = CullMode.CullCounterClockwiseFace;
			this.FillMode = FillMode.Solid;
			this.DepthBias = 0f;
			this.MultiSampleAntiAlias = true;
			this.ScissorTestEnable = false;
			this.SlopeScaleDepthBias = 0f;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00032247 File Offset: 0x00030447
		private RasterizerState(string name, CullMode cullMode)
			: this()
		{
			this.Name = name;
			this.CullMode = cullMode;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0003225D File Offset: 0x0003045D
		// Note: this type is marked as 'beforefieldinit'.
		static RasterizerState()
		{
		}

		// Token: 0x040009F7 RID: 2551
		public static readonly RasterizerState CullClockwise = new RasterizerState("RasterizerState.CullClockwise", CullMode.CullClockwiseFace);

		// Token: 0x040009F8 RID: 2552
		public static readonly RasterizerState CullCounterClockwise = new RasterizerState("RasterizerState.CullCounterClockwise", CullMode.CullCounterClockwiseFace);

		// Token: 0x040009F9 RID: 2553
		public static readonly RasterizerState CullNone = new RasterizerState("RasterizerState.CullNone", CullMode.None);

		// Token: 0x040009FA RID: 2554
		internal FNA3D.FNA3D_RasterizerState state;
	}
}
