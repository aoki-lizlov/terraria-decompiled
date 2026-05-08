using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020002E9 RID: 745
	public class SimulatorInfo
	{
		// Token: 0x06002656 RID: 9814 RVA: 0x0055EBFC File Offset: 0x0055CDFC
		public SimulatorInfo()
		{
			this.player = new Player();
			this._originalDayTimeCounter = Main.time;
			this._originalDayTimeFlag = Main.dayTime;
			this._originalPlayerPosition = this.player.position;
			this.runningExpertMode = false;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0055EC48 File Offset: 0x0055CE48
		public void ReturnToOriginalDaytime()
		{
			Main.dayTime = this._originalDayTimeFlag;
			Main.time = this._originalDayTimeCounter;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0055EC60 File Offset: 0x0055CE60
		public void AddItem(int itemId, int amount)
		{
			this.itemCounter.AddItem(itemId, amount, this.runningExpertMode);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0055EC75 File Offset: 0x0055CE75
		public void ReturnToOriginalPlayerPosition()
		{
			this.player.position = this._originalPlayerPosition;
		}

		// Token: 0x0400506C RID: 20588
		public Player player;

		// Token: 0x0400506D RID: 20589
		private double _originalDayTimeCounter;

		// Token: 0x0400506E RID: 20590
		private bool _originalDayTimeFlag;

		// Token: 0x0400506F RID: 20591
		private Vector2 _originalPlayerPosition;

		// Token: 0x04005070 RID: 20592
		public bool runningExpertMode;

		// Token: 0x04005071 RID: 20593
		public LootSimulationItemCounter itemCounter;

		// Token: 0x04005072 RID: 20594
		public NPC npcVictim;
	}
}
