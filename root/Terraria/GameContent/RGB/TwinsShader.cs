using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002AF RID: 687
	public class TwinsShader : ChromaShader
	{
		// Token: 0x0600258E RID: 9614 RVA: 0x00557C18 File Offset: 0x00555E18
		public TwinsShader(Color eyeColor, Color veinColor, Color laserColor, Color mouthColor, Color flameColor, Color backgroundColor)
		{
			this._eyeColor = eyeColor.ToVector4();
			this._veinColor = veinColor.ToVector4();
			this._laserColor = laserColor.ToVector4();
			this._mouthColor = mouthColor.ToVector4();
			this._flameColor = flameColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x00557C7C File Offset: 0x00555E7C
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._veinColor, this._eyeColor, (float)Math.Sin((double)(time + canvasPositionOfIndex.X * 4f)) * 0.5f + 0.5f);
				float num = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 25f);
				num = Math.Max(0f, 1f - num * 5f);
				vector = Vector4.Lerp(vector, TwinsShader._irisColors[((gridPositionOfIndex.Y * 47 + gridPositionOfIndex.X) % TwinsShader._irisColors.Length + TwinsShader._irisColors.Length) % TwinsShader._irisColors.Length], num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00557D5C File Offset: 0x00555F5C
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (device.Type != null && device.Type != 6)
			{
				this.ProcessLowDetail(device, fragment, quality, time);
				return;
			}
			bool flag = true;
			float num = time * 0.1f % 2f;
			if (num > 1f)
			{
				num = 2f - num;
				flag = false;
			}
			Vector2 vector = new Vector2(num * 7f - 3.5f, 0f) + fragment.CanvasCenter;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 vector2 = this._backgroundColor;
				Vector2 vector3 = canvasPositionOfIndex - vector;
				float num2 = vector3.Length();
				if (num2 < 0.5f)
				{
					float num3 = 1f - MathHelper.Clamp((num2 - 0.5f + 0.2f) / 0.2f, 0f, 1f);
					float num4 = MathHelper.Clamp((vector3.X + 0.5f - 0.2f) / 0.6f, 0f, 1f);
					if (flag)
					{
						num4 = 1f - num4;
					}
					Vector4 vector4 = Vector4.Lerp(this._eyeColor, this._veinColor, num4);
					float num5 = (float)Math.Atan2((double)vector3.Y, (double)vector3.X);
					if (!flag && 3.1415927f - Math.Abs(num5) < 0.6f)
					{
						vector4 = this._mouthColor;
					}
					vector2 = Vector4.Lerp(vector2, vector4, num3);
				}
				if (flag && gridPositionOfIndex.Y == 3 && canvasPositionOfIndex.X > vector.X)
				{
					float num6 = 1f - Math.Abs(canvasPositionOfIndex.X - vector.X * 2f - 0.5f) / 0.5f;
					vector2 = Vector4.Lerp(vector2, this._laserColor, MathHelper.Clamp(num6, 0f, 1f));
				}
				else if (!flag)
				{
					Vector2 vector5 = canvasPositionOfIndex - (vector - new Vector2(1.2f, 0f));
					vector5.Y *= 3.5f;
					float num7 = vector5.Length();
					if (num7 < 0.7f)
					{
						float num8 = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex, time);
						num8 = num8 * num8 * num8;
						num8 *= 1f - MathHelper.Clamp((num7 - 0.7f + 0.3f) / 0.3f, 0f, 1f);
						vector2 = Vector4.Lerp(vector2, this._flameColor, num8);
					}
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00557FE8 File Offset: 0x005561E8
		// Note: this type is marked as 'beforefieldinit'.
		static TwinsShader()
		{
		}

		// Token: 0x04004FF3 RID: 20467
		private readonly Vector4 _eyeColor;

		// Token: 0x04004FF4 RID: 20468
		private readonly Vector4 _veinColor;

		// Token: 0x04004FF5 RID: 20469
		private readonly Vector4 _laserColor;

		// Token: 0x04004FF6 RID: 20470
		private readonly Vector4 _mouthColor;

		// Token: 0x04004FF7 RID: 20471
		private readonly Vector4 _flameColor;

		// Token: 0x04004FF8 RID: 20472
		private readonly Vector4 _backgroundColor;

		// Token: 0x04004FF9 RID: 20473
		private static readonly Vector4[] _irisColors = new Vector4[]
		{
			Color.Green.ToVector4(),
			Color.Blue.ToVector4()
		};
	}
}
