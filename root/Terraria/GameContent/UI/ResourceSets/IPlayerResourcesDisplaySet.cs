using System;
using Terraria.DataStructures;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003C0 RID: 960
	public interface IPlayerResourcesDisplaySet : IConfigKeyHolder
	{
		// Token: 0x06002D1F RID: 11551
		void Draw();

		// Token: 0x06002D20 RID: 11552
		void TryToHover();
	}
}
