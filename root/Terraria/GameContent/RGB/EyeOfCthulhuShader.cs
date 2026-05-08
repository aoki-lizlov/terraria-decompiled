using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002A1 RID: 673
	public class EyeOfCthulhuShader : ChromaShader
	{
		// Token: 0x06002566 RID: 9574 RVA: 0x00556400 File Offset: 0x00554600
		public EyeOfCthulhuShader(Color eyeColor, Color veinColor, Color backgroundColor)
		{
			this._eyeColor = eyeColor.ToVector4();
			this._veinColor = veinColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00556430 File Offset: 0x00554630
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._veinColor, this._eyeColor, (float)Math.Sin((double)(time + canvasPositionOfIndex.X * 4f)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x00556494 File Offset: 0x00554694
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (device.Type != null && device.Type != 6)
			{
				this.ProcessLowDetail(device, fragment, quality, time);
				return;
			}
			float num = time * 0.2f % 2f;
			int num2 = 1;
			if (num > 1f)
			{
				num = 2f - num;
				num2 = -1;
			}
			Vector2 vector = new Vector2(num * 7f - 3.5f, 0f) + fragment.CanvasCenter;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector2 = this._backgroundColor;
				Vector2 vector3 = canvasPositionOfIndex - vector;
				float num3 = vector3.Length();
				if (num3 < 0.5f)
				{
					float num4 = 1f - MathHelper.Clamp((num3 - 0.5f + 0.2f) / 0.2f, 0f, 1f);
					float num5 = MathHelper.Clamp((vector3.X + 0.5f - 0.2f) / 0.6f, 0f, 1f);
					if (num2 == 1)
					{
						num5 = 1f - num5;
					}
					Vector4 vector4 = Vector4.Lerp(this._eyeColor, this._veinColor, num5);
					vector2 = Vector4.Lerp(vector2, vector4, num4);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04004FC9 RID: 20425
		private readonly Vector4 _eyeColor;

		// Token: 0x04004FCA RID: 20426
		private readonly Vector4 _veinColor;

		// Token: 0x04004FCB RID: 20427
		private readonly Vector4 _backgroundColor;
	}
}
