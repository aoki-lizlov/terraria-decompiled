using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D6 RID: 982
	public class UICreativeItemsInfiniteFilteringOptions : UIElement
	{
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06002DD6 RID: 11734 RVA: 0x005A6A20 File Offset: 0x005A4C20
		// (remove) Token: 0x06002DD7 RID: 11735 RVA: 0x005A6A58 File Offset: 0x005A4C58
		public event Action OnClickingOption
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnClickingOption;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnClickingOption, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnClickingOption;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnClickingOption, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x005A6A90 File Offset: 0x005A4C90
		public UICreativeItemsInfiniteFilteringOptions(EntryFilterer<Item, IItemEntryFilter> filterer, string snapPointsName, UICreativeItemsInfiniteFilteringOptions.ColorTheme theme = UICreativeItemsInfiniteFilteringOptions.ColorTheme.Blue)
		{
			this._theme = theme;
			this._filterer = filterer;
			int num = 40;
			int count = this._filterer.AvailableFilters.Count;
			int num2 = num * count;
			this.Height = new StyleDimension((float)num, 0f);
			this.Width = new StyleDimension((float)num2, 0f);
			this.Top = new StyleDimension(4f, 0f);
			base.SetPadding(0f);
			string text = "Images/UI/Creative/Infinite_Tabs_B";
			if (this._theme == UICreativeItemsInfiniteFilteringOptions.ColorTheme.Cyan)
			{
				text = "Images/UI/Creative/Infinite_Tabs_B_2";
			}
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(text, 1);
			for (int i = 0; i < this._filterer.AvailableFilters.Count; i++)
			{
				IItemEntryFilter itemEntryFilter = this._filterer.AvailableFilters[i];
				asset.Frame(2, 4, 0, 0, 0, 0).OffsetSize(-2, -2);
				UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(2, 4, 0, 0, 0, 0).OffsetSize(-2, -2));
				uiimageFramed.Left.Set((float)(num * i), 0f);
				uiimageFramed.OnLeftClick += this.singleFilterButtonClick;
				uiimageFramed.OnMouseOver += this.button_OnMouseOver;
				uiimageFramed.SetPadding(0f);
				uiimageFramed.SetSnapPoint(snapPointsName, i, null, null);
				this.AddOnHover(itemEntryFilter, uiimageFramed, i);
				UIElement image = itemEntryFilter.GetImage();
				image.IgnoresMouseInteraction = true;
				image.Left = new StyleDimension(6f, 0f);
				image.HAlign = 0f;
				uiimageFramed.Append(image);
				this._filtersByButtons[uiimageFramed] = itemEntryFilter;
				this._iconsByButtons[uiimageFramed] = image;
				base.Append(uiimageFramed);
				this.UpdateVisuals(uiimageFramed, i);
			}
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x00593C8E File Offset: 0x00591E8E
		private void button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x005A6C94 File Offset: 0x005A4E94
		private void singleFilterButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			UIImageFramed uiimageFramed = evt.Target as UIImageFramed;
			if (uiimageFramed == null)
			{
				return;
			}
			IItemEntryFilter itemEntryFilter;
			if (!this._filtersByButtons.TryGetValue(uiimageFramed, out itemEntryFilter))
			{
				return;
			}
			int num = this._filterer.AvailableFilters.IndexOf(itemEntryFilter);
			if (num == -1)
			{
				return;
			}
			if (!this._filterer.ActiveFilters.Contains(itemEntryFilter))
			{
				this._filterer.ActiveFilters.Clear();
			}
			this._filterer.ToggleFilter(num);
			this.UpdateVisuals(uiimageFramed, num);
			if (this.OnClickingOption != null)
			{
				this.OnClickingOption();
			}
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x005A6D24 File Offset: 0x005A4F24
		private void UpdateVisuals(UIImageFramed button, int indexOfFilter)
		{
			bool flag = this._filterer.IsFilterActive(indexOfFilter);
			bool isMouseHovering = button.IsMouseHovering;
			int num = flag.ToInt();
			int num2 = flag.ToInt() * 2 + isMouseHovering.ToInt();
			button.SetFrame(2, 4, num, num2, -2, -2);
			IColorable colorable = this._iconsByButtons[button] as IColorable;
			if (colorable != null)
			{
				colorable.Color = (flag ? Color.White : (Color.White * 0.5f));
			}
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x005A6DA4 File Offset: 0x005A4FA4
		private void AddOnHover(IItemEntryFilter filter, UIElement button, int indexOfFilter)
		{
			button.OnUpdate += delegate(UIElement element)
			{
				this.ShowButtonName(element, filter, indexOfFilter);
			};
			button.OnUpdate += delegate(UIElement element)
			{
				this.UpdateVisuals(button as UIImageFramed, indexOfFilter);
			};
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x005A6E04 File Offset: 0x005A5004
		private void ShowButtonName(UIElement element, IItemEntryFilter number, int indexOfFilter)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			string textValue = Language.GetTextValue(number.GetDisplayNameKey());
			Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x04005507 RID: 21767
		private EntryFilterer<Item, IItemEntryFilter> _filterer;

		// Token: 0x04005508 RID: 21768
		private Dictionary<UIImageFramed, IItemEntryFilter> _filtersByButtons = new Dictionary<UIImageFramed, IItemEntryFilter>();

		// Token: 0x04005509 RID: 21769
		private Dictionary<UIImageFramed, UIElement> _iconsByButtons = new Dictionary<UIImageFramed, UIElement>();

		// Token: 0x0400550A RID: 21770
		[CompilerGenerated]
		private Action OnClickingOption;

		// Token: 0x0400550B RID: 21771
		private const int barFramesX = 2;

		// Token: 0x0400550C RID: 21772
		private const int barFramesY = 4;

		// Token: 0x0400550D RID: 21773
		private UICreativeItemsInfiniteFilteringOptions.ColorTheme _theme;

		// Token: 0x0200092C RID: 2348
		public enum ColorTheme
		{
			// Token: 0x04007502 RID: 29954
			Blue,
			// Token: 0x04007503 RID: 29955
			Cyan
		}

		// Token: 0x0200092D RID: 2349
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06004801 RID: 18433 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06004802 RID: 18434 RVA: 0x006CC848 File Offset: 0x006CAA48
			internal void <AddOnHover>b__0(UIElement element)
			{
				this.<>4__this.ShowButtonName(element, this.filter, this.indexOfFilter);
			}

			// Token: 0x06004803 RID: 18435 RVA: 0x006CC862 File Offset: 0x006CAA62
			internal void <AddOnHover>b__1(UIElement element)
			{
				this.<>4__this.UpdateVisuals(this.button as UIImageFramed, this.indexOfFilter);
			}

			// Token: 0x04007504 RID: 29956
			public UICreativeItemsInfiniteFilteringOptions <>4__this;

			// Token: 0x04007505 RID: 29957
			public IItemEntryFilter filter;

			// Token: 0x04007506 RID: 29958
			public int indexOfFilter;

			// Token: 0x04007507 RID: 29959
			public UIElement button;
		}
	}
}
