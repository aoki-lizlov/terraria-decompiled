using System;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000461 RID: 1121
	public class WaterfowlLeashedCritter : BirdLeashedCritter
	{
		// Token: 0x0600329E RID: 12958 RVA: 0x005F0BA4 File Offset: 0x005EEDA4
		public WaterfowlLeashedCritter()
		{
			this.hasGroundBias = true;
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x005F0BB3 File Offset: 0x005EEDB3
		protected override void CopyToDummy()
		{
			base.CopyToDummy();
			if (this.velocity.Y != 0f)
			{
				LeashedCritter._dummy.type++;
			}
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x005F0BDF File Offset: 0x005EEDDF
		// Note: this type is marked as 'beforefieldinit'.
		static WaterfowlLeashedCritter()
		{
		}

		// Token: 0x04005838 RID: 22584
		public new static WaterfowlLeashedCritter Prototype = new WaterfowlLeashedCritter();
	}
}
