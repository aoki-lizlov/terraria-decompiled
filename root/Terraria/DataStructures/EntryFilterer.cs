using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x02000544 RID: 1348
	public class EntryFilterer<T, U> where T : new() where U : IEntryFilter<T>
	{
		// Token: 0x0600376F RID: 14191 RVA: 0x0062EFE9 File Offset: 0x0062D1E9
		public EntryFilterer()
		{
			this.AvailableFilters = new List<U>();
			this.ActiveFilters = new List<U>();
			this.AlwaysActiveFilters = new List<U>();
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x0062F012 File Offset: 0x0062D212
		public void AddFilters(List<U> filters)
		{
			this.AvailableFilters.AddRange(filters);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x0062F020 File Offset: 0x0062D220
		public bool FitsFilter(T entry)
		{
			if (this._searchFilter != null && !this._searchFilter.FitsFilter(entry))
			{
				return false;
			}
			for (int i = 0; i < this.AlwaysActiveFilters.Count; i++)
			{
				U u = this.AlwaysActiveFilters[i];
				if (!u.FitsFilter(entry))
				{
					return false;
				}
			}
			if (this.ActiveFilters.Count == 0)
			{
				return true;
			}
			for (int j = 0; j < this.ActiveFilters.Count; j++)
			{
				U u = this.ActiveFilters[j];
				if (u.FitsFilter(entry))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x0062F0C0 File Offset: 0x0062D2C0
		public void ToggleFilter(int filterIndex)
		{
			U u = this.AvailableFilters[filterIndex];
			if (this.ActiveFilters.Contains(u))
			{
				this.ActiveFilters.Remove(u);
				return;
			}
			this.ActiveFilters.Add(u);
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x0062F104 File Offset: 0x0062D304
		public bool IsFilterActive(int filterIndex)
		{
			if (!this.AvailableFilters.IndexInRange(filterIndex))
			{
				return false;
			}
			U u = this.AvailableFilters[filterIndex];
			return this.ActiveFilters.Contains(u);
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x0062F13A File Offset: 0x0062D33A
		public void SetSearchFilterObject<Z>(Z searchFilter) where Z : ISearchFilter<T>, U
		{
			this._searchFilterFromConstructor = searchFilter;
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x0062F148 File Offset: 0x0062D348
		public void SetSearchFilter(string searchFilter)
		{
			if (string.IsNullOrWhiteSpace(searchFilter))
			{
				this._searchFilter = null;
				return;
			}
			this._searchFilter = this._searchFilterFromConstructor;
			this._searchFilter.SetSearch(searchFilter);
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x0062F174 File Offset: 0x0062D374
		public string GetDisplayName()
		{
			object obj = new { this.ActiveFilters.Count };
			return Language.GetTextValueWith("BestiaryInfo.Filters", obj);
		}

		// Token: 0x04005B9F RID: 23455
		public List<U> AvailableFilters;

		// Token: 0x04005BA0 RID: 23456
		public List<U> ActiveFilters;

		// Token: 0x04005BA1 RID: 23457
		public List<U> AlwaysActiveFilters;

		// Token: 0x04005BA2 RID: 23458
		private ISearchFilter<T> _searchFilter;

		// Token: 0x04005BA3 RID: 23459
		private ISearchFilter<T> _searchFilterFromConstructor;
	}
}
