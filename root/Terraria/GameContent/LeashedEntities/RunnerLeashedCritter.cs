using System;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x0200046B RID: 1131
	public class RunnerLeashedCritter : WalkerLeashedCritter
	{
		// Token: 0x060032E2 RID: 13026 RVA: 0x005F2D6C File Offset: 0x005F0F6C
		public RunnerLeashedCritter()
		{
			this.anchorStyle = 1;
			this.walkingPace = 1.5f;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x005F2D86 File Offset: 0x005F0F86
		// Note: this type is marked as 'beforefieldinit'.
		static RunnerLeashedCritter()
		{
		}

		// Token: 0x04005874 RID: 22644
		public new static RunnerLeashedCritter Prototype = new RunnerLeashedCritter();
	}
}
