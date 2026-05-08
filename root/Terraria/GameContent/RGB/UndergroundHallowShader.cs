using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002CC RID: 716
	public class UndergroundHallowShader : ChromaShader
	{
		// Token: 0x060025EF RID: 9711 RVA: 0x0055AA28 File Offset: 0x00558C28
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._pinkCrystalColor, this._blueCrystalColor, (float)Math.Sin((double)(time * 2f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x0055AA8C File Offset: 0x00558C8C
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = this._baseColor;
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.4f, time * 0.05f);
				num = Math.Max(0f, 1f - 2.5f * num);
				float num2 = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.4f + new Vector2(0.05f, 0f), time * 0.05f);
				num2 = Math.Max(0f, 1f - 2.5f * num2);
				if (num > num2)
				{
					vector = Vector4.Lerp(vector, this._pinkCrystalColor, num);
				}
				else
				{
					vector = Vector4.Lerp(vector, this._blueCrystalColor, num2);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x0055AB68 File Offset: 0x00558D68
		public UndergroundHallowShader()
		{
		}

		// Token: 0x0400504E RID: 20558
		private readonly Vector4 _baseColor = new Color(0.05f, 0.05f, 0.05f).ToVector4();

		// Token: 0x0400504F RID: 20559
		private readonly Vector4 _pinkCrystalColor = Color.HotPink.ToVector4();

		// Token: 0x04005050 RID: 20560
		private readonly Vector4 _blueCrystalColor = Color.DeepSkyBlue.ToVector4();
	}
}
