using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Achievements;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003B4 RID: 948
	public class UIAchievementsMenu : UIState
	{
		// Token: 0x06002CB2 RID: 11442 RVA: 0x0059DA7C File Offset: 0x0059BC7C
		public void InitializePage()
		{
			base.RemoveAllChildren();
			this._categoryButtons.Clear();
			this._achievementElements.Clear();
			this._achievementsList = null;
			bool flag = true;
			int num = flag.ToInt() * 100;
			UIElement uielement = new UIElement();
			uielement.Width.Set(0f, 0.8f);
			uielement.MaxWidth.Set(800f + (float)num, 0f);
			uielement.MinWidth.Set(600f + (float)num, 0f);
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
			this._achievementsList = new UIList();
			this._achievementsList.Width.Set(-25f, 1f);
			this._achievementsList.Height.Set(-50f, 1f);
			this._achievementsList.Top.Set(50f, 0f);
			this._achievementsList.ListPadding = 5f;
			uipanel.Append(this._achievementsList);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Achievements"), 1f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-33f, 0f);
			uitextPanel.SetPadding(13f);
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
			this._backpanel = uitextPanel2;
			List<Achievement> list = Main.Achievements.CreateAchievementsList();
			for (int i = 0; i < list.Count; i++)
			{
				UIAchievementListItem uiachievementListItem = new UIAchievementListItem(list[i], flag);
				this._achievementsList.Add(uiachievementListItem);
				this._achievementElements.Add(uiachievementListItem);
			}
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(-50f, 1f);
			uiscrollbar.Top.Set(50f, 0f);
			uiscrollbar.HAlign = 1f;
			uipanel.Append(uiscrollbar);
			this._achievementsList.SetScrollbar(uiscrollbar);
			UIElement uielement2 = new UIElement();
			uielement2.Width.Set(0f, 1f);
			uielement2.Height.Set(32f, 0f);
			uielement2.Top.Set(10f, 0f);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Categories", 1);
			for (int j = 0; j < 4; j++)
			{
				UIToggleImage uitoggleImage = new UIToggleImage(asset, 32, 32, new Point(34 * j, 0), new Point(34 * j, 34));
				uitoggleImage.Left.Set((float)(j * 36 + 8), 0f);
				uitoggleImage.SetState(true);
				uitoggleImage.OnLeftClick += this.FilterList;
				this._categoryButtons.Add(uitoggleImage);
				uielement2.Append(uitoggleImage);
			}
			uipanel.Append(uielement2);
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x0059DEC8 File Offset: 0x0059C0C8
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			for (int i = 0; i < this._categoryButtons.Count; i++)
			{
				if (this._categoryButtons[i].IsMouseHovering)
				{
					string text;
					switch (i)
					{
					case -1:
						text = Language.GetTextValue("Achievements.NoCategory");
						break;
					case 0:
						text = Language.GetTextValue("Achievements.SlayerCategory");
						break;
					case 1:
						text = Language.GetTextValue("Achievements.CollectorCategory");
						break;
					case 2:
						text = Language.GetTextValue("Achievements.ExplorerCategory");
						break;
					case 3:
						text = Language.GetTextValue("Achievements.ChallengerCategory");
						break;
					default:
						text = Language.GetTextValue("Achievements.NoCategory");
						break;
					}
					float x = FontAssets.MouseText.Value.MeasureString(text).X;
					Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY) + new Vector2(16f);
					if (vector.Y > (float)(Main.screenHeight - 30))
					{
						vector.Y = (float)(Main.screenHeight - 30);
					}
					if (vector.X > (float)Main.screenWidth - x)
					{
						vector.X = (float)(Main.screenWidth - 460);
					}
					Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, vector.X, vector.Y, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, Vector2.Zero, 1f);
					break;
				}
			}
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x0059E048 File Offset: 0x0059C248
		public void GotoAchievement(Achievement achievement)
		{
			this._achievementsList.Goto(delegate(UIElement element)
			{
				UIAchievementListItem uiachievementListItem = element as UIAchievementListItem;
				return uiachievementListItem != null && uiachievementListItem.GetAchievement() == achievement;
			});
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x0058DFB6 File Offset: 0x0058C1B6
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.menuMode = 0;
			IngameFancyUI.Close(false);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x0059E07C File Offset: 0x0059C27C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x0058539D File Offset: 0x0058359D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x0059E0D4 File Offset: 0x0059C2D4
		private void FilterList(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._achievementsList.Clear();
			foreach (UIAchievementListItem uiachievementListItem in this._achievementElements)
			{
				if (this._categoryButtons[(int)uiachievementListItem.GetAchievement().Category].IsOn)
				{
					this._achievementsList.Add(uiachievementListItem);
				}
			}
			this.Recalculate();
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x0059E170 File Offset: 0x0059C370
		public override void OnActivate()
		{
			this.InitializePage();
			if (Main.gameMenu)
			{
				this._outerContainer.Top.Set(220f, 0f);
				this._outerContainer.Height.Set(-220f, 1f);
			}
			else
			{
				this._outerContainer.Top.Set(120f, 0f);
				this._outerContainer.Height.Set(-120f, 1f);
			}
			this._achievementsList.UpdateOrder();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3002);
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x0059E210 File Offset: 0x0059C410
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 3;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backpanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 1, this._outerContainer.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[num2];
			uilinkPoint.Unlink();
			uilinkPoint.Up = num2 + 1;
			num2++;
			UILinkPoint uilinkPoint2 = UILinkPointNavigator.Points[num2];
			uilinkPoint2.Unlink();
			uilinkPoint2.Up = num2 + 1;
			uilinkPoint2.Down = num2 - 1;
			for (int i = 0; i < this._categoryButtons.Count; i++)
			{
				num2++;
				UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num2;
				UILinkPointNavigator.SetPosition(num2, this._categoryButtons[i].GetInnerDimensions().ToRectangle().Center.ToVector2());
				UILinkPoint uilinkPoint3 = UILinkPointNavigator.Points[num2];
				uilinkPoint3.Unlink();
				uilinkPoint3.Left = ((i == 0) ? (-3) : (num2 - 1));
				uilinkPoint3.Right = ((i == this._categoryButtons.Count - 1) ? (-4) : (num2 + 1));
				uilinkPoint3.Down = num;
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x0059E355 File Offset: 0x0059C555
		public UIAchievementsMenu()
		{
		}

		// Token: 0x04005439 RID: 21561
		private UIList _achievementsList;

		// Token: 0x0400543A RID: 21562
		private List<UIAchievementListItem> _achievementElements = new List<UIAchievementListItem>();

		// Token: 0x0400543B RID: 21563
		private List<UIToggleImage> _categoryButtons = new List<UIToggleImage>();

		// Token: 0x0400543C RID: 21564
		private UIElement _backpanel;

		// Token: 0x0400543D RID: 21565
		private UIElement _outerContainer;

		// Token: 0x0200091D RID: 2333
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x060047C9 RID: 18377 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x060047CA RID: 18378 RVA: 0x006CC4E0 File Offset: 0x006CA6E0
			internal bool <GotoAchievement>b__0(UIElement element)
			{
				UIAchievementListItem uiachievementListItem = element as UIAchievementListItem;
				return uiachievementListItem != null && uiachievementListItem.GetAchievement() == this.achievement;
			}

			// Token: 0x040074D0 RID: 29904
			public Achievement achievement;
		}
	}
}
