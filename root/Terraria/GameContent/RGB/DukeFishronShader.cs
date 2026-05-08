using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200029F RID: 671
	public class DukeFishronShader : ChromaShader
	{
		// Token: 0x0600255F RID: 9567 RVA: 0x00555EFB File Offset: 0x005540FB
		public DukeFishronShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00555F20 File Offset: 0x00554120
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, Math.Max(0f, (float)Math.Sin((double)(time * 2f + canvasPositionOfIndex.X))));
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00555F80 File Offset: 0x00554180
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				ref Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetGridPositionOfIndex(i).Y, time);
				float num = (float)Math.Sin((double)(canvasPositionOfIndex.X + 2f * time + dynamicNoise)) - 0.2f;
				num = Math.Max(0f, num);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004FC6 RID: 20422
		private readonly Vector4 _primaryColor;

		// Token: 0x04004FC7 RID: 20423
		private readonly Vector4 _secondaryColor;
	}
}
