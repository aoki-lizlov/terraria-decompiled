using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000092 RID: 146
	public class DualTextureEffect : Effect, IEffectMatrices, IEffectFog
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00029E08 File Offset: 0x00028008
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x00029E10 File Offset: 0x00028010
		public Matrix World
		{
			get
			{
				return this.world;
			}
			set
			{
				this.world = value;
				this.dirtyFlags |= EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.Fog;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00029E28 File Offset: 0x00028028
		// (set) Token: 0x0600122E RID: 4654 RVA: 0x00029E30 File Offset: 0x00028030
		public Matrix View
		{
			get
			{
				return this.view;
			}
			set
			{
				this.view = value;
				this.dirtyFlags |= EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.Fog;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00029E48 File Offset: 0x00028048
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x00029E50 File Offset: 0x00028050
		public Matrix Projection
		{
			get
			{
				return this.projection;
			}
			set
			{
				this.projection = value;
				this.dirtyFlags |= EffectDirtyFlags.WorldViewProj;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00029E67 File Offset: 0x00028067
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x00029E6F File Offset: 0x0002806F
		public Vector3 DiffuseColor
		{
			get
			{
				return this.diffuseColor;
			}
			set
			{
				this.diffuseColor = value;
				this.dirtyFlags |= EffectDirtyFlags.MaterialColor;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00029E86 File Offset: 0x00028086
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x00029E8E File Offset: 0x0002808E
		public float Alpha
		{
			get
			{
				return this.alpha;
			}
			set
			{
				this.alpha = value;
				this.dirtyFlags |= EffectDirtyFlags.MaterialColor;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00029EA5 File Offset: 0x000280A5
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x00029EAD File Offset: 0x000280AD
		public bool FogEnabled
		{
			get
			{
				return this.fogEnabled;
			}
			set
			{
				if (this.fogEnabled != value)
				{
					this.fogEnabled = value;
					this.dirtyFlags |= EffectDirtyFlags.FogEnable | EffectDirtyFlags.ShaderIndex;
				}
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00029ED1 File Offset: 0x000280D1
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x00029ED9 File Offset: 0x000280D9
		public float FogStart
		{
			get
			{
				return this.fogStart;
			}
			set
			{
				this.fogStart = value;
				this.dirtyFlags |= EffectDirtyFlags.Fog;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00029EF1 File Offset: 0x000280F1
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x00029EF9 File Offset: 0x000280F9
		public float FogEnd
		{
			get
			{
				return this.fogEnd;
			}
			set
			{
				this.fogEnd = value;
				this.dirtyFlags |= EffectDirtyFlags.Fog;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00029F11 File Offset: 0x00028111
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x00029F1E File Offset: 0x0002811E
		public Vector3 FogColor
		{
			get
			{
				return this.fogColorParam.GetValueVector3();
			}
			set
			{
				this.fogColorParam.SetValue(value);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x00029F2C File Offset: 0x0002812C
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x00029F39 File Offset: 0x00028139
		public Texture2D Texture
		{
			get
			{
				return this.textureParam.GetValueTexture2D();
			}
			set
			{
				this.textureParam.SetValue(value);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00029F47 File Offset: 0x00028147
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x00029F54 File Offset: 0x00028154
		public Texture2D Texture2
		{
			get
			{
				return this.texture2Param.GetValueTexture2D();
			}
			set
			{
				this.texture2Param.SetValue(value);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00029F62 File Offset: 0x00028162
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x00029F6A File Offset: 0x0002816A
		public bool VertexColorEnabled
		{
			get
			{
				return this.vertexColorEnabled;
			}
			set
			{
				if (this.vertexColorEnabled != value)
				{
					this.vertexColorEnabled = value;
					this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
				}
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00029F90 File Offset: 0x00028190
		public DualTextureEffect(GraphicsDevice device)
			: base(device, Resources.DualTextureEffect)
		{
			this.CacheEffectParameters();
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00029FF8 File Offset: 0x000281F8
		protected DualTextureEffect(DualTextureEffect cloneSource)
			: base(cloneSource)
		{
			this.CacheEffectParameters();
			this.fogEnabled = cloneSource.fogEnabled;
			this.vertexColorEnabled = cloneSource.vertexColorEnabled;
			this.world = cloneSource.world;
			this.view = cloneSource.view;
			this.projection = cloneSource.projection;
			this.diffuseColor = cloneSource.diffuseColor;
			this.alpha = cloneSource.alpha;
			this.fogStart = cloneSource.fogStart;
			this.fogEnd = cloneSource.fogEnd;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0002A0C7 File Offset: 0x000282C7
		public override Effect Clone()
		{
			return new DualTextureEffect(this);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0002A0D0 File Offset: 0x000282D0
		private void CacheEffectParameters()
		{
			this.textureParam = base.Parameters["Texture"];
			this.texture2Param = base.Parameters["Texture2"];
			this.diffuseColorParam = base.Parameters["DiffuseColor"];
			this.fogColorParam = base.Parameters["FogColor"];
			this.fogVectorParam = base.Parameters["FogVector"];
			this.worldViewProjParam = base.Parameters["WorldViewProj"];
			this.shaderIndexParam = base.Parameters["ShaderIndex"];
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0002A178 File Offset: 0x00028378
		protected internal override void OnApply()
		{
			this.dirtyFlags = EffectHelpers.SetWorldViewProjAndFog(this.dirtyFlags, ref this.world, ref this.view, ref this.projection, ref this.worldView, this.fogEnabled, this.fogStart, this.fogEnd, this.worldViewProjParam, this.fogVectorParam);
			if ((this.dirtyFlags & EffectDirtyFlags.MaterialColor) != (EffectDirtyFlags)0)
			{
				this.diffuseColorParam.SetValue(new Vector4(this.diffuseColor * this.alpha, this.alpha));
				this.dirtyFlags &= ~EffectDirtyFlags.MaterialColor;
			}
			if ((this.dirtyFlags & EffectDirtyFlags.ShaderIndex) != (EffectDirtyFlags)0)
			{
				int num = 0;
				if (!this.fogEnabled)
				{
					num++;
				}
				if (this.vertexColorEnabled)
				{
					num += 2;
				}
				this.shaderIndexParam.SetValue(num);
				this.dirtyFlags &= ~EffectDirtyFlags.ShaderIndex;
			}
		}

		// Token: 0x04000852 RID: 2130
		private EffectParameter textureParam;

		// Token: 0x04000853 RID: 2131
		private EffectParameter texture2Param;

		// Token: 0x04000854 RID: 2132
		private EffectParameter diffuseColorParam;

		// Token: 0x04000855 RID: 2133
		private EffectParameter fogColorParam;

		// Token: 0x04000856 RID: 2134
		private EffectParameter fogVectorParam;

		// Token: 0x04000857 RID: 2135
		private EffectParameter worldViewProjParam;

		// Token: 0x04000858 RID: 2136
		private EffectParameter shaderIndexParam;

		// Token: 0x04000859 RID: 2137
		private bool fogEnabled;

		// Token: 0x0400085A RID: 2138
		private bool vertexColorEnabled;

		// Token: 0x0400085B RID: 2139
		private Matrix world = Matrix.Identity;

		// Token: 0x0400085C RID: 2140
		private Matrix view = Matrix.Identity;

		// Token: 0x0400085D RID: 2141
		private Matrix projection = Matrix.Identity;

		// Token: 0x0400085E RID: 2142
		private Matrix worldView;

		// Token: 0x0400085F RID: 2143
		private Vector3 diffuseColor = Vector3.One;

		// Token: 0x04000860 RID: 2144
		private float alpha = 1f;

		// Token: 0x04000861 RID: 2145
		private float fogStart;

		// Token: 0x04000862 RID: 2146
		private float fogEnd = 1f;

		// Token: 0x04000863 RID: 2147
		private EffectDirtyFlags dirtyFlags = EffectDirtyFlags.All;
	}
}
