using System;
using Microsoft.Xna.Framework;

namespace Terraria.ID
{
	// Token: 0x020001A4 RID: 420
	public static class TorchID
	{
		// Token: 0x06001F1C RID: 7964 RVA: 0x00513B98 File Offset: 0x00511D98
		public static void Initialize()
		{
			TorchID.ITorchLightProvider[] array = new TorchID.ITorchLightProvider[(int)TorchID.Count];
			array[0] = new TorchID.ConstantTorchLight(1f, 0.95f, 0.8f);
			array[1] = new TorchID.ConstantTorchLight(0f, 0.1f, 1.3f);
			array[2] = new TorchID.ConstantTorchLight(1f, 0.1f, 0.1f);
			array[3] = new TorchID.ConstantTorchLight(0f, 1f, 0.1f);
			array[4] = new TorchID.ConstantTorchLight(0.9f, 0f, 0.9f);
			array[5] = new TorchID.ConstantTorchLight(1.4f, 1.4f, 1.4f);
			array[6] = new TorchID.ConstantTorchLight(0.9f, 0.9f, 0f);
			array[7] = default(TorchID.DemonTorchLight);
			array[8] = new TorchID.ConstantTorchLight(1f, 1.6f, 0.5f);
			array[9] = new TorchID.ConstantTorchLight(0.75f, 0.85f, 1.4f);
			array[10] = new TorchID.ConstantTorchLight(1f, 0.5f, 0f);
			array[11] = new TorchID.ConstantTorchLight(1.4f, 1.4f, 0.7f);
			array[12] = new TorchID.ConstantTorchLight(0.75f, 1.3499999f, 1.5f);
			array[13] = new TorchID.ConstantTorchLight(0.95f, 0.75f, 1.3f);
			array[14] = default(TorchID.DiscoTorchLight);
			array[15] = new TorchID.ConstantTorchLight(1f, 0f, 1f);
			array[16] = new TorchID.ConstantTorchLight(1.4f, 0.85f, 0.55f);
			array[17] = new TorchID.ConstantTorchLight(0.25f, 1.3f, 0.8f);
			array[18] = new TorchID.ConstantTorchLight(0.95f, 0.4f, 1.4f);
			array[19] = new TorchID.ConstantTorchLight(1.4f, 0.7f, 0.5f);
			array[20] = new TorchID.ConstantTorchLight(1.25f, 0.6f, 1.2f);
			array[21] = new TorchID.ConstantTorchLight(0.75f, 1.45f, 0.9f);
			array[22] = new TorchID.ConstantTorchLight(0.3f, 0.78f, 1.2f);
			array[23] = default(TorchID.ShimmerTorchLight);
			TorchID._lights = array;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00513E44 File Offset: 0x00512044
		public static void TorchColor(int torchID, out float R, out float G, out float B)
		{
			if (torchID < 0 || torchID >= TorchID._lights.Length)
			{
				R = (G = (B = 0f));
				return;
			}
			TorchID._lights[torchID].GetRGB(out R, out G, out B);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00513E80 File Offset: 0x00512080
		// Note: this type is marked as 'beforefieldinit'.
		static TorchID()
		{
		}

		// Token: 0x0400196E RID: 6510
		public static int[] Dust = new int[]
		{
			6, 59, 60, 61, 62, 63, 64, 65, 75, 135,
			158, 169, 156, 234, 66, 242, 293, 294, 295, 296,
			297, 298, 307, 310
		};

		// Token: 0x0400196F RID: 6511
		private static TorchID.ITorchLightProvider[] _lights;

		// Token: 0x04001970 RID: 6512
		public const short Torch = 0;

		// Token: 0x04001971 RID: 6513
		public const short Blue = 1;

		// Token: 0x04001972 RID: 6514
		public const short Red = 2;

		// Token: 0x04001973 RID: 6515
		public const short Green = 3;

		// Token: 0x04001974 RID: 6516
		public const short Purple = 4;

		// Token: 0x04001975 RID: 6517
		public const short White = 5;

		// Token: 0x04001976 RID: 6518
		public const short Yellow = 6;

		// Token: 0x04001977 RID: 6519
		public const short Demon = 7;

		// Token: 0x04001978 RID: 6520
		public const short Cursed = 8;

		// Token: 0x04001979 RID: 6521
		public const short Ice = 9;

		// Token: 0x0400197A RID: 6522
		public const short Orange = 10;

		// Token: 0x0400197B RID: 6523
		public const short Ichor = 11;

		// Token: 0x0400197C RID: 6524
		public const short UltraBright = 12;

		// Token: 0x0400197D RID: 6525
		public const short Bone = 13;

		// Token: 0x0400197E RID: 6526
		public const short Rainbow = 14;

		// Token: 0x0400197F RID: 6527
		public const short Pink = 15;

		// Token: 0x04001980 RID: 6528
		public const short Desert = 16;

		// Token: 0x04001981 RID: 6529
		public const short Coral = 17;

		// Token: 0x04001982 RID: 6530
		public const short Corrupt = 18;

		// Token: 0x04001983 RID: 6531
		public const short Crimson = 19;

		// Token: 0x04001984 RID: 6532
		public const short Hallowed = 20;

		// Token: 0x04001985 RID: 6533
		public const short Jungle = 21;

		// Token: 0x04001986 RID: 6534
		public const short Mushroom = 22;

		// Token: 0x04001987 RID: 6535
		public const short Shimmer = 23;

		// Token: 0x04001988 RID: 6536
		public static readonly short Count = 24;

		// Token: 0x02000764 RID: 1892
		public class Sets
		{
			// Token: 0x06004118 RID: 16664 RVA: 0x0000357B File Offset: 0x0000177B
			public Sets()
			{
			}

			// Token: 0x06004119 RID: 16665 RVA: 0x006A0DFB File Offset: 0x0069EFFB
			// Note: this type is marked as 'beforefieldinit'.
			static Sets()
			{
			}

			// Token: 0x04006A15 RID: 27157
			public static SetFactory Factory = new SetFactory((int)TorchID.Count);

			// Token: 0x04006A16 RID: 27158
			public static bool[] IsABiomeTorch = TorchID.Sets.Factory.CreateBoolSet(false, new int[]
			{
				0, 18, 19, 20, 21, 23, 13, 7, 9, 22,
				16
			});
		}

		// Token: 0x02000765 RID: 1893
		private interface ITorchLightProvider
		{
			// Token: 0x0600411A RID: 16666
			void GetRGB(out float r, out float g, out float b);
		}

		// Token: 0x02000766 RID: 1894
		private struct ConstantTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x0600411B RID: 16667 RVA: 0x006A0E2E File Offset: 0x0069F02E
			public ConstantTorchLight(float Red, float Green, float Blue)
			{
				this.R = Red;
				this.G = Green;
				this.B = Blue;
			}

			// Token: 0x0600411C RID: 16668 RVA: 0x006A0E45 File Offset: 0x0069F045
			public void GetRGB(out float r, out float g, out float b)
			{
				r = this.R;
				g = this.G;
				b = this.B;
			}

			// Token: 0x04006A17 RID: 27159
			public float R;

			// Token: 0x04006A18 RID: 27160
			public float G;

			// Token: 0x04006A19 RID: 27161
			public float B;
		}

		// Token: 0x02000767 RID: 1895
		private struct DemonTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x0600411D RID: 16669 RVA: 0x006A0E5F File Offset: 0x0069F05F
			public void GetRGB(out float r, out float g, out float b)
			{
				r = 0.5f * Main.demonTorch + (1f - Main.demonTorch);
				g = 0.3f;
				b = Main.demonTorch + 0.5f * (1f - Main.demonTorch);
			}
		}

		// Token: 0x02000768 RID: 1896
		private struct ShimmerTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x0600411E RID: 16670 RVA: 0x006A0E9C File Offset: 0x0069F09C
			public void GetRGB(out float r, out float g, out float b)
			{
				float num = 0.9f;
				float num2 = 0.9f;
				num += (float)(270 - (int)Main.mouseTextColor) / 900f;
				num2 += (float)(270 - (int)Main.mouseTextColor) / 125f;
				num = MathHelper.Clamp(num, 0f, 1f);
				num2 = MathHelper.Clamp(num2, 0f, 1f);
				r = num * 0.9f;
				g = num2 * 0.55f;
				b = num * 1.2f;
			}
		}

		// Token: 0x02000769 RID: 1897
		private struct DiscoTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x0600411F RID: 16671 RVA: 0x006A0F1C File Offset: 0x0069F11C
			public void GetRGB(out float r, out float g, out float b)
			{
				r = (float)Main.DiscoR / 255f;
				g = (float)Main.DiscoG / 255f;
				b = (float)Main.DiscoB / 255f;
			}
		}
	}
}
