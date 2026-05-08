using System;
using System.IO;

namespace Terraria.GameContent
{
	// Token: 0x0200025C RID: 604
	public interface IPersistentPerPlayerContent
	{
		// Token: 0x06002364 RID: 9060
		void Save(Player player, BinaryWriter writer);

		// Token: 0x06002365 RID: 9061
		void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn);

		// Token: 0x06002366 RID: 9062
		void ApplyLoadedDataToOutOfPlayerFields(Player player);

		// Token: 0x06002367 RID: 9063
		void ResetDataForNewPlayer(Player player);

		// Token: 0x06002368 RID: 9064
		void Reset();
	}
}
