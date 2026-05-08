using System;

namespace NVorbis.Contracts
{
	// Token: 0x02000029 RID: 41
	internal interface IFloorData
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001C0 RID: 448
		bool ExecuteChannel { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001C1 RID: 449
		// (set) Token: 0x060001C2 RID: 450
		bool ForceEnergy { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001C3 RID: 451
		// (set) Token: 0x060001C4 RID: 452
		bool ForceNoEnergy { get; set; }
	}
}
