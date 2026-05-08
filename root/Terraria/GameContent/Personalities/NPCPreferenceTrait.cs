using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x0200041F RID: 1055
	public class NPCPreferenceTrait : IShopPersonalityTrait
	{
		// Token: 0x06003070 RID: 12400 RVA: 0x005BA0A0 File Offset: 0x005B82A0
		public void ModifyShopPrice(HelperInfo info, ShopHelper shopHelperInstance)
		{
			if (!info.nearbyNPCsByType[this.NpcId])
			{
				return;
			}
			AffectionLevel level = this.Level;
			if (level <= AffectionLevel.Dislike)
			{
				if (level != AffectionLevel.Hate)
				{
					if (level != AffectionLevel.Dislike)
					{
						return;
					}
					shopHelperInstance.DislikeNPC(this.NpcId);
					return;
				}
				else
				{
					shopHelperInstance.HateNPC(this.NpcId);
				}
			}
			else
			{
				if (level == AffectionLevel.Like)
				{
					shopHelperInstance.LikeNPC(this.NpcId);
					return;
				}
				if (level == AffectionLevel.Love)
				{
					shopHelperInstance.LoveNPC(this.NpcId);
					return;
				}
			}
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0000357B File Offset: 0x0000177B
		public NPCPreferenceTrait()
		{
		}

		// Token: 0x040056DA RID: 22234
		public AffectionLevel Level;

		// Token: 0x040056DB RID: 22235
		public int NpcId;
	}
}
