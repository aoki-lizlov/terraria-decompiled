using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000091 RID: 145
	public class BasicEffect : Effect, IEffectMatrices, IEffectLights, IEffectFog
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0002961D File Offset: 0x0002781D
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x00029625 File Offset: 0x00027825
		public Matrix World
		{
			get
			{
				return this.world;
			}
			set
			{
				this.world = value;
				this.dirtyFlags |= EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.World | EffectDirtyFlags.Fog;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0002963D File Offset: 0x0002783D
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x00029645 File Offset: 0x00027845
		public Matrix View
		{
			get
			{
				return this.view;
			}
			set
			{
				this.view = value;
				this.dirtyFlags |= EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.EyePosition | EffectDirtyFlags.Fog;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0002965D File Offset: 0x0002785D
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x00029665 File Offset: 0x00027865
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

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0002967C File Offset: 0x0002787C
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x00029684 File Offset: 0x00027884
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

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0002969B File Offset: 0x0002789B
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x000296A3 File Offset: 0x000278A3
		public Vector3 EmissiveColor
		{
			get
			{
				return this.emissiveColor;
			}
			set
			{
				this.emissiveColor = value;
				this.dirtyFlags |= EffectDirtyFlags.MaterialColor;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x000296BA File Offset: 0x000278BA
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x000296C7 File Offset: 0x000278C7
		public Vector3 SpecularColor
		{
			get
			{
				return this.specularColorParam.GetValueVector3();
			}
			set
			{
				this.specularColorParam.SetValue(value);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x000296D5 File Offset: 0x000278D5
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x000296E2 File Offset: 0x000278E2
		public float SpecularPower
		{
			get
			{
				return this.specularPowerParam.GetValueSingle();
			}
			set
			{
				this.specularPowerParam.SetValue(value);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x000296F0 File Offset: 0x000278F0
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x000296F8 File Offset: 0x000278F8
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

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0002970F File Offset: 0x0002790F
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x00029717 File Offset: 0x00027917
		public bool LightingEnabled
		{
			get
			{
				return this.lightingEnabled;
			}
			set
			{
				if (this.lightingEnabled != value)
				{
					this.lightingEnabled = value;
					this.dirtyFlags |= EffectDirtyFlags.MaterialColor | EffectDirtyFlags.ShaderIndex;
				}
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0002973B File Offset: 0x0002793B
		// (set) Token: 0x06001211 RID: 4625 RVA: 0x00029743 File Offset: 0x00027943
		public bool PreferPerPixelLighting
		{
			get
			{
				return this.preferPerPixelLighting;
			}
			set
			{
				if (this.preferPerPixelLighting != value)
				{
					this.preferPerPixelLighting = value;
					this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
				}
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00029767 File Offset: 0x00027967
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x0002976F File Offset: 0x0002796F
		public Vector3 AmbientLightColor
		{
			get
			{
				return this.ambientLightColor;
			}
			set
			{
				this.ambientLightColor = value;
				this.dirtyFlags |= EffectDirtyFlags.MaterialColor;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00029786 File Offset: 0x00027986
		public DirectionalLight DirectionalLight0
		{
			get
			{
				return this.light0;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0002978E File Offset: 0x0002798E
		public DirectionalLight DirectionalLight1
		{
			get
			{
				return this.light1;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00029796 File Offset: 0x00027996
		public DirectionalLight DirectionalLight2
		{
			get
			{
				return this.light2;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0002979E File Offset: 0x0002799E
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x000297A6 File Offset: 0x000279A6
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

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x000297CA File Offset: 0x000279CA
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x000297D2 File Offset: 0x000279D2
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

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x000297EA File Offset: 0x000279EA
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x000297F2 File Offset: 0x000279F2
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0002980A File Offset: 0x00027A0A
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x00029817 File Offset: 0x00027A17
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00029825 File Offset: 0x00027A25
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0002982D File Offset: 0x00027A2D
		public bool TextureEnabled
		{
			get
			{
				return this.textureEnabled;
			}
			set
			{
				if (this.textureEnabled != value)
				{
					this.textureEnabled = value;
					this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
				}
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00029851 File Offset: 0x00027A51
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x0002985E File Offset: 0x00027A5E
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

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0002986C File Offset: 0x00027A6C
		// (set) Token: 0x06001224 RID: 4644 RVA: 0x00029874 File Offset: 0x00027A74
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

		// Token: 0x06001225 RID: 4645 RVA: 0x00029898 File Offset: 0x00027A98
		public BasicEffect(GraphicsDevice device)
			: base(device, Resources.BasicEffect)
		{
			this.CacheEffectParameters(null);
			this.DirectionalLight0.Enabled = true;
			this.SpecularColor = Vector3.One;
			this.SpecularPower = 16f;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0002993C File Offset: 0x00027B3C
		protected BasicEffect(BasicEffect cloneSource)
			: base(cloneSource)
		{
			this.CacheEffectParameters(cloneSource);
			this.lightingEnabled = cloneSource.lightingEnabled;
			this.preferPerPixelLighting = cloneSource.preferPerPixelLighting;
			this.fogEnabled = cloneSource.fogEnabled;
			this.textureEnabled = cloneSource.textureEnabled;
			this.vertexColorEnabled = cloneSource.vertexColorEnabled;
			this.world = cloneSource.world;
			this.view = cloneSource.view;
			this.projection = cloneSource.projection;
			this.diffuseColor = cloneSource.diffuseColor;
			this.emissiveColor = cloneSource.emissiveColor;
			this.ambientLightColor = cloneSource.ambientLightColor;
			this.alpha = cloneSource.alpha;
			this.fogStart = cloneSource.fogStart;
			this.fogEnd = cloneSource.fogEnd;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00029A5E File Offset: 0x00027C5E
		public override Effect Clone()
		{
			return new BasicEffect(this);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00029A66 File Offset: 0x00027C66
		public void EnableDefaultLighting()
		{
			this.LightingEnabled = true;
			this.AmbientLightColor = EffectHelpers.EnableDefaultLighting(this.light0, this.light1, this.light2);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00029A8C File Offset: 0x00027C8C
		private void CacheEffectParameters(BasicEffect cloneSource)
		{
			this.textureParam = base.Parameters["Texture"];
			this.diffuseColorParam = base.Parameters["DiffuseColor"];
			this.emissiveColorParam = base.Parameters["EmissiveColor"];
			this.specularColorParam = base.Parameters["SpecularColor"];
			this.specularPowerParam = base.Parameters["SpecularPower"];
			this.eyePositionParam = base.Parameters["EyePosition"];
			this.fogColorParam = base.Parameters["FogColor"];
			this.fogVectorParam = base.Parameters["FogVector"];
			this.worldParam = base.Parameters["World"];
			this.worldInverseTransposeParam = base.Parameters["WorldInverseTranspose"];
			this.worldViewProjParam = base.Parameters["WorldViewProj"];
			this.shaderIndexParam = base.Parameters["ShaderIndex"];
			this.light0 = new DirectionalLight(base.Parameters["DirLight0Direction"], base.Parameters["DirLight0DiffuseColor"], base.Parameters["DirLight0SpecularColor"], (cloneSource != null) ? cloneSource.light0 : null);
			this.light1 = new DirectionalLight(base.Parameters["DirLight1Direction"], base.Parameters["DirLight1DiffuseColor"], base.Parameters["DirLight1SpecularColor"], (cloneSource != null) ? cloneSource.light1 : null);
			this.light2 = new DirectionalLight(base.Parameters["DirLight2Direction"], base.Parameters["DirLight2DiffuseColor"], base.Parameters["DirLight2SpecularColor"], (cloneSource != null) ? cloneSource.light2 : null);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00029C78 File Offset: 0x00027E78
		protected internal override void OnApply()
		{
			this.dirtyFlags = EffectHelpers.SetWorldViewProjAndFog(this.dirtyFlags, ref this.world, ref this.view, ref this.projection, ref this.worldView, this.fogEnabled, this.fogStart, this.fogEnd, this.worldViewProjParam, this.fogVectorParam);
			if ((this.dirtyFlags & EffectDirtyFlags.MaterialColor) != (EffectDirtyFlags)0)
			{
				EffectHelpers.SetMaterialColor(this.lightingEnabled, this.alpha, ref this.diffuseColor, ref this.emissiveColor, ref this.ambientLightColor, this.diffuseColorParam, this.emissiveColorParam);
				this.dirtyFlags &= ~EffectDirtyFlags.MaterialColor;
			}
			if (this.lightingEnabled)
			{
				this.dirtyFlags = EffectHelpers.SetLightingMatrices(this.dirtyFlags, ref this.world, ref this.view, this.worldParam, this.worldInverseTransposeParam, this.eyePositionParam);
				bool flag = !this.light1.Enabled && !this.light2.Enabled;
				if (this.oneLight != flag)
				{
					this.oneLight = flag;
					this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
				}
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
				if (this.textureEnabled)
				{
					num += 4;
				}
				if (this.lightingEnabled)
				{
					if (this.preferPerPixelLighting)
					{
						num += 24;
					}
					else if (this.oneLight)
					{
						num += 16;
					}
					else
					{
						num += 8;
					}
				}
				this.shaderIndexParam.SetValue(num);
				this.dirtyFlags &= ~EffectDirtyFlags.ShaderIndex;
			}
		}

		// Token: 0x04000832 RID: 2098
		private EffectParameter textureParam;

		// Token: 0x04000833 RID: 2099
		private EffectParameter diffuseColorParam;

		// Token: 0x04000834 RID: 2100
		private EffectParameter emissiveColorParam;

		// Token: 0x04000835 RID: 2101
		private EffectParameter specularColorParam;

		// Token: 0x04000836 RID: 2102
		private EffectParameter specularPowerParam;

		// Token: 0x04000837 RID: 2103
		private EffectParameter eyePositionParam;

		// Token: 0x04000838 RID: 2104
		private EffectParameter fogColorParam;

		// Token: 0x04000839 RID: 2105
		private EffectParameter fogVectorParam;

		// Token: 0x0400083A RID: 2106
		private EffectParameter worldParam;

		// Token: 0x0400083B RID: 2107
		private EffectParameter worldInverseTransposeParam;

		// Token: 0x0400083C RID: 2108
		private EffectParameter worldViewProjParam;

		// Token: 0x0400083D RID: 2109
		private EffectParameter shaderIndexParam;

		// Token: 0x0400083E RID: 2110
		private bool lightingEnabled;

		// Token: 0x0400083F RID: 2111
		private bool preferPerPixelLighting;

		// Token: 0x04000840 RID: 2112
		private bool oneLight;

		// Token: 0x04000841 RID: 2113
		private bool fogEnabled;

		// Token: 0x04000842 RID: 2114
		private bool textureEnabled;

		// Token: 0x04000843 RID: 2115
		private bool vertexColorEnabled;

		// Token: 0x04000844 RID: 2116
		private Matrix world = Matrix.Identity;

		// Token: 0x04000845 RID: 2117
		private Matrix view = Matrix.Identity;

		// Token: 0x04000846 RID: 2118
		private Matrix projection = Matrix.Identity;

		// Token: 0x04000847 RID: 2119
		private Matrix worldView;

		// Token: 0x04000848 RID: 2120
		private Vector3 diffuseColor = Vector3.One;

		// Token: 0x04000849 RID: 2121
		private Vector3 emissiveColor = Vector3.Zero;

		// Token: 0x0400084A RID: 2122
		private Vector3 ambientLightColor = Vector3.Zero;

		// Token: 0x0400084B RID: 2123
		private float alpha = 1f;

		// Token: 0x0400084C RID: 2124
		private DirectionalLight light0;

		// Token: 0x0400084D RID: 2125
		private DirectionalLight light1;

		// Token: 0x0400084E RID: 2126
		private DirectionalLight light2;

		// Token: 0x0400084F RID: 2127
		private float fogStart;

		// Token: 0x04000850 RID: 2128
		private float fogEnd = 1f;

		// Token: 0x04000851 RID: 2129
		private EffectDirtyFlags dirtyFlags = EffectDirtyFlags.All;
	}
}
