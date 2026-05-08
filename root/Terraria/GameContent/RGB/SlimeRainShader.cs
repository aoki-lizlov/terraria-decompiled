using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002C3 RID: 707
	public class SlimeRainShader : ChromaShader
	{
		// Token: 0x060025CF RID: 9679 RVA: 0x00559C40 File Offset: 0x00557E40
		[RgbProcessor(new EffectDetailLevel[] { 1 }, IsTransparent = true)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = new Vector4(0f, 0f, 0f, 0.75f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 7f + time * 0.4f) % 7f - canvasPositionOfIndex.Y;
				Vector4 vector2 = vector;
				if (num > 0f)
				{
					float num2 = Math.Max(0f, 1.2f - num);
					if (num < 0.4f)
					{
						num2 = num / 0.4f;
					}
					int num3 = (gridPositionOfIndex.X % SlimeRainShader._colors.Length + SlimeRainShader._colors.Length) % SlimeRainShader._colors.Length;
					vector2 = Vector4.Lerp(vector2, SlimeRainShader._colors[num3], num2);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x00556259 File Offset: 0x00554459
		public SlimeRainShader()
		{
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x00559D2C File Offset: 0x00557F2C
		// Note: this type is marked as 'beforefieldinit'.
		static SlimeRainShader()
		{
		}

		// Token: 0x04005033 RID: 20531
		private static readonly Vector4[] _colors = new Vector4[]
		{
			Color.Blue.ToVector4(),
			Color.Green.ToVector4(),
			Color.Purple.ToVector4()
		};
	}
}
