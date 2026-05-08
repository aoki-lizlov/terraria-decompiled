using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002A7 RID: 679
	public class LavaIndicatorShader : ChromaShader
	{
		// Token: 0x06002576 RID: 9590 RVA: 0x00556D4E File Offset: 0x00554F4E
		public LavaIndicatorShader(Color backgroundColor, Color primaryColor, Color secondaryColor)
		{
			this._backgroundColor = backgroundColor.ToVector4();
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x00556D80 File Offset: 0x00554F80
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * 0.3f + new Vector2(12.5f, time * 0.2f));
				num = Math.Max(0f, 1f - num * num * 4f * num);
				num = MathHelper.Clamp(num, 0f, 1f);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x00556E18 File Offset: 0x00555018
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = this._backgroundColor;
				float dynamicNoise = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.2f, time * 0.5f);
				float num = 0.4f;
				num += dynamicNoise * 0.4f;
				float num2 = 1.1f - canvasPositionOfIndex.Y;
				if (num2 < num)
				{
					float num3 = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
					num3 = Math.Max(0f, 1f - num3 * num3 * 4f * num3);
					num3 = MathHelper.Clamp(num3, 0f, 1f);
					Vector4 vector2 = Vector4.Lerp(this._primaryColor, this._secondaryColor, num3);
					float num4 = 1f - MathHelper.Clamp((num2 - num + 0.2f) / 0.2f, 0f, 1f);
					vector = Vector4.Lerp(vector, vector2, num4);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004FDB RID: 20443
		private readonly Vector4 _backgroundColor;

		// Token: 0x04004FDC RID: 20444
		private readonly Vector4 _primaryColor;

		// Token: 0x04004FDD RID: 20445
		private readonly Vector4 _secondaryColor;
	}
}
