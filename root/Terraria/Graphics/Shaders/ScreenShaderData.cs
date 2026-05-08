using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020001E3 RID: 483
	public class ScreenShaderData : ShaderData
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x00522008 File Offset: 0x00520208
		public float Intensity
		{
			get
			{
				return this._uIntensity;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x00522010 File Offset: 0x00520210
		public float CombinedOpacity
		{
			get
			{
				return this._uOpacity * this._globalOpacity;
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00522020 File Offset: 0x00520220
		public ScreenShaderData(string passName)
			: base(Main.ScreenShaderRef, passName)
		{
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x005220FC File Offset: 0x005202FC
		public ScreenShaderData(Asset<Effect> shader, string passName)
			: base(shader, passName)
		{
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void Update(GameTime gameTime)
		{
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x005221D4 File Offset: 0x005203D4
		public static Vector2 UnscaledScreenPosition
		{
			get
			{
				Matrix effectMatrix = Main.GameViewMatrix.EffectMatrix;
				Matrix transformationMatrix = Main.GameViewMatrix.TransformationMatrix;
				return Main.screenPosition + new Vector2(effectMatrix.M41 - transformationMatrix.M41, effectMatrix.M42 - transformationMatrix.M42) / new Vector2(transformationMatrix.M11, transformationMatrix.M22);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x00522236 File Offset: 0x00520436
		public static Vector2 UnscaledScreenSize
		{
			get
			{
				return new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / Main.GameViewMatrix.RenderZoom;
			}
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x00522258 File Offset: 0x00520458
		private void CheckCachedParameters()
		{
			if (this._effect != null && this._effect == base.Shader)
			{
				return;
			}
			this._effect = base.Shader;
			this.uColor = base.Shader.GetParameter("uColor");
			this.uOpacity = base.Shader.GetParameter("uOpacity");
			this.uSecondaryColor = base.Shader.GetParameter("uSecondaryColor");
			this.uTime = base.Shader.GetParameter("uTime");
			this.uScreenResolution = base.Shader.GetParameter("uScreenResolution");
			this.uScreenPosition = base.Shader.GetParameter("uScreenPosition");
			this.uTargetPosition = base.Shader.GetParameter("uTargetPosition");
			this.uImageOffset = base.Shader.GetParameter("uImageOffset");
			this.uSceneSize = base.Shader.GetParameter("uSceneSize");
			this.uSceneOffset = base.Shader.GetParameter("uSceneOffset");
			this.uIntensity = base.Shader.GetParameter("uIntensity");
			this.uProgress = base.Shader.GetParameter("uProgress");
			this.uDirection = base.Shader.GetParameter("uDirection");
			this.uZoom = base.Shader.GetParameter("uZoom");
			this.uMultiChunkScene = base.Shader.GetParameter("uMultiChunkScene");
			for (int i = 0; i < this.uImageSize.Length; i++)
			{
				this.uImageSize[i] = base.Shader.GetParameter("uImageSize" + i);
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00522408 File Offset: 0x00520608
		public override void Apply()
		{
			this.CheckCachedParameters();
			Vector2 vector = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			this.uColor.SetValue(this._uColor);
			this.uOpacity.SetValue(this.CombinedOpacity);
			this.uSecondaryColor.SetValue(this._uSecondaryColor);
			this.uTime.SetValue(Main.GlobalTimeWrappedHourly);
			this.uScreenResolution.SetValue(ScreenShaderData.UnscaledScreenSize);
			this.uScreenPosition.SetValue(ScreenShaderData.UnscaledScreenPosition - vector);
			this.uTargetPosition.SetValue(this._uTargetPosition - vector);
			this.uImageOffset.SetValue(this._uImageOffset);
			this.uSceneSize.SetValue(this._uSceneSize);
			this.uSceneOffset.SetValue(this._uSceneOffset);
			this.uIntensity.SetValue(this._uIntensity);
			this.uProgress.SetValue(this._uProgress);
			this.uDirection.SetValue(this._uDirection);
			this.uZoom.SetValue(Main.GameViewMatrix.RenderZoom);
			this.uMultiChunkScene.SetValue(ScreenShaderData.MultiChunkCapture);
			this.uImageSize[0].SetValue(this._uImageSize0);
			for (int i = 0; i < this._uAssetImages.Length; i++)
			{
				Texture2D texture2D = this._uCustomImages[i];
				if (this._uAssetImages[i] != null && this._uAssetImages[i].IsLoaded)
				{
					texture2D = this._uAssetImages[i].Value;
				}
				if (texture2D != null)
				{
					Main.graphics.GraphicsDevice.Textures[i + 1] = texture2D;
					int width = texture2D.Width;
					int height = texture2D.Height;
					if (this._samplerStates[i] != null)
					{
						Main.graphics.GraphicsDevice.SamplerStates[i + 1] = this._samplerStates[i];
					}
					else if (Utils.IsPowerOfTwo(width) && Utils.IsPowerOfTwo(height))
					{
						Main.graphics.GraphicsDevice.SamplerStates[i + 1] = SamplerState.LinearWrap;
					}
					else
					{
						Main.graphics.GraphicsDevice.SamplerStates[i + 1] = SamplerState.AnisotropicClamp;
					}
					this.uImageSize[i + 1].SetValue(new Vector2((float)width, (float)height) * this._imageScales[i]);
				}
			}
			base.Apply();
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00522668 File Offset: 0x00520868
		public ScreenShaderData UseImageOffset(Vector2 offset)
		{
			this._uImageOffset = offset;
			return this;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x00522672 File Offset: 0x00520872
		public ScreenShaderData UseIntensity(float intensity)
		{
			this._uIntensity = intensity;
			return this;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x0052267C File Offset: 0x0052087C
		public ScreenShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x0052268C File Offset: 0x0052088C
		public ScreenShaderData UseProgress(float progress)
		{
			this._uProgress = progress;
			return this;
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00522696 File Offset: 0x00520896
		public ScreenShaderData UseImage(Texture2D image, int index = 0, SamplerState samplerState = null)
		{
			this._samplerStates[index] = samplerState;
			this._uAssetImages[index] = null;
			this._uCustomImages[index] = image;
			return this;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x005226B4 File Offset: 0x005208B4
		public ScreenShaderData UseImage(string path, int index = 0, SamplerState samplerState = null)
		{
			this._uAssetImages[index] = Main.Assets.Request<Texture2D>(path, 1);
			this._uCustomImages[index] = null;
			this._samplerStates[index] = samplerState;
			return this;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x005226DD File Offset: 0x005208DD
		public ScreenShaderData UseSceneSize(Vector2 size)
		{
			this._uSceneSize = size;
			return this;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x005226E7 File Offset: 0x005208E7
		public ScreenShaderData UseSceneOffset(Vector2 size)
		{
			this._uSceneOffset = size;
			return this;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x005226F1 File Offset: 0x005208F1
		public ScreenShaderData UseImageSize0(Vector2 size)
		{
			this._uImageSize0 = size;
			return this;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x005226FB File Offset: 0x005208FB
		public ScreenShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0052270A File Offset: 0x0052090A
		public ScreenShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00522714 File Offset: 0x00520914
		public ScreenShaderData UseDirection(Vector2 direction)
		{
			this._uDirection = direction;
			return this;
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x0052271E File Offset: 0x0052091E
		public ScreenShaderData UseGlobalOpacity(float opacity)
		{
			this._globalOpacity = opacity;
			return this;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00522728 File Offset: 0x00520928
		public ScreenShaderData UseTargetPosition(Vector2 position)
		{
			this._uTargetPosition = position;
			return this;
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00522732 File Offset: 0x00520932
		public ScreenShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00522742 File Offset: 0x00520942
		public ScreenShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00522751 File Offset: 0x00520951
		public ScreenShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0052275B File Offset: 0x0052095B
		public ScreenShaderData UseOpacity(float opacity)
		{
			this._uOpacity = opacity;
			return this;
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00522765 File Offset: 0x00520965
		public ScreenShaderData UseImageScale(Vector2 scale, int index = 0)
		{
			this._imageScales[index] = scale;
			return this;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00521C1E File Offset: 0x0051FE1E
		public virtual ScreenShaderData GetSecondaryShader(Player player)
		{
			return this;
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static ScreenShaderData()
		{
		}

		// Token: 0x04004AC8 RID: 19144
		private Vector3 _uColor = Vector3.One;

		// Token: 0x04004AC9 RID: 19145
		private Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04004ACA RID: 19146
		private float _uOpacity = 1f;

		// Token: 0x04004ACB RID: 19147
		private float _globalOpacity = 1f;

		// Token: 0x04004ACC RID: 19148
		private float _uIntensity = 1f;

		// Token: 0x04004ACD RID: 19149
		private Vector2 _uTargetPosition = Vector2.One;

		// Token: 0x04004ACE RID: 19150
		private Vector2 _uDirection = new Vector2(0f, 1f);

		// Token: 0x04004ACF RID: 19151
		private float _uProgress;

		// Token: 0x04004AD0 RID: 19152
		private Vector2 _uImageOffset = Vector2.Zero;

		// Token: 0x04004AD1 RID: 19153
		private Vector2 _uSceneSize;

		// Token: 0x04004AD2 RID: 19154
		private Vector2 _uSceneOffset;

		// Token: 0x04004AD3 RID: 19155
		private Vector2 _uImageSize0;

		// Token: 0x04004AD4 RID: 19156
		private Asset<Texture2D>[] _uAssetImages = new Asset<Texture2D>[3];

		// Token: 0x04004AD5 RID: 19157
		private Texture2D[] _uCustomImages = new Texture2D[3];

		// Token: 0x04004AD6 RID: 19158
		private SamplerState[] _samplerStates = new SamplerState[3];

		// Token: 0x04004AD7 RID: 19159
		private Vector2[] _imageScales = new Vector2[]
		{
			Vector2.One,
			Vector2.One,
			Vector2.One
		};

		// Token: 0x04004AD8 RID: 19160
		public static bool MultiChunkCapture;

		// Token: 0x04004AD9 RID: 19161
		private Effect _effect;

		// Token: 0x04004ADA RID: 19162
		private ShaderData.EffectParameter<Vector3> uColor;

		// Token: 0x04004ADB RID: 19163
		private ShaderData.EffectParameter<float> uOpacity;

		// Token: 0x04004ADC RID: 19164
		private ShaderData.EffectParameter<Vector3> uSecondaryColor;

		// Token: 0x04004ADD RID: 19165
		private ShaderData.EffectParameter<float> uTime;

		// Token: 0x04004ADE RID: 19166
		private ShaderData.EffectParameter<Vector2> uScreenResolution;

		// Token: 0x04004ADF RID: 19167
		private ShaderData.EffectParameter<Vector2> uScreenPosition;

		// Token: 0x04004AE0 RID: 19168
		private ShaderData.EffectParameter<Vector2> uTargetPosition;

		// Token: 0x04004AE1 RID: 19169
		private ShaderData.EffectParameter<Vector2> uImageOffset;

		// Token: 0x04004AE2 RID: 19170
		private ShaderData.EffectParameter<Vector2> uSceneSize;

		// Token: 0x04004AE3 RID: 19171
		private ShaderData.EffectParameter<Vector2> uSceneOffset;

		// Token: 0x04004AE4 RID: 19172
		private ShaderData.EffectParameter<float> uIntensity;

		// Token: 0x04004AE5 RID: 19173
		private ShaderData.EffectParameter<float> uProgress;

		// Token: 0x04004AE6 RID: 19174
		private ShaderData.EffectParameter<Vector2> uDirection;

		// Token: 0x04004AE7 RID: 19175
		private ShaderData.EffectParameter<Vector2> uZoom;

		// Token: 0x04004AE8 RID: 19176
		private ShaderData.EffectParameter<Vector2>[] uImageSize = new ShaderData.EffectParameter<Vector2>[4];

		// Token: 0x04004AE9 RID: 19177
		private ShaderData.EffectParameter<bool> uMultiChunkScene;
	}
}
