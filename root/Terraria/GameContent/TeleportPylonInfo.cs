using System;
using Terraria.DataStructures;

namespace Terraria.GameContent
{
	// Token: 0x02000269 RID: 617
	public struct TeleportPylonInfo : IEquatable<TeleportPylonInfo>
	{
		// Token: 0x06002400 RID: 9216 RVA: 0x00549CC3 File Offset: 0x00547EC3
		public bool Equals(TeleportPylonInfo other)
		{
			return this.PositionInTiles == other.PositionInTiles && this.TypeOfPylon == other.TypeOfPylon;
		}

		// Token: 0x04004DBB RID: 19899
		public Point16 PositionInTiles;

		// Token: 0x04004DBC RID: 19900
		public TeleportPylonType TypeOfPylon;
	}
}
