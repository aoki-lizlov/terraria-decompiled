using System;
using ReLogic.Content;
using ReLogic.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x02000278 RID: 632
	public static class FontAssets
	{
		// Token: 0x06002450 RID: 9296 RVA: 0x0054C8B1 File Offset: 0x0054AAB1
		// Note: this type is marked as 'beforefieldinit'.
		static FontAssets()
		{
		}

		// Token: 0x04004DF8 RID: 19960
		public static Asset<DynamicSpriteFont> ItemStack;

		// Token: 0x04004DF9 RID: 19961
		public static Asset<DynamicSpriteFont> MouseText;

		// Token: 0x04004DFA RID: 19962
		public static Asset<DynamicSpriteFont> DeathText;

		// Token: 0x04004DFB RID: 19963
		public static Asset<DynamicSpriteFont>[] CombatText = new Asset<DynamicSpriteFont>[2];
	}
}
