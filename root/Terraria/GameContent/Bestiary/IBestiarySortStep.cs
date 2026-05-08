using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000331 RID: 817
	public interface IBestiarySortStep : IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600280D RID: 10253
		bool HiddenFromSortOptions { get; }
	}
}
