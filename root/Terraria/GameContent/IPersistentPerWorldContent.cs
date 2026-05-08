using System;
using System.IO;

namespace Terraria.GameContent
{
	// Token: 0x0200025B RID: 603
	public interface IPersistentPerWorldContent
	{
		// Token: 0x06002360 RID: 9056
		void Save(BinaryWriter writer);

		// Token: 0x06002361 RID: 9057
		void Load(BinaryReader reader, int gameVersionSaveWasMadeOn);

		// Token: 0x06002362 RID: 9058
		void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn);

		// Token: 0x06002363 RID: 9059
		void Reset();
	}
}
