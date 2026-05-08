using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002A8 RID: 680
	public class MartianMadnessShader : ChromaShader
	{
		// Token: 0x06002579 RID: 9593 RVA: 0x00556F3A File Offset: 0x0055513A
		public MartianMadnessShader(Color metalColor, Color glassColor, Color beamColor, Color backgroundColor)
		{
			this._metalColor = metalColor.ToVector4();
			this._glassColor = glassColor.ToVector4();
			this._beamColor = beamColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00556F78 File Offset: 0x00555178
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float num = (float)Math.Sin((double)(time * 2f + canvasPositionOfIndex.X * 5f)) * 0.5f + 0.5f;
				int num2 = (gridPositionOfIndex.X + gridPositionOfIndex.Y) % 2;
				if (num2 < 0)
				{
					num2 += 2;
				}
				Vector4 vector = ((num2 == 1) ? Vector4.Lerp(this._glassColor, this._beamColor, num) : this._metalColor);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0055701C File Offset: 0x0055521C
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (device.Type != null && device.Type != 6)
			{
				this.ProcessLowDetail(device, fragment, quality, time);
				return;
			}
			float num = time * 0.5f % 6.2831855f;
			if (num > 3.1415927f)
			{
				num = 6.2831855f - num;
			}
			Vector2 vector = new Vector2(1.7f + (float)Math.Cos((double)num) * 2f, -0.5f + (float)Math.Sin((double)num) * 1.1f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector2 = this._backgroundColor;
				float num2 = Math.Abs(vector.X - canvasPositionOfIndex.X);
				if (canvasPositionOfIndex.Y > vector.Y && num2 < 0.2f)
				{
					float num3 = 1f - MathHelper.Clamp((num2 - 0.2f + 0.2f) / 0.2f, 0f, 1f);
					float num4 = Math.Abs((num - 1.5707964f) / 1.5707964f);
					num4 = Math.Max(0f, 1f - num4 * 3f);
					vector2 = Vector4.Lerp(vector2, this._beamColor, num3 * num4);
				}
				Vector2 vector3 = vector - canvasPositionOfIndex;
				vector3.X /= 1f;
				vector3.Y /= 0.2f;
				float num5 = vector3.Length();
				if (num5 < 1f)
				{
					float num6 = 1f - MathHelper.Clamp((num5 - 1f + 0.2f) / 0.2f, 0f, 1f);
					vector2 = Vector4.Lerp(vector2, this._metalColor, num6);
				}
				Vector2 vector4 = vector - canvasPositionOfIndex + new Vector2(0f, -0.1f);
				vector4.X /= 0.3f;
				vector4.Y /= 0.3f;
				if (vector4.Y < 0f)
				{
					vector4.Y *= 2f;
				}
				float num7 = vector4.Length();
				if (num7 < 1f)
				{
					float num8 = 1f - MathHelper.Clamp((num7 - 1f + 0.2f) / 0.2f, 0f, 1f);
					vector2 = Vector4.Lerp(vector2, this._glassColor, num8);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04004FDE RID: 20446
		private readonly Vector4 _metalColor;

		// Token: 0x04004FDF RID: 20447
		private readonly Vector4 _glassColor;

		// Token: 0x04004FE0 RID: 20448
		private readonly Vector4 _beamColor;

		// Token: 0x04004FE1 RID: 20449
		private readonly Vector4 _backgroundColor;
	}
}
