using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000090 RID: 144
	public class AlphaTestEffect : Effect, IEffectMatrices, IEffectFog
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00028FA0 File Offset: 0x000271A0
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00028FA8 File Offset: 0x000271A8
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

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00028FC0 File Offset: 0x000271C0
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x00028FC8 File Offset: 0x000271C8
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

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00028FE0 File Offset: 0x000271E0
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x00028FE8 File Offset: 0x000271E8
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

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00028FFF File Offset: 0x000271FF
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x00029007 File Offset: 0x00027207
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

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0002901E File Offset: 0x0002721E
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x00029026 File Offset: 0x00027226
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

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x0002903D File Offset: 0x0002723D
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x00029045 File Offset: 0x00027245
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

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00029069 File Offset: 0x00027269
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x00029071 File Offset: 0x00027271
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

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00029089 File Offset: 0x00027289
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x00029091 File Offset: 0x00027291
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x000290A9 File Offset: 0x000272A9
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x000290B6 File Offset: 0x000272B6
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000290C4 File Offset: 0x000272C4
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x000290D1 File Offset: 0x000272D1
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

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000290DF File Offset: 0x000272DF
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x000290E7 File Offset: 0x000272E7
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

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0002910B File Offset: 0x0002730B
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x00029113 File Offset: 0x00027313
		public CompareFunction AlphaFunction
		{
			get
			{
				return this.alphaFunction;
			}
			set
			{
				this.alphaFunction = value;
				this.dirtyFlags |= EffectDirtyFlags.AlphaTest;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0002912B File Offset: 0x0002732B
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00029133 File Offset: 0x00027333
		public int ReferenceAlpha
		{
			get
			{
				return this.referenceAlpha;
			}
			set
			{
				this.referenceAlpha = value;
				this.dirtyFlags |= EffectDirtyFlags.AlphaTest;
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0002914C File Offset: 0x0002734C
		public AlphaTestEffect(GraphicsDevice device)
			: base(device, Resources.AlphaTestEffect)
		{
			this.CacheEffectParameters();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000291BC File Offset: 0x000273BC
		protected AlphaTestEffect(AlphaTestEffect cloneSource)
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
			this.alphaFunction = cloneSource.alphaFunction;
			this.referenceAlpha = cloneSource.referenceAlpha;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000292AA File Offset: 0x000274AA
		public override Effect Clone()
		{
			return new AlphaTestEffect(this);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x000292B4 File Offset: 0x000274B4
		private void CacheEffectParameters()
		{
			this.textureParam = base.Parameters["Texture"];
			this.diffuseColorParam = base.Parameters["DiffuseColor"];
			this.alphaTestParam = base.Parameters["AlphaTest"];
			this.fogColorParam = base.Parameters["FogColor"];
			this.fogVectorParam = base.Parameters["FogVector"];
			this.worldViewProjParam = base.Parameters["WorldViewProj"];
			this.shaderIndexParam = base.Parameters["ShaderIndex"];
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0002935C File Offset: 0x0002755C
		protected internal override void OnApply()
		{
			this.dirtyFlags = EffectHelpers.SetWorldViewProjAndFog(this.dirtyFlags, ref this.world, ref this.view, ref this.projection, ref this.worldView, this.fogEnabled, this.fogStart, this.fogEnd, this.worldViewProjParam, this.fogVectorParam);
			if ((this.dirtyFlags & EffectDirtyFlags.MaterialColor) != (EffectDirtyFlags)0)
			{
				this.diffuseColorParam.SetValue(new Vector4(this.diffuseColor * this.alpha, this.alpha));
				this.dirtyFlags &= ~EffectDirtyFlags.MaterialColor;
			}
			if ((this.dirtyFlags & EffectDirtyFlags.AlphaTest) != (EffectDirtyFlags)0)
			{
				Vector4 vector = default(Vector4);
				bool flag = false;
				float num = (float)this.referenceAlpha / 255f;
				switch (this.alphaFunction)
				{
				case CompareFunction.Never:
					vector.Z = -1f;
					vector.W = -1f;
					goto IL_021D;
				case CompareFunction.Less:
					vector.X = num - 0.0019607844f;
					vector.Z = 1f;
					vector.W = -1f;
					goto IL_021D;
				case CompareFunction.LessEqual:
					vector.X = num + 0.0019607844f;
					vector.Z = 1f;
					vector.W = -1f;
					goto IL_021D;
				case CompareFunction.Equal:
					vector.X = num;
					vector.Y = 0.0019607844f;
					vector.Z = 1f;
					vector.W = -1f;
					flag = true;
					goto IL_021D;
				case CompareFunction.GreaterEqual:
					vector.X = num - 0.0019607844f;
					vector.Z = -1f;
					vector.W = 1f;
					goto IL_021D;
				case CompareFunction.Greater:
					vector.X = num + 0.0019607844f;
					vector.Z = -1f;
					vector.W = 1f;
					goto IL_021D;
				case CompareFunction.NotEqual:
					vector.X = num;
					vector.Y = 0.0019607844f;
					vector.Z = -1f;
					vector.W = 1f;
					flag = true;
					goto IL_021D;
				}
				vector.Z = 1f;
				vector.W = 1f;
				IL_021D:
				this.alphaTestParam.SetValue(vector);
				this.dirtyFlags &= ~EffectDirtyFlags.AlphaTest;
				if (this.isEqNe != flag)
				{
					this.isEqNe = flag;
					this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
				}
			}
			if ((this.dirtyFlags & EffectDirtyFlags.ShaderIndex) != (EffectDirtyFlags)0)
			{
				int num2 = 0;
				if (!this.fogEnabled)
				{
					num2++;
				}
				if (this.vertexColorEnabled)
				{
					num2 += 2;
				}
				if (this.isEqNe)
				{
					num2 += 4;
				}
				this.shaderIndexParam.SetValue(num2);
				this.dirtyFlags &= ~EffectDirtyFlags.ShaderIndex;
			}
		}

		// Token: 0x0400081D RID: 2077
		private EffectParameter textureParam;

		// Token: 0x0400081E RID: 2078
		private EffectParameter diffuseColorParam;

		// Token: 0x0400081F RID: 2079
		private EffectParameter alphaTestParam;

		// Token: 0x04000820 RID: 2080
		private EffectParameter fogColorParam;

		// Token: 0x04000821 RID: 2081
		private EffectParameter fogVectorParam;

		// Token: 0x04000822 RID: 2082
		private EffectParameter worldViewProjParam;

		// Token: 0x04000823 RID: 2083
		private EffectParameter shaderIndexParam;

		// Token: 0x04000824 RID: 2084
		private bool fogEnabled;

		// Token: 0x04000825 RID: 2085
		private bool vertexColorEnabled;

		// Token: 0x04000826 RID: 2086
		private Matrix world = Matrix.Identity;

		// Token: 0x04000827 RID: 2087
		private Matrix view = Matrix.Identity;

		// Token: 0x04000828 RID: 2088
		private Matrix projection = Matrix.Identity;

		// Token: 0x04000829 RID: 2089
		private Matrix worldView;

		// Token: 0x0400082A RID: 2090
		private Vector3 diffuseColor = Vector3.One;

		// Token: 0x0400082B RID: 2091
		private float alpha = 1f;

		// Token: 0x0400082C RID: 2092
		private float fogStart;

		// Token: 0x0400082D RID: 2093
		private float fogEnd = 1f;

		// Token: 0x0400082E RID: 2094
		private CompareFunction alphaFunction = CompareFunction.Greater;

		// Token: 0x0400082F RID: 2095
		private int referenceAlpha;

		// Token: 0x04000830 RID: 2096
		private bool isEqNe;

		// Token: 0x04000831 RID: 2097
		private EffectDirtyFlags dirtyFlags = EffectDirtyFlags.All;
	}
}
