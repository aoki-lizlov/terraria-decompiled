using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent
{
	// Token: 0x0200026C RID: 620
	public interface ITownNPCProfile
	{
		// Token: 0x06002407 RID: 9223
		int RollVariation();

		// Token: 0x06002408 RID: 9224
		string GetNameForVariant(NPC npc);

		// Token: 0x06002409 RID: 9225
		Asset<Texture2D> GetTextureNPCShouldUse(NPC npc);

		// Token: 0x0600240A RID: 9226
		int GetHeadTextureIndex(NPC npc);
	}
}
