using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200029B RID: 667
	public class BrainShader : ChromaShader
	{
		// Token: 0x06002556 RID: 9558 RVA: 0x005558CD File Offset: 0x00553ACD
		public BrainShader(Color brainColor, Color veinColor)
		{
			this._brainColor = brainColor.ToVector4();
			this._veinColor = veinColor.ToVector4();
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x005558F0 File Offset: 0x00553AF0
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = Vector4.Lerp(this._brainColor, this._veinColor, Math.Max(0f, (float)Math.Sin((double)(time * 3f))));
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00555944 File Offset: 0x00553B44
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			new Vector2(1.6f, 0.5f);
			Vector4 vector = Vector4.Lerp(this._brainColor, this._veinColor, Math.Max(0f, (float)Math.Sin((double)(time * 3f))) * 0.5f + 0.5f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector2 = this._brainColor;
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.15f + new Vector2(time * 0.002f), time * 0.03f);
				num = (float)Math.Sin((double)(num * 10f)) * 0.5f + 0.5f;
				num = Math.Max(0f, 1f - 5f * num);
				vector2 = Vector4.Lerp(vector2, vector, num);
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04004FB9 RID: 20409
		private readonly Vector4 _brainColor;

		// Token: 0x04004FBA RID: 20410
		private readonly Vector4 _veinColor;
	}
}
