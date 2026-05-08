using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002B0 RID: 688
	public class UnderworldShader : ChromaShader
	{
		// Token: 0x06002592 RID: 9618 RVA: 0x00558028 File Offset: 0x00556228
		public UnderworldShader(Color backColor, Color frontColor, float speed)
		{
			this._backColor = backColor.ToVector4();
			this._frontColor = frontColor.ToVector4();
			this._speed = speed;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x00558054 File Offset: 0x00556254
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._backColor, this._frontColor, (float)Math.Sin((double)(time * this._speed + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x005580B8 File Offset: 0x005562B8
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i) * 0.5f, time * this._speed / 3f);
				Vector4 vector = Vector4.Lerp(this._backColor, this._frontColor, dynamicNoise);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004FFA RID: 20474
		private readonly Vector4 _backColor;

		// Token: 0x04004FFB RID: 20475
		private readonly Vector4 _frontColor;

		// Token: 0x04004FFC RID: 20476
		private readonly float _speed;
	}
}
