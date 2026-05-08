using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003A8 RID: 936
	public class UIResourcePackInfoMenu : UIState
	{
		// Token: 0x06002B04 RID: 11012 RVA: 0x00589F20 File Offset: 0x00588120
		public UIResourcePackInfoMenu(UIResourcePackSelectionMenu parent, ResourcePack pack)
		{
			this._resourceMenu = parent;
			this._pack = pack;
			this.BuildPage();
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x00589F3C File Offset: 0x0058813C
		private void BuildPage()
		{
			base.RemoveAllChildren();
			UIElement uielement = new UIElement();
			uielement.Width.Set(0f, 0.8f);
			uielement.MaxWidth.Set(500f, 0f);
			uielement.MinWidth.Set(300f, 0f);
			uielement.Top.Set(230f, 0f);
			uielement.Height.Set(-uielement.Top.Pixels, 1f);
			uielement.HAlign = 0.5f;
			base.Append(uielement);
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set(-110f, 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uielement.Append(uipanel);
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f)
			};
			uipanel.Append(uielement2);
			UIElement uielement3 = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(52f, 0f)
			};
			uielement3.SetPadding(0f);
			uielement2.Append(uielement3);
			UIText uitext = new UIText(this._pack.Name, 0.7f, true)
			{
				TextColor = Color.Gold
			};
			uitext.HAlign = 0.5f;
			uitext.VAlign = 0f;
			uielement3.Append(uitext);
			UIText uitext2 = new UIText(Language.GetTextValue("UI.Author", this._pack.Author), 0.9f, false)
			{
				HAlign = 0f,
				VAlign = 1f
			};
			uitext2.Top.Set(-6f, 0f);
			uielement3.Append(uitext2);
			UIText uitext3 = new UIText(Language.GetTextValue("UI.Version", this._pack.Version.GetFormattedVersion()), 0.9f, false)
			{
				HAlign = 1f,
				VAlign = 1f,
				TextColor = Color.Silver
			};
			uitext3.Top.Set(-6f, 0f);
			uielement3.Append(uitext3);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			UIImage uiimage = new UIImage(asset)
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixels((float)asset.Height()),
				ScaleToFit = true
			};
			uiimage.Top.Set(52f, 0f);
			uiimage.SetPadding(6f);
			uielement2.Append(uiimage);
			UIElement uielement4 = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-74f, 1f)
			};
			uielement2.Append(uielement4);
			this._container = uielement4;
			UIText uitext4 = new UIText(this._pack.Description, 1f, false)
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 0f),
				IsWrapped = true,
				WrappedTextBottomPadding = 0f
			};
			UIList uilist = new UIList
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				PaddingRight = 20f
			};
			uilist.ListPadding = 5f;
			uilist.Add(uitext4);
			uielement4.Append(uilist);
			this._list = uilist;
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(0f, 1f);
			uiscrollbar.HAlign = 1f;
			this._scrollbar = uiscrollbar;
			uilist.SetScrollbar(uiscrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-45f, 0f);
			uitextPanel.OnMouseOver += UIResourcePackInfoMenu.FadedMouseOver;
			uitextPanel.OnMouseOut += UIResourcePackInfoMenu.FadedMouseOut;
			uitextPanel.OnLeftClick += this.GoBackClick;
			uitextPanel.SetSnapPoint("GoBack", 0, null, null);
			uielement.Append(uitextPanel);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x0058A468 File Offset: 0x00588668
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._container.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._list.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._container.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._list.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0058A516 File Offset: 0x00588716
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.MenuUI.SetState(this._resourceMenu);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0058A528 File Offset: 0x00588728
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x00587919 File Offset: 0x00585B19
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x0058A57D File Offset: 0x0058877D
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x0058A590 File Offset: 0x00588790
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			int num2 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (name == "GoBack")
				{
					this._helper.MakeLinkPointFromSnapPoint(num2++, snapPoint);
				}
			}
			this._helper.MoveToVisuallyClosestPoint(num, num2);
		}

		// Token: 0x04005362 RID: 21346
		private UIResourcePackSelectionMenu _resourceMenu;

		// Token: 0x04005363 RID: 21347
		private ResourcePack _pack;

		// Token: 0x04005364 RID: 21348
		private UIElement _container;

		// Token: 0x04005365 RID: 21349
		private UIList _list;

		// Token: 0x04005366 RID: 21350
		private UIScrollbar _scrollbar;

		// Token: 0x04005367 RID: 21351
		private bool _isScrollbarAttached;

		// Token: 0x04005368 RID: 21352
		private const string _backPointName = "GoBack";

		// Token: 0x04005369 RID: 21353
		private UIGamepadHelper _helper;
	}
}
