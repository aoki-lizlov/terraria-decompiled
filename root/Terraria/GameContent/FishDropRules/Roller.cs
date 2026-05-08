using System;
using Terraria.DataStructures;

namespace Terraria.GameContent.FishDropRules
{
	// Token: 0x0200047E RID: 1150
	public class Roller
	{
		// Token: 0x0600334D RID: 13133 RVA: 0x005F5955 File Offset: 0x005F3B55
		public void Roll(Projectile projectile, FishingAttempt fisher)
		{
			FishingContext context = this._context;
			context.Player = Main.player[projectile.owner];
			context.Fisher = fisher;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x005F5975 File Offset: 0x005F3B75
		public Roller()
		{
		}

		// Token: 0x040058C8 RID: 22728
		private FishingContext _context = new FishingContext();

		// Token: 0x040058C9 RID: 22729
		private FishDropRuleList _ruleList = new FishDropRuleList();
	}
}
