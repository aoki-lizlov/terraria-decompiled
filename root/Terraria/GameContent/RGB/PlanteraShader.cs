using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002AB RID: 683
	public class PlanteraShader : ChromaShader
	{
		// Token: 0x06002582 RID: 9602 RVA: 0x005575F5 File Offset: 0x005557F5
		public PlanteraShader(Color bulbColor, Color vineColor, Color backgroundColor)
		{
			this._bulbColor = bulbColor.ToVector4();
			this._vineColor = vineColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x00557624 File Offset: 0x00555824
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._bulbColor, this._vineColor, (float)Math.Sin((double)(time * 2f + canvasPositionOfIndex.X * 10f)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x0055768C File Offset: 0x0055588C
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				canvasPositionOfIndex.X -= 1.8f;
				if (canvasPositionOfIndex.X < 0f)
				{
					canvasPositionOfIndex.X *= -1f;
					gridPositionOfIndex.Y += 101;
				}
				float num = NoiseHelper.GetStaticNoise(gridPositionOfIndex.Y);
				num = (num * 5f + time * 0.4f) % 5f;
				float num2 = 1f;
				if (num > 1f)
				{
					num2 = 1f - MathHelper.Clamp((num - 0.4f - 1f) / 0.4f, 0f, 1f);
					num = 1f;
				}
				float num3 = num - canvasPositionOfIndex.X / 5f;
				Vector4 vector = this._backgroundColor;
				if (num3 > 0f)
				{
					float num4 = 1f;
					if (num3 < 0.2f)
					{
						num4 = num3 / 0.2f;
					}
					if ((gridPositionOfIndex.X + 7 * gridPositionOfIndex.Y) % 5 == 0)
					{
						vector = Vector4.Lerp(this._backgroundColor, this._bulbColor, num4 * num2);
					}
					else
					{
						vector = Vector4.Lerp(this._backgroundColor, this._vineColor, num4 * num2);
					}
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004FE8 RID: 20456
		private readonly Vector4 _bulbColor;

		// Token: 0x04004FE9 RID: 20457
		private readonly Vector4 _vineColor;

		// Token: 0x04004FEA RID: 20458
		private readonly Vector4 _backgroundColor;
	}
}
