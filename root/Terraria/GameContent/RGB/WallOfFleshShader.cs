using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002B1 RID: 689
	public class WallOfFleshShader : ChromaShader
	{
		// Token: 0x06002595 RID: 9621 RVA: 0x00558117 File Offset: 0x00556317
		public WallOfFleshShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0055813C File Offset: 0x0055633C
		[RgbProcessor(new EffectDetailLevel[] { 1, 0 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = this._secondaryColor;
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f, time / 5f);
				num = Math.Max(0f, 1f - num * 2f);
				vector = Vector4.Lerp(vector, this._primaryColor, (float)Math.Sqrt((double)num) * 0.75f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004FFD RID: 20477
		private readonly Vector4 _primaryColor;

		// Token: 0x04004FFE RID: 20478
		private readonly Vector4 _secondaryColor;
	}
}
