using System;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002C7 RID: 711
	public static class NoiseHelper
	{
		// Token: 0x060025DB RID: 9691 RVA: 0x0055A11C File Offset: 0x0055831C
		private static float[] CreateStaticNoise(int length)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(1);
			float[] array = new float[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = unifiedRandom.NextFloat();
			}
			return array;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0055A150 File Offset: 0x00558350
		public static float GetDynamicNoise(int index, float currentTime)
		{
			float num = NoiseHelper.StaticNoise[index & 1023];
			float num2 = currentTime % 1f;
			return Math.Abs(Math.Abs(num - num2) - 0.5f) * 2f;
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0055A18A File Offset: 0x0055838A
		public static float GetStaticNoise(int index)
		{
			return NoiseHelper.StaticNoise[index & 1023];
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0055A199 File Offset: 0x00558399
		public static float GetDynamicNoise(int x, int y, float currentTime)
		{
			return NoiseHelper.GetDynamicNoiseInternal(x, y, currentTime % 1f);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0055A1A9 File Offset: 0x005583A9
		private static float GetDynamicNoiseInternal(int x, int y, float wrappedTime)
		{
			x &= 31;
			y &= 31;
			return Math.Abs(Math.Abs(NoiseHelper.StaticNoise[y * 32 + x] - wrappedTime) - 0.5f) * 2f;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0055A1DB File Offset: 0x005583DB
		public static float GetStaticNoise(int x, int y)
		{
			x &= 31;
			y &= 31;
			return NoiseHelper.StaticNoise[y * 32 + x];
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0055A1F8 File Offset: 0x005583F8
		public static float GetDynamicNoise(Vector2 position, float currentTime)
		{
			position *= 10f;
			currentTime %= 1f;
			Vector2 vector = new Vector2((float)Math.Floor((double)position.X), (float)Math.Floor((double)position.Y));
			Point point = new Point((int)vector.X, (int)vector.Y);
			Vector2 vector2 = new Vector2(position.X - vector.X, position.Y - vector.Y);
			float num = MathHelper.Lerp(NoiseHelper.GetDynamicNoiseInternal(point.X, point.Y, currentTime), NoiseHelper.GetDynamicNoiseInternal(point.X, point.Y + 1, currentTime), vector2.Y);
			float num2 = MathHelper.Lerp(NoiseHelper.GetDynamicNoiseInternal(point.X + 1, point.Y, currentTime), NoiseHelper.GetDynamicNoiseInternal(point.X + 1, point.Y + 1, currentTime), vector2.Y);
			return MathHelper.Lerp(num, num2, vector2.X);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0055A2E8 File Offset: 0x005584E8
		public static float GetStaticNoise(Vector2 position)
		{
			position *= 10f;
			Vector2 vector = new Vector2((float)Math.Floor((double)position.X), (float)Math.Floor((double)position.Y));
			Point point = new Point((int)vector.X, (int)vector.Y);
			Vector2 vector2 = new Vector2(position.X - vector.X, position.Y - vector.Y);
			float num = MathHelper.Lerp(NoiseHelper.GetStaticNoise(point.X, point.Y), NoiseHelper.GetStaticNoise(point.X, point.Y + 1), vector2.Y);
			float num2 = MathHelper.Lerp(NoiseHelper.GetStaticNoise(point.X + 1, point.Y), NoiseHelper.GetStaticNoise(point.X + 1, point.Y + 1), vector2.Y);
			return MathHelper.Lerp(num, num2, vector2.X);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0055A3C8 File Offset: 0x005585C8
		// Note: this type is marked as 'beforefieldinit'.
		static NoiseHelper()
		{
		}

		// Token: 0x0400503B RID: 20539
		private const int RANDOM_SEED = 1;

		// Token: 0x0400503C RID: 20540
		private const int NOISE_2D_SIZE = 32;

		// Token: 0x0400503D RID: 20541
		private const int NOISE_2D_SIZE_MASK = 31;

		// Token: 0x0400503E RID: 20542
		private const int NOISE_SIZE_MASK = 1023;

		// Token: 0x0400503F RID: 20543
		private static readonly float[] StaticNoise = NoiseHelper.CreateStaticNoise(1024);
	}
}
