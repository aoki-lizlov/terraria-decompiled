using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002A3 RID: 675
	public class GemCaveShader : ChromaShader
	{
		// Token: 0x0600256B RID: 9579 RVA: 0x005566A4 File Offset: 0x005548A4
		public GemCaveShader(Color primaryColor, Color secondaryColor, Vector4[] gemColors)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
			this._gemColors = gemColors;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x005566D0 File Offset: 0x005548D0
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= this.TimeRate;
			float num = time % 1f;
			bool flag = time % 2f > 1f;
			Vector4 vector = (flag ? this._secondaryColor : this._primaryColor);
			Vector4 vector2 = (flag ? this._primaryColor : this._secondaryColor);
			num *= 1.2f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float num2 = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.5f + new Vector2(0f, time * 0.5f));
				Vector4 vector3 = vector;
				num2 += num;
				if (num2 > 0.999f)
				{
					float num3 = MathHelper.Clamp((num2 - 0.999f) / 0.2f, 0f, 1f);
					vector3 = Vector4.Lerp(vector3, vector2, num3);
				}
				float num4 = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / this.CycleTime);
				num4 = Math.Max(0f, 1f - num4 * this.ColorRarity);
				vector3 = Vector4.Lerp(vector3, this._gemColors[((gridPositionOfIndex.Y * 47 + gridPositionOfIndex.X) % this._gemColors.Length + this._gemColors.Length) % this._gemColors.Length], num4);
				fragment.SetColor(i, vector3);
				fragment.SetColor(i, vector3);
			}
		}

		// Token: 0x04004FCE RID: 20430
		private readonly Vector4 _primaryColor;

		// Token: 0x04004FCF RID: 20431
		private readonly Vector4 _secondaryColor;

		// Token: 0x04004FD0 RID: 20432
		private readonly Vector4[] _gemColors;

		// Token: 0x04004FD1 RID: 20433
		public float CycleTime;

		// Token: 0x04004FD2 RID: 20434
		public float ColorRarity;

		// Token: 0x04004FD3 RID: 20435
		public float TimeRate;
	}
}
