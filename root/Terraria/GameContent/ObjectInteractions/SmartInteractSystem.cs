using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002D5 RID: 725
	public class SmartInteractSystem
	{
		// Token: 0x06002607 RID: 9735 RVA: 0x0055C828 File Offset: 0x0055AA28
		public SmartInteractSystem()
		{
			this._candidateProvidersByOrderOfPriority.Add(new PotionOfReturnSmartInteractCandidateProvider());
			this._candidateProvidersByOrderOfPriority.Add(new ProjectileSmartInteractCandidateProvider());
			this._candidateProvidersByOrderOfPriority.Add(new NPCSmartInteractCandidateProvider());
			this._candidateProvidersByOrderOfPriority.Add(new TileSmartInteractCandidateProvider());
			this._blockProviders.Add(new BlockBecauseYouAreOverAnImportantTile());
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0055C8AC File Offset: 0x0055AAAC
		public void Clear()
		{
			this._candidates.Clear();
			foreach (ISmartInteractCandidateProvider smartInteractCandidateProvider in this._candidateProvidersByOrderOfPriority)
			{
				smartInteractCandidateProvider.ClearSelfAndPrepareForCheck();
			}
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x0055C908 File Offset: 0x0055AB08
		public void RunQuery(SmartInteractScanSettings settings)
		{
			this.Clear();
			using (List<ISmartInteractBlockReasonProvider>.Enumerator enumerator = this._blockProviders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ShouldBlockSmartInteract(settings))
					{
						return;
					}
				}
			}
			using (List<ISmartInteractCandidateProvider>.Enumerator enumerator2 = this._candidateProvidersByOrderOfPriority.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ISmartInteractCandidate smartInteractCandidate;
					if (enumerator2.Current.ProvideCandidate(settings, out smartInteractCandidate))
					{
						this._candidates.Add(smartInteractCandidate);
						if (smartInteractCandidate.DistanceFromCursor == 0f)
						{
							break;
						}
					}
				}
			}
			ISmartInteractCandidate smartInteractCandidate2 = null;
			foreach (ISmartInteractCandidate smartInteractCandidate3 in this._candidates)
			{
				if (smartInteractCandidate2 == null || smartInteractCandidate2.DistanceFromCursor > smartInteractCandidate3.DistanceFromCursor)
				{
					smartInteractCandidate2 = smartInteractCandidate3;
				}
			}
			if (smartInteractCandidate2 == null)
			{
				return;
			}
			smartInteractCandidate2.WinCandidacy();
		}

		// Token: 0x0400505F RID: 20575
		private List<ISmartInteractCandidateProvider> _candidateProvidersByOrderOfPriority = new List<ISmartInteractCandidateProvider>();

		// Token: 0x04005060 RID: 20576
		private List<ISmartInteractBlockReasonProvider> _blockProviders = new List<ISmartInteractBlockReasonProvider>();

		// Token: 0x04005061 RID: 20577
		private List<ISmartInteractCandidate> _candidates = new List<ISmartInteractCandidate>();
	}
}
