using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002CD RID: 717
	public class UndergroundMushroomShader : ChromaShader
	{
		// Token: 0x060025F2 RID: 9714 RVA: 0x0055ABC4 File Offset: 0x00558DC4
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._edgeGlowColor, this._sporeColor, (float)Math.Sin((double)(time * 0.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0055AC28 File Offset: 0x00558E28
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = this._baseColor;
				float num = ((NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10f + time * 0.2f) % 10f - (1f - canvasPositionOfIndex.Y)) * 2f;
				if (num > 0f)
				{
					float num2 = Math.Max(0f, 1.5f - num);
					if (num < 0.5f)
					{
						num2 = num * 2f;
					}
					vector = Vector4.Lerp(vector, this._sporeColor, num2);
				}
				float num3 = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(0f, time * 0.1f));
				num3 = Math.Max(0f, 1f - num3 * (1f + (1f - canvasPositionOfIndex.Y) * 4f));
				num3 *= Math.Max(0f, (canvasPositionOfIndex.Y - 0.3f) / 0.7f);
				vector = Vector4.Lerp(vector, this._edgeGlowColor, num3);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0055AD60 File Offset: 0x00558F60
		public UndergroundMushroomShader()
		{
		}

		// Token: 0x04005051 RID: 20561
		private readonly Vector4 _baseColor = new Color(10, 10, 10).ToVector4();

		// Token: 0x04005052 RID: 20562
		private readonly Vector4 _edgeGlowColor = new Color(0, 0, 255).ToVector4();

		// Token: 0x04005053 RID: 20563
		private readonly Vector4 _sporeColor = new Color(255, 230, 150).ToVector4();
	}
}
