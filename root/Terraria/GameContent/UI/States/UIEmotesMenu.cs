using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003AC RID: 940
	public class UIEmotesMenu : UIState
	{
		// Token: 0x06002B6F RID: 11119 RVA: 0x0058D4F0 File Offset: 0x0058B6F0
		public override void OnActivate()
		{
			this.InitializePage();
			if (Main.gameMenu)
			{
				this._outerContainer.Top.Set(220f, 0f);
				this._outerContainer.Height.Set(-220f, 1f);
				return;
			}
			this._outerContainer.Top.Set(120f, 0f);
			this._outerContainer.Height.Set(-120f, 1f);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0058D574 File Offset: 0x0058B774
		public void InitializePage()
		{
			base.RemoveAllChildren();
			UIElement uielement = new UIElement();
			uielement.Width.Set(590f, 0f);
			uielement.Top.Set(220f, 0f);
			uielement.Height.Set(-220f, 1f);
			uielement.HAlign = 0.5f;
			this._outerContainer = uielement;
			base.Append(uielement);
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set(-110f, 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uipanel.PaddingTop = 0f;
			uielement.Append(uipanel);
			this._container = uipanel;
			UIList uilist = new UIList();
			uilist.Width.Set(-25f, 1f);
			uilist.Height.Set(-50f, 1f);
			uilist.Top.Set(50f, 0f);
			uilist.HAlign = 0.5f;
			uilist.ListPadding = 14f;
			uipanel.Append(uilist);
			this._list = uilist;
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(-20f, 1f);
			uiscrollbar.HAlign = 1f;
			uiscrollbar.VAlign = 1f;
			uiscrollbar.Top = StyleDimension.FromPixels(-5f);
			uilist.SetScrollbar(uiscrollbar);
			this._scrollBar = uiscrollbar;
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-45f, 0f);
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftClick += this.GoBackClick;
			uitextPanel.SetSnapPoint("Back", 0, null, null);
			uielement.Append(uitextPanel);
			this._backPanel = uitextPanel;
			int num = 0;
			this.TryAddingList(Language.GetText("UI.EmoteCategoryGeneral"), ref num, 10, this.GetEmotesGeneral());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryRPS"), ref num, 10, this.GetEmotesRPS());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryItems"), ref num, 11, this.GetEmotesItems());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryBiomesAndEvents"), ref num, 8, this.GetEmotesBiomesAndEvents());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryTownNPCs"), ref num, 9, this.GetEmotesTownNPCs());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryCritters"), ref num, 7, this.GetEmotesCritters());
			this.TryAddingList(Language.GetText("UI.EmoteCategoryBosses"), ref num, 8, this.GetEmotesBosses());
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0058D8A4 File Offset: 0x0058BAA4
		private void TryAddingList(LocalizedText title, ref int currentGroupIndex, int maxEmotesPerRow, List<int> emoteIds)
		{
			if (emoteIds == null)
			{
				return;
			}
			if (emoteIds.Count == 0)
			{
				return;
			}
			UIList list = this._list;
			int num = currentGroupIndex;
			currentGroupIndex = num + 1;
			list.Add(new EmotesGroupListItem(title, num, maxEmotesPerRow, emoteIds.ToArray()));
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0058D8E4 File Offset: 0x0058BAE4
		private List<int> GetEmotesGeneral()
		{
			return new List<int>
			{
				0, 1, 2, 3, 15, 136, 94, 16, 135, 134,
				137, 138, 139, 17, 87, 88, 89, 91, 92, 93,
				8, 9, 10, 11, 14, 100, 146, 147, 148
			};
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0058D9F4 File Offset: 0x0058BBF4
		private List<int> GetEmotesRPS()
		{
			return new List<int> { 36, 37, 38, 33, 34, 35 };
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0058DA2C File Offset: 0x0058BC2C
		private List<int> GetEmotesItems()
		{
			return new List<int>
			{
				7, 73, 74, 75, 76, 131, 77, 78, 79, 80,
				81, 82, 83, 84, 85, 86, 90, 132, 126, 127,
				128, 129, 149
			};
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0058DB04 File Offset: 0x0058BD04
		private List<int> GetEmotesBiomesAndEvents()
		{
			return new List<int>
			{
				22, 23, 24, 25, 26, 27, 28, 29, 30, 31,
				32, 18, 19, 20, 21, 99, 4, 5, 6, 95,
				96, 97, 98
			};
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0058DBCC File Offset: 0x0058BDCC
		private List<int> GetEmotesTownNPCs()
		{
			return new List<int>
			{
				101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
				111, 112, 113, 114, 115, 116, 117, 118, 119, 120,
				121, 122, 123, 124, 125, 130, 140, 141, 142, 145
			};
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0058DCE0 File Offset: 0x0058BEE0
		private List<int> GetEmotesCritters()
		{
			List<int> list = new List<int>();
			list.AddRange(new int[] { 12, 13, 61, 62, 63 });
			list.AddRange(new int[] { 67, 68, 69, 70 });
			list.Add(72);
			if (NPC.downedGoblins)
			{
				list.Add(64);
			}
			if (NPC.downedFrost)
			{
				list.Add(66);
			}
			if (NPC.downedPirates)
			{
				list.Add(65);
			}
			if (NPC.downedMartians)
			{
				list.Add(71);
			}
			return list;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0058DD68 File Offset: 0x0058BF68
		private List<int> GetEmotesBosses()
		{
			List<int> list = new List<int>();
			if (NPC.downedBoss1)
			{
				list.Add(39);
			}
			if (NPC.downedBoss2)
			{
				list.Add(40);
				list.Add(41);
			}
			if (NPC.downedSlimeKing)
			{
				list.Add(51);
			}
			if (NPC.downedDeerclops)
			{
				list.Add(150);
			}
			if (NPC.downedQueenBee)
			{
				list.Add(42);
			}
			if (NPC.downedBoss3)
			{
				list.Add(43);
			}
			if (Main.hardMode)
			{
				list.Add(44);
			}
			if (NPC.downedQueenSlime)
			{
				list.Add(144);
			}
			if (NPC.downedMechBoss1)
			{
				list.Add(45);
			}
			if (NPC.downedMechBoss3)
			{
				list.Add(46);
			}
			if (NPC.downedMechBoss2)
			{
				list.Add(47);
			}
			if (NPC.downedPlantBoss)
			{
				list.Add(48);
			}
			if (NPC.downedGolemBoss)
			{
				list.Add(49);
			}
			if (NPC.downedFishron)
			{
				list.Add(50);
			}
			if (NPC.downedEmpressOfLight)
			{
				list.Add(143);
			}
			if (NPC.downedAncientCultist)
			{
				list.Add(52);
			}
			if (NPC.downedMoonlord)
			{
				list.Add(53);
			}
			if (NPC.downedHalloweenTree)
			{
				list.Add(54);
			}
			if (NPC.downedHalloweenKing)
			{
				list.Add(55);
			}
			if (NPC.downedChristmasTree)
			{
				list.Add(56);
			}
			if (NPC.downedChristmasIceQueen)
			{
				list.Add(57);
			}
			if (NPC.downedChristmasSantank)
			{
				list.Add(58);
			}
			if (NPC.downedPirates)
			{
				list.Add(59);
			}
			if (NPC.downedMartians)
			{
				list.Add(60);
			}
			if (DD2Event.DownedInvasionAnyDifficulty)
			{
				list.Add(133);
			}
			return list;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0058DF08 File Offset: 0x0058C108
		public override void Recalculate()
		{
			if (this._scrollBar != null)
			{
				if (this._isScrollbarAttached && !this._scrollBar.CanScroll)
				{
					this._container.RemoveChild(this._scrollBar);
					this._isScrollbarAttached = false;
					this._list.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollBar.CanScroll)
				{
					this._container.Append(this._scrollBar);
					this._isScrollbarAttached = true;
					this._list.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0058DFB6 File Offset: 0x0058C1B6
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.menuMode = 0;
			IngameFancyUI.Close(false);
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0058DFC4 File Offset: 0x0058C1C4
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0058539D File Offset: 0x0058359D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x0058E019 File Offset: 0x0058C219
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints2(spriteBatch);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x0058E02C File Offset: 0x0058C22C
		private void SetupGamepadPoints2(SpriteBatch spriteBatch)
		{
			int num = 7;
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num3;
			int num2 = (num3 = 3001);
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			this.RemoveSnapPointsOutOfScreen(spriteBatch, snapPoints);
			UILinkPointNavigator.SetPosition(num2, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num3];
			uilinkPoint.Unlink();
			uilinkPoint.Up = num3 + 1;
			UILinkPoint uilinkPoint2 = uilinkPoint;
			num3++;
			int num4 = 0;
			List<List<SnapPoint>> list = new List<List<SnapPoint>>();
			for (int i = 0; i < num; i++)
			{
				List<SnapPoint> emoteGroup = this.GetEmoteGroup(snapPoints, i);
				if (emoteGroup.Count > 0)
				{
					list.Add(emoteGroup);
				}
				num4 += (int)Math.Ceiling((double)((float)emoteGroup.Count / 14f));
			}
			SnapPoint[,] array = new SnapPoint[14, num4];
			int num5 = 0;
			for (int j = 0; j < list.Count; j++)
			{
				List<SnapPoint> list2 = list[j];
				for (int k = 0; k < list2.Count; k++)
				{
					int num6 = num5 + k / 14;
					int num7 = k % 14;
					array[num7, num6] = list2[k];
				}
				num5 += (int)Math.Ceiling((double)((float)list2.Count / 14f));
			}
			int[,] array2 = new int[14, num4];
			int num8 = 0;
			for (int l = 0; l < array.GetLength(1); l++)
			{
				for (int m = 0; m < array.GetLength(0); m++)
				{
					SnapPoint snapPoint = array[m, l];
					if (snapPoint != null)
					{
						UILinkPointNavigator.Points[num3].Unlink();
						UILinkPointNavigator.SetPosition(num3, snapPoint.Position);
						array2[m, l] = num3;
						if (m == 0)
						{
							num8 = num3;
						}
						num3++;
					}
				}
			}
			uilinkPoint2.Up = num8;
			for (int n = 0; n < array.GetLength(1); n++)
			{
				for (int num9 = 0; num9 < array.GetLength(0); num9++)
				{
					int num10 = array2[num9, n];
					if (num10 != 0)
					{
						UILinkPoint uilinkPoint3 = UILinkPointNavigator.Points[num10];
						if (this.TryGetPointOnGrid(array2, num9, n, -1, 0))
						{
							uilinkPoint3.Left = array2[num9 - 1, n];
						}
						else
						{
							uilinkPoint3.Left = uilinkPoint3.ID;
							for (int num11 = num9; num11 < array.GetLength(0); num11++)
							{
								if (this.TryGetPointOnGrid(array2, num11, n, 0, 0))
								{
									uilinkPoint3.Left = array2[num11, n];
								}
							}
						}
						if (this.TryGetPointOnGrid(array2, num9, n, 1, 0))
						{
							uilinkPoint3.Right = array2[num9 + 1, n];
						}
						else
						{
							uilinkPoint3.Right = uilinkPoint3.ID;
							for (int num12 = num9; num12 >= 0; num12--)
							{
								if (this.TryGetPointOnGrid(array2, num12, n, 0, 0))
								{
									uilinkPoint3.Right = array2[num12, n];
								}
							}
						}
						if (this.TryGetPointOnGrid(array2, num9, n, 0, -1))
						{
							uilinkPoint3.Up = array2[num9, n - 1];
						}
						else
						{
							uilinkPoint3.Up = uilinkPoint3.ID;
							for (int num13 = n - 1; num13 >= 0; num13--)
							{
								if (this.TryGetPointOnGrid(array2, num9, num13, 0, 0))
								{
									uilinkPoint3.Up = array2[num9, num13];
									break;
								}
							}
						}
						if (this.TryGetPointOnGrid(array2, num9, n, 0, 1))
						{
							uilinkPoint3.Down = array2[num9, n + 1];
						}
						else
						{
							uilinkPoint3.Down = uilinkPoint3.ID;
							for (int num14 = n + 1; num14 < array.GetLength(1); num14++)
							{
								if (this.TryGetPointOnGrid(array2, num9, num14, 0, 0))
								{
									uilinkPoint3.Down = array2[num9, num14];
									break;
								}
							}
							if (uilinkPoint3.Down == uilinkPoint3.ID)
							{
								uilinkPoint3.Down = uilinkPoint2.ID;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x0058E428 File Offset: 0x0058C628
		private bool TryGetPointOnGrid(int[,] grid, int x, int y, int offsetX, int offsetY)
		{
			return x + offsetX >= 0 && x + offsetX < grid.GetLength(0) && y + offsetY >= 0 && y + offsetY < grid.GetLength(1) && grid[x + offsetX, y + offsetY] != 0;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x0058E478 File Offset: 0x0058C678
		private void RemoveSnapPointsOutOfScreen(SpriteBatch spriteBatch, List<SnapPoint> pts)
		{
			float num = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._container.GetClippingRectangle(spriteBatch);
			Vector2 vector = clippingRectangle.TopLeft() * num;
			Vector2 vector2 = clippingRectangle.BottomRight() * num;
			for (int i = 0; i < pts.Count; i++)
			{
				if (!pts[i].Position.Between(vector, vector2))
				{
					pts.Remove(pts[i]);
					i--;
				}
			}
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x0058E4F0 File Offset: 0x0058C6F0
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3001;
			int num2 = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Up = num2 + 1;
			UILinkPoint uilinkPoint2 = uilinkPoint;
			num2++;
			float num3 = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._container.GetClippingRectangle(spriteBatch);
			Vector2 vector = clippingRectangle.TopLeft() * num3;
			Vector2 vector2 = clippingRectangle.BottomRight() * num3;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				if (!snapPoints[i].Position.Between(vector, vector2))
				{
					snapPoints.Remove(snapPoints[i]);
					i--;
				}
			}
			int num4 = 0;
			int num5 = 7;
			List<List<SnapPoint>> list = new List<List<SnapPoint>>();
			for (int j = 0; j < num5; j++)
			{
				List<SnapPoint> emoteGroup = this.GetEmoteGroup(snapPoints, j);
				if (emoteGroup.Count > 0)
				{
					list.Add(emoteGroup);
				}
			}
			List<SnapPoint>[] array = list.ToArray();
			for (int k = 0; k < array.Length; k++)
			{
				List<SnapPoint> list2 = array[k];
				int num6 = list2.Count / 14;
				if (list2.Count % 14 > 0)
				{
					num6++;
				}
				int num7 = 14;
				if (list2.Count % 14 != 0)
				{
					num7 = list2.Count % 14;
				}
				for (int l = 0; l < list2.Count; l++)
				{
					uilinkPoint = UILinkPointNavigator.Points[num2];
					uilinkPoint.Unlink();
					UILinkPointNavigator.SetPosition(num2, list2[l].Position);
					int num8 = 14;
					if (l / 14 == num6 - 1 && list2.Count % 14 != 0)
					{
						num8 = list2.Count % 14;
					}
					int num9 = l % 14;
					uilinkPoint.Left = num2 - 1;
					uilinkPoint.Right = num2 + 1;
					uilinkPoint.Up = num2 - 14;
					uilinkPoint.Down = num2 + 14;
					if (num9 == num8 - 1)
					{
						uilinkPoint.Right = num2 - num8 + 1;
					}
					if (num9 == 0)
					{
						uilinkPoint.Left = num2 + num8 - 1;
					}
					if (num9 == 0)
					{
						uilinkPoint2.Up = num2;
					}
					if (l < 14)
					{
						if (num4 == 0)
						{
							uilinkPoint.Up = -1;
						}
						else
						{
							uilinkPoint.Up = num2 - 14;
							if (num9 >= num4)
							{
								uilinkPoint.Up -= 14;
							}
							int num10 = k - 1;
							while (num10 > 0 && array[num10].Count <= num9)
							{
								uilinkPoint.Up -= 14;
								num10--;
							}
						}
					}
					int num11 = num;
					if (k == array.Length - 1)
					{
						if (l / 14 < num6 - 1 && num9 >= list2.Count % 14)
						{
							uilinkPoint.Down = num11;
						}
						if (l / 14 == num6 - 1)
						{
							uilinkPoint.Down = num11;
						}
					}
					else if (l / 14 == num6 - 1)
					{
						uilinkPoint.Down = num2 + 14;
						int num12 = k + 1;
						while (num12 < array.Length && array[num12].Count <= num9)
						{
							uilinkPoint.Down += 14;
							num12++;
						}
						if (k == array.Length - 1)
						{
							uilinkPoint.Down = num11;
						}
					}
					else if (num9 >= num7)
					{
						uilinkPoint.Down = num2 + 14 + 14;
						int num13 = k + 1;
						while (num13 < array.Length && array[num13].Count <= num9)
						{
							uilinkPoint.Down += 14;
							num13++;
						}
					}
					num2++;
				}
				num4 = num7;
				int num14 = 14 - num4;
				num2 += num14;
			}
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0058E8A0 File Offset: 0x0058CAA0
		private List<SnapPoint> GetEmoteGroup(List<SnapPoint> ptsOnPage, int groupIndex)
		{
			string groupName = "Group " + groupIndex;
			List<SnapPoint> list = ptsOnPage.Where((SnapPoint a) => a.Name == groupName).ToList<SnapPoint>();
			list.Sort(new Comparison<SnapPoint>(this.SortPoints));
			return list;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x0058E8F4 File Offset: 0x0058CAF4
		private int SortPoints(SnapPoint a, SnapPoint b)
		{
			return a.Id.CompareTo(b.Id);
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x0058E915 File Offset: 0x0058CB15
		public UIEmotesMenu()
		{
		}

		// Token: 0x0400538D RID: 21389
		private UIElement _outerContainer;

		// Token: 0x0400538E RID: 21390
		private UIElement _backPanel;

		// Token: 0x0400538F RID: 21391
		private UIElement _container;

		// Token: 0x04005390 RID: 21392
		private UIList _list;

		// Token: 0x04005391 RID: 21393
		private UIScrollbar _scrollBar;

		// Token: 0x04005392 RID: 21394
		private bool _isScrollbarAttached;

		// Token: 0x02000906 RID: 2310
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x06004752 RID: 18258 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x06004753 RID: 18259 RVA: 0x006CB4B7 File Offset: 0x006C96B7
			internal bool <GetEmoteGroup>b__0(SnapPoint a)
			{
				return a.Name == this.groupName;
			}

			// Token: 0x04007434 RID: 29748
			public string groupName;
		}
	}
}
