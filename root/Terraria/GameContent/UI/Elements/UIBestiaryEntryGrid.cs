using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D3 RID: 979
	public class UIBestiaryEntryGrid : UIElement
	{
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06002DB7 RID: 11703 RVA: 0x005A594C File Offset: 0x005A3B4C
		// (remove) Token: 0x06002DB8 RID: 11704 RVA: 0x005A5984 File Offset: 0x005A3B84
		public event Action OnGridContentsChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnGridContentsChanged;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnGridContentsChanged, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnGridContentsChanged;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnGridContentsChanged, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x005A59BC File Offset: 0x005A3BBC
		public UIBestiaryEntryGrid(List<BestiaryEntry> workingSet, UIElement.MouseEvent clickOnEntryEvent)
		{
			this.Width = new StyleDimension(0f, 1f);
			this.Height = new StyleDimension(0f, 1f);
			this._workingSetEntries = workingSet;
			this._clickOnEntryEvent = clickOnEntryEvent;
			base.SetPadding(0f);
			this.UpdateEntries();
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x005A5A1E File Offset: 0x005A3C1E
		public void UpdateEntries()
		{
			this._lastEntry = this._workingSetEntries.Count;
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x005A5A34 File Offset: 0x005A3C34
		public void FillBestiarySpaceWithEntries()
		{
			base.RemoveAllChildren();
			this.UpdateEntries();
			int num;
			int num2;
			int num3;
			this.GetEntriesToShow(out num, out num2, out num3);
			this.FixBestiaryRange(0, num3);
			int atEntryIndex = this._atEntryIndex;
			int num4 = Math.Min(this._lastEntry, atEntryIndex + num3);
			List<BestiaryEntry> list = new List<BestiaryEntry>();
			for (int i = atEntryIndex; i < num4; i++)
			{
				list.Add(this._workingSetEntries[i]);
			}
			int num5 = 0;
			float num6 = 0.5f / (float)num;
			float num7 = 0.5f / (float)num2;
			for (int j = 0; j < num2; j++)
			{
				int num8 = 0;
				while (num8 < num && num5 < list.Count)
				{
					UIElement uielement = new UIBestiaryEntryButton(list[num5], false);
					num5++;
					uielement.OnLeftClick += this._clickOnEntryEvent;
					uielement.VAlign = (uielement.HAlign = 0.5f);
					uielement.Left.Set(0f, (float)num8 / (float)num - 0.5f + num6);
					uielement.Top.Set(0f, (float)j / (float)num2 - 0.5f + num7);
					uielement.SetSnapPoint("Entries", num5, new Vector2?(new Vector2(0.2f, 0.7f)), null);
					base.Append(uielement);
					num8++;
				}
			}
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x005A5BA5 File Offset: 0x005A3DA5
		public override void Recalculate()
		{
			base.Recalculate();
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x005A5BB4 File Offset: 0x005A3DB4
		public void GetEntriesToShow(out int maxEntriesWidth, out int maxEntriesHeight, out int maxEntriesToHave)
		{
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			maxEntriesWidth = rectangle.Width / 72;
			maxEntriesHeight = rectangle.Height / 72;
			int num = 0;
			maxEntriesToHave = maxEntriesWidth * maxEntriesHeight - num;
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x005A5BF4 File Offset: 0x005A3DF4
		public string GetRangeText()
		{
			int num;
			int num2;
			int num3;
			this.GetEntriesToShow(out num, out num2, out num3);
			int atEntryIndex = this._atEntryIndex;
			int num4 = Math.Min(this._lastEntry, atEntryIndex + num3);
			int num5 = Math.Min(atEntryIndex + 1, num4);
			return string.Format("{0}-{1} ({2})", num5, num4, this._lastEntry);
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x005A5C54 File Offset: 0x005A3E54
		public void MakeButtonGoByOffset(UIElement element, int howManyPages)
		{
			element.OnLeftClick += delegate(UIMouseEvent e, UIElement v)
			{
				this.OffsetLibraryByPages(howManyPages);
			};
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x005A5C88 File Offset: 0x005A3E88
		public void OffsetLibraryByPages(int howManyPages)
		{
			int num;
			int num2;
			int num3;
			this.GetEntriesToShow(out num, out num2, out num3);
			this.OffsetLibrary(howManyPages * num3);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x005A5CAC File Offset: 0x005A3EAC
		public void OffsetLibrary(int offset)
		{
			int num;
			int num2;
			int num3;
			this.GetEntriesToShow(out num, out num2, out num3);
			this.FixBestiaryRange(offset, num3);
			this.FillBestiarySpaceWithEntries();
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x005A5CD3 File Offset: 0x005A3ED3
		private void FixBestiaryRange(int offset, int maxEntriesToHave)
		{
			this._atEntryIndex = Utils.Clamp<int>(this._atEntryIndex + offset, 0, Math.Max(0, this._lastEntry - maxEntriesToHave));
			if (this.OnGridContentsChanged != null)
			{
				this.OnGridContentsChanged();
			}
		}

		// Token: 0x040054F7 RID: 21751
		private List<BestiaryEntry> _workingSetEntries;

		// Token: 0x040054F8 RID: 21752
		private UIElement.MouseEvent _clickOnEntryEvent;

		// Token: 0x040054F9 RID: 21753
		private int _atEntryIndex;

		// Token: 0x040054FA RID: 21754
		private int _lastEntry;

		// Token: 0x040054FB RID: 21755
		[CompilerGenerated]
		private Action OnGridContentsChanged;

		// Token: 0x0200092A RID: 2346
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x060047FD RID: 18429 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x060047FE RID: 18430 RVA: 0x006CC821 File Offset: 0x006CAA21
			internal void <MakeButtonGoByOffset>b__0(UIMouseEvent e, UIElement v)
			{
				this.<>4__this.OffsetLibraryByPages(this.howManyPages);
			}

			// Token: 0x040074FD RID: 29949
			public UIBestiaryEntryGrid <>4__this;

			// Token: 0x040074FE RID: 29950
			public int howManyPages;
		}
	}
}
