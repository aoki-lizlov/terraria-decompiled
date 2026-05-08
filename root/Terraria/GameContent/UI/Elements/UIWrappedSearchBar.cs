using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003C8 RID: 968
	public class UIWrappedSearchBar : UIElement
	{
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06002D55 RID: 11605 RVA: 0x005A3344 File Offset: 0x005A1544
		// (remove) Token: 0x06002D56 RID: 11606 RVA: 0x005A337C File Offset: 0x005A157C
		public event Action<string> OnSearchContentsChanged
		{
			[CompilerGenerated]
			add
			{
				Action<string> action = this.OnSearchContentsChanged;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> action3 = (Action<string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref this.OnSearchContentsChanged, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string> action = this.OnSearchContentsChanged;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> action3 = (Action<string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref this.OnSearchContentsChanged, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x005A33B1 File Offset: 0x005A15B1
		public bool HasContents
		{
			get
			{
				return this._searchBar.HasContents;
			}
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x005A33BE File Offset: 0x005A15BE
		public void SetContents(string contents, bool forced = false)
		{
			this._searchBar.SetContents(contents, forced);
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x005A33CD File Offset: 0x005A15CD
		public bool IsWritingText
		{
			get
			{
				return this._searchBar.IsWritingText;
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x005A33DA File Offset: 0x005A15DA
		public void ToggleTakingText()
		{
			this._searchBar.ToggleTakingText();
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x005A33E7 File Offset: 0x005A15E7
		// (set) Token: 0x06002D5C RID: 11612 RVA: 0x005A33F4 File Offset: 0x005A15F4
		public int MaxInputLength
		{
			get
			{
				return this._searchBar.MaxInputLength;
			}
			set
			{
				this._searchBar.MaxInputLength = value;
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x005A3402 File Offset: 0x005A1602
		public void SetSearchSnapPoint(string name, int id, Vector2? anchor = null, Vector2? offset = null)
		{
			this._searchButton.SetSnapPoint(name, id, anchor, offset);
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x005A3414 File Offset: 0x005A1614
		public UIWrappedSearchBar(Action goBackFromVirtualKeyboard, LocalizedText emptyText = null, UIWrappedSearchBar.ColorTheme theme = UIWrappedSearchBar.ColorTheme.Blue)
		{
			this._theme = theme;
			this._goBackFromVirtualKeyboard = goBackFromVirtualKeyboard;
			this._emptyText = ((emptyText != null) ? emptyText : Language.GetText("UI.PlayerNameSlot"));
			this.Height = new StyleDimension(24f, 0f);
			this.Width = new StyleDimension(0f, 1f);
			base.SetPadding(0f);
			this.AddSearchBar();
			this.SetContents(null, true);
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x005A348E File Offset: 0x005A168E
		public void HideSearchButton()
		{
			base.RemoveChild(this._searchButton);
			this._searchBoxPanel.Width = new StyleDimension(-3f, 1f);
			this.Recalculate();
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x005A34BC File Offset: 0x005A16BC
		private void AddSearchBar()
		{
			string text = "Images/UI/Bestiary/Button_Search";
			if (this._theme == UIWrappedSearchBar.ColorTheme.Red)
			{
				text = "Images/UI/Bestiary/Button_Search_2";
			}
			UIImageButton uiimageButton = new UIImageButton(Main.Assets.Request<Texture2D>(text, 1), null)
			{
				VAlign = 0.5f
			};
			this._searchButton = uiimageButton;
			uiimageButton.OnLeftClick += this.Click_SearchArea;
			uiimageButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border", 1), null);
			uiimageButton.SetVisibility(1f, 1f);
			base.Append(uiimageButton);
			UIPanel uipanel = new UIPanel
			{
				Width = new StyleDimension(-uiimageButton.Width.Pixels - 3f, 1f),
				Height = new StyleDimension(0f, 1f),
				VAlign = 0.5f,
				HAlign = 1f
			};
			this._searchBoxPanel = uipanel;
			uipanel.BackgroundColor = new Color(35, 40, 83);
			uipanel.BorderColor = new Color(35, 40, 83);
			if (this._theme == UIWrappedSearchBar.ColorTheme.Red)
			{
				uipanel.BackgroundColor = Utils.ShiftBlueToCyanTheme(uipanel.BackgroundColor);
				uipanel.BorderColor = Utils.ShiftBlueToCyanTheme(uipanel.BorderColor);
			}
			uipanel.SetPadding(0f);
			base.Append(uipanel);
			UISearchBar uisearchBar = new UISearchBar(this._emptyText, 0.8f)
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				HAlign = 0f,
				VAlign = 0.5f,
				Left = new StyleDimension(0f, 0f),
				IgnoresMouseInteraction = true
			};
			this._searchBar = uisearchBar;
			uipanel.OnLeftClick += this.Click_SearchArea;
			uipanel.OnRightClick += this.SearchBox_OnRightClick;
			uisearchBar.OnContentsChanged += this.UpdateSearchContents;
			uipanel.Append(uisearchBar);
			uisearchBar.OnStartTakingInput += this.OnStartTakingInput;
			uisearchBar.OnEndTakingInput += this.OnEndTakingInput;
			uisearchBar.OnNeedingVirtualKeyboard += this.OpenVirtualKeyboardWhenNeeded;
			UIImageButton uiimageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel", 1), null)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(-2f, 0f)
			};
			uiimageButton2.OnMouseOver += this.searchCancelButton_OnMouseOver;
			uiimageButton2.OnLeftClick += this.searchCancelButton_OnClick;
			uipanel.Append(uiimageButton2);
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x005A376F File Offset: 0x005A196F
		private void searchCancelButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this.HasContents)
			{
				this.SetContents(null, true);
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x00593C8E File Offset: 0x00591E8E
		private void searchCancelButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x005A37AC File Offset: 0x005A19AC
		private void OpenVirtualKeyboardWhenNeeded()
		{
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(this._emptyText.Value, this._searchString, new UIVirtualKeyboard.KeyboardSubmitEvent(this.SubmitVirtualText), new Action(this.GoBackFromVirtualKeyboard), 0, true, this.MaxInputLength);
			if (this.CustomOpenVirtualKeyboard != null)
			{
				this.CustomOpenVirtualKeyboard(uivirtualKeyboard);
				return;
			}
			UserInterface.ActiveInstance.SetState(uivirtualKeyboard);
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x005A3810 File Offset: 0x005A1A10
		private void SubmitVirtualText(string text)
		{
			this.SetContents(text.Trim(), false);
			this.GoBackFromVirtualKeyboard();
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x005A3825 File Offset: 0x005A1A25
		private void GoBackFromVirtualKeyboard()
		{
			this._searchBar.ToggleTakingText();
			this._goBackFromVirtualKeyboard();
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x005A383D File Offset: 0x005A1A3D
		private void OnStartTakingInput()
		{
			this._searchBoxPanel.BorderColor = Main.OurFavoriteColor;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x005A384F File Offset: 0x005A1A4F
		private void OnEndTakingInput()
		{
			this._searchBoxPanel.BorderColor = new Color(35, 40, 83);
			if (this._theme == UIWrappedSearchBar.ColorTheme.Red)
			{
				this._searchBoxPanel.BorderColor = Utils.ShiftBlueToCyanTheme(this._searchBoxPanel.BorderColor);
			}
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x005A388B File Offset: 0x005A1A8B
		private void UpdateSearchContents(string contents)
		{
			this._searchString = contents;
			if (this.OnSearchContentsChanged != null)
			{
				this.OnSearchContentsChanged(contents);
			}
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x005A38A8 File Offset: 0x005A1AA8
		private void Click_SearchArea(UIMouseEvent evt, UIElement listeningElement)
		{
			if (evt.Target.Parent == this._searchBoxPanel)
			{
				return;
			}
			this.ToggleTakingText();
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x005A38C4 File Offset: 0x005A1AC4
		private void SearchBox_OnRightClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.SetContents(null, true);
			if (!this.IsWritingText)
			{
				this.ToggleTakingText();
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x005A38DC File Offset: 0x005A1ADC
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (this.IsWritingText && FocusHelper.AllowUIInputs && (Main.mouseLeft || Main.mouseRight))
			{
				if (!this.Elements.Any((UIElement e) => e.IsMouseHovering))
				{
					this.ToggleTakingText();
				}
			}
		}

		// Token: 0x040054B7 RID: 21687
		private Action _goBackFromVirtualKeyboard;

		// Token: 0x040054B8 RID: 21688
		private LocalizedText _emptyText;

		// Token: 0x040054B9 RID: 21689
		private UISearchBar _searchBar;

		// Token: 0x040054BA RID: 21690
		private UIPanel _searchBoxPanel;

		// Token: 0x040054BB RID: 21691
		private UIElement _searchButton;

		// Token: 0x040054BC RID: 21692
		private string _searchString;

		// Token: 0x040054BD RID: 21693
		[CompilerGenerated]
		private Action<string> OnSearchContentsChanged;

		// Token: 0x040054BE RID: 21694
		public Action<UIState> CustomOpenVirtualKeyboard;

		// Token: 0x040054BF RID: 21695
		private UIWrappedSearchBar.ColorTheme _theme;

		// Token: 0x02000924 RID: 2340
		public enum ColorTheme
		{
			// Token: 0x040074F0 RID: 29936
			Blue,
			// Token: 0x040074F1 RID: 29937
			Red
		}

		// Token: 0x02000925 RID: 2341
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060047F1 RID: 18417 RVA: 0x006CC7A4 File Offset: 0x006CA9A4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060047F2 RID: 18418 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060047F3 RID: 18419 RVA: 0x006CC7B0 File Offset: 0x006CA9B0
			internal bool <Update>b__35_0(UIElement e)
			{
				return e.IsMouseHovering;
			}

			// Token: 0x040074F2 RID: 29938
			public static readonly UIWrappedSearchBar.<>c <>9 = new UIWrappedSearchBar.<>c();

			// Token: 0x040074F3 RID: 29939
			public static Func<UIElement, bool> <>9__35_0;
		}
	}
}
