using System;

namespace Terraria.Audio
{
	// Token: 0x020005CC RID: 1484
	public class VampireSizzleTracker
	{
		// Token: 0x06003A3C RID: 14908 RVA: 0x006552E3 File Offset: 0x006534E3
		public VampireSizzleTracker(int whoAmI)
		{
			this._playerIndex = whoAmI;
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x006552F2 File Offset: 0x006534F2
		public bool IsActiveAndInGame()
		{
			return !Main.gameMenu && Main.vampireSeed && Main.player[this._playerIndex].sunScorchCounter > 0;
		}

		// Token: 0x04005DD9 RID: 24025
		private int _playerIndex;
	}
}
