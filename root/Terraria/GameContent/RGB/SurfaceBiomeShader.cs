using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002C4 RID: 708
	public class SurfaceBiomeShader : ChromaShader
	{
		// Token: 0x060025D2 RID: 9682 RVA: 0x00559D80 File Offset: 0x00557F80
		public SurfaceBiomeShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x00559DA4 File Offset: 0x00557FA4
		public override void Update(float elapsedTime)
		{
			this._surfaceColor = Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f;
			if (Main.dayTime)
			{
				float num = (float)(Main.time / 54000.0);
				if (num < 0.25f)
				{
					this._starVisibility = 1f - num / 0.25f;
					return;
				}
				if (num > 0.75f)
				{
					this._starVisibility = (num - 0.75f) / 0.25f;
					return;
				}
			}
			else
			{
				this._starVisibility = 1f;
			}
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00559E3C File Offset: 0x0055803C
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = this._primaryColor * this._surfaceColor;
			Vector4 vector2 = this._secondaryColor * this._surfaceColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector3 = Vector4.Lerp(vector, vector2, (float)Math.Sin((double)(time * 0.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector3);
			}
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x00559EBC File Offset: 0x005580BC
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = this._primaryColor * this._surfaceColor;
			Vector4 vector2 = this._secondaryColor * this._surfaceColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float num = (float)Math.Sin((double)(canvasPositionOfIndex.X * 1.5f + canvasPositionOfIndex.Y + time)) * 0.5f + 0.5f;
				Vector4 vector3 = Vector4.Lerp(vector, vector2, num);
				float num2 = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 60f);
				num2 = Math.Max(0f, 1f - num2 * 20f);
				num2 *= 1f - this._surfaceColor.X;
				vector3 = Vector4.Max(vector3, new Vector4(num2 * this._starVisibility));
				fragment.SetColor(i, vector3);
			}
		}

		// Token: 0x04005034 RID: 20532
		private readonly Vector4 _primaryColor;

		// Token: 0x04005035 RID: 20533
		private readonly Vector4 _secondaryColor;

		// Token: 0x04005036 RID: 20534
		private Vector4 _surfaceColor;

		// Token: 0x04005037 RID: 20535
		private float _starVisibility;
	}
}
