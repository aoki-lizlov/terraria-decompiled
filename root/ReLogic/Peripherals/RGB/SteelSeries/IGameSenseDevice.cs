using System;
using SteelSeries.GameSense;

namespace ReLogic.Peripherals.RGB.SteelSeries
{
	// Token: 0x0200005A RID: 90
	public interface IGameSenseDevice : IGameSenseUpdater
	{
		// Token: 0x06000200 RID: 512
		void CollectEventsToTrack(Bind_Event[] bindEvents, ARgbGameValueTracker[] miscEvents);
	}
}
