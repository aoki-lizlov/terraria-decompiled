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
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003A6 RID: 934
	public class UIWorkshopSelectWorldToPublish : UIState, IHaveBackButtonCommand
	{
		// Token: 0x06002AEC RID: 10988 RVA: 0x00588EE8 File Offset: 0x005870E8
		public UIWorkshopSelectWorldToPublish(UIState menuToGoBackTo)
		{
			this._menuToGoBackTo = menuToGoBackTo;
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x00588EF8 File Offset: 0x005870F8
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
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopSelectWorldToPublishMenuTitle"), 0.8f, true);
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

		// Token: 0x06002AEE RID: 10990 RVA: 0x0058918C File Offset: 0x0058738C
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

		// Token: 0x06002AEF RID: 10991 RVA: 0x0058923A File Offset: 0x0058743A
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x00589242 File Offset: 0x00587442
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._menuToGoBackTo);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0058926C File Offset: 0x0058746C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x00588AB1 File Offset: 0x00586CB1
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x005892C1 File Offset: 0x005874C1
		public override void OnActivate()
		{
			this.PopulateEntries();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._entryList.Count == 0) ? 0 : 1));
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x005892EC File Offset: 0x005874EC
		private void PopulateEntries()
		{
			Main.LoadWorlds();
			this._entryList.Clear();
			List<WorldFileData> list = new List<WorldFileData>(Main.WorldList);
			list.RemoveAll((WorldFileData x) => !x.IsValid);
			IEnumerable<WorldFileData> enumerable = from x in list
				orderby x.IsFavorite descending, x.Name, x.GetFileName(true)
				select x;
			this._entryList.Clear();
			int num = 0;
			foreach (WorldFileData worldFileData in enumerable)
			{
				this._entryList.Add(new UIWorkshopPublishWorldListItem(this, worldFileData, num++));
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x005893FC File Offset: 0x005875FC
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

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0058941C File Offset: 0x0058761C
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

		// Token: 0x04005352 RID: 21330
		private UIList _entryList;

		// Token: 0x04005353 RID: 21331
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x04005354 RID: 21332
		private UIPanel _containerPanel;

		// Token: 0x04005355 RID: 21333
		private UIScrollbar _scrollbar;

		// Token: 0x04005356 RID: 21334
		private bool _isScrollbarAttached;

		// Token: 0x04005357 RID: 21335
		private UIState _menuToGoBackTo;

		// Token: 0x04005358 RID: 21336
		private bool skipDraw;

		// Token: 0x020008FB RID: 2299
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600473A RID: 18234 RVA: 0x006CB3F1 File Offset: 0x006C95F1
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600473B RID: 18235 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600473C RID: 18236 RVA: 0x006CB3FD File Offset: 0x006C95FD
			internal bool <PopulateEntries>b__14_0(WorldFileData x)
			{
				return !x.IsValid;
			}

			// Token: 0x0600473D RID: 18237 RVA: 0x006CB408 File Offset: 0x006C9608
			internal bool <PopulateEntries>b__14_1(WorldFileData x)
			{
				return x.IsFavorite;
			}

			// Token: 0x0600473E RID: 18238 RVA: 0x006CB410 File Offset: 0x006C9610
			internal string <PopulateEntries>b__14_2(WorldFileData x)
			{
				return x.Name;
			}

			// Token: 0x0600473F RID: 18239 RVA: 0x006CB418 File Offset: 0x006C9618
			internal string <PopulateEntries>b__14_3(WorldFileData x)
			{
				return x.GetFileName(true);
			}

			// Token: 0x06004740 RID: 18240 RVA: 0x006CB3DF File Offset: 0x006C95DF
			internal bool <SetupGamepadPoints>b__17_0(SnapPoint a)
			{
				return a.Name == "Publish";
			}

			// Token: 0x0400740E RID: 29710
			public static readonly UIWorkshopSelectWorldToPublish.<>c <>9 = new UIWorkshopSelectWorldToPublish.<>c();

			// Token: 0x0400740F RID: 29711
			public static Predicate<WorldFileData> <>9__14_0;

			// Token: 0x04007410 RID: 29712
			public static Func<WorldFileData, bool> <>9__14_1;

			// Token: 0x04007411 RID: 29713
			public static Func<WorldFileData, string> <>9__14_2;

			// Token: 0x04007412 RID: 29714
			public static Func<WorldFileData, string> <>9__14_3;

			// Token: 0x04007413 RID: 29715
			public static Func<SnapPoint, bool> <>9__17_0;
		}
	}
}
