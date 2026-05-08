using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Testing;

namespace Terraria.UI.Gamepad
{
	// Token: 0x02000108 RID: 264
	public class UILinkPointNavigator
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x004F6100 File Offset: 0x004F4300
		public static int CurrentPoint
		{
			get
			{
				return UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].CurrentPoint;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x004F6118 File Offset: 0x004F4318
		public static bool Available
		{
			get
			{
				return Main.playerInventory || Main.ingameOptionsWindow || Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1 || Main.mapFullscreen || Main.clothesWindow || Main.MenuUI.IsVisible || Main.InGameUI.IsVisible;
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x004F617F File Offset: 0x004F437F
		public static void SuggestUsage(int PointID)
		{
			if (!UILinkPointNavigator.Points.ContainsKey(PointID))
			{
				return;
			}
			UILinkPointNavigator._suggestedPointID = new int?(PointID);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x004F619C File Offset: 0x004F439C
		public static void ConsumeSuggestion()
		{
			if (UILinkPointNavigator._suggestedPointID == null)
			{
				return;
			}
			int value = UILinkPointNavigator._suggestedPointID.Value;
			UILinkPointNavigator.ClearSuggestion();
			UILinkPointNavigator.CurrentPage = UILinkPointNavigator.Points[value].Page;
			UILinkPointNavigator.OverridePoint = value;
			UILinkPointNavigator.ProcessChanges();
			PlayerInput.Triggers.Current.UsedMovementKey = true;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x004F61F6 File Offset: 0x004F43F6
		public static void ClearSuggestion()
		{
			UILinkPointNavigator._suggestedPointID = null;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x004F6204 File Offset: 0x004F4404
		public static void GoToDefaultPage(int specialFlag = 0)
		{
			TileEntity tileEntity = Main.LocalPlayer.tileEntityAnchor.GetTileEntity();
			if (Main.MenuUI.IsVisible)
			{
				UILinkPointNavigator.CurrentPage = 1004;
				return;
			}
			if (Main.InGameUI.IsVisible || specialFlag == 1)
			{
				UILinkPointNavigator.CurrentPage = 1004;
				return;
			}
			if (Main.gameMenu)
			{
				UILinkPointNavigator.CurrentPage = 1000;
				return;
			}
			if (Main.ingameOptionsWindow)
			{
				UILinkPointNavigator.CurrentPage = 1001;
				return;
			}
			if (Main.CreativeMenu.Enabled)
			{
				UILinkPointNavigator.CurrentPage = 1005;
				return;
			}
			if (NewCraftingUI.Visible)
			{
				UILinkPointNavigator.CurrentPage = 24;
				return;
			}
			if (Main.hairWindow)
			{
				UILinkPointNavigator.CurrentPage = 12;
				return;
			}
			if (Main.clothesWindow)
			{
				UILinkPointNavigator.CurrentPage = 15;
				return;
			}
			if (Main.npcShop != 0)
			{
				UILinkPointNavigator.CurrentPage = 13;
				return;
			}
			if (Main.InGuideCraftMenu)
			{
				UILinkPointNavigator.CurrentPage = 0;
				return;
			}
			if (Main.InReforgeMenu)
			{
				UILinkPointNavigator.CurrentPage = 0;
				return;
			}
			if (Main.player[Main.myPlayer].chest != -1)
			{
				UILinkPointNavigator.CurrentPage = 4;
				return;
			}
			if (tileEntity is TEDisplayDoll)
			{
				UILinkPointNavigator.CurrentPage = 20;
				return;
			}
			if (tileEntity is TEHatRack)
			{
				UILinkPointNavigator.CurrentPage = 21;
				return;
			}
			if (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1)
			{
				UILinkPointNavigator.CurrentPage = 1003;
				return;
			}
			UILinkPointNavigator.CurrentPage = 0;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x004F6358 File Offset: 0x004F4558
		public static void Update()
		{
			bool inUse = UILinkPointNavigator.InUse;
			UILinkPointNavigator.InUse = false;
			bool flag = true;
			if (flag)
			{
				InputMode currentInputMode = PlayerInput.CurrentInputMode;
				if (currentInputMode <= InputMode.Mouse && !Main.gameMenu)
				{
					flag = false;
				}
			}
			if (flag && PlayerInput.NavigatorRebindingLock > 0)
			{
				flag = false;
			}
			if (flag && !Main.gameMenu && !PlayerInput.UsingGamepadUI)
			{
				flag = false;
			}
			if (flag && !Main.gameMenu && PlayerInput.InBuildingMode)
			{
				flag = false;
			}
			if (flag && !Main.gameMenu && !UILinkPointNavigator.Available)
			{
				flag = false;
			}
			if (flag && Main.gameMenu && Main.MenuUI.IsVisible && Main.MenuUI.CurrentState != null && Main.MenuUI.CurrentState.NoGamepadSupport)
			{
				flag = false;
			}
			bool flag2 = false;
			UILinkPage uilinkPage;
			if (!UILinkPointNavigator.Pages.TryGetValue(UILinkPointNavigator.CurrentPage, out uilinkPage))
			{
				flag2 = true;
			}
			else if (!uilinkPage.IsValid())
			{
				flag2 = true;
			}
			if (flag2)
			{
				UILinkPointNavigator.GoToDefaultPage(0);
				UILinkPointNavigator.ProcessChanges();
				flag = false;
			}
			if (inUse != flag)
			{
				if (!flag)
				{
					uilinkPage.Leave();
					UILinkPointNavigator.GoToDefaultPage(0);
					UILinkPointNavigator.ProcessChanges();
				}
				else
				{
					UILinkPointNavigator.GoToDefaultPage(0);
					UILinkPointNavigator.ProcessChanges();
					UILinkPointNavigator.ConsumeSuggestion();
					uilinkPage.Enter();
				}
				if (flag)
				{
					if (!PlayerInput.SteamDeckIsUsed || PlayerInput.PreventCursorModeSwappingToGamepad)
					{
						Main.player[Main.myPlayer].releaseInventory = false;
					}
					Main.player[Main.myPlayer].releaseUseTile = false;
					PlayerInput.LockGamepadTileUseButton = true;
				}
				if (!Main.gameMenu)
				{
					if (flag)
					{
						PlayerInput.NavigatorCachePosition();
					}
					else
					{
						PlayerInput.NavigatorUnCachePosition();
					}
				}
			}
			UILinkPointNavigator.ClearSuggestion();
			if (!flag)
			{
				return;
			}
			UILinkPointNavigator.InUse = true;
			UILinkPointNavigator.OverridePoint = -1;
			if (UILinkPointNavigator.PageLeftCD > 0)
			{
				UILinkPointNavigator.PageLeftCD--;
			}
			if (UILinkPointNavigator.PageRightCD > 0)
			{
				UILinkPointNavigator.PageRightCD--;
			}
			Vector2 navigatorDirections = PlayerInput.Triggers.Current.GetNavigatorDirections();
			object obj = PlayerInput.Triggers.Current.HotbarMinus && !PlayerInput.Triggers.Current.HotbarPlus;
			bool flag3 = PlayerInput.Triggers.Current.HotbarPlus && !PlayerInput.Triggers.Current.HotbarMinus;
			object obj2 = obj;
			if (obj2 == null)
			{
				UILinkPointNavigator.PageLeftCD = 0;
			}
			if (!flag3)
			{
				UILinkPointNavigator.PageRightCD = 0;
			}
			object obj3 = obj2 != null && UILinkPointNavigator.PageLeftCD == 0;
			flag3 = flag3 && UILinkPointNavigator.PageRightCD == 0;
			if (UILinkPointNavigator.LastInput.X != navigatorDirections.X)
			{
				UILinkPointNavigator.XCooldown = 0;
			}
			if (UILinkPointNavigator.LastInput.Y != navigatorDirections.Y)
			{
				UILinkPointNavigator.YCooldown = 0;
			}
			if (UILinkPointNavigator.XCooldown > 0)
			{
				UILinkPointNavigator.XCooldown--;
			}
			if (UILinkPointNavigator.YCooldown > 0)
			{
				UILinkPointNavigator.YCooldown--;
			}
			UILinkPointNavigator.LastInput = navigatorDirections;
			object obj4 = obj3;
			if (obj4 != null)
			{
				UILinkPointNavigator.PageLeftCD = 16;
			}
			if (flag3)
			{
				UILinkPointNavigator.PageRightCD = 16;
			}
			UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].Update();
			int num = 10;
			if (!Main.gameMenu && Main.playerInventory && !Main.ingameOptionsWindow && !Main.inFancyUI && (UILinkPointNavigator.CurrentPage == 0 || UILinkPointNavigator.CurrentPage == 4 || UILinkPointNavigator.CurrentPage == 2 || UILinkPointNavigator.CurrentPage == 1 || UILinkPointNavigator.CurrentPage == 20 || UILinkPointNavigator.CurrentPage == 21))
			{
				num = PlayerInput.CurrentProfile.InventoryMoveCD;
			}
			if (navigatorDirections.X == -1f && UILinkPointNavigator.XCooldown == 0)
			{
				UILinkPointNavigator.XCooldown = num;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelLeft();
			}
			if (navigatorDirections.X == 1f && UILinkPointNavigator.XCooldown == 0)
			{
				UILinkPointNavigator.XCooldown = num;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelRight();
			}
			if (navigatorDirections.Y == -1f && UILinkPointNavigator.YCooldown == 0)
			{
				UILinkPointNavigator.YCooldown = num;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelUp();
			}
			if (navigatorDirections.Y == 1f && UILinkPointNavigator.YCooldown == 0)
			{
				UILinkPointNavigator.YCooldown = num;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelDown();
			}
			UILinkPointNavigator.XCooldown = (UILinkPointNavigator.YCooldown = Math.Max(UILinkPointNavigator.XCooldown, UILinkPointNavigator.YCooldown));
			if (obj4 != null)
			{
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].SwapPageLeft();
			}
			if (flag3)
			{
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].SwapPageRight();
			}
			if (PlayerInput.Triggers.Current.UsedMovementKey)
			{
				Vector2 position = UILinkPointNavigator.Points[UILinkPointNavigator.CurrentPoint].Position;
				Vector2 vector = new Vector2((float)PlayerInput.MouseX, (float)PlayerInput.MouseY);
				float num2 = 0.3f;
				if (PlayerInput.InvisibleGamepadInMenus)
				{
					num2 = 1f;
				}
				Vector2 vector2 = Vector2.Lerp(vector, position, num2);
				if (Main.gameMenu)
				{
					if (Math.Abs(vector2.X - position.X) <= 5f)
					{
						vector2.X = position.X;
					}
					if (Math.Abs(vector2.Y - position.Y) <= 5f)
					{
						vector2.Y = position.Y;
					}
				}
				PlayerInput.MouseX = (int)vector2.X;
				PlayerInput.MouseY = (int)vector2.Y;
			}
			UILinkPointNavigator.ResetFlagsEnd();
			if (DebugOptions.DrawLinkPoints)
			{
				UILinkPointNavigator.DrawLinks();
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x004F6853 File Offset: 0x004F4A53
		public static void ResetFlagsEnd()
		{
			UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 0;
			UILinkPointNavigator.Shortcuts.BackButtonLock = false;
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 0;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x004F6868 File Offset: 0x004F4A68
		public static string GetInstructions()
		{
			UILinkPage uilinkPage = UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage];
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[UILinkPointNavigator.CurrentPoint];
			if (UILinkPointNavigator._suggestedPointID != null)
			{
				UILinkPointNavigator.SwapToSuggestion();
				uilinkPoint = UILinkPointNavigator.Points[UILinkPointNavigator._suggestedPointID.Value];
				uilinkPage = UILinkPointNavigator.Pages[uilinkPoint.Page];
				UILinkPointNavigator.CurrentPage = uilinkPage.ID;
				uilinkPage.CurrentPoint = UILinkPointNavigator._suggestedPointID.Value;
			}
			string text = uilinkPage.SpecialInteractions();
			if ((PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Gamepad && PlayerInput.Triggers.Current.UsedMovementKey && UILinkPointNavigator.InUse) || UILinkPointNavigator._suggestedPointID != null)
			{
				text += uilinkPoint.SpecialInteractions();
			}
			text += uilinkPage.SpecialInteractionsLate();
			UILinkPointNavigator.ConsumeSuggestionSwap();
			return text;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x004F693D File Offset: 0x004F4B3D
		public static void SwapToSuggestion()
		{
			UILinkPointNavigator._preSuggestionPoint = new int?(UILinkPointNavigator.CurrentPoint);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x004F6950 File Offset: 0x004F4B50
		public static void ConsumeSuggestionSwap()
		{
			if (UILinkPointNavigator._preSuggestionPoint != null)
			{
				int value = UILinkPointNavigator._preSuggestionPoint.Value;
				UILinkPointNavigator.CurrentPage = UILinkPointNavigator.Points[value].Page;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].CurrentPoint = value;
			}
			UILinkPointNavigator._preSuggestionPoint = null;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x004F69A9 File Offset: 0x004F4BA9
		public static void ForceMovementCooldown(int time)
		{
			UILinkPointNavigator.LastInput = PlayerInput.Triggers.Current.GetNavigatorDirections();
			UILinkPointNavigator.XCooldown = time;
			UILinkPointNavigator.YCooldown = time;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x004F69CB File Offset: 0x004F4BCB
		public static void SetPosition(int ID, Vector2 Position)
		{
			UILinkPointNavigator.Points[ID].Position = Position * Main.UIScale;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x004F69E8 File Offset: 0x004F4BE8
		public static Vector2 GetPosition(int ID)
		{
			UILinkPoint uilinkPoint;
			Vector2 vector;
			if (!UILinkPointNavigator.Points.TryGetValue(ID, out uilinkPoint))
			{
				vector = Vector2.Zero;
			}
			else
			{
				vector = uilinkPoint.Position;
			}
			if (vector == Vector2.Zero)
			{
				if (ID >= 180 && ID <= 184)
				{
					vector = UILinkPointNavigator.GetPosition(ID - 180 + 100);
				}
				else if (ID >= 185 && ID <= 189)
				{
					vector = UILinkPointNavigator.GetPosition(ID - 185 + 110);
				}
			}
			return vector / Main.UIScale;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x004F6A70 File Offset: 0x004F4C70
		public static void RegisterPage(UILinkPage page, int ID, bool automatedDefault = true)
		{
			if (automatedDefault)
			{
				page.DefaultPoint = page.LinkMap.Keys.First<int>();
			}
			page.CurrentPoint = page.DefaultPoint;
			page.ID = ID;
			UILinkPointNavigator.Pages.Add(page.ID, page);
			foreach (KeyValuePair<int, UILinkPoint> keyValuePair in page.LinkMap)
			{
				keyValuePair.Value.SetPage(ID);
				UILinkPointNavigator.Points.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x004F6B20 File Offset: 0x004F4D20
		public static void ChangePage(int PageID)
		{
			if (UILinkPointNavigator.Pages.ContainsKey(PageID) && UILinkPointNavigator.Pages[PageID].CanEnter())
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				UILinkPointNavigator.CurrentPage = PageID;
				UILinkPointNavigator.ProcessChanges();
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x004F6B6C File Offset: 0x004F4D6C
		public static void ChangePoint(int PointID)
		{
			if (UILinkPointNavigator.Points.ContainsKey(PointID))
			{
				UILinkPointNavigator.CurrentPage = UILinkPointNavigator.Points[PointID].Page;
				UILinkPointNavigator.OverridePoint = PointID;
				UILinkPointNavigator.ProcessChanges();
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x004F6B9C File Offset: 0x004F4D9C
		public static void ProcessChanges()
		{
			UILinkPage uilinkPage = UILinkPointNavigator.Pages[UILinkPointNavigator.OldPage];
			if (UILinkPointNavigator.OldPage != UILinkPointNavigator.CurrentPage)
			{
				uilinkPage.Leave();
				if (!UILinkPointNavigator.Pages.TryGetValue(UILinkPointNavigator.CurrentPage, out uilinkPage))
				{
					UILinkPointNavigator.GoToDefaultPage(0);
					UILinkPointNavigator.ProcessChanges();
					UILinkPointNavigator.OverridePoint = -1;
				}
				uilinkPage.CurrentPoint = uilinkPage.DefaultPoint;
				uilinkPage.Enter();
				uilinkPage.Update();
				UILinkPointNavigator.OldPage = UILinkPointNavigator.CurrentPage;
			}
			if (UILinkPointNavigator.OverridePoint != -1 && uilinkPage.LinkMap.ContainsKey(UILinkPointNavigator.OverridePoint))
			{
				uilinkPage.CurrentPoint = UILinkPointNavigator.OverridePoint;
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x004F6C38 File Offset: 0x004F4E38
		private static void DrawLinks()
		{
			UILinkPoint uilinkPoint;
			if (!UILinkPointNavigator.Points.TryGetValue(UILinkPointNavigator.CurrentPoint, out uilinkPoint))
			{
				return;
			}
			UILinkPointNavigator._visited.Clear();
			UILinkPointNavigator._visited.Add(uilinkPoint);
			UILinkPointNavigator._queue.Clear();
			UILinkPointNavigator._queue.Enqueue(uilinkPoint);
			while (UILinkPointNavigator._queue.Any<UILinkPoint>())
			{
				UILinkPoint uilinkPoint2 = UILinkPointNavigator._queue.Dequeue();
				UILinkPointNavigator.DrawLink(uilinkPoint2, uilinkPoint2.Up, new Vector2(0f, -1f), new Color(120, 0, 20), new Color(255, 0, 255));
				UILinkPointNavigator.DrawLink(uilinkPoint2, uilinkPoint2.Down, new Vector2(0f, 1f), new Color(0, 0, 255), new Color(0, 255, 255));
				UILinkPointNavigator.DrawLink(uilinkPoint2, uilinkPoint2.Left, new Vector2(-1f, 0f), new Color(0, 100, 0), new Color(50, 205, 50));
				UILinkPointNavigator.DrawLink(uilinkPoint2, uilinkPoint2.Right, new Vector2(1f, 0f), new Color(100, 100, 0), new Color(255, 215, 0));
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x004F6D74 File Offset: 0x004F4F74
		private static void DrawLink(UILinkPoint src, int targetId, Vector2 dir, Color colorStart, Color colorEnd)
		{
			UILinkPoint uilinkPoint;
			if (!UILinkPointNavigator.Points.TryGetValue(targetId, out uilinkPoint) || uilinkPoint.Position == Vector2.Zero)
			{
				return;
			}
			if (UILinkPointNavigator._visited.Add(uilinkPoint))
			{
				UILinkPointNavigator._queue.Enqueue(uilinkPoint);
			}
			Vector2 vector = dir.RotatedBy(1.5707963267948966, default(Vector2));
			Vector2 vector2 = src.Position / Main.UIScale + vector * 2f;
			Vector2 vector3 = uilinkPoint.Position / Main.UIScale + vector * 2f;
			if (Vector2.Dot(vector3 - vector2, dir) < 0f)
			{
				DebugLineDraw.UI.AddLine(vector2, vector2 += (vector + dir * 2f) * 2f * 2f, colorStart, default(Color), 1, 1f);
				DebugLineDraw.UI.AddLine(vector3, vector3 += (vector - dir * 2f) * 2f * 2f, colorEnd, default(Color), 1, 1f);
			}
			DebugLineDraw.UI.AddLine(vector2, vector3, colorStart, colorEnd, 1, 1f);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0000357B File Offset: 0x0000177B
		public UILinkPointNavigator()
		{
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x004F6ED8 File Offset: 0x004F50D8
		// Note: this type is marked as 'beforefieldinit'.
		static UILinkPointNavigator()
		{
		}

		// Token: 0x040014E7 RID: 5351
		public static Dictionary<int, UILinkPage> Pages = new Dictionary<int, UILinkPage>();

		// Token: 0x040014E8 RID: 5352
		public static Dictionary<int, UILinkPoint> Points = new Dictionary<int, UILinkPoint>();

		// Token: 0x040014E9 RID: 5353
		public static int CurrentPage = 1000;

		// Token: 0x040014EA RID: 5354
		public static int OldPage = 1000;

		// Token: 0x040014EB RID: 5355
		private static int XCooldown;

		// Token: 0x040014EC RID: 5356
		private static int YCooldown;

		// Token: 0x040014ED RID: 5357
		private static Vector2 LastInput;

		// Token: 0x040014EE RID: 5358
		private static int PageLeftCD;

		// Token: 0x040014EF RID: 5359
		private static int PageRightCD;

		// Token: 0x040014F0 RID: 5360
		public static bool InUse;

		// Token: 0x040014F1 RID: 5361
		public static int OverridePoint = -1;

		// Token: 0x040014F2 RID: 5362
		private static int? _suggestedPointID;

		// Token: 0x040014F3 RID: 5363
		private static int? _preSuggestionPoint;

		// Token: 0x040014F4 RID: 5364
		private static HashSet<UILinkPoint> _visited = new HashSet<UILinkPoint>();

		// Token: 0x040014F5 RID: 5365
		private static Queue<UILinkPoint> _queue = new Queue<UILinkPoint>();

		// Token: 0x0200071C RID: 1820
		public static class Shortcuts
		{
			// Token: 0x06004051 RID: 16465 RVA: 0x0069D894 File Offset: 0x0069BA94
			// Note: this type is marked as 'beforefieldinit'.
			static Shortcuts()
			{
			}

			// Token: 0x04006915 RID: 26901
			public static int NPCS_IconsPerColumn = 100;

			// Token: 0x04006916 RID: 26902
			public static int NPCS_IconsTotal = 0;

			// Token: 0x04006917 RID: 26903
			public static int NPCS_HoveredBanner = -2;

			// Token: 0x04006918 RID: 26904
			public static int NPCS_SelectedNPC = -2;

			// Token: 0x04006919 RID: 26905
			public static bool NPCS_IconsDisplay = false;

			// Token: 0x0400691A RID: 26906
			public static int CRAFT_IconsPerRow = 100;

			// Token: 0x0400691B RID: 26907
			public static int CRAFT_IconsPerColumn = 100;

			// Token: 0x0400691C RID: 26908
			public static int CRAFT_CurrentIngredientsCount = 0;

			// Token: 0x0400691D RID: 26909
			public static int CRAFT_CurrentRecipeBig = 0;

			// Token: 0x0400691E RID: 26910
			public static int CRAFT_CurrentRecipeSmall = 0;

			// Token: 0x0400691F RID: 26911
			public static int NewCraftingUI_MaterialIndex = 0;

			// Token: 0x04006920 RID: 26912
			public static bool NPCCHAT_ButtonsNew = false;

			// Token: 0x04006921 RID: 26913
			public static int NPCCHAT_ButtonsCount = 1;

			// Token: 0x04006922 RID: 26914
			public static bool NPCCHAT_ButtonsLeft = false;

			// Token: 0x04006923 RID: 26915
			public static bool NPCCHAT_ButtonsMiddle = false;

			// Token: 0x04006924 RID: 26916
			public static bool NPCCHAT_ButtonsRight = false;

			// Token: 0x04006925 RID: 26917
			public static bool NPCCHAT_ButtonsRight2 = false;

			// Token: 0x04006926 RID: 26918
			public static int INGAMEOPTIONS_BUTTONS_LEFT = 0;

			// Token: 0x04006927 RID: 26919
			public static int INGAMEOPTIONS_BUTTONS_RIGHT = 0;

			// Token: 0x04006928 RID: 26920
			public static bool ItemSlotShouldHighlightAsSelected = false;

			// Token: 0x04006929 RID: 26921
			public static bool ItemSlotShouldHighlightAsPreviouslySelected = false;

			// Token: 0x0400692A RID: 26922
			public static int OPTIONS_BUTTON_SPECIALFEATURE;

			// Token: 0x0400692B RID: 26923
			public static int BackButtonCommand;

			// Token: 0x0400692C RID: 26924
			public static bool BackButtonInUse = false;

			// Token: 0x0400692D RID: 26925
			public static bool BackButtonLock;

			// Token: 0x0400692E RID: 26926
			public static int FANCYUI_HIGHEST_INDEX = 1;

			// Token: 0x0400692F RID: 26927
			public static int FANCYUI_SPECIAL_INSTRUCTIONS = 0;

			// Token: 0x04006930 RID: 26928
			public static int INFOACCCOUNT = 0;

			// Token: 0x04006931 RID: 26929
			public static int BUILDERACCCOUNT = 0;

			// Token: 0x04006932 RID: 26930
			public static int BUFFS_PER_COLUMN = 0;

			// Token: 0x04006933 RID: 26931
			public static int BUFFS_DRAWN = 0;

			// Token: 0x04006934 RID: 26932
			public static int INV_MOVE_OPTION_CD = 0;
		}
	}
}
