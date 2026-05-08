using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002B5 RID: 693
	public class CorruptSurfaceShader : ChromaShader
	{
		// Token: 0x060025A0 RID: 9632 RVA: 0x005585E8 File Offset: 0x005567E8
		public CorruptSurfaceShader(Color color)
		{
			this._baseColor = color.ToVector4();
			this._skyColor = Vector4.Lerp(this._baseColor, Color.DeepSkyBlue.ToVector4(), 0.5f);
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0055862B File Offset: 0x0055682B
		public CorruptSurfaceShader(Color vineColor, Color skyColor)
		{
			this._baseColor = vineColor.ToVector4();
			this._skyColor = skyColor.ToVector4();
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x0055864D File Offset: 0x0055684D
		public override void Update(float elapsedTime)
		{
			this._lightColor = Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00558680 File Offset: 0x00556880
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = this._skyColor * this._lightColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector2 = Vector4.Lerp(this._baseColor, vector, (float)Math.Sin((double)(time * 0.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x005586F0 File Offset: 0x005568F0
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = this._skyColor * this._lightColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = NoiseHelper.GetStaticNoise(gridPositionOfIndex.X);
				num = (num * 10f + time * 0.4f) % 10f;
				float num2 = 1f;
				if (num > 1f)
				{
					num2 = MathHelper.Clamp(1f - (num - 1.4f), 0f, 1f);
					num = 1f;
				}
				float num3 = (float)Math.Sin((double)canvasPositionOfIndex.X) * 0.3f + 0.7f;
				float num4 = num - (1f - canvasPositionOfIndex.Y);
				Vector4 vector2 = vector;
				if (num4 > 0f)
				{
					float num5 = 1f;
					if (num4 < 0.2f)
					{
						num5 = num4 * 5f;
					}
					vector2 = Vector4.Lerp(vector2, this._baseColor, num5 * num2);
				}
				if (canvasPositionOfIndex.Y > num3)
				{
					vector2 = this._baseColor;
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04005009 RID: 20489
		private readonly Vector4 _baseColor;

		// Token: 0x0400500A RID: 20490
		private readonly Vector4 _skyColor;

		// Token: 0x0400500B RID: 20491
		private Vector4 _lightColor;
	}
}
