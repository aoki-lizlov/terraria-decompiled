using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003A3 RID: 931
	public class UIReportsPage : UIState
	{
		// Token: 0x06002ABA RID: 10938 RVA: 0x00587160 File Offset: 0x00585360
		public UIReportsPage(UIState stateToGoBackTo, int menuIdToGoBackTo, List<IProvideReports> reporters)
		{
			this._previousUIState = stateToGoBackTo;
			this._menuIdToGoBackTo = menuIdToGoBackTo;
			this._reporters = reporters;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0058717D File Offset: 0x0058537D
		public override void OnInitialize()
		{
			this.BuildPage();
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x00587188 File Offset: 0x00585388
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
				Height = new StyleDimension(28f, 0f)
			};
			uielement3.SetPadding(0f);
			uielement2.Append(uielement3);
			uielement3.Append(new UIText(Language.GetTextValue("UI.ReportsPage"), 0.7f, true)
			{
				HAlign = 0.5f,
				VAlign = 0f
			});
			UIElement uielement4 = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Top = new StyleDimension(-2f, 0f)
			};
			uielement2.Append(uielement4);
			this._container = uielement4;
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixels(2f)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.5f;
			uielement4.Append(uislicedImage);
			UIList uilist = new UIList
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				PaddingRight = 20f
			};
			uilist.ListPadding = 40f;
			uilist.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
			UIElement uielement5 = new UIElement();
			uilist.Add(uielement5);
			this.PopulateLogs(uilist);
			uielement4.Append(uilist);
			this._list = uilist;
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(0f, 1f);
			uiscrollbar.HAlign = 1f;
			this._scrollbar = uiscrollbar;
			uilist.SetScrollbar(uiscrollbar);
			uiscrollbar.GoToBottom();
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel.Width.Set(-10f, 0.5f);
			uitextPanel.Height.Set(50f, 0f);
			uitextPanel.VAlign = 1f;
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-45f, 0f);
			uitextPanel.OnMouseOver += UIReportsPage.FadedMouseOver;
			uitextPanel.OnMouseOut += UIReportsPage.FadedMouseOut;
			uitextPanel.OnLeftClick += this.GoBackClick;
			uitextPanel.SetSnapPoint("GoBack", 0, null, null);
			uielement.Append(uitextPanel);
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x00009E46 File Offset: 0x00008046
		private void ManualIfnoSortingMethod(List<UIElement> list)
		{
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x005875F0 File Offset: 0x005857F0
		private void PopulateLogs(UIList listContents)
		{
			List<IssueReport> list = (from report in this._reporters.SelectMany((IProvideReports reporter) => reporter.GetReports())
				orderby report.timeReported
				select report).ToList<IssueReport>();
			if (list.Count == 0)
			{
				UIText uitext = new UIText(Language.GetTextValue("Workshop.ReportLogsInitialMessage"), 1f, false)
				{
					HAlign = 0f,
					VAlign = 0f,
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Height = StyleDimension.FromPixelsAndPercent(0f, 0f),
					IsWrapped = true,
					WrappedTextBottomPadding = 0f,
					TextOriginX = 0.5f,
					TextColor = Color.Gray
				};
				listContents.Add(uitext);
			}
			for (int i = 0; i < list.Count; i++)
			{
				UIText uitext2 = new UIText(list[i].reportText, 1f, false)
				{
					HAlign = 0f,
					VAlign = 0f,
					Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
					Height = StyleDimension.FromPixelsAndPercent(0f, 0f),
					IsWrapped = true,
					WrappedTextBottomPadding = 0f,
					TextOriginX = 0f
				};
				listContents.Add(uitext2);
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
				UIImage uiimage = new UIImage(asset)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Height = StyleDimension.FromPixels((float)asset.Height()),
					ScaleToFit = true,
					VAlign = 1f
				};
				uitext2.Append(uiimage);
			}
			UIElement uielement = new UIElement();
			listContents.Add(uielement);
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x005877E4 File Offset: 0x005859E4
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

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00587892 File Offset: 0x00585A92
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.MenuUI.SetState(this._previousUIState);
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = this._menuIdToGoBackTo;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x005878C4 File Offset: 0x00585AC4
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00587919 File Offset: 0x00585B19
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x00587958 File Offset: 0x00585B58
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x00587968 File Offset: 0x00585B68
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

		// Token: 0x04005337 RID: 21303
		private UIState _previousUIState;

		// Token: 0x04005338 RID: 21304
		private int _menuIdToGoBackTo;

		// Token: 0x04005339 RID: 21305
		private UIElement _container;

		// Token: 0x0400533A RID: 21306
		private UIList _list;

		// Token: 0x0400533B RID: 21307
		private UIScrollbar _scrollbar;

		// Token: 0x0400533C RID: 21308
		private bool _isScrollbarAttached;

		// Token: 0x0400533D RID: 21309
		private const string _backPointName = "GoBack";

		// Token: 0x0400533E RID: 21310
		private List<IProvideReports> _reporters;

		// Token: 0x0400533F RID: 21311
		private UIGamepadHelper _helper;

		// Token: 0x020008F8 RID: 2296
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600472E RID: 18222 RVA: 0x006CB398 File Offset: 0x006C9598
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600472F RID: 18223 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004730 RID: 18224 RVA: 0x006CB3A4 File Offset: 0x006C95A4
			internal IEnumerable<IssueReport> <PopulateLogs>b__12_0(IProvideReports reporter)
			{
				return reporter.GetReports();
			}

			// Token: 0x06004731 RID: 18225 RVA: 0x006CB3AC File Offset: 0x006C95AC
			internal DateTime <PopulateLogs>b__12_1(IssueReport report)
			{
				return report.timeReported;
			}

			// Token: 0x04007406 RID: 29702
			public static readonly UIReportsPage.<>c <>9 = new UIReportsPage.<>c();

			// Token: 0x04007407 RID: 29703
			public static Func<IProvideReports, IEnumerable<IssueReport>> <>9__12_0;

			// Token: 0x04007408 RID: 29704
			public static Func<IssueReport, DateTime> <>9__12_1;
		}
	}
}
