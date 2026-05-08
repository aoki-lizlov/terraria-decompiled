using System;

namespace Terraria.GameContent.FishDropRules
{
	// Token: 0x0200047F RID: 1151
	public abstract class AFishingCondition
	{
		// Token: 0x0600334F RID: 13135
		public abstract bool Matches(FishingContext context);

		// Token: 0x06003350 RID: 13136 RVA: 0x0000357B File Offset: 0x0000177B
		protected AFishingCondition()
		{
		}

		// Token: 0x040058CA RID: 22730
		public bool CanBeSkippedForDisplay;
	}
}
