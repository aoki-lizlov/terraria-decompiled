using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002A0 RID: 672
	public class EmpressShader : ChromaShader
	{
		// Token: 0x06002562 RID: 9570 RVA: 0x00556000 File Offset: 0x00554200
		[RgbProcessor(new EffectDetailLevel[] { 1 }, IsTransparent = false)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float num = time * 2f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float staticNoise = NoiseHelper.GetStaticNoise(gridPositionOfIndex.X);
				float num2 = MathHelper.Max(0f, (float)Math.Cos((double)((staticNoise + num) * 6.2831855f * 0.2f)));
				Vector4 vector = Color.Lerp(Color.Black, Color.Indigo, 0.5f).ToVector4();
				float num3 = Math.Max(0f, (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 2f + canvasPositionOfIndex.X * 1f)));
				num3 = 0f;
				vector = Vector4.Lerp(vector, new Vector4(1f, 0.1f, 0.1f, 1f), num3);
				float num4 = (num2 + canvasPositionOfIndex.X + canvasPositionOfIndex.Y) % 1f;
				if (num4 > 0f)
				{
					int num5 = (gridPositionOfIndex.X + gridPositionOfIndex.Y) % EmpressShader._colors.Length;
					if (num5 < 0)
					{
						num5 += EmpressShader._colors.Length;
					}
					Vector4 vector2 = Main.hslToRgb(((canvasPositionOfIndex.X + canvasPositionOfIndex.Y) * 0.15f + time * 0.1f) % 1f, 1f, 0.5f, byte.MaxValue).ToVector4();
					vector = Vector4.Lerp(vector, vector2, num4);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00556180 File Offset: 0x00554380
		private static void RedsVersion(Fragment fragment, float time)
		{
			time *= 3f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 7f + time * 0.4f) % 7f - canvasPositionOfIndex.Y;
				Vector4 vector = default(Vector4);
				if (num > 0f)
				{
					float num2 = Math.Max(0f, 1.4f - num);
					if (num < 0.4f)
					{
						num2 = num / 0.4f;
					}
					int num3 = (gridPositionOfIndex.X + EmpressShader._colors.Length + (int)(time / 6f)) % EmpressShader._colors.Length;
					vector = Vector4.Lerp(vector, EmpressShader._colors[num3], num2);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00556259 File Offset: 0x00554459
		public EmpressShader()
		{
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x00556264 File Offset: 0x00554464
		// Note: this type is marked as 'beforefieldinit'.
		static EmpressShader()
		{
		}

		// Token: 0x04004FC8 RID: 20424
		private static readonly Vector4[] _colors = new Vector4[]
		{
			new Vector4(1f, 0.1f, 0.1f, 1f),
			new Vector4(1f, 0.5f, 0.1f, 1f),
			new Vector4(1f, 1f, 0.1f, 1f),
			new Vector4(0.5f, 1f, 0.1f, 1f),
			new Vector4(0.1f, 1f, 0.1f, 1f),
			new Vector4(0.1f, 1f, 0.5f, 1f),
			new Vector4(0.1f, 1f, 1f, 1f),
			new Vector4(0.1f, 0.5f, 1f, 1f),
			new Vector4(0.1f, 0.1f, 1f, 1f),
			new Vector4(0.5f, 0.1f, 1f, 1f),
			new Vector4(1f, 0.1f, 1f, 1f),
			new Vector4(1f, 0.1f, 0.5f, 1f)
		};
	}
}
