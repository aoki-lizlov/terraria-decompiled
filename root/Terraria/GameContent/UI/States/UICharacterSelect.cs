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
	// Token: 0x020003B6 RID: 950
	public class UICharacterSelect : UIState
	{
		// Token: 0x06002CCC RID: 11468 RVA: 0x0059F0A4 File Offset: 0x0059D2A4
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
			this._containerPanel = uipanel;
			uielement.Append(uipanel);
			this._playerList = new UIList();
			this._playerList.Width.Set(0f, 1f);
			this._playerList.Height.Set(0f, 1f);
			this._playerList.ListPadding = 5f;
			uipanel.Append(this._playerList);
			this._scrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._playerList.SetScrollbar(this._scrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.SelectPlayer"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-40f, 0f);
			uitextPanel.SetPadding(15f);
			uitextPanel.BackgroundColor = new Color(73, 94, 171);
			uielement.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel2.Width.Set(-10f, 0.5f);
			uitextPanel2.Height.Set(50f, 0f);
			uitextPanel2.VAlign = 1f;
			uitextPanel2.Top.Set(-45f, 0f);
			uitextPanel2.OnMouseOver += this.FadedMouseOver;
			uitextPanel2.OnMouseOut += this.FadedMouseOut;
			uitextPanel2.OnLeftClick += this.GoBackClick;
			uitextPanel2.SetSnapPoint("Back", 0, null, null);
			uielement.Append(uitextPanel2);
			this._backPanel = uitextPanel2;
			UITextPanel<LocalizedText> uitextPanel3 = new UITextPanel<LocalizedText>(Language.GetText("UI.New"), 0.7f, true);
			uitextPanel3.CopyStyle(uitextPanel2);
			uitextPanel3.HAlign = 1f;
			uitextPanel3.OnMouseOver += this.FadedMouseOver;
			uitextPanel3.OnMouseOut += this.FadedMouseOut;
			uitextPanel3.OnLeftClick += this.NewCharacterClick;
			uielement.Append(uitextPanel3);
			uitextPanel2.SetSnapPoint("New", 0, null, null);
			this._newPanel = uitextPanel3;
			base.Append(uielement);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x0059F3E4 File Offset: 0x0059D5E4
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._containerPanel.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._playerList.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._containerPanel.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._playerList.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x0059F492 File Offset: 0x0059D692
		private void NewCharacterClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.PendingPlayer = new Player();
			Main.menuMode = 888;
			Main.MenuUI.SetState(new UICharacterCreation(Main.PendingPlayer));
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x0059F4D1 File Offset: 0x0059D6D1
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = 0;
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x0059F4F0 File Offset: 0x0059D6F0
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x00588AB1 File Offset: 0x00586CB1
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x0059F545 File Offset: 0x0059D745
		public override void OnActivate()
		{
			Main.LoadPlayers();
			Main.ActivePlayerFileData = new PlayerFileData();
			this.UpdatePlayersList();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._playerList.Count == 0) ? 1 : 2));
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x0059F580 File Offset: 0x0059D780
		private void UpdatePlayersList()
		{
			this._playerList.Clear();
			IEnumerable<PlayerFileData> enumerable = from x in Main.PlayerList
				orderby x.IsFavorite descending, x.LastPlayed descending, x.Name, x.GetFileName(true)
				select x;
			int num = 0;
			foreach (PlayerFileData playerFileData in enumerable)
			{
				this._playerList.Add(new UICharacterListItem(playerFileData, num++));
			}
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x0059F678 File Offset: 0x0059D878
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (this.skipDraw)
			{
				this.skipDraw = false;
				return;
			}
			if (this.UpdateFavoritesCache())
			{
				this.skipDraw = true;
				Main.MenuUI.Draw(spriteBatch, new GameTime());
			}
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x0059F6B8 File Offset: 0x0059D8B8
		private bool UpdateFavoritesCache()
		{
			List<PlayerFileData> list = new List<PlayerFileData>(Main.PlayerList);
			list.Sort(delegate(PlayerFileData x, PlayerFileData y)
			{
				if (x.IsFavorite && !y.IsFavorite)
				{
					return -1;
				}
				if (!x.IsFavorite && y.IsFavorite)
				{
					return 1;
				}
				if (x.Name.CompareTo(y.Name) != 0)
				{
					return x.Name.CompareTo(y.Name);
				}
				return x.GetFileName(true).CompareTo(y.GetFileName(true));
			});
			bool flag = false;
			if (!flag && list.Count != this.favoritesCache.Count)
			{
				flag = true;
			}
			if (!flag)
			{
				for (int i = 0; i < this.favoritesCache.Count; i++)
				{
					Tuple<string, bool> tuple = this.favoritesCache[i];
					if (!(list[i].Name == tuple.Item1) || list[i].IsFavorite != tuple.Item2)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.favoritesCache.Clear();
				foreach (PlayerFileData playerFileData in list)
				{
					this.favoritesCache.Add(Tuple.Create<string, bool>(playerFileData.Name, playerFileData.IsFavorite));
				}
				this.UpdatePlayersList();
			}
			return flag;
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x0059F7D8 File Offset: 0x0059D9D8
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 1, this._newPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Right = num2 + 1;
			num2 = num + 1;
			uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Left = num2 - 1;
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
			int num4 = 5;
			SnapPoint[,] array = new SnapPoint[this._playerList.Count, num4];
			foreach (SnapPoint snapPoint in snapPoints.Where((SnapPoint a) => a.Name == "Play"))
			{
				array[snapPoint.Id, 0] = snapPoint;
			}
			foreach (SnapPoint snapPoint2 in snapPoints.Where((SnapPoint a) => a.Name == "Favorite"))
			{
				array[snapPoint2.Id, 1] = snapPoint2;
			}
			foreach (SnapPoint snapPoint3 in snapPoints.Where((SnapPoint a) => a.Name == "Cloud"))
			{
				array[snapPoint3.Id, 2] = snapPoint3;
			}
			foreach (SnapPoint snapPoint4 in snapPoints.Where((SnapPoint a) => a.Name == "Rename"))
			{
				array[snapPoint4.Id, 3] = snapPoint4;
			}
			foreach (SnapPoint snapPoint5 in snapPoints.Where((SnapPoint a) => a.Name == "Delete"))
			{
				array[snapPoint5.Id, 4] = snapPoint5;
			}
			num2 = num + 2;
			int[] array2 = new int[this._playerList.Count];
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
			if (PlayerInput.UsingGamepadUI && this._playerList.Count == 0 && UILinkPointNavigator.CurrentPoint > 3001)
			{
				UILinkPointNavigator.ChangePoint(3001);
			}
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x0059FC8C File Offset: 0x0059DE8C
		public UICharacterSelect()
		{
		}

		// Token: 0x04005447 RID: 21575
		private UIList _playerList;

		// Token: 0x04005448 RID: 21576
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x04005449 RID: 21577
		private UITextPanel<LocalizedText> _newPanel;

		// Token: 0x0400544A RID: 21578
		private UIPanel _containerPanel;

		// Token: 0x0400544B RID: 21579
		private UIScrollbar _scrollbar;

		// Token: 0x0400544C RID: 21580
		private bool _isScrollbarAttached;

		// Token: 0x0400544D RID: 21581
		private List<Tuple<string, bool>> favoritesCache = new List<Tuple<string, bool>>();

		// Token: 0x0400544E RID: 21582
		private bool skipDraw;

		// Token: 0x0200091F RID: 2335
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060047D8 RID: 18392 RVA: 0x006CC5FB File Offset: 0x006CA7FB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060047D9 RID: 18393 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060047DA RID: 18394 RVA: 0x006CB408 File Offset: 0x006C9608
			internal bool <UpdatePlayersList>b__14_0(PlayerFileData x)
			{
				return x.IsFavorite;
			}

			// Token: 0x060047DB RID: 18395 RVA: 0x006CC607 File Offset: 0x006CA807
			internal DateTime <UpdatePlayersList>b__14_1(PlayerFileData x)
			{
				return x.LastPlayed;
			}

			// Token: 0x060047DC RID: 18396 RVA: 0x006CB410 File Offset: 0x006C9610
			internal string <UpdatePlayersList>b__14_2(PlayerFileData x)
			{
				return x.Name;
			}

			// Token: 0x060047DD RID: 18397 RVA: 0x006CB418 File Offset: 0x006C9618
			internal string <UpdatePlayersList>b__14_3(PlayerFileData x)
			{
				return x.GetFileName(true);
			}

			// Token: 0x060047DE RID: 18398 RVA: 0x006CC610 File Offset: 0x006CA810
			internal int <UpdateFavoritesCache>b__17_0(PlayerFileData x, PlayerFileData y)
			{
				if (x.IsFavorite && !y.IsFavorite)
				{
					return -1;
				}
				if (!x.IsFavorite && y.IsFavorite)
				{
					return 1;
				}
				if (x.Name.CompareTo(y.Name) != 0)
				{
					return x.Name.CompareTo(y.Name);
				}
				return x.GetFileName(true).CompareTo(y.GetFileName(true));
			}

			// Token: 0x060047DF RID: 18399 RVA: 0x006CC58F File Offset: 0x006CA78F
			internal bool <SetupGamepadPoints>b__18_0(SnapPoint a)
			{
				return a.Name == "Play";
			}

			// Token: 0x060047E0 RID: 18400 RVA: 0x006CC5A1 File Offset: 0x006CA7A1
			internal bool <SetupGamepadPoints>b__18_1(SnapPoint a)
			{
				return a.Name == "Favorite";
			}

			// Token: 0x060047E1 RID: 18401 RVA: 0x006CC5B3 File Offset: 0x006CA7B3
			internal bool <SetupGamepadPoints>b__18_2(SnapPoint a)
			{
				return a.Name == "Cloud";
			}

			// Token: 0x060047E2 RID: 18402 RVA: 0x006CC5D7 File Offset: 0x006CA7D7
			internal bool <SetupGamepadPoints>b__18_3(SnapPoint a)
			{
				return a.Name == "Rename";
			}

			// Token: 0x060047E3 RID: 18403 RVA: 0x006CC5E9 File Offset: 0x006CA7E9
			internal bool <SetupGamepadPoints>b__18_4(SnapPoint a)
			{
				return a.Name == "Delete";
			}

			// Token: 0x040074DD RID: 29917
			public static readonly UICharacterSelect.<>c <>9 = new UICharacterSelect.<>c();

			// Token: 0x040074DE RID: 29918
			public static Func<PlayerFileData, bool> <>9__14_0;

			// Token: 0x040074DF RID: 29919
			public static Func<PlayerFileData, DateTime> <>9__14_1;

			// Token: 0x040074E0 RID: 29920
			public static Func<PlayerFileData, string> <>9__14_2;

			// Token: 0x040074E1 RID: 29921
			public static Func<PlayerFileData, string> <>9__14_3;

			// Token: 0x040074E2 RID: 29922
			public static Comparison<PlayerFileData> <>9__17_0;

			// Token: 0x040074E3 RID: 29923
			public static Func<SnapPoint, bool> <>9__18_0;

			// Token: 0x040074E4 RID: 29924
			public static Func<SnapPoint, bool> <>9__18_1;

			// Token: 0x040074E5 RID: 29925
			public static Func<SnapPoint, bool> <>9__18_2;

			// Token: 0x040074E6 RID: 29926
			public static Func<SnapPoint, bool> <>9__18_3;

			// Token: 0x040074E7 RID: 29927
			public static Func<SnapPoint, bool> <>9__18_4;
		}
	}
}
