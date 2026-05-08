using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002A9 RID: 681
	public class PillarShader : ChromaShader
	{
		// Token: 0x0600257C RID: 9596 RVA: 0x0055727C File Offset: 0x0055547C
		public PillarShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x005572A0 File Offset: 0x005554A0
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, (float)Math.Sin((double)(time * 2.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00557304 File Offset: 0x00555504
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector2 vector = new Vector2(1.5f, 0.5f);
			time *= 4f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 vector2 = fragment.GetCanvasPositionOfIndex(i) - vector;
				float num = vector2.Length() * 2f;
				float num2 = (float)Math.Atan2((double)vector2.Y, (double)vector2.X);
				float num3 = (float)Math.Sin((double)(num * 4f - time - num2)) * 0.5f + 0.5f;
				Vector4 vector3 = Vector4.Lerp(this._primaryColor, this._secondaryColor, num3);
				if (num < 1f)
				{
					float num4 = num / 1f;
					num4 *= num4 * num4;
					float num5 = (float)Math.Sin((double)(4f - time - num2)) * 0.5f + 0.5f;
					vector3 = Vector4.Lerp(this._primaryColor, this._secondaryColor, num5) * num4;
				}
				vector3.W = 1f;
				fragment.SetColor(i, vector3);
			}
		}

		// Token: 0x04004FE2 RID: 20450
		private readonly Vector4 _primaryColor;

		// Token: 0x04004FE3 RID: 20451
		private readonly Vector4 _secondaryColor;
	}
}
