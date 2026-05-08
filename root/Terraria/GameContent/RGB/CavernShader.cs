using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002B4 RID: 692
	public class CavernShader : ChromaShader
	{
		// Token: 0x0600259D RID: 9629 RVA: 0x0055846D File Offset: 0x0055666D
		public CavernShader(Color backColor, Color frontColor, float speed)
		{
			this._backColor = backColor.ToVector4();
			this._frontColor = frontColor.ToVector4();
			this._speed = speed;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x00558498 File Offset: 0x00556698
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._backColor, this._frontColor, (float)Math.Sin((double)(time * this._speed + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x005584FC File Offset: 0x005566FC
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= this._speed * 0.5f;
			float num = time % 1f;
			bool flag = time % 2f > 1f;
			Vector4 vector = (flag ? this._frontColor : this._backColor);
			Vector4 vector2 = (flag ? this._backColor : this._frontColor);
			num *= 1.2f;
			for (int i = 0; i < fragment.Count; i++)
			{
				float num2 = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * 0.5f + new Vector2(0f, time * 0.5f));
				Vector4 vector3 = vector;
				num2 += num;
				if (num2 > 0.999f)
				{
					float num3 = MathHelper.Clamp((num2 - 0.999f) / 0.2f, 0f, 1f);
					vector3 = Vector4.Lerp(vector3, vector2, num3);
				}
				fragment.SetColor(i, vector3);
			}
		}

		// Token: 0x04005006 RID: 20486
		private readonly Vector4 _backColor;

		// Token: 0x04005007 RID: 20487
		private readonly Vector4 _frontColor;

		// Token: 0x04005008 RID: 20488
		private readonly float _speed;
	}
}
