using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000174 RID: 372
	[StructLayout(0, Pack = 4)]
	public struct SteamItemDetails_t
	{
		// Token: 0x040009F1 RID: 2545
		public SteamItemInstanceID_t m_itemId;

		// Token: 0x040009F2 RID: 2546
		public SteamItemDef_t m_iDefinition;

		// Token: 0x040009F3 RID: 2547
		public ushort m_unQuantity;

		// Token: 0x040009F4 RID: 2548
		public ushort m_unFlags;
	}
}
