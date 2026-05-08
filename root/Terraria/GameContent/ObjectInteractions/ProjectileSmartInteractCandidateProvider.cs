using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002D9 RID: 729
	public class ProjectileSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x06002613 RID: 9747 RVA: 0x0055D96B File Offset: 0x0055BB6B
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractProj = -1;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0055D974 File Offset: 0x0055BB74
		public bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
		{
			candidate = null;
			if (!settings.FullInteraction)
			{
				return false;
			}
			List<int> listOfProjectilesToInteractWithHack = settings.player.GetListOfProjectilesToInteractWithHack();
			bool flag = false;
			Vector2 mousevec = settings.mousevec;
			mousevec.ToPoint();
			int num = -1;
			float num2 = -1f;
			for (int i = 0; i < listOfProjectilesToInteractWithHack.Count; i++)
			{
				int num3 = listOfProjectilesToInteractWithHack[i];
				Projectile projectile = Main.projectile[num3];
				if (projectile.active)
				{
					float num4 = projectile.Hitbox.Distance(mousevec);
					if (num == -1 || Main.projectile[num].Hitbox.Distance(mousevec) > num4)
					{
						num = num3;
						num2 = num4;
					}
					if (num4 == 0f)
					{
						flag = true;
						num = num3;
						num2 = num4;
						break;
					}
				}
			}
			if (settings.DemandOnlyZeroDistanceTargets && !flag)
			{
				return false;
			}
			if (num != -1)
			{
				this._candidate.Reuse(num, num2);
				candidate = this._candidate;
				return true;
			}
			return false;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x0055DA58 File Offset: 0x0055BC58
		public ProjectileSmartInteractCandidateProvider()
		{
		}

		// Token: 0x04005065 RID: 20581
		private ProjectileSmartInteractCandidateProvider.ReusableCandidate _candidate = new ProjectileSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000823 RID: 2083
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x0600430D RID: 17165 RVA: 0x006C0AA1 File Offset: 0x006BECA1
			// (set) Token: 0x0600430E RID: 17166 RVA: 0x006C0AA9 File Offset: 0x006BECA9
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

			// Token: 0x0600430F RID: 17167 RVA: 0x006C0AB2 File Offset: 0x006BECB2
			public void WinCandidacy()
			{
				Main.SmartInteractProj = this._projectileIndexToTarget;
				Main.SmartInteractShowingGenuine = true;
			}

			// Token: 0x06004310 RID: 17168 RVA: 0x006C0AC5 File Offset: 0x006BECC5
			public void Reuse(int projectileIndex, float projectileDistanceFromCursor)
			{
				this._projectileIndexToTarget = projectileIndex;
				this.DistanceFromCursor = projectileDistanceFromCursor;
			}

			// Token: 0x06004311 RID: 17169 RVA: 0x0000357B File Offset: 0x0000177B
			public ReusableCandidate()
			{
			}

			// Token: 0x04007253 RID: 29267
			[CompilerGenerated]
			private float <DistanceFromCursor>k__BackingField;

			// Token: 0x04007254 RID: 29268
			private int _projectileIndexToTarget;
		}
	}
}
