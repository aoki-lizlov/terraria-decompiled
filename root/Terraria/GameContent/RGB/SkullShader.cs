using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002AE RID: 686
	public class SkullShader : ChromaShader
	{
		// Token: 0x0600258B RID: 9611 RVA: 0x00557A18 File Offset: 0x00555C18
		public SkullShader(Color skullColor, Color bloodDark, Color bloodLight)
		{
			this._skullColor = skullColor.ToVector4();
			this._bloodDark = bloodDark.ToVector4();
			this._bloodLight = bloodLight.ToVector4();
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00557A68 File Offset: 0x00555C68
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._skullColor, this._bloodLight, (float)Math.Sin((double)(time * 2f + canvasPositionOfIndex.X * 2f)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00557AD0 File Offset: 0x00555CD0
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 vector = this._backgroundColor;
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10f + time * 0.75f) % 10f + canvasPositionOfIndex.Y - 1f;
				if (num > 0f)
				{
					float num2 = Math.Max(0f, 1.2f - num);
					if (num < 0.2f)
					{
						num2 = num * 5f;
					}
					vector = Vector4.Lerp(vector, this._skullColor, num2);
				}
				float num3 = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.5f + new Vector2(12.5f, time * 0.2f));
				num3 = Math.Max(0f, 1f - num3 * num3 * 4f * num3 * (1f - canvasPositionOfIndex.Y * canvasPositionOfIndex.Y)) * canvasPositionOfIndex.Y * canvasPositionOfIndex.Y;
				num3 = MathHelper.Clamp(num3, 0f, 1f);
				Vector4 vector2 = Vector4.Lerp(this._bloodDark, this._bloodLight, num3);
				vector = Vector4.Lerp(vector, vector2, num3);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004FEF RID: 20463
		private readonly Vector4 _skullColor;

		// Token: 0x04004FF0 RID: 20464
		private readonly Vector4 _bloodDark;

		// Token: 0x04004FF1 RID: 20465
		private readonly Vector4 _bloodLight;

		// Token: 0x04004FF2 RID: 20466
		private readonly Vector4 _backgroundColor = Color.Black.ToVector4();
	}
}
