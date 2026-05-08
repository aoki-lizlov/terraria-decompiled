using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000096 RID: 150
	public class SkinnedEffect : Effect, IEffectMatrices, IEffectLights, IEffectFog
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0002AD91 File Offset: 0x00028F91
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x0002AD99 File Offset: 0x00028F99
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

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0002ADB1 File Offset: 0x00028FB1
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x0002ADB9 File Offset: 0x00028FB9
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

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0002ADD1 File Offset: 0x00028FD1
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x0002ADD9 File Offset: 0x00028FD9
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

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0002ADF0 File Offset: 0x00028FF0
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x0002ADF8 File Offset: 0x00028FF8
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

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0002AE0F File Offset: 0x0002900F
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x0002AE17 File Offset: 0x00029017
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

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0002AE2E File Offset: 0x0002902E
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x0002AE3B File Offset: 0x0002903B
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0002AE49 File Offset: 0x00029049
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x0002AE56 File Offset: 0x00029056
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

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0002AE64 File Offset: 0x00029064
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x0002AE6C File Offset: 0x0002906C
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

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0002AE83 File Offset: 0x00029083
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x0002AE8B File Offset: 0x0002908B
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

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0002AEAF File Offset: 0x000290AF
		// (set) Token: 0x0600128B RID: 4747 RVA: 0x0002AEB7 File Offset: 0x000290B7
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

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x0002AECE File Offset: 0x000290CE
		public DirectionalLight DirectionalLight0
		{
			get
			{
				return this.light0;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0002AED6 File Offset: 0x000290D6
		public DirectionalLight DirectionalLight1
		{
			get
			{
				return this.light1;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0002AEDE File Offset: 0x000290DE
		public DirectionalLight DirectionalLight2
		{
			get
			{
				return this.light2;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0002AEE6 File Offset: 0x000290E6
		// (set) Token: 0x06001290 RID: 4752 RVA: 0x0002AEEE File Offset: 0x000290EE
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

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0002AF12 File Offset: 0x00029112
		// (set) Token: 0x06001292 RID: 4754 RVA: 0x0002AF1A File Offset: 0x0002911A
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

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0002AF32 File Offset: 0x00029132
		// (set) Token: 0x06001294 RID: 4756 RVA: 0x0002AF3A File Offset: 0x0002913A
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

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0002AF52 File Offset: 0x00029152
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x0002AF5F File Offset: 0x0002915F
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

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0002AF6D File Offset: 0x0002916D
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x0002AF7A File Offset: 0x0002917A
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0002AF88 File Offset: 0x00029188
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x0002AF90 File Offset: 0x00029190
		public int WeightsPerVertex
		{
			get
			{
				return this.weightsPerVertex;
			}
			set
			{
				if (value != 1 && value != 2 && value != 4)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.weightsPerVertex = value;
				this.dirtyFlags |= EffectDirtyFlags.ShaderIndex;
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0002AFC2 File Offset: 0x000291C2
		public void SetBoneTransforms(Matrix[] boneTransforms)
		{
			if (boneTransforms == null || boneTransforms.Length == 0)
			{
				throw new ArgumentNullException("boneTransforms");
			}
			if (boneTransforms.Length > 72)
			{
				throw new ArgumentException();
			}
			this.bonesParam.SetValue(boneTransforms);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0002AFF0 File Offset: 0x000291F0
		public Matrix[] GetBoneTransforms(int count)
		{
			if (count <= 0 || count > 72)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			Matrix[] valueMatrixArray = this.bonesParam.GetValueMatrixArray(count);
			for (int i = 0; i < valueMatrixArray.Length; i++)
			{
				valueMatrixArray[i].M44 = 1f;
			}
			return valueMatrixArray;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x0002B03E File Offset: 0x0002923E
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
					throw new NotSupportedException("SkinnedEffect does not support setting LightingEnabled to false.");
				}
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0002B050 File Offset: 0x00029250
		public SkinnedEffect(GraphicsDevice device)
			: base(device, Resources.SkinnedEffect)
		{
			this.CacheEffectParameters(null);
			this.DirectionalLight0.Enabled = true;
			this.SpecularColor = Vector3.One;
			this.SpecularPower = 16f;
			Matrix[] array = new Matrix[72];
			for (int i = 0; i < 72; i++)
			{
				array[i] = Matrix.Identity;
			}
			this.SetBoneTransforms(array);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0002B120 File Offset: 0x00029320
		protected SkinnedEffect(SkinnedEffect cloneSource)
			: base(cloneSource)
		{
			this.CacheEffectParameters(cloneSource);
			this.preferPerPixelLighting = cloneSource.preferPerPixelLighting;
			this.fogEnabled = cloneSource.fogEnabled;
			this.world = cloneSource.world;
			this.view = cloneSource.view;
			this.projection = cloneSource.projection;
			this.diffuseColor = cloneSource.diffuseColor;
			this.emissiveColor = cloneSource.emissiveColor;
			this.ambientLightColor = cloneSource.ambientLightColor;
			this.alpha = cloneSource.alpha;
			this.fogStart = cloneSource.fogStart;
			this.fogEnd = cloneSource.fogEnd;
			this.weightsPerVertex = cloneSource.weightsPerVertex;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0002B231 File Offset: 0x00029431
		public override Effect Clone()
		{
			return new SkinnedEffect(this);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0002B239 File Offset: 0x00029439
		public void EnableDefaultLighting()
		{
			this.AmbientLightColor = EffectHelpers.EnableDefaultLighting(this.light0, this.light1, this.light2);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0002B258 File Offset: 0x00029458
		private void CacheEffectParameters(SkinnedEffect cloneSource)
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
			this.bonesParam = base.Parameters["Bones"];
			this.shaderIndexParam = base.Parameters["ShaderIndex"];
			this.light0 = new DirectionalLight(base.Parameters["DirLight0Direction"], base.Parameters["DirLight0DiffuseColor"], base.Parameters["DirLight0SpecularColor"], (cloneSource != null) ? cloneSource.light0 : null);
			this.light1 = new DirectionalLight(base.Parameters["DirLight1Direction"], base.Parameters["DirLight1DiffuseColor"], base.Parameters["DirLight1SpecularColor"], (cloneSource != null) ? cloneSource.light1 : null);
			this.light2 = new DirectionalLight(base.Parameters["DirLight2Direction"], base.Parameters["DirLight2DiffuseColor"], base.Parameters["DirLight2SpecularColor"], (cloneSource != null) ? cloneSource.light2 : null);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0002B458 File Offset: 0x00029658
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
				if (this.weightsPerVertex == 2)
				{
					num += 2;
				}
				else if (this.weightsPerVertex == 4)
				{
					num += 4;
				}
				if (this.preferPerPixelLighting)
				{
					num += 12;
				}
				else if (this.oneLight)
				{
					num += 6;
				}
				this.shaderIndexParam.SetValue(num);
				this.dirtyFlags &= ~EffectDirtyFlags.ShaderIndex;
			}
		}

		// Token: 0x0400088E RID: 2190
		public const int MaxBones = 72;

		// Token: 0x0400088F RID: 2191
		private EffectParameter textureParam;

		// Token: 0x04000890 RID: 2192
		private EffectParameter diffuseColorParam;

		// Token: 0x04000891 RID: 2193
		private EffectParameter emissiveColorParam;

		// Token: 0x04000892 RID: 2194
		private EffectParameter specularColorParam;

		// Token: 0x04000893 RID: 2195
		private EffectParameter specularPowerParam;

		// Token: 0x04000894 RID: 2196
		private EffectParameter eyePositionParam;

		// Token: 0x04000895 RID: 2197
		private EffectParameter fogColorParam;

		// Token: 0x04000896 RID: 2198
		private EffectParameter fogVectorParam;

		// Token: 0x04000897 RID: 2199
		private EffectParameter worldParam;

		// Token: 0x04000898 RID: 2200
		private EffectParameter worldInverseTransposeParam;

		// Token: 0x04000899 RID: 2201
		private EffectParameter worldViewProjParam;

		// Token: 0x0400089A RID: 2202
		private EffectParameter bonesParam;

		// Token: 0x0400089B RID: 2203
		private EffectParameter shaderIndexParam;

		// Token: 0x0400089C RID: 2204
		private bool preferPerPixelLighting;

		// Token: 0x0400089D RID: 2205
		private bool oneLight;

		// Token: 0x0400089E RID: 2206
		private bool fogEnabled;

		// Token: 0x0400089F RID: 2207
		private Matrix world = Matrix.Identity;

		// Token: 0x040008A0 RID: 2208
		private Matrix view = Matrix.Identity;

		// Token: 0x040008A1 RID: 2209
		private Matrix projection = Matrix.Identity;

		// Token: 0x040008A2 RID: 2210
		private Matrix worldView;

		// Token: 0x040008A3 RID: 2211
		private Vector3 diffuseColor = Vector3.One;

		// Token: 0x040008A4 RID: 2212
		private Vector3 emissiveColor = Vector3.Zero;

		// Token: 0x040008A5 RID: 2213
		private Vector3 ambientLightColor = Vector3.Zero;

		// Token: 0x040008A6 RID: 2214
		private float alpha = 1f;

		// Token: 0x040008A7 RID: 2215
		private DirectionalLight light0;

		// Token: 0x040008A8 RID: 2216
		private DirectionalLight light1;

		// Token: 0x040008A9 RID: 2217
		private DirectionalLight light2;

		// Token: 0x040008AA RID: 2218
		private float fogStart;

		// Token: 0x040008AB RID: 2219
		private float fogEnd = 1f;

		// Token: 0x040008AC RID: 2220
		private int weightsPerVertex = 4;

		// Token: 0x040008AD RID: 2221
		private EffectDirtyFlags dirtyFlags = EffectDirtyFlags.All;
	}
}
