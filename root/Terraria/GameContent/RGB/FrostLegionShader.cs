using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002A2 RID: 674
	public class FrostLegionShader : ChromaShader
	{
		// Token: 0x06002569 RID: 9577 RVA: 0x005565D5 File Offset: 0x005547D5
		public FrostLegionShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x005565F8 File Offset: 0x005547F8
		[RgbProcessor(new EffectDetailLevel[] { 1, 0 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetGridPositionOfIndex(i).X / 2);
				float num = (canvasPositionOfIndex.Y + canvasPositionOfIndex.X / 2f - staticNoise + time) % 2f;
				if (num < 0f)
				{
					num += 2f;
				}
				if (num < 0.2f)
				{
					num = 1f - num / 0.2f;
				}
				float num2 = num / 2f;
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, num2);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004FCC RID: 20428
		private readonly Vector4 _primaryColor;

		// Token: 0x04004FCD RID: 20429
		private readonly Vector4 _secondaryColor;
	}
}
