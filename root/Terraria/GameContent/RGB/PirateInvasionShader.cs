using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002AA RID: 682
	public class PirateInvasionShader : ChromaShader
	{
		// Token: 0x0600257F RID: 9599 RVA: 0x00557418 File Offset: 0x00555618
		public PirateInvasionShader(Color cannonBallColor, Color splashColor, Color waterColor, Color backgroundColor)
		{
			this._cannonBallColor = cannonBallColor.ToVector4();
			this._splashColor = splashColor.ToVector4();
			this._waterColor = waterColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x00557454 File Offset: 0x00555654
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._waterColor, this._cannonBallColor, (float)Math.Sin((double)(time * 0.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x005574B8 File Offset: 0x005556B8
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				gridPositionOfIndex.X /= 2;
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 40f + time * 1f) % 40f;
				float num2 = 0f;
				float num3 = num - canvasPositionOfIndex.Y / 1.2f;
				if (num > 1f)
				{
					float num4 = 1f - canvasPositionOfIndex.Y / 1.2f;
					num2 = (1f - Math.Min(1f, num3 - num4)) * (1f - Math.Min(1f, num4 / 1f));
				}
				Vector4 vector = this._backgroundColor;
				if (num3 > 0f)
				{
					float num5 = Math.Max(0f, 1.2f - num3 * 4f);
					if (num3 < 0.1f)
					{
						num5 = num3 / 0.1f;
					}
					vector = Vector4.Lerp(vector, this._cannonBallColor, num5);
					vector = Vector4.Lerp(vector, this._splashColor, num2);
				}
				if (canvasPositionOfIndex.Y > 0.8f)
				{
					vector = this._waterColor;
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004FE4 RID: 20452
		private readonly Vector4 _cannonBallColor;

		// Token: 0x04004FE5 RID: 20453
		private readonly Vector4 _splashColor;

		// Token: 0x04004FE6 RID: 20454
		private readonly Vector4 _waterColor;

		// Token: 0x04004FE7 RID: 20455
		private readonly Vector4 _backgroundColor;
	}
}
