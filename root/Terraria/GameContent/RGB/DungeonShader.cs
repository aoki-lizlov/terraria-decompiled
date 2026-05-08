using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002B8 RID: 696
	public class DungeonShader : ChromaShader
	{
		// Token: 0x060025AB RID: 9643 RVA: 0x005589E0 File Offset: 0x00556BE0
		[RgbProcessor(new EffectDetailLevel[] { 0, 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = ((NoiseHelper.GetStaticNoise(gridPositionOfIndex.Y) * 10f + time) % 10f - (canvasPositionOfIndex.X + 2f)) * 0.5f;
				Vector4 vector = this._backgroundColor;
				if (num > 0f)
				{
					float num2 = Math.Max(0f, 1.2f - num);
					float num3 = MathHelper.Clamp(num2 * num2 * num2, 0f, 1f);
					if (num < 0.2f)
					{
						num2 = num / 0.2f;
					}
					Vector4 vector2 = Vector4.Lerp(this._spiritTrailColor, this._spiritColor, num3);
					vector = Vector4.Lerp(vector, vector2, num2);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00558AB4 File Offset: 0x00556CB4
		public DungeonShader()
		{
		}

		// Token: 0x0400500E RID: 20494
		private readonly Vector4 _backgroundColor = new Color(5, 5, 5).ToVector4();

		// Token: 0x0400500F RID: 20495
		private readonly Vector4 _spiritTrailColor = new Color(6, 51, 222).ToVector4();

		// Token: 0x04005010 RID: 20496
		private readonly Vector4 _spiritColor = Color.White.ToVector4();
	}
}
