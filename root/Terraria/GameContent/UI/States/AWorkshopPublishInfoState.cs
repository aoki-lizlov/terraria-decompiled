using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.Utilities.FileBrowser;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003A0 RID: 928
	public abstract class AWorkshopPublishInfoState<TPublishedObjectType> : UIState, IHaveBackButtonCommand
	{
		// Token: 0x06002A86 RID: 10886 RVA: 0x005854F0 File Offset: 0x005836F0
		public AWorkshopPublishInfoState(UIState stateToGoBackTo, TPublishedObjectType dataObject)
		{
			this._previousUIState = stateToGoBackTo;
			this._dataObject = dataObject;
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x00585510 File Offset: 0x00583710
		public override void OnInitialize()
		{
			base.OnInitialize();
			int num = 40;
			int num2 = 200;
			int num3 = 50 + num + 10;
			int num4 = 70;
			UIElement uielement = new UIElement();
			uielement.Width.Set(600f, 0f);
			uielement.Top.Set((float)num2, 0f);
			uielement.Height.Set((float)(-(float)num2), 1f);
			uielement.HAlign = 0.5f;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set((float)(-(float)num3), 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			this.AddBackButton(num, uielement);
			this.AddPublishButton(num, uielement);
			int num5 = 6 + num4;
			UIList uilist = this.AddUIList(uipanel, (float)num5);
			this.FillUIList(uilist);
			this.AddHorizontalSeparator(uipanel, 0f, 0).Top = new StyleDimension((float)(-(float)num4 + 3), 1f);
			this.AddDescriptionPanel(uipanel, (float)(num4 - 6), "desc");
			uielement.Append(uipanel);
			base.Append(uielement);
			this.SetDefaultOptions();
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x0058564C File Offset: 0x0058384C
		private void SetDefaultOptions()
		{
			this._optionPublicity = WorkshopItemPublicSettingId.Public;
			GroupOptionButton<WorkshopItemPublicSettingId>[] publicityOptions = this._publicityOptions;
			for (int i = 0; i < publicityOptions.Length; i++)
			{
				publicityOptions[i].SetCurrentOption(this._optionPublicity);
			}
			this.SetTagsFromFoundEntry();
			this.UpdateImagePreview();
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x00585690 File Offset: 0x00583890
		private void FillUIList(UIList uiList)
		{
			UIElement uielement = new UIElement
			{
				Width = new StyleDimension(0f, 0f),
				Height = new StyleDimension(0f, 0f)
			};
			uielement.SetPadding(0f);
			uiList.Add(uielement);
			uiList.Add(this.CreateSteamDisclaimer("disclaimer"));
			uiList.Add(this.CreatePreviewImageSelectionPanel("image"));
			uiList.Add(this.CreatePublicSettingsRow(0f, 44f, "public"));
			uiList.Add(this.CreateTagOptionsPanel(0f, 44, "tags"));
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x00585734 File Offset: 0x00583934
		private UIElement CreatePreviewImageSelectionPanel(string tagGroup)
		{
			UIElement uielement = new UIElement();
			uielement.Width = new StyleDimension(0f, 1f);
			uielement.Height = new StyleDimension(80f, 0f);
			UIElement uielement2 = new UIElement
			{
				Width = new StyleDimension(72f, 0f),
				Height = new StyleDimension(72f, 0f),
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(-6f, 0f),
				Top = new StyleDimension(0f, 0f)
			};
			uielement2.SetPadding(0f);
			uielement.Append(uielement2);
			float num = 86f;
			this._defaultPreviewImageTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/DefaultPreviewImage", 1);
			UIImage uiimage = new UIImage(this._defaultPreviewImageTexture)
			{
				Width = new StyleDimension(-4f, 1f),
				Height = new StyleDimension(-4f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				ScaleToFit = true,
				AllowResizingDimensions = false
			};
			UIImage uiimage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 1))
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uielement2.Append(uiimage);
			uielement2.Append(uiimage2);
			this._previewImageUIElement = uiimage;
			UICharacterNameButton uicharacterNameButton = new UICharacterNameButton(Language.GetText("Workshop.PreviewImagePathTitle"), Language.GetText("Workshop.PreviewImagePathEmpty"), Language.GetText("Workshop.PreviewImagePathDescription"))
			{
				Width = StyleDimension.FromPixelsAndPercent(-num, 1f),
				Height = new StyleDimension(0f, 1f)
			};
			uicharacterNameButton.OnLeftMouseDown += this.Click_SetPreviewImage;
			uicharacterNameButton.OnMouseOver += this.ShowOptionDescription;
			uicharacterNameButton.OnMouseOut += this.ClearOptionDescription;
			uicharacterNameButton.SetSnapPoint(tagGroup, 0, null, null);
			uielement.Append(uicharacterNameButton);
			this._previewImagePathPlate = uicharacterNameButton;
			return uielement;
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0058595C File Offset: 0x00583B5C
		private void SetTagsFromFoundEntry()
		{
			FoundWorkshopEntryInfo foundWorkshopEntryInfo;
			if (!this.TryFindingTags(out foundWorkshopEntryInfo))
			{
				return;
			}
			if (foundWorkshopEntryInfo.tags != null)
			{
				foreach (GroupOptionButton<WorkshopTagOption> groupOptionButton in this._tagOptions)
				{
					bool flag = foundWorkshopEntryInfo.tags.Contains(groupOptionButton.OptionValue.InternalNameForAPIs);
					groupOptionButton.SetCurrentOption(flag ? groupOptionButton.OptionValue : null);
					groupOptionButton.SetColor(groupOptionButton.IsSelected ? new Color(152, 175, 235) : Colors.InventoryDefaultColor, 1f);
				}
			}
			this._optionPublicity = foundWorkshopEntryInfo.publicity;
			GroupOptionButton<WorkshopItemPublicSettingId>[] publicityOptions = this._publicityOptions;
			for (int i = 0; i < publicityOptions.Length; i++)
			{
				publicityOptions[i].SetCurrentOption(foundWorkshopEntryInfo.publicity);
			}
		}

		// Token: 0x06002A8C RID: 10892
		protected abstract bool TryFindingTags(out FoundWorkshopEntryInfo info);

		// Token: 0x06002A8D RID: 10893 RVA: 0x00585A4C File Offset: 0x00583C4C
		private void Click_SetPreviewImage(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			this.OpenFileDialogueToSelectPreviewImage();
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x00585A6C File Offset: 0x00583C6C
		private UIElement CreateSteamDisclaimer(string tagGroup)
		{
			float num = 60f;
			float num2 = 0f + num;
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 1f, 0.5f, 16f);
			groupOptionButton.HAlign = 0.5f;
			groupOptionButton.VAlign = 0f;
			groupOptionButton.Width = StyleDimension.FromPixelsAndPercent(0f, 1f);
			groupOptionButton.Left = StyleDimension.FromPixels(0f);
			groupOptionButton.Height = StyleDimension.FromPixelsAndPercent(num2 + 4f, 0f);
			groupOptionButton.Top = StyleDimension.FromPixels(0f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetCurrentOption(false);
			groupOptionButton.Width.Set(0f, 1f);
			UIElement uielement = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(num, 0f)
			};
			groupOptionButton.Append(uielement);
			UIText uitext = new UIText(Language.GetText("Workshop.SteamDisclaimer"), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				TextColor = Color.Cyan,
				IgnoresMouseInteraction = true
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 4f;
			uitext.IsWrapped = true;
			this._disclaimerText = uitext;
			groupOptionButton.OnLeftClick += this.steamDisclaimerText_OnClick;
			groupOptionButton.OnMouseOver += this.steamDisclaimerText_OnMouseOver;
			groupOptionButton.OnMouseOut += this.steamDisclaimerText_OnMouseOut;
			uielement.Append(uitext);
			uitext.SetSnapPoint(tagGroup, 0, null, null);
			this._steamDisclaimerButton = uitext;
			return groupOptionButton;
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x00585C69 File Offset: 0x00583E69
		private void steamDisclaimerText_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._disclaimerText.TextColor = Color.Cyan;
			this.ClearOptionDescription(evt, listeningElement);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00585C83 File Offset: 0x00583E83
		private void steamDisclaimerText_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._disclaimerText.TextColor = Color.LightCyan;
			this.ShowOptionDescription(evt, listeningElement);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x00585CB4 File Offset: 0x00583EB4
		private void steamDisclaimerText_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			try
			{
				Platform.Get<IPathService>().OpenURL("https://steamcommunity.com/sharedfiles/workshoplegalagreement");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x00585CE8 File Offset: 0x00583EE8
		public override void Recalculate()
		{
			this.UpdateScrollbar();
			base.Recalculate();
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x00585CF8 File Offset: 0x00583EF8
		private void UpdateScrollbar()
		{
			if (this._scrollbar == null)
			{
				return;
			}
			if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
			{
				this._uiListContainer.RemoveChild(this._scrollbar);
				this._isScrollbarAttached = false;
				this._uiListRect.Width.Set(0f, 1f);
				return;
			}
			if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
			{
				this._uiListContainer.Append(this._scrollbar);
				this._isScrollbarAttached = true;
				this._uiListRect.Width.Set(-25f, 1f);
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00585DA0 File Offset: 0x00583FA0
		private UIList AddUIList(UIElement container, float antiHeight)
		{
			this._uiListContainer = container;
			float num = 0f;
			UIElement uielement = new UIElement
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(-2f - antiHeight, 1f),
				OverflowHidden = true
			};
			this._listContainer = uielement;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/Workshop/ListBackground", 1))
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				Color = Color.White * 0.7f
			};
			uislicedImage.SetSliceDepths(4);
			container.Append(uielement);
			uielement.Append(uislicedImage);
			UIList uilist = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-4f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				OverflowHidden = true
			};
			uilist.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
			uilist.ListPadding = 5f;
			uielement.Append(uilist);
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue)
			{
				HAlign = 1f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(-14f - antiHeight, 1f),
				Top = StyleDimension.FromPixels(6f)
			};
			uiscrollbar.SetView(100f, 1000f);
			uilist.SetScrollbar(uiscrollbar);
			this._uiListRect = uielement;
			this._scrollbar = uiscrollbar;
			return uilist;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x00009E46 File Offset: 0x00008046
		private void ManualIfnoSortingMethod(List<UIElement> list)
		{
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x00585F90 File Offset: 0x00584190
		private UIElement CreatePublicSettingsRow(float accumulatedHeight, float height, string tagGroup)
		{
			UIElement uielement;
			UIElement uielement2;
			this.CreateStylizedCategoryPanel(height, "Workshop.CategoryTitlePublicity", out uielement, out uielement2);
			WorkshopItemPublicSettingId[] array = new WorkshopItemPublicSettingId[3];
			array[0] = WorkshopItemPublicSettingId.Public;
			array[1] = WorkshopItemPublicSettingId.FriendsOnly;
			WorkshopItemPublicSettingId[] array2 = array;
			LocalizedText[] array3 = new LocalizedText[]
			{
				Language.GetText("Workshop.SettingsPublicityPublic"),
				Language.GetText("Workshop.SettingsPublicityFriendsOnly"),
				Language.GetText("Workshop.SettingsPublicityPrivate")
			};
			LocalizedText[] array4 = new LocalizedText[]
			{
				Language.GetText("Workshop.SettingsPublicityPublicDescription"),
				Language.GetText("Workshop.SettingsPublicityFriendsOnlyDescription"),
				Language.GetText("Workshop.SettingsPublicityPrivateDescription")
			};
			Color[] array5 = new Color[]
			{
				Color.White,
				Color.White,
				Color.White
			};
			string[] array6 = new string[] { "Images/UI/Workshop/PublicityPublic", "Images/UI/Workshop/PublicityFriendsOnly", "Images/UI/Workshop/PublicityPrivate" };
			float num = 0.98f;
			GroupOptionButton<WorkshopItemPublicSettingId>[] array7 = new GroupOptionButton<WorkshopItemPublicSettingId>[array2.Length];
			for (int i = 0; i < array7.Length; i++)
			{
				GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton = new GroupOptionButton<WorkshopItemPublicSettingId>(array2[i], array3[i], array4[i], array5[i], array6[i], 1f, 1f, 16f);
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-4 * (array7.Length - 1)), 1f / (float)array7.Length * num);
				groupOptionButton.HAlign = (float)i / (float)(array7.Length - 1);
				groupOptionButton.Left = StyleDimension.FromPercent((1f - num) * (1f - groupOptionButton.HAlign * 2f));
				groupOptionButton.Top.Set(accumulatedHeight, 0f);
				groupOptionButton.OnLeftMouseDown += this.ClickPublicityOption;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				uielement2.Append(groupOptionButton);
				array7[i] = groupOptionButton;
			}
			this._publicityOptions = array7;
			return uielement;
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x005861A0 File Offset: 0x005843A0
		private UIElement CreateTagOptionsPanel(float accumulatedHeight, int heightPerRow, string tagGroup)
		{
			List<WorkshopTagOption> tagsToShow = this.GetTagsToShow();
			int num = 3;
			int num2 = (int)Math.Ceiling((double)((float)tagsToShow.Count / (float)num));
			int num3 = heightPerRow * num2;
			UIElement uielement;
			UIElement uielement2;
			this.CreateStylizedCategoryPanel((float)num3, "Workshop.CategoryTitleTags", out uielement, out uielement2);
			float num4 = 0.98f;
			List<GroupOptionButton<WorkshopTagOption>> list = new List<GroupOptionButton<WorkshopTagOption>>();
			for (int i = 0; i < tagsToShow.Count; i++)
			{
				WorkshopTagOption workshopTagOption = tagsToShow[i];
				GroupOptionButton<WorkshopTagOption> groupOptionButton = new GroupOptionButton<WorkshopTagOption>(workshopTagOption, Language.GetText(workshopTagOption.NameKey), Language.GetText(workshopTagOption.NameKey + "Description"), Color.White, null, 1f, 0.5f, 16f);
				groupOptionButton.ShowHighlightWhenSelected = false;
				groupOptionButton.SetCurrentOption(null);
				int num5 = i / num;
				int num6 = i - num5 * num;
				groupOptionButton.Width = StyleDimension.FromPixelsAndPercent((float)(-4 * (num - 1)), 1f / (float)num * num4);
				groupOptionButton.HAlign = (float)num6 / (float)(num - 1);
				groupOptionButton.Left = StyleDimension.FromPercent((1f - num4) * (1f - groupOptionButton.HAlign * 2f));
				groupOptionButton.Top.Set((float)(num5 * heightPerRow), 0f);
				groupOptionButton.OnLeftMouseDown += this.ClickTagOption;
				groupOptionButton.OnMouseOver += this.ShowOptionDescription;
				groupOptionButton.OnMouseOut += this.ClearOptionDescription;
				groupOptionButton.SetSnapPoint(tagGroup, i, null, null);
				uielement2.Append(groupOptionButton);
				list.Add(groupOptionButton);
			}
			this._tagOptions = list;
			return uielement;
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x00586350 File Offset: 0x00584550
		private void CreateStylizedCategoryPanel(float height, string titleTextKey, out UIElement entirePanel, out UIElement innerPanel)
		{
			float num = 44f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1))
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Left = StyleDimension.FromPixels(0f),
				Height = StyleDimension.FromPixelsAndPercent(height + num + 4f, 0f),
				Top = StyleDimension.FromPixels(0f)
			};
			uislicedImage.SetSliceDepths(8);
			uislicedImage.Color = Color.White * 0.7f;
			innerPanel = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(height, 0f)
			};
			uislicedImage.Append(innerPanel);
			this.AddHorizontalSeparator(uislicedImage, num, 4);
			UIText uitext = new UIText(Language.GetText(titleTextKey), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(num, 0f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 6f;
			uitext.IsWrapped = false;
			uislicedImage.Append(uitext);
			entirePanel = uislicedImage;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x005864E8 File Offset: 0x005846E8
		private void ClickTagOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<WorkshopTagOption> groupOptionButton = (GroupOptionButton<WorkshopTagOption>)listeningElement;
			groupOptionButton.SetCurrentOption(groupOptionButton.IsSelected ? null : groupOptionButton.OptionValue);
			groupOptionButton.SetColor(groupOptionButton.IsSelected ? new Color(152, 175, 235) : Colors.InventoryDefaultColor, 1f);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x00586544 File Offset: 0x00584744
		private void ClickPublicityOption(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton = (GroupOptionButton<WorkshopItemPublicSettingId>)listeningElement;
			this._optionPublicity = groupOptionButton.OptionValue;
			GroupOptionButton<WorkshopItemPublicSettingId>[] publicityOptions = this._publicityOptions;
			for (int i = 0; i < publicityOptions.Length; i++)
			{
				publicityOptions[i].SetCurrentOption(groupOptionButton.OptionValue);
			}
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x00586588 File Offset: 0x00584788
		public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = null;
			GroupOptionButton<WorkshopItemPublicSettingId> groupOptionButton = listeningElement as GroupOptionButton<WorkshopItemPublicSettingId>;
			if (groupOptionButton != null)
			{
				localizedText = groupOptionButton.Description;
			}
			UICharacterNameButton uicharacterNameButton = listeningElement as UICharacterNameButton;
			if (uicharacterNameButton != null)
			{
				localizedText = uicharacterNameButton.Description;
			}
			GroupOptionButton<bool> groupOptionButton2 = listeningElement as GroupOptionButton<bool>;
			if (groupOptionButton2 != null)
			{
				localizedText = groupOptionButton2.Description;
			}
			GroupOptionButton<WorkshopTagOption> groupOptionButton3 = listeningElement as GroupOptionButton<WorkshopTagOption>;
			if (groupOptionButton3 != null)
			{
				localizedText = groupOptionButton3.Description;
			}
			if (listeningElement == this._steamDisclaimerButton)
			{
				localizedText = Language.GetText("Workshop.SteamDisclaimerDescrpition");
			}
			if (localizedText != null)
			{
				this._descriptionText.SetText(localizedText);
			}
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x00586601 File Offset: 0x00584801
		public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this._descriptionText.SetText(Language.GetText("Workshop.InfoDescriptionDefault"));
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x00586618 File Offset: 0x00584818
		private UIElement CreateInsturctionsPanel(float accumulatedHeight, float height, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1));
			uislicedImage.HAlign = 0.5f;
			uislicedImage.VAlign = 0f;
			uislicedImage.Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f);
			uislicedImage.Left = StyleDimension.FromPixels(-num);
			uislicedImage.Height = StyleDimension.FromPixelsAndPercent(height, 0f);
			uislicedImage.Top = StyleDimension.FromPixels(accumulatedHeight);
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			UIText uitext = new UIText(Language.GetText(this._instructionsTextKey), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 6f;
			uitext.IsWrapped = true;
			uislicedImage.Append(uitext);
			return uislicedImage;
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x00586750 File Offset: 0x00584950
		private void AddDescriptionPanel(UIElement container, float height, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(height, 0f),
				Top = StyleDimension.FromPixels(2f)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			container.Append(uislicedImage);
			UIText uitext = new UIText(Language.GetText("Workshop.InfoDescriptionDefault"), 0.85f, false)
			{
				HAlign = 0f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f)
			};
			uitext.PaddingLeft = 4f;
			uitext.PaddingRight = 4f;
			uitext.PaddingTop = 4f;
			uitext.IsWrapped = true;
			uislicedImage.Append(uitext);
			this._descriptionText = uitext;
		}

		// Token: 0x06002A9F RID: 10911
		protected abstract string GetPublishedObjectDisplayName();

		// Token: 0x06002AA0 RID: 10912
		protected abstract List<WorkshopTagOption> GetTagsToShow();

		// Token: 0x06002AA1 RID: 10913 RVA: 0x00586885 File Offset: 0x00584A85
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0058688D File Offset: 0x00584A8D
		public void HandleBackButtonUsage()
		{
			if (this._previousUIState == null)
			{
				Main.menuMode = 0;
				return;
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._previousUIState);
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x005868B8 File Offset: 0x00584AB8
		private void Click_Publish(UIMouseEvent evt, UIElement listeningElement)
		{
			this.GoToPublishConfirmation();
		}

		// Token: 0x06002AA4 RID: 10916
		protected abstract void GoToPublishConfirmation();

		// Token: 0x06002AA5 RID: 10917 RVA: 0x005868C0 File Offset: 0x00584AC0
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0058539D File Offset: 0x0058359D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00586918 File Offset: 0x00584B18
		private void AddPublishButton(int backButtonYLift, UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("Workshop.Publish"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.Top.Set((float)(-(float)backButtonYLift), 0f);
			uitextPanel.HAlign = 1f;
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftClick += this.Click_Publish;
			uitextPanel.SetSnapPoint("publish", 0, null, null);
			outerContainer.Append(uitextPanel);
			this._publishButton = uitextPanel;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x005869F0 File Offset: 0x00584BF0
		private void AddBackButton(int backButtonYLift, UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.Top.Set((float)(-(float)backButtonYLift), 0f);
			uitextPanel.HAlign = 0f;
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftClick += this.Click_GoBack;
			uitextPanel.SetSnapPoint("back", 0, null, null);
			outerContainer.Append(uitextPanel);
			this._backButton = uitextPanel;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x00586AC8 File Offset: 0x00584CC8
		private UIElement AddHorizontalSeparator(UIElement Container, float accumualtedHeight, int widthReduction = 0)
		{
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPixelsAndPercent((float)(-(float)widthReduction), 1f),
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(accumualtedHeight - 8f),
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			Container.Append(uihorizontalSeparator);
			return uihorizontalSeparator;
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x00586B48 File Offset: 0x00584D48
		protected WorkshopItemPublishSettings GetPublishSettings()
		{
			WorkshopItemPublishSettings workshopItemPublishSettings = new WorkshopItemPublishSettings();
			workshopItemPublishSettings.Publicity = this._optionPublicity;
			workshopItemPublishSettings.UsedTags = (from x in this._tagOptions
				where x.IsSelected
				select x.OptionValue).ToArray<WorkshopTagOption>();
			workshopItemPublishSettings.PreviewImagePath = this._previewImagePath;
			return workshopItemPublishSettings;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x00586BCC File Offset: 0x00584DCC
		private void OpenFileDialogueToSelectPreviewImage()
		{
			ExtensionFilter[] array = new ExtensionFilter[]
			{
				new ExtensionFilter("Image files", new string[] { "png", "jpg", "jpeg" })
			};
			string text = FileBrowser.OpenFilePanel("Open icon", array);
			if (text != null)
			{
				this._previewImagePath = text;
				this.UpdateImagePreview();
			}
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x00586C2C File Offset: 0x00584E2C
		private string PrettifyPath(string path)
		{
			if (path == null)
			{
				return path;
			}
			char[] array = new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			};
			int num = path.LastIndexOfAny(array);
			if (num != -1)
			{
				path = path.Substring(num + 1);
			}
			if (path.Length > 30)
			{
				path = path.Substring(0, 30) + "…";
			}
			return path;
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x00586C8C File Offset: 0x00584E8C
		private void UpdateImagePreview()
		{
			Texture2D texture2D = null;
			string text = this.PrettifyPath(this._previewImagePath);
			this._previewImagePathPlate.SetContents(text);
			if (this._previewImagePath != null)
			{
				try
				{
					using (FileStream fileStream = File.OpenRead(this._previewImagePath))
					{
						texture2D = Texture2D.FromStream(Main.graphics.GraphicsDevice, fileStream);
					}
				}
				catch (Exception ex)
				{
					FancyErrorPrinter.ShowFailedToLoadAssetError(ex, this._previewImagePath);
				}
			}
			if (texture2D != null && (texture2D.Width > 512 || texture2D.Height > 512))
			{
				object obj = new { texture2D.Width, texture2D.Height };
				string textValueWith = Language.GetTextValueWith("Workshop.ReportIssue_FailedToPublish_ImageSizeIsTooLarge", obj);
				if (SocialAPI.Workshop != null)
				{
					SocialAPI.Workshop.IssueReporter.ReportInstantUploadProblemFromValue(textValueWith);
				}
				this._previewImagePath = null;
				this._previewImagePathPlate.SetContents(null);
				this._previewImageUIElement.SetImage(this._defaultPreviewImageTexture);
				return;
			}
			if (this._previewImageTransientTexture != null)
			{
				this._previewImageTransientTexture.Dispose();
				this._previewImageTransientTexture = null;
			}
			if (texture2D != null)
			{
				this._previewImageUIElement.SetImage(texture2D);
				this._previewImageTransientTexture = texture2D;
				return;
			}
			this._previewImageUIElement.SetImage(this._defaultPreviewImageTexture);
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x00586DCC File Offset: 0x00584FCC
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x00586DDC File Offset: 0x00584FDC
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int num2 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			this._helper.RemovePointsOutOfView(snapPoints, this._listContainer, spriteBatch);
			UILinkPoint linkPoint = this._helper.GetLinkPoint(num2++, this._backButton);
			UILinkPoint linkPoint2 = this._helper.GetLinkPoint(num2++, this._publishButton);
			SnapPoint snapPoint = null;
			SnapPoint snapPoint2 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint3 = snapPoints[i];
				string name = snapPoint3.Name;
				if (!(name == "disclaimer"))
				{
					if (name == "image")
					{
						snapPoint2 = snapPoint3;
					}
				}
				else
				{
					snapPoint = snapPoint3;
				}
			}
			UILinkPoint uilinkPoint = this._helper.TryMakeLinkPoint(ref num2, snapPoint);
			UILinkPoint uilinkPoint2 = this._helper.TryMakeLinkPoint(ref num2, snapPoint2);
			this._helper.PairLeftRight(linkPoint, linkPoint2);
			this._helper.PairUpDown(uilinkPoint, uilinkPoint2);
			UILinkPoint[] array = this._helper.CreateUILinkStripHorizontal(ref num2, snapPoints.Where((SnapPoint x) => x.Name == "public").ToList<SnapPoint>());
			if (array.Length != 0)
			{
				this._helper.LinkHorizontalStripUpSideToSingle(array, uilinkPoint2);
			}
			UILinkPoint uilinkPoint3 = ((array.Length != 0) ? array[0] : null);
			UILinkPoint uilinkPoint4 = linkPoint;
			List<SnapPoint> list = snapPoints.Where((SnapPoint x) => x.Name == "tags").ToList<SnapPoint>();
			UILinkPoint[,] array2 = this._helper.CreateUILinkPointGrid(ref num2, list, 3, uilinkPoint3, null, null, uilinkPoint4);
			int num3 = array2.GetLength(1) - 1;
			if (num3 >= 0)
			{
				this._helper.LinkHorizontalStripBottomSideToSingle(array, array2[0, 0]);
				for (int j = array2.GetLength(0) - 1; j >= 0; j--)
				{
					if (array2[j, num3] != null)
					{
						this._helper.PairUpDown(array2[j, num3], linkPoint2);
						break;
					}
				}
			}
			UILinkPoint uilinkPoint5 = UILinkPointNavigator.Points[num2 - 1];
			this._helper.PairUpDown(uilinkPoint5, linkPoint);
			this._helper.MoveToVisuallyClosestPoint(num, num2);
		}

		// Token: 0x04005320 RID: 21280
		protected UIState _previousUIState;

		// Token: 0x04005321 RID: 21281
		protected TPublishedObjectType _dataObject;

		// Token: 0x04005322 RID: 21282
		protected string _publishedObjectNameDescriptorTexKey;

		// Token: 0x04005323 RID: 21283
		protected string _instructionsTextKey;

		// Token: 0x04005324 RID: 21284
		private UIElement _uiListContainer;

		// Token: 0x04005325 RID: 21285
		private UIElement _uiListRect;

		// Token: 0x04005326 RID: 21286
		private UIScrollbar _scrollbar;

		// Token: 0x04005327 RID: 21287
		private bool _isScrollbarAttached;

		// Token: 0x04005328 RID: 21288
		private UIText _descriptionText;

		// Token: 0x04005329 RID: 21289
		private UIElement _listContainer;

		// Token: 0x0400532A RID: 21290
		private UIElement _backButton;

		// Token: 0x0400532B RID: 21291
		private UIElement _publishButton;

		// Token: 0x0400532C RID: 21292
		private WorkshopItemPublicSettingId _optionPublicity = WorkshopItemPublicSettingId.Public;

		// Token: 0x0400532D RID: 21293
		private GroupOptionButton<WorkshopItemPublicSettingId>[] _publicityOptions;

		// Token: 0x0400532E RID: 21294
		private List<GroupOptionButton<WorkshopTagOption>> _tagOptions;

		// Token: 0x0400532F RID: 21295
		private UICharacterNameButton _previewImagePathPlate;

		// Token: 0x04005330 RID: 21296
		private Texture2D _previewImageTransientTexture;

		// Token: 0x04005331 RID: 21297
		private UIImage _previewImageUIElement;

		// Token: 0x04005332 RID: 21298
		private string _previewImagePath;

		// Token: 0x04005333 RID: 21299
		private Asset<Texture2D> _defaultPreviewImageTexture;

		// Token: 0x04005334 RID: 21300
		private UIElement _steamDisclaimerButton;

		// Token: 0x04005335 RID: 21301
		private UIText _disclaimerText;

		// Token: 0x04005336 RID: 21302
		private UIGamepadHelper _helper;

		// Token: 0x020008F7 RID: 2295
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004728 RID: 18216 RVA: 0x006CB358 File Offset: 0x006C9558
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004729 RID: 18217 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600472A RID: 18218 RVA: 0x006CB364 File Offset: 0x006C9564
			internal bool <GetPublishSettings>b__58_0(GroupOptionButton<WorkshopTagOption> x)
			{
				return x.IsSelected;
			}

			// Token: 0x0600472B RID: 18219 RVA: 0x006CB36C File Offset: 0x006C956C
			internal WorkshopTagOption <GetPublishSettings>b__58_1(GroupOptionButton<WorkshopTagOption> x)
			{
				return x.OptionValue;
			}

			// Token: 0x0600472C RID: 18220 RVA: 0x006CB374 File Offset: 0x006C9574
			internal bool <SetupGamepadPoints>b__64_0(SnapPoint x)
			{
				return x.Name == "public";
			}

			// Token: 0x0600472D RID: 18221 RVA: 0x006CB386 File Offset: 0x006C9586
			internal bool <SetupGamepadPoints>b__64_1(SnapPoint x)
			{
				return x.Name == "tags";
			}

			// Token: 0x04007401 RID: 29697
			public static readonly AWorkshopPublishInfoState<TPublishedObjectType>.<>c <>9 = new AWorkshopPublishInfoState<TPublishedObjectType>.<>c();

			// Token: 0x04007402 RID: 29698
			public static Func<GroupOptionButton<WorkshopTagOption>, bool> <>9__58_0;

			// Token: 0x04007403 RID: 29699
			public static Func<GroupOptionButton<WorkshopTagOption>, WorkshopTagOption> <>9__58_1;

			// Token: 0x04007404 RID: 29700
			public static Func<SnapPoint, bool> <>9__64_0;

			// Token: 0x04007405 RID: 29701
			public static Func<SnapPoint, bool> <>9__64_1;
		}
	}
}
