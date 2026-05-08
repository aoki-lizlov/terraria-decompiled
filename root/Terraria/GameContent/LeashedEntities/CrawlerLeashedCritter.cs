using System;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000469 RID: 1129
	public class CrawlerLeashedCritter : WalkerLeashedCritter
	{
		// Token: 0x060032DC RID: 13020 RVA: 0x005F2C71 File Offset: 0x005F0E71
		public CrawlerLeashedCritter()
		{
			this.anchorStyle = 1;
			this.walkingPace = 0.4f;
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x005F2C8B File Offset: 0x005F0E8B
		// Note: this type is marked as 'beforefieldinit'.
		static CrawlerLeashedCritter()
		{
		}

		// Token: 0x04005872 RID: 22642
		public new static CrawlerLeashedCritter Prototype = new CrawlerLeashedCritter();
	}
}
