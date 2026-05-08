using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ReLogic.Peripherals.RGB.SteelSeries
{
	// Token: 0x0200005C RID: 92
	public class SteelSeriesEventRelay : IGameSenseUpdater
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000927A File Offset: 0x0000747A
		public SteelSeriesEventRelay(ARgbGameValueTracker theEvent)
		{
			this._trackedEvent = theEvent;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009294 File Offset: 0x00007494
		public List<JObject> TryGetEventUpdateRequest()
		{
			this._requestList.Clear();
			JObject jobject = this._trackedEvent.TryGettingRequest();
			if (jobject != null)
			{
				this._requestList.Add(jobject);
			}
			return this._requestList;
		}

		// Token: 0x040002E6 RID: 742
		private List<JObject> _requestList = new List<JObject>();

		// Token: 0x040002E7 RID: 743
		private ARgbGameValueTracker _trackedEvent;
	}
}
