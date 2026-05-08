using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D4 RID: 980
	public class UIBestiarySortingOptionsGrid : UIPanel
	{
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06002DC3 RID: 11715 RVA: 0x005A5D0C File Offset: 0x005A3F0C
		// (remove) Token: 0x06002DC4 RID: 11716 RVA: 0x005A5D44 File Offset: 0x005A3F44
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

		// Token: 0x06002DC5 RID: 11717 RVA: 0x005A5D7C File Offset: 0x005A3F7C
		public UIBestiarySortingOptionsGrid(EntrySorter<BestiaryEntry, IBestiarySortStep> sorter)
		{
			this._sorter = sorter;
			this._buttonsBySorting = new List<GroupOptionButton<int>>();
			this.Width = new StyleDimension(0f, 1f);
			this.Height = new StyleDimension(0f, 1f);
			this.BackgroundColor = new Color(35, 40, 83) * 0.5f;
			this.BorderColor = new Color(35, 40, 83) * 0.5f;
			this.IgnoresMouseInteraction = false;
			base.SetPadding(0f);
			this.BuildGrid();
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x005A5E20 File Offset: 0x005A4020
		private void BuildGrid()
		{
			int num = 2;
			int num2 = 26 + num;
			int num3 = 0;
			for (int i = 0; i < this._sorter.Steps.Count; i++)
			{
				if (!this._sorter.Steps[i].HiddenFromSortOptions)
				{
					num3++;
				}
			}
			UIPanel uipanel = new UIPanel
			{
				Width = new StyleDimension(126f, 0f),
				Height = new StyleDimension((float)(num3 * num2 + 5 + 3), 0f),
				HAlign = 1f,
				VAlign = 0f,
				Left = new StyleDimension(-118f, 0f),
				Top = new StyleDimension(0f, 0f)
			};
			uipanel.BorderColor = new Color(89, 116, 213, 255) * 0.9f;
			uipanel.BackgroundColor = new Color(73, 94, 171) * 0.9f;
			uipanel.SetPadding(0f);
			base.Append(uipanel);
			int num4 = 0;
			for (int j = 0; j < this._sorter.Steps.Count; j++)
			{
				IBestiarySortStep bestiarySortStep = this._sorter.Steps[j];
				if (!bestiarySortStep.HiddenFromSortOptions)
				{
					GroupOptionButton<int> groupOptionButton = new GroupOptionButton<int>(j, Language.GetText(bestiarySortStep.GetDisplayNameKey()), null, Color.White, null, 0.8f, 0.5f, 10f)
					{
						Width = new StyleDimension(114f, 0f),
						Height = new StyleDimension((float)(num2 - num), 0f),
						HAlign = 0.5f,
						Top = new StyleDimension((float)(5 + num2 * num4), 0f)
					};
					groupOptionButton.ShowHighlightWhenSelected = false;
					groupOptionButton.OnLeftClick += this.ClickOption;
					groupOptionButton.SetSnapPoint("SortSteps", num4, null, null);
					uipanel.Append(groupOptionButton);
					this._buttonsBySorting.Add(groupOptionButton);
					num4++;
				}
			}
			foreach (GroupOptionButton<int> groupOptionButton2 in this._buttonsBySorting)
			{
				groupOptionButton2.SetCurrentOption(-1);
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x005A6094 File Offset: 0x005A4294
		private void ClickOption(UIMouseEvent evt, UIElement listeningElement)
		{
			int num = ((GroupOptionButton<int>)listeningElement).OptionValue;
			if (num == this._currentSelected)
			{
				num = this._defaultStepIndex;
			}
			foreach (GroupOptionButton<int> groupOptionButton in this._buttonsBySorting)
			{
				bool flag = num == groupOptionButton.OptionValue;
				groupOptionButton.SetCurrentOption(flag ? num : (-1));
				if (flag)
				{
					groupOptionButton.SetColor(new Color(152, 175, 235), 1f);
				}
				else
				{
					groupOptionButton.SetColor(Colors.InventoryDefaultColor, 0.7f);
				}
			}
			this._currentSelected = num;
			this._sorter.SetPrioritizedStepIndex(num);
			if (this.OnClickingOption != null)
			{
				this.OnClickingOption();
			}
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x005A6170 File Offset: 0x005A4370
		public void GetEntriesToShow(out int maxEntriesWidth, out int maxEntriesHeight, out int maxEntriesToHave)
		{
			maxEntriesWidth = 1;
			maxEntriesHeight = this._buttonsBySorting.Count;
			maxEntriesToHave = this._buttonsBySorting.Count;
		}

		// Token: 0x040054FC RID: 21756
		private EntrySorter<BestiaryEntry, IBestiarySortStep> _sorter;

		// Token: 0x040054FD RID: 21757
		private List<GroupOptionButton<int>> _buttonsBySorting;

		// Token: 0x040054FE RID: 21758
		private int _currentSelected = -1;

		// Token: 0x040054FF RID: 21759
		private int _defaultStepIndex;

		// Token: 0x04005500 RID: 21760
		[CompilerGenerated]
		private Action OnClickingOption;
	}
}
