using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Dyes;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria.Initializers
{
	// Token: 0x0200008A RID: 138
	public static class DyeInitializer
	{
		// Token: 0x060015C4 RID: 5572 RVA: 0x004D18D4 File Offset: 0x004CFAD4
		private static void LoadBasicColorDye(int baseDyeItem, int blackDyeItem, int brightDyeItem, int silverDyeItem, float r, float g, float b, float saturation = 1f, int oldShader = 1)
		{
			Asset<Effect> pixelShaderRef = Main.PixelShaderRef;
			GameShaders.Armor.BindShader<ArmorShaderData>(baseDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColored")).UseColor(r, g, b).UseSaturation(saturation);
			GameShaders.Armor.BindShader<ArmorShaderData>(blackDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlack")).UseColor(r, g, b).UseSaturation(saturation);
			GameShaders.Armor.BindShader<ArmorShaderData>(brightDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColored")).UseColor(r * 0.5f + 0.5f, g * 0.5f + 0.5f, b * 0.5f + 0.5f).UseSaturation(saturation);
			GameShaders.Armor.BindShader<ArmorShaderData>(silverDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrim")).UseColor(r, g, b).UseSaturation(saturation);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x004D19B0 File Offset: 0x004CFBB0
		private static void LoadBasicColorDye(int baseDyeItem, float r, float g, float b, float saturation = 1f, int oldShader = 1)
		{
			DyeInitializer.LoadBasicColorDye(baseDyeItem, baseDyeItem + 12, baseDyeItem + 31, baseDyeItem + 44, r, g, b, saturation, oldShader);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x004D19D8 File Offset: 0x004CFBD8
		private static void LoadBasicColorDyes()
		{
			DyeInitializer.LoadBasicColorDye(1007, 1f, 0f, 0f, 1.2f, 1);
			DyeInitializer.LoadBasicColorDye(1008, 1f, 0.5f, 0f, 1.2f, 2);
			DyeInitializer.LoadBasicColorDye(1009, 1f, 1f, 0f, 1.2f, 3);
			DyeInitializer.LoadBasicColorDye(1010, 0.5f, 1f, 0f, 1.2f, 4);
			DyeInitializer.LoadBasicColorDye(1011, 0f, 1f, 0f, 1.2f, 5);
			DyeInitializer.LoadBasicColorDye(1012, 0f, 1f, 0.5f, 1.2f, 6);
			DyeInitializer.LoadBasicColorDye(1013, 0f, 1f, 1f, 1.2f, 7);
			DyeInitializer.LoadBasicColorDye(1014, 0.2f, 0.5f, 1f, 1.2f, 8);
			DyeInitializer.LoadBasicColorDye(1015, 0f, 0f, 1f, 1.2f, 9);
			DyeInitializer.LoadBasicColorDye(1016, 0.5f, 0f, 1f, 1.2f, 10);
			DyeInitializer.LoadBasicColorDye(1017, 1f, 0f, 1f, 1.2f, 11);
			DyeInitializer.LoadBasicColorDye(1018, 1f, 0.1f, 0.5f, 1.3f, 12);
			DyeInitializer.LoadBasicColorDye(2874, 2875, 2876, 2877, 0.4f, 0.2f, 0f, 1f, 1);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x004D1B8C File Offset: 0x004CFD8C
		private static void LoadArmorDyes()
		{
			Asset<Effect> pixelShaderRef = Main.PixelShaderRef;
			DyeInitializer.LoadBasicColorDyes();
			GameShaders.Armor.BindShader<ArmorShaderData>(1050, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(0.6f, 0.6f, 0.6f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1037, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(1f, 1f, 1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3558, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(1.5f, 1.5f, 1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2871, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(0.05f, 0.05f, 0.05f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3559, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlack")).UseColor(1f, 1f, 1f).UseSaturation(1.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1031, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f)
				.UseSaturation(1.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1032, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f)
				.UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3550, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f)
				.UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1063, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1035, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0f, 0f, 1f).UseSecondaryColor(0f, 1f, 1f)
				.UseSaturation(1.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1036, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(0f, 0f, 1f).UseSecondaryColor(0f, 1f, 1f)
				.UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3552, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(0f, 0f, 1f).UseSecondaryColor(0f, 1f, 1f)
				.UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1065, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(0f, 0f, 1f).UseSecondaryColor(0f, 1f, 1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1033, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0f, 1f, 0f).UseSecondaryColor(1f, 1f, 0f)
				.UseSaturation(1.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1034, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(0f, 1f, 0f).UseSecondaryColor(1f, 1f, 0f)
				.UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3551, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(0f, 1f, 0f).UseSecondaryColor(1f, 1f, 0f)
				.UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1064, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(0f, 1f, 0f).UseSecondaryColor(1f, 1f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1068, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.5f, 1f, 0f).UseSecondaryColor(1f, 0.5f, 0f)
				.UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1069, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0f, 1f, 0.5f).UseSecondaryColor(0f, 0.5f, 1f)
				.UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1070, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(1f, 0f, 0.5f).UseSecondaryColor(0.5f, 0f, 1f)
				.UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1066, new ArmorShaderData(pixelShaderRef, "ArmorColoredRainbow"));
			GameShaders.Armor.BindShader<ArmorShaderData>(1067, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessRainbow"));
			GameShaders.Armor.BindShader<ArmorShaderData>(3556, new ArmorShaderData(pixelShaderRef, "ArmorMidnightRainbow"));
			GameShaders.Armor.BindShader<ArmorShaderData>(2869, new ArmorShaderData(pixelShaderRef, "ArmorLivingFlame")).UseColor(1f, 0.9f, 0f).UseSecondaryColor(1f, 0.2f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2870, new ArmorShaderData(pixelShaderRef, "ArmorLivingRainbow"));
			GameShaders.Armor.BindShader<ArmorShaderData>(2873, new ArmorShaderData(pixelShaderRef, "ArmorLivingOcean"));
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3026, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1f, 1f, 1f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3027, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1.5f, 1.2f, 0.5f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3553, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1.35f, 0.7f, 0.4f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3554, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(0.25f, 0f, 0.7f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3555, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(0.4f, 0.4f, 0.4f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3190, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflective"));
			GameShaders.Armor.BindShader<TeamArmorShaderData>(1969, new TeamArmorShaderData(pixelShaderRef, "ArmorColored"));
			GameShaders.Armor.BindShader<ArmorShaderData>(2864, new ArmorShaderData(pixelShaderRef, "ArmorMartian")).UseColor(0f, 2f, 3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2872, new ArmorShaderData(pixelShaderRef, "ArmorInvert"));
			GameShaders.Armor.BindShader<ArmorShaderData>(2878, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(0.7f, 1f, 0.9f).UseSecondaryColor(0.35f, 0.85f, 0.8f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2879, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1f, 1.2f, 0f).UseSecondaryColor(1f, 0.6f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2885, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1.2f, 0.8f, 0f).UseSecondaryColor(0.8f, 0.2f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2884, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1f, 0f, 1f).UseSecondaryColor(1f, 0.3f, 0.6f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2883, new ArmorShaderData(pixelShaderRef, "ArmorHighContrastGlow")).UseColor(0f, 1f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3025, new ArmorShaderData(pixelShaderRef, "ArmorFlow")).UseColor(1f, 0.5f, 1f).UseSecondaryColor(0.6f, 0.1f, 1f);
			GameShaders.Armor.BindShader<TwilightDyeShaderData>(3039, new TwilightDyeShaderData(pixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/noise").UseColor(0.5f, 0.1f, 1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3040, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.5f, 1f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3041, new ArmorShaderData(pixelShaderRef, "ArmorMushroom")).UseColor(0.05f, 0.2f, 1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3042, new ArmorShaderData(pixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.2f, 1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3560, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.9f, 0.2f, 0.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3561, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.7f, 1.4f)
				.UseSecondaryColor(0f, 0f, 0.1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3562, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(1.4f, 0.75f, 1f)
				.UseSecondaryColor(0.45f, 0.1f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3024, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(-0.5f, -1f, 0f)
				.UseSecondaryColor(1.5f, 1f, 2.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(4663, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(2.6f, 0.6f, 0.6f)
				.UseSecondaryColor(0.2f, -0.2f, -0.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(4662, new ArmorShaderData(pixelShaderRef, "ArmorFog")).UseImage("Images/Misc/noise").UseColor(0.95f, 0.95f, 0.95f)
				.UseSecondaryColor(0.3f, 0.3f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(4778, new ArmorShaderData(pixelShaderRef, "ArmorHallowBoss")).UseImage("Images/Extra_" + 156);
			GameShaders.Armor.BindShader<ArmorShaderData>(3534, new ArmorShaderData(pixelShaderRef, "ArmorMirage"));
			GameShaders.Armor.BindShader<ArmorShaderData>(3028, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.5f, 0.7f, 1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3557, new ArmorShaderData(pixelShaderRef, "ArmorPolarized"));
			GameShaders.Armor.BindShader<ArmorShaderData>(3978, new ArmorShaderData(pixelShaderRef, "ColorOnly"));
			GameShaders.Armor.BindShader<ArmorShaderData>(3038, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.5f, 0.7f, 1.3f).UseSecondaryColor(0.5f, 0.7f, 1.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3600, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.7f, 0.4f, 1.5f).UseSecondaryColor(0.7f, 0.4f, 1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3597, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(1.5f, 0.6f, 0.4f).UseSecondaryColor(1.5f, 0.6f, 0.4f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3598, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.1f, 0.1f, 0.1f).UseSecondaryColor(0.4f, 0.05f, 0.025f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3599, new ArmorShaderData(pixelShaderRef, "ArmorLoki")).UseColor(0.1f, 0.1f, 0.1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3533, new ArmorShaderData(pixelShaderRef, "ArmorShiftingSands")).UseImage("Images/Misc/noise").UseColor(1.1f, 1f, 0.5f)
				.UseSecondaryColor(0.7f, 0.5f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3535, new ArmorShaderData(pixelShaderRef, "ArmorShiftingPearlsands")).UseImage("Images/Misc/noise").UseColor(1.1f, 0.8f, 0.9f)
				.UseSecondaryColor(0.35f, 0.25f, 0.44f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3526, new ArmorShaderData(pixelShaderRef, "ArmorSolar")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3527, new ArmorShaderData(pixelShaderRef, "ArmorNebula")).UseImage("Images/Misc/noise").UseColor(1f, 0f, 1f)
				.UseSecondaryColor(1f, 1f, 1f)
				.UseSaturation(1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3528, new ArmorShaderData(pixelShaderRef, "ArmorVortex")).UseImage("Images/Misc/noise").UseColor(0.1f, 0.5f, 0.35f)
				.UseSecondaryColor(1f, 1f, 1f)
				.UseSaturation(1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3529, new ArmorShaderData(pixelShaderRef, "ArmorStardust")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.6f, 1f)
				.UseSecondaryColor(1f, 1f, 1f)
				.UseSaturation(1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3530, new ArmorShaderData(pixelShaderRef, "ArmorVoid"));
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x004D2B20 File Offset: 0x004D0D20
		private static void LoadHairDyes()
		{
			Asset<Effect> pixelShaderRef = Main.PixelShaderRef;
			DyeInitializer.LoadLegacyHairdyes();
			GameShaders.Hair.BindShader<TwilightHairDyeShaderData>(3259, new TwilightHairDyeShaderData(pixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/noise").UseColor(0.5f, 0.1f, 1f);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x004D2B74 File Offset: 0x004D0D74
		private static void LoadLegacyHairdyes()
		{
			Asset<Effect> pixelShaderRef = Main.PixelShaderRef;
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1977, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				newColor.R = (byte)((float)player.statLife / (float)player.statLifeMax2 * 235f + 20f);
				newColor.B = 20;
				newColor.G = 20;
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1978, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				newColor.R = (byte)((1f - (float)player.statMana / (float)player.statManaMax2) * 200f + 50f);
				newColor.B = byte.MaxValue;
				newColor.G = (byte)((1f - (float)player.statMana / (float)player.statManaMax2) * 180f + 75f);
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1979, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				float num = (float)(Main.worldSurface * 0.45) * 16f;
				float num2 = (float)(Main.worldSurface + Main.rockLayer) * 8f;
				float num3 = (float)(Main.rockLayer + (double)Main.maxTilesY) * 8f;
				float num4 = (float)(Main.maxTilesY - 150) * 16f;
				Vector2 center = player.Center;
				if (center.Y < num)
				{
					float num5 = center.Y / num;
					float num6 = 1f - num5;
					newColor.R = (byte)(116f * num6 + 28f * num5);
					newColor.G = (byte)(160f * num6 + 216f * num5);
					newColor.B = (byte)(249f * num6 + 94f * num5);
				}
				else if (center.Y < num2)
				{
					float num7 = num;
					float num8 = (center.Y - num7) / (num2 - num7);
					float num9 = 1f - num8;
					newColor.R = (byte)(28f * num9 + 151f * num8);
					newColor.G = (byte)(216f * num9 + 107f * num8);
					newColor.B = (byte)(94f * num9 + 75f * num8);
				}
				else if (center.Y < num3)
				{
					float num10 = num2;
					float num11 = (center.Y - num10) / (num3 - num10);
					float num12 = 1f - num11;
					newColor.R = (byte)(151f * num12 + 128f * num11);
					newColor.G = (byte)(107f * num12 + 128f * num11);
					newColor.B = (byte)(75f * num12 + 128f * num11);
				}
				else if (center.Y < num4)
				{
					float num13 = num3;
					float num14 = (center.Y - num13) / (num4 - num13);
					float num15 = 1f - num14;
					newColor.R = (byte)(128f * num15 + 255f * num14);
					newColor.G = (byte)(128f * num15 + 50f * num14);
					newColor.B = (byte)(128f * num15 + 15f * num14);
				}
				else
				{
					newColor.R = byte.MaxValue;
					newColor.G = 50;
					newColor.B = 10;
				}
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1980, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				long num16 = 0L;
				for (int i = 0; i < 54; i++)
				{
					if (player.inventory[i].type == 71)
					{
						num16 += (long)player.inventory[i].stack;
					}
					if (player.inventory[i].type == 72)
					{
						num16 += (long)player.inventory[i].stack * 100L;
					}
					if (player.inventory[i].type == 73)
					{
						num16 += (long)player.inventory[i].stack * 10000L;
					}
					if (player.inventory[i].type == 74)
					{
						num16 += (long)player.inventory[i].stack * 1000000L;
					}
					if (num16 < 0L || num16 > 9999999999L)
					{
						num16 = 9999999999L;
						break;
					}
				}
				if (num16 < 0L || num16 > 9999999999L)
				{
					num16 = 9999999999L;
				}
				float num17 = (float)Item.buyPrice(0, 5, 0, 0);
				float num18 = (float)Item.buyPrice(0, 50, 0, 0);
				float num19 = (float)Item.buyPrice(2, 0, 0, 0);
				Color color = new Color(226, 118, 76);
				Color color2 = new Color(174, 194, 196);
				Color color3 = new Color(204, 181, 72);
				Color color4 = new Color(161, 172, 173);
				if ((float)num16 < num17)
				{
					float num20 = (float)num16 / num17;
					float num21 = 1f - num20;
					newColor.R = (byte)((float)color.R * num21 + (float)color2.R * num20);
					newColor.G = (byte)((float)color.G * num21 + (float)color2.G * num20);
					newColor.B = (byte)((float)color.B * num21 + (float)color2.B * num20);
				}
				else if ((float)num16 < num18)
				{
					float num22 = num17;
					float num23 = ((float)num16 - num22) / (num18 - num22);
					float num24 = 1f - num23;
					newColor.R = (byte)((float)color2.R * num24 + (float)color3.R * num23);
					newColor.G = (byte)((float)color2.G * num24 + (float)color3.G * num23);
					newColor.B = (byte)((float)color2.B * num24 + (float)color3.B * num23);
				}
				else if ((float)num16 < num19)
				{
					float num25 = num18;
					float num26 = ((float)num16 - num25) / (num19 - num25);
					float num27 = 1f - num26;
					newColor.R = (byte)((float)color3.R * num27 + (float)color4.R * num26);
					newColor.G = (byte)((float)color3.G * num27 + (float)color4.G * num26);
					newColor.B = (byte)((float)color3.B * num27 + (float)color4.B * num26);
				}
				else
				{
					newColor = color4;
				}
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1981, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				Color color5 = new Color(1, 142, 255);
				Color color6 = new Color(255, 255, 0);
				Color color7 = new Color(211, 45, 127);
				Color color8 = new Color(67, 44, 118);
				if (Main.dayTime)
				{
					if (Main.time < 27000.0)
					{
						float num28 = (float)(Main.time / 27000.0);
						float num29 = 1f - num28;
						newColor.R = (byte)((float)color5.R * num29 + (float)color6.R * num28);
						newColor.G = (byte)((float)color5.G * num29 + (float)color6.G * num28);
						newColor.B = (byte)((float)color5.B * num29 + (float)color6.B * num28);
					}
					else
					{
						float num30 = 27000f;
						float num31 = (float)((Main.time - (double)num30) / (54000.0 - (double)num30));
						float num32 = 1f - num31;
						newColor.R = (byte)((float)color6.R * num32 + (float)color7.R * num31);
						newColor.G = (byte)((float)color6.G * num32 + (float)color7.G * num31);
						newColor.B = (byte)((float)color6.B * num32 + (float)color7.B * num31);
					}
				}
				else if (Main.time < 16200.0)
				{
					float num33 = (float)(Main.time / 16200.0);
					float num34 = 1f - num33;
					newColor.R = (byte)((float)color7.R * num34 + (float)color8.R * num33);
					newColor.G = (byte)((float)color7.G * num34 + (float)color8.G * num33);
					newColor.B = (byte)((float)color7.B * num34 + (float)color8.B * num33);
				}
				else
				{
					float num35 = 16200f;
					float num36 = (float)((Main.time - (double)num35) / (32400.0 - (double)num35));
					float num37 = 1f - num36;
					newColor.R = (byte)((float)color8.R * num37 + (float)color5.R * num36);
					newColor.G = (byte)((float)color8.G * num37 + (float)color5.G * num36);
					newColor.B = (byte)((float)color8.B * num37 + (float)color5.B * num36);
				}
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1982, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				int num38 = ((Main.netMode == 0) ? 0 : player.team);
				if (num38 >= 0 && num38 < Main.teamColor.Length)
				{
					newColor = Main.teamColor[num38];
				}
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1983, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				Color color9 = default(Color);
				if (player.ZoneShimmer)
				{
					float num39;
					float num40;
					float num41;
					TorchID.TorchColor(23, out num39, out num40, out num41);
					color9 = new Color(num39, num40, num41);
				}
				else if (Main.waterStyle == 2)
				{
					color9 = new Color(124, 118, 242);
				}
				else if (Main.waterStyle == 3)
				{
					color9 = new Color(143, 215, 29);
				}
				else if (Main.waterStyle == 4)
				{
					color9 = new Color(78, 193, 227);
				}
				else if (Main.waterStyle == 5)
				{
					color9 = new Color(189, 231, 255);
				}
				else if (Main.waterStyle == 6)
				{
					color9 = new Color(230, 219, 100);
				}
				else if (Main.waterStyle == 7)
				{
					color9 = new Color(151, 107, 75);
				}
				else if (Main.waterStyle == 8)
				{
					color9 = new Color(128, 128, 128);
				}
				else if (Main.waterStyle == 9)
				{
					color9 = new Color(200, 0, 0);
				}
				else if (Main.waterStyle == 10)
				{
					color9 = new Color(208, 80, 80);
				}
				else if (Main.waterStyle == 12)
				{
					color9 = new Color(230, 219, 100);
				}
				else if (Main.waterStyle == 13)
				{
					color9 = new Color(28, 216, 94);
				}
				else
				{
					color9 = new Color(28, 216, 94);
				}
				Color color10 = player.hairDyeColor;
				if (color10.A == 0)
				{
					color10 = color9;
				}
				if (color10.R > color9.R)
				{
					color10.R -= 1;
				}
				if (color10.R < color9.R)
				{
					color10.R += 1;
				}
				if (color10.G > color9.G)
				{
					color10.G -= 1;
				}
				if (color10.G < color9.G)
				{
					color10.G += 1;
				}
				if (color10.B > color9.B)
				{
					color10.B -= 1;
				}
				if (color10.B < color9.B)
				{
					color10.B += 1;
				}
				return color10;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1984, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				newColor = new Color(244, 22, 175);
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1985, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1986, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				float num42 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
				float num43 = 10f;
				if (num42 > num43)
				{
					num42 = num43;
				}
				float num44 = num42 / num43;
				float num45 = 1f - num44;
				newColor.R = (byte)(75f * num44 + (float)player.hairColor.R * num45);
				newColor.G = (byte)(255f * num44 + (float)player.hairColor.G * num45);
				newColor.B = (byte)(200f * num44 + (float)player.hairColor.B * num45);
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(2863, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				lighting = false;
				int num46 = (int)((double)player.position.X + (double)player.width * 0.5) / 16;
				int num47 = (int)(((double)player.position.Y + (double)player.height * 0.25) / 16.0);
				Color color11 = Lighting.GetColor(num46, num47);
				newColor.R = (byte)(color11.R + newColor.R >> 1);
				newColor.G = (byte)(color11.G + newColor.G >> 1);
				newColor.B = (byte)(color11.B + newColor.B >> 1);
				return newColor;
			}));
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x004D2DFC File Offset: 0x004D0FFC
		private static void LoadMisc()
		{
			Asset<Effect> pixelShaderRef = Main.PixelShaderRef;
			GameShaders.Misc["ForceField"] = new MiscShaderData(pixelShaderRef, "ForceField");
			GameShaders.Misc["WaterProcessor"] = new MiscShaderData(pixelShaderRef, "WaterProcessor");
			GameShaders.Misc["WaterDistortionObject"] = new MiscShaderData(pixelShaderRef, "WaterDistortionObject");
			GameShaders.Misc["WaterDebugDraw"] = new MiscShaderData(Main.ScreenShaderRef, "WaterDebugDraw");
			GameShaders.Misc["HallowBoss"] = new MiscShaderData(pixelShaderRef, "HallowBoss");
			GameShaders.Misc["HallowBoss"].UseImage1("Images/Extra_" + 156);
			GameShaders.Misc["MaskedFade"] = new MiscShaderData(pixelShaderRef, "MaskedFade");
			GameShaders.Misc["MaskedFade"].UseImage1("Images/Extra_" + 216);
			GameShaders.Misc["QueenSlime"] = new MiscShaderData(pixelShaderRef, "QueenSlime");
			GameShaders.Misc["QueenSlime"].UseImage1("Images/Extra_" + 180);
			GameShaders.Misc["QueenSlime"].UseImage2("Images/Extra_" + 179);
			GameShaders.Misc["StardewValleyFade"] = new MiscShaderData(pixelShaderRef, "MaskedFade").UseSamplerState(SamplerState.LinearClamp);
			GameShaders.Misc["StardewValleyFade"].UseImage1("Images/Extra_" + 248);
			GameShaders.Misc["RainbowTownSlime"] = new MiscShaderData(pixelShaderRef, "RainbowTownSlime");
			GameShaders.Misc["HorizonClouds"] = new MiscShaderData(pixelShaderRef, "HorizonClouds");
			GameShaders.Misc["LitNature"] = new MiscShaderData(pixelShaderRef, "LitNature");
			GameShaders.Misc["LensFlare"] = new MiscShaderData(pixelShaderRef, "LensFlare");
			GameShaders.Misc["MouseItem"] = new MiscShaderData(pixelShaderRef, "MouseItem");
			int num = 3530;
			bool[] array = new bool[GameShaders.Armor.GetShaderIdFromItemId(num) + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			foreach (int num2 in ItemID.Sets.NonColorfulDyeItems)
			{
				array[GameShaders.Armor.GetShaderIdFromItemId(num2)] = false;
			}
			ItemID.Sets.ColorfulDyeValues = array;
			DyeInitializer.LoadMiscVertexShaders();
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x004D30C4 File Offset: 0x004D12C4
		private static void LoadMiscVertexShaders()
		{
			Asset<Effect> pixelShaderRef = Main.PixelShaderRef;
			GameShaders.Misc["MagicMissile"] = new MiscShaderData(pixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
			GameShaders.Misc["MagicMissile"].UseImage0("Images/Extra_" + 192);
			GameShaders.Misc["MagicMissile"].UseImage1("Images/Extra_" + 194);
			GameShaders.Misc["MagicMissile"].UseImage2("Images/Extra_" + 193);
			GameShaders.Misc["FlameLash"] = new MiscShaderData(pixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
			GameShaders.Misc["FlameLash"].UseImage0("Images/Extra_" + 191);
			GameShaders.Misc["FlameLash"].UseImage1("Images/Extra_" + 189);
			GameShaders.Misc["FlameLash"].UseImage2("Images/Extra_" + 190);
			GameShaders.Misc["StormLightning"] = new MiscShaderData(pixelShaderRef, "StormLightning").UseProjectionMatrix(true);
			GameShaders.Misc["StormLightning"].UseImage0("Images/Extra_" + 288);
			GameShaders.Misc["RainbowRod"] = new MiscShaderData(pixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
			GameShaders.Misc["RainbowRod"].UseImage0("Images/Extra_" + 195);
			GameShaders.Misc["RainbowRod"].UseImage1("Images/Extra_" + 197);
			GameShaders.Misc["RainbowRod"].UseImage2("Images/Extra_" + 196);
			GameShaders.Misc["FinalFractal"] = new MiscShaderData(pixelShaderRef, "FinalFractalVertex").UseProjectionMatrix(true);
			GameShaders.Misc["FinalFractal"].UseImage0("Images/Extra_" + 195);
			GameShaders.Misc["FinalFractal"].UseImage1("Images/Extra_" + 197);
			GameShaders.Misc["EmpressBlade"] = new MiscShaderData(pixelShaderRef, "FinalFractalVertex").UseProjectionMatrix(true);
			GameShaders.Misc["EmpressBlade"].UseImage0("Images/Extra_" + 209);
			GameShaders.Misc["EmpressBlade"].UseImage1("Images/Extra_" + 210);
			GameShaders.Misc["LightDisc"] = new MiscShaderData(pixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
			GameShaders.Misc["LightDisc"].UseImage0("Images/Extra_" + 195);
			GameShaders.Misc["LightDisc"].UseImage1("Images/Extra_" + 195);
			GameShaders.Misc["LightDisc"].UseImage2("Images/Extra_" + 252);
			GameShaders.Misc["Aurora"] = new MiscShaderData(pixelShaderRef, "Aurora").UseProjectionMatrix(true);
			GameShaders.Misc["Aurora"].UseImage0("Images/Extra_" + 286);
			GameShaders.Misc["Aurora"].UseImage1("Images/Extra_" + 287);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x004D34E2 File Offset: 0x004D16E2
		public static void Load()
		{
			DyeInitializer.LoadArmorDyes();
			DyeInitializer.LoadHairDyes();
			DyeInitializer.LoadMisc();
		}

		// Token: 0x02000687 RID: 1671
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003E74 RID: 15988 RVA: 0x0069725A File Offset: 0x0069545A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003E75 RID: 15989 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003E76 RID: 15990 RVA: 0x00697266 File Offset: 0x00695466
			internal Color <LoadLegacyHairdyes>b__5_0(Player player, Color newColor, ref bool lighting)
			{
				newColor.R = (byte)((float)player.statLife / (float)player.statLifeMax2 * 235f + 20f);
				newColor.B = 20;
				newColor.G = 20;
				return newColor;
			}

			// Token: 0x06003E77 RID: 15991 RVA: 0x006972A0 File Offset: 0x006954A0
			internal Color <LoadLegacyHairdyes>b__5_1(Player player, Color newColor, ref bool lighting)
			{
				newColor.R = (byte)((1f - (float)player.statMana / (float)player.statManaMax2) * 200f + 50f);
				newColor.B = byte.MaxValue;
				newColor.G = (byte)((1f - (float)player.statMana / (float)player.statManaMax2) * 180f + 75f);
				return newColor;
			}

			// Token: 0x06003E78 RID: 15992 RVA: 0x0069730C File Offset: 0x0069550C
			internal Color <LoadLegacyHairdyes>b__5_2(Player player, Color newColor, ref bool lighting)
			{
				float num = (float)(Main.worldSurface * 0.45) * 16f;
				float num2 = (float)(Main.worldSurface + Main.rockLayer) * 8f;
				float num3 = (float)(Main.rockLayer + (double)Main.maxTilesY) * 8f;
				float num4 = (float)(Main.maxTilesY - 150) * 16f;
				Vector2 center = player.Center;
				if (center.Y < num)
				{
					float num5 = center.Y / num;
					float num6 = 1f - num5;
					newColor.R = (byte)(116f * num6 + 28f * num5);
					newColor.G = (byte)(160f * num6 + 216f * num5);
					newColor.B = (byte)(249f * num6 + 94f * num5);
				}
				else if (center.Y < num2)
				{
					float num7 = num;
					float num8 = (center.Y - num7) / (num2 - num7);
					float num9 = 1f - num8;
					newColor.R = (byte)(28f * num9 + 151f * num8);
					newColor.G = (byte)(216f * num9 + 107f * num8);
					newColor.B = (byte)(94f * num9 + 75f * num8);
				}
				else if (center.Y < num3)
				{
					float num10 = num2;
					float num11 = (center.Y - num10) / (num3 - num10);
					float num12 = 1f - num11;
					newColor.R = (byte)(151f * num12 + 128f * num11);
					newColor.G = (byte)(107f * num12 + 128f * num11);
					newColor.B = (byte)(75f * num12 + 128f * num11);
				}
				else if (center.Y < num4)
				{
					float num13 = num3;
					float num14 = (center.Y - num13) / (num4 - num13);
					float num15 = 1f - num14;
					newColor.R = (byte)(128f * num15 + 255f * num14);
					newColor.G = (byte)(128f * num15 + 50f * num14);
					newColor.B = (byte)(128f * num15 + 15f * num14);
				}
				else
				{
					newColor.R = byte.MaxValue;
					newColor.G = 50;
					newColor.B = 10;
				}
				return newColor;
			}

			// Token: 0x06003E79 RID: 15993 RVA: 0x00697568 File Offset: 0x00695768
			internal Color <LoadLegacyHairdyes>b__5_3(Player player, Color newColor, ref bool lighting)
			{
				long num = 0L;
				for (int i = 0; i < 54; i++)
				{
					if (player.inventory[i].type == 71)
					{
						num += (long)player.inventory[i].stack;
					}
					if (player.inventory[i].type == 72)
					{
						num += (long)player.inventory[i].stack * 100L;
					}
					if (player.inventory[i].type == 73)
					{
						num += (long)player.inventory[i].stack * 10000L;
					}
					if (player.inventory[i].type == 74)
					{
						num += (long)player.inventory[i].stack * 1000000L;
					}
					if (num < 0L || num > 9999999999L)
					{
						num = 9999999999L;
						break;
					}
				}
				if (num < 0L || num > 9999999999L)
				{
					num = 9999999999L;
				}
				float num2 = (float)Item.buyPrice(0, 5, 0, 0);
				float num3 = (float)Item.buyPrice(0, 50, 0, 0);
				float num4 = (float)Item.buyPrice(2, 0, 0, 0);
				Color color = new Color(226, 118, 76);
				Color color2 = new Color(174, 194, 196);
				Color color3 = new Color(204, 181, 72);
				Color color4 = new Color(161, 172, 173);
				if ((float)num < num2)
				{
					float num5 = (float)num / num2;
					float num6 = 1f - num5;
					newColor.R = (byte)((float)color.R * num6 + (float)color2.R * num5);
					newColor.G = (byte)((float)color.G * num6 + (float)color2.G * num5);
					newColor.B = (byte)((float)color.B * num6 + (float)color2.B * num5);
				}
				else if ((float)num < num3)
				{
					float num7 = num2;
					float num8 = ((float)num - num7) / (num3 - num7);
					float num9 = 1f - num8;
					newColor.R = (byte)((float)color2.R * num9 + (float)color3.R * num8);
					newColor.G = (byte)((float)color2.G * num9 + (float)color3.G * num8);
					newColor.B = (byte)((float)color2.B * num9 + (float)color3.B * num8);
				}
				else if ((float)num < num4)
				{
					float num10 = num3;
					float num11 = ((float)num - num10) / (num4 - num10);
					float num12 = 1f - num11;
					newColor.R = (byte)((float)color3.R * num12 + (float)color4.R * num11);
					newColor.G = (byte)((float)color3.G * num12 + (float)color4.G * num11);
					newColor.B = (byte)((float)color3.B * num12 + (float)color4.B * num11);
				}
				else
				{
					newColor = color4;
				}
				return newColor;
			}

			// Token: 0x06003E7A RID: 15994 RVA: 0x00697854 File Offset: 0x00695A54
			internal Color <LoadLegacyHairdyes>b__5_4(Player player, Color newColor, ref bool lighting)
			{
				Color color = new Color(1, 142, 255);
				Color color2 = new Color(255, 255, 0);
				Color color3 = new Color(211, 45, 127);
				Color color4 = new Color(67, 44, 118);
				if (Main.dayTime)
				{
					if (Main.time < 27000.0)
					{
						float num = (float)(Main.time / 27000.0);
						float num2 = 1f - num;
						newColor.R = (byte)((float)color.R * num2 + (float)color2.R * num);
						newColor.G = (byte)((float)color.G * num2 + (float)color2.G * num);
						newColor.B = (byte)((float)color.B * num2 + (float)color2.B * num);
					}
					else
					{
						float num3 = 27000f;
						float num4 = (float)((Main.time - (double)num3) / (54000.0 - (double)num3));
						float num5 = 1f - num4;
						newColor.R = (byte)((float)color2.R * num5 + (float)color3.R * num4);
						newColor.G = (byte)((float)color2.G * num5 + (float)color3.G * num4);
						newColor.B = (byte)((float)color2.B * num5 + (float)color3.B * num4);
					}
				}
				else if (Main.time < 16200.0)
				{
					float num6 = (float)(Main.time / 16200.0);
					float num7 = 1f - num6;
					newColor.R = (byte)((float)color3.R * num7 + (float)color4.R * num6);
					newColor.G = (byte)((float)color3.G * num7 + (float)color4.G * num6);
					newColor.B = (byte)((float)color3.B * num7 + (float)color4.B * num6);
				}
				else
				{
					float num8 = 16200f;
					float num9 = (float)((Main.time - (double)num8) / (32400.0 - (double)num8));
					float num10 = 1f - num9;
					newColor.R = (byte)((float)color4.R * num10 + (float)color.R * num9);
					newColor.G = (byte)((float)color4.G * num10 + (float)color.G * num9);
					newColor.B = (byte)((float)color4.B * num10 + (float)color.B * num9);
				}
				return newColor;
			}

			// Token: 0x06003E7B RID: 15995 RVA: 0x00697AE0 File Offset: 0x00695CE0
			internal Color <LoadLegacyHairdyes>b__5_5(Player player, Color newColor, ref bool lighting)
			{
				int num = ((Main.netMode == 0) ? 0 : player.team);
				if (num >= 0 && num < Main.teamColor.Length)
				{
					newColor = Main.teamColor[num];
				}
				return newColor;
			}

			// Token: 0x06003E7C RID: 15996 RVA: 0x00697B1C File Offset: 0x00695D1C
			internal Color <LoadLegacyHairdyes>b__5_6(Player player, Color newColor, ref bool lighting)
			{
				Color color = default(Color);
				if (player.ZoneShimmer)
				{
					float num;
					float num2;
					float num3;
					TorchID.TorchColor(23, out num, out num2, out num3);
					color = new Color(num, num2, num3);
				}
				else if (Main.waterStyle == 2)
				{
					color = new Color(124, 118, 242);
				}
				else if (Main.waterStyle == 3)
				{
					color = new Color(143, 215, 29);
				}
				else if (Main.waterStyle == 4)
				{
					color = new Color(78, 193, 227);
				}
				else if (Main.waterStyle == 5)
				{
					color = new Color(189, 231, 255);
				}
				else if (Main.waterStyle == 6)
				{
					color = new Color(230, 219, 100);
				}
				else if (Main.waterStyle == 7)
				{
					color = new Color(151, 107, 75);
				}
				else if (Main.waterStyle == 8)
				{
					color = new Color(128, 128, 128);
				}
				else if (Main.waterStyle == 9)
				{
					color = new Color(200, 0, 0);
				}
				else if (Main.waterStyle == 10)
				{
					color = new Color(208, 80, 80);
				}
				else if (Main.waterStyle == 12)
				{
					color = new Color(230, 219, 100);
				}
				else if (Main.waterStyle == 13)
				{
					color = new Color(28, 216, 94);
				}
				else
				{
					color = new Color(28, 216, 94);
				}
				Color color2 = player.hairDyeColor;
				if (color2.A == 0)
				{
					color2 = color;
				}
				if (color2.R > color.R)
				{
					color2.R -= 1;
				}
				if (color2.R < color.R)
				{
					color2.R += 1;
				}
				if (color2.G > color.G)
				{
					color2.G -= 1;
				}
				if (color2.G < color.G)
				{
					color2.G += 1;
				}
				if (color2.B > color.B)
				{
					color2.B -= 1;
				}
				if (color2.B < color.B)
				{
					color2.B += 1;
				}
				return color2;
			}

			// Token: 0x06003E7D RID: 15997 RVA: 0x00697D86 File Offset: 0x00695F86
			internal Color <LoadLegacyHairdyes>b__5_7(Player player, Color newColor, ref bool lighting)
			{
				newColor = new Color(244, 22, 175);
				return newColor;
			}

			// Token: 0x06003E7E RID: 15998 RVA: 0x00697D9C File Offset: 0x00695F9C
			internal Color <LoadLegacyHairdyes>b__5_8(Player player, Color newColor, ref bool lighting)
			{
				newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
				return newColor;
			}

			// Token: 0x06003E7F RID: 15999 RVA: 0x00697DB8 File Offset: 0x00695FB8
			internal Color <LoadLegacyHairdyes>b__5_9(Player player, Color newColor, ref bool lighting)
			{
				float num = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
				float num2 = 10f;
				if (num > num2)
				{
					num = num2;
				}
				float num3 = num / num2;
				float num4 = 1f - num3;
				newColor.R = (byte)(75f * num3 + (float)player.hairColor.R * num4);
				newColor.G = (byte)(255f * num3 + (float)player.hairColor.G * num4);
				newColor.B = (byte)(200f * num3 + (float)player.hairColor.B * num4);
				return newColor;
			}

			// Token: 0x06003E80 RID: 16000 RVA: 0x00697E5C File Offset: 0x0069605C
			internal Color <LoadLegacyHairdyes>b__5_10(Player player, Color newColor, ref bool lighting)
			{
				lighting = false;
				int num = (int)((double)player.position.X + (double)player.width * 0.5) / 16;
				int num2 = (int)(((double)player.position.Y + (double)player.height * 0.25) / 16.0);
				Color color = Lighting.GetColor(num, num2);
				newColor.R = (byte)(color.R + newColor.R >> 1);
				newColor.G = (byte)(color.G + newColor.G >> 1);
				newColor.B = (byte)(color.B + newColor.B >> 1);
				return newColor;
			}

			// Token: 0x0400674B RID: 26443
			public static readonly DyeInitializer.<>c <>9 = new DyeInitializer.<>c();

			// Token: 0x0400674C RID: 26444
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_0;

			// Token: 0x0400674D RID: 26445
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_1;

			// Token: 0x0400674E RID: 26446
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_2;

			// Token: 0x0400674F RID: 26447
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_3;

			// Token: 0x04006750 RID: 26448
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_4;

			// Token: 0x04006751 RID: 26449
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_5;

			// Token: 0x04006752 RID: 26450
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_6;

			// Token: 0x04006753 RID: 26451
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_7;

			// Token: 0x04006754 RID: 26452
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_8;

			// Token: 0x04006755 RID: 26453
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_9;

			// Token: 0x04006756 RID: 26454
			public static LegacyHairShaderData.ColorProcessingMethod <>9__5_10;
		}
	}
}
