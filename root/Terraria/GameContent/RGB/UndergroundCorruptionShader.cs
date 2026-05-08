using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002CA RID: 714
	public class UndergroundCorruptionShader : ChromaShader
	{
		// Token: 0x060025E9 RID: 9705 RVA: 0x0055A740 File Offset: 0x00558940
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = Vector4.Lerp(this._flameColor, this._flameTipColor, 0.25f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector2 = Vector4.Lerp(this._corruptionColor, vector, (float)Math.Sin((double)(time + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x0055A7B0 File Offset: 0x005589B0
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.05f), time * 0.1f);
				num = Math.Max(0f, 1f - num * num * 4f * (1.2f - canvasPositionOfIndex.Y)) * canvasPositionOfIndex.Y;
				num = MathHelper.Clamp(num, 0f, 1f);
				Vector4 vector = Vector4.Lerp(this._flameColor, this._flameTipColor, num);
				Vector4 vector2 = Vector4.Lerp(this._corruptionColor, vector, num);
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x0055A880 File Offset: 0x00558A80
		public UndergroundCorruptionShader()
		{
		}

		// Token: 0x04005048 RID: 20552
		private readonly Vector4 _corruptionColor = new Vector4(Color.Purple.ToVector3() * 0.2f, 1f);

		// Token: 0x04005049 RID: 20553
		private readonly Vector4 _flameColor = Color.Green.ToVector4();

		// Token: 0x0400504A RID: 20554
		private readonly Vector4 _flameTipColor = Color.Yellow.ToVector4();
	}
}
