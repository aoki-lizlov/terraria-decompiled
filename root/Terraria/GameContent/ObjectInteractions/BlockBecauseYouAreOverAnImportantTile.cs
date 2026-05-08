using System;
using Terraria.ID;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002D6 RID: 726
	public class BlockBecauseYouAreOverAnImportantTile : ISmartInteractBlockReasonProvider
	{
		// Token: 0x0600260A RID: 9738 RVA: 0x0055CA24 File Offset: 0x0055AC24
		public bool ShouldBlockSmartInteract(SmartInteractScanSettings settings)
		{
			int tileTargetX = Player.tileTargetX;
			int tileTargetY = Player.tileTargetY;
			if (!WorldGen.InWorld(tileTargetX, tileTargetY, 10))
			{
				return true;
			}
			Tile tile = Main.tile[tileTargetX, tileTargetY];
			return tile == null || (tile.active() && TileID.Sets.DisableSmartInteract[(int)tile.type]);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0000357B File Offset: 0x0000177B
		public BlockBecauseYouAreOverAnImportantTile()
		{
		}
	}
}
