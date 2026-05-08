using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003AA RID: 938
	public class UICreativePowersMenu : UIState
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x0058BEB0 File Offset: 0x0058A0B0
		public bool IsShowingResearchMenu
		{
			get
			{
				return this._mainCategory.CurrentOption == 2;
			}
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x0058BEC0 File Offset: 0x0058A0C0
		public override void OnActivate()
		{
			this.InitializePage();
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0058BEC8 File Offset: 0x0058A0C8
		private void InitializePage()
		{
			int num = 270;
			int num2 = 20;
			this._container = new UIElement
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension((float)(-(float)num - num2), 1f),
				Top = new StyleDimension((float)num, 0f)
			};
			base.Append(this._container);
			List<UIElement> list = this.CreateMainPowerStrip();
			PowerStripUIElement powerStripUIElement = new PowerStripUIElement("strip 0", list)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(20f, 0f)
			};
			powerStripUIElement.OnMouseOver += this.strip_OnMouseOver;
			powerStripUIElement.OnMouseOut += this.strip_OnMouseOut;
			this._mainPowerStrip = powerStripUIElement;
			List<UIElement> list2 = this.CreateTimePowerStrip();
			PowerStripUIElement powerStripUIElement2 = new PowerStripUIElement("strip 1", list2)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(80f, 0f)
			};
			powerStripUIElement2.OnMouseOver += this.strip_OnMouseOver;
			powerStripUIElement2.OnMouseOut += this.strip_OnMouseOut;
			this._timePowersStrip = powerStripUIElement2;
			List<UIElement> list3 = this.CreateWeatherPowerStrip();
			PowerStripUIElement powerStripUIElement3 = new PowerStripUIElement("strip 1", list3)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(80f, 0f)
			};
			powerStripUIElement3.OnMouseOver += this.strip_OnMouseOver;
			powerStripUIElement3.OnMouseOut += this.strip_OnMouseOut;
			this._weatherPowersStrip = powerStripUIElement3;
			List<UIElement> list4 = this.CreatePersonalPowerStrip();
			PowerStripUIElement powerStripUIElement4 = new PowerStripUIElement("strip 1", list4)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(80f, 0f)
			};
			powerStripUIElement4.OnMouseOver += this.strip_OnMouseOver;
			powerStripUIElement4.OnMouseOut += this.strip_OnMouseOut;
			this._personalPowersStrip = powerStripUIElement4;
			this._infiniteItemsWindow = new UICreativeInfiniteItemsDisplay
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(80f, 0f),
				Width = new StyleDimension(480f, 0f),
				Height = new StyleDimension(-88f, 1f)
			};
			this.RefreshElementsOrder();
			base.OnUpdate += this.UICreativePowersMenu_OnUpdate;
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x0058C178 File Offset: 0x0058A378
		private List<UIElement> CreateMainPowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory> mainCategory = this._mainCategory;
			mainCategory.Buttons.Clear();
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo creativePowerUIElementRequestInfo = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			GroupOptionButton<int> groupOptionButton = CreativePowersHelper.CreateCategoryButton<int>(creativePowerUIElementRequestInfo, 1, 0);
			groupOptionButton.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.ItemDuplication));
			groupOptionButton.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton.OnUpdate += this.itemsWindowButton_OnUpdate;
			mainCategory.Buttons.Add(1, groupOptionButton);
			list.Add(groupOptionButton);
			this._infiniteItemsButton = groupOptionButton;
			GroupOptionButton<int> groupOptionButton2 = CreativePowersHelper.CreateCategoryButton<int>(creativePowerUIElementRequestInfo, 2, 0);
			groupOptionButton2.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.ItemResearch));
			groupOptionButton2.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton2.OnUpdate += this.researchWindowButton_OnUpdate;
			mainCategory.Buttons.Add(2, groupOptionButton2);
			list.Add(groupOptionButton2);
			GroupOptionButton<int> groupOptionButton3 = CreativePowersHelper.CreateCategoryButton<int>(creativePowerUIElementRequestInfo, 3, 0);
			groupOptionButton3.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.TimeCategory));
			groupOptionButton3.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton3.OnUpdate += this.timeCategoryButton_OnUpdate;
			mainCategory.Buttons.Add(3, groupOptionButton3);
			list.Add(groupOptionButton3);
			GroupOptionButton<int> groupOptionButton4 = CreativePowersHelper.CreateCategoryButton<int>(creativePowerUIElementRequestInfo, 4, 0);
			groupOptionButton4.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.WeatherCategory));
			groupOptionButton4.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton4.OnUpdate += this.weatherCategoryButton_OnUpdate;
			mainCategory.Buttons.Add(4, groupOptionButton4);
			list.Add(groupOptionButton4);
			GroupOptionButton<int> groupOptionButton5 = CreativePowersHelper.CreateCategoryButton<int>(creativePowerUIElementRequestInfo, 6, 0);
			groupOptionButton5.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.PersonalCategory));
			groupOptionButton5.OnLeftClick += this.MainCategoryButtonClick;
			groupOptionButton5.OnUpdate += this.personalCategoryButton_OnUpdate;
			mainCategory.Buttons.Add(6, groupOptionButton5);
			list.Add(groupOptionButton5);
			CreativePowerManager.Instance.GetPower<CreativePowers.StopBiomeSpreadPower>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			GroupOptionButton<int> groupOptionButton6 = this.CreateSubcategoryButton<CreativePowers.DifficultySliderPower>(ref creativePowerUIElementRequestInfo, 1, "strip 1", 5, 0, mainCategory.Buttons, mainCategory.Sliders);
			groupOptionButton6.OnLeftClick += this.MainCategoryButtonClick;
			list.Add(groupOptionButton6);
			return list;
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x0058C3BC File Offset: 0x0058A5BC
		private static void CategoryButton_OnUpdate_DisplayTooltips(UIElement affectedElement, string categoryNameKey)
		{
			GroupOptionButton<int> groupOptionButton = affectedElement as GroupOptionButton<int>;
			if (affectedElement.IsMouseHovering)
			{
				string textValue = Language.GetTextValue(groupOptionButton.IsSelected ? (categoryNameKey + "Opened") : (categoryNameKey + "Closed"));
				CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, categoryNameKey);
				Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x0058C418 File Offset: 0x0058A618
		private void itemsWindowButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.InfiniteItemsCategory");
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x0058C425 File Offset: 0x0058A625
		private void researchWindowButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.ResearchItemsCategory");
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0058C432 File Offset: 0x0058A632
		private void timeCategoryButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.TimeCategory");
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0058C43F File Offset: 0x0058A63F
		private void weatherCategoryButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.WeatherCategory");
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x0058C44C File Offset: 0x0058A64C
		private void personalCategoryButton_OnUpdate(UIElement affectedElement)
		{
			UICreativePowersMenu.CategoryButton_OnUpdate_DisplayTooltips(affectedElement, "CreativePowers.PersonalCategory");
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0058C459 File Offset: 0x0058A659
		private void UICreativePowersMenu_OnUpdate(UIElement affectedElement)
		{
			if (this._hovered)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x0058C46E File Offset: 0x0058A66E
		private void strip_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = false;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x0058C477 File Offset: 0x0058A677
		private void strip_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = true;
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0058C480 File Offset: 0x0058A680
		private void MainCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			this.ToggleMainCategory(groupOptionButton.OptionValue);
			this.RefreshElementsOrder();
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0058C4A6 File Offset: 0x0058A6A6
		private void ToggleMainCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.OpenMainSubCategory>(this._mainCategory, option, UICreativePowersMenu.OpenMainSubCategory.None);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x0058C4B6 File Offset: 0x0058A6B6
		private void ToggleWeatherCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.WeatherSubcategory>(this._weatherCategory, option, UICreativePowersMenu.WeatherSubcategory.None);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0058C4C6 File Offset: 0x0058A6C6
		private void ToggleTimeCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.TimeSubcategory>(this._timeCategory, option, UICreativePowersMenu.TimeSubcategory.None);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x0058C4D6 File Offset: 0x0058A6D6
		private void TogglePersonalCategory(int option)
		{
			this.ToggleCategory<UICreativePowersMenu.PersonalSubcategory>(this._personalCategory, option, UICreativePowersMenu.PersonalSubcategory.None);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0058C4E6 File Offset: 0x0058A6E6
		public void SacrificeWhatsInResearchMenu()
		{
			this._infiniteItemsWindow.SacrificeWhatYouCan();
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0058C4F3 File Offset: 0x0058A6F3
		public void StopPlayingResearchAnimations()
		{
			this._infiniteItemsWindow.StopPlayingAnimation();
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0058C500 File Offset: 0x0058A700
		private void ToggleCategory<TEnum>(UICreativePowersMenu.MenuTree<TEnum> tree, int option, TEnum defaultOption) where TEnum : struct, IConvertible
		{
			if (tree.CurrentOption == option)
			{
				option = defaultOption.ToInt32(null);
			}
			tree.CurrentOption = option;
			foreach (GroupOptionButton<int> groupOptionButton in tree.Buttons.Values)
			{
				groupOptionButton.SetCurrentOption(option);
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x0058C578 File Offset: 0x0058A778
		private List<UIElement> CreateTimePowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory> timeCategory = this._timeCategory;
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo creativePowerUIElementRequestInfo = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartDayImmediately>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartNoonImmediately>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartNightImmediately>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.StartMidnightImmediately>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			GroupOptionButton<int> groupOptionButton = this.CreateSubcategoryButton<CreativePowers.ModifyTimeRate>(ref creativePowerUIElementRequestInfo, 2, "strip 2", 1, 0, timeCategory.Buttons, timeCategory.Sliders);
			groupOptionButton.OnLeftClick += this.TimeCategoryButtonClick;
			list.Add(groupOptionButton);
			return list;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0058C63C File Offset: 0x0058A83C
		private List<UIElement> CreatePersonalPowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory> personalCategory = this._personalCategory;
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo creativePowerUIElementRequestInfo = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			CreativePowerManager.Instance.GetPower<CreativePowers.GodmodePower>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			CreativePowerManager.Instance.GetPower<CreativePowers.FarPlacementRangePower>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			GroupOptionButton<int> groupOptionButton = this.CreateSubcategoryButton<CreativePowers.SpawnRateSliderPerPlayerPower>(ref creativePowerUIElementRequestInfo, 2, "strip 2", 1, 0, personalCategory.Buttons, personalCategory.Sliders);
			groupOptionButton.OnLeftClick += this.PersonalCategoryButtonClick;
			list.Add(groupOptionButton);
			return list;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0058C6CC File Offset: 0x0058A8CC
		private List<UIElement> CreateWeatherPowerStrip()
		{
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory> weatherCategory = this._weatherCategory;
			List<UIElement> list = new List<UIElement>();
			CreativePowerUIElementRequestInfo creativePowerUIElementRequestInfo = new CreativePowerUIElementRequestInfo
			{
				PreferredButtonWidth = 40,
				PreferredButtonHeight = 40
			};
			GroupOptionButton<int> groupOptionButton = this.CreateSubcategoryButton<CreativePowers.ModifyWindDirectionAndStrength>(ref creativePowerUIElementRequestInfo, 2, "strip 2", 1, 0, weatherCategory.Buttons, weatherCategory.Sliders);
			groupOptionButton.OnLeftClick += this.WeatherCategoryButtonClick;
			list.Add(groupOptionButton);
			CreativePowerManager.Instance.GetPower<CreativePowers.FreezeWindDirectionAndStrength>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			GroupOptionButton<int> groupOptionButton2 = this.CreateSubcategoryButton<CreativePowers.ModifyRainPower>(ref creativePowerUIElementRequestInfo, 2, "strip 2", 2, 0, weatherCategory.Buttons, weatherCategory.Sliders);
			groupOptionButton2.OnLeftClick += this.WeatherCategoryButtonClick;
			list.Add(groupOptionButton2);
			CreativePowerManager.Instance.GetPower<CreativePowers.FreezeRainPower>().ProvidePowerButtons(creativePowerUIElementRequestInfo, list);
			return list;
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x0058C798 File Offset: 0x0058A998
		private GroupOptionButton<int> CreateSubcategoryButton<T>(ref CreativePowerUIElementRequestInfo request, int subcategoryDepth, string subcategoryName, int subcategoryIndex, int currentSelectedInSubcategory, Dictionary<int, GroupOptionButton<int>> subcategoryButtons, Dictionary<int, UIElement> slidersSet) where T : ICreativePower, IProvideSliderElement, IPowerSubcategoryElement
		{
			T power = CreativePowerManager.Instance.GetPower<T>();
			UIElement uielement = power.ProvideSlider();
			uielement.Left = new StyleDimension((float)(20 + subcategoryDepth * 60), 0f);
			slidersSet[subcategoryIndex] = uielement;
			uielement.SetSnapPoint(subcategoryName, 0, new Vector2?(new Vector2(0f, 0.5f)), new Vector2?(new Vector2(28f, 0f)));
			GroupOptionButton<int> optionButton = power.GetOptionButton(request, subcategoryIndex, currentSelectedInSubcategory);
			subcategoryButtons[subcategoryIndex] = optionButton;
			CreativePowersHelper.UpdateUnlockStateByPower(power, optionButton, CreativePowersHelper.CommonSelectedColor);
			return optionButton;
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x0058C844 File Offset: 0x0058AA44
		private void WeatherCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			int optionValue = groupOptionButton.OptionValue;
			if (optionValue != 1)
			{
				if (optionValue == 2 && !CreativePowerManager.Instance.GetPower<CreativePowers.ModifyRainPower>().GetIsUnlocked())
				{
					return;
				}
			}
			else if (!CreativePowerManager.Instance.GetPower<CreativePowers.ModifyWindDirectionAndStrength>().GetIsUnlocked())
			{
				return;
			}
			this.ToggleWeatherCategory(groupOptionButton.OptionValue);
			this.RefreshElementsOrder();
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x0058C8A0 File Offset: 0x0058AAA0
		private void TimeCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			int optionValue = groupOptionButton.OptionValue;
			if (optionValue == 1 && !CreativePowerManager.Instance.GetPower<CreativePowers.ModifyTimeRate>().GetIsUnlocked())
			{
				return;
			}
			this.ToggleTimeCategory(groupOptionButton.OptionValue);
			this.RefreshElementsOrder();
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x0058C8E4 File Offset: 0x0058AAE4
		private void PersonalCategoryButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<int> groupOptionButton = listeningElement as GroupOptionButton<int>;
			int optionValue = groupOptionButton.OptionValue;
			if (optionValue == 1 && !CreativePowerManager.Instance.GetPower<CreativePowers.SpawnRateSliderPerPlayerPower>().GetIsUnlocked())
			{
				return;
			}
			this.TogglePersonalCategory(groupOptionButton.OptionValue);
			this.RefreshElementsOrder();
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0058C928 File Offset: 0x0058AB28
		private void RefreshElementsOrder()
		{
			this._container.RemoveAllChildren();
			this._container.Append(this._mainPowerStrip);
			UIElement uielement = null;
			UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory> mainCategory = this._mainCategory;
			if (mainCategory.Sliders.TryGetValue(mainCategory.CurrentOption, out uielement))
			{
				this._container.Append(uielement);
			}
			if (mainCategory.CurrentOption == 1)
			{
				Main.LocalPlayerCreativeTracker.ItemSacrifices.DismissNewlyUnlockedFromTeamMatesIcon();
				this._infiniteItemsWindow.SetPageTypeToShow(UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage.InfiniteItemsPickup);
				this._container.Append(this._infiniteItemsWindow);
			}
			if (mainCategory.CurrentOption == 2)
			{
				this._infiniteItemsWindow.SetPageTypeToShow(UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage.InfiniteItemsResearch);
				this._container.Append(this._infiniteItemsWindow);
			}
			if (mainCategory.CurrentOption == 3)
			{
				this._container.Append(this._timePowersStrip);
				UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory> timeCategory = this._timeCategory;
				if (timeCategory.Sliders.TryGetValue(timeCategory.CurrentOption, out uielement))
				{
					this._container.Append(uielement);
				}
			}
			if (mainCategory.CurrentOption == 4)
			{
				this._container.Append(this._weatherPowersStrip);
				UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory> weatherCategory = this._weatherCategory;
				if (weatherCategory.Sliders.TryGetValue(weatherCategory.CurrentOption, out uielement))
				{
					this._container.Append(uielement);
				}
			}
			if (mainCategory.CurrentOption == 6)
			{
				this._container.Append(this._personalPowersStrip);
				UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory> personalCategory = this._personalCategory;
				if (personalCategory.Sliders.TryGetValue(personalCategory.CurrentOption, out uielement))
				{
					this._container.Append(uielement);
				}
			}
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0058CAA0 File Offset: 0x0058ACA0
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (Main.LocalPlayerCreativeTracker.ItemSacrifices.AnyNewUnlocksFromTeammates)
			{
				Rectangle rectangle = this._infiniteItemsButton.GetDimensions().ToRectangle();
				Utils.DrawNotificationIcon(spriteBatch, rectangle, 1f, false);
			}
			this.SetupGamepadPoints();
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0058CAEC File Offset: 0x0058ACEC
		private void SetupGamepadPoints()
		{
			int num = 10000;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			List<SnapPoint> orderedPointsByCategoryName = this._helper.GetOrderedPointsByCategoryName(snapPoints, "strip 0");
			List<SnapPoint> orderedPointsByCategoryName2 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "strip 1");
			List<SnapPoint> orderedPointsByCategoryName3 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "strip 2");
			UILinkPoint[] array = null;
			UILinkPoint[] array2 = null;
			UILinkPoint[] array3 = null;
			if (orderedPointsByCategoryName.Count > 0)
			{
				array = this._helper.CreateUILinkStripVertical(ref num, orderedPointsByCategoryName);
			}
			if (orderedPointsByCategoryName2.Count > 0)
			{
				array2 = this._helper.CreateUILinkStripVertical(ref num, orderedPointsByCategoryName2);
			}
			if (orderedPointsByCategoryName3.Count > 0)
			{
				array3 = this._helper.CreateUILinkStripVertical(ref num, orderedPointsByCategoryName3);
			}
			if (array != null && array2 != null)
			{
				this._helper.LinkVerticalStrips(array, array2, (array.Length - array2.Length) / 2);
			}
			if (array2 != null && array3 != null)
			{
				this._helper.LinkVerticalStrips(array2, array3, (array.Length - array2.Length) / 2);
			}
			UILinkPoint uilinkPoint = null;
			UILinkPoint uilinkPoint2 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (!(name == "CreativeSacrificeConfirm"))
				{
					if (name == "CreativeInfinitesSearch")
					{
						uilinkPoint2 = this._helper.MakeLinkPointFromSnapPoint(num++, snapPoint);
					}
				}
				else
				{
					uilinkPoint = this._helper.MakeLinkPointFromSnapPoint(num++, snapPoint);
				}
			}
			UILinkPoint uilinkPoint3 = UILinkPointNavigator.Points[15000];
			List<SnapPoint> orderedPointsByCategoryName4 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "CreativeInfinitesFilter");
			if (orderedPointsByCategoryName4.Count > 0)
			{
				UILinkPoint[] array4 = this._helper.CreateUILinkStripHorizontal(ref num, orderedPointsByCategoryName4);
				if (uilinkPoint2 != null)
				{
					uilinkPoint2.Up = array4[0].ID;
					for (int j = 0; j < array4.Length; j++)
					{
						array4[j].Down = uilinkPoint2.ID;
					}
				}
			}
			List<SnapPoint> orderedPointsByCategoryName5 = this._helper.GetOrderedPointsByCategoryName(snapPoints, "DynamicItemCollectionSlot");
			UILinkPoint[,] array5 = null;
			if (orderedPointsByCategoryName5.Count > 0)
			{
				array5 = this._helper.CreateUILinkPointGrid(ref num, orderedPointsByCategoryName5, this._infiniteItemsWindow.GetItemsPerLine(), uilinkPoint2, array[0], null, null);
				this._helper.LinkVerticalStripRightSideToSingle(array, array5[0, 0]);
			}
			else if (uilinkPoint2 != null)
			{
				this._helper.LinkVerticalStripRightSideToSingle(array, uilinkPoint2);
			}
			if (uilinkPoint2 != null && array5 != null)
			{
				this._helper.PairUpDown(uilinkPoint2, array5[0, 0]);
			}
			if (uilinkPoint3 != null && this.IsShowingResearchMenu)
			{
				this._helper.LinkVerticalStripRightSideToSingle(array, uilinkPoint3);
			}
			if (uilinkPoint != null)
			{
				this._helper.PairUpDown(uilinkPoint3, uilinkPoint);
				uilinkPoint.Left = array[0].ID;
			}
			if (Main.CreativeMenu.GamepadMoveToSearchButtonHack)
			{
				Main.CreativeMenu.GamepadMoveToSearchButtonHack = false;
				if (uilinkPoint2 != null)
				{
					UILinkPointNavigator.ChangePoint(uilinkPoint2.ID);
				}
			}
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x0058CDBC File Offset: 0x0058AFBC
		public UICreativePowersMenu()
		{
		}

		// Token: 0x0400537B RID: 21371
		private bool _hovered;

		// Token: 0x0400537C RID: 21372
		private PowerStripUIElement _mainPowerStrip;

		// Token: 0x0400537D RID: 21373
		private PowerStripUIElement _timePowersStrip;

		// Token: 0x0400537E RID: 21374
		private PowerStripUIElement _weatherPowersStrip;

		// Token: 0x0400537F RID: 21375
		private PowerStripUIElement _personalPowersStrip;

		// Token: 0x04005380 RID: 21376
		private UICreativeInfiniteItemsDisplay _infiniteItemsWindow;

		// Token: 0x04005381 RID: 21377
		private UIElement _infiniteItemsButton;

		// Token: 0x04005382 RID: 21378
		private UIElement _container;

		// Token: 0x04005383 RID: 21379
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory> _mainCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.OpenMainSubCategory>(UICreativePowersMenu.OpenMainSubCategory.None);

		// Token: 0x04005384 RID: 21380
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory> _weatherCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.WeatherSubcategory>(UICreativePowersMenu.WeatherSubcategory.None);

		// Token: 0x04005385 RID: 21381
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory> _timeCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.TimeSubcategory>(UICreativePowersMenu.TimeSubcategory.None);

		// Token: 0x04005386 RID: 21382
		private UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory> _personalCategory = new UICreativePowersMenu.MenuTree<UICreativePowersMenu.PersonalSubcategory>(UICreativePowersMenu.PersonalSubcategory.None);

		// Token: 0x04005387 RID: 21383
		private const int INITIAL_LEFT_PIXELS = 20;

		// Token: 0x04005388 RID: 21384
		private const int LEFT_PIXELS_PER_STRIP_DEPTH = 60;

		// Token: 0x04005389 RID: 21385
		private const string STRIP_MAIN = "strip 0";

		// Token: 0x0400538A RID: 21386
		private const string STRIP_DEPTH_1 = "strip 1";

		// Token: 0x0400538B RID: 21387
		private const string STRIP_DEPTH_2 = "strip 2";

		// Token: 0x0400538C RID: 21388
		private UIGamepadHelper _helper;

		// Token: 0x020008FF RID: 2303
		private class MenuTree<TEnum> where TEnum : struct, IConvertible
		{
			// Token: 0x0600474C RID: 18252 RVA: 0x006CB466 File Offset: 0x006C9666
			public MenuTree(TEnum defaultValue)
			{
				this.CurrentOption = defaultValue.ToInt32(null);
			}

			// Token: 0x0400741C RID: 29724
			public int CurrentOption;

			// Token: 0x0400741D RID: 29725
			public Dictionary<int, GroupOptionButton<int>> Buttons = new Dictionary<int, GroupOptionButton<int>>();

			// Token: 0x0400741E RID: 29726
			public Dictionary<int, UIElement> Sliders = new Dictionary<int, UIElement>();
		}

		// Token: 0x02000900 RID: 2304
		private enum OpenMainSubCategory
		{
			// Token: 0x04007420 RID: 29728
			None,
			// Token: 0x04007421 RID: 29729
			InfiniteItems,
			// Token: 0x04007422 RID: 29730
			ResearchWindow,
			// Token: 0x04007423 RID: 29731
			Time,
			// Token: 0x04007424 RID: 29732
			Weather,
			// Token: 0x04007425 RID: 29733
			EnemyStrengthSlider,
			// Token: 0x04007426 RID: 29734
			PersonalPowers
		}

		// Token: 0x02000901 RID: 2305
		private enum WeatherSubcategory
		{
			// Token: 0x04007428 RID: 29736
			None,
			// Token: 0x04007429 RID: 29737
			WindSlider,
			// Token: 0x0400742A RID: 29738
			RainSlider
		}

		// Token: 0x02000902 RID: 2306
		private enum TimeSubcategory
		{
			// Token: 0x0400742C RID: 29740
			None,
			// Token: 0x0400742D RID: 29741
			TimeRate
		}

		// Token: 0x02000903 RID: 2307
		private enum PersonalSubcategory
		{
			// Token: 0x0400742F RID: 29743
			None,
			// Token: 0x04007430 RID: 29744
			EnemySpawnRateSlider
		}
	}
}
