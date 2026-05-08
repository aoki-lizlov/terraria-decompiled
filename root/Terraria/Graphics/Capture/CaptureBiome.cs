using System;

namespace Terraria.Graphics.Capture
{
	// Token: 0x020001DA RID: 474
	public class CaptureBiome
	{
		// Token: 0x06001FE5 RID: 8165 RVA: 0x0051EBCC File Offset: 0x0051CDCC
		public CaptureBiome(int backgroundIndex, int waterStyle, CaptureBiome.TileColorStyle tileColorStyle = CaptureBiome.TileColorStyle.Normal)
		{
			this.BackgroundIndex = backgroundIndex;
			this.WaterStyle = waterStyle;
			this.TileColor = tileColorStyle;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0051EBEC File Offset: 0x0051CDEC
		public static CaptureBiome GetCaptureBiome(int biomeChoice)
		{
			switch (biomeChoice)
			{
			case 1:
				return CaptureBiome.GetPurityForPlayer();
			case 2:
				return CaptureBiome.Styles.Corruption;
			case 3:
				return CaptureBiome.Styles.Jungle;
			case 4:
				return CaptureBiome.Styles.Hallow;
			case 5:
				return CaptureBiome.Styles.Snow;
			case 6:
				return CaptureBiome.Styles.Desert;
			case 7:
				return CaptureBiome.Styles.DirtLayer;
			case 8:
				return CaptureBiome.Styles.RockLayer;
			case 9:
				return CaptureBiome.Styles.Crimson;
			case 10:
				return CaptureBiome.Styles.UndergroundDesert;
			case 11:
				return CaptureBiome.Styles.Ocean;
			case 12:
				return CaptureBiome.Styles.Mushroom;
			}
			CaptureBiome biomeByLocation = CaptureBiome.GetBiomeByLocation();
			if (biomeByLocation != null)
			{
				return biomeByLocation;
			}
			CaptureBiome biomeByWater = CaptureBiome.GetBiomeByWater();
			if (biomeByWater != null)
			{
				return biomeByWater;
			}
			return CaptureBiome.GetPurityForPlayer();
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0051ECA0 File Offset: 0x0051CEA0
		private static CaptureBiome GetBiomeByWater()
		{
			int num = Main.CalculateWaterStyle(true);
			for (int i = 0; i < CaptureBiome.BiomesByWaterStyle.Length; i++)
			{
				CaptureBiome captureBiome = CaptureBiome.BiomesByWaterStyle[i];
				if (captureBiome != null && captureBiome.WaterStyle == num)
				{
					return captureBiome;
				}
			}
			return null;
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x0051ECE0 File Offset: 0x0051CEE0
		private static CaptureBiome GetBiomeByLocation()
		{
			switch (Main.GetPreferredBGStyleForPlayer())
			{
			case 0:
				return CaptureBiome.Styles.Purity;
			case 1:
				return CaptureBiome.Styles.Corruption;
			case 2:
			case 5:
			case 13:
			case 14:
				return CaptureBiome.Styles.Desert;
			case 3:
				return CaptureBiome.Styles.Jungle;
			case 4:
				return CaptureBiome.Styles.Ocean;
			case 6:
				return CaptureBiome.Styles.Hallow;
			case 7:
				return CaptureBiome.Styles.Snow;
			case 8:
				return CaptureBiome.Styles.Crimson;
			case 9:
				return CaptureBiome.Styles.Mushroom;
			case 10:
				return CaptureBiome.Styles.Purity2;
			case 11:
				return CaptureBiome.Styles.Purity3;
			case 12:
				return CaptureBiome.Styles.Purity4;
			default:
				return null;
			}
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0051ED80 File Offset: 0x0051CF80
		private static CaptureBiome GetPurityForPlayer()
		{
			int num = (int)Main.LocalPlayer.Center.X / 16;
			if (num < Main.treeX[0])
			{
				return CaptureBiome.Styles.Purity;
			}
			if (num < Main.treeX[1])
			{
				return CaptureBiome.Styles.Purity2;
			}
			if (num < Main.treeX[2])
			{
				return CaptureBiome.Styles.Purity3;
			}
			return CaptureBiome.Styles.Purity4;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0051EDD8 File Offset: 0x0051CFD8
		// Note: this type is marked as 'beforefieldinit'.
		static CaptureBiome()
		{
		}

		// Token: 0x04004A5C RID: 19036
		public static readonly CaptureBiome DefaultPurity = new CaptureBiome(0, 0, CaptureBiome.TileColorStyle.Normal);

		// Token: 0x04004A5D RID: 19037
		public static CaptureBiome[] BiomesByWaterStyle = new CaptureBiome[]
		{
			null,
			null,
			CaptureBiome.Styles.Corruption,
			CaptureBiome.Styles.Jungle,
			CaptureBiome.Styles.Hallow,
			CaptureBiome.Styles.Snow,
			CaptureBiome.Styles.Desert,
			CaptureBiome.Styles.DirtLayer,
			CaptureBiome.Styles.RockLayer,
			CaptureBiome.Styles.BloodMoon,
			CaptureBiome.Styles.Crimson,
			null,
			CaptureBiome.Styles.UndergroundDesert,
			CaptureBiome.Styles.Ocean,
			CaptureBiome.Styles.Mushroom
		};

		// Token: 0x04004A5E RID: 19038
		public readonly int WaterStyle;

		// Token: 0x04004A5F RID: 19039
		public readonly int BackgroundIndex;

		// Token: 0x04004A60 RID: 19040
		public readonly CaptureBiome.TileColorStyle TileColor;

		// Token: 0x02000795 RID: 1941
		public enum TileColorStyle
		{
			// Token: 0x04007047 RID: 28743
			Normal,
			// Token: 0x04007048 RID: 28744
			Jungle,
			// Token: 0x04007049 RID: 28745
			Crimson,
			// Token: 0x0400704A RID: 28746
			Corrupt,
			// Token: 0x0400704B RID: 28747
			Mushroom
		}

		// Token: 0x02000796 RID: 1942
		public class Sets
		{
			// Token: 0x0600417E RID: 16766 RVA: 0x0000357B File Offset: 0x0000177B
			public Sets()
			{
			}

			// Token: 0x02000AB1 RID: 2737
			public class WaterStyles
			{
				// Token: 0x06004C19 RID: 19481 RVA: 0x0000357B File Offset: 0x0000177B
				public WaterStyles()
				{
				}

				// Token: 0x04007868 RID: 30824
				public const int BloodMoon = 9;
			}
		}

		// Token: 0x02000797 RID: 1943
		public class Styles
		{
			// Token: 0x0600417F RID: 16767 RVA: 0x0000357B File Offset: 0x0000177B
			public Styles()
			{
			}

			// Token: 0x06004180 RID: 16768 RVA: 0x006BA754 File Offset: 0x006B8954
			// Note: this type is marked as 'beforefieldinit'.
			static Styles()
			{
			}

			// Token: 0x0400704C RID: 28748
			public static CaptureBiome Purity = new CaptureBiome(0, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x0400704D RID: 28749
			public static CaptureBiome Purity2 = new CaptureBiome(10, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x0400704E RID: 28750
			public static CaptureBiome Purity3 = new CaptureBiome(11, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x0400704F RID: 28751
			public static CaptureBiome Purity4 = new CaptureBiome(12, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007050 RID: 28752
			public static CaptureBiome Corruption = new CaptureBiome(1, 2, CaptureBiome.TileColorStyle.Corrupt);

			// Token: 0x04007051 RID: 28753
			public static CaptureBiome Jungle = new CaptureBiome(3, 3, CaptureBiome.TileColorStyle.Jungle);

			// Token: 0x04007052 RID: 28754
			public static CaptureBiome Hallow = new CaptureBiome(6, 4, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007053 RID: 28755
			public static CaptureBiome Snow = new CaptureBiome(7, 5, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007054 RID: 28756
			public static CaptureBiome Desert = new CaptureBiome(2, 6, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007055 RID: 28757
			public static CaptureBiome DirtLayer = new CaptureBiome(0, 7, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007056 RID: 28758
			public static CaptureBiome RockLayer = new CaptureBiome(0, 8, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007057 RID: 28759
			public static CaptureBiome BloodMoon = new CaptureBiome(0, 9, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007058 RID: 28760
			public static CaptureBiome Crimson = new CaptureBiome(8, 10, CaptureBiome.TileColorStyle.Crimson);

			// Token: 0x04007059 RID: 28761
			public static CaptureBiome UndergroundDesert = new CaptureBiome(2, 12, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x0400705A RID: 28762
			public static CaptureBiome Ocean = new CaptureBiome(4, 13, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x0400705B RID: 28763
			public static CaptureBiome Mushroom = new CaptureBiome(9, 7, CaptureBiome.TileColorStyle.Mushroom);
		}

		// Token: 0x02000798 RID: 1944
		private enum BiomeChoiceIndex
		{
			// Token: 0x0400705D RID: 28765
			AutomatedForPlayer = -1,
			// Token: 0x0400705E RID: 28766
			Purity = 1,
			// Token: 0x0400705F RID: 28767
			Corruption,
			// Token: 0x04007060 RID: 28768
			Jungle,
			// Token: 0x04007061 RID: 28769
			Hallow,
			// Token: 0x04007062 RID: 28770
			Snow,
			// Token: 0x04007063 RID: 28771
			Desert,
			// Token: 0x04007064 RID: 28772
			DirtLayer,
			// Token: 0x04007065 RID: 28773
			RockLayer,
			// Token: 0x04007066 RID: 28774
			Crimson,
			// Token: 0x04007067 RID: 28775
			UndergroundDesert,
			// Token: 0x04007068 RID: 28776
			Ocean,
			// Token: 0x04007069 RID: 28777
			Mushroom
		}
	}
}
