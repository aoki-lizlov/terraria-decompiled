using System;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002D1 RID: 721
	public interface ISmartInteractCandidate
	{
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06002602 RID: 9730
		float DistanceFromCursor { get; }

		// Token: 0x06002603 RID: 9731
		void WinCandidacy();
	}
}
