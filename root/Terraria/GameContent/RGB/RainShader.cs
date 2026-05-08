using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002C1 RID: 705
	public class RainShader : ChromaShader
	{
		// Token: 0x060025CA RID: 9674 RVA: 0x00559A6C File Offset: 0x00557C6C
		public override void Update(float elapsedTime)
		{
			this._inBloodMoon = Main.bloodMoon;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x00559A7C File Offset: 0x00557C7C
		[RgbProcessor(new EffectDetailLevel[] { 1 }, IsTransparent = true)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector;
			if (this._inBloodMoon)
			{
				vector = new Vector4(1f, 0f, 0f, 1f);
			}
			else
			{
				vector = new Vector4(0f, 0f, 1f, 1f);
			}
			Vector4 vector2 = new Vector4(0f, 0f, 0f, 0.75f);
			for (int i = 0; i < fragment.Count; i++)
			{
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10f + time) % 10f - canvasPositionOfIndex.Y;
				Vector4 vector3 = vector2;
				if (num > 0f)
				{
					float num2 = Math.Max(0f, 1.2f - num);
					if (num < 0.2f)
					{
						num2 = num * 5f;
					}
					vector3 = Vector4.Lerp(vector3, vector, num2);
				}
				fragment.SetColor(i, vector3);
			}
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00556259 File Offset: 0x00554459
		public RainShader()
		{
		}

		// Token: 0x04005030 RID: 20528
		private bool _inBloodMoon;
	}
}
