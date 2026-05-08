using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003DA RID: 986
	public class UIBestiaryEntryInfoPage : UIPanel
	{
		// Token: 0x06002DEF RID: 11759 RVA: 0x005A76B8 File Offset: 0x005A58B8
		public UIBestiaryEntryInfoPage()
		{
			this.Width.Set(230f, 0f);
			this.Height.Set(0f, 1f);
			base.SetPadding(0f);
			this.BorderColor = new Color(89, 116, 213, 255);
			this.BackgroundColor = new Color(73, 94, 171);
			UIList uilist = new UIList
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f)
			};
			uilist.SetPadding(2f);
			uilist.PaddingBottom = 4f;
			uilist.PaddingTop = 4f;
			base.Append(uilist);
			this._list = uilist;
			uilist.ListPadding = 4f;
			uilist.ManualSortMethod = new Action<List<UIElement>>(this.ManualIfnoSortingMethod);
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(-20f, 1f);
			uiscrollbar.HAlign = 1f;
			uiscrollbar.VAlign = 0.5f;
			uiscrollbar.Left.Set(-6f, 0f);
			this._scrollbar = uiscrollbar;
			this._list.SetScrollbar(this._scrollbar);
			this.CheckScrollBar();
			this.AppendBorderOverEverything();
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x005A7824 File Offset: 0x005A5A24
		public void UpdateScrollbar(int scrollWheelValue)
		{
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)scrollWheelValue;
			}
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x005A7844 File Offset: 0x005A5A44
		private void AppendBorderOverEverything()
		{
			UIPanel uipanel = new UIPanel
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				IgnoresMouseInteraction = true
			};
			uipanel.BorderColor = new Color(89, 116, 213, 255);
			uipanel.BackgroundColor = Color.Transparent;
			base.Append(uipanel);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x00009E46 File Offset: 0x00008046
		private void ManualIfnoSortingMethod(List<UIElement> list)
		{
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x005A78B3 File Offset: 0x005A5AB3
		public override void Recalculate()
		{
			base.Recalculate();
			this.CheckScrollBar();
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x005A78C4 File Offset: 0x005A5AC4
		private void CheckScrollBar()
		{
			if (this._scrollbar != null)
			{
				bool flag = this._scrollbar.CanScroll;
				flag = true;
				if (this._isScrollbarAttached && !flag)
				{
					base.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._list.Width.Set(0f, 1f);
					return;
				}
				if (!this._isScrollbarAttached && flag)
				{
					base.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._list.Width.Set(-20f, 1f);
				}
			}
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x005A795D File Offset: 0x005A5B5D
		public void FillInfoForEntry(BestiaryEntry entry, ExtraBestiaryInfoPageInformation extraInfo)
		{
			this._list.Clear();
			if (entry == null)
			{
				return;
			}
			this.AddInfoToList(entry, extraInfo);
			this.Recalculate();
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x005A797C File Offset: 0x005A5B7C
		private BestiaryUICollectionInfo GetUICollectionInfo(BestiaryEntry entry, ExtraBestiaryInfoPageInformation extraInfo)
		{
			IBestiaryUICollectionInfoProvider uiinfoProvider = entry.UIInfoProvider;
			BestiaryUICollectionInfo bestiaryUICollectionInfo;
			if (uiinfoProvider != null)
			{
				bestiaryUICollectionInfo = uiinfoProvider.GetEntryUICollectionInfo();
			}
			else
			{
				bestiaryUICollectionInfo = default(BestiaryUICollectionInfo);
			}
			bestiaryUICollectionInfo.OwnerEntry = entry;
			return bestiaryUICollectionInfo;
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x005A79B0 File Offset: 0x005A5BB0
		private void AddInfoToList(BestiaryEntry entry, ExtraBestiaryInfoPageInformation extraInfo)
		{
			BestiaryUICollectionInfo uicollectionInfo = this.GetUICollectionInfo(entry, extraInfo);
			IEnumerable<IGrouping<UIBestiaryEntryInfoPage.BestiaryInfoCategory, IBestiaryInfoElement>> enumerable = from x in new List<IBestiaryInfoElement>(entry.Info).GroupBy(new Func<IBestiaryInfoElement, UIBestiaryEntryInfoPage.BestiaryInfoCategory>(this.GetBestiaryInfoCategory))
				orderby x.Key
				select x;
			UIElement uielement = null;
			foreach (IGrouping<UIBestiaryEntryInfoPage.BestiaryInfoCategory, IBestiaryInfoElement> grouping in enumerable)
			{
				if (grouping.Count<IBestiaryInfoElement>() != 0)
				{
					bool flag = false;
					foreach (IBestiaryInfoElement bestiaryInfoElement in grouping.OrderByDescending(new Func<IBestiaryInfoElement, float>(this.GetIndividualElementPriority)))
					{
						UIElement uielement2 = bestiaryInfoElement.ProvideUIElement(uicollectionInfo);
						if (uielement2 != null)
						{
							this._list.Add(uielement2);
							flag = true;
						}
					}
					if (flag)
					{
						UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
						{
							Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
							Color = new Color(89, 116, 213, 255) * 0.9f
						};
						this._list.Add(uihorizontalSeparator);
						uielement = uihorizontalSeparator;
					}
				}
			}
			this._list.Remove(uielement);
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x005A7B14 File Offset: 0x005A5D14
		private float GetIndividualElementPriority(IBestiaryInfoElement element)
		{
			IBestiaryPrioritizedElement bestiaryPrioritizedElement = element as IBestiaryPrioritizedElement;
			if (bestiaryPrioritizedElement != null)
			{
				return bestiaryPrioritizedElement.OrderPriority;
			}
			return 0f;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x005A7B38 File Offset: 0x005A5D38
		private UIBestiaryEntryInfoPage.BestiaryInfoCategory GetBestiaryInfoCategory(IBestiaryInfoElement element)
		{
			if (element is NPCPortraitInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.Portrait;
			}
			if (element is FlavorTextBestiaryInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.FlavorText;
			}
			if (element is NamePlateInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.Nameplate;
			}
			if (element is ItemFromCatchingNPCBestiaryInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.ItemsFromCatchingNPC;
			}
			if (element is ItemDropBestiaryInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.ItemsFromDrops;
			}
			if (element is NPCStatsReportInfoElement || element is NPCKillCounterInfoElement)
			{
				return UIBestiaryEntryInfoPage.BestiaryInfoCategory.Stats;
			}
			return UIBestiaryEntryInfoPage.BestiaryInfoCategory.Misc;
		}

		// Token: 0x04005517 RID: 21783
		private UIList _list;

		// Token: 0x04005518 RID: 21784
		private UIScrollbar _scrollbar;

		// Token: 0x04005519 RID: 21785
		private bool _isScrollbarAttached;

		// Token: 0x0200092E RID: 2350
		private enum BestiaryInfoCategory
		{
			// Token: 0x04007509 RID: 29961
			Nameplate,
			// Token: 0x0400750A RID: 29962
			Portrait,
			// Token: 0x0400750B RID: 29963
			FlavorText,
			// Token: 0x0400750C RID: 29964
			Stats,
			// Token: 0x0400750D RID: 29965
			ItemsFromCatchingNPC,
			// Token: 0x0400750E RID: 29966
			ItemsFromDrops,
			// Token: 0x0400750F RID: 29967
			Misc
		}

		// Token: 0x0200092F RID: 2351
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004804 RID: 18436 RVA: 0x006CC880 File Offset: 0x006CAA80
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004805 RID: 18437 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004806 RID: 18438 RVA: 0x006CC88C File Offset: 0x006CAA8C
			internal UIBestiaryEntryInfoPage.BestiaryInfoCategory <AddInfoToList>b__11_0(IGrouping<UIBestiaryEntryInfoPage.BestiaryInfoCategory, IBestiaryInfoElement> x)
			{
				return x.Key;
			}

			// Token: 0x04007510 RID: 29968
			public static readonly UIBestiaryEntryInfoPage.<>c <>9 = new UIBestiaryEntryInfoPage.<>c();

			// Token: 0x04007511 RID: 29969
			public static Func<IGrouping<UIBestiaryEntryInfoPage.BestiaryInfoCategory, IBestiaryInfoElement>, UIBestiaryEntryInfoPage.BestiaryInfoCategory> <>9__11_0;
		}
	}
}
