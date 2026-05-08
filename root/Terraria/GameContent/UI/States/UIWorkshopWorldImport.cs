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
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003A7 RID: 935
	public class UIWorkshopWorldImport : UIState, IHaveBackButtonCommand
	{
		// Token: 0x06002AF7 RID: 10999 RVA: 0x005896D8 File Offset: 0x005878D8
		public UIWorkshopWorldImport(UIState uiStateToGoBackTo)
		{
			this._uiStateToGoBackTo = uiStateToGoBackTo;
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x005896F4 File Offset: 0x005878F4
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
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopImportWorld"), 0.8f, true);
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

		// Token: 0x06002AF9 RID: 11001 RVA: 0x00589988 File Offset: 0x00587B88
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

		// Token: 0x06002AFA RID: 11002 RVA: 0x00589A36 File Offset: 0x00587C36
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x00589A3E File Offset: 0x00587C3E
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._uiStateToGoBackTo);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x00589A68 File Offset: 0x00587C68
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x00588AB1 File Offset: 0x00586CB1
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x00589ABD File Offset: 0x00587CBD
		public override void OnActivate()
		{
			Main.LoadWorlds();
			this.UpdateWorkshopWorldList();
			this.UpdateWorldsList();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._worldList.Count == 0) ? 1 : 2));
			}
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x00589AF4 File Offset: 0x00587CF4
		public void UpdateWorkshopWorldList()
		{
			UIWorkshopWorldImport.WorkshopWorldList.Clear();
			if (SocialAPI.Workshop != null)
			{
				foreach (string text in SocialAPI.Workshop.GetListOfSubscribedWorldPaths())
				{
					UIWorkshopWorldImport.WorkshopWorldList.Add(WorldFile.GetAllMetadata(text, false));
				}
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x00589B68 File Offset: 0x00587D68
		private void UpdateWorldsList()
		{
			this._worldList.Clear();
			IEnumerable<WorldFileData> enumerable = from x in new List<WorldFileData>(UIWorkshopWorldImport.WorkshopWorldList)
				orderby x.IsFavorite descending, x.Name, x.GetFileName(true)
				select x;
			int num = 0;
			foreach (WorldFileData worldFileData in enumerable)
			{
				this._worldList.Add(new UIWorkshopImportWorldListItem(this, worldFileData, num++));
			}
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x00589C44 File Offset: 0x00587E44
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

		// Token: 0x06002B02 RID: 11010 RVA: 0x00589C64 File Offset: 0x00587E64
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
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
			SnapPoint[,] array = new SnapPoint[this._worldList.Count, 1];
			foreach (SnapPoint snapPoint in snapPoints.Where((SnapPoint a) => a.Name == "Import"))
			{
				array[snapPoint.Id, 0] = snapPoint;
			}
			num2 = num + 2;
			int[] array2 = new int[this._worldList.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = -1;
			}
			for (int k = 0; k < 1; k++)
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

		// Token: 0x06002B03 RID: 11011 RVA: 0x00589F14 File Offset: 0x00588114
		// Note: this type is marked as 'beforefieldinit'.
		static UIWorkshopWorldImport()
		{
		}

		// Token: 0x04005359 RID: 21337
		private UIList _worldList;

		// Token: 0x0400535A RID: 21338
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x0400535B RID: 21339
		private UIPanel _containerPanel;

		// Token: 0x0400535C RID: 21340
		private UIScrollbar _scrollbar;

		// Token: 0x0400535D RID: 21341
		private bool _isScrollbarAttached;

		// Token: 0x0400535E RID: 21342
		private List<Tuple<string, bool>> favoritesCache = new List<Tuple<string, bool>>();

		// Token: 0x0400535F RID: 21343
		private UIState _uiStateToGoBackTo;

		// Token: 0x04005360 RID: 21344
		public static List<WorldFileData> WorkshopWorldList = new List<WorldFileData>();

		// Token: 0x04005361 RID: 21345
		private bool skipDraw;

		// Token: 0x020008FC RID: 2300
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004741 RID: 18241 RVA: 0x006CB421 File Offset: 0x006C9621
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004742 RID: 18242 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004743 RID: 18243 RVA: 0x006CB408 File Offset: 0x006C9608
			internal bool <UpdateWorldsList>b__17_0(WorldFileData x)
			{
				return x.IsFavorite;
			}

			// Token: 0x06004744 RID: 18244 RVA: 0x006CB410 File Offset: 0x006C9610
			internal string <UpdateWorldsList>b__17_1(WorldFileData x)
			{
				return x.Name;
			}

			// Token: 0x06004745 RID: 18245 RVA: 0x006CB418 File Offset: 0x006C9618
			internal string <UpdateWorldsList>b__17_2(WorldFileData x)
			{
				return x.GetFileName(true);
			}

			// Token: 0x06004746 RID: 18246 RVA: 0x006CB42D File Offset: 0x006C962D
			internal bool <SetupGamepadPoints>b__20_0(SnapPoint a)
			{
				return a.Name == "Import";
			}

			// Token: 0x04007414 RID: 29716
			public static readonly UIWorkshopWorldImport.<>c <>9 = new UIWorkshopWorldImport.<>c();

			// Token: 0x04007415 RID: 29717
			public static Func<WorldFileData, bool> <>9__17_0;

			// Token: 0x04007416 RID: 29718
			public static Func<WorldFileData, string> <>9__17_1;

			// Token: 0x04007417 RID: 29719
			public static Func<WorldFileData, string> <>9__17_2;

			// Token: 0x04007418 RID: 29720
			public static Func<SnapPoint, bool> <>9__20_0;
		}
	}
}
