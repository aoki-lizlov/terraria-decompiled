using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria
{
	// Token: 0x02000028 RID: 40
	public static class IngameOptions
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x000164D0 File Offset: 0x000146D0
		public static void Open()
		{
			Main.ClosePlayerChat();
			Main.chatText = "";
			Main.playerInventory = false;
			Main.editChest = false;
			Main.npcChatText = "";
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.ingameOptionsWindow = true;
			IngameOptions.category = 0;
			for (int i = 0; i < IngameOptions.leftScale.Length; i++)
			{
				IngameOptions.leftScale[i] = 0f;
			}
			for (int j = 0; j < IngameOptions.rightScale.Length; j++)
			{
				IngameOptions.rightScale[j] = 0f;
			}
			IngameOptions.leftHover = -1;
			IngameOptions.rightHover = -1;
			IngameOptions.oldLeftHover = -1;
			IngameOptions.oldRightHover = -1;
			IngameOptions.rightLock = -1;
			IngameOptions.inBar = false;
			IngameOptions.notBar = false;
			IngameOptions.noSound = false;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0001658F File Offset: 0x0001478F
		public static void Close()
		{
			IngameOptions.Close(false);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00016597 File Offset: 0x00014797
		public static void Close(bool quiet = false)
		{
			if (Main.setKey != -1)
			{
				return;
			}
			Main.ingameOptionsWindow = false;
			if (!quiet)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			}
			Main.playerInventory = true;
			Main.SaveSettings();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000165CC File Offset: 0x000147CC
		public static void Draw(Main mainInstance, SpriteBatch sb)
		{
			IngameOptions._canConsumeHover = true;
			for (int i = 0; i < IngameOptions.skipRightSlot.Length; i++)
			{
				IngameOptions.skipRightSlot[i] = false;
			}
			bool flag = GameCulture.FromCultureName(GameCulture.CultureName.Russian).IsActive || GameCulture.FromCultureName(GameCulture.CultureName.Portuguese).IsActive || GameCulture.FromCultureName(GameCulture.CultureName.Polish).IsActive || GameCulture.FromCultureName(GameCulture.CultureName.French).IsActive;
			bool isActive = GameCulture.FromCultureName(GameCulture.CultureName.Polish).IsActive;
			bool isActive2 = GameCulture.FromCultureName(GameCulture.CultureName.German).IsActive;
			bool flag2 = GameCulture.FromCultureName(GameCulture.CultureName.Italian).IsActive || GameCulture.FromCultureName(GameCulture.CultureName.Spanish).IsActive;
			bool flag3 = false;
			int num = 70;
			float num2 = 0.75f;
			float num3 = 60f;
			float num4 = 300f;
			if (flag)
			{
				flag3 = true;
			}
			if (isActive)
			{
				num4 = 200f;
			}
			new Vector2((float)Main.mouseX, (float)Main.mouseY);
			bool flag4 = Main.mouseLeft && Main.mouseLeftRelease;
			Vector2 vector = new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
			Vector2 vector2 = new Vector2(670f, 480f);
			Vector2 vector3 = vector / 2f - vector2 / 2f;
			int num5 = 20;
			IngameOptions._GUIHover = new Rectangle((int)(vector3.X - (float)num5), (int)(vector3.Y - (float)num5), (int)(vector2.X + (float)(num5 * 2)), (int)(vector2.Y + (float)(num5 * 2)));
			Utils.DrawInvBG(sb, vector3.X - (float)num5, vector3.Y - (float)num5, vector2.X + (float)(num5 * 2), vector2.Y + (float)(num5 * 2), new Color(33, 15, 91, 255) * 0.685f);
			if (new Rectangle((int)vector3.X - num5, (int)vector3.Y - num5, (int)vector2.X + num5 * 2, (int)vector2.Y + num5 * 2).Contains(new Point(Main.mouseX, Main.mouseY)))
			{
				Main.player[Main.myPlayer].mouseInterface = true;
			}
			Utils.DrawBorderString(sb, Language.GetTextValue("GameUI.SettingsMenu"), vector3 + vector2 * new Vector2(0.5f, 0f), Color.White, 1f, 0.5f, 0f, -1);
			if (flag)
			{
				Utils.DrawInvBG(sb, vector3.X + (float)(num5 / 2), vector3.Y + (float)(num5 * 5 / 2), vector2.X / 3f - (float)num5, vector2.Y - (float)(num5 * 3), default(Color));
				Utils.DrawInvBG(sb, vector3.X + vector2.X / 3f + (float)num5, vector3.Y + (float)(num5 * 5 / 2), vector2.X * 2f / 3f - (float)(num5 * 3 / 2), vector2.Y - (float)(num5 * 3), default(Color));
			}
			else
			{
				Utils.DrawInvBG(sb, vector3.X + (float)(num5 / 2), vector3.Y + (float)(num5 * 5 / 2), vector2.X / 2f - (float)num5, vector2.Y - (float)(num5 * 3), default(Color));
				Utils.DrawInvBG(sb, vector3.X + vector2.X / 2f + (float)num5, vector3.Y + (float)(num5 * 5 / 2), vector2.X / 2f - (float)(num5 * 3 / 2), vector2.Y - (float)(num5 * 3), default(Color));
			}
			float num6 = 0.7f;
			float num7 = 0.8f;
			float num8 = 0.01f;
			if (flag)
			{
				num6 = 0.4f;
				num7 = 0.44f;
			}
			if (isActive2)
			{
				num6 = 0.55f;
				num7 = 0.6f;
			}
			if (IngameOptions.oldLeftHover != IngameOptions.leftHover && IngameOptions.leftHover != -1)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			if (IngameOptions.oldRightHover != IngameOptions.rightHover && IngameOptions.rightHover != -1)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			if (flag4 && IngameOptions.rightHover != -1 && !IngameOptions.noSound)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			IngameOptions.oldLeftHover = IngameOptions.leftHover;
			IngameOptions.oldRightHover = IngameOptions.rightHover;
			IngameOptions.noSound = false;
			bool flag5 = SocialAPI.Network != null && SocialAPI.Network.CanInvite();
			int num9 = (flag5 ? 1 : 0);
			int num10 = 5 + num9 + 2;
			Vector2 vector4 = new Vector2(vector3.X + vector2.X / 4f, vector3.Y + (float)(num5 * 5 / 2));
			Vector2 vector5 = new Vector2(0f, vector2.Y - (float)(num5 * 5)) / (float)(num10 + 1);
			if (flag)
			{
				vector4.X -= 55f;
			}
			UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_LEFT = num10 + 1;
			for (int j = 0; j <= num10; j++)
			{
				bool flag6 = false;
				int num11;
				if (IngameOptions._leftSideCategoryMapping.TryGetValue(j, out num11))
				{
					flag6 = IngameOptions.category == num11;
				}
				if (IngameOptions.leftHover == j || flag6)
				{
					IngameOptions.leftScale[j] += num8;
				}
				else
				{
					IngameOptions.leftScale[j] -= num8;
				}
				if (IngameOptions.leftScale[j] < num6)
				{
					IngameOptions.leftScale[j] = num6;
				}
				if (IngameOptions.leftScale[j] > num7)
				{
					IngameOptions.leftScale[j] = num7;
				}
			}
			IngameOptions.leftHover = -1;
			int num12 = IngameOptions.category;
			int num13 = 0;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[114].Value, num13, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num13;
				if (flag4)
				{
					IngameOptions.category = 0;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			num13++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[210].Value, num13, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num13;
				if (flag4)
				{
					IngameOptions.category = 1;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			num13++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[63].Value, num13, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num13;
				if (flag4)
				{
					IngameOptions.category = 2;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			num13++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[218].Value, num13, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num13;
				if (flag4)
				{
					IngameOptions.category = 3;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			num13++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[66].Value, num13, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num13;
				if (flag4)
				{
					IngameOptions.Close();
					IngameFancyUI.OpenKeybinds();
				}
			}
			num13++;
			if (flag5 && IngameOptions.DrawLeftSide(sb, Lang.menu[147].Value, num13, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num13;
				if (flag4)
				{
					IngameOptions.Close();
					SocialAPI.Network.OpenInviteInterface();
				}
			}
			if (flag5)
			{
				num13++;
			}
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[131].Value, num13, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num13;
				if (flag4)
				{
					IngameOptions.Close();
					IngameFancyUI.OpenAchievements();
				}
			}
			num13++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[118].Value, num13, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num13;
				if (flag4)
				{
					IngameOptions.Close();
				}
			}
			num13++;
			if (IngameOptions.DrawLeftSide(sb, Lang.inter[35].Value, num13, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num13;
				if (flag4)
				{
					IngameOptions.Close();
					Main.menuMode = 10;
					Main.gameMenu = true;
					WorldGen.SaveAndQuit(null);
				}
			}
			num13++;
			if (num12 != IngameOptions.category)
			{
				for (int k = 0; k < IngameOptions.rightScale.Length; k++)
				{
					IngameOptions.rightScale[k] = 0f;
				}
			}
			int num14 = 0;
			int num15 = 0;
			switch (IngameOptions.category)
			{
			case 0:
				num15 = 17;
				num6 = 1f;
				num7 = 1.001f;
				num8 = 0.001f;
				break;
			case 1:
				num15 = 14;
				num6 = 1f;
				num7 = 1.001f;
				num8 = 0.001f;
				break;
			case 2:
				num15 = 14;
				num6 = 1f;
				num7 = 1.001f;
				num8 = 0.001f;
				break;
			case 3:
				num15 = 15;
				num6 = 1f;
				num7 = 1.001f;
				num8 = 0.001f;
				break;
			}
			if (flag)
			{
				num6 -= 0.1f;
				num7 -= 0.1f;
			}
			if (isActive2 && IngameOptions.category == 3)
			{
				num6 -= 0.15f;
				num7 -= 0.15f;
			}
			if (flag2 && (IngameOptions.category == 0 || IngameOptions.category == 3))
			{
				num6 -= 0.2f;
				num7 -= 0.2f;
			}
			UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT = num15;
			Vector2 vector6 = new Vector2(vector3.X + vector2.X * 3f / 4f, vector3.Y + (float)(num5 * 5 / 2));
			Vector2 vector7 = new Vector2(0f, vector2.Y - (float)(num5 * 3)) / (float)(num15 + 1);
			if (IngameOptions.category == 2)
			{
				vector7.Y -= 2f;
			}
			new Vector2(8f, 0f);
			if (flag)
			{
				vector6.X = vector3.X + vector2.X * 2f / 3f;
			}
			for (int l = 0; l < IngameOptions.rightScale.Length; l++)
			{
				if (IngameOptions.rightLock == l || (IngameOptions.rightHover == l && IngameOptions.rightLock == -1))
				{
					IngameOptions.rightScale[l] += num8;
				}
				else
				{
					IngameOptions.rightScale[l] -= num8;
				}
				if (IngameOptions.rightScale[l] < num6)
				{
					IngameOptions.rightScale[l] = num6;
				}
				if (IngameOptions.rightScale[l] > num7)
				{
					IngameOptions.rightScale[l] = num7;
				}
			}
			IngameOptions.inBar = false;
			IngameOptions.rightHover = -1;
			if (!Main.mouseLeft)
			{
				IngameOptions.rightLock = -1;
			}
			if (IngameOptions.rightLock == -1)
			{
				IngameOptions.notBar = false;
			}
			if (IngameOptions.category == 0)
			{
				int num16 = 0;
				IngameOptions.DrawRightSide(sb, Lang.menu[65].Value, num16, vector6, vector7, IngameOptions.rightScale[num16], 1f, default(Color));
				IngameOptions.skipRightSlot[num16] = true;
				num16++;
				vector6.X -= (float)num;
				if (IngameOptions.DrawRightSide(sb, string.Concat(new object[]
				{
					Lang.menu[99].Value,
					" ",
					Math.Round((double)(Main.musicVolume * 100f)),
					"%"
				}), num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.noSound = true;
					IngameOptions.rightHover = num16;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num17 = IngameOptions.DrawValueBar(sb, num2, Main.musicVolume, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num16) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num16;
					if (Main.mouseLeft && IngameOptions.rightLock == num16)
					{
						Main.musicVolume = num17;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num16;
				}
				if (IngameOptions.rightHover == num16)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 2;
				}
				num16++;
				if (IngameOptions.DrawRightSide(sb, string.Concat(new object[]
				{
					Lang.menu[98].Value,
					" ",
					Math.Round((double)(Main.soundVolume * 100f)),
					"%"
				}), num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num16;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num18 = IngameOptions.DrawValueBar(sb, num2, Main.soundVolume, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num16) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num16;
					if (Main.mouseLeft && IngameOptions.rightLock == num16)
					{
						Main.soundVolume = num18;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num16;
				}
				if (IngameOptions.rightHover == num16)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 3;
				}
				num16++;
				if (IngameOptions.DrawRightSide(sb, string.Concat(new object[]
				{
					Lang.menu[119].Value,
					" ",
					Math.Round((double)(Main.ambientVolume * 100f)),
					"%"
				}), num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num16;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num19 = IngameOptions.DrawValueBar(sb, num2, Main.ambientVolume, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num16) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num16;
					if (Main.mouseLeft && IngameOptions.rightLock == num16)
					{
						Main.ambientVolume = num19;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num16;
				}
				if (IngameOptions.rightHover == num16)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 4;
				}
				num16++;
				vector6.X += (float)num;
				IngameOptions.DrawRightSide(sb, "", num16, vector6, vector7, IngameOptions.rightScale[num16], 1f, default(Color));
				IngameOptions.skipRightSlot[num16] = true;
				num16++;
				IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.ZoomCategory"), num16, vector6, vector7, IngameOptions.rightScale[num16], 1f, default(Color));
				IngameOptions.skipRightSlot[num16] = true;
				num16++;
				vector6.X -= (float)num;
				string text = Language.GetTextValue("GameUI.GameZoom", Math.Round((double)(Main.GameZoomTarget * 100f)), Math.Round((double)(Main.GameViewMatrix.Zoom.X * 100f)));
				if (flag3)
				{
					text = FontAssets.ItemStack.Value.CreateWrappedText(text, num4, Language.ActiveCulture.CultureInfo);
				}
				if (IngameOptions.DrawRightSide(sb, text, num16, vector6, vector7, IngameOptions.rightScale[num16] * 0.85f, (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num16;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num20 = IngameOptions.DrawValueBar(sb, num2, Main.GameZoomTarget - 1f, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num16) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num16;
					if (Main.mouseLeft && IngameOptions.rightLock == num16)
					{
						Main.GameZoomTarget = num20 + 1f;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num16;
				}
				if (IngameOptions.rightHover == num16)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 10;
				}
				num16++;
				bool flag7 = false;
				if (Main.temporaryGUIScaleSlider == -1f)
				{
					Main.temporaryGUIScaleSlider = Main.UIScaleWanted;
				}
				string text2 = Language.GetTextValue("GameUI.UIScale", Math.Round((double)(Main.temporaryGUIScaleSlider * 100f)), Math.Round((double)(Main.UIScale * 100f)));
				if (flag3)
				{
					text2 = FontAssets.ItemStack.Value.CreateWrappedText(text2, num4, Language.ActiveCulture.CultureInfo);
				}
				if (IngameOptions.DrawRightSide(sb, text2, num16, vector6, vector7, IngameOptions.rightScale[num16] * 0.75f, (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num16;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num21 = IngameOptions.DrawValueBar(sb, num2, MathHelper.Clamp((Main.temporaryGUIScaleSlider - 0.5f) / 1.5f, 0f, 1f), 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num16) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num16;
					if (Main.mouseLeft && IngameOptions.rightLock == num16)
					{
						Main.temporaryGUIScaleSlider = num21 * 1.5f + 0.5f;
						Main.temporaryGUIScaleSlider = (float)((int)(Main.temporaryGUIScaleSlider * 100f)) / 100f;
						Main.temporaryGUIScaleSliderUpdate = true;
						flag7 = true;
					}
				}
				if (!flag7 && Main.temporaryGUIScaleSliderUpdate && Main.temporaryGUIScaleSlider != -1f)
				{
					Main.UIScale = Main.temporaryGUIScaleSlider;
					Main.temporaryGUIScaleSliderUpdate = false;
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num16;
				}
				if (IngameOptions.rightHover == num16)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 11;
				}
				num16++;
				vector6.X += (float)num;
				IngameOptions.DrawRightSide(sb, "", num16, vector6, vector7, IngameOptions.rightScale[num16], 1f, default(Color));
				IngameOptions.skipRightSlot[num16] = true;
				num16++;
				IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.Gameplay"), num16, vector6, vector7, IngameOptions.rightScale[num16], 1f, default(Color));
				IngameOptions.skipRightSlot[num16] = true;
				num16++;
				if (IngameOptions.DrawRightSide(sb, Main.autoSave ? Lang.menu[67].Value : Lang.menu[68].Value, num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num16;
					if (flag4)
					{
						Main.autoSave = !Main.autoSave;
					}
				}
				num16++;
				if (IngameOptions.DrawRightSide(sb, Main.autoPause ? Lang.menu[69].Value : Lang.menu[70].Value, num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num16;
					if (flag4)
					{
						Main.autoPause = !Main.autoPause;
					}
				}
				num16++;
				string textValue = Language.GetTextValue(Main.SettingPlayWhenUnfocused ? "UI.PlayWhenUnfocusedOn" : "UI.PlayWhenUnfocusedOff");
				if (IngameOptions.DrawRightSide(sb, textValue, num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num16;
					if (flag4)
					{
						Main.SettingPlayWhenUnfocused = !Main.SettingPlayWhenUnfocused;
					}
				}
				num16++;
				if (IngameOptions.DrawRightSide(sb, Main.ReversedUpDownArmorSetBonuses ? Lang.menu[220].Value : Lang.menu[221].Value, num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num16;
					if (flag4)
					{
						Main.ReversedUpDownArmorSetBonuses = !Main.ReversedUpDownArmorSetBonuses;
					}
				}
				num16++;
				string text3;
				switch (DoorOpeningHelper.PreferenceSettings)
				{
				default:
					text3 = Language.GetTextValue("UI.SmartDoorsDisabled");
					break;
				case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForGamepadOnly:
					text3 = Language.GetTextValue("UI.SmartDoorsGamepad");
					break;
				case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything:
					text3 = Language.GetTextValue("UI.SmartDoorsEnabled");
					break;
				}
				if (IngameOptions.DrawRightSide(sb, text3, num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num16;
					if (flag4)
					{
						DoorOpeningHelper.CyclePreferences();
					}
				}
				num16++;
				Player.Settings.HoverControlMode hoverControl = Player.Settings.HoverControl;
				string text4;
				if (hoverControl != Player.Settings.HoverControlMode.Hold)
				{
					text4 = Language.GetTextValue("UI.HoverControlSettingIsClick");
				}
				else
				{
					text4 = Language.GetTextValue("UI.HoverControlSettingIsHold");
				}
				if (IngameOptions.DrawRightSide(sb, text4, num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num16;
					if (flag4)
					{
						Player.Settings.CycleHoverControl();
					}
				}
				num16++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue(Main.SettingsEnabled_AutoReuseAllItems ? "UI.AutoReuseAllOn" : "UI.AutoReuseAllOff"), num16, vector6, vector7, IngameOptions.rightScale[num16], (IngameOptions.rightScale[num16] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num16;
					if (flag4)
					{
						Main.SettingsEnabled_AutoReuseAllItems = !Main.SettingsEnabled_AutoReuseAllItems;
					}
				}
				num16++;
				IngameOptions.DrawRightSide(sb, "", num16, vector6, vector7, IngameOptions.rightScale[num16], 1f, default(Color));
				IngameOptions.skipRightSlot[num16] = true;
				num16++;
			}
			if (IngameOptions.category == 1)
			{
				int num22 = 0;
				if (IngameOptions.DrawRightSide(sb, Main.showItemText ? Lang.menu[71].Value : Lang.menu[72].Value, num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.showItemText = !Main.showItemText;
					}
				}
				num22++;
				if (IngameOptions.DrawRightSide(sb, Lang.menu[123].Value + " " + Lang.menu[124 + Main.invasionProgressMode], num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.invasionProgressMode++;
						if (Main.invasionProgressMode >= 3)
						{
							Main.invasionProgressMode = 0;
						}
					}
				}
				num22++;
				if (IngameOptions.DrawRightSide(sb, Main.placementPreview ? Lang.menu[128].Value : Lang.menu[129].Value, num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.placementPreview = !Main.placementPreview;
					}
				}
				num22++;
				if (IngameOptions.DrawRightSide(sb, ItemSlot.Options.HighlightNewItems ? Lang.inter[117].Value : Lang.inter[116].Value, num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						ItemSlot.Options.HighlightNewItems = !ItemSlot.Options.HighlightNewItems;
					}
				}
				num22++;
				if (IngameOptions.DrawRightSide(sb, Main.MouseShowBuildingGrid ? Lang.menu[229].Value : Lang.menu[230].Value, num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.MouseShowBuildingGrid = !Main.MouseShowBuildingGrid;
					}
				}
				num22++;
				if (IngameOptions.DrawRightSide(sb, Main.GamepadDisableInstructionsDisplay ? Lang.menu[241].Value : Lang.menu[242].Value, num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.GamepadDisableInstructionsDisplay = !Main.GamepadDisableInstructionsDisplay;
					}
				}
				num22++;
				string textValue2 = Language.GetTextValue("UI.MinimapFrame_" + Main.MinimapFrameManagerInstance.ActiveSelectionKeyName);
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("UI.SelectMapBorder", textValue2), num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.MinimapFrameManagerInstance.CycleSelection();
					}
				}
				num22++;
				vector6.X -= (float)num;
				string text5 = Language.GetTextValue("GameUI.MapScale", Math.Round((double)(Main.MapScale * 100f)));
				if (flag3)
				{
					text5 = FontAssets.ItemStack.Value.CreateWrappedText(text5, num4, Language.ActiveCulture.CultureInfo);
				}
				if (IngameOptions.DrawRightSide(sb, text5, num22, vector6, vector7, IngameOptions.rightScale[num22] * 0.85f, (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num22;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num23 = IngameOptions.DrawValueBar(sb, num2, (Main.MapScale - 0.5f) / 0.5f, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num22) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num22;
					if (Main.mouseLeft && IngameOptions.rightLock == num22)
					{
						Main.MapScale = num23 * 0.5f + 0.5f;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num22;
				}
				if (IngameOptions.rightHover == num22)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 12;
				}
				num22++;
				vector6.X += (float)num;
				string activeSetKeyName = Main.ResourceSetsManager.ActiveSetKeyName;
				string textValue3 = Language.GetTextValue("UI.HealthManaStyle_" + activeSetKeyName);
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("UI.SelectHealthStyle", textValue3), num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.ResourceSetsManager.CycleResourceSet();
					}
				}
				num22++;
				string text6;
				switch (Main.DialoguePortraitPreference)
				{
				default:
					text6 = Language.GetTextValue("UI.PortraitsDetailed");
					break;
				case Main.DialoguePortraitDrawOption.CloseUp:
					text6 = Language.GetTextValue("UI.PortraitsCloseUp");
					break;
				case Main.DialoguePortraitDrawOption.FullBodyRetro:
					text6 = Language.GetTextValue("UI.PortraitsFullBody");
					break;
				case Main.DialoguePortraitDrawOption.Disabled:
					text6 = Language.GetTextValue("UI.PortraitsDisabled");
					break;
				}
				if (IngameOptions.DrawRightSide(sb, text6, num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.CycleNPCPortraitMode();
					}
				}
				num22++;
				string textValue4 = Language.GetTextValue(BigProgressBarSystem.ShowText ? "UI.ShowBossLifeTextOn" : "UI.ShowBossLifeTextOff");
				if (IngameOptions.DrawRightSide(sb, textValue4, num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						BigProgressBarSystem.ToggleShowText();
					}
				}
				num22++;
				if (IngameOptions.DrawRightSide(sb, Main.SettingsEnabled_OpaqueBoxBehindTooltips ? Language.GetTextValue("GameUI.HoverTextBoxesOn") : Language.GetTextValue("GameUI.HoverTextBoxesOff"), num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.SettingsEnabled_OpaqueBoxBehindTooltips = !Main.SettingsEnabled_OpaqueBoxBehindTooltips;
					}
				}
				num22++;
				string text7 = (ItemSlot.Options.DisableQuickTrash ? Lang.menu[253].Value : (ItemSlot.Options.DisableLeftShiftTrashCan ? Lang.menu[224].Value : Lang.menu[223].Value));
				if (IngameOptions.DrawRightSide(sb, text7, num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						if (ItemSlot.Options.DisableQuickTrash)
						{
							ItemSlot.Options.DisableQuickTrash = false;
							ItemSlot.Options.DisableLeftShiftTrashCan = true;
						}
						else if (ItemSlot.Options.DisableLeftShiftTrashCan)
						{
							ItemSlot.Options.DisableLeftShiftTrashCan = false;
						}
						else
						{
							ItemSlot.Options.DisableQuickTrash = true;
							ItemSlot.Options.DisableLeftShiftTrashCan = false;
						}
					}
				}
				num22++;
				string textValue5 = Language.GetTextValue(Main.FlashyEffectsInterface ? "UI.FlashyEffectsInterfaceOn" : "UI.FlashyEffectsInterfaceOff");
				if (IngameOptions.DrawRightSide(sb, textValue5, num22, vector6, vector7, IngameOptions.rightScale[num22], (IngameOptions.rightScale[num22] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num22;
					if (flag4)
					{
						Main.FlashyEffectsInterface = !Main.FlashyEffectsInterface;
					}
				}
				num22++;
			}
			if (IngameOptions.category == 2)
			{
				int num24 = 0;
				if (IngameOptions.DrawRightSide(sb, Main.graphics.IsFullScreen ? Lang.menu[49].Value : Lang.menu[50].Value, num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.ToggleFullScreen();
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, string.Concat(new object[]
				{
					Lang.menu[51].Value,
					": ",
					Main.PendingResolutionWidth,
					"x",
					Main.PendingResolutionHeight
				}), num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						int num25 = 0;
						for (int m = 0; m < Main.numDisplayModes; m++)
						{
							if (Main.displayWidth[m] == Main.PendingResolutionWidth && Main.displayHeight[m] == Main.PendingResolutionHeight)
							{
								num25 = m;
								break;
							}
						}
						num25++;
						if (num25 >= Main.numDisplayModes)
						{
							num25 = 0;
						}
						Main.PendingResolutionWidth = Main.displayWidth[num25];
						Main.PendingResolutionHeight = Main.displayHeight[num25];
						Main.SetResolution(Main.PendingResolutionWidth, Main.PendingResolutionHeight);
					}
				}
				num24++;
				vector6.X -= (float)num;
				if (IngameOptions.DrawRightSide(sb, string.Concat(new object[]
				{
					Lang.menu[52].Value,
					": ",
					Main.bgScroll,
					"%"
				}), num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.noSound = true;
					IngameOptions.rightHover = num24;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num26 = IngameOptions.DrawValueBar(sb, num2, (float)Main.bgScroll / 100f, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num24) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num24;
					if (Main.mouseLeft && IngameOptions.rightLock == num24)
					{
						Main.bgScroll = (int)(num26 * 100f);
						Main.caveParallax = 1f - (float)Main.bgScroll / 500f;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num24;
				}
				if (IngameOptions.rightHover == num24)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 1;
				}
				num24++;
				vector6.X += (float)num;
				if (IngameOptions.DrawRightSide(sb, Lang.menu[(int)(247 + Main.FrameSkipMode)].Value, num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.CycleFrameSkipMode();
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("UI.LightMode_" + Lighting.Mode), num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Lighting.NextLightMode();
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, Lang.menu[59 + Main.qaStyle].Value, num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.qaStyle++;
						if (Main.qaStyle > 3)
						{
							Main.qaStyle = 0;
						}
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, Main.BackgroundEnabled ? Lang.menu[100].Value : Lang.menu[101].Value, num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.BackgroundEnabled = !Main.BackgroundEnabled;
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, ChildSafety.Disabled ? Lang.menu[132].Value : Lang.menu[133].Value, num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						ChildSafety.Disabled = !ChildSafety.Disabled;
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.ForegroundSunlightEffects", Main.ForegroundSunlightEffects ? Language.GetTextValue("GameUI.Enabled") : Language.GetTextValue("GameUI.Disabled")), num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.ForegroundSunlightEffects = !Main.ForegroundSunlightEffects;
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.HeatDistortion", Main.UseHeatDistortion ? Language.GetTextValue("GameUI.Enabled") : Language.GetTextValue("GameUI.Disabled")), num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.UseHeatDistortion = !Main.UseHeatDistortion;
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.StormEffects", Main.UseStormEffects ? Language.GetTextValue("GameUI.Enabled") : Language.GetTextValue("GameUI.Disabled")), num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.UseStormEffects = !Main.UseStormEffects;
					}
				}
				num24++;
				string text8;
				switch (Main.WaveQuality)
				{
				case 1:
					text8 = Language.GetTextValue("GameUI.QualityLow");
					break;
				case 2:
					text8 = Language.GetTextValue("GameUI.QualityMedium");
					break;
				case 3:
					text8 = Language.GetTextValue("GameUI.QualityHigh");
					break;
				default:
					text8 = Language.GetTextValue("GameUI.QualityOff");
					break;
				}
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.WaveQuality", text8), num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.WaveQuality = (Main.WaveQuality + 1) % 4;
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("UI.TilesSwayInWind" + (Main.SettingsEnabled_TilesSwayInWind ? "On" : "Off")), num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.SettingsEnabled_TilesSwayInWind = !Main.SettingsEnabled_TilesSwayInWind;
					}
				}
				num24++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.ScreenShake", Main.UseScreenShake ? Language.GetTextValue("GameUI.Enabled") : Language.GetTextValue("GameUI.Disabled")), num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.UseScreenShake = !Main.UseScreenShake;
					}
				}
				num24++;
				string textValue6 = Language.GetTextValue(Main.FlashyEffectsWorld ? "UI.FlashyEffectsWorldOn" : "UI.FlashyEffectsWorldOff");
				if (IngameOptions.DrawRightSide(sb, textValue6, num24, vector6, vector7, IngameOptions.rightScale[num24], (IngameOptions.rightScale[num24] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num24;
					if (flag4)
					{
						Main.FlashyEffectsWorld = !Main.FlashyEffectsWorld;
					}
				}
				num24++;
			}
			if (IngameOptions.category == 3)
			{
				int num27 = 0;
				float num28 = (float)num;
				if (flag)
				{
					num3 = 126f;
				}
				Vector3 vector8 = Main.mouseColorSlider.GetHSLVector();
				Main.mouseColorSlider.ApplyToMainLegacyBars();
				IngameOptions.DrawRightSide(sb, Lang.menu[64].Value, num27, vector6, vector7, IngameOptions.rightScale[num27], 1f, default(Color));
				IngameOptions.skipRightSlot[num27] = true;
				num27++;
				vector6.X -= num28;
				if (IngameOptions.DrawRightSide(sb, "", num27, vector6, vector7, IngameOptions.rightScale[num27], (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num3;
				DelegateMethods.v3_1 = vector8;
				float num29 = IngameOptions.DrawValueBar(sb, num2, vector8.X, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_H));
				if ((IngameOptions.inBar || IngameOptions.rightLock == num27) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num27;
					if (Main.mouseLeft && IngameOptions.rightLock == num27)
					{
						vector8.X = num29;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				if (IngameOptions.rightHover == num27)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 5;
					Main.menuMode = 25;
				}
				num27++;
				if (IngameOptions.DrawRightSide(sb, "", num27, vector6, vector7, IngameOptions.rightScale[num27], (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num3;
				DelegateMethods.v3_1 = vector8;
				num29 = IngameOptions.DrawValueBar(sb, num2, vector8.Y, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_S));
				if ((IngameOptions.inBar || IngameOptions.rightLock == num27) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num27;
					if (Main.mouseLeft && IngameOptions.rightLock == num27)
					{
						vector8.Y = num29;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				if (IngameOptions.rightHover == num27)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 6;
					Main.menuMode = 25;
				}
				num27++;
				if (IngameOptions.DrawRightSide(sb, "", num27, vector6, vector7, IngameOptions.rightScale[num27], (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num3;
				DelegateMethods.v3_1 = vector8;
				DelegateMethods.v3_1.Z = Utils.GetLerpValue(0.15f, 1f, DelegateMethods.v3_1.Z, true);
				num29 = IngameOptions.DrawValueBar(sb, num2, DelegateMethods.v3_1.Z, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_L));
				if ((IngameOptions.inBar || IngameOptions.rightLock == num27) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num27;
					if (Main.mouseLeft && IngameOptions.rightLock == num27)
					{
						vector8.Z = num29 * 0.85f + 0.15f;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				if (IngameOptions.rightHover == num27)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 7;
					Main.menuMode = 25;
				}
				num27++;
				if (vector8.Z < 0.15f)
				{
					vector8.Z = 0.15f;
				}
				Main.mouseColorSlider.SetHSL(vector8);
				Main.mouseColor = Main.mouseColorSlider.GetColor();
				vector6.X += num28;
				IngameOptions.DrawRightSide(sb, "", num27, vector6, vector7, IngameOptions.rightScale[num27], 1f, default(Color));
				IngameOptions.skipRightSlot[num27] = true;
				num27++;
				vector8 = Main.mouseBorderColorSlider.GetHSLVector();
				if (PlayerInput.UsingGamepad && IngameOptions.rightHover == -1)
				{
					Main.mouseBorderColorSlider.ApplyToMainLegacyBars();
				}
				IngameOptions.DrawRightSide(sb, Lang.menu[217].Value, num27, vector6, vector7, IngameOptions.rightScale[num27], 1f, default(Color));
				IngameOptions.skipRightSlot[num27] = true;
				num27++;
				vector6.X -= num28;
				if (IngameOptions.DrawRightSide(sb, "", num27, vector6, vector7, IngameOptions.rightScale[num27], (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num3;
				DelegateMethods.v3_1 = vector8;
				num29 = IngameOptions.DrawValueBar(sb, num2, vector8.X, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_H));
				if ((IngameOptions.inBar || IngameOptions.rightLock == num27) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num27;
					if (Main.mouseLeft && IngameOptions.rightLock == num27)
					{
						vector8.X = num29;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				if (IngameOptions.rightHover == num27)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 5;
					Main.menuMode = 252;
				}
				num27++;
				if (IngameOptions.DrawRightSide(sb, "", num27, vector6, vector7, IngameOptions.rightScale[num27], (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num3;
				DelegateMethods.v3_1 = vector8;
				num29 = IngameOptions.DrawValueBar(sb, num2, vector8.Y, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_S));
				if ((IngameOptions.inBar || IngameOptions.rightLock == num27) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num27;
					if (Main.mouseLeft && IngameOptions.rightLock == num27)
					{
						vector8.Y = num29;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				if (IngameOptions.rightHover == num27)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 6;
					Main.menuMode = 252;
				}
				num27++;
				if (IngameOptions.DrawRightSide(sb, "", num27, vector6, vector7, IngameOptions.rightScale[num27], (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num3;
				DelegateMethods.v3_1 = vector8;
				num29 = IngameOptions.DrawValueBar(sb, num2, vector8.Z, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_L));
				if ((IngameOptions.inBar || IngameOptions.rightLock == num27) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num27;
					if (Main.mouseLeft && IngameOptions.rightLock == num27)
					{
						vector8.Z = num29;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				if (IngameOptions.rightHover == num27)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 7;
					Main.menuMode = 252;
				}
				num27++;
				if (IngameOptions.DrawRightSide(sb, "", num27, vector6, vector7, IngameOptions.rightScale[num27], (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num5 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num3;
				DelegateMethods.v3_1 = vector8;
				float num30 = Main.mouseBorderColorSlider.Alpha;
				num29 = IngameOptions.DrawValueBar(sb, num2, num30, 0, new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_O));
				if ((IngameOptions.inBar || IngameOptions.rightLock == num27) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num27;
					if (Main.mouseLeft && IngameOptions.rightLock == num27)
					{
						num30 = num29;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num5 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num27;
				}
				if (IngameOptions.rightHover == num27)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 8;
					Main.menuMode = 252;
				}
				num27++;
				Main.mouseBorderColorSlider.SetHSL(vector8);
				Main.mouseBorderColorSlider.Alpha = num30;
				Main.MouseBorderColor = Main.mouseBorderColorSlider.GetColor();
				vector6.X += num28;
				IngameOptions.DrawRightSide(sb, "", num27, vector6, vector7, IngameOptions.rightScale[num27], 1f, default(Color));
				IngameOptions.skipRightSlot[num27] = true;
				num27++;
				string text9 = "";
				switch (LockOnHelper.UseMode)
				{
				case LockOnHelper.LockOnMode.FocusTarget:
					text9 = Lang.menu[232].Value;
					break;
				case LockOnHelper.LockOnMode.TargetClosest:
					text9 = Lang.menu[233].Value;
					break;
				case LockOnHelper.LockOnMode.ThreeDS:
					text9 = Lang.menu[234].Value;
					break;
				}
				if (IngameOptions.DrawRightSide(sb, text9, num27, vector6, vector7, IngameOptions.rightScale[num27] * 0.9f, (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num27;
					if (flag4)
					{
						LockOnHelper.CycleUseModes();
					}
				}
				num27++;
				if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartBlocksEnabled ? Lang.menu[215].Value : Lang.menu[216].Value, num27, vector6, vector7, IngameOptions.rightScale[num27] * 0.9f, (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num27;
					if (flag4)
					{
						Player.SmartCursorSettings.SmartBlocksEnabled = !Player.SmartCursorSettings.SmartBlocksEnabled;
					}
				}
				num27++;
				if (IngameOptions.DrawRightSide(sb, Main.cSmartCursorModeIsToggleAndNotHold ? Lang.menu[121].Value : Lang.menu[122].Value, num27, vector6, vector7, IngameOptions.rightScale[num27], (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num27;
					if (flag4)
					{
						Main.cSmartCursorModeIsToggleAndNotHold = !Main.cSmartCursorModeIsToggleAndNotHold;
					}
				}
				num27++;
				if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartAxeAfterPickaxe ? Lang.menu[214].Value : Lang.menu[213].Value, num27, vector6, vector7, IngameOptions.rightScale[num27] * 0.9f, (IngameOptions.rightScale[num27] - num6) / (num7 - num6), default(Color)))
				{
					IngameOptions.rightHover = num27;
					if (flag4)
					{
						Player.SmartCursorSettings.SmartAxeAfterPickaxe = !Player.SmartCursorSettings.SmartAxeAfterPickaxe;
					}
				}
				num27++;
			}
			if (IngameOptions.rightHover != -1 && IngameOptions.rightLock == -1)
			{
				IngameOptions.rightLock = IngameOptions.rightHover;
			}
			for (int n = 0; n < num10 + 1; n++)
			{
				UILinkPointNavigator.SetPosition(2900 + n, vector4 + vector5 * (float)(n + 1));
			}
			Vector2 zero = Vector2.Zero;
			if (flag)
			{
				zero.X = -40f;
			}
			for (int num31 = 0; num31 < num15; num31++)
			{
				if (!IngameOptions.skipRightSlot[num31])
				{
					UILinkPointNavigator.SetPosition(2930 + num14, vector6 + zero + vector7 * (float)(num31 + 1));
					num14++;
				}
			}
			UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT = num14;
			Main.DrawInterface_29_SettingsButton();
			Main.DrawGamepadInstructions();
			Main.mouseText = false;
			Main.instance.GUIBarsDraw();
			Main.instance.DrawMouseOver();
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.SamplerStateForCursor, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
			Main.DrawCursor(Main.DrawThickCursor(false), false);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0001A038 File Offset: 0x00018238
		public static void MouseOver()
		{
			if (!Main.ingameOptionsWindow)
			{
				return;
			}
			if (IngameOptions._GUIHover.Contains(Main.MouseScreen.ToPoint()))
			{
				Main.mouseText = true;
			}
			if (IngameOptions._mouseOverText != null)
			{
				Main.instance.MouseText(IngameOptions._mouseOverText, 0, 0, -1, -1, -1, -1, 0);
			}
			IngameOptions._mouseOverText = null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0001A08C File Offset: 0x0001828C
		public static bool DrawLeftSide(SpriteBatch sb, string txt, int i, Vector2 anchor, Vector2 offset, float[] scales, float minscale = 0.7f, float maxscale = 0.8f, float scalespeed = 0.01f)
		{
			bool flag = false;
			int num;
			if (IngameOptions._leftSideCategoryMapping.TryGetValue(i, out num))
			{
				flag = IngameOptions.category == num;
			}
			Color color = Color.Lerp(Color.Gray, Color.White, (scales[i] - minscale) / (maxscale - minscale));
			if (flag)
			{
				color = Color.Gold;
			}
			Vector2 vector = Utils.DrawBorderStringBig(sb, txt, anchor + offset * (float)(1 + i), color, scales[i], 0.5f, 0.5f, -1);
			bool flag2 = new Rectangle((int)anchor.X - (int)vector.X / 2, (int)anchor.Y + (int)(offset.Y * (float)(1 + i)) - (int)vector.Y / 2, (int)vector.X, (int)vector.Y).Contains(new Point(Main.mouseX, Main.mouseY));
			if (!IngameOptions._canConsumeHover)
			{
				return false;
			}
			if (flag2)
			{
				IngameOptions._canConsumeHover = false;
				return true;
			}
			return false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0001A178 File Offset: 0x00018378
		public static bool DrawRightSide(SpriteBatch sb, string txt, int i, Vector2 anchor, Vector2 offset, float scale, float colorScale, Color over = default(Color))
		{
			Color color = Color.Lerp(Color.Gray, Color.White, colorScale);
			if (over != default(Color))
			{
				color = over;
			}
			Vector2 vector = Utils.DrawBorderString(sb, txt, anchor + offset * (float)(1 + i), color, scale, 0.5f, 0.5f, -1);
			IngameOptions.valuePosition = anchor + offset * (float)(1 + i) + vector * new Vector2(0.5f, 0f);
			bool flag = new Rectangle((int)anchor.X - (int)vector.X / 2, (int)anchor.Y + (int)(offset.Y * (float)(1 + i)) - (int)vector.Y / 2, (int)vector.X, (int)vector.Y).Contains(new Point(Main.mouseX, Main.mouseY));
			if (!IngameOptions._canConsumeHover)
			{
				return false;
			}
			if (flag)
			{
				IngameOptions._canConsumeHover = false;
				return true;
			}
			return false;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0001A274 File Offset: 0x00018474
		public static Rectangle GetExpectedRectangleForNotification(int itemIndex, Vector2 anchor, Vector2 offset, int areaWidth)
		{
			return Utils.CenteredRectangle(anchor + offset * (float)(1 + itemIndex), new Vector2((float)areaWidth, offset.Y - 4f));
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0001A2A0 File Offset: 0x000184A0
		public static bool DrawValue(SpriteBatch sb, string txt, int i, float scale, Color over = default(Color))
		{
			Color color = Color.Gray;
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(txt) * scale;
			bool flag = new Rectangle((int)IngameOptions.valuePosition.X, (int)IngameOptions.valuePosition.Y - (int)vector.Y / 2, (int)vector.X, (int)vector.Y).Contains(new Point(Main.mouseX, Main.mouseY));
			if (flag)
			{
				color = Color.White;
			}
			if (over != default(Color))
			{
				color = over;
			}
			Utils.DrawBorderString(sb, txt, IngameOptions.valuePosition, color, scale, 0f, 0.5f, -1);
			IngameOptions.valuePosition.X = IngameOptions.valuePosition.X + vector.X;
			if (!IngameOptions._canConsumeHover)
			{
				return false;
			}
			if (flag)
			{
				IngameOptions._canConsumeHover = false;
				return true;
			}
			return false;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0001A378 File Offset: 0x00018578
		public static float DrawValueBar(SpriteBatch sb, float scale, float perc, int lockState = 0, Utils.ColorLerpMethod colorMethod = null)
		{
			if (colorMethod == null)
			{
				colorMethod = new Utils.ColorLerpMethod(Utils.ColorLerp_BlackToWhite);
			}
			Texture2D value = TextureAssets.ColorBar.Value;
			Vector2 vector = new Vector2((float)value.Width, (float)value.Height) * scale;
			IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - (float)((int)vector.X);
			Rectangle rectangle = new Rectangle((int)IngameOptions.valuePosition.X, (int)IngameOptions.valuePosition.Y - (int)vector.Y / 2, (int)vector.X, (int)vector.Y);
			Rectangle rectangle2 = rectangle;
			sb.Draw(value, rectangle, Color.White);
			int num = 167;
			float num2 = (float)rectangle.X + 5f * scale;
			float num3 = (float)rectangle.Y + 4f * scale;
			for (float num4 = 0f; num4 < (float)num; num4 += 1f)
			{
				float num5 = num4 / (float)num;
				sb.Draw(TextureAssets.ColorBlip.Value, new Vector2(num2 + num4 * scale, num3), null, colorMethod(num5), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
			}
			rectangle.Inflate((int)(-5f * scale), 0);
			bool flag = rectangle.Contains(new Point(Main.mouseX, Main.mouseY));
			if (lockState == 2)
			{
				flag = false;
			}
			if (flag || lockState == 1)
			{
				sb.Draw(TextureAssets.ColorHighlight.Value, rectangle2, Main.OurFavoriteColor);
			}
			sb.Draw(TextureAssets.ColorSlider.Value, new Vector2(num2 + 167f * scale * perc, num3 + 4f * scale), null, Color.White, 0f, new Vector2(0.5f * (float)TextureAssets.ColorSlider.Width(), 0.5f * (float)TextureAssets.ColorSlider.Height()), scale, SpriteEffects.None, 0f);
			if (Main.mouseX >= rectangle.X && Main.mouseX <= rectangle.X + rectangle.Width)
			{
				IngameOptions.inBar = flag;
				return (float)(Main.mouseX - rectangle.X) / (float)rectangle.Width;
			}
			IngameOptions.inBar = false;
			if (rectangle.X >= Main.mouseX)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0001A5BC File Offset: 0x000187BC
		// Note: this type is marked as 'beforefieldinit'.
		static IngameOptions()
		{
		}

		// Token: 0x04000147 RID: 327
		public const int width = 670;

		// Token: 0x04000148 RID: 328
		public const int height = 480;

		// Token: 0x04000149 RID: 329
		public static float[] leftScale = new float[] { 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f };

		// Token: 0x0400014A RID: 330
		public static float[] rightScale = new float[]
		{
			0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f,
			0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f
		};

		// Token: 0x0400014B RID: 331
		private static Dictionary<int, int> _leftSideCategoryMapping = new Dictionary<int, int>
		{
			{ 0, 0 },
			{ 1, 1 },
			{ 2, 2 },
			{ 3, 3 }
		};

		// Token: 0x0400014C RID: 332
		public static bool[] skipRightSlot = new bool[20];

		// Token: 0x0400014D RID: 333
		public static int leftHover = -1;

		// Token: 0x0400014E RID: 334
		public static int rightHover = -1;

		// Token: 0x0400014F RID: 335
		public static int oldLeftHover = -1;

		// Token: 0x04000150 RID: 336
		public static int oldRightHover = -1;

		// Token: 0x04000151 RID: 337
		public static int rightLock = -1;

		// Token: 0x04000152 RID: 338
		public static bool inBar;

		// Token: 0x04000153 RID: 339
		public static bool notBar;

		// Token: 0x04000154 RID: 340
		public static bool noSound;

		// Token: 0x04000155 RID: 341
		private static Rectangle _GUIHover;

		// Token: 0x04000156 RID: 342
		public static int category;

		// Token: 0x04000157 RID: 343
		public static Vector2 valuePosition = Vector2.Zero;

		// Token: 0x04000158 RID: 344
		private static string _mouseOverText;

		// Token: 0x04000159 RID: 345
		private static bool _canConsumeHover;
	}
}
