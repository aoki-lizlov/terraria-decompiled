using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002A4 RID: 676
	public class GoblinArmyShader : ChromaShader
	{
		// Token: 0x0600256D RID: 9581 RVA: 0x00556845 File Offset: 0x00554A45
		public GoblinArmyShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00556868 File Offset: 0x00554A68
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= 0.5f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				canvasPositionOfIndex.Y = 1f;
				float num = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
				num = Math.Max(0f, 1f - num * num * 4f * num);
				num = MathHelper.Clamp(num, 0f, 1f);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				vector = Vector4.Lerp(vector, Vector4.One, num * num);
				Vector4 vector2 = Vector4.Lerp(new Vector4(0f, 0f, 0f, 1f), vector, num);
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x0055694C File Offset: 0x00554B4C
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
				num = Math.Max(0f, 1f - num * num * 4f * num * (1.2f - canvasPositionOfIndex.Y)) * canvasPositionOfIndex.Y * canvasPositionOfIndex.Y;
				num = MathHelper.Clamp(num, 0f, 1f);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				vector = Vector4.Lerp(vector, Vector4.One, num * num * num);
				Vector4 vector2 = Vector4.Lerp(new Vector4(0f, 0f, 0f, 1f), vector, num);
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04004FD4 RID: 20436
		private readonly Vector4 _primaryColor;

		// Token: 0x04004FD5 RID: 20437
		private readonly Vector4 _secondaryColor;
	}
}
