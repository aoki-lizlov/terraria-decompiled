using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002D8 RID: 728
	public class NPCSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x06002610 RID: 9744 RVA: 0x0055D845 File Offset: 0x0055BA45
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractNPC = -1;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0055D850 File Offset: 0x0055BA50
		public bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
		{
			candidate = null;
			if (!settings.FullInteraction)
			{
				return false;
			}
			Rectangle worldRegion = TileReachCheckSettings.Simple.GetWorldRegion(settings.player, 0);
			Vector2 mousevec = settings.mousevec;
			mousevec.ToPoint();
			bool flag = false;
			int num = -1;
			float num2 = -1f;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.townNPC && npc.Hitbox.Intersects(worldRegion) && !flag)
				{
					float num3 = npc.Hitbox.Distance(mousevec);
					if (num == -1 || Main.npc[num].Hitbox.Distance(mousevec) > num3)
					{
						num = i;
						num2 = num3;
					}
					if (num3 == 0f)
					{
						flag = true;
						num = i;
						num2 = num3;
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

		// Token: 0x06002612 RID: 9746 RVA: 0x0055D958 File Offset: 0x0055BB58
		public NPCSmartInteractCandidateProvider()
		{
		}

		// Token: 0x04005064 RID: 20580
		private NPCSmartInteractCandidateProvider.ReusableCandidate _candidate = new NPCSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000822 RID: 2082
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x06004308 RID: 17160 RVA: 0x006C0A6D File Offset: 0x006BEC6D
			// (set) Token: 0x06004309 RID: 17161 RVA: 0x006C0A75 File Offset: 0x006BEC75
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

			// Token: 0x0600430A RID: 17162 RVA: 0x006C0A7E File Offset: 0x006BEC7E
			public void WinCandidacy()
			{
				Main.SmartInteractNPC = this._npcIndexToTarget;
				Main.SmartInteractShowingGenuine = true;
			}

			// Token: 0x0600430B RID: 17163 RVA: 0x006C0A91 File Offset: 0x006BEC91
			public void Reuse(int npcIndex, float npcDistanceFromCursor)
			{
				this._npcIndexToTarget = npcIndex;
				this.DistanceFromCursor = npcDistanceFromCursor;
			}

			// Token: 0x0600430C RID: 17164 RVA: 0x0000357B File Offset: 0x0000177B
			public ReusableCandidate()
			{
			}

			// Token: 0x04007251 RID: 29265
			[CompilerGenerated]
			private float <DistanceFromCursor>k__BackingField;

			// Token: 0x04007252 RID: 29266
			private int _npcIndexToTarget;
		}
	}
}
