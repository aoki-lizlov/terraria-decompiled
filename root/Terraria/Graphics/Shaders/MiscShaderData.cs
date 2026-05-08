using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020001E1 RID: 481
	public class MiscShaderData : ShaderData
	{
		// Token: 0x0600202C RID: 8236 RVA: 0x00521544 File Offset: 0x0051F744
		public MiscShaderData(Asset<Effect> shader, string passName)
			: base(shader, passName)
		{
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00521590 File Offset: 0x0051F790
		private void CheckCachedParameters()
		{
			if (this._effect != null && this._effect == base.Shader)
			{
				return;
			}
			this._effect = base.Shader;
			this.uColor = base.Shader.GetParameter("uColor");
			this.uSaturation = base.Shader.GetParameter("uSaturation");
			this.uSecondaryColor = base.Shader.GetParameter("uSecondaryColor");
			this.uTime = base.Shader.GetParameter("uTime");
			this.uOpacity = base.Shader.GetParameter("uOpacity");
			this.uShaderSpecificData = base.Shader.GetParameter("uShaderSpecificData");
			this.uSourceRect = base.Shader.GetParameter("uSourceRect");
			this.uDrawPosition = base.Shader.GetParameter("uDrawPosition");
			this.uImageSize0 = base.Shader.GetParameter("uImageSize0");
			this.uImageSize1 = base.Shader.GetParameter("uImageSize1");
			this.uImageSize2 = base.Shader.GetParameter("uImageSize2");
			this.MatrixTransform = base.Shader.GetParameter("MatrixTransform");
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x005216C8 File Offset: 0x0051F8C8
		public virtual void Apply(DrawData? drawData = null)
		{
			this.CheckCachedParameters();
			this.uColor.SetValue(this._uColor);
			this.uSaturation.SetValue(this._uSaturation);
			this.uSecondaryColor.SetValue(this._uSecondaryColor);
			this.uTime.SetValue(Main.GlobalTimeWrappedHourly);
			this.uOpacity.SetValue(this._uOpacity);
			this.uShaderSpecificData.SetValue(this._shaderSpecificData);
			if (drawData != null)
			{
				DrawData value = drawData.Value;
				Vector4 zero = Vector4.Zero;
				if (drawData.Value.sourceRect != null)
				{
					zero = new Vector4((float)value.sourceRect.Value.X, (float)value.sourceRect.Value.Y, (float)value.sourceRect.Value.Width, (float)value.sourceRect.Value.Height);
				}
				this.uSourceRect.SetValue(zero);
				this.uDrawPosition.SetValue(value.position);
				this.uImageSize0.SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
			}
			else
			{
				this.uSourceRect.SetValue(new Vector4(0f, 0f, 4f, 4f));
			}
			SamplerState samplerState = SamplerState.LinearWrap;
			if (this._customSamplerState != null)
			{
				samplerState = this._customSamplerState;
			}
			Texture texture = ((this._uImage0 != null) ? this._uImage0.Value : this._uImage0Tex);
			if (texture != null)
			{
				Main.graphics.GraphicsDevice.Textures[0] = texture;
				Main.graphics.GraphicsDevice.SamplerStates[0] = samplerState;
				if (texture is Texture2D)
				{
					this.uImageSize0.SetValue(((Texture2D)texture).Size());
				}
			}
			texture = ((this._uImage1 != null) ? this._uImage1.Value : this._uImage1Tex);
			if (texture != null)
			{
				Main.graphics.GraphicsDevice.Textures[1] = texture;
				Main.graphics.GraphicsDevice.SamplerStates[1] = samplerState;
				if (texture is Texture2D)
				{
					this.uImageSize1.SetValue(((Texture2D)texture).Size());
				}
			}
			texture = ((this._uImage2 != null) ? this._uImage2.Value : this._uImage2Tex);
			if (texture != null)
			{
				Main.graphics.GraphicsDevice.Textures[2] = texture;
				Main.graphics.GraphicsDevice.SamplerStates[2] = samplerState;
				if (texture is Texture2D)
				{
					this.uImageSize2.SetValue(((Texture2D)texture).Size());
				}
			}
			if (this._useProjectionMatrix)
			{
				this.MatrixTransform.SetValue(Main.GameViewMatrix.NormalizedTransformationMatrix);
			}
			if (this._transformMatrix != null)
			{
				this.MatrixTransform.SetValue(this._transformMatrix.Value);
			}
			base.Apply();
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x005219C1 File Offset: 0x0051FBC1
		public MiscShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x005219D1 File Offset: 0x0051FBD1
		public MiscShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x005219E0 File Offset: 0x0051FBE0
		public MiscShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x005219EA File Offset: 0x0051FBEA
		public MiscShaderData UseSamplerState(SamplerState state)
		{
			this._customSamplerState = state;
			return this;
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x005219F4 File Offset: 0x0051FBF4
		public MiscShaderData UseImage0(string path)
		{
			if (Main.dedServ)
			{
				return this;
			}
			this._uImage0Tex = null;
			this._uImage0 = Main.Assets.Request<Texture2D>(path, 1);
			return this;
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00521A19 File Offset: 0x0051FC19
		public MiscShaderData UseImage1(string path)
		{
			if (Main.dedServ)
			{
				return this;
			}
			this._uImage1Tex = null;
			this._uImage1 = Main.Assets.Request<Texture2D>(path, 1);
			return this;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00521A3E File Offset: 0x0051FC3E
		public MiscShaderData UseImage2(string path)
		{
			if (Main.dedServ)
			{
				return this;
			}
			this._uImage2Tex = null;
			this._uImage2 = Main.Assets.Request<Texture2D>(path, 1);
			return this;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00521A63 File Offset: 0x0051FC63
		public MiscShaderData UseImage0(Texture texture)
		{
			if (Main.dedServ)
			{
				return this;
			}
			this._uImage0Tex = texture;
			this._uImage0 = null;
			return this;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00521A7D File Offset: 0x0051FC7D
		public MiscShaderData UseImage1(Texture texture)
		{
			if (Main.dedServ)
			{
				return this;
			}
			this._uImage1Tex = texture;
			this._uImage1 = null;
			return this;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00521A97 File Offset: 0x0051FC97
		public MiscShaderData UseImage2(Texture texture)
		{
			if (Main.dedServ)
			{
				return this;
			}
			this._uImage2Tex = texture;
			this._uImage2 = null;
			return this;
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00521AB1 File Offset: 0x0051FCB1
		private static bool IsPowerOfTwo(int n)
		{
			return (int)Math.Ceiling(Math.Log((double)n) / Math.Log(2.0)) == (int)Math.Floor(Math.Log((double)n) / Math.Log(2.0));
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00521AED File Offset: 0x0051FCED
		public MiscShaderData UseOpacity(float alpha)
		{
			this._uOpacity = alpha;
			return this;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00521AF7 File Offset: 0x0051FCF7
		public MiscShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00521B07 File Offset: 0x0051FD07
		public MiscShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00521B16 File Offset: 0x0051FD16
		public MiscShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00521B20 File Offset: 0x0051FD20
		public MiscShaderData UseProjectionMatrix(bool doUse)
		{
			this._useProjectionMatrix = doUse;
			return this;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x00521B2C File Offset: 0x0051FD2C
		public MiscShaderData UseSpriteTransformMatrix(Matrix? transform)
		{
			if (transform == null)
			{
				this._transformMatrix = null;
				return this;
			}
			Viewport viewport = Main.graphics.GraphicsDevice.Viewport;
			float num = ((viewport.Width > 0) ? (1f / (float)viewport.Width) : 0f);
			float num2 = ((viewport.Height > 0) ? (-1f / (float)viewport.Height) : 0f);
			Matrix matrix = new Matrix
			{
				M11 = num * 2f,
				M22 = num2 * 2f,
				M33 = 1f,
				M44 = 1f,
				M41 = -1f - num,
				M42 = 1f - num2
			};
			this._transformMatrix = new Matrix?(transform.Value * matrix);
			return this;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x00521C14 File Offset: 0x0051FE14
		public MiscShaderData UseSaturation(float saturation)
		{
			this._uSaturation = saturation;
			return this;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x00521C1E File Offset: 0x0051FE1E
		public virtual MiscShaderData GetSecondaryShader(Entity entity)
		{
			return this;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00521C21 File Offset: 0x0051FE21
		public MiscShaderData UseShaderSpecificData(Vector4 specificData)
		{
			this._shaderSpecificData = specificData;
			return this;
		}

		// Token: 0x04004A9A RID: 19098
		private Vector3 _uColor = Vector3.One;

		// Token: 0x04004A9B RID: 19099
		private Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04004A9C RID: 19100
		private float _uSaturation = 1f;

		// Token: 0x04004A9D RID: 19101
		private float _uOpacity = 1f;

		// Token: 0x04004A9E RID: 19102
		private Asset<Texture2D> _uImage0;

		// Token: 0x04004A9F RID: 19103
		private Asset<Texture2D> _uImage1;

		// Token: 0x04004AA0 RID: 19104
		private Asset<Texture2D> _uImage2;

		// Token: 0x04004AA1 RID: 19105
		private Texture _uImage0Tex;

		// Token: 0x04004AA2 RID: 19106
		private Texture _uImage1Tex;

		// Token: 0x04004AA3 RID: 19107
		private Texture _uImage2Tex;

		// Token: 0x04004AA4 RID: 19108
		private bool _useProjectionMatrix;

		// Token: 0x04004AA5 RID: 19109
		private Vector4 _shaderSpecificData = Vector4.Zero;

		// Token: 0x04004AA6 RID: 19110
		private SamplerState _customSamplerState;

		// Token: 0x04004AA7 RID: 19111
		private Matrix? _transformMatrix;

		// Token: 0x04004AA8 RID: 19112
		private Effect _effect;

		// Token: 0x04004AA9 RID: 19113
		private ShaderData.EffectParameter<Vector3> uColor;

		// Token: 0x04004AAA RID: 19114
		private ShaderData.EffectParameter<float> uSaturation;

		// Token: 0x04004AAB RID: 19115
		private ShaderData.EffectParameter<Vector3> uSecondaryColor;

		// Token: 0x04004AAC RID: 19116
		private ShaderData.EffectParameter<float> uTime;

		// Token: 0x04004AAD RID: 19117
		private ShaderData.EffectParameter<float> uOpacity;

		// Token: 0x04004AAE RID: 19118
		private ShaderData.EffectParameter<Vector4> uShaderSpecificData;

		// Token: 0x04004AAF RID: 19119
		private ShaderData.EffectParameter<Vector4> uSourceRect;

		// Token: 0x04004AB0 RID: 19120
		private ShaderData.EffectParameter<Vector2> uDrawPosition;

		// Token: 0x04004AB1 RID: 19121
		private ShaderData.EffectParameter<Vector2> uImageSize0;

		// Token: 0x04004AB2 RID: 19122
		private ShaderData.EffectParameter<Vector2> uImageSize1;

		// Token: 0x04004AB3 RID: 19123
		private ShaderData.EffectParameter<Vector2> uImageSize2;

		// Token: 0x04004AB4 RID: 19124
		private ShaderData.EffectParameter<Matrix> MatrixTransform;
	}
}
