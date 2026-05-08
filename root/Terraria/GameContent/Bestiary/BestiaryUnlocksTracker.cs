using System;
using System.IO;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000333 RID: 819
	public class BestiaryUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x0600280E RID: 10254 RVA: 0x00571C72 File Offset: 0x0056FE72
		public void Save(BinaryWriter writer)
		{
			this.Kills.Save(writer);
			this.Sights.Save(writer);
			this.Chats.Save(writer);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x00571C98 File Offset: 0x0056FE98
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.Kills.Load(reader, gameVersionSaveWasMadeOn);
			this.Sights.Load(reader, gameVersionSaveWasMadeOn);
			this.Chats.Load(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x00571CC1 File Offset: 0x0056FEC1
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.Kills.ValidateWorld(reader, gameVersionSaveWasMadeOn);
			this.Sights.ValidateWorld(reader, gameVersionSaveWasMadeOn);
			this.Chats.ValidateWorld(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x00571CEA File Offset: 0x0056FEEA
		public void Reset()
		{
			this.Kills.Reset();
			this.Sights.Reset();
			this.Chats.Reset();
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x00571D0D File Offset: 0x0056FF0D
		public void OnPlayerJoining(int playerIndex)
		{
			this.Kills.OnPlayerJoining(playerIndex);
			this.Sights.OnPlayerJoining(playerIndex);
			this.Chats.OnPlayerJoining(playerIndex);
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x00009E46 File Offset: 0x00008046
		public void FillBasedOnVersionBefore210()
		{
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00571D33 File Offset: 0x0056FF33
		public BestiaryUnlocksTracker()
		{
		}

		// Token: 0x0400511B RID: 20763
		public NPCKillsTracker Kills = new NPCKillsTracker();

		// Token: 0x0400511C RID: 20764
		public NPCWasNearPlayerTracker Sights = new NPCWasNearPlayerTracker();

		// Token: 0x0400511D RID: 20765
		public NPCWasChatWithTracker Chats = new NPCWasChatWithTracker();
	}
}
