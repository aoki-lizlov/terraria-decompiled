using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x02000293 RID: 659
	public class ReflectiveArmorShaderData : ArmorShaderData
	{
		// Token: 0x06002537 RID: 9527 RVA: 0x005539DD File Offset: 0x00551BDD
		public ReflectiveArmorShaderData(Asset<Effect> shader, string passName)
			: base(shader, passName)
		{
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00553BE8 File Offset: 0x00551DE8
		private void CheckCachedParameters()
		{
			if (this._effect != null && this._effect == base.Shader)
			{
				return;
			}
			this._effect = base.Shader;
			this.uLightSource = base.Shader.GetParameter("uLightSource");
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00553C24 File Offset: 0x00551E24
		public override void Apply(Entity entity, DrawData? drawData)
		{
			this.CheckCachedParameters();
			if (entity == null)
			{
				this.uLightSource.SetValue(Vector3.Zero);
			}
			else
			{
				float num = 0f;
				if (drawData != null)
				{
					num = drawData.Value.rotation;
				}
				Vector2 position = entity.position;
				float num2 = (float)entity.width;
				float num3 = (float)entity.height;
				Vector2 vector = position + new Vector2(num2, num3) * 0.1f;
				num2 *= 0.8f;
				num3 *= 0.8f;
				Vector3 subLight = Lighting.GetSubLight(vector + new Vector2(num2 * 0.5f, 0f));
				Vector3 subLight2 = Lighting.GetSubLight(vector + new Vector2(0f, num3 * 0.5f));
				Vector3 subLight3 = Lighting.GetSubLight(vector + new Vector2(num2, num3 * 0.5f));
				Vector3 subLight4 = Lighting.GetSubLight(vector + new Vector2(num2 * 0.5f, num3));
				float num4 = subLight.X + subLight.Y + subLight.Z;
				float num5 = subLight2.X + subLight2.Y + subLight2.Z;
				float num6 = subLight3.X + subLight3.Y + subLight3.Z;
				float num7 = subLight4.X + subLight4.Y + subLight4.Z;
				Vector2 vector2 = new Vector2(num6 - num5, num7 - num4);
				float num8 = vector2.Length();
				if (num8 > 1f)
				{
					num8 = 1f;
					vector2 /= num8;
				}
				if (entity.direction == -1)
				{
					vector2.X *= -1f;
				}
				vector2 = vector2.RotatedBy((double)(-(double)num), default(Vector2));
				Vector3 vector3 = new Vector3(vector2, 1f - (vector2.X * vector2.X + vector2.Y * vector2.Y));
				vector3.X *= 2f;
				vector3.Y -= 0.15f;
				vector3.Y *= 2f;
				vector3.Normalize();
				vector3.Z *= 0.6f;
				this.uLightSource.SetValue(vector3);
			}
			base.Apply(entity, drawData);
		}

		// Token: 0x04004F92 RID: 20370
		private Effect _effect;

		// Token: 0x04004F93 RID: 20371
		private ShaderData.EffectParameter<Vector3> uLightSource;
	}
}
