using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003B0 RID: 944
	public class UIResourcePackSelectionMenu : UIState, IHaveBackButtonCommand
	{
		// Token: 0x06002C3F RID: 11327 RVA: 0x00596E34 File Offset: 0x00595034
		public UIResourcePackSelectionMenu(UIState uiStateToGoBackTo, AssetSourceController sourceController, ResourcePackList currentResourcePackList)
		{
			this._sourceController = sourceController;
			this._uiStateToGoBackTo = uiStateToGoBackTo;
			this.BuildPage();
			this._packsList = currentResourcePackList;
			this.PopulatePackList();
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x00596E60 File Offset: 0x00595060
		private void PopulatePackList()
		{
			this._availablePacksList.Clear();
			this._enabledPacksList.Clear();
			this.CleanUpResourcePackPriority();
			IEnumerable<ResourcePack> enabledPacks = this._packsList.EnabledPacks;
			IEnumerable<ResourcePack> disabledPacks = this._packsList.DisabledPacks;
			int num = 0;
			foreach (ResourcePack resourcePack in disabledPacks)
			{
				UIResourcePack uiresourcePack = new UIResourcePack(resourcePack, num)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f)
				};
				UIElement uielement = this.CreatePackToggleButton(resourcePack);
				uielement.OnUpdate += this.EnablePackUpdate;
				uielement.SetSnapPoint("ToggleToOn", num, null, null);
				uiresourcePack.ContentPanel.Append(uielement);
				uielement = this.CreatePackInfoButton(resourcePack);
				uielement.OnUpdate += this.SeeInfoUpdate;
				uielement.SetSnapPoint("InfoOff", num, null, null);
				uiresourcePack.ContentPanel.Append(uielement);
				this._availablePacksList.Add(uiresourcePack);
				num++;
			}
			num = 0;
			foreach (ResourcePack resourcePack2 in enabledPacks)
			{
				UIResourcePack uiresourcePack2 = new UIResourcePack(resourcePack2, num)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f)
				};
				if (resourcePack2.IsEnabled)
				{
					UIElement uielement2 = this.CreatePackToggleButton(resourcePack2);
					uielement2.Left = new StyleDimension(0f, 0f);
					uielement2.Width = new StyleDimension(0f, 0.5f);
					uielement2.OnUpdate += this.DisablePackUpdate;
					uielement2.SetSnapPoint("ToggleToOff", num, null, null);
					uiresourcePack2.ContentPanel.Append(uielement2);
					uielement2 = this.CreatePackInfoButton(resourcePack2);
					uielement2.OnUpdate += this.SeeInfoUpdate;
					uielement2.Left = new StyleDimension(0f, 0.5f);
					uielement2.Width = new StyleDimension(0f, 0.16666667f);
					uielement2.SetSnapPoint("InfoOn", num, null, null);
					uiresourcePack2.ContentPanel.Append(uielement2);
					uielement2 = this.CreateOffsetButton(resourcePack2, -1);
					uielement2.Left = new StyleDimension(0f, 0.6666667f);
					uielement2.Width = new StyleDimension(0f, 0.16666667f);
					uielement2.SetSnapPoint("OrderUp", num, null, null);
					uiresourcePack2.ContentPanel.Append(uielement2);
					uielement2 = this.CreateOffsetButton(resourcePack2, 1);
					uielement2.Left = new StyleDimension(0f, 0.8333334f);
					uielement2.Width = new StyleDimension(0f, 0.16666667f);
					uielement2.SetSnapPoint("OrderDown", num, null, null);
					uiresourcePack2.ContentPanel.Append(uielement2);
				}
				this._enabledPacksList.Add(uiresourcePack2);
				num++;
			}
			this.UpdateTitles();
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x005971F0 File Offset: 0x005953F0
		private UIElement CreateOffsetButton(ResourcePack resourcePack, int offset)
		{
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 0.8f, 0.5f, 10f)
			{
				Left = StyleDimension.FromPercent(0.5f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 0.5f),
				Height = StyleDimension.Fill
			};
			bool flag = (offset == -1 && resourcePack.SortingOrder == 0) | (offset == 1 && resourcePack.SortingOrder == this._packsList.EnabledPacks.Count<ResourcePack>() - 1);
			Color lightCyan = Color.LightCyan;
			groupOptionButton.SetColorsBasedOnSelectionState(lightCyan, lightCyan, 0.7f, 0.7f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetPadding(0f);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/TexturePackButtons", 1);
			UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(2, 2, (offset == 1) ? 1 : 0, 0, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			groupOptionButton.Append(uiimageFramed);
			groupOptionButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			};
			int offsetLocalForLambda = offset;
			if (flag)
			{
				groupOptionButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				};
			}
			else
			{
				groupOptionButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					this.OffsetResourcePackPriority(resourcePack, offsetLocalForLambda);
					this.PopulatePackList();
					Main.instance.ResetAllContentBasedRenderTargets();
				};
			}
			if (offset == 1)
			{
				groupOptionButton.OnUpdate += this.OffsetFrontwardUpdate;
			}
			else
			{
				groupOptionButton.OnUpdate += this.OffsetBackwardUpdate;
			}
			return groupOptionButton;
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x005973A4 File Offset: 0x005955A4
		private UIElement CreatePackToggleButton(ResourcePack resourcePack)
		{
			Language.GetText(resourcePack.IsEnabled ? "GameUI.Enabled" : "GameUI.Disabled");
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 0.8f, 0.5f, 10f);
			groupOptionButton.Left = StyleDimension.FromPercent(0.5f);
			groupOptionButton.Width = StyleDimension.FromPixelsAndPercent(0f, 0.5f);
			groupOptionButton.Height = StyleDimension.Fill;
			groupOptionButton.SetColorsBasedOnSelectionState(Color.LightGreen, Color.PaleVioletRed, 0.7f, 0.7f);
			groupOptionButton.SetCurrentOption(resourcePack.IsEnabled);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetPadding(0f);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/TexturePackButtons", 1);
			UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(2, 2, resourcePack.IsEnabled ? 0 : 1, 1, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			groupOptionButton.Append(uiimageFramed);
			groupOptionButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			};
			groupOptionButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				resourcePack.IsEnabled = !resourcePack.IsEnabled;
				this.SetResourcePackAsTopPriority(resourcePack);
				this.PopulatePackList();
				Main.instance.ResetAllContentBasedRenderTargets();
			};
			return groupOptionButton;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x005974FC File Offset: 0x005956FC
		private void SetResourcePackAsTopPriority(ResourcePack resourcePack)
		{
			if (!resourcePack.IsEnabled)
			{
				return;
			}
			int num = -1;
			foreach (ResourcePack resourcePack2 in this._packsList.EnabledPacks)
			{
				if (num < resourcePack2.SortingOrder && resourcePack2 != resourcePack)
				{
					num = resourcePack2.SortingOrder;
				}
			}
			resourcePack.SortingOrder = num + 1;
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x00597570 File Offset: 0x00595770
		private void OffsetResourcePackPriority(ResourcePack resourcePack, int offset)
		{
			if (!resourcePack.IsEnabled)
			{
				return;
			}
			List<ResourcePack> list = this._packsList.EnabledPacks.ToList<ResourcePack>();
			int num = list.IndexOf(resourcePack);
			int num2 = Utils.Clamp<int>(num + offset, 0, list.Count - 1);
			if (num2 == num)
			{
				return;
			}
			int sortingOrder = list[num].SortingOrder;
			list[num].SortingOrder = list[num2].SortingOrder;
			list[num2].SortingOrder = sortingOrder;
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x005975E8 File Offset: 0x005957E8
		private UIElement CreatePackInfoButton(ResourcePack resourcePack)
		{
			UIResourcePackInfoButton<string> uiresourcePackInfoButton = new UIResourcePackInfoButton<string>("", 0.8f, false);
			uiresourcePackInfoButton.Width = StyleDimension.FromPixelsAndPercent(0f, 0.5f);
			uiresourcePackInfoButton.Height = StyleDimension.Fill;
			uiresourcePackInfoButton.ResourcePack = resourcePack;
			uiresourcePackInfoButton.SetPadding(0f);
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CharInfo", 1))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			uiresourcePackInfoButton.Append(uiimage);
			uiresourcePackInfoButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			};
			uiresourcePackInfoButton.OnLeftClick += this.Click_Info;
			return uiresourcePackInfoButton;
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x005976A8 File Offset: 0x005958A8
		private void Click_Info(UIMouseEvent evt, UIElement listeningElement)
		{
			UIResourcePackInfoButton<string> uiresourcePackInfoButton = listeningElement as UIResourcePackInfoButton<string>;
			if (uiresourcePackInfoButton != null)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.MenuUI.SetState(new UIResourcePackInfoMenu(this, uiresourcePackInfoButton.ResourcePack));
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x005976EA File Offset: 0x005958EA
		private void ApplyListChanges()
		{
			this._sourceController.UseResourcePacks(new ResourcePackList(this._enabledPacksList.Select((UIElement uiPack) => ((UIResourcePack)uiPack).ResourcePack)));
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x00597728 File Offset: 0x00595928
		private void CleanUpResourcePackPriority()
		{
			IEnumerable<ResourcePack> enumerable = this._packsList.EnabledPacks.OrderBy((ResourcePack pack) => pack.SortingOrder);
			int num = 0;
			foreach (ResourcePack resourcePack in enumerable)
			{
				resourcePack.SortingOrder = num++;
			}
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x005977A4 File Offset: 0x005959A4
		private void BuildPage()
		{
			base.RemoveAllChildren();
			UIElement uielement = new UIElement();
			uielement.Width.Set(0f, 0.8f);
			uielement.MaxWidth.Set(800f, 0f);
			uielement.MinWidth.Set(600f, 0f);
			uielement.Top.Set(240f, 0f);
			uielement.Height.Set(-240f, 1f);
			uielement.HAlign = 0.5f;
			base.Append(uielement);
			UIPanel uipanel = new UIPanel
			{
				Width = StyleDimension.Fill,
				Height = new StyleDimension(-110f, 1f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f,
				PaddingRight = 0f,
				PaddingLeft = 0f
			};
			uielement.Append(uipanel);
			int num = 35;
			int num2 = num;
			int num3 = 30;
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.FromPixelsAndPercent((float)(-(float)(num3 + 4 + 5)), 1f),
				VAlign = 1f
			};
			uielement2.SetPadding(0f);
			uipanel.Append(uielement2);
			UIElement uielement3 = new UIElement
			{
				Width = new StyleDimension(-20f, 0.5f),
				Height = new StyleDimension(0f, 1f),
				Left = new StyleDimension(10f, 0f)
			};
			uielement3.SetPadding(0f);
			uielement2.Append(uielement3);
			UIElement uielement4 = new UIElement
			{
				Width = new StyleDimension(-20f, 0.5f),
				Height = new StyleDimension(0f, 1f),
				Left = new StyleDimension(-10f, 0f),
				HAlign = 1f
			};
			uielement4.SetPadding(0f);
			uielement2.Append(uielement4);
			UIList uilist = new UIList
			{
				Width = new StyleDimension(-25f, 1f),
				Height = new StyleDimension(0f, 1f),
				ListPadding = 5f,
				HAlign = 1f
			};
			uielement3.Append(uilist);
			this._availablePacksList = uilist;
			UIList uilist2 = new UIList
			{
				Width = new StyleDimension(-25f, 1f),
				Height = new StyleDimension(0f, 1f),
				ListPadding = 5f,
				HAlign = 0f,
				Left = new StyleDimension(0f, 0f)
			};
			uielement4.Append(uilist2);
			this._enabledPacksList = uilist2;
			UIText uitext = new UIText(Language.GetText("UI.AvailableResourcePacksTitle"), 1f, false)
			{
				HAlign = 0f,
				Left = new StyleDimension(25f, 0f),
				Width = new StyleDimension(-25f, 0.5f),
				VAlign = 0f,
				Top = new StyleDimension(10f, 0f)
			};
			this._titleAvailable = uitext;
			uipanel.Append(uitext);
			UIText uitext2 = new UIText(Language.GetText("UI.EnabledResourcePacksTitle"), 1f, false)
			{
				HAlign = 1f,
				Left = new StyleDimension(-25f, 0f),
				Width = new StyleDimension(-25f, 0.5f),
				VAlign = 0f,
				Top = new StyleDimension(10f, 0f)
			};
			this._titleEnabled = uitext2;
			uipanel.Append(uitext2);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.ResourcePacks"), 1f, true)
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Top = new StyleDimension(-44f, 0f),
				BackgroundColor = new Color(73, 94, 171)
			};
			uitextPanel.SetPadding(13f);
			uielement.Append(uitextPanel);
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue)
			{
				Height = new StyleDimension(0f, 1f),
				HAlign = 0f,
				Left = new StyleDimension(0f, 0f)
			};
			uielement3.Append(uiscrollbar);
			this._availablePacksList.SetScrollbar(uiscrollbar);
			UIVerticalSeparator uiverticalSeparator = new UIVerticalSeparator
			{
				Height = new StyleDimension(-12f, 1f),
				HAlign = 0.5f,
				VAlign = 1f,
				Color = new Color(89, 116, 213, 255) * 0.9f
			};
			uipanel.Append(uiverticalSeparator);
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true);
			uihorizontalSeparator.Width = new StyleDimension((float)(-(float)num2), 0.5f);
			uihorizontalSeparator.VAlign = 0f;
			uihorizontalSeparator.HAlign = 0f;
			uihorizontalSeparator.Color = new Color(89, 116, 213, 255) * 0.9f;
			uihorizontalSeparator.Top = new StyleDimension((float)num3, 0f);
			uihorizontalSeparator.Left = new StyleDimension((float)num, 0f);
			UIHorizontalSeparator uihorizontalSeparator2 = new UIHorizontalSeparator(2, true);
			uihorizontalSeparator2.Width = new StyleDimension((float)(-(float)num2), 0.5f);
			uihorizontalSeparator2.VAlign = 0f;
			uihorizontalSeparator2.HAlign = 1f;
			uihorizontalSeparator2.Color = new Color(89, 116, 213, 255) * 0.9f;
			uihorizontalSeparator2.Top = new StyleDimension((float)num3, 0f);
			uihorizontalSeparator2.Left = new StyleDimension((float)(-(float)num), 0f);
			UIScrollbar uiscrollbar2 = new UIScrollbar(UIScrollbar.ColorTheme.Blue)
			{
				Height = new StyleDimension(0f, 1f),
				HAlign = 1f
			};
			uielement4.Append(uiscrollbar2);
			this._enabledPacksList.SetScrollbar(uiscrollbar2);
			this.AddBackAndFolderButtons(uielement);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x00597DB8 File Offset: 0x00595FB8
		private void UpdateTitles()
		{
			this._titleAvailable.SetText(Language.GetText("UI.AvailableResourcePacksTitle").FormatWith(new
			{
				Amount = this._availablePacksList.Count
			}));
			this._titleEnabled.SetText(Language.GetText("UI.EnabledResourcePacksTitle").FormatWith(new
			{
				Amount = this._enabledPacksList.Count
			}));
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x00597E1C File Offset: 0x0059601C
		private void AddBackAndFolderButtons(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true)
			{
				Width = new StyleDimension(-8f, 0.5f),
				Height = new StyleDimension(50f, 0f),
				VAlign = 1f,
				HAlign = 0f,
				Top = new StyleDimension(-45f, 0f)
			};
			uitextPanel.OnMouseOver += UIResourcePackSelectionMenu.FadedMouseOver;
			uitextPanel.OnMouseOut += UIResourcePackSelectionMenu.FadedMouseOut;
			uitextPanel.OnLeftClick += this.GoBackClick;
			uitextPanel.SetSnapPoint("GoBack", 0, null, null);
			outerContainer.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("GameUI.OpenFileFolder"), 0.7f, true)
			{
				Width = new StyleDimension(-8f, 0.5f),
				Height = new StyleDimension(50f, 0f),
				VAlign = 1f,
				HAlign = 1f,
				Top = new StyleDimension(-45f, 0f)
			};
			uitextPanel2.OnMouseOver += UIResourcePackSelectionMenu.FadedMouseOver;
			uitextPanel2.OnMouseOut += UIResourcePackSelectionMenu.FadedMouseOut;
			uitextPanel2.OnLeftClick += this.OpenFoldersClick;
			uitextPanel2.SetSnapPoint("OpenFolder", 0, null, null);
			outerContainer.Append(uitextPanel2);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x00597FB8 File Offset: 0x005961B8
		private void OpenFoldersClick(UIMouseEvent evt, UIElement listeningElement)
		{
			JArray jarray;
			string text;
			AssetInitializer.GetResourcePacksFolderPathAndConfirmItExists(out jarray, out text);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Utils.OpenFolder(text);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x00597FE9 File Offset: 0x005961E9
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x00597FF4 File Offset: 0x005961F4
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			this.ApplyListChanges();
			Main.SaveSettings();
			if (this._uiStateToGoBackTo != null)
			{
				Main.MenuUI.SetState(this._uiStateToGoBackTo);
				return;
			}
			Main.menuMode = 0;
			IngameFancyUI.Close(true);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x00598048 File Offset: 0x00596248
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x00587919 File Offset: 0x00585B19
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x0059809D File Offset: 0x0059629D
		private void OffsetBackwardUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.OffsetTexturePackPriorityDown");
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x005980AA File Offset: 0x005962AA
		private void OffsetFrontwardUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.OffsetTexturePackPriorityUp");
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x005980B7 File Offset: 0x005962B7
		private void EnablePackUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.EnableTexturePack");
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x005980C4 File Offset: 0x005962C4
		private void DisablePackUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.DisableTexturePack");
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x005980D1 File Offset: 0x005962D1
		private void SeeInfoUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.SeeTexturePackInfo");
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x005980E0 File Offset: 0x005962E0
		private static void DisplayMouseTextIfHovered(UIElement affectedElement, string textKey)
		{
			if (affectedElement.IsMouseHovering)
			{
				string textValue = Language.GetTextValue(textKey);
				Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0059810E File Offset: 0x0059630E
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x00598120 File Offset: 0x00596320
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int num2 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			List<SnapPoint> snapPoints2 = this._availablePacksList.GetSnapPoints();
			this._helper.CullPointsOutOfElementArea(spriteBatch, snapPoints2, this._availablePacksList);
			List<SnapPoint> snapPoints3 = this._enabledPacksList.GetSnapPoints();
			this._helper.CullPointsOutOfElementArea(spriteBatch, snapPoints3, this._enabledPacksList);
			UILinkPoint[] verticalStripFromCategoryName = this._helper.GetVerticalStripFromCategoryName(ref num2, snapPoints2, "ToggleToOn");
			UILinkPoint[] verticalStripFromCategoryName2 = this._helper.GetVerticalStripFromCategoryName(ref num2, snapPoints2, "InfoOff");
			UILinkPoint[] verticalStripFromCategoryName3 = this._helper.GetVerticalStripFromCategoryName(ref num2, snapPoints3, "ToggleToOff");
			UILinkPoint[] verticalStripFromCategoryName4 = this._helper.GetVerticalStripFromCategoryName(ref num2, snapPoints3, "InfoOn");
			UILinkPoint[] verticalStripFromCategoryName5 = this._helper.GetVerticalStripFromCategoryName(ref num2, snapPoints3, "OrderUp");
			UILinkPoint[] verticalStripFromCategoryName6 = this._helper.GetVerticalStripFromCategoryName(ref num2, snapPoints3, "OrderDown");
			UILinkPoint uilinkPoint = null;
			UILinkPoint uilinkPoint2 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (!(name == "GoBack"))
				{
					if (name == "OpenFolder")
					{
						uilinkPoint2 = this._helper.MakeLinkPointFromSnapPoint(num2++, snapPoint);
					}
				}
				else
				{
					uilinkPoint = this._helper.MakeLinkPointFromSnapPoint(num2++, snapPoint);
				}
			}
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName2, verticalStripFromCategoryName, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName, verticalStripFromCategoryName3, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName3, verticalStripFromCategoryName4, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName4, verticalStripFromCategoryName5, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName5, verticalStripFromCategoryName6, 0);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName, uilinkPoint);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName2, uilinkPoint);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName5, uilinkPoint2);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName6, uilinkPoint2);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName3, uilinkPoint2);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName4, uilinkPoint2);
			this._helper.PairLeftRight(uilinkPoint, uilinkPoint2);
			this._helper.MoveToVisuallyClosestPoint(num, num2);
		}

		// Token: 0x040053E2 RID: 21474
		private readonly AssetSourceController _sourceController;

		// Token: 0x040053E3 RID: 21475
		private UIList _availablePacksList;

		// Token: 0x040053E4 RID: 21476
		private UIList _enabledPacksList;

		// Token: 0x040053E5 RID: 21477
		private ResourcePackList _packsList;

		// Token: 0x040053E6 RID: 21478
		private UIText _titleAvailable;

		// Token: 0x040053E7 RID: 21479
		private UIText _titleEnabled;

		// Token: 0x040053E8 RID: 21480
		private UIState _uiStateToGoBackTo;

		// Token: 0x040053E9 RID: 21481
		private const string _snapCategory_ToggleFromOffToOn = "ToggleToOn";

		// Token: 0x040053EA RID: 21482
		private const string _snapCategory_ToggleFromOnToOff = "ToggleToOff";

		// Token: 0x040053EB RID: 21483
		private const string _snapCategory_InfoWhenOff = "InfoOff";

		// Token: 0x040053EC RID: 21484
		private const string _snapCategory_InfoWhenOn = "InfoOn";

		// Token: 0x040053ED RID: 21485
		private const string _snapCategory_OffsetOrderUp = "OrderUp";

		// Token: 0x040053EE RID: 21486
		private const string _snapCategory_OffsetOrderDown = "OrderDown";

		// Token: 0x040053EF RID: 21487
		private const string _snapPointName_goBack = "GoBack";

		// Token: 0x040053F0 RID: 21488
		private const string _snapPointName_openFolder = "OpenFolder";

		// Token: 0x040053F1 RID: 21489
		private UIGamepadHelper _helper;

		// Token: 0x02000911 RID: 2321
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x06004766 RID: 18278 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x06004767 RID: 18279 RVA: 0x006CB57C File Offset: 0x006C977C
			internal void <CreateOffsetButton>b__2(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.<>4__this.OffsetResourcePackPriority(this.resourcePack, this.offsetLocalForLambda);
				this.<>4__this.PopulatePackList();
				Main.instance.ResetAllContentBasedRenderTargets();
			}

			// Token: 0x04007461 RID: 29793
			public UIResourcePackSelectionMenu <>4__this;

			// Token: 0x04007462 RID: 29794
			public ResourcePack resourcePack;

			// Token: 0x04007463 RID: 29795
			public int offsetLocalForLambda;
		}

		// Token: 0x02000912 RID: 2322
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004768 RID: 18280 RVA: 0x006CB5CA File Offset: 0x006C97CA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004769 RID: 18281 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600476A RID: 18282 RVA: 0x00593C8E File Offset: 0x00591E8E
			internal void <CreateOffsetButton>b__17_0(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600476B RID: 18283 RVA: 0x00593C8E File Offset: 0x00591E8E
			internal void <CreateOffsetButton>b__17_1(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600476C RID: 18284 RVA: 0x00593C8E File Offset: 0x00591E8E
			internal void <CreatePackToggleButton>b__18_0(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600476D RID: 18285 RVA: 0x00593C8E File Offset: 0x00591E8E
			internal void <CreatePackInfoButton>b__21_0(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600476E RID: 18286 RVA: 0x006CB5D6 File Offset: 0x006C97D6
			internal ResourcePack <ApplyListChanges>b__23_0(UIElement uiPack)
			{
				return ((UIResourcePack)uiPack).ResourcePack;
			}

			// Token: 0x0600476F RID: 18287 RVA: 0x00693182 File Offset: 0x00691382
			internal int <CleanUpResourcePackPriority>b__24_0(ResourcePack pack)
			{
				return pack.SortingOrder;
			}

			// Token: 0x04007464 RID: 29796
			public static readonly UIResourcePackSelectionMenu.<>c <>9 = new UIResourcePackSelectionMenu.<>c();

			// Token: 0x04007465 RID: 29797
			public static UIElement.MouseEvent <>9__17_0;

			// Token: 0x04007466 RID: 29798
			public static UIElement.MouseEvent <>9__17_1;

			// Token: 0x04007467 RID: 29799
			public static UIElement.MouseEvent <>9__18_0;

			// Token: 0x04007468 RID: 29800
			public static UIElement.MouseEvent <>9__21_0;

			// Token: 0x04007469 RID: 29801
			public static Func<UIElement, ResourcePack> <>9__23_0;

			// Token: 0x0400746A RID: 29802
			public static Func<ResourcePack, int> <>9__24_0;
		}

		// Token: 0x02000913 RID: 2323
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x06004770 RID: 18288 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x06004771 RID: 18289 RVA: 0x006CB5E4 File Offset: 0x006C97E4
			internal void <CreatePackToggleButton>b__1(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.resourcePack.IsEnabled = !this.resourcePack.IsEnabled;
				this.<>4__this.SetResourcePackAsTopPriority(this.resourcePack);
				this.<>4__this.PopulatePackList();
				Main.instance.ResetAllContentBasedRenderTargets();
			}

			// Token: 0x0400746B RID: 29803
			public ResourcePack resourcePack;

			// Token: 0x0400746C RID: 29804
			public UIResourcePackSelectionMenu <>4__this;
		}
	}
}
