using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020001E2 RID: 482
	public class HairShaderData : ShaderData
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00521C2B File Offset: 0x0051FE2B
		public bool ShaderDisabled
		{
			get
			{
				return this._shaderDisabled;
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00521C34 File Offset: 0x0051FE34
		public HairShaderData(Asset<Effect> shader, string passName)
			: base(shader, passName)
		{
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00521C80 File Offset: 0x0051FE80
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
			this.uDirection = base.Shader.GetParameter("uDirection");
			this.uSourceRect = base.Shader.GetParameter("uSourceRect");
			this.uDrawPosition = base.Shader.GetParameter("uDrawPosition");
			this.uTargetPosition = base.Shader.GetParameter("uTargetPosition");
			this.uImageSize0 = base.Shader.GetParameter("uImageSize0");
			this.uImageSize1 = base.Shader.GetParameter("uImageSize1");
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00521DA4 File Offset: 0x0051FFA4
		public virtual void Apply(Player player, DrawData? drawData = null)
		{
			if (this._shaderDisabled)
			{
				return;
			}
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
				Vector4 vector = new Vector4((float)value.sourceRect.Value.X, (float)value.sourceRect.Value.Y, (float)value.sourceRect.Value.Width, (float)value.sourceRect.Value.Height);
				this.uSourceRect.SetValue(vector);
				this.uDrawPosition.SetValue(value.position);
				this.uImageSize0.SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
			}
			else
			{
				this.uSourceRect.SetValue(new Vector4(0f, 0f, 4f, 4f));
			}
			if (this._uImage != null)
			{
				Main.graphics.GraphicsDevice.Textures[1] = this._uImage.Value;
				this.uImageSize1.SetValue(new Vector2((float)this._uImage.Width(), (float)this._uImage.Height()));
			}
			if (player != null)
			{
				this.uDirection.SetValue((float)player.direction);
			}
			this.Apply();
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00521F5E File Offset: 0x0052015E
		public virtual Color GetColor(Player player, Color lightColor)
		{
			return new Color(lightColor.ToVector4() * player.hairColor.ToVector4());
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00521F7C File Offset: 0x0052017C
		public HairShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00521F8C File Offset: 0x0052018C
		public HairShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00521F9B File Offset: 0x0052019B
		public HairShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00521FA5 File Offset: 0x005201A5
		public HairShaderData UseImage(string path)
		{
			if (!Main.dedServ)
			{
				this._uImage = Main.Assets.Request<Texture2D>(path, 1);
			}
			return this;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00521FC1 File Offset: 0x005201C1
		public HairShaderData UseOpacity(float alpha)
		{
			this._uOpacity = alpha;
			return this;
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00521FCB File Offset: 0x005201CB
		public HairShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00521FDB File Offset: 0x005201DB
		public HairShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00521FEA File Offset: 0x005201EA
		public HairShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00521FF4 File Offset: 0x005201F4
		public HairShaderData UseSaturation(float saturation)
		{
			this._uSaturation = saturation;
			return this;
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00521FFE File Offset: 0x005201FE
		public HairShaderData UseTargetPosition(Vector2 position)
		{
			this._uTargetPosition = position;
			return this;
		}

		// Token: 0x04004AB5 RID: 19125
		protected Vector3 _uColor = Vector3.One;

		// Token: 0x04004AB6 RID: 19126
		protected Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04004AB7 RID: 19127
		protected float _uSaturation = 1f;

		// Token: 0x04004AB8 RID: 19128
		protected float _uOpacity = 1f;

		// Token: 0x04004AB9 RID: 19129
		protected Asset<Texture2D> _uImage;

		// Token: 0x04004ABA RID: 19130
		protected bool _shaderDisabled;

		// Token: 0x04004ABB RID: 19131
		private Vector2 _uTargetPosition = Vector2.One;

		// Token: 0x04004ABC RID: 19132
		private Effect _effect;

		// Token: 0x04004ABD RID: 19133
		private ShaderData.EffectParameter<Vector3> uColor;

		// Token: 0x04004ABE RID: 19134
		private ShaderData.EffectParameter<float> uSaturation;

		// Token: 0x04004ABF RID: 19135
		private ShaderData.EffectParameter<Vector3> uSecondaryColor;

		// Token: 0x04004AC0 RID: 19136
		private ShaderData.EffectParameter<float> uTime;

		// Token: 0x04004AC1 RID: 19137
		private ShaderData.EffectParameter<float> uOpacity;

		// Token: 0x04004AC2 RID: 19138
		private ShaderData.EffectParameter<float> uDirection;

		// Token: 0x04004AC3 RID: 19139
		private ShaderData.EffectParameter<Vector4> uSourceRect;

		// Token: 0x04004AC4 RID: 19140
		private ShaderData.EffectParameter<Vector2> uDrawPosition;

		// Token: 0x04004AC5 RID: 19141
		private ShaderData.EffectParameter<Vector2> uTargetPosition;

		// Token: 0x04004AC6 RID: 19142
		private ShaderData.EffectParameter<Vector2> uImageSize0;

		// Token: 0x04004AC7 RID: 19143
		private ShaderData.EffectParameter<Vector2> uImageSize1;
	}
}
