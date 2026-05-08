using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000095 RID: 149
	public class EnvironmentMapEffect : Effect, IEffectMatrices, IEffectLights, IEffectFog
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0002A5D9 File Offset: 0x000287D9
		// (set) Token: 0x0600124E RID: 4686 RVA: 0x0002A5E1 File Offset: 0x000287E1
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

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0002A5F9 File Offset: 0x000287F9
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x0002A601 File Offset: 0x00028801
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

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0002A619 File Offset: 0x00028819
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x0002A621 File Offset: 0x00028821
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

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0002A638 File Offset: 0x00028838
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x0002A640 File Offset: 0x00028840
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

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0002A657 File Offset: 0x00028857
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x0002A65F File Offset: 0x0002885F
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

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0002A676 File Offset: 0x00028876
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x0002A67E File Offset: 0x0002887E
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

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0002A695 File Offset: 0x00028895
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x0002A69D File Offset: 0x0002889D
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

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0002A6B4 File Offset: 0x000288B4
		public DirectionalLight DirectionalLight0
		{
			get
			{
				return this.light0;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0002A6BC File Offset: 0x000288BC
		public DirectionalLight DirectionalLight1
		{
			get
			{
				return this.light1;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0002A6C4 File Offset: 0x000288C4
		public DirectionalLight DirectionalLight2
		{
			get
			{
				return this.light2;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0002A6CC File Offset: 0x000288CC
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x0002A6D4 File Offset: 0x000288D4
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

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0002A6F8 File Offset: 0x000288F8
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x0002A700 File Offset: 0x00028900
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0002A718 File Offset: 0x00028918
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x0002A720 File Offset: 0x00028920
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

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0002A738 File Offset: 0x00028938
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x0002A745 File Offset: 0x00028945
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

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0002A753 File Offset: 0x00028953
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x0002A760 File Offset: 0x00028960
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

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0002A76E File Offset: 0x0002896E
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x0002A77B File Offset: 0x0002897B
		public TextureCube EnvironmentMap
		{
			get
			{
				return this.environmentMapParam.GetValueTextureCube();
			}
			set
			{
				this.environmentMapParam.SetValue(value);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0002A789 File Offset: 0x00028989
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x0002A796 File Offset: 0x00028996
		public float EnvironmentMapAmount
		{
			get
			{
				return this.environmentMapAmountParam.GetValueSingle();
			}
			set
			{
				this.environmentMapAmountParam.SetValue(value);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0002A7A4 File Offset: 0x000289A4
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x0002A7B4 File Offset: 0x000289B4
		public Vector3 EnvironmentMapSpecular
		{
			get
			{
				return this.environmentMapSpecularParam.GetValueVector3();
			}
			set
			{
				this.environmentMapSpecularParam.SetValue(value);
				bool flag = value != Vector3.Zero;
				if (this.specularEnabled != flag)
				{
					this.specularEnabled = flag;
					this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
				}
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0002A7FB File Offset: 0x000289FB
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x0002A808 File Offset: 0x00028A08
		public float FresnelFactor
		{
			get
			{
				return this.fresnelFactorParam.GetValueSingle();
			}
			set
			{
				this.fresnelFactorParam.SetValue(value);
				bool flag = value != 0f;
				if (this.fresnelEnabled != flag)
				{
					this.fresnelEnabled = flag;
					this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x0002A84F File Offset: 0x00028A4F
		bool IEffectLights.LightingEnabled
		{
			get
			{
				return true;
			}
			set
			{
				if (!value)
				{
					throw new NotSupportedException("EnvironmentMapEffect does not support setting LightingEnabled to false.");
				}
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0002A860 File Offset: 0x00028A60
		public EnvironmentMapEffect(GraphicsDevice device)
			: base(device, Resources.EnvironmentMapEffect)
		{
			this.CacheEffectParameters(null);
			this.DirectionalLight0.Enabled = true;
			this.EnvironmentMapAmount = 1f;
			this.EnvironmentMapSpecular = Vector3.Zero;
			this.FresnelFactor = 1f;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0002A90C File Offset: 0x00028B0C
		protected EnvironmentMapEffect(EnvironmentMapEffect cloneSource)
			: base(cloneSource)
		{
			this.CacheEffectParameters(cloneSource);
			this.fogEnabled = cloneSource.fogEnabled;
			this.fresnelEnabled = cloneSource.fresnelEnabled;
			this.specularEnabled = cloneSource.specularEnabled;
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

		// Token: 0x06001274 RID: 4724 RVA: 0x0002AA16 File Offset: 0x00028C16
		public override Effect Clone()
		{
			return new EnvironmentMapEffect(this);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0002AA1E File Offset: 0x00028C1E
		public void EnableDefaultLighting()
		{
			this.AmbientLightColor = EffectHelpers.EnableDefaultLighting(this.light0, this.light1, this.light2);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0002AA40 File Offset: 0x00028C40
		private void CacheEffectParameters(EnvironmentMapEffect cloneSource)
		{
			this.textureParam = base.Parameters["Texture"];
			this.environmentMapParam = base.Parameters["EnvironmentMap"];
			this.environmentMapAmountParam = base.Parameters["EnvironmentMapAmount"];
			this.environmentMapSpecularParam = base.Parameters["EnvironmentMapSpecular"];
			this.fresnelFactorParam = base.Parameters["FresnelFactor"];
			this.diffuseColorParam = base.Parameters["DiffuseColor"];
			this.emissiveColorParam = base.Parameters["EmissiveColor"];
			this.eyePositionParam = base.Parameters["EyePosition"];
			this.fogColorParam = base.Parameters["FogColor"];
			this.fogVectorParam = base.Parameters["FogVector"];
			this.worldParam = base.Parameters["World"];
			this.worldInverseTransposeParam = base.Parameters["WorldInverseTranspose"];
			this.worldViewProjParam = base.Parameters["WorldViewProj"];
			this.shaderIndexParam = base.Parameters["ShaderIndex"];
			this.light0 = new DirectionalLight(base.Parameters["DirLight0Direction"], base.Parameters["DirLight0DiffuseColor"], null, (cloneSource != null) ? cloneSource.light0 : null);
			this.light1 = new DirectionalLight(base.Parameters["DirLight1Direction"], base.Parameters["DirLight1DiffuseColor"], null, (cloneSource != null) ? cloneSource.light1 : null);
			this.light2 = new DirectionalLight(base.Parameters["DirLight2Direction"], base.Parameters["DirLight2DiffuseColor"], null, (cloneSource != null) ? cloneSource.light2 : null);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0002AC2C File Offset: 0x00028E2C
		protected internal override void OnApply()
		{
			this.dirtyFlags = EffectHelpers.SetWorldViewProjAndFog(this.dirtyFlags, ref this.world, ref this.view, ref this.projection, ref this.worldView, this.fogEnabled, this.fogStart, this.fogEnd, this.worldViewProjParam, this.fogVectorParam);
			this.dirtyFlags = EffectHelpers.SetLightingMatrices(this.dirtyFlags, ref this.world, ref this.view, this.worldParam, this.worldInverseTransposeParam, this.eyePositionParam);
			if ((this.dirtyFlags & EffectDirtyFlags.MaterialColor) != (EffectDirtyFlags)0)
			{
				EffectHelpers.SetMaterialColor(true, this.alpha, ref this.diffuseColor, ref this.emissiveColor, ref this.ambientLightColor, this.diffuseColorParam, this.emissiveColorParam);
				this.dirtyFlags &= ~EffectDirtyFlags.MaterialColor;
			}
			bool flag = !this.light1.Enabled && !this.light2.Enabled;
			if (this.oneLight != flag)
			{
				this.oneLight = flag;
				this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
			}
			if ((this.dirtyFlags & EffectDirtyFlags.ShaderIndex) != (EffectDirtyFlags)0)
			{
				int num = 0;
				if (!this.fogEnabled)
				{
					num++;
				}
				if (this.fresnelEnabled)
				{
					num += 2;
				}
				if (this.specularEnabled)
				{
					num += 4;
				}
				if (this.oneLight)
				{
					num += 8;
				}
				this.shaderIndexParam.SetValue(num);
				this.dirtyFlags &= ~EffectDirtyFlags.ShaderIndex;
			}
		}

		// Token: 0x0400086E RID: 2158
		private EffectParameter textureParam;

		// Token: 0x0400086F RID: 2159
		private EffectParameter environmentMapParam;

		// Token: 0x04000870 RID: 2160
		private EffectParameter environmentMapAmountParam;

		// Token: 0x04000871 RID: 2161
		private EffectParameter environmentMapSpecularParam;

		// Token: 0x04000872 RID: 2162
		private EffectParameter fresnelFactorParam;

		// Token: 0x04000873 RID: 2163
		private EffectParameter diffuseColorParam;

		// Token: 0x04000874 RID: 2164
		private EffectParameter emissiveColorParam;

		// Token: 0x04000875 RID: 2165
		private EffectParameter eyePositionParam;

		// Token: 0x04000876 RID: 2166
		private EffectParameter fogColorParam;

		// Token: 0x04000877 RID: 2167
		private EffectParameter fogVectorParam;

		// Token: 0x04000878 RID: 2168
		private EffectParameter worldParam;

		// Token: 0x04000879 RID: 2169
		private EffectParameter worldInverseTransposeParam;

		// Token: 0x0400087A RID: 2170
		private EffectParameter worldViewProjParam;

		// Token: 0x0400087B RID: 2171
		private EffectParameter shaderIndexParam;

		// Token: 0x0400087C RID: 2172
		private bool oneLight;

		// Token: 0x0400087D RID: 2173
		private bool fogEnabled;

		// Token: 0x0400087E RID: 2174
		private bool fresnelEnabled;

		// Token: 0x0400087F RID: 2175
		private bool specularEnabled;

		// Token: 0x04000880 RID: 2176
		private Matrix world = Matrix.Identity;

		// Token: 0x04000881 RID: 2177
		private Matrix view = Matrix.Identity;

		// Token: 0x04000882 RID: 2178
		private Matrix projection = Matrix.Identity;

		// Token: 0x04000883 RID: 2179
		private Matrix worldView;

		// Token: 0x04000884 RID: 2180
		private Vector3 diffuseColor = Vector3.One;

		// Token: 0x04000885 RID: 2181
		private Vector3 emissiveColor = Vector3.Zero;

		// Token: 0x04000886 RID: 2182
		private Vector3 ambientLightColor = Vector3.Zero;

		// Token: 0x04000887 RID: 2183
		private float alpha = 1f;

		// Token: 0x04000888 RID: 2184
		private DirectionalLight light0;

		// Token: 0x04000889 RID: 2185
		private DirectionalLight light1;

		// Token: 0x0400088A RID: 2186
		private DirectionalLight light2;

		// Token: 0x0400088B RID: 2187
		private float fogStart;

		// Token: 0x0400088C RID: 2188
		private float fogEnd = 1f;

		// Token: 0x0400088D RID: 2189
		private EffectDirtyFlags dirtyFlags = EffectDirtyFlags.All;
	}
}
