using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002DA RID: 730
	public class PotionOfReturnSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x06002616 RID: 9750 RVA: 0x0055DA6B File Offset: 0x0055BC6B
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractPotionOfReturn = false;
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x0055DA74 File Offset: 0x0055BC74
		public bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
		{
			candidate = null;
			Rectangle rectangle;
			if (!PotionOfReturnHelper.TryGetGateHitbox(settings.player, out rectangle))
			{
				return false;
			}
			Vector2 vector = rectangle.ClosestPointInRect(settings.mousevec);
			float num = vector.Distance(settings.mousevec);
			Point point = vector.ToTileCoordinates();
			if (point.X < settings.LX || point.X > settings.HX || point.Y < settings.LY || point.Y > settings.HY)
			{
				return false;
			}
			this._candidate.Reuse(num);
			candidate = this._candidate;
			return true;
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x0055DB0B File Offset: 0x0055BD0B
		public PotionOfReturnSmartInteractCandidateProvider()
		{
		}

		// Token: 0x04005066 RID: 20582
		private PotionOfReturnSmartInteractCandidateProvider.ReusableCandidate _candidate = new PotionOfReturnSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000824 RID: 2084
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x1700053E RID: 1342
			// (get) Token: 0x06004312 RID: 17170 RVA: 0x006C0AD5 File Offset: 0x006BECD5
			// (set) Token: 0x06004313 RID: 17171 RVA: 0x006C0ADD File Offset: 0x006BECDD
			public float DistanceFromCursor
			{
				[CompilerGenerated]
				get
				{
					return this.<DistanceFromCursor>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<DistanceFromCursor>k__BackingField = value;
				}
			}

			// Token: 0x06004314 RID: 17172 RVA: 0x006C0AE6 File Offset: 0x006BECE6
			public void WinCandidacy()
			{
				Main.SmartInteractPotionOfReturn = true;
				Main.SmartInteractShowingGenuine = true;
			}

			// Token: 0x06004315 RID: 17173 RVA: 0x006C0AF4 File Offset: 0x006BECF4
			public void Reuse(float distanceFromCursor)
			{
				this.DistanceFromCursor = distanceFromCursor;
			}

			// Token: 0x06004316 RID: 17174 RVA: 0x0000357B File Offset: 0x0000177B
			public ReusableCandidate()
			{
			}

			// Token: 0x04007255 RID: 29269
			[CompilerGenerated]
			private float <DistanceFromCursor>k__BackingField;
		}
	}
}
