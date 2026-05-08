using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200029E RID: 670
	public class DeathShader : ChromaShader
	{
		// Token: 0x0600255D RID: 9565 RVA: 0x00555E76 File Offset: 0x00554076
		public DeathShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00555E98 File Offset: 0x00554098
		[RgbProcessor(new EffectDetailLevel[] { 1, 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= 3f;
			float num = 0f;
			float num2 = time % 12.566371f;
			if (num2 < 3.1415927f)
			{
				num = (float)Math.Sin((double)num2);
			}
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004FC4 RID: 20420
		private readonly Vector4 _primaryColor;

		// Token: 0x04004FC5 RID: 20421
		private readonly Vector4 _secondaryColor;
	}
}
