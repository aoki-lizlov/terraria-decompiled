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
using Terraria.Map;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003B5 RID: 949
	public class UIWorldSelect : UIState
	{
		// Token: 0x06002CBC RID: 11452 RVA: 0x0059E374 File Offset: 0x0059C574
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
			this._worldList = new UIList();
			this._worldList.Width.Set(0f, 1f);
			this._worldList.Height.Set(0f, 1f);
			this._worldList.ListPadding = 5f;
			uipanel.Append(this._worldList);
			this._scrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._worldList.SetScrollbar(this._scrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.SelectWorld"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-40f, 0f);
			uitextPanel.SetPadding(15f);
			uitextPanel.BackgroundColor = new Color(73, 94, 171);
			uielement.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel2.Width.Set(-10f, 0.5f);
			uitextPanel2.Height.Set(50f, 0f);
			uitextPanel2.VAlign = 1f;
			uitextPanel2.HAlign = 0f;
			uitextPanel2.Top.Set(-45f, 0f);
			uitextPanel2.OnMouseOver += this.FadedMouseOver;
			uitextPanel2.OnMouseOut += this.FadedMouseOut;
			uitextPanel2.OnLeftClick += this.GoBackClick;
			uielement.Append(uitextPanel2);
			this._backPanel = uitextPanel2;
			UITextPanel<LocalizedText> uitextPanel3 = new UITextPanel<LocalizedText>(Language.GetText("UI.New"), 0.7f, true);
			uitextPanel3.CopyStyle(uitextPanel2);
			uitextPanel3.HAlign = 1f;
			uitextPanel3.OnMouseOver += this.FadedMouseOver;
			uitextPanel3.OnMouseOut += this.FadedMouseOut;
			uitextPanel3.OnLeftClick += this.NewWorldClick;
			uielement.Append(uitextPanel3);
			this._newPanel = uitextPanel3;
			base.Append(uielement);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x0059E67C File Offset: 0x0059C87C
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._containerPanel.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._worldList.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._containerPanel.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._worldList.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x0059E72C File Offset: 0x0059C92C
		private void NewWorldClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.newWorldName = Lang.gen[57].Value + " " + (Main.WorldList.Count + 1);
			Main.menuMode = 888;
			Main.MenuUI.SetState(new UIWorldCreation());
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x0059E794 File Offset: 0x0059C994
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = (Main.menuMultiplayer ? 12 : 1);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x0059E7BC File Offset: 0x0059C9BC
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x00588AB1 File Offset: 0x00586CB1
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x0059E811 File Offset: 0x0059CA11
		public override void OnActivate()
		{
			Main.LoadWorlds();
			this.UpdateWorldsList();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._worldList.Count == 0) ? 1 : 2));
			}
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x0059E841 File Offset: 0x0059CA41
		public override void OnDeactivate()
		{
			base.OnDeactivate();
			UIWorldSelect.NewlyGeneratedWorld = null;
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x0059E850 File Offset: 0x0059CA50
		private void UpdateWorldsList()
		{
			this._worldList.Clear();
			IEnumerable<WorldFileData> enumerable = Main.WorldList.OrderByDescending(new Func<WorldFileData, bool>(UIWorldSelect.CanWorldBeJoinedByActivePlayer)).ThenByDescending(new Func<WorldFileData, bool>(UIWorldSelect.IsNewlyGenerated)).ThenByDescending((WorldFileData x) => x.IsFavorite)
				.ThenByDescending(new Func<WorldFileData, bool>(UIWorldSelect.HasWorldBeenPlayedByActivePlayer))
				.ThenByDescending((WorldFileData x) => x.LastPlayed)
				.ThenBy((WorldFileData x) => x.Name)
				.ThenBy((WorldFileData x) => x.GetFileName(true));
			int num = 0;
			foreach (WorldFileData worldFileData in enumerable)
			{
				this._worldList.Add(new UIWorldListItem(worldFileData, num++, UIWorldSelect.CanWorldBeJoinedByActivePlayer(worldFileData), UIWorldSelect.HasWorldBeenPlayedByActivePlayer(worldFileData), UIWorldSelect.IsNewlyGenerated(worldFileData)));
			}
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x0059E990 File Offset: 0x0059CB90
		private static bool IsNewlyGenerated(WorldFileData file)
		{
			return UIWorldSelect.NewlyGeneratedWorld != null && file.Path == UIWorldSelect.NewlyGeneratedWorld.Path && file.IsCloudSave == UIWorldSelect.NewlyGeneratedWorld.IsCloudSave;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x0059E9C4 File Offset: 0x0059CBC4
		private static bool CanWorldBeJoinedByActivePlayer(WorldFileData file)
		{
			bool flag = Main.ActivePlayerFileData.Player.difficulty == 3;
			bool flag2 = file.GameMode == 3;
			return flag == flag2;
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x0059E9F0 File Offset: 0x0059CBF0
		private static bool HasWorldBeenPlayedByActivePlayer(WorldFileData file)
		{
			string text;
			return WorldMap.TryGetMapPath(Main.ActivePlayerFileData, file, out text);
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x0059EA0A File Offset: 0x0059CC0A
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

		// Token: 0x06002CC9 RID: 11465 RVA: 0x0059EA4C File Offset: 0x0059CC4C
		private bool UpdateFavoritesCache()
		{
			List<WorldFileData> list = new List<WorldFileData>(Main.WorldList);
			list.Sort(delegate(WorldFileData x, WorldFileData y)
			{
				if (x.IsFavorite && !y.IsFavorite)
				{
					return -1;
				}
				if (!x.IsFavorite && y.IsFavorite)
				{
					return 1;
				}
				if (x.Name == null)
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
				foreach (WorldFileData worldFileData in list)
				{
					this.favoritesCache.Add(Tuple.Create<string, bool>(worldFileData.Name, worldFileData.IsFavorite));
				}
				this.UpdateWorldsList();
			}
			return flag;
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x0059EB6C File Offset: 0x0059CD6C
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 2;
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
			SnapPoint[,] array = new SnapPoint[this._worldList.Count, 6];
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
			foreach (SnapPoint snapPoint4 in snapPoints.Where((SnapPoint a) => a.Name == "Seed"))
			{
				array[snapPoint4.Id, 3] = snapPoint4;
			}
			foreach (SnapPoint snapPoint5 in snapPoints.Where((SnapPoint a) => a.Name == "Rename"))
			{
				array[snapPoint5.Id, 4] = snapPoint5;
			}
			foreach (SnapPoint snapPoint6 in snapPoints.Where((SnapPoint a) => a.Name == "Delete"))
			{
				array[snapPoint6.Id, 5] = snapPoint6;
			}
			num2 = num + 2;
			int[] array2 = new int[this._worldList.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = -1;
			}
			for (int k = 0; k < array.GetLength(1); k++)
			{
				int num4 = -1;
				for (int l = 0; l < array.GetLength(0); l++)
				{
					if (array[l, k] != null)
					{
						uilinkPoint = UILinkPointNavigator.Points[num2];
						uilinkPoint.Unlink();
						UILinkPointNavigator.SetPosition(num2, array[l, k].Position);
						if (num4 != -1)
						{
							uilinkPoint.Up = num4;
							UILinkPointNavigator.Points[num4].Down = num2;
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
						num4 = num2;
						array2[l] = num2;
						UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num2;
						num2++;
					}
				}
			}
			if (PlayerInput.UsingGamepadUI && this._worldList.Count == 0 && UILinkPointNavigator.CurrentPoint > 3001)
			{
				UILinkPointNavigator.ChangePoint(3001);
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0059F090 File Offset: 0x0059D290
		public UIWorldSelect()
		{
		}

		// Token: 0x0400543E RID: 21566
		public static WorldFileData NewlyGeneratedWorld;

		// Token: 0x0400543F RID: 21567
		private UIList _worldList;

		// Token: 0x04005440 RID: 21568
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x04005441 RID: 21569
		private UITextPanel<LocalizedText> _newPanel;

		// Token: 0x04005442 RID: 21570
		private UIPanel _containerPanel;

		// Token: 0x04005443 RID: 21571
		private UIScrollbar _scrollbar;

		// Token: 0x04005444 RID: 21572
		private bool _isScrollbarAttached;

		// Token: 0x04005445 RID: 21573
		private List<Tuple<string, bool>> favoritesCache = new List<Tuple<string, bool>>();

		// Token: 0x04005446 RID: 21574
		private bool skipDraw;

		// Token: 0x0200091E RID: 2334
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060047CB RID: 18379 RVA: 0x006CC507 File Offset: 0x006CA707
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060047CC RID: 18380 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060047CD RID: 18381 RVA: 0x006CB408 File Offset: 0x006C9608
			internal bool <UpdateWorldsList>b__16_0(WorldFileData x)
			{
				return x.IsFavorite;
			}

			// Token: 0x060047CE RID: 18382 RVA: 0x006CC513 File Offset: 0x006CA713
			internal DateTime <UpdateWorldsList>b__16_1(WorldFileData x)
			{
				return x.LastPlayed;
			}

			// Token: 0x060047CF RID: 18383 RVA: 0x006CB410 File Offset: 0x006C9610
			internal string <UpdateWorldsList>b__16_2(WorldFileData x)
			{
				return x.Name;
			}

			// Token: 0x060047D0 RID: 18384 RVA: 0x006CB418 File Offset: 0x006C9618
			internal string <UpdateWorldsList>b__16_3(WorldFileData x)
			{
				return x.GetFileName(true);
			}

			// Token: 0x060047D1 RID: 18385 RVA: 0x006CC51C File Offset: 0x006CA71C
			internal int <UpdateFavoritesCache>b__22_0(WorldFileData x, WorldFileData y)
			{
				if (x.IsFavorite && !y.IsFavorite)
				{
					return -1;
				}
				if (!x.IsFavorite && y.IsFavorite)
				{
					return 1;
				}
				if (x.Name == null)
				{
					return 1;
				}
				if (x.Name.CompareTo(y.Name) != 0)
				{
					return x.Name.CompareTo(y.Name);
				}
				return x.GetFileName(true).CompareTo(y.GetFileName(true));
			}

			// Token: 0x060047D2 RID: 18386 RVA: 0x006CC58F File Offset: 0x006CA78F
			internal bool <SetupGamepadPoints>b__23_0(SnapPoint a)
			{
				return a.Name == "Play";
			}

			// Token: 0x060047D3 RID: 18387 RVA: 0x006CC5A1 File Offset: 0x006CA7A1
			internal bool <SetupGamepadPoints>b__23_1(SnapPoint a)
			{
				return a.Name == "Favorite";
			}

			// Token: 0x060047D4 RID: 18388 RVA: 0x006CC5B3 File Offset: 0x006CA7B3
			internal bool <SetupGamepadPoints>b__23_2(SnapPoint a)
			{
				return a.Name == "Cloud";
			}

			// Token: 0x060047D5 RID: 18389 RVA: 0x006CC5C5 File Offset: 0x006CA7C5
			internal bool <SetupGamepadPoints>b__23_3(SnapPoint a)
			{
				return a.Name == "Seed";
			}

			// Token: 0x060047D6 RID: 18390 RVA: 0x006CC5D7 File Offset: 0x006CA7D7
			internal bool <SetupGamepadPoints>b__23_4(SnapPoint a)
			{
				return a.Name == "Rename";
			}

			// Token: 0x060047D7 RID: 18391 RVA: 0x006CC5E9 File Offset: 0x006CA7E9
			internal bool <SetupGamepadPoints>b__23_5(SnapPoint a)
			{
				return a.Name == "Delete";
			}

			// Token: 0x040074D1 RID: 29905
			public static readonly UIWorldSelect.<>c <>9 = new UIWorldSelect.<>c();

			// Token: 0x040074D2 RID: 29906
			public static Func<WorldFileData, bool> <>9__16_0;

			// Token: 0x040074D3 RID: 29907
			public static Func<WorldFileData, DateTime> <>9__16_1;

			// Token: 0x040074D4 RID: 29908
			public static Func<WorldFileData, string> <>9__16_2;

			// Token: 0x040074D5 RID: 29909
			public static Func<WorldFileData, string> <>9__16_3;

			// Token: 0x040074D6 RID: 29910
			public static Comparison<WorldFileData> <>9__22_0;

			// Token: 0x040074D7 RID: 29911
			public static Func<SnapPoint, bool> <>9__23_0;

			// Token: 0x040074D8 RID: 29912
			public static Func<SnapPoint, bool> <>9__23_1;

			// Token: 0x040074D9 RID: 29913
			public static Func<SnapPoint, bool> <>9__23_2;

			// Token: 0x040074DA RID: 29914
			public static Func<SnapPoint, bool> <>9__23_3;

			// Token: 0x040074DB RID: 29915
			public static Func<SnapPoint, bool> <>9__23_4;

			// Token: 0x040074DC RID: 29916
			public static Func<SnapPoint, bool> <>9__23_5;
		}
	}
}
