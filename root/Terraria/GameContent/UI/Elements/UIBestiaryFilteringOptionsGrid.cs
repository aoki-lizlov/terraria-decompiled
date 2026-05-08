using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D5 RID: 981
	public class UIBestiaryFilteringOptionsGrid : UIPanel
	{
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06002DC9 RID: 11721 RVA: 0x005A6190 File Offset: 0x005A4390
		// (remove) Token: 0x06002DCA RID: 11722 RVA: 0x005A61C8 File Offset: 0x005A43C8
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

		// Token: 0x06002DCB RID: 11723 RVA: 0x005A6200 File Offset: 0x005A4400
		public UIBestiaryFilteringOptionsGrid(EntryFilterer<BestiaryEntry, IBestiaryEntryFilter> filterer)
		{
			this._filterer = filterer;
			this._filterButtons = new List<GroupOptionButton<int>>();
			this._areFiltersAvailable = new List<bool>();
			this._filterAvailabilityTests = new List<List<BestiaryEntry>>();
			this.Width = new StyleDimension(0f, 1f);
			this.Height = new StyleDimension(0f, 1f);
			this.BackgroundColor = new Color(35, 40, 83) * 0.5f;
			this.BorderColor = new Color(35, 40, 83) * 0.5f;
			this.IgnoresMouseInteraction = false;
			base.SetPadding(0f);
			this.BuildContainer();
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x005A62B4 File Offset: 0x005A44B4
		private void BuildContainer()
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			float num6;
			float num7;
			int num8;
			this.GetDisplaySettings(out num, out num2, out num3, out num4, out num5, out num6, out num7, out num8);
			UIPanel uipanel = new UIPanel
			{
				Width = new StyleDimension((float)(num5 * num3 + 10), 0f),
				Height = new StyleDimension((float)(num8 * num4 + 10), 0f),
				HAlign = 1f,
				VAlign = 0f,
				Left = new StyleDimension(0f, 0f),
				Top = new StyleDimension(0f, 0f)
			};
			uipanel.BorderColor = new Color(89, 116, 213, 255) * 0.9f;
			uipanel.BackgroundColor = new Color(73, 94, 171) * 0.9f;
			uipanel.SetPadding(0f);
			base.Append(uipanel);
			this._container = uipanel;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x005A63B0 File Offset: 0x005A45B0
		public void SetupAvailabilityTest(List<BestiaryEntry> allAvailableEntries)
		{
			this._filterAvailabilityTests.Clear();
			for (int i = 0; i < this._filterer.AvailableFilters.Count; i++)
			{
				List<BestiaryEntry> list = new List<BestiaryEntry>();
				this._filterAvailabilityTests.Add(list);
				IBestiaryEntryFilter bestiaryEntryFilter = this._filterer.AvailableFilters[i];
				for (int j = 0; j < allAvailableEntries.Count; j++)
				{
					if (bestiaryEntryFilter.FitsFilter(allAvailableEntries[j]))
					{
						list.Add(allAvailableEntries[j]);
					}
				}
			}
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x005A6434 File Offset: 0x005A4634
		public void UpdateAvailability()
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			float num6;
			float num7;
			int num8;
			this.GetDisplaySettings(out num, out num2, out num3, out num4, out num5, out num6, out num7, out num8);
			this._container.RemoveAllChildren();
			this._filterButtons.Clear();
			this._areFiltersAvailable.Clear();
			int num9 = -1;
			int num10 = -1;
			for (int i = 0; i < this._filterer.AvailableFilters.Count; i++)
			{
				int num11 = i / num5;
				int num12 = i % num5;
				IBestiaryEntryFilter bestiaryEntryFilter = this._filterer.AvailableFilters[i];
				List<BestiaryEntry> list = this._filterAvailabilityTests[i];
				if (this.GetIsFilterAvailableForEntries(bestiaryEntryFilter, list))
				{
					GroupOptionButton<int> groupOptionButton = new GroupOptionButton<int>(i, null, null, Color.White, null, 1f, 0.5f, 10f)
					{
						Width = new StyleDimension((float)num, 0f),
						Height = new StyleDimension((float)num2, 0f),
						HAlign = 0f,
						VAlign = 0f,
						Top = new StyleDimension(num7 + (float)(num11 * num4), 0f),
						Left = new StyleDimension(num6 + (float)(num12 * num3), 0f)
					};
					groupOptionButton.OnLeftClick += this.ClickOption;
					groupOptionButton.SetSnapPoint("Filters", i, null, null);
					groupOptionButton.ShowHighlightWhenSelected = false;
					this.AddOnHover(bestiaryEntryFilter, groupOptionButton);
					this._container.Append(groupOptionButton);
					UIElement image = bestiaryEntryFilter.GetImage();
					if (image != null)
					{
						image.Left = new StyleDimension((float)num9, 0f);
						image.Top = new StyleDimension((float)num10, 0f);
						groupOptionButton.Append(image);
					}
					this._filterButtons.Add(groupOptionButton);
				}
				else
				{
					this._filterer.ActiveFilters.Remove(bestiaryEntryFilter);
					GroupOptionButton<int> groupOptionButton2 = new GroupOptionButton<int>(-2, null, null, Color.White, null, 1f, 0.5f, 10f)
					{
						Width = new StyleDimension((float)num, 0f),
						Height = new StyleDimension((float)num2, 0f),
						HAlign = 0f,
						VAlign = 0f,
						Top = new StyleDimension(num7 + (float)(num11 * num4), 0f),
						Left = new StyleDimension(num6 + (float)(num12 * num3), 0f),
						FadeFromBlack = 0.5f
					};
					groupOptionButton2.ShowHighlightWhenSelected = false;
					groupOptionButton2.SetPadding(0f);
					groupOptionButton2.SetSnapPoint("Filters", i, null, null);
					Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow", 1);
					UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(16, 5, 0, 4, 0, 0))
					{
						HAlign = 0.5f,
						VAlign = 0.5f,
						Color = Color.White * 0.2f
					};
					uiimageFramed.Left = new StyleDimension((float)num9, 0f);
					uiimageFramed.Top = new StyleDimension((float)num10, 0f);
					groupOptionButton2.Append(uiimageFramed);
					this._filterButtons.Add(groupOptionButton2);
					this._container.Append(groupOptionButton2);
				}
			}
			this.UpdateButtonSelections();
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x005A6790 File Offset: 0x005A4990
		public void GetEntriesToShow(out int maxEntriesWidth, out int maxEntriesHeight, out int maxEntriesToHave)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			float num6;
			float num7;
			int num8;
			this.GetDisplaySettings(out num, out num2, out num3, out num4, out num5, out num6, out num7, out num8);
			maxEntriesWidth = num5;
			maxEntriesHeight = num8;
			maxEntriesToHave = this._filterer.AvailableFilters.Count;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x005A67D0 File Offset: 0x005A49D0
		private void GetDisplaySettings(out int widthPerButton, out int heightPerButton, out int widthWithSpacing, out int heightWithSpacing, out int perRow, out float offsetLeft, out float offsetTop, out int howManyRows)
		{
			widthPerButton = 32;
			heightPerButton = 32;
			int num = 2;
			widthWithSpacing = widthPerButton + num;
			heightWithSpacing = heightPerButton + num;
			perRow = (int)Math.Ceiling(Math.Sqrt((double)this._filterer.AvailableFilters.Count));
			perRow = 12;
			howManyRows = (int)Math.Ceiling((double)((float)this._filterer.AvailableFilters.Count / (float)perRow));
			offsetLeft = (float)(perRow * widthWithSpacing - num) * 0.5f;
			offsetTop = (float)(howManyRows * heightWithSpacing - num) * 0.5f;
			offsetLeft = 6f;
			offsetTop = 6f;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x005A6870 File Offset: 0x005A4A70
		private void UpdateButtonSelections()
		{
			foreach (GroupOptionButton<int> groupOptionButton in this._filterButtons)
			{
				bool flag = this._filterer.IsFilterActive(groupOptionButton.OptionValue);
				groupOptionButton.SetCurrentOption(flag ? groupOptionButton.OptionValue : (-1));
				if (flag)
				{
					groupOptionButton.SetColor(new Color(152, 175, 235), 1f);
				}
				else
				{
					groupOptionButton.SetColor(Colors.InventoryDefaultColor, 0.7f);
				}
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x005A6914 File Offset: 0x005A4B14
		private bool GetIsFilterAvailableForEntries(IBestiaryEntryFilter filter, List<BestiaryEntry> entries)
		{
			bool? forcedDisplay = filter.ForcedDisplay;
			if (forcedDisplay != null)
			{
				return forcedDisplay.Value;
			}
			for (int i = 0; i < entries.Count; i++)
			{
				if (filter.FitsFilter(entries[i]) && entries[i].UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x005A6978 File Offset: 0x005A4B78
		private void AddOnHover(IBestiaryEntryFilter filter, UIElement button)
		{
			button.OnUpdate += delegate(UIElement element)
			{
				this.ShowButtonName(element, filter);
			};
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x005A69AC File Offset: 0x005A4BAC
		private void ShowButtonName(UIElement element, IBestiaryEntryFilter number)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			string textValue = Language.GetTextValue(number.GetDisplayNameKey());
			Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x005A69E0 File Offset: 0x005A4BE0
		private void ClickOption(UIMouseEvent evt, UIElement listeningElement)
		{
			int optionValue = ((GroupOptionButton<int>)listeningElement).OptionValue;
			this._filterer.ToggleFilter(optionValue);
			this.UpdateButtonSelections();
			if (this.OnClickingOption != null)
			{
				this.OnClickingOption();
			}
		}

		// Token: 0x04005501 RID: 21761
		private EntryFilterer<BestiaryEntry, IBestiaryEntryFilter> _filterer;

		// Token: 0x04005502 RID: 21762
		private List<GroupOptionButton<int>> _filterButtons;

		// Token: 0x04005503 RID: 21763
		private List<bool> _areFiltersAvailable;

		// Token: 0x04005504 RID: 21764
		private List<List<BestiaryEntry>> _filterAvailabilityTests;

		// Token: 0x04005505 RID: 21765
		[CompilerGenerated]
		private Action OnClickingOption;

		// Token: 0x04005506 RID: 21766
		private UIElement _container;

		// Token: 0x0200092B RID: 2347
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x060047FF RID: 18431 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x06004800 RID: 18432 RVA: 0x006CC834 File Offset: 0x006CAA34
			internal void <AddOnHover>b__0(UIElement element)
			{
				this.<>4__this.ShowButtonName(element, this.filter);
			}

			// Token: 0x040074FF RID: 29951
			public UIBestiaryFilteringOptionsGrid <>4__this;

			// Token: 0x04007500 RID: 29952
			public IBestiaryEntryFilter filter;
		}
	}
}
