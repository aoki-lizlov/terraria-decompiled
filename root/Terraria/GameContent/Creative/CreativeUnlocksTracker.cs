using System;
using System.IO;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200032A RID: 810
	public class CreativeUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x060027D0 RID: 10192 RVA: 0x0056955C File Offset: 0x0056775C
		public void Save(BinaryWriter writer)
		{
			this.ItemSacrifices.Save(writer);
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x0056956A File Offset: 0x0056776A
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.ItemSacrifices.Load(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x00569579 File Offset: 0x00567779
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.ValidateWorld(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x00569583 File Offset: 0x00567783
		public void Reset()
		{
			this.ItemSacrifices.Reset();
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x00569590 File Offset: 0x00567790
		public void OnPlayerJoining(int playerIndex)
		{
			this.ItemSacrifices.OnPlayerJoining(playerIndex);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x0056959E File Offset: 0x0056779E
		public CreativeUnlocksTracker()
		{
		}

		// Token: 0x0400510B RID: 20747
		public ItemsSacrificedUnlocksTracker ItemSacrifices = new ItemsSacrificedUnlocksTracker();
	}
}
