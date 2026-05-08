using System;
using System.Collections.Generic;
using SteelSeries.GameSense;

namespace ReLogic.Peripherals.RGB.SteelSeries
{
	// Token: 0x0200005B RID: 91
	public class GameSenseSpecificInfo
	{
		// Token: 0x06000201 RID: 513 RVA: 0x0000448A File Offset: 0x0000268A
		public GameSenseSpecificInfo()
		{
		}

		// Token: 0x040002E4 RID: 740
		public List<Bind_Event> EventsToBind;

		// Token: 0x040002E5 RID: 741
		public List<ARgbGameValueTracker> MiscEvents;
	}
}
