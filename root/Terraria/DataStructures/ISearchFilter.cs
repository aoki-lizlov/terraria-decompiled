using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000543 RID: 1347
	public interface ISearchFilter<T> : IEntryFilter<T>
	{
		// Token: 0x0600376E RID: 14190
		void SetSearch(string searchText);
	}
}
