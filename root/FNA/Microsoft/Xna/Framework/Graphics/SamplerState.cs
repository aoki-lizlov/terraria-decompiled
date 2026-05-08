using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000C5 RID: 197
	public class SamplerState : GraphicsResource
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0003228F File Offset: 0x0003048F
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x0003229C File Offset: 0x0003049C
		public TextureAddressMode AddressU
		{
			get
			{
				return this.state.addressU;
			}
			set
			{
				this.state.addressU = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x000322AA File Offset: 0x000304AA
		// (set) Token: 0x060014DB RID: 5339 RVA: 0x000322B7 File Offset: 0x000304B7
		public TextureAddressMode AddressV
		{
			get
			{
				return this.state.addressV;
			}
			set
			{
				this.state.addressV = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x000322C5 File Offset: 0x000304C5
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x000322D2 File Offset: 0x000304D2
		public TextureAddressMode AddressW
		{
			get
			{
				return this.state.addressW;
			}
			set
			{
				this.state.addressW = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x000322E0 File Offset: 0x000304E0
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x000322ED File Offset: 0x000304ED
		public TextureFilter Filter
		{
			get
			{
				return this.state.filter;
			}
			set
			{
				this.state.filter = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x000322FB File Offset: 0x000304FB
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x00032308 File Offset: 0x00030508
		public int MaxAnisotropy
		{
			get
			{
				return this.state.maxAnisotropy;
			}
			set
			{
				this.state.maxAnisotropy = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00032316 File Offset: 0x00030516
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x00032323 File Offset: 0x00030523
		public int MaxMipLevel
		{
			get
			{
				return this.state.maxMipLevel;
			}
			set
			{
				this.state.maxMipLevel = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00032331 File Offset: 0x00030531
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0003233E File Offset: 0x0003053E
		public float MipMapLevelOfDetailBias
		{
			get
			{
				return this.state.mipMapLevelOfDetailBias;
			}
			set
			{
				this.state.mipMapLevelOfDetailBias = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		protected internal override bool IsHarmlessToLeakInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0003234C File Offset: 0x0003054C
		public SamplerState()
		{
			this.Filter = TextureFilter.Linear;
			this.AddressU = TextureAddressMode.Wrap;
			this.AddressV = TextureAddressMode.Wrap;
			this.AddressW = TextureAddressMode.Wrap;
			this.MaxAnisotropy = 4;
			this.MaxMipLevel = 0;
			this.MipMapLevelOfDetailBias = 0f;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00032389 File Offset: 0x00030589
		private SamplerState(string name, TextureFilter filter, TextureAddressMode addressU, TextureAddressMode addressV, TextureAddressMode addressW)
			: this()
		{
			this.Name = name;
			this.Filter = filter;
			this.AddressU = addressU;
			this.AddressV = addressV;
			this.AddressW = addressW;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x000323B8 File Offset: 0x000305B8
		// Note: this type is marked as 'beforefieldinit'.
		static SamplerState()
		{
		}

		// Token: 0x040009FB RID: 2555
		public static readonly SamplerState AnisotropicClamp = new SamplerState("SamplerState.AnisotropicClamp", TextureFilter.Anisotropic, TextureAddressMode.Clamp, TextureAddressMode.Clamp, TextureAddressMode.Clamp);

		// Token: 0x040009FC RID: 2556
		public static readonly SamplerState AnisotropicWrap = new SamplerState("SamplerState.AnisotropicWrap", TextureFilter.Anisotropic, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap);

		// Token: 0x040009FD RID: 2557
		public static readonly SamplerState LinearClamp = new SamplerState("SamplerState.LinearClamp", TextureFilter.Linear, TextureAddressMode.Clamp, TextureAddressMode.Clamp, TextureAddressMode.Clamp);

		// Token: 0x040009FE RID: 2558
		public static readonly SamplerState LinearWrap = new SamplerState("SamplerState.LinearWrap", TextureFilter.Linear, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap);

		// Token: 0x040009FF RID: 2559
		public static readonly SamplerState PointClamp = new SamplerState("SamplerState.PointClamp", TextureFilter.Point, TextureAddressMode.Clamp, TextureAddressMode.Clamp, TextureAddressMode.Clamp);

		// Token: 0x04000A00 RID: 2560
		public static readonly SamplerState PointWrap = new SamplerState("SamplerState.PointWrap", TextureFilter.Point, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap);

		// Token: 0x04000A01 RID: 2561
		internal FNA3D.FNA3D_SamplerState state;
	}
}
