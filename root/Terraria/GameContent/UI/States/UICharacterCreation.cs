using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003AE RID: 942
	public class UICharacterCreation : UIState, IHaveBackButtonCommand
	{
		// Token: 0x06002BCC RID: 11212 RVA: 0x005915D4 File Offset: 0x0058F7D4
		public UICharacterCreation(Player player)
		{
			this._player = player;
			this._player.difficulty = 0;
			this._tips = new GameTipsDisplay(new CharacterCreationTipsProvider());
			this.BuildPage();
			this.initialState = this.GetPlayerTemplateValues();
			UICharacterCreation.dirty = false;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x005916DD File Offset: 0x0058F8DD
		public override void Update(GameTime gameTime)
		{
			this._playedVoicePreviewThisFrame = false;
			base.Update(gameTime);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x005916F0 File Offset: 0x0058F8F0
		private void BuildPage()
		{
			base.RemoveAllChildren();
			int num = 4;
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixels(500f),
				Height = StyleDimension.FromPixels((float)(380 + num)),
				Top = StyleDimension.FromPixels(220f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uielement.SetPadding(0f);
			base.Append(uielement);
			UIPanel uipanel = new UIPanel
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixels(uielement.Height.Pixels - 150f - (float)num),
				Top = StyleDimension.FromPixels(50f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f
			};
			uipanel.SetPadding(0f);
			uielement.Append(uipanel);
			this.MakeBackAndCreatebuttons(uielement);
			this.MakeCharPreview(uipanel);
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(50f, 0f)
			};
			uielement2.SetPadding(0f);
			uielement2.PaddingTop = 4f;
			uielement2.PaddingBottom = 0f;
			uipanel.Append(uielement2);
			UIElement uielement3 = new UIElement
			{
				Top = StyleDimension.FromPixelsAndPercent(uielement2.Height.Pixels + 6f, 0f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(uipanel.Height.Pixels - 70f, 0f)
			};
			uielement3.SetPadding(0f);
			uielement3.PaddingTop = 3f;
			uielement3.PaddingBottom = 0f;
			uipanel.Append(uielement3);
			this._topContainer = uielement2;
			this._middleContainer = uielement3;
			this.MakeInfoMenu(uielement3);
			this.MakeHSLMenu(uielement3);
			this.MakeHairstylesMenu(uielement3);
			this.MakeClothStylesMenu(uielement3);
			this.MakeCategoriesBar(uielement2);
			this.Click_CharInfo(null, null);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x00591908 File Offset: 0x0058FB08
		private void MakeCharPreview(UIPanel container)
		{
			float num = 70f;
			for (float num2 = 0f; num2 < 1f; num2 += 1f)
			{
				UICharacter uicharacter = new UICharacter(this._player, true, false, 1.5f, false)
				{
					Width = StyleDimension.FromPixels(80f),
					Height = StyleDimension.FromPixelsAndPercent(80f, 0f),
					Top = StyleDimension.FromPixelsAndPercent(-num, 0f),
					VAlign = 0f,
					HAlign = 0.5f
				};
				uicharacter.PrepareAction = new Action(this.PreparePreview_Main);
				container.Append(uicharacter);
			}
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x005919B4 File Offset: 0x0058FBB4
		private void MakeHairstylesMenu(UIElement middleInnerPanel)
		{
			Main.Hairstyles.UpdateUnlocks();
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				Top = StyleDimension.FromPixels(6f)
			};
			middleInnerPanel.Append(uielement);
			uielement.SetPadding(0f);
			UIList uilist = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(-18f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-6f, 1f)
			};
			uilist.SetPadding(4f);
			uielement.Append(uilist);
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue)
			{
				HAlign = 1f,
				Height = StyleDimension.FromPixelsAndPercent(-30f, 1f),
				Top = StyleDimension.FromPixels(10f)
			};
			uiscrollbar.SetView(100f, 1000f);
			uilist.SetScrollbar(uiscrollbar);
			uielement.Append(uiscrollbar);
			int count = Main.Hairstyles.AvailableHairstyles.Count;
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent((float)(48 * (count / 10 + ((count % 10 == 0) ? 0 : 1))), 0f)
			};
			uilist.Add(uielement2);
			uielement2.SetPadding(0f);
			for (int i = 0; i < count; i++)
			{
				UIHairStyleButton uihairStyleButton = new UIHairStyleButton(this._player, Main.Hairstyles.AvailableHairstyles[i])
				{
					Left = StyleDimension.FromPixels((float)(i % 10) * 46f + 6f),
					Top = StyleDimension.FromPixels((float)(i / 10) * 48f + 1f)
				};
				uihairStyleButton.SetSnapPoint("Middle", i, null, null);
				uihairStyleButton.SkipRenderingContent(i);
				uihairStyleButton.OnLeftMouseDown += this.RecordThatHairWasSelected;
				uielement2.Append(uihairStyleButton);
			}
			this._hairstylesContainer = uielement;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x00591BE9 File Offset: 0x0058FDE9
		private void RecordThatHairWasSelected(UIMouseEvent evt, UIElement listeningElement)
		{
			this._lastSelectedHairstyle = new int?(this._player.hair);
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x00591C04 File Offset: 0x0058FE04
		private void MakeClothStylesMenu(UIElement middleInnerPanel)
		{
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			middleInnerPanel.Append(uielement);
			uielement.SetPadding(0f);
			int num = 0;
			for (int i = 0; i < this._validClothStyles.Length; i++)
			{
				int num2 = 19;
				if (i >= this._validClothStyles.Length / 2)
				{
					num2 += 10;
				}
				else
				{
					num2 -= 8;
				}
				UIClothStyleButton uiclothStyleButton = new UIClothStyleButton(this._player, this._validClothStyles[i], new Action(this.PreparePreview_ClothStyle))
				{
					Left = StyleDimension.FromPixels((float)i * 46f + (float)num2),
					Top = StyleDimension.FromPixels((float)num)
				};
				uiclothStyleButton.OnLeftMouseDown += this.Click_CharClothStyle;
				uiclothStyleButton.SetSnapPoint("Middle", i, null, null);
				uielement.Append(uiclothStyleButton);
			}
			int num3 = 15;
			int num4 = 60;
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.FromPixels(170f),
				Height = StyleDimension.FromPixels(50f),
				HAlign = 0f,
				Left = new StyleDimension((float)num4 - 34f, 0.5f),
				VAlign = 1f,
				Top = StyleDimension.FromPixels((float)(-(float)num3 - 7))
			};
			uielement.Append(uielement2);
			UIColoredImageButton uicoloredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/Item_" + 271, 1), true)
			{
				VAlign = 0.5f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f)
			};
			uicoloredImageButton.SetColor(this._player.hairColor);
			uicoloredImageButton.OnLeftMouseDown += this.EquipArmorNone;
			uielement2.Append(uicoloredImageButton);
			UIColoredImageButton uicoloredImageButton2 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/Item_" + 5660, 1), true)
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			};
			uicoloredImageButton2.OnLeftMouseDown += this.EquipArmorHallowed;
			uielement2.Append(uicoloredImageButton2);
			UIColoredImageButton uicoloredImageButton3 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/Item_" + 91, 1), true)
			{
				VAlign = 0.5f,
				HAlign = 0.25f
			};
			uicoloredImageButton3.OnLeftMouseDown += this.EquipArmorSilver;
			uielement2.Append(uicoloredImageButton3);
			UIColoredImageButton uicoloredImageButton4 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/Item_" + 239, 1), true)
			{
				VAlign = 0.5f,
				HAlign = 0.75f
			};
			uicoloredImageButton4.OnLeftMouseDown += this.EquipArmorFormal;
			uielement2.Append(uicoloredImageButton4);
			UIColoredImageButton uicoloredImageButton5 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/Item_" + 237, 1), true)
			{
				VAlign = 0.5f,
				HAlign = 1f
			};
			uicoloredImageButton5.OnLeftMouseDown += this.EquipArmorSwimming;
			uielement2.Append(uicoloredImageButton5);
			this._previewArmorButton = new UIElement[5];
			this._previewArmorButton[0] = uicoloredImageButton;
			this._previewArmorButton[1] = uicoloredImageButton2;
			this._previewArmorButton[2] = uicoloredImageButton3;
			this._previewArmorButton[3] = uicoloredImageButton4;
			this._previewArmorButton[4] = uicoloredImageButton5;
			this._previewArmorButton[0].SetSnapPoint("Preview", 0, null, null);
			this._previewArmorButton[2].SetSnapPoint("Preview", 1, null, null);
			this._previewArmorButton[1].SetSnapPoint("Preview", 2, null, null);
			this._previewArmorButton[3].SetSnapPoint("Preview", 3, null, null);
			this._previewArmorButton[4].SetSnapPoint("Preview", 4, null, null);
			UIElement uielement3 = new UIElement
			{
				Width = StyleDimension.FromPixels(100f),
				Height = StyleDimension.FromPixels(50f),
				HAlign = 0f,
				Left = new StyleDimension((float)num4, 0.5f),
				VAlign = 1f,
				Top = StyleDimension.FromPixels((float)(-(float)num3 + 38 - 9))
			};
			uielement.Append(uielement3);
			UIColoredImageButton uicoloredImageButton6 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Copy", 1), true)
			{
				VAlign = 0.5f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f)
			};
			uicoloredImageButton6.OnLeftMouseDown += this.Click_CopyPlayerTemplate;
			uielement3.Append(uicoloredImageButton6);
			this._copyTemplateButton = uicoloredImageButton6;
			UIColoredImageButton uicoloredImageButton7 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Paste", 1), true)
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			};
			uicoloredImageButton7.OnLeftMouseDown += this.Click_PastePlayerTemplate;
			uielement3.Append(uicoloredImageButton7);
			this._pasteTemplateButton = uicoloredImageButton7;
			UIColoredImageButton uicoloredImageButton8 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Randomize", 1), true)
			{
				VAlign = 0.5f,
				HAlign = 1f
			};
			uicoloredImageButton8.OnLeftMouseDown += this.Click_RandomizePlayer;
			uielement3.Append(uicoloredImageButton8);
			this._randomizePlayerButton = uicoloredImageButton8;
			UIElement uielement4 = new UIElement
			{
				Width = StyleDimension.FromPixels(90f),
				Height = StyleDimension.FromPixels(50f),
				HAlign = 1f,
				Left = new StyleDimension((float)(-(float)num4), -0.5f),
				VAlign = 1f,
				Top = StyleDimension.FromPixels((float)(-(float)num3))
			};
			uielement.Append(uielement4);
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-38f, 1f),
				HAlign = 0.5f,
				VAlign = 1f,
				Top = StyleDimension.FromPixelsAndPercent((float)(-52 - num3), 0f),
				Left = new StyleDimension(-3f, 0f),
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			uielement.Append(uihorizontalSeparator);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/TexturePackButtons", 1);
			Asset<Texture2D> asset2 = Main.Assets.Request<Texture2D>("Images/UI/TexturePackButtonsOutline", 1);
			UIImageButton uiimageButton = new UIImageButton(asset, new Rectangle?(asset.Frame(2, 2, 0, 1, 0, 0)))
			{
				VAlign = 0.5f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f),
				BorderColor = Main.OurFavoriteColor
			};
			uiimageButton.SetVisibility(1f, 1f);
			uiimageButton.SetHoverImage(asset2, new Rectangle?(asset2.Frame(2, 2, 0, 1, 0, 0)));
			uiimageButton.OnLeftMouseDown += this.Click_VoiceCycleBack;
			uielement4.Append(uiimageButton);
			UIImageButton uiimageButton2 = new UIImageButton(asset, new Rectangle?(asset.Frame(2, 2, 1, 1, 0, 0)))
			{
				VAlign = 0.5f,
				HAlign = 1f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f),
				BorderColor = Main.OurFavoriteColor
			};
			uiimageButton2.SetVisibility(1f, 1f);
			uiimageButton2.SetHoverImage(asset2, new Rectangle?(asset2.Frame(2, 2, 1, 1, 0, 0)));
			uiimageButton2.OnLeftMouseDown += this.Click_VoiceCycleForward;
			uielement4.Append(uiimageButton2);
			UIColoredImageButton uicoloredImageButton9 = new UIColoredImageButton(null, false)
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f),
				Width = StyleDimension.FromPixels(52f),
				Height = StyleDimension.FromPixels(52f)
			};
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Voice", 1))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true,
				Color = Main.OurFavoriteColor
			};
			uiimage.OnUpdate += this.voiceIcon_OnUpdate;
			uicoloredImageButton9.Append(uiimage);
			UIText uitext = new UIText("", 0.85f, false)
			{
				VAlign = 1f,
				HAlign = 1f,
				TextOriginX = 0.5f,
				TextOriginY = 1f,
				Top = StyleDimension.FromPixels(-6f),
				Left = StyleDimension.FromPixels(-12f),
				ShadowColor = Color.Black * 0.3f
			};
			uitext.OnUpdate += this.voiceNumber_OnUpdate;
			uicoloredImageButton9.Append(uitext);
			uicoloredImageButton9.OnLeftMouseDown += this.Click_VoicePlay;
			uielement4.Append(uicoloredImageButton9);
			UIColoredSlider uicoloredSlider = new UIColoredSlider(LocalizedText.Empty, new Func<float>(this.GetPitchSlider), new Action<float>(this.SetPitchSlider_Keyboard), new Action(this.SetPitchSlider_GamePad), new Func<float, Color>(this.GetVoicePitchColorAt), Color.Transparent)
			{
				VAlign = 1f,
				HAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(187f, 0f),
				Top = StyleDimension.FromPixels(-10f),
				Left = StyleDimension.FromPixels(55f)
			};
			uicoloredSlider.OnLeftMouseDown += this.Click_VoicePitch;
			uicoloredSlider.OnUpdate += this.PitchSliderUpdate;
			uicoloredSlider.SetSnapPoint("pitch", 0, null, new Vector2?(new Vector2(-93f, 16f)));
			uielement4.Append(uicoloredSlider);
			this._pitchSlider = uicoloredSlider;
			uiimageButton.SetSnapPoint("Low", 1, null, null);
			uicoloredImageButton9.SetSnapPoint("Low", 2, null, null);
			uiimageButton2.SetSnapPoint("Low", 3, null, null);
			this._voicePrevious = uiimageButton;
			this._voiceNext = uiimageButton2;
			this._voicePlay = uicoloredImageButton9;
			uicoloredImageButton6.SetSnapPoint("Low", 4, null, null);
			uicoloredImageButton7.SetSnapPoint("Low", 5, null, null);
			uicoloredImageButton8.SetSnapPoint("Low", 6, null, null);
			this._clothStylesContainer = uielement;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x00592774 File Offset: 0x00590974
		private void EquipArmorNone(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._femaleArmor = (this._maleArmor = default(UICharacterCreation.ArmorAssignments));
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x005927B0 File Offset: 0x005909B0
		private void EquipArmorGold(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._femaleArmor = (this._maleArmor = new UICharacterCreation.ArmorAssignments
			{
				HeadItem = 92,
				BodyItem = 83,
				LegItem = 79
			});
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x00592808 File Offset: 0x00590A08
		private void EquipArmorSilver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._femaleArmor = (this._maleArmor = new UICharacterCreation.ArmorAssignments
			{
				HeadItem = 91,
				BodyItem = 82,
				LegItem = 78
			});
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x00592860 File Offset: 0x00590A60
		private void EquipArmorFuneral(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._femaleArmor = (this._maleArmor = new UICharacterCreation.ArmorAssignments
			{
				HeadItem = 4704,
				BodyItem = 4705,
				LegItem = 4706
			});
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x005928C0 File Offset: 0x00590AC0
		private void EquipArmorHallowed(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._femaleArmor = (this._maleArmor = new UICharacterCreation.ArmorAssignments
			{
				HeadItem = 5660,
				BodyItem = 551,
				LegItem = 552
			});
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x00592920 File Offset: 0x00590B20
		private void EquipArmorFormal(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._maleArmor = new UICharacterCreation.ArmorAssignments
			{
				HeadItem = 239,
				BodyItem = 240,
				LegItem = 241
			};
			this._femaleArmor = new UICharacterCreation.ArmorAssignments
			{
				HeadItem = 3478,
				BodyItem = 3479,
				LegItem = 0
			};
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x005929A4 File Offset: 0x00590BA4
		private void EquipArmorSwimming(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._maleArmor = new UICharacterCreation.ArmorAssignments
			{
				HeadItem = 237,
				BodyItem = 3785,
				LegItem = 5649
			};
			this._femaleArmor = new UICharacterCreation.ArmorAssignments
			{
				HeadItem = 237,
				BodyItem = 5646,
				LegItem = 5647,
				Accessory1Item = 208
			};
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x00592A38 File Offset: 0x00590C38
		private void PreparePreview_Main()
		{
			this._player.direction = 1;
			this.TryAutoAssigningHair();
			this.UpdatePreviewItems();
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x00592A52 File Offset: 0x00590C52
		private void PreparePreview_ClothStyle()
		{
			this._player.direction = (this._player.Male ? 1 : (-1));
			this.TryAutoAssigningHair();
			this.UpdatePreviewItems();
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x00592A7C File Offset: 0x00590C7C
		private void TryAutoAssigningHair()
		{
			if (this._lastSelectedHairstyle != null)
			{
				return;
			}
			int num;
			if (this._defaultHairstylesForClothStyle.TryGetValue(this._player.skinVariant, out num))
			{
				this._player.hair = num;
			}
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x00592AC0 File Offset: 0x00590CC0
		private void UpdatePreviewItems()
		{
			UICharacterCreation.ArmorAssignments armorAssignments = this._femaleArmor;
			if (this._player.Male)
			{
				armorAssignments = this._maleArmor;
			}
			this._player.armor[0].SetDefaults(armorAssignments.HeadItem, null);
			this._player.armor[1].SetDefaults(armorAssignments.BodyItem, null);
			this._player.armor[2].SetDefaults(armorAssignments.LegItem, null);
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x00592B34 File Offset: 0x00590D34
		private void PitchSliderUpdate(UIElement affectedElement)
		{
			if (!this._pitchChanged)
			{
				return;
			}
			int num = this._pitchChangedCooldown - 1;
			this._pitchChangedCooldown = num;
			if (num > 0)
			{
				return;
			}
			this._pitchChanged = false;
			this.PlayVoicePreview();
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x00592B6C File Offset: 0x00590D6C
		private void PitchChanged()
		{
			this._pitchChanged = true;
			this._pitchChangedCooldown = 3;
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x00592B7C File Offset: 0x00590D7C
		private void SetPitchSlider_GamePad()
		{
			if (!PlayerInput.UsingGamepad)
			{
				return;
			}
			float pitchAmount = this._pitchAmount;
			float num = UILinksInitializer.HandleSliderHorizontalInput(Utils.Remap(this._pitchAmount, -1f, 1f, 0f, 1f, true), 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this._pitchAmount = Utils.Remap(num, 0f, 1f, -1f, 1f, true);
			num = this.RemapPitchSliderKnob(num);
			this._player.voicePitchOffset = Utils.Remap(num, 0f, 1f, -1f, 1f, true);
			if (pitchAmount != this._pitchAmount)
			{
				this.PitchChanged();
			}
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x00592C34 File Offset: 0x00590E34
		private float RemapPitchSliderKnob(float pitchSliderValue)
		{
			int num = 20;
			return (float)Math.Round((double)(pitchSliderValue * (float)num)) / (float)num;
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x00592C54 File Offset: 0x00590E54
		private void SetPitchSlider_Keyboard(float amount)
		{
			amount = this.RemapPitchSliderKnob(amount);
			float voicePitchOffset = this._player.voicePitchOffset;
			this._pitchAmount = (this._player.voicePitchOffset = Utils.Remap(amount, 0f, 1f, -1f, 1f, true));
			this._pitchChangedCooldown = 3;
			if (voicePitchOffset != this._player.voicePitchOffset)
			{
				this.PitchChanged();
			}
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x00592CBE File Offset: 0x00590EBE
		private float GetPitchSlider()
		{
			return Utils.Remap(this.RemapPitchSliderKnob(this._pitchAmount), -1f, 1f, 0f, 1f, true);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x00592CE8 File Offset: 0x00590EE8
		private Color GetVoicePitchColorAt(float x)
		{
			float num = (x * 4f + 0.5f) % 1f;
			float num2 = Utils.Remap(num, 0f, 0.5f, 0f, 1f, true) * Utils.Remap(num, 0.5f, 1f, 1f, 0f, true);
			float num3 = num2 * num2 * num2 * num2 * num2;
			return Color.Lerp(new Color(90, 90, 120), Color.White, num3);
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x00592D64 File Offset: 0x00590F64
		private void voiceNumber_OnUpdate(UIElement affectedElement)
		{
			int num = 0;
			int[] variantOrder = PlayerVoiceID.VariantOrder;
			for (int i = 0; i < variantOrder.Length; i++)
			{
				if (variantOrder[i] == this._player.voiceVariant)
				{
					num = i;
					break;
				}
			}
			(affectedElement as UIText).SetText((num + 1).ToString());
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x00592DB1 File Offset: 0x00590FB1
		private void voiceIcon_OnUpdate(UIElement affectedElement)
		{
			(affectedElement as UIImage).Color = PlayerVoiceID.Sets.Colors[this._player.voiceVariant];
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x00592DD4 File Offset: 0x00590FD4
		private void MakeCategoriesBar(UIElement categoryContainer)
		{
			float num = -240f;
			float num2 = 48f;
			this._colorPickers = new UIColoredImageButton[10];
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.HairColor, "Images/UI/CharCreation/ColorHair", num, num2));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Eye, "Images/UI/CharCreation/ColorEye", num, num2));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Skin, "Images/UI/CharCreation/ColorSkin", num, num2));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Shirt, "Images/UI/CharCreation/ColorShirt", num, num2));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Undershirt, "Images/UI/CharCreation/ColorUndershirt", num, num2));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Pants, "Images/UI/CharCreation/ColorPants", num, num2));
			categoryContainer.Append(this.CreateColorPicker(UICharacterCreation.CategoryId.Shoes, "Images/UI/CharCreation/ColorShoes", num, num2));
			this._colorPickers[4].SetMiddleTexture(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/ColorEyeBack", 1));
			this._clothingStylesCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.Clothing, "Images/UI/CharCreation/ClothStyleMale", num, num2);
			this._clothingStylesCategoryButton.OnLeftMouseDown += this.Click_ClothStyles;
			this._clothingStylesCategoryButton.SetSnapPoint("Top", 1, null, null);
			categoryContainer.Append(this._clothingStylesCategoryButton);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/ColorCharacter", 1);
			this._clothingStylesCategoryButton.SetColor(Color.Transparent);
			for (int i = 0; i < this._characterPreviewLayers.Length; i++)
			{
				UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(1, 7, 0, i, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
				this._characterPreviewLayers[i] = uiimageFramed;
				this._clothingStylesCategoryButton.Append(uiimageFramed);
				this._clothingStylesCategoryButton.OnUpdate += this._clothingStylesCategoryButton_OnUpdate;
			}
			this._hairStylesCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.HairStyle, "Images/UI/CharCreation/HairStyle_Hair", num, num2);
			this._hairStylesCategoryButton.OnLeftMouseDown += this.Click_HairStyles;
			this._hairStylesCategoryButton.SetMiddleTexture(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/HairStyle_Arrow", 1));
			this._hairStylesCategoryButton.SetSnapPoint("Top", 2, null, null);
			categoryContainer.Append(this._hairStylesCategoryButton);
			this._charInfoCategoryButton = this.CreatePickerWithoutClick(UICharacterCreation.CategoryId.CharInfo, "Images/UI/CharCreation/CharInfo", num, num2);
			this._charInfoCategoryButton.OnLeftMouseDown += this.Click_CharInfo;
			this._charInfoCategoryButton.SetSnapPoint("Top", 0, null, null);
			categoryContainer.Append(this._charInfoCategoryButton);
			this.UpdateColorPickers();
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-25f, 1f),
				Top = StyleDimension.FromPixels(6f),
				Left = new StyleDimension(-2.5f, 0f),
				VAlign = 1f,
				HAlign = 0.5f,
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			categoryContainer.Append(uihorizontalSeparator);
			int num3 = 21;
			UIText uitext = new UIText(PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus"), 1f, false)
			{
				Left = new StyleDimension((float)(-(float)num3), 0f),
				VAlign = 0.5f,
				Top = new StyleDimension(-4f, 0f)
			};
			categoryContainer.Append(uitext);
			UIText uitext2 = new UIText(PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus"), 1f, false)
			{
				HAlign = 1f,
				Left = new StyleDimension((float)(12 + num3), 0f),
				VAlign = 0.5f,
				Top = new StyleDimension(-4f, 0f)
			};
			categoryContainer.Append(uitext2);
			this._helpGlyphLeft = uitext;
			this._helpGlyphRight = uitext2;
			categoryContainer.OnUpdate += this.UpdateHelpGlyphs;
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x005931D8 File Offset: 0x005913D8
		private void _clothingStylesCategoryButton_OnUpdate(UIElement affectedElement)
		{
			this._characterPreviewLayers[0].Color = this._player.hairColor;
			this._characterPreviewLayers[1].Color = this._player.eyeColor;
			this._characterPreviewLayers[2].Color = this._player.skinColor;
			this._characterPreviewLayers[3].Color = this._player.shirtColor;
			this._characterPreviewLayers[4].Color = this._player.underShirtColor;
			this._characterPreviewLayers[5].Color = this._player.pantsColor;
			this._characterPreviewLayers[6].Color = this._player.shoeColor;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x00593290 File Offset: 0x00591490
		private void UpdateHelpGlyphs(UIElement element)
		{
			string text = "";
			string text2 = "";
			if (PlayerInput.UsingGamepad)
			{
				text = PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarMinus");
				text2 = PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "HotbarPlus");
			}
			this._helpGlyphLeft.SetText(text);
			this._helpGlyphRight.SetText(text2);
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x005932E0 File Offset: 0x005914E0
		private UIColoredImageButton CreateColorPicker(UICharacterCreation.CategoryId id, string texturePath, float xPositionStart, float xPositionPerId)
		{
			UIColoredImageButton uicoloredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>(texturePath, 1), false);
			this._colorPickers[(int)id] = uicoloredImageButton;
			uicoloredImageButton.VAlign = 0f;
			uicoloredImageButton.HAlign = 0f;
			uicoloredImageButton.Left.Set(xPositionStart + (float)id * xPositionPerId, 0.5f);
			uicoloredImageButton.OnLeftMouseDown += this.Click_ColorPicker;
			uicoloredImageButton.SetSnapPoint("Top", (int)id, null, null);
			return uicoloredImageButton;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x00593368 File Offset: 0x00591568
		private UIColoredImageButton CreatePickerWithoutClick(UICharacterCreation.CategoryId id, string texturePath, float xPositionStart, float xPositionPerId)
		{
			UIColoredImageButton uicoloredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>(texturePath, 1), false);
			uicoloredImageButton.VAlign = 0f;
			uicoloredImageButton.HAlign = 0f;
			uicoloredImageButton.Left.Set(xPositionStart + (float)id * xPositionPerId, 0.5f);
			return uicoloredImageButton;
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x005933B4 File Offset: 0x005915B4
		private void MakeInfoMenu(UIElement parentContainer)
		{
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uielement.SetPadding(10f);
			uielement.PaddingBottom = 0f;
			uielement.PaddingTop = 0f;
			parentContainer.Append(uielement);
			UICharacterNameButton uicharacterNameButton = new UICharacterNameButton(Language.GetText("UI.WorldCreationName"), Language.GetText("UI.PlayerEmptyName"), null);
			uicharacterNameButton.Width = StyleDimension.FromPixelsAndPercent(0f, 1f);
			uicharacterNameButton.HAlign = 0.5f;
			uielement.Append(uicharacterNameButton);
			this._charName = uicharacterNameButton;
			uicharacterNameButton.OnLeftMouseDown += this.Click_Naming;
			uicharacterNameButton.SetSnapPoint("Middle", 0, null, null);
			float num = 4f;
			float num2 = 0f;
			float num3 = 0.4f;
			UIElement uielement2 = new UIElement
			{
				HAlign = 0f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num, num3),
				Height = StyleDimension.FromPixelsAndPercent(-50f, 1f)
			};
			uielement2.SetPadding(0f);
			uielement.Append(uielement2);
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 1f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f - num3),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(uielement2.Height.Pixels, uielement2.Height.Precent)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			uielement.Append(uislicedImage);
			float num4 = 4f;
			UIDifficultyButton uidifficultyButton = new UIDifficultyButton(this._player, Lang.menu[26], Lang.menu[31], 0, Color.Cyan)
			{
				HAlign = 0f,
				VAlign = 1f / (num4 - 1f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num4)
			};
			UIDifficultyButton uidifficultyButton2 = new UIDifficultyButton(this._player, Lang.menu[25], Lang.menu[30], 1, Main.mcColor)
			{
				HAlign = 0f,
				VAlign = 2f / (num4 - 1f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num4)
			};
			UIDifficultyButton uidifficultyButton3 = new UIDifficultyButton(this._player, Lang.menu[24], Lang.menu[29], 2, Main.hcColor)
			{
				HAlign = 0f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num4)
			};
			UIDifficultyButton uidifficultyButton4 = new UIDifficultyButton(this._player, Language.GetText("UI.Creative"), Language.GetText("UI.CreativeDescriptionPlayer"), 3, Main.creativeModeColor)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-num2, 1f / num4)
			};
			UIText uitext = new UIText(Lang.menu[26], 1f, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(15f, 0f),
				IsWrapped = true
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uislicedImage.Append(uitext);
			uielement2.Append(uidifficultyButton4);
			uielement2.Append(uidifficultyButton);
			uielement2.Append(uidifficultyButton2);
			uielement2.Append(uidifficultyButton3);
			this._infoContainer = uielement;
			this._difficultyDescriptionText = uitext;
			uidifficultyButton4.OnLeftMouseDown += this.UpdateDifficultyDescription;
			uidifficultyButton.OnLeftMouseDown += this.UpdateDifficultyDescription;
			uidifficultyButton2.OnLeftMouseDown += this.UpdateDifficultyDescription;
			uidifficultyButton3.OnLeftMouseDown += this.UpdateDifficultyDescription;
			this.UpdateDifficultyDescription(null, null);
			uidifficultyButton4.SetSnapPoint("Middle", 1, null, null);
			uidifficultyButton.SetSnapPoint("Middle", 2, null, null);
			uidifficultyButton2.SetSnapPoint("Middle", 3, null, null);
			uidifficultyButton3.SetSnapPoint("Middle", 4, null, null);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x005938F8 File Offset: 0x00591AF8
		private void UpdateDifficultyDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = Lang.menu[31];
			switch (this._player.difficulty)
			{
			case 0:
				localizedText = Lang.menu[31];
				break;
			case 1:
				localizedText = Lang.menu[30];
				break;
			case 2:
				localizedText = Lang.menu[29];
				break;
			case 3:
				localizedText = Language.GetText("UI.CreativeDescriptionPlayer");
				break;
			}
			this._difficultyDescriptionText.SetText(localizedText);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x0059396C File Offset: 0x00591B6C
		private void MakeHSLMenu(UIElement parentContainer)
		{
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(220f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(158f, 0f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uielement.SetPadding(0f);
			parentContainer.Append(uielement);
			UIElement uielement2 = new UIPanel
			{
				Width = StyleDimension.FromPixelsAndPercent(220f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(104f, 0f),
				HAlign = 0.5f,
				VAlign = 0f,
				Top = StyleDimension.FromPixelsAndPercent(10f, 0f)
			};
			uielement2.SetPadding(0f);
			uielement2.PaddingTop = 3f;
			uielement.Append(uielement2);
			uielement2.Append(this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Hue));
			uielement2.Append(this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Saturation));
			uielement2.Append(this.CreateHSLSlider(UICharacterCreation.HSLSliderId.Luminance));
			UIPanel uipanel = new UIPanel
			{
				VAlign = 1f,
				HAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(100f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(32f, 0f)
			};
			UIText uitext = new UIText("FFFFFF", 1f, false)
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			};
			uipanel.Append(uitext);
			uielement.Append(uipanel);
			UIColoredImageButton uicoloredImageButton = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Copy", 1), true)
			{
				VAlign = 1f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(0f, 0f)
			};
			uicoloredImageButton.OnLeftMouseDown += this.Click_CopyHex;
			uielement.Append(uicoloredImageButton);
			this._copyHexButton = uicoloredImageButton;
			UIColoredImageButton uicoloredImageButton2 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Paste", 1), true)
			{
				VAlign = 1f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(40f, 0f)
			};
			uicoloredImageButton2.OnLeftMouseDown += this.Click_PasteHex;
			uielement.Append(uicoloredImageButton2);
			this._pasteHexButton = uicoloredImageButton2;
			UIColoredImageButton uicoloredImageButton3 = new UIColoredImageButton(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Randomize", 1), true)
			{
				VAlign = 1f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(80f, 0f)
			};
			uicoloredImageButton3.OnLeftMouseDown += this.Click_RandomizeSingleColor;
			uielement.Append(uicoloredImageButton3);
			this._randomColorButton = uicoloredImageButton3;
			this._hslContainer = uielement;
			this._hslHexText = uitext;
			uicoloredImageButton.SetSnapPoint("Low", 0, null, null);
			uicoloredImageButton2.SetSnapPoint("Low", 1, null, null);
			uicoloredImageButton3.SetSnapPoint("Low", 2, null, null);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x00593C8E File Offset: 0x00591E8E
		private void Click_VoicePitch(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x00593CA8 File Offset: 0x00591EA8
		private UIColoredSlider CreateHSLSlider(UICharacterCreation.HSLSliderId id)
		{
			UIColoredSlider uicoloredSlider = this.CreateHSLSliderButtonBase(id);
			uicoloredSlider.VAlign = 0f;
			uicoloredSlider.HAlign = 0f;
			uicoloredSlider.Width = StyleDimension.FromPixelsAndPercent(-10f, 1f);
			uicoloredSlider.Top.Set((float)((UICharacterCreation.HSLSliderId)30 * id), 0f);
			uicoloredSlider.OnLeftMouseDown += this.Click_ColorPicker;
			uicoloredSlider.SetSnapPoint("Middle", (int)id, null, new Vector2?(new Vector2(0f, 20f)));
			return uicoloredSlider;
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x00593D38 File Offset: 0x00591F38
		private UIColoredSlider CreateHSLSliderButtonBase(UICharacterCreation.HSLSliderId id)
		{
			UIColoredSlider uicoloredSlider;
			if (id != UICharacterCreation.HSLSliderId.Saturation)
			{
				if (id != UICharacterCreation.HSLSliderId.Luminance)
				{
					uicoloredSlider = new UIColoredSlider(LocalizedText.Empty, () => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Hue), delegate(float x)
					{
						this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Hue, x);
					}, new Action(this.UpdateHSL_H), (float x) => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Hue, x), Color.Transparent);
				}
				else
				{
					uicoloredSlider = new UIColoredSlider(LocalizedText.Empty, () => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Luminance), delegate(float x)
					{
						this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Luminance, x);
					}, new Action(this.UpdateHSL_L), (float x) => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Luminance, x), Color.Transparent);
				}
			}
			else
			{
				uicoloredSlider = new UIColoredSlider(LocalizedText.Empty, () => this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Saturation), delegate(float x)
				{
					this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Saturation, x);
				}, new Action(this.UpdateHSL_S), (float x) => this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Saturation, x), Color.Transparent);
			}
			return uicoloredSlider;
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x00593E1C File Offset: 0x0059201C
		private void UpdateHSL_H()
		{
			float num = UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.X, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Hue, num);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x00593E5C File Offset: 0x0059205C
		private void UpdateHSL_S()
		{
			float num = UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.Y, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Saturation, num);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x00593E9C File Offset: 0x0059209C
		private void UpdateHSL_L()
		{
			float num = UILinksInitializer.HandleSliderHorizontalInput(this._currentColorHSL.Z, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Luminance, num);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x00593EDB File Offset: 0x005920DB
		private float GetHSLSliderPosition(UICharacterCreation.HSLSliderId id)
		{
			switch (id)
			{
			case UICharacterCreation.HSLSliderId.Hue:
				return this._currentColorHSL.X;
			case UICharacterCreation.HSLSliderId.Saturation:
				return this._currentColorHSL.Y;
			case UICharacterCreation.HSLSliderId.Luminance:
				return this._currentColorHSL.Z;
			default:
				return 1f;
			}
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x00593F1C File Offset: 0x0059211C
		private void UpdateHSLValue(UICharacterCreation.HSLSliderId id, float value)
		{
			switch (id)
			{
			case UICharacterCreation.HSLSliderId.Hue:
				this._currentColorHSL.X = value;
				break;
			case UICharacterCreation.HSLSliderId.Saturation:
				this._currentColorHSL.Y = value;
				break;
			case UICharacterCreation.HSLSliderId.Luminance:
				this._currentColorHSL.Z = value;
				break;
			}
			Color color = UICharacterCreation.ScaledHslToRgb(this._currentColorHSL.X, this._currentColorHSL.Y, this._currentColorHSL.Z);
			this.ApplyPendingColor(color);
			UIColoredImageButton uicoloredImageButton = this._colorPickers[(int)this._selectedPicker];
			if (uicoloredImageButton != null)
			{
				uicoloredImageButton.SetColor(color);
			}
			if (this._selectedPicker == UICharacterCreation.CategoryId.HairColor)
			{
				this._hairStylesCategoryButton.SetColor(color);
			}
			this.UpdateHexText(color);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x00593FC8 File Offset: 0x005921C8
		private Color GetHSLSliderColorAt(UICharacterCreation.HSLSliderId id, float pointAt)
		{
			switch (id)
			{
			case UICharacterCreation.HSLSliderId.Hue:
				return UICharacterCreation.ScaledHslToRgb(pointAt, 1f, 0.5f);
			case UICharacterCreation.HSLSliderId.Saturation:
				return UICharacterCreation.ScaledHslToRgb(this._currentColorHSL.X, pointAt, this._currentColorHSL.Z);
			case UICharacterCreation.HSLSliderId.Luminance:
				return UICharacterCreation.ScaledHslToRgb(this._currentColorHSL.X, this._currentColorHSL.Y, pointAt);
			default:
				return Color.White;
			}
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x0059403C File Offset: 0x0059223C
		private void ApplyPendingColor(Color pendingColor)
		{
			switch (this._selectedPicker)
			{
			case UICharacterCreation.CategoryId.HairColor:
				this._player.hairColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Eye:
				this._player.eyeColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Skin:
				this._player.skinColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Shirt:
				this._player.shirtColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Undershirt:
				this._player.underShirtColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Pants:
				this._player.pantsColor = pendingColor;
				return;
			case UICharacterCreation.CategoryId.Shoes:
				this._player.shoeColor = pendingColor;
				return;
			default:
				return;
			}
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x005940CF File Offset: 0x005922CF
		private void UpdateHexText(Color pendingColor)
		{
			this._hslHexText.SetText(UICharacterCreation.GetHexText(pendingColor));
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x005940E2 File Offset: 0x005922E2
		private static string GetHexText(Color pendingColor)
		{
			return "#" + pendingColor.Hex3().ToUpper();
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x005940FC File Offset: 0x005922FC
		private void MakeBackAndCreatebuttons(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 0f,
				Top = StyleDimension.FromPixels(-45f)
			};
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftMouseDown += this.Click_GoBack;
			uitextPanel.SetSnapPoint("Back", 0, null, null);
			outerContainer.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Create"), 0.7f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 1f,
				Top = StyleDimension.FromPixels(-45f)
			};
			uitextPanel2.OnMouseOver += this.FadedMouseOver;
			uitextPanel2.OnMouseOut += this.FadedMouseOut;
			uitextPanel2.OnLeftMouseDown += this.Click_NamingAndCreating;
			uitextPanel2.SetSnapPoint("Create", 0, null, null);
			outerContainer.Append(uitextPanel2);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x00594281 File Offset: 0x00592481
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			UICharacterCreation.GoBack();
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x00594288 File Offset: 0x00592488
		private static void GoBack()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			if (UICharacterCreation.dirty)
			{
				UICharacterCreation.BackupConfirmationState = Main.MenuUI.CurrentState;
				Main.menuMode = 40;
				return;
			}
			Main.OpenCharacterSelectUI();
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x005942C4 File Offset: 0x005924C4
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x0058539D File Offset: 0x0058359D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x0059431C File Offset: 0x0059251C
		private void Click_ColorPicker(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			for (int i = 0; i < this._colorPickers.Length; i++)
			{
				if (this._colorPickers[i] == evt.Target)
				{
					this.SelectColorPicker((UICharacterCreation.CategoryId)i);
					return;
				}
			}
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x0059436C File Offset: 0x0059256C
		private void Click_ClothStyles(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.UnselectAllCategories();
			this._selectedPicker = UICharacterCreation.CategoryId.Clothing;
			this._middleContainer.Append(this._clothStylesContainer);
			this._clothingStylesCategoryButton.SetSelected(true);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x005943B8 File Offset: 0x005925B8
		private void Click_HairStyles(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.UnselectAllCategories();
			this._selectedPicker = UICharacterCreation.CategoryId.HairStyle;
			this._middleContainer.Append(this._hairstylesContainer);
			this._hairStylesCategoryButton.SetSelected(true);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x00594404 File Offset: 0x00592604
		private void Click_CharInfo(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.UnselectAllCategories();
			this._selectedPicker = UICharacterCreation.CategoryId.CharInfo;
			this._middleContainer.Append(this._infoContainer);
			this._charInfoCategoryButton.SetSelected(true);
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x00594450 File Offset: 0x00592650
		private void Click_CharClothStyle(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._maleArmor.HeadItem != 0 || this._maleArmor.BodyItem != 0 || this._maleArmor.LegItem != 0)
			{
				this.EquipArmorNone(evt, listeningElement);
				return;
			}
			UIClothStyleButton uiclothStyleButton = listeningElement as UIClothStyleButton;
			if (uiclothStyleButton != null)
			{
				int clothStyleId = uiclothStyleButton.ClothStyleId;
				this._player.skinVariant = clothStyleId;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._clothingStylesCategoryButton.SetImageWithoutSettingSize(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/" + (this._player.Male ? "ClothStyleMale" : "ClothStyleFemale"), 1));
			this.UpdateSelectedGender();
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x00594500 File Offset: 0x00592700
		private void TryChangingVoice()
		{
			if (this._player.Male && this._player.voiceVariant == 2)
			{
				this._player.voiceVariant = 1;
			}
			if (!this._player.Male && this._player.voiceVariant == 1)
			{
				this._player.voiceVariant = 2;
			}
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x0059455C File Offset: 0x0059275C
		private void UpdateSelectedGender()
		{
			if (this._oldMaleForVoiceAutoSwitch == this._player.Male)
			{
				this.PlayVoicePreview();
				return;
			}
			int voiceVariant = this._player.voiceVariant;
			if (voiceVariant != 1)
			{
				if (voiceVariant == 2)
				{
					if (!this._oldMaleForVoiceAutoSwitch)
					{
						this._player.voiceVariant = 1;
					}
				}
			}
			else if (this._oldMaleForVoiceAutoSwitch)
			{
				this._player.voiceVariant = 2;
			}
			this._oldMaleForVoiceAutoSwitch = this._player.Male;
			this.PlayVoicePreview();
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x005945DA File Offset: 0x005927DA
		private void Click_CopyHex(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Platform.Get<IClipboard>().Value = this._hslHexText.Text;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00594608 File Offset: 0x00592808
		private void Click_PasteHex(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			string value = Platform.Get<IClipboard>().Value;
			Vector3 vector;
			if (this.GetHexColor(value, out vector))
			{
				this.ApplyPendingColor(UICharacterCreation.ScaledHslToRgb(vector.X, vector.Y, vector.Z));
				this._currentColorHSL = vector;
				this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(vector.X, vector.Y, vector.Z));
				this.UpdateColorPickers();
			}
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x00594688 File Offset: 0x00592888
		private string GetPlayerTemplateValues()
		{
			string text = JsonConvert.SerializeObject(new Dictionary<string, object>
			{
				{ "version", 1 },
				{
					"hairStyle",
					this._player.hair
				},
				{
					"clothingStyle",
					this._player.skinVariant
				},
				{
					"voiceStyle",
					this._player.voiceVariant
				},
				{
					"voicePitch",
					this._player.voicePitchOffset
				},
				{
					"hairColor",
					UICharacterCreation.GetHexText(this._player.hairColor)
				},
				{
					"eyeColor",
					UICharacterCreation.GetHexText(this._player.eyeColor)
				},
				{
					"skinColor",
					UICharacterCreation.GetHexText(this._player.skinColor)
				},
				{
					"shirtColor",
					UICharacterCreation.GetHexText(this._player.shirtColor)
				},
				{
					"underShirtColor",
					UICharacterCreation.GetHexText(this._player.underShirtColor)
				},
				{
					"pantsColor",
					UICharacterCreation.GetHexText(this._player.pantsColor)
				},
				{
					"shoeColor",
					UICharacterCreation.GetHexText(this._player.shoeColor)
				}
			}, new JsonSerializerSettings
			{
				TypeNameHandling = 4,
				MetadataPropertyHandling = 1,
				Formatting = 1
			});
			PlayerInput.PrettyPrintProfiles(ref text);
			return text;
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x005947FC File Offset: 0x005929FC
		private void Click_CopyPlayerTemplate(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Platform.Get<IClipboard>().Value = this.GetPlayerTemplateValues();
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00594824 File Offset: 0x00592A24
		private void Click_PastePlayerTemplate(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			try
			{
				string text = Platform.Get<IClipboard>().Value;
				int num = text.IndexOf("{");
				if (num != -1)
				{
					text = text.Substring(num);
					int num2 = text.LastIndexOf("}");
					if (num2 != -1)
					{
						text = text.Substring(0, num2 + 1);
						Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(text);
						if (dictionary != null)
						{
							Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
							foreach (KeyValuePair<string, object> keyValuePair in dictionary)
							{
								dictionary2[keyValuePair.Key.ToLower()] = keyValuePair.Value;
							}
							object obj;
							if (dictionary2.TryGetValue("version", out obj))
							{
								long num3 = (long)obj;
							}
							if (dictionary2.TryGetValue("hairstyle", out obj))
							{
								int num4 = (int)((long)obj);
								if (Main.Hairstyles.AvailableHairstyles.Contains(num4))
								{
									this._player.hair = num4;
									this._lastSelectedHairstyle = new int?(num4);
								}
							}
							if (dictionary2.TryGetValue("clothingstyle", out obj))
							{
								int num5 = (int)((long)obj);
								if (this._validClothStyles.Contains(num5))
								{
									this._player.skinVariant = num5;
								}
							}
							if (dictionary2.TryGetValue("voicestyle", out obj))
							{
								int num6 = (int)((long)obj);
								if (this._validVoiceStyles.Contains(num6))
								{
									this._player.voiceVariant = num6;
								}
							}
							if (dictionary2.TryGetValue("voicepitch", out obj))
							{
								float num7 = (float)((double)obj);
								this._player.voicePitchOffset = num7;
								this._pitchAmount = num7;
							}
							Vector3 vector;
							if (dictionary2.TryGetValue("haircolor", out obj) && this.GetHexColor((string)obj, out vector))
							{
								this._player.hairColor = UICharacterCreation.ScaledHslToRgb(vector);
							}
							if (dictionary2.TryGetValue("eyecolor", out obj) && this.GetHexColor((string)obj, out vector))
							{
								this._player.eyeColor = UICharacterCreation.ScaledHslToRgb(vector);
							}
							if (dictionary2.TryGetValue("skincolor", out obj) && this.GetHexColor((string)obj, out vector))
							{
								this._player.skinColor = UICharacterCreation.ScaledHslToRgb(vector);
							}
							if (dictionary2.TryGetValue("shirtcolor", out obj) && this.GetHexColor((string)obj, out vector))
							{
								this._player.shirtColor = UICharacterCreation.ScaledHslToRgb(vector);
							}
							if (dictionary2.TryGetValue("undershirtcolor", out obj) && this.GetHexColor((string)obj, out vector))
							{
								this._player.underShirtColor = UICharacterCreation.ScaledHslToRgb(vector);
							}
							if (dictionary2.TryGetValue("pantscolor", out obj) && this.GetHexColor((string)obj, out vector))
							{
								this._player.pantsColor = UICharacterCreation.ScaledHslToRgb(vector);
							}
							if (dictionary2.TryGetValue("shoecolor", out obj) && this.GetHexColor((string)obj, out vector))
							{
								this._player.shoeColor = UICharacterCreation.ScaledHslToRgb(vector);
							}
							this.Click_CharClothStyle(null, null);
							this.UpdateColorPickers();
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x00594B8C File Offset: 0x00592D8C
		private void Click_VoicePlay(UIMouseEvent evt, UIElement listeningElement)
		{
			this.PlayVoicePreview();
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x00594B94 File Offset: 0x00592D94
		private void PlayVoicePreview()
		{
			if (this._playedVoicePreviewThisFrame)
			{
				return;
			}
			this._playedVoicePreviewThisFrame = true;
			Vector2 position = this._player.position;
			this._player.position = new Vector2(-1f, -1f);
			this._player.PlayHurtSound();
			this._player.position = position;
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x00594BEE File Offset: 0x00592DEE
		private void Click_VoiceCycleBack(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.CycleVoiceStyle(this._player, -1);
			this.PlayVoicePreview();
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x00594C02 File Offset: 0x00592E02
		private void Click_VoiceCycleForward(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.CycleVoiceStyle(this._player, 1);
			this.PlayVoicePreview();
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00009E46 File Offset: 0x00008046
		private void Update_VoiceIconColor()
		{
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x00594C18 File Offset: 0x00592E18
		private void Click_RandomizePlayer(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Player player = this._player;
			int num = Main.rand.Next(Main.Hairstyles.AvailableHairstyles.Count);
			player.hair = Main.Hairstyles.AvailableHairstyles[num];
			this._lastSelectedHairstyle = new int?(player.hair);
			player.eyeColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			while ((int)(player.eyeColor.R + player.eyeColor.G + player.eyeColor.B) > 300)
			{
				player.eyeColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			}
			float num2 = (float)Main.rand.Next(60, 120) * 0.01f;
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			player.skinColor.R = (byte)((float)Main.rand.Next(240, 255) * num2);
			player.skinColor.G = (byte)((float)Main.rand.Next(110, 140) * num2);
			player.skinColor.B = (byte)((float)Main.rand.Next(75, 110) * num2);
			player.hairColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.shirtColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.underShirtColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.pantsColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.shoeColor = UICharacterCreation.ScaledHslToRgb(UICharacterCreation.GetRandomColorVector());
			player.skinVariant = this._validClothStyles[Main.rand.Next(this._validClothStyles.Length)];
			player.voiceVariant = (player.Male ? 1 : 2);
			if (Main.rand.Next(2) == 0)
			{
				player.voiceVariant = 3;
			}
			int num3 = player.hair + 1;
			if (num3 <= 135)
			{
				if (num3 <= 124)
				{
					switch (num3)
					{
					case 5:
					case 6:
					case 7:
					case 10:
					case 12:
					case 19:
					case 22:
					case 23:
					case 26:
					case 27:
					case 30:
					case 33:
					case 34:
					case 35:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 51:
					case 56:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
					case 70:
					case 71:
					case 72:
					case 73:
					case 74:
					case 79:
					case 80:
					case 81:
					case 82:
					case 84:
					case 85:
					case 86:
					case 87:
					case 88:
					case 90:
					case 91:
					case 92:
					case 93:
					case 95:
					case 96:
					case 98:
					case 100:
					case 102:
					case 104:
					case 107:
					case 108:
					case 113:
						break;
					case 8:
					case 9:
					case 11:
					case 13:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 20:
					case 21:
					case 24:
					case 25:
					case 28:
					case 29:
					case 31:
					case 32:
					case 36:
					case 42:
					case 43:
					case 50:
					case 52:
					case 53:
					case 54:
					case 55:
					case 57:
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
					case 63:
					case 64:
					case 75:
					case 76:
					case 77:
					case 78:
					case 83:
					case 89:
					case 94:
					case 97:
					case 99:
					case 101:
					case 103:
					case 105:
					case 106:
					case 109:
					case 110:
					case 111:
					case 112:
						goto IL_03E7;
					default:
						if (num3 != 124)
						{
							goto IL_03E7;
						}
						break;
					}
				}
				else if (num3 != 126 && num3 - 133 > 2)
				{
					goto IL_03E7;
				}
			}
			else if (num3 <= 147)
			{
				if (num3 != 144 && num3 - 146 > 1)
				{
					goto IL_03E7;
				}
			}
			else if (num3 != 163 && num3 != 165)
			{
				goto IL_03E7;
			}
			player.Male = false;
			goto IL_03EE;
			IL_03E7:
			player.Male = true;
			IL_03EE:
			this._femaleArmor = (this._maleArmor = default(UICharacterCreation.ArmorAssignments));
			this.Click_CharClothStyle(null, null);
			this.UpdateSelectedGender();
			this.UpdateColorPickers();
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x00595044 File Offset: 0x00593244
		private void Click_Naming(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			this._player.name = "";
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNaming), new Action(this.OnCanceledNaming), 0, true, 20);
			Main.MenuUI.SetState(uivirtualKeyboard);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x005950BC File Offset: 0x005932BC
		private void Click_NamingAndCreating(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			if (string.IsNullOrEmpty(this._player.name))
			{
				this._player.name = "";
				Main.clrInput();
				UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNamingAndCreating), new Action(this.OnCanceledNaming), 0, false, 20);
				Main.MenuUI.SetState(uivirtualKeyboard);
				return;
			}
			this.FinishCreatingCharacter();
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x0059514B File Offset: 0x0059334B
		private void OnFinishedNaming(string name)
		{
			this._player.name = name.Trim();
			Main.MenuUI.SetState(this);
			this._charName.SetContents(this._player.name);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x0059517F File Offset: 0x0059337F
		private void OnCanceledNaming()
		{
			Main.MenuUI.SetState(this);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x0059518C File Offset: 0x0059338C
		private void OnFinishedNamingAndCreating(string name)
		{
			this._player.name = name.Trim();
			Main.MenuUI.SetState(this);
			this._charName.SetContents(this._player.name);
			this.FinishCreatingCharacter();
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x005951C6 File Offset: 0x005933C6
		private void FinishCreatingCharacter()
		{
			this.TryAutoAssigningHair();
			this.SetupPlayerStatsAndInventoryBasedOnDifficulty();
			PlayerFileData.CreateAndSave(this._player);
			Main.LoadPlayers();
			Main.menuMode = 1;
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x005951EC File Offset: 0x005933EC
		private void SetupPlayerStatsAndInventoryBasedOnDifficulty()
		{
			this._femaleArmor = (this._maleArmor = default(UICharacterCreation.ArmorAssignments));
			this.UpdatePreviewItems();
			int num = 0;
			byte difficulty = this._player.difficulty;
			if (difficulty == 3)
			{
				this._player.statLife = (this._player.statLifeMax = 100);
				this._player.statMana = (this._player.statManaMax = 20);
				this._player.inventory[num].SetDefaults(6, null);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(1, null);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(10, null);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(7, null);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(4281, null);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(8, null);
				this._player.inventory[num++].stack = 100;
				this._player.inventory[num].SetDefaults(965, null);
				this._player.inventory[num++].stack = 100;
				this._player.inventory[num++].SetDefaults(50, null);
				this._player.inventory[num++].SetDefaults(84, null);
				this._player.armor[3].SetDefaults(4978, null);
				this._player.armor[3].Prefix(-1);
				string text = this._player.name.ToLower();
				if (text == "wolf pet" || text == "wolfpet")
				{
					this._player.miscEquips[3].SetDefaults(5130, null);
				}
				this._player.AddBuff(216, 3600, false);
			}
			else
			{
				this._player.inventory[num].SetDefaults(3507, null);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(3509, null);
				this._player.inventory[num++].Prefix(-1);
				this._player.inventory[num].SetDefaults(3506, null);
				this._player.inventory[num++].Prefix(-1);
			}
			if (Main.runningCollectorsEdition)
			{
				this._player.inventory[num++].SetDefaults(603, null);
			}
			this._player.savedPerPlayerFieldsThatArentInThePlayerClass = new Player.SavedPlayerDataWithAnnoyingRules();
			CreativePowerManager.Instance.ResetDataForNewPlayer(this._player);
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x00595534 File Offset: 0x00593734
		private bool GetHexColor(string hexString, out Vector3 hsl)
		{
			if (hexString.StartsWith("#"))
			{
				hexString = hexString.Substring(1);
			}
			uint num;
			if (hexString.Length <= 6 && uint.TryParse(hexString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out num))
			{
				uint num2 = num & 255U;
				uint num3 = (num >> 8) & 255U;
				uint num4 = (num >> 16) & 255U;
				hsl = UICharacterCreation.RgbToScaledHsl(new Color((int)num4, (int)num3, (int)num2));
				return true;
			}
			hsl = Vector3.Zero;
			return false;
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x005955B4 File Offset: 0x005937B4
		private void Click_RandomizeSingleColor(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Vector3 randomColorVector = UICharacterCreation.GetRandomColorVector();
			this.ApplyPendingColor(UICharacterCreation.ScaledHslToRgb(randomColorVector.X, randomColorVector.Y, randomColorVector.Z));
			this._currentColorHSL = randomColorVector;
			this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(randomColorVector.X, randomColorVector.Y, randomColorVector.Z));
			this.UpdateColorPickers();
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x00595623 File Offset: 0x00593823
		private static Vector3 GetRandomColorVector()
		{
			return new Vector3(Main.rand.NextFloat(), Main.rand.NextFloat(), Main.rand.NextFloat());
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x00595648 File Offset: 0x00593848
		private void UnselectAllCategories()
		{
			foreach (UIColoredImageButton uicoloredImageButton in this._colorPickers)
			{
				if (uicoloredImageButton != null)
				{
					uicoloredImageButton.SetSelected(false);
				}
			}
			this._clothingStylesCategoryButton.SetSelected(false);
			this._hairStylesCategoryButton.SetSelected(false);
			this._charInfoCategoryButton.SetSelected(false);
			this._hslContainer.Remove();
			this._hairstylesContainer.Remove();
			this._clothStylesContainer.Remove();
			this._infoContainer.Remove();
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x005956C8 File Offset: 0x005938C8
		private void SelectColorPicker(UICharacterCreation.CategoryId selection)
		{
			this._selectedPicker = selection;
			if (selection == UICharacterCreation.CategoryId.CharInfo)
			{
				this.Click_CharInfo(null, null);
				return;
			}
			if (selection == UICharacterCreation.CategoryId.Clothing)
			{
				this.Click_ClothStyles(null, null);
				return;
			}
			if (selection == UICharacterCreation.CategoryId.HairStyle)
			{
				this.Click_HairStyles(null, null);
				return;
			}
			this.UnselectAllCategories();
			this._middleContainer.Append(this._hslContainer);
			for (int i = 0; i < this._colorPickers.Length; i++)
			{
				if (this._colorPickers[i] != null)
				{
					this._colorPickers[i].SetSelected(i == (int)selection);
				}
			}
			Vector3 vector = Vector3.One;
			switch (this._selectedPicker)
			{
			case UICharacterCreation.CategoryId.HairColor:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.hairColor);
				break;
			case UICharacterCreation.CategoryId.Eye:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.eyeColor);
				break;
			case UICharacterCreation.CategoryId.Skin:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.skinColor);
				break;
			case UICharacterCreation.CategoryId.Shirt:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.shirtColor);
				break;
			case UICharacterCreation.CategoryId.Undershirt:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.underShirtColor);
				break;
			case UICharacterCreation.CategoryId.Pants:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.pantsColor);
				break;
			case UICharacterCreation.CategoryId.Shoes:
				vector = UICharacterCreation.RgbToScaledHsl(this._player.shoeColor);
				break;
			}
			this._currentColorHSL = vector;
			this.UpdateHexText(UICharacterCreation.ScaledHslToRgb(vector.X, vector.Y, vector.Z));
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x00595824 File Offset: 0x00593A24
		private void UpdateColorPickers()
		{
			UICharacterCreation.CategoryId selectedPicker = this._selectedPicker;
			this._colorPickers[3].SetColor(this._player.hairColor);
			this._hairStylesCategoryButton.SetColor(this._player.hairColor);
			this._colorPickers[4].SetColor(this._player.eyeColor);
			this._colorPickers[5].SetColor(this._player.skinColor);
			this._colorPickers[6].SetColor(this._player.shirtColor);
			this._colorPickers[7].SetColor(this._player.underShirtColor);
			this._colorPickers[8].SetColor(this._player.pantsColor);
			this._colorPickers[9].SetColor(this._player.shoeColor);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x005958F8 File Offset: 0x00593AF8
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			string text = null;
			if (this._copyHexButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.CopyColorToClipboard");
			}
			if (this._pasteHexButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PasteColorFromClipboard");
			}
			if (this._randomColorButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.RandomizeColor");
			}
			if (this._copyTemplateButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.CopyPlayerToClipboard");
			}
			if (this._pasteTemplateButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PastePlayerFromClipboard");
			}
			if (this._randomizePlayerButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.RandomizePlayer");
			}
			if (this._previewArmorButton[0].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PreviewArmorNone");
			}
			if (this._previewArmorButton[1].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PreviewArmorHallowed");
			}
			if (this._previewArmorButton[2].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PreviewArmorSilver");
			}
			if (this._previewArmorButton[3].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PreviewArmorFormal");
			}
			if (this._previewArmorButton[4].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PreviewArmorSwimming");
			}
			if (UISliderBase.CurrentAimedSlider == this._pitchSlider)
			{
				text = Language.GetTextValue("UI.PlayerCreateVoicePitch");
			}
			if (this._voicePrevious.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateVoicePrev");
			}
			if (this._voiceNext.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateVoiceNext");
			}
			if (this._voicePlay.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateVoicePlay");
			}
			if (this._charInfoCategoryButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategoryInfo");
			}
			if (this._clothingStylesCategoryButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategoryBodyStyle");
			}
			if (this._hairStylesCategoryButton.IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategoryHairStyle");
			}
			if (this._colorPickers[3].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategoryHairColor");
			}
			if (this._colorPickers[4].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategoryEyeColor");
			}
			if (this._colorPickers[5].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategorySkinColor");
			}
			if (this._colorPickers[6].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategoryShirtColor");
			}
			if (this._colorPickers[7].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategoryUndershirtColor");
			}
			if (this._colorPickers[8].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategoryPantsColor");
			}
			if (this._colorPickers[9].IsMouseHovering)
			{
				text = Language.GetTextValue("UI.PlayerCreateCategoryShoesColor");
			}
			if (text != null)
			{
				float x = FontAssets.MouseText.Value.MeasureString(text).X;
				Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY) + new Vector2(16f);
				if (vector.Y > (float)(Main.screenHeight - 30))
				{
					vector.Y = (float)(Main.screenHeight - 30);
				}
				if (vector.X > (float)Main.screenWidth - x)
				{
					vector.X = (float)(Main.screenWidth - 460);
				}
				Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, vector.X, vector.Y, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, Vector2.Zero, 1f);
			}
			this.SetupGamepadPoints(spriteBatch);
			this._tips.Update();
			int num = Main.screenHeight - 560;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = 150;
			if (num < 300)
			{
				num2 = num / 2;
			}
			if (num > 30)
			{
				this._tips.TipOffsetY = (float)(-(float)num2);
				this._tips.Draw();
			}
			if (!UICharacterCreation.dirty)
			{
				if (!string.IsNullOrEmpty(this._player.name))
				{
					UICharacterCreation.dirty = true;
				}
				if (this.GetPlayerTemplateValues() != this.initialState)
				{
					UICharacterCreation.dirty = true;
				}
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x00595CD8 File Offset: 0x00593ED8
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int num2 = num + 20;
			int num3 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			SnapPoint snapPoint = snapPoints.First((SnapPoint a) => a.Name == "Back");
			SnapPoint snapPoint2 = snapPoints.First((SnapPoint a) => a.Name == "Create");
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num3];
			uilinkPoint.Unlink();
			UILinkPointNavigator.SetPosition(num3, snapPoint.Position);
			num3++;
			UILinkPoint uilinkPoint2 = UILinkPointNavigator.Points[num3];
			uilinkPoint2.Unlink();
			UILinkPointNavigator.SetPosition(num3, snapPoint2.Position);
			num3++;
			uilinkPoint.Right = uilinkPoint2.ID;
			uilinkPoint2.Left = uilinkPoint.ID;
			this._foundPoints.Clear();
			this._foundPoints.Add(uilinkPoint.ID);
			this._foundPoints.Add(uilinkPoint2.ID);
			List<SnapPoint> list = snapPoints.Where((SnapPoint a) => a.Name == "Top").ToList<SnapPoint>();
			list.Sort(new Comparison<SnapPoint>(this.SortPoints));
			for (int i = 0; i < list.Count; i++)
			{
				UILinkPoint uilinkPoint3 = UILinkPointNavigator.Points[num3];
				uilinkPoint3.Unlink();
				UILinkPointNavigator.SetPosition(num3, list[i].Position);
				uilinkPoint3.Left = num3 - 1;
				uilinkPoint3.Right = num3 + 1;
				uilinkPoint3.Down = num2;
				if (i == 0)
				{
					uilinkPoint3.Left = -3;
				}
				if (i == list.Count - 1)
				{
					uilinkPoint3.Right = -4;
				}
				if (this._selectedPicker == UICharacterCreation.CategoryId.HairStyle || this._selectedPicker == UICharacterCreation.CategoryId.Clothing)
				{
					uilinkPoint3.Down = num2 + i;
				}
				this._foundPoints.Add(num3);
				num3++;
			}
			List<SnapPoint> list2 = snapPoints.Where((SnapPoint a) => a.Name == "Middle").ToList<SnapPoint>();
			list2.Sort(new Comparison<SnapPoint>(this.SortPoints));
			num3 = num2;
			switch (this._selectedPicker)
			{
			case UICharacterCreation.CategoryId.CharInfo:
			{
				for (int j = 0; j < list2.Count; j++)
				{
					UILinkPoint andSet = this.GetAndSet(num3, list2[j]);
					andSet.Up = andSet.ID - 1;
					andSet.Down = andSet.ID + 1;
					if (j == 0)
					{
						andSet.Up = num + 2;
					}
					if (j == list2.Count - 1)
					{
						andSet.Down = uilinkPoint.ID;
						uilinkPoint.Up = andSet.ID;
						uilinkPoint2.Up = andSet.ID;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				break;
			}
			case UICharacterCreation.CategoryId.Clothing:
			{
				List<SnapPoint> list3 = snapPoints.Where((SnapPoint a) => a.Name == "Preview").ToList<SnapPoint>();
				list3.Sort(new Comparison<SnapPoint>(this.SortPoints));
				List<SnapPoint> list4 = snapPoints.Where((SnapPoint a) => a.Name == "Low").ToList<SnapPoint>();
				list4.Sort(new Comparison<SnapPoint>(this.SortPoints));
				int num4 = -2;
				SnapPoint snapPoint3 = null;
				UILinkPoint uilinkPoint4 = null;
				if (this._pitchSlider.GetSnapPoint(out snapPoint3))
				{
					uilinkPoint4 = this.GetAndSet(num2 + 40, snapPoint3);
					this._foundPoints.Add(uilinkPoint4.ID);
				}
				uilinkPoint4.Down = uilinkPoint.ID;
				int num5 = num2 + 20;
				num3 = num2 + 20;
				int num6 = num3 + list4.Count;
				UILinkPoint uilinkPoint5 = null;
				for (int k = 0; k < list4.Count; k++)
				{
					UILinkPoint andSet2 = this.GetAndSet(num3, list4[k]);
					andSet2.Up = num2 + k + 2;
					andSet2.Down = uilinkPoint4.ID;
					if (k >= 3)
					{
						andSet2.Up = num6 + (k - 3) + 1;
						andSet2.Down = uilinkPoint2.ID;
					}
					andSet2.Left = andSet2.ID - 1;
					andSet2.Right = andSet2.ID + 1;
					if (k == 0)
					{
						num4 = andSet2.ID;
						andSet2.Left = andSet2.ID + 5;
						uilinkPoint.Up = andSet2.ID;
					}
					if (k == list4.Count - 1)
					{
						int id = andSet2.ID;
						andSet2.Right = andSet2.ID - 5;
						uilinkPoint2.Up = andSet2.ID;
					}
					if (k == 1)
					{
						uilinkPoint5 = andSet2;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				for (int l = 0; l < list3.Count; l++)
				{
					UILinkPoint andSet3 = this.GetAndSet(num3, list3[l]);
					andSet3.Up = num2 + l + 5;
					andSet3.Down = num5 + ((int)MathHelper.Clamp((float)l, 1f, 4f) - 1) + 3;
					andSet3.Left = andSet3.ID - 1;
					andSet3.Right = andSet3.ID + 1;
					if (l == 0)
					{
						andSet3.Left = num5 + 2;
					}
					if (l == list3.Count - 1)
					{
						andSet3.Right = num5;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				if (list4.Count > 1)
				{
					uilinkPoint4.Up = uilinkPoint5.ID;
				}
				uilinkPoint.Up = uilinkPoint4.ID;
				num3 = num2;
				for (int m = 0; m < list2.Count; m++)
				{
					UILinkPoint andSet4 = this.GetAndSet(num3, list2[m]);
					andSet4.Up = num + 2 + m;
					andSet4.Left = andSet4.ID - 1;
					andSet4.Right = andSet4.ID + 1;
					if (m == 0)
					{
						andSet4.Left = andSet4.ID + 9;
					}
					if (m == list2.Count - 1)
					{
						andSet4.Right = andSet4.ID - 9;
					}
					andSet4.Down = num4;
					if (m >= 5)
					{
						andSet4.Down = num6 + m - 5;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				break;
			}
			case UICharacterCreation.CategoryId.HairStyle:
				if (list2.Count != 0)
				{
					this._helper.CullPointsOutOfElementArea(spriteBatch, list2, this._hairstylesContainer);
					SnapPoint snapPoint4 = list2[list2.Count - 1];
					int num7 = snapPoint4.Id / 10;
					int num8 = snapPoint4.Id % 10;
					int count = Main.Hairstyles.AvailableHairstyles.Count;
					for (int n = 0; n < list2.Count; n++)
					{
						SnapPoint snapPoint5 = list2[n];
						UILinkPoint andSet5 = this.GetAndSet(num3, snapPoint5);
						andSet5.Left = andSet5.ID - 1;
						if (snapPoint5.Id == 0)
						{
							andSet5.Left = -3;
						}
						andSet5.Right = andSet5.ID + 1;
						if (snapPoint5.Id == count - 1)
						{
							andSet5.Right = -4;
						}
						andSet5.Up = andSet5.ID - 10;
						if (n < 10)
						{
							andSet5.Up = num + 2 + n;
						}
						andSet5.Down = andSet5.ID + 10;
						if (snapPoint5.Id + 10 > snapPoint4.Id)
						{
							if (snapPoint5.Id % 10 < 5)
							{
								andSet5.Down = uilinkPoint.ID;
							}
							else
							{
								andSet5.Down = uilinkPoint2.ID;
							}
						}
						if (n == list2.Count - 1)
						{
							uilinkPoint.Up = andSet5.ID;
							uilinkPoint2.Up = andSet5.ID;
						}
						this._foundPoints.Add(num3);
						num3++;
					}
				}
				break;
			default:
			{
				List<SnapPoint> list5 = snapPoints.Where((SnapPoint a) => a.Name == "Low").ToList<SnapPoint>();
				list5.Sort(new Comparison<SnapPoint>(this.SortPoints));
				num3 = num2 + 20;
				for (int num9 = 0; num9 < list5.Count; num9++)
				{
					UILinkPoint andSet6 = this.GetAndSet(num3, list5[num9]);
					andSet6.Up = num2 + 2;
					andSet6.Down = uilinkPoint.ID;
					andSet6.Left = andSet6.ID - 1;
					andSet6.Right = andSet6.ID + 1;
					if (num9 == 0)
					{
						andSet6.Left = andSet6.ID + 2;
						uilinkPoint.Up = andSet6.ID;
					}
					if (num9 == list5.Count - 1)
					{
						andSet6.Right = andSet6.ID - 2;
						uilinkPoint2.Up = andSet6.ID;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				num3 = num2;
				for (int num10 = 0; num10 < list2.Count; num10++)
				{
					UILinkPoint andSet7 = this.GetAndSet(num3, list2[num10]);
					andSet7.Up = andSet7.ID - 1;
					andSet7.Down = andSet7.ID + 1;
					if (num10 == 0)
					{
						andSet7.Up = num + 2 + 5;
					}
					if (num10 == list2.Count - 1)
					{
						andSet7.Down = num2 + 20 + 2;
					}
					this._foundPoints.Add(num3);
					num3++;
				}
				break;
			}
			}
			if (PlayerInput.UsingGamepadUI && !this._foundPoints.Contains(UILinkPointNavigator.CurrentPoint))
			{
				this.MoveToVisuallyClosestPoint();
			}
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x00596684 File Offset: 0x00594884
		private void MoveToVisuallyClosestPoint()
		{
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uilinkPoint = null;
			foreach (int num in this._foundPoints)
			{
				UILinkPoint uilinkPoint2;
				if (!points.TryGetValue(num, out uilinkPoint2))
				{
					return;
				}
				if (uilinkPoint == null || Vector2.Distance(mouseScreen, uilinkPoint.Position) > Vector2.Distance(mouseScreen, uilinkPoint2.Position))
				{
					uilinkPoint = uilinkPoint2;
				}
			}
			if (uilinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uilinkPoint.ID);
			}
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x0059671C File Offset: 0x0059491C
		public void TryMovingCategory(int direction)
		{
			int num = (int)((this._selectedPicker + direction) % UICharacterCreation.CategoryId.Count);
			if (num < 0)
			{
				num += 10;
			}
			this.SelectColorPicker((UICharacterCreation.CategoryId)num);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x00596745 File Offset: 0x00594945
		private UILinkPoint GetAndSet(int ptid, SnapPoint snap)
		{
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[ptid];
			uilinkPoint.Unlink();
			UILinkPointNavigator.SetPosition(uilinkPoint.ID, snap.Position);
			return uilinkPoint;
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x00596769 File Offset: 0x00594969
		private bool PointWithName(SnapPoint a, string comp)
		{
			return a.Name == comp;
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x00596778 File Offset: 0x00594978
		private int SortPoints(SnapPoint a, SnapPoint b)
		{
			return a.Id.CompareTo(b.Id);
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x00596799 File Offset: 0x00594999
		private static Color ScaledHslToRgb(Vector3 hsl)
		{
			return UICharacterCreation.ScaledHslToRgb(hsl.X, hsl.Y, hsl.Z);
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x005967B2 File Offset: 0x005949B2
		private static Color ScaledHslToRgb(float hue, float saturation, float luminosity)
		{
			return Main.hslToRgb(hue, saturation, luminosity * 0.85f + 0.15f, byte.MaxValue);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x005967D0 File Offset: 0x005949D0
		private static Vector3 RgbToScaledHsl(Color color)
		{
			Vector3 vector = Main.rgbToHsl(color);
			vector.Z = (vector.Z - 0.15f) / 0.85f;
			vector = Vector3.Clamp(vector, Vector3.Zero, Vector3.One);
			return vector;
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x00596810 File Offset: 0x00594A10
		public void HandleBackButtonUsage()
		{
			if (this._selectedPicker != UICharacterCreation.CategoryId.CharInfo)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.UnselectAllCategories();
				this._selectedPicker = UICharacterCreation.CategoryId.CharInfo;
				this._middleContainer.Append(this._infoContainer);
				this._charInfoCategoryButton.SetSelected(true);
				return;
			}
			UICharacterCreation.GoBack();
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static UICharacterCreation()
		{
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0059686A File Offset: 0x00594A6A
		[CompilerGenerated]
		private float <CreateHSLSliderButtonBase>b__86_0()
		{
			return this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Saturation);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x00596873 File Offset: 0x00594A73
		[CompilerGenerated]
		private void <CreateHSLSliderButtonBase>b__86_1(float x)
		{
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Saturation, x);
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0059687D File Offset: 0x00594A7D
		[CompilerGenerated]
		private Color <CreateHSLSliderButtonBase>b__86_2(float x)
		{
			return this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Saturation, x);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x00596887 File Offset: 0x00594A87
		[CompilerGenerated]
		private float <CreateHSLSliderButtonBase>b__86_3()
		{
			return this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Luminance);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x00596890 File Offset: 0x00594A90
		[CompilerGenerated]
		private void <CreateHSLSliderButtonBase>b__86_4(float x)
		{
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Luminance, x);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0059689A File Offset: 0x00594A9A
		[CompilerGenerated]
		private Color <CreateHSLSliderButtonBase>b__86_5(float x)
		{
			return this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Luminance, x);
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x005968A4 File Offset: 0x00594AA4
		[CompilerGenerated]
		private float <CreateHSLSliderButtonBase>b__86_6()
		{
			return this.GetHSLSliderPosition(UICharacterCreation.HSLSliderId.Hue);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x005968AD File Offset: 0x00594AAD
		[CompilerGenerated]
		private void <CreateHSLSliderButtonBase>b__86_7(float x)
		{
			this.UpdateHSLValue(UICharacterCreation.HSLSliderId.Hue, x);
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x005968B7 File Offset: 0x00594AB7
		[CompilerGenerated]
		private Color <CreateHSLSliderButtonBase>b__86_8(float x)
		{
			return this.GetHSLSliderColorAt(UICharacterCreation.HSLSliderId.Hue, x);
		}

		// Token: 0x040053B0 RID: 21424
		private int[] _validClothStyles = new int[] { 0, 2, 1, 3, 8, 9, 7, 5, 6, 4 };

		// Token: 0x040053B1 RID: 21425
		private Dictionary<int, int> _defaultHairstylesForClothStyle = new Dictionary<int, int>
		{
			{ 0, 0 },
			{ 2, 1 },
			{ 1, 12 },
			{ 3, 2 },
			{ 8, 28 },
			{ 9, 68 },
			{ 7, 18 },
			{ 5, 22 },
			{ 6, 81 },
			{ 4, 5 }
		};

		// Token: 0x040053B2 RID: 21426
		private int[] _validVoiceStyles = new int[] { 1, 2, 3 };

		// Token: 0x040053B3 RID: 21427
		private readonly Player _player;

		// Token: 0x040053B4 RID: 21428
		private UIColoredImageButton[] _colorPickers;

		// Token: 0x040053B5 RID: 21429
		private UICharacterCreation.CategoryId _selectedPicker;

		// Token: 0x040053B6 RID: 21430
		private Vector3 _currentColorHSL;

		// Token: 0x040053B7 RID: 21431
		private UIColoredImageButton _clothingStylesCategoryButton;

		// Token: 0x040053B8 RID: 21432
		private UIColoredImageButton _hairStylesCategoryButton;

		// Token: 0x040053B9 RID: 21433
		private UIColoredImageButton _charInfoCategoryButton;

		// Token: 0x040053BA RID: 21434
		private UIElement _topContainer;

		// Token: 0x040053BB RID: 21435
		private UIElement _middleContainer;

		// Token: 0x040053BC RID: 21436
		private UIElement _hslContainer;

		// Token: 0x040053BD RID: 21437
		private UIElement _hairstylesContainer;

		// Token: 0x040053BE RID: 21438
		private UIElement _clothStylesContainer;

		// Token: 0x040053BF RID: 21439
		private UIElement _infoContainer;

		// Token: 0x040053C0 RID: 21440
		private UIText _hslHexText;

		// Token: 0x040053C1 RID: 21441
		private UIText _difficultyDescriptionText;

		// Token: 0x040053C2 RID: 21442
		private UIElement _copyHexButton;

		// Token: 0x040053C3 RID: 21443
		private UIElement _pasteHexButton;

		// Token: 0x040053C4 RID: 21444
		private UIElement _randomColorButton;

		// Token: 0x040053C5 RID: 21445
		private UIElement _copyTemplateButton;

		// Token: 0x040053C6 RID: 21446
		private UIElement _pasteTemplateButton;

		// Token: 0x040053C7 RID: 21447
		private UIElement _randomizePlayerButton;

		// Token: 0x040053C8 RID: 21448
		private UIElement _pitchSlider;

		// Token: 0x040053C9 RID: 21449
		private UIElement _voiceNext;

		// Token: 0x040053CA RID: 21450
		private UIElement _voicePrevious;

		// Token: 0x040053CB RID: 21451
		private UIElement _voicePlay;

		// Token: 0x040053CC RID: 21452
		private float _pitchAmount;

		// Token: 0x040053CD RID: 21453
		private UIElement[] _previewArmorButton = new UIElement[0];

		// Token: 0x040053CE RID: 21454
		private UICharacterNameButton _charName;

		// Token: 0x040053CF RID: 21455
		private UIText _helpGlyphLeft;

		// Token: 0x040053D0 RID: 21456
		private UIText _helpGlyphRight;

		// Token: 0x040053D1 RID: 21457
		private bool _oldMaleForVoiceAutoSwitch = true;

		// Token: 0x040053D2 RID: 21458
		private int? _lastSelectedHairstyle;

		// Token: 0x040053D3 RID: 21459
		private UIImageFramed[] _characterPreviewLayers = new UIImageFramed[7];

		// Token: 0x040053D4 RID: 21460
		public const int MAX_NAME_LENGTH = 20;

		// Token: 0x040053D5 RID: 21461
		private bool _playedVoicePreviewThisFrame;

		// Token: 0x040053D6 RID: 21462
		private UICharacterCreation.ArmorAssignments _maleArmor;

		// Token: 0x040053D7 RID: 21463
		private UICharacterCreation.ArmorAssignments _femaleArmor;

		// Token: 0x040053D8 RID: 21464
		private GameTipsDisplay _tips;

		// Token: 0x040053D9 RID: 21465
		public static UIState BackupConfirmationState;

		// Token: 0x040053DA RID: 21466
		private static bool dirty;

		// Token: 0x040053DB RID: 21467
		private string initialState;

		// Token: 0x040053DC RID: 21468
		private bool _pitchChanged;

		// Token: 0x040053DD RID: 21469
		private int _pitchChangedCooldown;

		// Token: 0x040053DE RID: 21470
		private UIGamepadHelper _helper;

		// Token: 0x040053DF RID: 21471
		private List<int> _foundPoints = new List<int>();

		// Token: 0x0200090C RID: 2316
		private enum CategoryId
		{
			// Token: 0x04007444 RID: 29764
			CharInfo,
			// Token: 0x04007445 RID: 29765
			Clothing,
			// Token: 0x04007446 RID: 29766
			HairStyle,
			// Token: 0x04007447 RID: 29767
			HairColor,
			// Token: 0x04007448 RID: 29768
			Eye,
			// Token: 0x04007449 RID: 29769
			Skin,
			// Token: 0x0400744A RID: 29770
			Shirt,
			// Token: 0x0400744B RID: 29771
			Undershirt,
			// Token: 0x0400744C RID: 29772
			Pants,
			// Token: 0x0400744D RID: 29773
			Shoes,
			// Token: 0x0400744E RID: 29774
			Count
		}

		// Token: 0x0200090D RID: 2317
		private enum HSLSliderId
		{
			// Token: 0x04007450 RID: 29776
			Hue,
			// Token: 0x04007451 RID: 29777
			Saturation,
			// Token: 0x04007452 RID: 29778
			Luminance
		}

		// Token: 0x0200090E RID: 2318
		private struct ArmorAssignments
		{
			// Token: 0x04007453 RID: 29779
			public int HeadItem;

			// Token: 0x04007454 RID: 29780
			public int BodyItem;

			// Token: 0x04007455 RID: 29781
			public int LegItem;

			// Token: 0x04007456 RID: 29782
			public int Accessory1Item;
		}

		// Token: 0x0200090F RID: 2319
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600475A RID: 18266 RVA: 0x006CB4DD File Offset: 0x006C96DD
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600475B RID: 18267 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600475C RID: 18268 RVA: 0x006CB4E9 File Offset: 0x006C96E9
			internal bool <SetupGamepadPoints>b__135_0(SnapPoint a)
			{
				return a.Name == "Back";
			}

			// Token: 0x0600475D RID: 18269 RVA: 0x006CB4FB File Offset: 0x006C96FB
			internal bool <SetupGamepadPoints>b__135_1(SnapPoint a)
			{
				return a.Name == "Create";
			}

			// Token: 0x0600475E RID: 18270 RVA: 0x006CB50D File Offset: 0x006C970D
			internal bool <SetupGamepadPoints>b__135_2(SnapPoint a)
			{
				return a.Name == "Top";
			}

			// Token: 0x0600475F RID: 18271 RVA: 0x006CB51F File Offset: 0x006C971F
			internal bool <SetupGamepadPoints>b__135_3(SnapPoint a)
			{
				return a.Name == "Middle";
			}

			// Token: 0x06004760 RID: 18272 RVA: 0x006CB531 File Offset: 0x006C9731
			internal bool <SetupGamepadPoints>b__135_4(SnapPoint a)
			{
				return a.Name == "Low";
			}

			// Token: 0x06004761 RID: 18273 RVA: 0x006CB543 File Offset: 0x006C9743
			internal bool <SetupGamepadPoints>b__135_5(SnapPoint a)
			{
				return a.Name == "Preview";
			}

			// Token: 0x06004762 RID: 18274 RVA: 0x006CB531 File Offset: 0x006C9731
			internal bool <SetupGamepadPoints>b__135_6(SnapPoint a)
			{
				return a.Name == "Low";
			}

			// Token: 0x04007457 RID: 29783
			public static readonly UICharacterCreation.<>c <>9 = new UICharacterCreation.<>c();

			// Token: 0x04007458 RID: 29784
			public static Func<SnapPoint, bool> <>9__135_0;

			// Token: 0x04007459 RID: 29785
			public static Func<SnapPoint, bool> <>9__135_1;

			// Token: 0x0400745A RID: 29786
			public static Func<SnapPoint, bool> <>9__135_2;

			// Token: 0x0400745B RID: 29787
			public static Func<SnapPoint, bool> <>9__135_3;

			// Token: 0x0400745C RID: 29788
			public static Func<SnapPoint, bool> <>9__135_4;

			// Token: 0x0400745D RID: 29789
			public static Func<SnapPoint, bool> <>9__135_5;

			// Token: 0x0400745E RID: 29790
			public static Func<SnapPoint, bool> <>9__135_6;
		}
	}
}
