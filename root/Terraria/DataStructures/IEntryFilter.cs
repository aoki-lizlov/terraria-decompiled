using System;
using Terraria.UI;

namespace Terraria.DataStructures
{
	// Token: 0x02000541 RID: 1345
	public interface IEntryFilter<T>
	{
		// Token: 0x0600376A RID: 14186
		bool FitsFilter(T entry);

		// Token: 0x0600376B RID: 14187
		string GetDisplayNameKey();

		// Token: 0x0600376C RID: 14188
		UIElement GetImage();
	}
}
