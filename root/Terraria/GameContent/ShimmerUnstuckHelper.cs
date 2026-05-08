using System;
using Terraria.GameContent.Drawing;

namespace Terraria.GameContent
{
	// Token: 0x02000247 RID: 583
	public struct ShimmerUnstuckHelper
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x0053BE1D File Offset: 0x0053A01D
		public bool ShouldUnstuck
		{
			get
			{
				return this.IndefiniteProtectionActive || this.TimeLeftUnstuck > 0;
			}
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x0053BE34 File Offset: 0x0053A034
		public void Update(Player player)
		{
			bool flag = !player.shimmering && !player.shimmerWet;
			if (flag)
			{
				this.IndefiniteProtectionActive = false;
			}
			if (this.TimeLeftUnstuck > 0 && !flag)
			{
				this.StartUnstuck();
			}
			if (this.IndefiniteProtectionActive)
			{
				return;
			}
			if (this.TimeLeftUnstuck > 0)
			{
				this.TimeLeftUnstuck--;
				if (this.TimeLeftUnstuck == 0)
				{
					ParticleOrchestrator.BroadcastOrRequestParticleSpawn(ParticleOrchestraType.ShimmerTownNPC, new ParticleOrchestraSettings
					{
						PositionInWorld = player.Bottom
					});
				}
			}
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0053BEB7 File Offset: 0x0053A0B7
		public void StartUnstuck()
		{
			this.IndefiniteProtectionActive = true;
			this.TimeLeftUnstuck = 120;
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x0053BEC8 File Offset: 0x0053A0C8
		public void Clear()
		{
			this.IndefiniteProtectionActive = false;
			this.TimeLeftUnstuck = 0;
		}

		// Token: 0x04004D40 RID: 19776
		public int TimeLeftUnstuck;

		// Token: 0x04004D41 RID: 19777
		public bool IndefiniteProtectionActive;
	}
}
