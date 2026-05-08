using System;

namespace Terraria.GameContent
{
	// Token: 0x02000259 RID: 601
	public static class TreePaintSystemData
	{
		// Token: 0x0600234D RID: 9037 RVA: 0x0053D76C File Offset: 0x0053B96C
		public static TreePaintingSettings GetCageTopSettings()
		{
			return TreePaintSystemData.DefaultNoSpecialGroups;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0053D774 File Offset: 0x0053B974
		public static TreePaintingSettings GetTileSettings(int tileType, int tileStyle)
		{
			if (tileType <= 109)
			{
				if (tileType > 5)
				{
					if (tileType <= 60)
					{
						if (tileType == 23)
						{
							goto IL_0104;
						}
						if (tileType - 59 > 1)
						{
							goto IL_00FE;
						}
					}
					else if (tileType != 70)
					{
						if (tileType != 109)
						{
							goto IL_00FE;
						}
						goto IL_0104;
					}
					return TreePaintSystemData.CullMud;
				}
				if (tileType == 0 || tileType == 2)
				{
					goto IL_0104;
				}
				if (tileType == 5)
				{
					switch (tileStyle)
					{
					default:
						return TreePaintSystemData.WoodPurity;
					case 0:
						return TreePaintSystemData.WoodCorruption;
					case 1:
						return TreePaintSystemData.WoodJungle;
					case 2:
						return TreePaintSystemData.WoodHallow;
					case 3:
						return TreePaintSystemData.WoodSnow;
					case 4:
						return TreePaintSystemData.WoodCrimson;
					case 5:
						return TreePaintSystemData.WoodJungleUnderground;
					case 6:
						return TreePaintSystemData.WoodGlowingMushroom;
					}
				}
			}
			else if (tileType <= 492)
			{
				if (tileType <= 323)
				{
					if (tileType == 199)
					{
						goto IL_0104;
					}
					if (tileType == 323)
					{
						switch (tileStyle)
						{
						case 0:
						case 4:
							return TreePaintSystemData.PalmTreePurity;
						case 1:
						case 5:
							return TreePaintSystemData.PalmTreeCrimson;
						case 2:
						case 6:
							return TreePaintSystemData.PalmTreeHallow;
						case 3:
						case 7:
							return TreePaintSystemData.PalmTreeCorruption;
						default:
							return TreePaintSystemData.WoodPurity;
						}
					}
				}
				else if (tileType == 477 || tileType == 492)
				{
					goto IL_0104;
				}
			}
			else if (tileType <= 616)
			{
				switch (tileType)
				{
				case 583:
					return TreePaintSystemData.GemTreeTopaz;
				case 584:
					return TreePaintSystemData.GemTreeAmethyst;
				case 585:
					return TreePaintSystemData.GemTreeSapphire;
				case 586:
					return TreePaintSystemData.GemTreeEmerald;
				case 587:
					return TreePaintSystemData.GemTreeRuby;
				case 588:
					return TreePaintSystemData.GemTreeDiamond;
				case 589:
					return TreePaintSystemData.GemTreeAmber;
				case 590:
				case 591:
				case 592:
				case 593:
				case 594:
					break;
				case 595:
				case 596:
					return TreePaintSystemData.VanityCherry;
				default:
					if (tileType - 615 <= 1)
					{
						return TreePaintSystemData.VanityYellowWillow;
					}
					break;
				}
			}
			else
			{
				if (tileType == 633)
				{
					goto IL_0104;
				}
				if (tileType == 634)
				{
					return TreePaintSystemData.TreeAsh;
				}
			}
			IL_00FE:
			return TreePaintSystemData.DefaultNoSpecialGroups;
			IL_0104:
			return TreePaintSystemData.DefaultDirt;
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0053D968 File Offset: 0x0053BB68
		public static TreePaintingSettings GetTreeFoliageSettings(int foliageIndex, int foliageStyle)
		{
			switch (foliageIndex)
			{
			case 0:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
				return TreePaintSystemData.WoodPurity;
			case 1:
				return TreePaintSystemData.WoodCorruption;
			case 2:
			case 11:
			case 13:
				return TreePaintSystemData.WoodJungle;
			case 3:
			case 19:
			case 20:
				return TreePaintSystemData.WoodHallow;
			case 4:
			case 12:
			case 16:
			case 17:
			case 18:
				return TreePaintSystemData.WoodSnow;
			case 5:
				return TreePaintSystemData.WoodCrimson;
			case 14:
				return TreePaintSystemData.WoodGlowingMushroom;
			case 15:
			case 21:
				switch (foliageStyle)
				{
				case 0:
				case 4:
					return TreePaintSystemData.PalmTreePurity;
				case 1:
				case 5:
					return TreePaintSystemData.PalmTreeCrimson;
				case 2:
				case 6:
					return TreePaintSystemData.PalmTreeHallow;
				case 3:
				case 7:
					return TreePaintSystemData.PalmTreeCorruption;
				default:
					return TreePaintSystemData.WoodPurity;
				}
				break;
			case 22:
				return TreePaintSystemData.GemTreeTopaz;
			case 23:
				return TreePaintSystemData.GemTreeAmethyst;
			case 24:
				return TreePaintSystemData.GemTreeSapphire;
			case 25:
				return TreePaintSystemData.GemTreeEmerald;
			case 26:
				return TreePaintSystemData.GemTreeRuby;
			case 27:
				return TreePaintSystemData.GemTreeDiamond;
			case 28:
				return TreePaintSystemData.GemTreeAmber;
			case 29:
				return TreePaintSystemData.VanityCherry;
			case 30:
				return TreePaintSystemData.VanityYellowWillow;
			default:
				return TreePaintSystemData.DefaultDirt;
			}
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0053DAA0 File Offset: 0x0053BCA0
		public static TreePaintingSettings GetWallSettings(int wallType)
		{
			return TreePaintSystemData.DefaultNoSpecialGroups_ForWalls;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0053DAA8 File Offset: 0x0053BCA8
		// Note: this type is marked as 'beforefieldinit'.
		static TreePaintSystemData()
		{
		}

		// Token: 0x04004D6A RID: 19818
		private static TreePaintingSettings DefaultNoSpecialGroups = new TreePaintingSettings
		{
			UseSpecialGroups = false
		};

		// Token: 0x04004D6B RID: 19819
		private static TreePaintingSettings DefaultNoSpecialGroups_ForWalls = new TreePaintingSettings
		{
			UseSpecialGroups = false,
			UseWallShaderHacks = true
		};

		// Token: 0x04004D6C RID: 19820
		private static TreePaintingSettings DefaultDirt = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.03f,
			SpecialGroupMaximumHueValue = 0.08f,
			SpecialGroupMinimumSaturationValue = 0.38f,
			SpecialGroupMaximumSaturationValue = 0.53f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D6D RID: 19821
		private static TreePaintingSettings CullMud = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			HueTestOffset = 0.5f,
			SpecialGroupMinimalHueValue = 0.42f,
			SpecialGroupMaximumHueValue = 0.55f,
			SpecialGroupMinimumSaturationValue = 0.2f,
			SpecialGroupMaximumSaturationValue = 0.27f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D6E RID: 19822
		private static TreePaintingSettings WoodPurity = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.16666667f,
			SpecialGroupMaximumHueValue = 0.8333333f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D6F RID: 19823
		private static TreePaintingSettings WoodCorruption = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.5f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0.27f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D70 RID: 19824
		private static TreePaintingSettings WoodJungle = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.16666667f,
			SpecialGroupMaximumHueValue = 0.8333333f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D71 RID: 19825
		private static TreePaintingSettings WoodHallow = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.34f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D72 RID: 19826
		private static TreePaintingSettings WoodSnow = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 0.06944445f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D73 RID: 19827
		private static TreePaintingSettings WoodCrimson = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.33333334f,
			SpecialGroupMaximumHueValue = 0.6666667f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D74 RID: 19828
		private static TreePaintingSettings WoodJungleUnderground = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.16666667f,
			SpecialGroupMaximumHueValue = 0.8333333f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D75 RID: 19829
		private static TreePaintingSettings WoodGlowingMushroom = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.5f,
			SpecialGroupMaximumHueValue = 0.8333333f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D76 RID: 19830
		private static TreePaintingSettings VanityCherry = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.8333333f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D77 RID: 19831
		private static TreePaintingSettings VanityYellowWillow = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 0.025f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D78 RID: 19832
		private static TreePaintingSettings TreeAsh = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 0.025f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D79 RID: 19833
		private static TreePaintingSettings GemTreeRuby = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D7A RID: 19834
		private static TreePaintingSettings GemTreeAmber = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D7B RID: 19835
		private static TreePaintingSettings GemTreeSapphire = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D7C RID: 19836
		private static TreePaintingSettings GemTreeEmerald = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D7D RID: 19837
		private static TreePaintingSettings GemTreeAmethyst = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D7E RID: 19838
		private static TreePaintingSettings GemTreeTopaz = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D7F RID: 19839
		private static TreePaintingSettings GemTreeDiamond = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x04004D80 RID: 19840
		private static TreePaintingSettings PalmTreePurity = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.15277778f,
			SpecialGroupMaximumHueValue = 0.25f,
			SpecialGroupMinimumSaturationValue = 0.88f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D81 RID: 19841
		private static TreePaintingSettings PalmTreeCorruption = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0.4f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D82 RID: 19842
		private static TreePaintingSettings PalmTreeCrimson = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			HueTestOffset = 0.5f,
			SpecialGroupMinimalHueValue = 0.33333334f,
			SpecialGroupMaximumHueValue = 0.5277778f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x04004D83 RID: 19843
		private static TreePaintingSettings PalmTreeHallow = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.5f,
			SpecialGroupMaximumHueValue = 0.6111111f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};
	}
}
