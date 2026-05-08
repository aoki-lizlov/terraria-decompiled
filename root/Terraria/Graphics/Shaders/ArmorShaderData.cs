using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020001E4 RID: 484
	public class ArmorShaderData : ShaderData
	{
		// Token: 0x06002070 RID: 8304 RVA: 0x00522778 File Offset: 0x00520978
		public ArmorShaderData(Asset<Effect> shader, string passName)
			: base(shader, passName)
		{
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x005227C4 File Offset: 0x005209C4
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
			this.uTargetPosition = base.Shader.GetParameter("uTargetPosition");
			this.uSourceRect = base.Shader.GetParameter("uSourceRect");
			this.uLegacyArmorSourceRect = base.Shader.GetParameter("uLegacyArmorSourceRect");
			this.uLegacyArmorSheetSize = base.Shader.GetParameter("uLegacyArmorSheetSize");
			this.uDrawPosition = base.Shader.GetParameter("uDrawPosition");
			this.uRotation = base.Shader.GetParameter("uRotation");
			this.uDirection = base.Shader.GetParameter("uDirection");
			this.uImageSize0 = base.Shader.GetParameter("uImageSize0");
			this.uImageSize1 = base.Shader.GetParameter("uImageSize1");
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00522928 File Offset: 0x00520B28
		public virtual void Apply(Entity entity, DrawData? drawData = null)
		{
			this.CheckCachedParameters();
			this.uColor.SetValue(this._uColor);
			this.uSaturation.SetValue(this._uSaturation);
			this.uSecondaryColor.SetValue(this._uSecondaryColor);
			this.uTime.SetValue(Main.GlobalTimeWrappedHourly);
			this.uOpacity.SetValue(this._uOpacity);
			this.uTargetPosition.SetValue(this._uTargetPosition);
			if (drawData != null)
			{
				DrawData value = drawData.Value;
				Vector4 vector;
				if (value.sourceRect != null)
				{
					vector = new Vector4((float)value.sourceRect.Value.X, (float)value.sourceRect.Value.Y, (float)value.sourceRect.Value.Width, (float)value.sourceRect.Value.Height);
				}
				else
				{
					vector = new Vector4(0f, 0f, (float)value.texture.Width, (float)value.texture.Height);
				}
				this.uSourceRect.SetValue(vector);
				this.uLegacyArmorSourceRect.SetValue(vector);
				this.uDrawPosition.SetValue(value.position);
				this.uImageSize0.SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
				this.uLegacyArmorSheetSize.SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
				this.uRotation.SetValue(value.rotation * (((value.effect & SpriteEffects.FlipHorizontally) != SpriteEffects.None) ? (-1f) : 1f));
				this.uDirection.SetValue((float)(((value.effect & SpriteEffects.FlipHorizontally) != SpriteEffects.None) ? (-1) : 1));
			}
			else
			{
				Vector4 vector2 = new Vector4(0f, 0f, 4f, 4f);
				this.uSourceRect.SetValue(vector2);
				this.uLegacyArmorSourceRect.SetValue(vector2);
				this.uRotation.SetValue(0f);
			}
			if (this._uImage != null)
			{
				Main.graphics.GraphicsDevice.Textures[1] = this._uImage.Value;
				this.uImageSize1.SetValue(new Vector2((float)this._uImage.Width(), (float)this._uImage.Height()));
			}
			if (entity != null)
			{
				this.uDirection.SetValue((float)entity.direction);
			}
			Player player = entity as Player;
			if (player != null)
			{
				Rectangle bodyFrame = player.bodyFrame;
				this.uLegacyArmorSourceRect.SetValue(new Vector4((float)bodyFrame.X, (float)bodyFrame.Y, (float)bodyFrame.Width, (float)bodyFrame.Height));
				this.uLegacyArmorSheetSize.SetValue(new Vector2(40f, 1120f));
			}
			this.Apply();
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00522C03 File Offset: 0x00520E03
		public ArmorShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00522C13 File Offset: 0x00520E13
		public ArmorShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00522C22 File Offset: 0x00520E22
		public ArmorShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00522C2C File Offset: 0x00520E2C
		public ArmorShaderData UseImage(string path)
		{
			if (!Main.dedServ)
			{
				this._uImage = Main.Assets.Request<Texture2D>(path, 1);
			}
			return this;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x00522C48 File Offset: 0x00520E48
		public ArmorShaderData UseOpacity(float alpha)
		{
			this._uOpacity = alpha;
			return this;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00522C52 File Offset: 0x00520E52
		public ArmorShaderData UseTargetPosition(Vector2 position)
		{
			this._uTargetPosition = position;
			return this;
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x00522C5C File Offset: 0x00520E5C
		public ArmorShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x00522C6C File Offset: 0x00520E6C
		public ArmorShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x00522C7B File Offset: 0x00520E7B
		public ArmorShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x00522C85 File Offset: 0x00520E85
		public ArmorShaderData UseSaturation(float saturation)
		{
			this._uSaturation = saturation;
			return this;
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x00521C1E File Offset: 0x0051FE1E
		public virtual ArmorShaderData GetSecondaryShader(Entity entity)
		{
			return this;
		}

		// Token: 0x04004AEA RID: 19178
		private Vector3 _uColor = Vector3.One;

		// Token: 0x04004AEB RID: 19179
		private Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04004AEC RID: 19180
		private float _uSaturation = 1f;

		// Token: 0x04004AED RID: 19181
		private float _uOpacity = 1f;

		// Token: 0x04004AEE RID: 19182
		private Asset<Texture2D> _uImage;

		// Token: 0x04004AEF RID: 19183
		private Vector2 _uTargetPosition = Vector2.One;

		// Token: 0x04004AF0 RID: 19184
		private Effect _effect;

		// Token: 0x04004AF1 RID: 19185
		private ShaderData.EffectParameter<Vector3> uColor;

		// Token: 0x04004AF2 RID: 19186
		private ShaderData.EffectParameter<float> uSaturation;

		// Token: 0x04004AF3 RID: 19187
		private ShaderData.EffectParameter<Vector3> uSecondaryColor;

		// Token: 0x04004AF4 RID: 19188
		private ShaderData.EffectParameter<float> uTime;

		// Token: 0x04004AF5 RID: 19189
		private ShaderData.EffectParameter<float> uOpacity;

		// Token: 0x04004AF6 RID: 19190
		private ShaderData.EffectParameter<Vector2> uTargetPosition;

		// Token: 0x04004AF7 RID: 19191
		private ShaderData.EffectParameter<Vector4> uSourceRect;

		// Token: 0x04004AF8 RID: 19192
		private ShaderData.EffectParameter<Vector4> uLegacyArmorSourceRect;

		// Token: 0x04004AF9 RID: 19193
		private ShaderData.EffectParameter<Vector2> uLegacyArmorSheetSize;

		// Token: 0x04004AFA RID: 19194
		private ShaderData.EffectParameter<Vector2> uDrawPosition;

		// Token: 0x04004AFB RID: 19195
		private ShaderData.EffectParameter<float> uRotation;

		// Token: 0x04004AFC RID: 19196
		private ShaderData.EffectParameter<float> uDirection;

		// Token: 0x04004AFD RID: 19197
		private ShaderData.EffectParameter<Vector2> uImageSize0;

		// Token: 0x04004AFE RID: 19198
		private ShaderData.EffectParameter<Vector2> uImageSize1;
	}
}
