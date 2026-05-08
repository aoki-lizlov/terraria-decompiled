using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002C9 RID: 713
	public class TempleShader : ChromaShader
	{
		// Token: 0x060025E7 RID: 9703 RVA: 0x0055A650 File Offset: 0x00558850
		[RgbProcessor(new EffectDetailLevel[] { 0, 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 vector = this._backgroundColor;
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.Y * 7) * 10f + time) % 10f - (canvasPositionOfIndex.X + 2f);
				if (num > 0f)
				{
					float num2 = Math.Max(0f, 1.2f - num);
					if (num < 0.2f)
					{
						num2 = num * 5f;
					}
					vector = Vector4.Lerp(vector, this._glowColor, num2);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0055A6F8 File Offset: 0x005588F8
		public TempleShader()
		{
		}

		// Token: 0x04005046 RID: 20550
		private readonly Vector4 _backgroundColor = new Vector4(0.05f, 0.025f, 0f, 1f);

		// Token: 0x04005047 RID: 20551
		private readonly Vector4 _glowColor = Color.Orange.ToVector4();
	}
}
