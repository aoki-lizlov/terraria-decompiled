using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002A5 RID: 677
	public class GolemShader : ChromaShader
	{
		// Token: 0x06002570 RID: 9584 RVA: 0x00556A37 File Offset: 0x00554C37
		public GolemShader(Color glowColor, Color coreColor, Color backgroundColor)
		{
			this._glowColor = glowColor.ToVector4();
			this._coreColor = coreColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00556A68 File Offset: 0x00554C68
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = Vector4.Lerp(this._backgroundColor, this._coreColor, Math.Max(0f, (float)Math.Sin((double)(time * 0.5f))));
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector2 = Vector4.Lerp(vector, this._glowColor, Math.Max(0f, (float)Math.Sin((double)(canvasPositionOfIndex.X * 2f + time + 101f))));
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x00556AF4 File Offset: 0x00554CF4
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float num = 0.5f + (float)Math.Sin((double)(time * 3f)) * 0.1f;
			Vector2 vector = new Vector2(1.6f, 0.5f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 vector2 = this._backgroundColor;
				float num2 = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.Y) * 10f + time * 2f) % 10f - Math.Abs(canvasPositionOfIndex.X - vector.X);
				if (num2 > 0f)
				{
					float num3 = Math.Max(0f, 1.2f - num2);
					if (num2 < 0.2f)
					{
						num3 = num2 * 5f;
					}
					vector2 = Vector4.Lerp(vector2, this._glowColor, num3);
				}
				float num4 = (canvasPositionOfIndex - vector).Length();
				if (num4 < num)
				{
					float num5 = 1f - MathHelper.Clamp((num4 - num + 0.1f) / 0.1f, 0f, 1f);
					vector2 = Vector4.Lerp(vector2, this._coreColor, num5);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04004FD6 RID: 20438
		private readonly Vector4 _glowColor;

		// Token: 0x04004FD7 RID: 20439
		private readonly Vector4 _coreColor;

		// Token: 0x04004FD8 RID: 20440
		private readonly Vector4 _backgroundColor;
	}
}
