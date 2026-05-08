using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002C2 RID: 706
	public class SandstormShader : ChromaShader
	{
		// Token: 0x060025CD RID: 9677 RVA: 0x00559B74 File Offset: 0x00557D74
		[RgbProcessor(new EffectDetailLevel[] { 0, 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (quality == null)
			{
				time *= 0.25f;
			}
			for (int i = 0; i < fragment.Count; i++)
			{
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * 0.3f + new Vector2(time, -time) * 0.5f);
				Vector4 vector = Vector4.Lerp(this._backColor, this._frontColor, staticNoise);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x00559BEC File Offset: 0x00557DEC
		public SandstormShader()
		{
		}

		// Token: 0x04005031 RID: 20529
		private readonly Vector4 _backColor = new Vector4(0.2f, 0f, 0f, 1f);

		// Token: 0x04005032 RID: 20530
		private readonly Vector4 _frontColor = new Vector4(1f, 0.5f, 0f, 1f);
	}
}
