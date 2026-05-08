using System;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002D3 RID: 723
	public interface ISmartInteractCandidateProvider
	{
		// Token: 0x06002605 RID: 9733
		void ClearSelfAndPrepareForCheck();

		// Token: 0x06002606 RID: 9734
		bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate);
	}
}
