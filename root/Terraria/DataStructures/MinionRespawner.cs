using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x02000555 RID: 1365
	public class MinionRespawner
	{
		// Token: 0x06003793 RID: 14227 RVA: 0x0062F7A5 File Offset: 0x0062D9A5
		public void Clear()
		{
			this._minions.Clear();
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x0062F7B4 File Offset: 0x0062D9B4
		public void CollectMinionsFor(Player player)
		{
			int whoAmI = player.whoAmI;
			this.Clear();
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active && projectile.owner == whoAmI && projectile.MinionSpawnInfo != null)
				{
					this._minions.Add(projectile.MinionSpawnInfo);
				}
			}
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x0062F810 File Offset: 0x0062DA10
		public void RestoreMinionsFor(Player player)
		{
			int mouseX = Main.mouseX;
			int mouseY = Main.mouseY;
			Main.mouseX = Main.screenWidth / 2;
			Main.mouseY = Main.screenHeight / 2;
			foreach (MinionSpawnInfo minionSpawnInfo in this._minions)
			{
				minionSpawnInfo.TryRespawn(player);
			}
			Main.mouseX = mouseX;
			Main.mouseY = mouseY;
			this.Clear();
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x0062F898 File Offset: 0x0062DA98
		public MinionRespawner()
		{
		}

		// Token: 0x04005BC5 RID: 23493
		private List<MinionSpawnInfo> _minions = new List<MinionSpawnInfo>();
	}
}
