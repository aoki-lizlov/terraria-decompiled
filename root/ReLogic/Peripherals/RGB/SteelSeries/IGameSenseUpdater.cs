using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ReLogic.Peripherals.RGB.SteelSeries
{
	// Token: 0x02000059 RID: 89
	public interface IGameSenseUpdater
	{
		// Token: 0x060001FF RID: 511
		List<JObject> TryGetEventUpdateRequest();
	}
}
