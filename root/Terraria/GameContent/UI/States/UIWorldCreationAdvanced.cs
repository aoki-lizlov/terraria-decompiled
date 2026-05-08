using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200039F RID: 927
	public class UIWorldCreationAdvanced : UIState, IHaveBackButtonCommand
	{
		// Token: 0x06002A68 RID: 10856 RVA: 0x0058435E File Offset: 0x0058255E
		public UIWorldCreationAdvanced(UIWorldCreation state, bool allowScrolling = false)
		{
			this._creationState = state;
			this._creationState.SubmitSeed = new UIWorldCreation.SubmitSeedEvent(this.UpdateContents);
			this._allowScrolling = allowScrolling;
			this.BuildPage();
			this.Prepare();
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x00584397 File Offset: 0x00582597
		private void Prepare()
		{
			this.UpdateContents();
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x005843A0 File Offset: 0x005825A0
		private void UpdateContents()
		{
			this._creationState.FillSeedContent(this._seedPlate);
			foreach (GroupOptionButton<AWorldGenerationOption> groupOptionButton in this._seedButtons)
			{
				groupOptionButton.SetCurrentOption(groupOptionButton.OptionValue.Enabled ? groupOptionButton.OptionValue : null);
			}
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x005843F4 File Offset: 0x005825F4
		private void BuildPage()
		{
			base.RemoveAllChildren();
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixels(500f),
				Height = StyleDimension.FromPixelsAndPercent(-200f, 1f),
				Top = StyleDimension.FromPixels(202f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			if (!this._allowScrolling)
			{
				uielement.MaxHeight = StyleDimension.FromPixels(400f);
			}
			uielement.SetPadding(0f);
			base.Append(uielement);
			UIPanel uipanel = new UIPanel
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixelsAndPercent(-102f, 1f),
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

		// Token: 0x06002A6C RID: 10860 RVA: 0x00584560 File Offset: 0x00582760
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
			this.AddSeedButtons(uielement);
			this.AddListArea(uielement);
			this.AddDescriptionPanel(uielement);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x005845F0 File Offset: 0x005827F0
		private void AddListArea(UIElement infoContainer)
		{
			int num = 0;
			UIList uilist = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(-48f, 1f),
				Height = StyleDimension.FromPixelsAndPercent((float)(-138 - num * 2), 1f),
				HAlign = 0f,
				VAlign = 0f,
				Top = StyleDimension.FromPixels((float)(44 + num)),
				Left = StyleDimension.FromPixels(24f)
			};
			num = 4;
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue)
			{
				Height = StyleDimension.FromPixelsAndPercent((float)(-138 - num * 2), 1f),
				Top = StyleDimension.FromPixels((float)(44 + num)),
				HAlign = 1f
			};
			uilist.SetScrollbar(uiscrollbar);
			infoContainer.Append(uilist);
			if (this._allowScrolling)
			{
				infoContainer.Append(uiscrollbar);
			}
			this.AddSpecialSeedOptions(uilist);
			this._optionList = uilist;
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x005846D4 File Offset: 0x005828D4
		public void RefreshSecretSeedButton()
		{
			bool flag = SecretSeedsTracker.SeedsForInterface.Count > 0 || this._creationState.HasEnteredSpecialSeed || this._creationState.HasDisabledSecretSeed;
			if (this._secretSeedButton == null && flag)
			{
				int num = this._seedButtons.Length;
				int num2 = num % 6;
				int num3 = num / 6;
				this._secretSeedButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 1f, 0.5f, 10f)
				{
					Width = StyleDimension.FromPixels(60f),
					Height = StyleDimension.FromPixels(60f),
					InnerHighlightRim = 4,
					HAlign = (float)num2 / 5f,
					Top = StyleDimension.FromPixelsAndPercent((float)(num3 * 67 + 3), 0f),
					ShowHighlightWhenSelected = true
				};
				this._secretSeedButton.SetCurrentOption(this._creationState.HasEnteredSpecialSeed);
				UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/Seed_Secret", 1).Value)
				{
					Left = StyleDimension.FromPixels(-1f)
				};
				uiimage.OnUpdate += this.UpdateIconOpacity;
				this._secretSeedButton.Append(uiimage);
				this._secretSeedButton.SetSnapPoint("seeds", num, null, null);
				this._secretSeedButton.OnMouseOver += this.ShowSecretSeedDescription;
				this._secretSeedButton.OnMouseOut += this.ClearOptionDescription;
				this._secretSeedButton.OnDraw += this._creationState.DrawSpecialSeedRingCallback;
				this._secretSeedButton.OnLeftClick += this.SecretSeedButton_OnLeftClick;
				this._seedButtonRegion.Append(this._secretSeedButton);
				return;
			}
			if (this._secretSeedButton != null && !flag)
			{
				this._seedButtonRegion.RemoveChild(this._secretSeedButton);
				this._secretSeedButton = null;
				return;
			}
			if (this._secretSeedButton != null)
			{
				this._secretSeedButton.SetCurrentOption(this._creationState.HasEnteredSpecialSeed);
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x005848E0 File Offset: 0x00582AE0
		private void SecretSeedButton_OnLeftClick(UIMouseEvent evt, UIElement listeningElement)
		{
			UIWorldCreationAdvancedSecretSeedsList uiworldCreationAdvancedSecretSeedsList = new UIWorldCreationAdvancedSecretSeedsList(this, this._creationState);
			Main.MenuUI.SetState(uiworldCreationAdvancedSecretSeedsList);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x0058491C File Offset: 0x00582B1C
		private void AddSpecialSeedOptions(UIList listArea)
		{
			int num = 6;
			GroupOptionButton<AWorldGenerationOption>[] array = this.PrepareSeedButtons();
			this._seedButtons = array;
			this._seedButtonRegion = new UIElement
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixels((float)Math.Ceiling((double)array.Length / (double)num) * 70f - 10f)
			};
			listArea.Add(this._seedButtonRegion);
			for (int i = 0; i < array.Length; i++)
			{
				GroupOptionButton<AWorldGenerationOption> groupOptionButton = array[i];
				int num2 = i % 6;
				int num3 = i / 6;
				groupOptionButton.HAlign = (float)num2 / 5f;
				groupOptionButton.Top.Set((float)(num3 * 67 + 3), 0f);
				groupOptionButton.OnLeftMouseDown += this.ClickSeedOption;
				groupOptionButton.SetSnapPoint("seeds", i, null, null);
				this._seedButtonRegion.Append(groupOptionButton);
				array[i] = groupOptionButton;
			}
			this.RefreshSecretSeedButton();
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x00584A14 File Offset: 0x00582C14
		private GroupOptionButton<AWorldGenerationOption>[] PrepareSeedButtons()
		{
			List<GroupOptionButton<AWorldGenerationOption>> list = new List<GroupOptionButton<AWorldGenerationOption>>();
			foreach (AWorldGenerationOption aworldGenerationOption in WorldGenerationOptions.Options)
			{
				aworldGenerationOption.Load();
				list.Add(this.CreateButton(new UIWorldCreationAdvanced.WorldSpecialSeedOption
				{
					Seed = aworldGenerationOption,
					Description = aworldGenerationOption.Description,
					Title = aworldGenerationOption.Title,
					Element = aworldGenerationOption.ProvideUIElement()
				}));
			}
			return list.ToArray();
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x00584AB0 File Offset: 0x00582CB0
		private void ClickSeedOption(UIMouseEvent evt, UIElement listeningElement)
		{
			AWorldGenerationOption optionValue = ((GroupOptionButton<AWorldGenerationOption>)listeningElement).OptionValue;
			this._creationState.ToggleSeedOption(optionValue);
			this.UpdateContents();
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x00584ADC File Offset: 0x00582CDC
		private GroupOptionButton<AWorldGenerationOption> CreateButton(UIWorldCreationAdvanced.WorldSpecialSeedOption option)
		{
			GroupOptionButton<AWorldGenerationOption> groupOptionButton = new GroupOptionButton<AWorldGenerationOption>(option.Seed, null, option.Description, Color.White, null, 1f, 1f, 16f)
			{
				Width = StyleDimension.FromPixels(60f),
				Height = StyleDimension.FromPixels(60f),
				InnerHighlightRim = 4
			};
			groupOptionButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement elem)
			{
				this.ShowOptionDescription(option.Description, option.Title);
			};
			groupOptionButton.OnMouseOut += this.ClearOptionDescription;
			UIElement element = option.Element;
			element.OnUpdate += this.UpdateIconOpacity;
			groupOptionButton.Append(element);
			if (false)
			{
				UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/IconCompletion", 1))
				{
					HAlign = 0.5f,
					VAlign = 0.5f,
					Top = new StyleDimension(-9f, 0f),
					Left = new StyleDimension(-3f, 0f),
					IgnoresMouseInteraction = true
				};
				groupOptionButton.Append(uiimage);
			}
			return groupOptionButton;
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x00584C08 File Offset: 0x00582E08
		private void UpdateIconOpacity(UIElement affectedElement)
		{
			GroupOptionButton<AWorldGenerationOption> groupOptionButton = affectedElement.Parent as GroupOptionButton<AWorldGenerationOption>;
			if (groupOptionButton == null)
			{
				return;
			}
			float num = 0.5f;
			bool flag = groupOptionButton.IsSelected || groupOptionButton.IsMouseHovering;
			UIImage uiimage = affectedElement as UIImage;
			if (uiimage != null)
			{
				uiimage.Color = (flag ? Color.White : (Color.White * num));
			}
			UIImageFramed uiimageFramed = affectedElement as UIImageFramed;
			if (uiimageFramed != null)
			{
				uiimageFramed.Color = (flag ? Color.White : (Color.White * num));
			}
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x00584C8C File Offset: 0x00582E8C
		private void AddDescriptionPanel(UIElement container)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(88f, 0f),
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
				Height = StyleDimension.FromPixelsAndPercent(24f, 0f),
				Top = StyleDimension.FromPixelsAndPercent(0f, 0f),
				PaddingLeft = 20f,
				PaddingRight = 20f,
				PaddingTop = 6f
			};
			uislicedImage.Append(uitext);
			this._titleText = uitext;
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPercent(1f),
				Top = StyleDimension.FromPixels(22f),
				VAlign = 0f,
				Color = new Color(131, 135, 183, 255)
			};
			uislicedImage.Append(uihorizontalSeparator);
			UIText uitext2 = new UIText(Language.GetText("UI.WorldDescriptionDefault"), 0.7f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-30f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(25f, 0f),
				PaddingLeft = 20f,
				PaddingRight = 20f,
				PaddingTop = 6f,
				IsWrapped = true
			};
			uislicedImage.Append(uitext2);
			this._descriptionText = uitext2;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x00584ED0 File Offset: 0x005830D0
		private void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = null;
			UICharacterNameButton uicharacterNameButton = listeningElement as UICharacterNameButton;
			if (uicharacterNameButton != null)
			{
				localizedText = uicharacterNameButton.Description;
			}
			GroupOptionButton<bool> groupOptionButton = listeningElement as GroupOptionButton<bool>;
			if (groupOptionButton != null)
			{
				localizedText = groupOptionButton.Description;
			}
			if (localizedText != null)
			{
				this.ShowOptionDescription(localizedText, Language.Exists(localizedText.Key + "_Title") ? Language.GetText(localizedText.Key + "_Title") : null);
			}
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x00584F3C File Offset: 0x0058313C
		private void ShowSecretSeedDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			DynamicSpriteFont value = FontAssets.MouseText.Value;
			string joinedSecretSeedString = this._creationState.GetJoinedSecretSeedString(value, this._descriptionText.GetInnerDimensions().Width / 0.7f, this._descriptionText.GetInnerDimensions().Height / 0.7f);
			this._descriptionText.SetText(joinedSecretSeedString);
			this._titleText.SetText(Language.GetText("UI.WorldDescriptionSecretSeeds_Title"));
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x00584FAE File Offset: 0x005831AE
		private void ShowOptionDescription(LocalizedText description, LocalizedText title)
		{
			this._descriptionText.SetText(description);
			if (title != null)
			{
				this._titleText.SetText(title);
			}
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x00584FCB File Offset: 0x005831CB
		private void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this.ShowOptionDescription(Language.GetText("UI.WorldDescriptionDefault"), Language.GetText("UI.WorldDescriptionDefault_Title"));
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00584FE8 File Offset: 0x005831E8
		private void AddSeedButtons(UIElement infoContainer)
		{
			float num = 0f;
			float num2 = 44f;
			float num3 = num + num2;
			float num4 = num2;
			float num5 = 0f;
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, Language.GetText("UI.WorldCreationRandomizeSeedDescription"), Color.White, null, 1f, 0.5f, 10f)
			{
				Width = StyleDimension.FromPixelsAndPercent(40f, 0f),
				Height = new StyleDimension(40f, 0f),
				HAlign = 0f,
				Top = StyleDimension.FromPixelsAndPercent(num5, 0f),
				ShowHighlightWhenSelected = false
			};
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom", 1))
			{
				IgnoresMouseInteraction = true,
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			groupOptionButton.Append(uiimage);
			groupOptionButton.OnLeftMouseDown += this.ClickRandomizeSeed;
			groupOptionButton.OnMouseOver += this.ShowOptionDescription;
			groupOptionButton.OnMouseOut += this.ClearOptionDescription;
			groupOptionButton.SetSnapPoint("RandomizeSeed", 0, null, null);
			infoContainer.Append(groupOptionButton);
			this._randomButton = groupOptionButton;
			UICharacterNameButton uicharacterNameButton = new UICharacterNameButton(Language.GetText("UI.WorldCreationSeed"), Language.GetText("UI.WorldCreationSeedEmpty"), Language.GetText("UI.WorldDescriptionSeed"))
			{
				Width = StyleDimension.FromPixelsAndPercent(-num3, 1f),
				HAlign = 0f,
				Left = new StyleDimension(num4, 0f),
				Top = StyleDimension.FromPixelsAndPercent(num5, 0f),
				DistanceFromTitleToOption = 29f
			};
			uicharacterNameButton.OnLeftMouseDown += this.Click_SetSeed;
			uicharacterNameButton.OnMouseOver += this.ShowOptionDescription;
			uicharacterNameButton.OnMouseOut += this.ClearOptionDescription;
			uicharacterNameButton.SetSnapPoint("Seed", 0, null, null);
			infoContainer.Append(uicharacterNameButton);
			this._seedPlate = uicharacterNameButton;
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00585200 File Offset: 0x00583400
		private void ClickRandomizeSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			this._creationState.RandomizeSeed();
			this.UpdateContents();
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00585213 File Offset: 0x00583413
		private void Click_SetSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			this._creationState.OpenSeedInputMenu();
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x00585220 File Offset: 0x00583420
		private void MakeBackAndCreatebuttons(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Apply"), 0.65f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(-43f)
			};
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftMouseDown += this.Click_GoBack;
			uitextPanel.SetSnapPoint("Back", 0, null, null);
			outerContainer.Append(uitextPanel);
			this._backButton = uitextPanel;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x005852F0 File Offset: 0x005834F0
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			this.GoBack();
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x005852F8 File Offset: 0x005834F8
		private void GoBack()
		{
			this._creationState.ResetSpecialSeedRing();
			this._creationState.SetGoBackTarget(this._creationState);
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._creationState);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x00585348 File Offset: 0x00583548
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x0058539D File Offset: 0x0058359D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x005853DC File Offset: 0x005835DC
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
			this._creationState.DrawSeedSystems(spriteBatch);
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x005853F8 File Offset: 0x005835F8
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int num2 = num;
			this.GetSnapPoints();
			UILinkPoint linkPoint = this._helper.GetLinkPoint(num2++, this._backButton);
			UILinkPoint linkPoint2 = this._helper.GetLinkPoint(num2++, this._seedPlate);
			UILinkPoint linkPoint3 = this._helper.GetLinkPoint(num2++, this._randomButton);
			List<SnapPoint> snapPoints = this._optionList.GetSnapPoints();
			UILinkPoint[,] array = this._helper.CreateUILinkPointGrid(ref num2, snapPoints, 6, linkPoint2, null, null, linkPoint);
			this._helper.PairLeftRight(linkPoint3, linkPoint2);
			UILinkPoint uilinkPoint = array[0, 0];
			this._helper.PairUpDown(linkPoint3, uilinkPoint);
			this._helper.PairUpDown(linkPoint2, uilinkPoint);
			UILinkPoint uilinkPoint2 = array[0, array.GetLength(1) - 1];
			this._helper.PairUpDown(uilinkPoint2, linkPoint);
			this._helper.MoveToVisuallyClosestPoint(num, num2);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x005854E8 File Offset: 0x005836E8
		public GroupOptionButton<bool> GetSecretSeedButton()
		{
			return this._secretSeedButton;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x005852F0 File Offset: 0x005834F0
		public void HandleBackButtonUsage()
		{
			this.GoBack();
		}

		// Token: 0x04005314 RID: 21268
		private UIWorldCreation _creationState;

		// Token: 0x04005315 RID: 21269
		private UIText _descriptionText;

		// Token: 0x04005316 RID: 21270
		private UIText _titleText;

		// Token: 0x04005317 RID: 21271
		private UICharacterNameButton _seedPlate;

		// Token: 0x04005318 RID: 21272
		private UIElement _backButton;

		// Token: 0x04005319 RID: 21273
		private UIElement _optionList;

		// Token: 0x0400531A RID: 21274
		private UIElement _randomButton;

		// Token: 0x0400531B RID: 21275
		private GroupOptionButton<AWorldGenerationOption>[] _seedButtons;

		// Token: 0x0400531C RID: 21276
		private UIElement _seedButtonRegion;

		// Token: 0x0400531D RID: 21277
		private GroupOptionButton<bool> _secretSeedButton;

		// Token: 0x0400531E RID: 21278
		private bool _allowScrolling;

		// Token: 0x0400531F RID: 21279
		private UIGamepadHelper _helper;

		// Token: 0x020008F5 RID: 2293
		private struct WorldSpecialSeedOption
		{
			// Token: 0x040073FB RID: 29691
			public AWorldGenerationOption Seed;

			// Token: 0x040073FC RID: 29692
			public UIElement Element;

			// Token: 0x040073FD RID: 29693
			public LocalizedText Description;

			// Token: 0x040073FE RID: 29694
			public LocalizedText Title;
		}

		// Token: 0x020008F6 RID: 2294
		[CompilerGenerated]
		private sealed class <>c__DisplayClass23_0
		{
			// Token: 0x06004726 RID: 18214 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass23_0()
			{
			}

			// Token: 0x06004727 RID: 18215 RVA: 0x006CB335 File Offset: 0x006C9535
			internal void <CreateButton>b__0(UIMouseEvent evt, UIElement elem)
			{
				this.<>4__this.ShowOptionDescription(this.option.Description, this.option.Title);
			}

			// Token: 0x040073FF RID: 29695
			public UIWorldCreationAdvanced <>4__this;

			// Token: 0x04007400 RID: 29696
			public UIWorldCreationAdvanced.WorldSpecialSeedOption option;
		}
	}
}
