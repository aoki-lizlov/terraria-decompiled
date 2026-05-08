using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
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
	// Token: 0x020003A5 RID: 933
	public class UIWorkshopSelectResourcePackToPublish : UIState, IHaveBackButtonCommand
	{
		// Token: 0x06002AE1 RID: 10977 RVA: 0x005886CD File Offset: 0x005868CD
		public UIWorkshopSelectResourcePackToPublish(UIState menuToGoBackTo)
		{
			this._menuToGoBackTo = menuToGoBackTo;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x005886E8 File Offset: 0x005868E8
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Set(0f, 0.8f);
			uielement.MaxWidth.Set(650f, 0f);
			uielement.Top.Set(220f, 0f);
			uielement.Height.Set(-220f, 1f);
			uielement.HAlign = 0.5f;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set(-110f, 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uielement.Append(uipanel);
			this._containerPanel = uipanel;
			this._entryList = new UIList();
			this._entryList.Width.Set(0f, 1f);
			this._entryList.Height.Set(0f, 1f);
			this._entryList.ListPadding = 5f;
			uipanel.Append(this._entryList);
			this._scrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._entryList.SetScrollbar(this._scrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopSelectResourcePackToPublishMenuTitle"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-40f, 0f);
			uitextPanel.SetPadding(15f);
			uitextPanel.BackgroundColor = new Color(73, 94, 171);
			uielement.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel2.Width.Set(-10f, 0.5f);
			uitextPanel2.Height.Set(50f, 0f);
			uitextPanel2.VAlign = 1f;
			uitextPanel2.HAlign = 0.5f;
			uitextPanel2.Top.Set(-45f, 0f);
			uitextPanel2.OnMouseOver += this.FadedMouseOver;
			uitextPanel2.OnMouseOut += this.FadedMouseOut;
			uitextPanel2.OnLeftClick += this.GoBackClick;
			uielement.Append(uitextPanel2);
			this._backPanel = uitextPanel2;
			base.Append(uielement);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0058897C File Offset: 0x00586B7C
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._containerPanel.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._entryList.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._containerPanel.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._entryList.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x00588A2A File Offset: 0x00586C2A
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x00588A32 File Offset: 0x00586C32
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._menuToGoBackTo);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x00588A5C File Offset: 0x00586C5C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x00588AB1 File Offset: 0x00586CB1
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x00588AF0 File Offset: 0x00586CF0
		public override void OnActivate()
		{
			this.PopulateEntries();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._entryList.Count == 0) ? 0 : 1));
			}
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x00588B1C File Offset: 0x00586D1C
		public void PopulateEntries()
		{
			this._entries.Clear();
			IOrderedEnumerable<ResourcePack> orderedEnumerable = from x in AssetInitializer.CreatePublishableResourcePacksList(Main.instance.Services).AllPacks
				where x.Branding == ResourcePack.BrandingType.None
				orderby x.IsCompressed
				select x;
			this._entries.AddRange(orderedEnumerable);
			this._entryList.Clear();
			int num = 0;
			foreach (ResourcePack resourcePack in this._entries)
			{
				this._entryList.Add(new UIWorkshopPublishResourcePackListItem(this, resourcePack, num++, !resourcePack.IsCompressed));
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x00588C0C File Offset: 0x00586E0C
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (this.skipDraw)
			{
				this.skipDraw = false;
				return;
			}
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x00588C2C File Offset: 0x00586E2C
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Right = num2;
			float num3 = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._containerPanel.GetClippingRectangle(spriteBatch);
			Vector2 vector = clippingRectangle.TopLeft() * num3;
			Vector2 vector2 = clippingRectangle.BottomRight() * num3;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			for (int i = 0; i < snapPoints.Count; i++)
			{
				if (!snapPoints[i].Position.Between(vector, vector2))
				{
					snapPoints.Remove(snapPoints[i]);
					i--;
				}
			}
			int num4 = 1;
			SnapPoint[,] array = new SnapPoint[this._entryList.Count, num4];
			foreach (SnapPoint snapPoint in snapPoints.Where((SnapPoint a) => a.Name == "Publish"))
			{
				array[snapPoint.Id, 0] = snapPoint;
			}
			num2 = num + 1;
			int[] array2 = new int[this._entryList.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = -1;
			}
			for (int k = 0; k < num4; k++)
			{
				int num5 = -1;
				for (int l = 0; l < array.GetLength(0); l++)
				{
					if (array[l, k] != null)
					{
						uilinkPoint = UILinkPointNavigator.Points[num2];
						uilinkPoint.Unlink();
						UILinkPointNavigator.SetPosition(num2, array[l, k].Position);
						if (num5 != -1)
						{
							uilinkPoint.Up = num5;
							UILinkPointNavigator.Points[num5].Down = num2;
						}
						if (array2[l] != -1)
						{
							uilinkPoint.Left = array2[l];
							UILinkPointNavigator.Points[array2[l]].Right = num2;
						}
						uilinkPoint.Down = num;
						if (k == 0)
						{
							UILinkPointNavigator.Points[num].Up = (UILinkPointNavigator.Points[num + 1].Up = num2);
						}
						num5 = num2;
						array2[l] = num2;
						UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num2;
						num2++;
					}
				}
			}
			if (PlayerInput.UsingGamepadUI && this._entryList.Count == 0 && UILinkPointNavigator.CurrentPoint > 3000)
			{
				UILinkPointNavigator.ChangePoint(3000);
			}
		}

		// Token: 0x0400534A RID: 21322
		private UIList _entryList;

		// Token: 0x0400534B RID: 21323
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x0400534C RID: 21324
		private UIPanel _containerPanel;

		// Token: 0x0400534D RID: 21325
		private UIScrollbar _scrollbar;

		// Token: 0x0400534E RID: 21326
		private bool _isScrollbarAttached;

		// Token: 0x0400534F RID: 21327
		private UIState _menuToGoBackTo;

		// Token: 0x04005350 RID: 21328
		private List<ResourcePack> _entries = new List<ResourcePack>();

		// Token: 0x04005351 RID: 21329
		private bool skipDraw;

		// Token: 0x020008FA RID: 2298
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004735 RID: 18229 RVA: 0x006CB3C0 File Offset: 0x006C95C0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004736 RID: 18230 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004737 RID: 18231 RVA: 0x006CB3CC File Offset: 0x006C95CC
			internal bool <PopulateEntries>b__15_0(ResourcePack x)
			{
				return x.Branding == ResourcePack.BrandingType.None;
			}

			// Token: 0x06004738 RID: 18232 RVA: 0x006CB3D7 File Offset: 0x006C95D7
			internal bool <PopulateEntries>b__15_1(ResourcePack x)
			{
				return x.IsCompressed;
			}

			// Token: 0x06004739 RID: 18233 RVA: 0x006CB3DF File Offset: 0x006C95DF
			internal bool <SetupGamepadPoints>b__18_0(SnapPoint a)
			{
				return a.Name == "Publish";
			}

			// Token: 0x0400740A RID: 29706
			public static readonly UIWorkshopSelectResourcePackToPublish.<>c <>9 = new UIWorkshopSelectResourcePackToPublish.<>c();

			// Token: 0x0400740B RID: 29707
			public static Func<ResourcePack, bool> <>9__15_0;

			// Token: 0x0400740C RID: 29708
			public static Func<ResourcePack, bool> <>9__15_1;

			// Token: 0x0400740D RID: 29709
			public static Func<SnapPoint, bool> <>9__18_0;
		}
	}
}
