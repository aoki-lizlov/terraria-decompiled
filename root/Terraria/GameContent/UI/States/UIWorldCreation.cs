using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics;
using Terraria.Graphics.Renderers;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Testing;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003AD RID: 941
	public class UIWorldCreation : UIState, IHaveBackButtonCommand
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x0058E91D File Offset: 0x0058CB1D
		// (set) Token: 0x06002B86 RID: 11142 RVA: 0x0058E924 File Offset: 0x0058CB24
		private UIWorldCreation.WorldSizeId _optionSize
		{
			get
			{
				return (UIWorldCreation.WorldSizeId)WorldGen.GetWorldSize();
			}
			set
			{
				WorldGen.SetWorldSize((int)value);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x0058E92C File Offset: 0x0058CB2C
		// (set) Token: 0x06002B88 RID: 11144 RVA: 0x0058E933 File Offset: 0x0058CB33
		private UIWorldCreation.WorldDifficultyId _optionDifficulty
		{
			get
			{
				return (UIWorldCreation.WorldDifficultyId)Main.GameMode;
			}
			set
			{
				Main.GameMode = (int)value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x0058E93B File Offset: 0x0058CB3B
		// (set) Token: 0x06002B8A RID: 11146 RVA: 0x0058E944 File Offset: 0x0058CB44
		private UIWorldCreation.WorldEvilId _optionEvil
		{
			get
			{
				return WorldGen.WorldGenParam_Evil + UIWorldCreation.WorldEvilId.Corruption;
			}
			set
			{
				WorldGen.WorldGenParam_Evil = value - UIWorldCreation.WorldEvilId.Corruption;
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x0058E950 File Offset: 0x0058CB50
		public UIWorldCreation()
		{
			this._goBackTarget = this;
			this.BuildPage();
			this.SeedDust.Clear();
			this.SeedParticleSystem.Clear();
			this.ResetSpecialSeedRing();
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x0058E9FE File Offset: 0x0058CBFE
		public void SetGoBackTarget(UIState state)
		{
			this._goBackTarget = state;
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x0058EA08 File Offset: 0x0058CC08
		private void BuildPage()
		{
			int num = 18;
			base.RemoveAllChildren();
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixels(500f),
				Height = StyleDimension.FromPixels(434f + (float)num),
				Top = StyleDimension.FromPixels(170f - (float)num),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uielement.SetPadding(0f);
			base.Append(uielement);
			UIPanel uipanel = new UIPanel
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixels((float)(280 + num)),
				Top = StyleDimension.FromPixels(50f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f
			};
			uipanel.SetPadding(0f);
			uielement.Append(uipanel);
			this.MakeBackAndCreatebuttons(uielement);
			UIElement uielement2 = new UIElement
			{
				Top = StyleDimension.FromPixelsAndPercent(0f, 0f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 1f
			};
			uielement2.SetPadding(0f);
			uielement2.PaddingTop = 8f;
			uielement2.PaddingBottom = 12f;
			uipanel.Append(uielement2);
			this.MakeInfoMenu(uielement2);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x0058EB6E File Offset: 0x0058CD6E
		private void PreparePreviouslyUnlockedSecretSeeds()
		{
			SecretSeedsTracker.PrepareInterface();
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x0058EB78 File Offset: 0x0058CD78
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
			float num = 0f;
			float num2 = 88f;
			float num3 = 44f;
			float num4 = num2 + num3;
			float num5 = num3;
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, Language.GetText("UI.WorldCreationRandomizeNameDescription"), Color.White, "Images/UI/WorldCreation/IconRandomName", 1f, 0.5f, 10f)
			{
				Width = StyleDimension.FromPixelsAndPercent(40f, 0f),
				Height = new StyleDimension(40f, 0f),
				HAlign = 0f,
				Top = StyleDimension.FromPixelsAndPercent(num, 0f),
				ShowHighlightWhenSelected = false
			};
			groupOptionButton.OnLeftMouseDown += this.ClickRandomizeName;
			groupOptionButton.OnMouseOver += this.ShowOptionDescription;
			groupOptionButton.OnMouseOut += this.ClearOptionDescription;
			groupOptionButton.SetSnapPoint("RandomizeName", 0, null, null);
			uielement.Append(groupOptionButton);
			UICharacterNameButton uicharacterNameButton = new UICharacterNameButton(Language.GetText("UI.WorldCreationName"), Language.GetText("UI.WorldCreationNameEmpty"), Language.GetText("UI.WorldDescriptionName"))
			{
				Width = StyleDimension.FromPixelsAndPercent(-num4, 1f),
				HAlign = 0f,
				Left = new StyleDimension(num5, 0f),
				Top = StyleDimension.FromPixelsAndPercent(num, 0f)
			};
			uicharacterNameButton.OnLeftMouseDown += this.Click_SetName;
			uicharacterNameButton.OnMouseOver += this.ShowOptionDescription;
			uicharacterNameButton.OnMouseOut += this.ClearOptionDescription;
			uicharacterNameButton.SetSnapPoint("Name", 0, null, null);
			uielement.Append(uicharacterNameButton);
			this._namePlate = uicharacterNameButton;
			CalculatedStyle calculatedStyle = uicharacterNameButton.GetDimensions();
			num += calculatedStyle.Height + 4f;
			this._advancedSeedButton = new GroupOptionButton<bool>(true, null, Language.GetText("UI.WorldCreationSeedMenuDescription"), Color.White, "Images/UI/WorldCreation/IconRandomSeed", 1f, 0.5f, 10f)
			{
				Width = StyleDimension.FromPixelsAndPercent(40f, 0f),
				Height = new StyleDimension(40f, 0f),
				HAlign = 0f,
				Top = StyleDimension.FromPixelsAndPercent(num, 0f),
				ShowHighlightWhenSelected = false
			};
			this._advancedSeedButton.OnLeftMouseDown += this.ClickAdvancedSeedMenu;
			this._advancedSeedButton.OnMouseOver += this.ShowOptionDescription;
			this._advancedSeedButton.OnMouseOut += this.ClearOptionDescription;
			this._advancedSeedButton.SetSnapPoint("RandomizeSeed", 0, null, null);
			this._advancedSeedButton.OnDraw += this.DrawSpecialSeedRingCallback;
			uielement.Append(this._advancedSeedButton);
			UICharacterNameButton uicharacterNameButton2 = new UICharacterNameButton(Language.GetText("UI.WorldCreationSeed"), Language.GetText("UI.WorldCreationSeedEmpty"), Language.GetText("UI.WorldDescriptionSeed"))
			{
				Width = StyleDimension.FromPixelsAndPercent(-num4, 1f),
				HAlign = 0f,
				Left = new StyleDimension(num5, 0f),
				Top = StyleDimension.FromPixelsAndPercent(num, 0f),
				DistanceFromTitleToOption = 29f
			};
			uicharacterNameButton2.OnLeftMouseDown += this.Click_SetSeed;
			uicharacterNameButton2.OnMouseOver += this.ShowOptionDescription;
			uicharacterNameButton2.OnMouseOut += this.ClearOptionDescription;
			uicharacterNameButton2.SetSnapPoint("Seed", 0, null, null);
			uielement.Append(uicharacterNameButton2);
			this._seedPlate = uicharacterNameButton2;
			UIWorldCreationPreview uiworldCreationPreview = new UIWorldCreationPreview
			{
				Width = StyleDimension.FromPixels(84f),
				Height = StyleDimension.FromPixels(84f),
				HAlign = 1f,
				VAlign = 0f
			};
			uielement.Append(uiworldCreationPreview);
			this._previewPlate = uiworldCreationPreview;
			calculatedStyle = uicharacterNameButton2.GetDimensions();
			num += calculatedStyle.Height + 10f;
			UIWorldCreation.AddHorizontalSeparator(uielement, num + 2f);
			float num6 = 1f;
			this.AddWorldSizeOptions(uielement, num, new UIElement.MouseEvent(this.ClickSizeOption), "size", num6);
			num += 48f;
			UIWorldCreation.AddHorizontalSeparator(uielement, num);
			this.AddWorldDifficultyOptions(uielement, num, new UIElement.MouseEvent(this.ClickDifficultyOption), "difficulty", num6);
			num += 48f;
			UIWorldCreation.AddHorizontalSeparator(uielement, num);
			this.AddWorldEvilOptions(uielement, num, new UIElement.MouseEvent(this.ClickEvilOption), "evil", num6);
			num += 48f;
			UIWorldCreation.AddHorizontalSeparator(uielement, num);
			this.AddDescriptionPanel(uielement, num, "desc");
			this.SetDefaultOptions();
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x0058F0BC File Offset: 0x0058D2BC
		private static void AddHorizontalSeparator(UIElement Container, float accumualtedHeight)
		{
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPercent(1f),
				Top = StyleDimension.FromPixels(accumualtedHeight - 8f),
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			Container.Append(uihorizontalSeparator);
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x0058F12C File Offset: 0x0058D32C
		private void SetDefaultOptions()
		{
			Main.ActiveWorldFileData = new WorldFileData();
			this.AssignRandomWorldName();
			this.ClearSeed();
			this._optionSize = UIWorldCreation.WorldSizeId.Medium;
			if (Main.ActivePlayerFileData.Player.difficulty == 3)
			{
				this._optionDifficulty = UIWorldCreation.WorldDifficultyId.Creative;
			}
			this._optionEvil = UIWorldCreation.WorldEvilId.Random;
			this.UpdateSliders();
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x0058F184 File Offset: 0x0058D384
		private void AddDescriptionPanel(UIElement container, float accumulatedHeight, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(40f, 0f),
				Top = StyleDimension.FromPixels(2f)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			container.Append(uislicedImage);
			UIText uitext = new UIText(Language.GetText("UI.WorldDescriptionDefault"), 0.82f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 6f;
			uislicedImage.Append(uitext);
			this._descriptionText = uitext;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x0058F2CC File Offset: 0x0058D4CC
		private void AddWorldSizeOptions(UIElement container, float accumualtedHeight, UIElement.MouseEvent clickEvent, string tagGroup, float usableWidthPercent)
		{
			UIWorldCreation.WorldSizeId[] array = new UIWorldCreation.WorldSizeId[]
			{
				UIWorldCreation.WorldSizeId.Small,
				UIWorldCreation.WorldSizeId.Medium,
				UIWorldCreation.WorldSizeId.Large
			};
			LocalizedText[] array2 = new LocalizedText[]
			{
				Lang.menu[92],
				Lang.menu[93],
				Lang.menu[94]
			};
			LocalizedText[] array3 = new LocalizedText[]
			{
				Language.GetText("UI.WorldDescriptionSizeSmall"),
				Language.GetText("UI.WorldDescriptionSizeMedium"),
				Language.GetText("UI.WorldDescriptionSizeLarge")
			};
			Color[] array4 = new Color[]
			{
				Color.Cyan,
				Color.Lerp(Color.Cyan, Color.LimeGreen, 0.5f),
				Color.LimeGreen
			};
			string[] array5 = new string[] { "Images/UI/WorldCreation/IconSizeSmall", "Images/UI/WorldCreation/IconSizeMedium", "Images/UI/WorldCreation/IconSizeLarge" };
			GroupOptionButton<UIWorldCreation.WorldSizeId>[] array6 = new GroupOptionButton<UIWorldCreation.WorldSizeId>[array.Length];
			for (int i = 0; i < array6.Length; i++)
			{
				GroupOptionButton<UIWorldCreation.WorldSizeId> groupOptionButton = new GroupOptionButton<UIWorldCreation.WorldSizeId>(array[i], array2[i], array3[i], array4[i], array5[i], 1f, 1f, 16f);
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-4 * (array6.Length - 1)), 1f / (float)array6.Length * usableWidthPercent);
				groupOptionButton.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
				groupOptionButton.HAlign = (float)i / (float)(array6.Length - 1);
				groupOptionButton.Top.Set(accumualtedHeight, 0f);
				groupOptionButton.OnLeftMouseDown += clickEvent;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				container.Append(groupOptionButton);
				array6[i] = groupOptionButton;
			}
			this._sizeButtons = array6;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x0058F4AC File Offset: 0x0058D6AC
		private void AddWorldDifficultyOptions(UIElement container, float accumualtedHeight, UIElement.MouseEvent clickEvent, string tagGroup, float usableWidthPercent)
		{
			UIWorldCreation.WorldDifficultyId[] array = new UIWorldCreation.WorldDifficultyId[]
			{
				UIWorldCreation.WorldDifficultyId.Creative,
				UIWorldCreation.WorldDifficultyId.Normal,
				UIWorldCreation.WorldDifficultyId.Expert,
				UIWorldCreation.WorldDifficultyId.Master
			};
			LocalizedText[] array2 = new LocalizedText[]
			{
				Language.GetText("UI.Creative"),
				Language.GetText("UI.Normal"),
				Language.GetText("UI.Expert"),
				Language.GetText("UI.Master")
			};
			LocalizedText[] array3 = new LocalizedText[]
			{
				Language.GetText("UI.WorldDescriptionCreative"),
				Language.GetText("UI.WorldDescriptionNormal"),
				Language.GetText("UI.WorldDescriptionExpert"),
				Language.GetText("UI.WorldDescriptionMaster")
			};
			Color[] array4 = new Color[]
			{
				Main.creativeModeColor,
				Color.White,
				Main.mcColor,
				Main.hcColor
			};
			string[] array5 = new string[] { "Images/UI/WorldCreation/IconDifficultyCreative", "Images/UI/WorldCreation/IconDifficultyNormal", "Images/UI/WorldCreation/IconDifficultyExpert", "Images/UI/WorldCreation/IconDifficultyMaster" };
			GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] array6 = new GroupOptionButton<UIWorldCreation.WorldDifficultyId>[array.Length];
			for (int i = 0; i < array6.Length; i++)
			{
				GroupOptionButton<UIWorldCreation.WorldDifficultyId> groupOptionButton = new GroupOptionButton<UIWorldCreation.WorldDifficultyId>(array[i], array2[i], array3[i], array4[i], array5[i], 1f, 1f, 16f);
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-1 * (array6.Length - 1)), 1f / (float)array6.Length * usableWidthPercent);
				groupOptionButton.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
				groupOptionButton.HAlign = (float)i / (float)(array6.Length - 1);
				groupOptionButton.Top.Set(accumualtedHeight, 0f);
				groupOptionButton.OnLeftMouseDown += clickEvent;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				container.Append(groupOptionButton);
				array6[i] = groupOptionButton;
			}
			this._difficultyButtons = array6;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x0058F6B4 File Offset: 0x0058D8B4
		private void AddWorldEvilOptions(UIElement container, float accumualtedHeight, UIElement.MouseEvent clickEvent, string tagGroup, float usableWidthPercent)
		{
			UIWorldCreation.WorldEvilId[] array = new UIWorldCreation.WorldEvilId[]
			{
				UIWorldCreation.WorldEvilId.Random,
				UIWorldCreation.WorldEvilId.Corruption,
				UIWorldCreation.WorldEvilId.Crimson
			};
			LocalizedText[] array2 = new LocalizedText[]
			{
				Lang.misc[103],
				Lang.misc[101],
				Lang.misc[102]
			};
			LocalizedText[] array3 = new LocalizedText[]
			{
				Language.GetText("UI.WorldDescriptionEvilRandom"),
				Language.GetText("UI.WorldDescriptionEvilCorrupt"),
				Language.GetText("UI.WorldDescriptionEvilCrimson")
			};
			Color[] array4 = new Color[]
			{
				Color.White,
				Color.MediumPurple,
				Color.IndianRed
			};
			string[] array5 = new string[] { "Images/UI/WorldCreation/IconEvilRandom", "Images/UI/WorldCreation/IconEvilCorruption", "Images/UI/WorldCreation/IconEvilCrimson" };
			GroupOptionButton<UIWorldCreation.WorldEvilId>[] array6 = new GroupOptionButton<UIWorldCreation.WorldEvilId>[array.Length];
			for (int i = 0; i < array6.Length; i++)
			{
				GroupOptionButton<UIWorldCreation.WorldEvilId> groupOptionButton = new GroupOptionButton<UIWorldCreation.WorldEvilId>(array[i], array2[i], array3[i], array4[i], array5[i], 1f, 1f, 16f);
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-4 * (array6.Length - 1)), 1f / (float)array6.Length * usableWidthPercent);
				groupOptionButton.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
				groupOptionButton.HAlign = (float)i / (float)(array6.Length - 1);
				groupOptionButton.Top.Set(accumualtedHeight, 0f);
				groupOptionButton.OnLeftMouseDown += clickEvent;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				container.Append(groupOptionButton);
				array6[i] = groupOptionButton;
			}
			this._evilButtons = array6;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x0058F883 File Offset: 0x0058DA83
		private void ClickRandomizeName(UIMouseEvent evt, UIElement listeningElement)
		{
			this.AssignRandomWorldName();
			this.UpdateInputFields();
			this.UpdateSliders();
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x0058F8A0 File Offset: 0x0058DAA0
		private void ClickAdvancedSeedMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			this.ResetSpecialSeedRing();
			UIWorldCreationAdvanced uiworldCreationAdvanced = new UIWorldCreationAdvanced(this, false);
			this.SetGoBackTarget(uiworldCreationAdvanced);
			Main.MenuUI.SetState(uiworldCreationAdvanced);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x0058F8CD File Offset: 0x0058DACD
		public void ClearSeedText()
		{
			this._optionSeed = "";
			this._isSpecialSeedText = false;
			this.UpdateInputFields();
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x0058F8E7 File Offset: 0x0058DAE7
		public void ClearSeed()
		{
			this._optionSeed = string.Empty;
			this._isSpecialSeedText = false;
			this._secretSeedTextsEntered.Clear();
			this._disabledSecretSeedTextsEntered.Clear();
			WorldGenerationOptions.Reset();
			WorldGen.SecretSeed.ClearAllSeeds();
			this.PreparePreviouslyUnlockedSecretSeeds();
			this.UpdateInputFields();
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x0058F928 File Offset: 0x0058DB28
		public void RandomizeSeed()
		{
			this._optionSeed = Main.rand.Next().ToString();
			this._isSpecialSeedText = false;
			this.UpdateInputFields();
			this.UpdateSliders();
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0058F968 File Offset: 0x0058DB68
		private void ClickSizeOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<UIWorldCreation.WorldSizeId> groupOptionButton = (GroupOptionButton<UIWorldCreation.WorldSizeId>)listeningElement;
			this._optionSize = groupOptionButton.OptionValue;
			GroupOptionButton<UIWorldCreation.WorldSizeId>[] sizeButtons = this._sizeButtons;
			for (int i = 0; i < sizeButtons.Length; i++)
			{
				sizeButtons[i].SetCurrentOption(groupOptionButton.OptionValue);
			}
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x0058F9B4 File Offset: 0x0058DBB4
		private void ClickDifficultyOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<UIWorldCreation.WorldDifficultyId> groupOptionButton = (GroupOptionButton<UIWorldCreation.WorldDifficultyId>)listeningElement;
			this._optionDifficulty = groupOptionButton.OptionValue;
			GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] difficultyButtons = this._difficultyButtons;
			for (int i = 0; i < difficultyButtons.Length; i++)
			{
				difficultyButtons[i].SetCurrentOption(groupOptionButton.OptionValue);
			}
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x0058FA00 File Offset: 0x0058DC00
		private void ClickEvilOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<UIWorldCreation.WorldEvilId> groupOptionButton = (GroupOptionButton<UIWorldCreation.WorldEvilId>)listeningElement;
			this._optionEvil = groupOptionButton.OptionValue;
			GroupOptionButton<UIWorldCreation.WorldEvilId>[] evilButtons = this._evilButtons;
			for (int i = 0; i < evilButtons.Length; i++)
			{
				evilButtons[i].SetCurrentOption(groupOptionButton.OptionValue);
			}
			this.UpdatePreviewPlate();
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x0058FA49 File Offset: 0x0058DC49
		private void UpdatePreviewPlate()
		{
			this._previewPlate.UpdateOption((byte)this._optionDifficulty, (byte)this._optionEvil, (byte)this._optionSize);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x0058FA6C File Offset: 0x0058DC6C
		private void UpdateSliders()
		{
			GroupOptionButton<UIWorldCreation.WorldSizeId>[] sizeButtons = this._sizeButtons;
			for (int i = 0; i < sizeButtons.Length; i++)
			{
				sizeButtons[i].SetCurrentOption(this._optionSize);
			}
			GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] difficultyButtons = this._difficultyButtons;
			for (int i = 0; i < difficultyButtons.Length; i++)
			{
				difficultyButtons[i].SetCurrentOption(this._optionDifficulty);
			}
			GroupOptionButton<UIWorldCreation.WorldEvilId>[] evilButtons = this._evilButtons;
			for (int i = 0; i < evilButtons.Length; i++)
			{
				evilButtons[i].SetCurrentOption(this._optionEvil);
			}
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0058FAE4 File Offset: 0x0058DCE4
		public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = null;
			GroupOptionButton<UIWorldCreation.WorldSizeId> groupOptionButton = listeningElement as GroupOptionButton<UIWorldCreation.WorldSizeId>;
			if (groupOptionButton != null)
			{
				localizedText = groupOptionButton.Description;
			}
			GroupOptionButton<UIWorldCreation.WorldDifficultyId> groupOptionButton2 = listeningElement as GroupOptionButton<UIWorldCreation.WorldDifficultyId>;
			if (groupOptionButton2 != null)
			{
				localizedText = groupOptionButton2.Description;
			}
			GroupOptionButton<UIWorldCreation.WorldEvilId> groupOptionButton3 = listeningElement as GroupOptionButton<UIWorldCreation.WorldEvilId>;
			if (groupOptionButton3 != null)
			{
				localizedText = groupOptionButton3.Description;
			}
			UICharacterNameButton uicharacterNameButton = listeningElement as UICharacterNameButton;
			if (uicharacterNameButton != null)
			{
				localizedText = uicharacterNameButton.Description;
			}
			GroupOptionButton<bool> groupOptionButton4 = listeningElement as GroupOptionButton<bool>;
			if (groupOptionButton4 != null)
			{
				localizedText = groupOptionButton4.Description;
			}
			if (localizedText != null)
			{
				this._descriptionText.SetText(localizedText);
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0058FB5D File Offset: 0x0058DD5D
		public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this._descriptionText.SetText(Language.GetText("UI.WorldDescriptionDefault"));
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x0058FB74 File Offset: 0x0058DD74
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

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0058FCF9 File Offset: 0x0058DEF9
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			UIWorldCreation.GoBack();
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x0058FD00 File Offset: 0x0058DF00
		private static void GoBack()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.OpenWorldSelectUI();
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0058FD1C File Offset: 0x0058DF1C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0058539D File Offset: 0x0058359D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0058FD74 File Offset: 0x0058DF74
		private void Click_SetName(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true, 27);
			Main.MenuUI.SetState(uivirtualKeyboard);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x0058FDDA File Offset: 0x0058DFDA
		private void Click_SetSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			this.OpenSeedInputMenu();
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x0058FDE4 File Offset: 0x0058DFE4
		public void OpenSeedInputMenu()
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Language.GetTextValue("UI.EnterSeed"), "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingSeed), new Action(this.GoBackHere), 0, true, int.MaxValue);
			Main.MenuUI.SetState(uivirtualKeyboard);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0058FE4C File Offset: 0x0058E04C
		private void Click_NamingAndCreating(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			if (string.IsNullOrEmpty(this._optionwWorldName))
			{
				this._optionwWorldName = "";
				Main.clrInput();
				UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedNamingAndCreating), new Action(this.GoBackHere), 0, false, 27);
				Main.MenuUI.SetState(uivirtualKeyboard);
				return;
			}
			this.FinishCreatingWorld();
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0058FED1 File Offset: 0x0058E0D1
		private void OnFinishedSettingName(string name)
		{
			this._optionwWorldName = name.Trim();
			this.UpdateInputFields();
			this.GoBackHere();
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x0058FEEC File Offset: 0x0058E0EC
		private void UpdateInputFields()
		{
			this._namePlate.SetContents(this._optionwWorldName);
			this._namePlate.Recalculate();
			this._namePlate.TrimDisplayIfOverElementDimensions(27);
			this._namePlate.Recalculate();
			this.FillSeedContent(this._seedPlate);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x0058FF39 File Offset: 0x0058E139
		public void FillSeedContent(UICharacterNameButton button)
		{
			button.SetContents(this._optionSeed);
			button.Recalculate();
			button.TrimDisplayIfOverElementDimensions(WorldFileData.MAX_USER_SEED_TEXT_LENGTH);
			button.Recalculate();
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x0058FF5E File Offset: 0x0058E15E
		public void ToggleSeedOption(AWorldGenerationOption seedOption)
		{
			if (this._isSpecialSeedText)
			{
				this._optionSeed = string.Empty;
				this._isSpecialSeedText = false;
				this.UpdateInputFields();
				this.UpdateSliders();
				this.UpdatePreviewPlate();
			}
			seedOption.Enabled = !seedOption.Enabled;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x0058FF9B File Offset: 0x0058E19B
		public bool HasEnteredSpecialSeed
		{
			get
			{
				return this._secretSeedTextsEntered.Count > 0;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x0058FFAB File Offset: 0x0058E1AB
		public bool HasDisabledSecretSeed
		{
			get
			{
				return this._disabledSecretSeedTextsEntered.Count > 0;
			}
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x0058FFBC File Offset: 0x0058E1BC
		public void EnableSecretSeedOptions(bool enabled)
		{
			if (enabled)
			{
				for (int i = 0; i < this._disabledSecretSeedTextsEntered.Count; i++)
				{
					WorldGen.SecretSeed secretSeed;
					if (WorldGen.SecretSeed.CheckInputForSecretSeed(this._disabledSecretSeedTextsEntered[i], out secretSeed) && !secretSeed.Enabled)
					{
						this._secretSeedTextsEntered.Add(this._disabledSecretSeedTextsEntered[i]);
						WorldGen.SecretSeed.Enable(secretSeed, false);
					}
				}
				this._disabledSecretSeedTextsEntered.Clear();
				return;
			}
			this._disabledSecretSeedTextsEntered.Clear();
			this._disabledSecretSeedTextsEntered.AddRange(this._secretSeedTextsEntered);
			WorldGen.SecretSeed.ClearAllSeeds();
			this._secretSeedTextsEntered.Clear();
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x00590058 File Offset: 0x0058E258
		public string GetJoinedSecretSeedString(DynamicSpriteFont font, float maxWidth, float maxHeight)
		{
			float num = 0f;
			string text = string.Empty;
			List<string> list = (this.HasEnteredSpecialSeed ? this._secretSeedTextsEntered : this._disabledSecretSeedTextsEntered);
			if (list.Count == 0)
			{
				return "-";
			}
			string text2 = list[0];
			for (int i = 1; i < list.Count; i++)
			{
				string text3 = string.Format("{0}|{1}", text2, list[i]);
				if (font.MeasureString(text3).X >= maxWidth)
				{
					if (num <= maxHeight)
					{
						text = text + text2 + "\n";
					}
					num += (float)font.LineSpacing;
					text3 = list[i];
				}
				text2 = text3;
			}
			if (num <= maxHeight)
			{
				text += text2;
			}
			return text;
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x00590110 File Offset: 0x0058E310
		private void OnFinishedSettingSeed(string seed)
		{
			this._optionSeed = seed.Trim();
			string text;
			string text2;
			List<string> list;
			if (WorldFileData.TryApplyingCopiedSeed(this._optionSeed, true, out text, out text2, out list))
			{
				this._optionSeed = text;
				this._secretSeedTextsEntered = list;
				this._disabledSecretSeedTextsEntered.Clear();
			}
			else
			{
				this._optionSeed = Utils.TrimUserString(this._optionSeed, WorldFileData.MAX_USER_SEED_TEXT_LENGTH);
				AWorldGenerationOption optionFromSeedText = WorldGenerationOptions.GetOptionFromSeedText(this._optionSeed);
				this._isSpecialSeedText = optionFromSeedText != null;
				if (this._isSpecialSeedText)
				{
					WorldGenerationOptions.SelectOption(optionFromSeedText);
					SoundEngine.PlaySound(24, -1, -1, 1, 1f, 0f);
				}
				WorldGen.SecretSeed secretSeed;
				if (WorldGen.SecretSeed.CheckInputForSecretSeed(this._optionSeed, out secretSeed))
				{
					if (!secretSeed.Enabled)
					{
						this._secretSeedTextsEntered.Add(this._optionSeed);
						WorldGen.SecretSeed.Enable(secretSeed, true);
						this.EnableSecretSeedOptions(true);
						CalculatedStyle calculatedStyle = this._advancedSeedButton.GetDimensions();
						if (this._goBackTarget != this)
						{
							UIWorldCreationAdvanced uiworldCreationAdvanced = this._goBackTarget as UIWorldCreationAdvanced;
							if (uiworldCreationAdvanced != null)
							{
								uiworldCreationAdvanced.RefreshSecretSeedButton();
								calculatedStyle = uiworldCreationAdvanced.GetSecretSeedButton().GetDimensions();
								uiworldCreationAdvanced.GetSecretSeedButton().SetCurrentOption(this.HasEnteredSpecialSeed);
							}
						}
						Vector2 vector = calculatedStyle.Center();
						Vector2 vector2 = Main.rand.NextVector2Circular(5f, 5f);
						this.Spawn_RainbowRodHit(new ParticleOrchestraSettings
						{
							PositionInWorld = vector,
							MovementVector = new Vector2(16f, 0f) + vector2
						});
						if (this._goBackTarget != this)
						{
							this.Spawn_RainbowRodHit(new ParticleOrchestraSettings
							{
								PositionInWorld = vector,
								MovementVector = new Vector2(16f, 0f) - vector2
							});
						}
						Vector2 vector3 = Main.rand.NextVector2Circular(5f, 5f);
						this.Spawn_RainbowRodHit(new ParticleOrchestraSettings
						{
							PositionInWorld = vector,
							MovementVector = new Vector2(0f, 16f) + vector3
						});
						if (this._goBackTarget != this)
						{
							this.Spawn_RainbowRodHit(new ParticleOrchestraSettings
							{
								PositionInWorld = vector,
								MovementVector = new Vector2(0f, 16f) - vector3
							});
						}
						for (int i = 0; i < 3; i++)
						{
							this.Spawn_BestReforge(new ParticleOrchestraSettings
							{
								PositionInWorld = vector + new Vector2(calculatedStyle.Width * 0.25f * (float)(i - 1), 0f)
							});
						}
					}
					this.ClearSeedText();
				}
			}
			this.UpdateInputFields();
			this.UpdateSliders();
			this.UpdatePreviewPlate();
			if (this.SubmitSeed != null)
			{
				this.SubmitSeed();
			}
			this.GoBackHere();
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x005903D4 File Offset: 0x0058E5D4
		private void Spawn_BestReforge(ParticleOrchestraSettings settings)
		{
			Vector2 vector = new Vector2(0f, 0.16350001f);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Spark", 1);
			for (int i = 0; i < 8; i++)
			{
				Vector2 vector2 = Main.rand.NextVector2Circular(3f, 4f);
				this.SeedParticleSystem.Add(new CreativeSacrificeParticle(asset, null, settings.MovementVector + vector2, settings.PositionInWorld)
				{
					AccelerationPerFrame = vector,
					ScaleOffsetPerFrame = -0.016666668f
				});
			}
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x0059046C File Offset: 0x0058E66C
		private void Spawn_RainbowRodHit(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 6f;
			float num3 = Main.rand.NextFloat();
			for (float num4 = 0f; num4 < 1f; num4 += 1f / num2)
			{
				Vector2 vector = settings.MovementVector * Main.rand.NextFloatDirection() * 0.15f;
				Vector2 vector2 = new Vector2(Main.rand.NextFloat() * 0.4f + 0.4f);
				float num5 = num + Main.rand.NextFloat() * 6.2831855f;
				float num6 = 1.5707964f;
				Vector2 vector3 = 1.5f * vector2;
				float num7 = 60f;
				Vector2 vector4 = Main.rand.NextVector2Circular(8f, 8f) * vector2;
				PrettySparkleParticle prettySparkleParticle = new PrettySparkleParticle();
				prettySparkleParticle.Velocity = num5.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = num5.ToRotationVector2() * -(vector3 / num7) - vector * 1f / 60f;
				prettySparkleParticle.ColorTint = Main.hslToRgb((num3 + Main.rand.NextFloat() * 0.33f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				prettySparkleParticle.ColorTint.A = 0;
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + vector4;
				prettySparkleParticle.Rotation = num6;
				prettySparkleParticle.Scale = vector2;
				this.SeedParticleSystem.Add(prettySparkleParticle);
				prettySparkleParticle = new PrettySparkleParticle();
				prettySparkleParticle.Velocity = num5.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = num5.ToRotationVector2() * -(vector3 / num7) - vector * 1f / 60f;
				prettySparkleParticle.ColorTint = new Color(255, 255, 255, 0);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + vector4;
				prettySparkleParticle.Rotation = num6;
				prettySparkleParticle.Scale = vector2 * 0.6f;
				this.SeedParticleSystem.Add(prettySparkleParticle);
			}
			for (int i = 0; i < 12; i++)
			{
				Color color = Main.hslToRgb((num3 + Main.rand.NextFloat() * 0.12f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				Dust dust = this.SeedDust.NewDust(settings.PositionInWorld, 0, 0, 267, 0f, 0f, 0, color, 1f);
				dust.velocity = Main.rand.NextVector2Circular(1f, 1f);
				dust.velocity += settings.MovementVector * Main.rand.NextFloatDirection() * 0.5f;
				dust.noGravity = true;
				dust.scale = 0.6f + Main.rand.NextFloat() * 0.9f;
				dust.fadeIn = 0.7f + Main.rand.NextFloat() * 0.8f;
				if (dust.dustIndex != 200)
				{
					Dust dust2 = this.SeedDust.CloneDust(dust);
					dust2.scale /= 2f;
					dust2.fadeIn *= 0.75f;
					dust2.color = new Color(255, 255, 255, 255);
				}
			}
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x0059084E File Offset: 0x0058EA4E
		private void GoBackHere()
		{
			Main.MenuUI.SetState(this._goBackTarget);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x00590860 File Offset: 0x0058EA60
		private void OnFinishedNamingAndCreating(string name)
		{
			this.OnFinishedSettingName(name);
			this.FinishCreatingWorld();
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x00590870 File Offset: 0x0058EA70
		private void FinishCreatingWorld()
		{
			string text = (Main.worldName = this._optionwWorldName.Trim());
			UIWorldCreation.WorldDifficultyId optionDifficulty = this._optionDifficulty;
			Main.ActiveWorldFileData = WorldFile.CreateMetadata(text, SocialAPI.Cloud != null && SocialAPI.Cloud.EnabledByDefault, Main.GameMode);
			this._optionDifficulty = optionDifficulty;
			if (this._optionSeed.Length == 0 || this._isSpecialSeedText)
			{
				Main.ActiveWorldFileData.SetSeedToRandomWithCurrentEvents();
			}
			else
			{
				Main.ActiveWorldFileData.SetSeed(this._optionSeed);
			}
			if (this._secretSeedTextsEntered.Count > 0)
			{
				string text2 = string.Join("|", this._secretSeedTextsEntered) + "|" + Main.ActiveWorldFileData.SeedText;
				Main.ActiveWorldFileData.SetSeed(text2);
			}
			WorldGenerator.Controller controller = new WorldGenerator.Controller(null)
			{
				Paused = (DebugOptions.enableDebugCommands && Main.keyState.PressingControl())
			};
			WorldGen.CreateNewWorld(null, controller, null);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x00590958 File Offset: 0x0058EB58
		private void AssignRandomWorldName()
		{
			do
			{
				LocalizedText localizedText = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Composition.", false), null);
				LocalizedText localizedText2 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Adjective.", true), null);
				LocalizedText localizedText3 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Location.", true), null);
				LocalizedText localizedText4 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Noun.", true), null);
				var <>f__AnonymousType = new
				{
					Adjective = localizedText2.Value,
					Location = localizedText3.Value,
					Noun = localizedText4.Value
				};
				this._optionwWorldName = localizedText.FormatWith(<>f__AnonymousType);
				if (Main.rand.Next(10000) == 0)
				{
					this._optionwWorldName = Language.GetTextValue("SpecialWorldName.TheConstant");
				}
			}
			while (this._optionwWorldName.Length > 27);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x00590A03 File Offset: 0x0058EC03
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (this._goBackTarget != this)
			{
				this._goBackTarget = this;
			}
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
			this.DrawSeedSystems(spriteBatch);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x00590A2A File Offset: 0x0058EC2A
		public void ResetSpecialSeedRing()
		{
			this.ringPoint = 0f;
			Array.Clear(this.oldPos, 0, this.oldPos.Length);
			Array.Clear(this.oldTangent, 0, this.oldTangent.Length);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x00590A60 File Offset: 0x0058EC60
		public void DrawSpecialSeedRingCallback(UIElement element, SpriteBatch spriteBatch)
		{
			if (this.HasEnteredSpecialSeed)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
				if (this.oldPos[0] == Vector2.Zero)
				{
					for (int i = 0; i < 61; i++)
					{
						this.UpdateSpecialSeedRing(element);
					}
				}
				else
				{
					this.specialSeedIndex = (this.specialSeedIndex + 1) % 4;
					if (this.specialSeedIndex % 4 == 0)
					{
						this.UpdateSpecialSeedRing(element);
					}
				}
				this.DrawSpecialSeedRing();
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
			}
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00590B1C File Offset: 0x0058ED1C
		public void DrawSpecialSeedRingCallbackWithoutCondition(UIElement element, SpriteBatch spriteBatch)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
			if (this.oldPos[0] == Vector2.Zero)
			{
				for (int i = 0; i < 61; i++)
				{
					this.UpdateSpecialSeedRing(element);
				}
			}
			else
			{
				this.UpdateSpecialSeedRing(element);
			}
			this.DrawSpecialSeedRing();
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x00590BB4 File Offset: 0x0058EDB4
		private void UpdateSpecialSeedRing(UIElement element)
		{
			CalculatedStyle calculatedStyle = this._advancedSeedButton.GetDimensions();
			if (this._goBackTarget != this)
			{
				UIWorldCreationAdvanced uiworldCreationAdvanced = this._goBackTarget as UIWorldCreationAdvanced;
				if (uiworldCreationAdvanced != null)
				{
					uiworldCreationAdvanced.RefreshSecretSeedButton();
					calculatedStyle = uiworldCreationAdvanced.GetSecretSeedButton().GetDimensions();
				}
			}
			if (element is GroupOptionButton<WorldGen.SecretSeed>)
			{
				calculatedStyle = element.GetDimensions();
			}
			Rectangle rectangle = calculatedStyle.ToRectangle();
			rectangle.Inflate(-1, -1);
			int num = rectangle.Width * 2 + rectangle.Height * 2;
			float num2 = (float)num / 60f;
			this.ringPoint += num2;
			if (this.ringPoint >= (float)num)
			{
				this.ringPoint -= (float)num;
			}
			float num3 = (float)Math.Sqrt((double)(rectangle.Width / 2 * rectangle.Width / 2 + rectangle.Height / 2 * rectangle.Height / 2));
			float num4 = 6.2831855f * this.ringPoint / (float)num;
			Vector2 vector = new Vector2((float)Math.Cos((double)num4), (float)Math.Sin((double)num4));
			Vector2 vector2 = vector * num3;
			float num5 = Math.Abs(vector2.X) / ((float)rectangle.Width / 2f);
			float num6 = Math.Abs(vector2.Y) / ((float)rectangle.Height / 2f);
			if (num5 > num6)
			{
				vector2 /= num5;
				vector /= num5;
			}
			else
			{
				vector2 /= num6;
				vector /= num6;
			}
			vector2 += rectangle.Center.ToVector2();
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldTangent[i] = this.oldTangent[i - 1];
			}
			this.oldPos[0] = vector2;
			this.oldTangent[0] = vector;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x00590DAC File Offset: 0x0058EFAC
		private void DrawSpecialSeedRing()
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(this.trial);
			miscShaderData.UseOpacity((this._goBackTarget != this) ? this.opacity2 : this.opacity);
			miscShaderData.UseSpriteTransformMatrix(new Matrix?(Main.UIScaleMatrix));
			miscShaderData.Apply(null);
			float num = 4f;
			if (this._goBackTarget != this)
			{
				num = 5f;
			}
			int num2 = this.oldPos.Length;
			UIWorldCreation._vertexStrip.Reset(num2 * 2);
			int num3 = num2;
			int num4 = 0;
			while (num4 < num2 && !(this.oldPos[num4] == Vector2.Zero))
			{
				Vector2 vector = this.oldPos[num4];
				float num5 = (float)num4 / (float)(num3 - 1);
				num5 *= 0.6f;
				Color color = this.StripColors(num5);
				float num6 = this.StripWidth(num5);
				Vector2 vector2 = this.oldTangent[num4] * num;
				Vector3 vector3 = new Vector3(num5, num6 / 2f, num6);
				Vector3 vector4 = new Vector3(num5, num6 / 2f, num6);
				UIWorldCreation._vertexStrip.AddVertexPair(vector + vector2, vector, vector3, vector4, color);
				num4++;
			}
			UIWorldCreation._vertexStrip.PrepareIndices(true);
			UIWorldCreation._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
			miscShaderData.UseSpriteTransformMatrix(null);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00590F44 File Offset: 0x0058F144
		private Color StripColors(float progressOnStrip)
		{
			Color color = Main.hslToRgb((progressOnStrip - Main.GlobalTimeWrappedHourly * this.animationSpeed) % 1f, this.saturation, 0.5f, byte.MaxValue);
			color.A = 0;
			return color;
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00590F84 File Offset: 0x0058F184
		private float StripWidth(float progressOnStrip)
		{
			float lerpValue = Utils.GetLerpValue(0f, 0.2f, progressOnStrip, true);
			return 24f * lerpValue;
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x00590FB0 File Offset: 0x0058F1B0
		public void DrawSeedSystems(SpriteBatch spriteBatch)
		{
			this.SeedDust.UpdateDust();
			this.SeedDust.DrawDust();
			this.SeedParticleSystem.Update();
			this.SeedParticleSystem.Draw(spriteBatch);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x00590FE0 File Offset: 0x0058F1E0
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			SnapPoint snapPoint = null;
			SnapPoint snapPoint2 = null;
			SnapPoint snapPoint3 = null;
			SnapPoint snapPoint4 = null;
			SnapPoint snapPoint5 = null;
			SnapPoint snapPoint6 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint7 = snapPoints[i];
				string name = snapPoint7.Name;
				if (!(name == "Back"))
				{
					if (!(name == "Create"))
					{
						if (!(name == "Name"))
						{
							if (!(name == "Seed"))
							{
								if (!(name == "RandomizeName"))
								{
									if (name == "RandomizeSeed")
									{
										snapPoint6 = snapPoint7;
									}
								}
								else
								{
									snapPoint5 = snapPoint7;
								}
							}
							else
							{
								snapPoint4 = snapPoint7;
							}
						}
						else
						{
							snapPoint3 = snapPoint7;
						}
					}
					else
					{
						snapPoint2 = snapPoint7;
					}
				}
				else
				{
					snapPoint = snapPoint7;
				}
			}
			List<SnapPoint> snapGroup = this.GetSnapGroup(snapPoints, "size");
			List<SnapPoint> snapGroup2 = this.GetSnapGroup(snapPoints, "difficulty");
			List<SnapPoint> snapGroup3 = this.GetSnapGroup(snapPoints, "evil");
			UILinkPointNavigator.SetPosition(num, snapPoint.Position);
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint2 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint2.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint3 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint5.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint4 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint3.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint5 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint6.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint6 = uilinkPoint;
			num++;
			UILinkPointNavigator.SetPosition(num, snapPoint4.Position);
			uilinkPoint = UILinkPointNavigator.Points[num];
			uilinkPoint.Unlink();
			UILinkPoint uilinkPoint7 = uilinkPoint;
			num++;
			UILinkPoint[] array = new UILinkPoint[snapGroup.Count];
			for (int j = 0; j < snapGroup.Count; j++)
			{
				UILinkPointNavigator.SetPosition(num, snapGroup[j].Position);
				uilinkPoint = UILinkPointNavigator.Points[num];
				uilinkPoint.Unlink();
				array[j] = uilinkPoint;
				num++;
			}
			UILinkPoint[] array2 = new UILinkPoint[snapGroup2.Count];
			for (int k = 0; k < snapGroup2.Count; k++)
			{
				UILinkPointNavigator.SetPosition(num, snapGroup2[k].Position);
				uilinkPoint = UILinkPointNavigator.Points[num];
				uilinkPoint.Unlink();
				array2[k] = uilinkPoint;
				num++;
			}
			UILinkPoint[] array3 = new UILinkPoint[snapGroup3.Count];
			for (int l = 0; l < snapGroup3.Count; l++)
			{
				UILinkPointNavigator.SetPosition(num, snapGroup3[l].Position);
				uilinkPoint = UILinkPointNavigator.Points[num];
				uilinkPoint.Unlink();
				array3[l] = uilinkPoint;
				num++;
			}
			this.LoopHorizontalLineLinks(array);
			this.LoopHorizontalLineLinks(array2);
			this.EstablishUpDownRelationship(array, array2);
			for (int m = 0; m < array.Length; m++)
			{
				array[m].Up = uilinkPoint7.ID;
			}
			if (true)
			{
				this.LoopHorizontalLineLinks(array3);
				this.EstablishUpDownRelationship(array2, array3);
				for (int n = 0; n < array3.Length; n++)
				{
					array3[n].Down = uilinkPoint2.ID;
				}
				array3[array3.Length - 1].Down = uilinkPoint3.ID;
				uilinkPoint3.Up = array3[array3.Length - 1].ID;
				uilinkPoint2.Up = array3[0].ID;
			}
			else
			{
				for (int num2 = 0; num2 < array2.Length; num2++)
				{
					array2[num2].Down = uilinkPoint2.ID;
				}
				array2[array2.Length - 1].Down = uilinkPoint3.ID;
				uilinkPoint3.Up = array2[array2.Length - 1].ID;
				uilinkPoint2.Up = array2[0].ID;
			}
			uilinkPoint3.Left = uilinkPoint2.ID;
			uilinkPoint2.Right = uilinkPoint3.ID;
			uilinkPoint5.Down = uilinkPoint7.ID;
			uilinkPoint5.Left = uilinkPoint4.ID;
			uilinkPoint4.Right = uilinkPoint5.ID;
			uilinkPoint7.Up = uilinkPoint5.ID;
			uilinkPoint7.Down = array[0].ID;
			uilinkPoint7.Left = uilinkPoint6.ID;
			uilinkPoint6.Right = uilinkPoint7.ID;
			uilinkPoint6.Up = uilinkPoint4.ID;
			uilinkPoint6.Down = array[0].ID;
			uilinkPoint4.Down = uilinkPoint6.ID;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x0059147C File Offset: 0x0058F67C
		private void EstablishUpDownRelationship(UILinkPoint[] topSide, UILinkPoint[] bottomSide)
		{
			int num = Math.Max(topSide.Length, bottomSide.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = Math.Min(i, topSide.Length - 1);
				int num3 = Math.Min(i, bottomSide.Length - 1);
				topSide[num2].Down = bottomSide[num3].ID;
				bottomSide[num3].Up = topSide[num2].ID;
			}
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x005914DC File Offset: 0x0058F6DC
		private void LoopHorizontalLineLinks(UILinkPoint[] pointsLine)
		{
			for (int i = 1; i < pointsLine.Length - 1; i++)
			{
				pointsLine[i - 1].Right = pointsLine[i].ID;
				pointsLine[i].Left = pointsLine[i - 1].ID;
				pointsLine[i].Right = pointsLine[i + 1].ID;
				pointsLine[i + 1].Left = pointsLine[i].ID;
			}
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x00591544 File Offset: 0x0058F744
		private List<SnapPoint> GetSnapGroup(List<SnapPoint> ptsOnPage, string groupName)
		{
			List<SnapPoint> list = ptsOnPage.Where((SnapPoint a) => a.Name == groupName).ToList<SnapPoint>();
			list.Sort(new Comparison<SnapPoint>(this.SortPoints));
			return list;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x00591588 File Offset: 0x0058F788
		private int SortPoints(SnapPoint a, SnapPoint b)
		{
			return a.Id.CompareTo(b.Id);
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x005915A9 File Offset: 0x0058F7A9
		public void AddSeedFromSeedmenu(string seed)
		{
			this._secretSeedTextsEntered.Add(seed);
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x005915B7 File Offset: 0x0058F7B7
		public void RemoveSeedFromSeedMenu(string seed)
		{
			this._secretSeedTextsEntered.Remove(seed);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x0058FCF9 File Offset: 0x0058DEF9
		public void HandleBackButtonUsage()
		{
			UIWorldCreation.GoBack();
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x005915C6 File Offset: 0x0058F7C6
		// Note: this type is marked as 'beforefieldinit'.
		static UIWorldCreation()
		{
		}

		// Token: 0x04005393 RID: 21395
		private string _optionwWorldName;

		// Token: 0x04005394 RID: 21396
		private string _optionSeed;

		// Token: 0x04005395 RID: 21397
		private bool _isSpecialSeedText;

		// Token: 0x04005396 RID: 21398
		private List<string> _secretSeedTextsEntered = new List<string>();

		// Token: 0x04005397 RID: 21399
		private List<string> _disabledSecretSeedTextsEntered = new List<string>();

		// Token: 0x04005398 RID: 21400
		private ParticleRenderer SeedParticleSystem = new ParticleRenderer();

		// Token: 0x04005399 RID: 21401
		private UIDust SeedDust = new UIDust();

		// Token: 0x0400539A RID: 21402
		private GroupOptionButton<bool> _advancedSeedButton;

		// Token: 0x0400539B RID: 21403
		private UICharacterNameButton _namePlate;

		// Token: 0x0400539C RID: 21404
		private UICharacterNameButton _seedPlate;

		// Token: 0x0400539D RID: 21405
		private UIWorldCreationPreview _previewPlate;

		// Token: 0x0400539E RID: 21406
		private GroupOptionButton<UIWorldCreation.WorldSizeId>[] _sizeButtons;

		// Token: 0x0400539F RID: 21407
		private GroupOptionButton<UIWorldCreation.WorldDifficultyId>[] _difficultyButtons;

		// Token: 0x040053A0 RID: 21408
		private GroupOptionButton<UIWorldCreation.WorldEvilId>[] _evilButtons;

		// Token: 0x040053A1 RID: 21409
		private UIText _descriptionText;

		// Token: 0x040053A2 RID: 21410
		public const int MAX_NAME_LENGTH = 27;

		// Token: 0x040053A3 RID: 21411
		private UIState _goBackTarget;

		// Token: 0x040053A4 RID: 21412
		public UIWorldCreation.SubmitSeedEvent SubmitSeed;

		// Token: 0x040053A5 RID: 21413
		private float ringPoint;

		// Token: 0x040053A6 RID: 21414
		private const int numSteps = 61;

		// Token: 0x040053A7 RID: 21415
		private Vector2[] oldPos = new Vector2[61];

		// Token: 0x040053A8 RID: 21416
		private Vector2[] oldTangent = new Vector2[61];

		// Token: 0x040053A9 RID: 21417
		private int specialSeedIndex;

		// Token: 0x040053AA RID: 21418
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x040053AB RID: 21419
		private float opacity = 0.6f;

		// Token: 0x040053AC RID: 21420
		private float opacity2 = 0.5f;

		// Token: 0x040053AD RID: 21421
		private float trial;

		// Token: 0x040053AE RID: 21422
		private float animationSpeed = 0.5f;

		// Token: 0x040053AF RID: 21423
		private float saturation = 0.5f;

		// Token: 0x02000907 RID: 2311
		private enum WorldSizeId
		{
			// Token: 0x04007436 RID: 29750
			Small,
			// Token: 0x04007437 RID: 29751
			Medium,
			// Token: 0x04007438 RID: 29752
			Large
		}

		// Token: 0x02000908 RID: 2312
		private enum WorldDifficultyId
		{
			// Token: 0x0400743A RID: 29754
			Normal,
			// Token: 0x0400743B RID: 29755
			Expert,
			// Token: 0x0400743C RID: 29756
			Master,
			// Token: 0x0400743D RID: 29757
			Creative
		}

		// Token: 0x02000909 RID: 2313
		private enum WorldEvilId
		{
			// Token: 0x0400743F RID: 29759
			Random,
			// Token: 0x04007440 RID: 29760
			Corruption,
			// Token: 0x04007441 RID: 29761
			Crimson
		}

		// Token: 0x0200090A RID: 2314
		// (Invoke) Token: 0x06004755 RID: 18261
		public delegate void SubmitSeedEvent();

		// Token: 0x0200090B RID: 2315
		[CompilerGenerated]
		private sealed class <>c__DisplayClass103_0
		{
			// Token: 0x06004758 RID: 18264 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass103_0()
			{
			}

			// Token: 0x06004759 RID: 18265 RVA: 0x006CB4CA File Offset: 0x006C96CA
			internal bool <GetSnapGroup>b__0(SnapPoint a)
			{
				return a.Name == this.groupName;
			}

			// Token: 0x04007442 RID: 29762
			public string groupName;
		}
	}
}
