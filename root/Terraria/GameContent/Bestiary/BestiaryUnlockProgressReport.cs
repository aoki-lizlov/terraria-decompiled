using System;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200032D RID: 813
	public struct BestiaryUnlockProgressReport
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x00569C24 File Offset: 0x00567E24
		public float CompletionPercent
		{
			get
			{
				if (this.EntriesTotal == 0)
				{
					return 1f;
				}
				return this.CompletionAmountTotal / (float)this.EntriesTotal;
			}
		}

		// Token: 0x04005118 RID: 20760
		public int EntriesTotal;

		// Token: 0x04005119 RID: 20761
		public float CompletionAmountTotal;
	}
}
