using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002B3 RID: 691
	public class BlizzardShader : ChromaShader
	{
		// Token: 0x0600259B RID: 9627 RVA: 0x00558378 File Offset: 0x00556578
		public BlizzardShader(Vector4 frontColor, Vector4 backColor, float panSpeedX, float panSpeedY)
		{
			this._frontColor = frontColor;
			this._backColor = backColor;
			this._timeScaleX = panSpeedX;
			this._timeScaleY = panSpeedY;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x005583E8 File Offset: 0x005565E8
		[RgbProcessor(new EffectDetailLevel[] { 0, 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (quality == null)
			{
				time *= 0.25f;
			}
			for (int i = 0; i < fragment.Count; i++)
			{
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * new Vector2(0.2f, 0.4f) + new Vector2(time * this._timeScaleX, time * this._timeScaleY));
				Vector4 vector = Vector4.Lerp(this._backColor, this._frontColor, staticNoise * staticNoise);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04005002 RID: 20482
		private readonly Vector4 _backColor = new Vector4(0.1f, 0.1f, 0.3f, 1f);

		// Token: 0x04005003 RID: 20483
		private readonly Vector4 _frontColor = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x04005004 RID: 20484
		private readonly float _timeScaleX;

		// Token: 0x04005005 RID: 20485
		private readonly float _timeScaleY;
	}
}
