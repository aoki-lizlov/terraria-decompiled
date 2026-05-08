using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002CB RID: 715
	public class DrippingShader : ChromaShader
	{
		// Token: 0x060025EC RID: 9708 RVA: 0x0055A8E0 File Offset: 0x00558AE0
		public DrippingShader(Color baseColor, Color liquidColor, float viscosity = 1f)
		{
			this._baseColor = baseColor.ToVector4();
			this._liquidColor = liquidColor.ToVector4();
			this._viscosity = viscosity;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0055A90C File Offset: 0x00558B0C
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._baseColor, this._liquidColor, (float)Math.Sin((double)(time * 0.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0055A970 File Offset: 0x00558B70
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * new Vector2(0.7f * this._viscosity, 0.075f) + new Vector2(0f, time * -0.1f * this._viscosity));
				num = Math.Max(0f, 1f - (canvasPositionOfIndex.Y * 4.5f + 0.5f) * num);
				Vector4 vector = this._baseColor;
				vector = Vector4.Lerp(vector, this._liquidColor, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400504B RID: 20555
		private readonly Vector4 _baseColor;

		// Token: 0x0400504C RID: 20556
		private readonly Vector4 _liquidColor;

		// Token: 0x0400504D RID: 20557
		private readonly float _viscosity;
	}
}
