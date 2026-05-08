using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002C8 RID: 712
	public class SkyShader : ChromaShader
	{
		// Token: 0x060025E4 RID: 9700 RVA: 0x0055A3D9 File Offset: 0x005585D9
		public SkyShader(Color skyColor, Color spaceColor)
		{
			this._baseSkyColor = skyColor.ToVector4();
			this._baseSpaceColor = spaceColor.ToVector4();
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0055A3FC File Offset: 0x005585FC
		public override void Update(float elapsedTime)
		{
			float num = Main.player[Main.myPlayer].position.Y / 16f;
			this._backgroundTransition = MathHelper.Clamp((num - (float)Main.worldSurface * 0.25f) / ((float)Main.worldSurface * 0.1f), 0f, 1f);
			this._processedSkyColor = this._baseSkyColor * (Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f);
			this._processedCloudColor = Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f;
			if (Main.dayTime)
			{
				float num2 = (float)(Main.time / 54000.0);
				if (num2 < 0.25f)
				{
					this._starVisibility = 1f - num2 / 0.25f;
				}
				else if (num2 > 0.75f)
				{
					this._starVisibility = (num2 - 0.75f) / 0.25f;
				}
				else
				{
					this._starVisibility = 0f;
				}
			}
			else
			{
				this._starVisibility = 1f;
			}
			this._starVisibility = Math.Max(1f - this._backgroundTransition, this._starVisibility);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0055A544 File Offset: 0x00558744
		[RgbProcessor(new EffectDetailLevel[] { 0, 1 })]
		private void ProcessAnyDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * new Vector2(0.1f, 0.5f) + new Vector2(time * 0.05f, 0f), time / 20f);
				num = (float)Math.Sqrt((double)Math.Max(0f, 1f - 2f * num));
				Vector4 vector = Vector4.Lerp(this._processedSkyColor, this._processedCloudColor, num);
				vector = Vector4.Lerp(this._baseSpaceColor, vector, this._backgroundTransition);
				float num2 = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 60f);
				num2 = Math.Max(0f, 1f - num2 * 20f);
				vector = Vector4.Lerp(vector, Vector4.One, num2 * 0.98f * this._starVisibility);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04005040 RID: 20544
		private readonly Vector4 _baseSkyColor;

		// Token: 0x04005041 RID: 20545
		private readonly Vector4 _baseSpaceColor;

		// Token: 0x04005042 RID: 20546
		private Vector4 _processedSkyColor;

		// Token: 0x04005043 RID: 20547
		private Vector4 _processedCloudColor;

		// Token: 0x04005044 RID: 20548
		private float _backgroundTransition;

		// Token: 0x04005045 RID: 20549
		private float _starVisibility;
	}
}
