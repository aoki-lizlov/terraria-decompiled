using System;

namespace Terraria.DataStructures
{
	// Token: 0x020005A5 RID: 1445
	public struct WingStats
	{
		// Token: 0x0600390E RID: 14606 RVA: 0x006512A7 File Offset: 0x0064F4A7
		public WingStats(int flyTime = 100, float flySpeedOverride = -1f, float accelerationMultiplier = 1f, bool hasHoldDownHoverFeatures = false, float hoverFlySpeedOverride = -1f, float hoverAccelerationMultiplier = 1f)
		{
			this.FlyTime = flyTime;
			this.AccRunSpeedOverride = flySpeedOverride;
			this.AccRunAccelerationMult = accelerationMultiplier;
			this.HasDownHoverStats = hasHoldDownHoverFeatures;
			this.DownHoverSpeedOverride = hoverFlySpeedOverride;
			this.DownHoverAccelerationMult = hoverAccelerationMultiplier;
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x006512D6 File Offset: 0x0064F4D6
		public WingStats WithSpeedBoost(float multiplier)
		{
			return new WingStats(this.FlyTime, this.AccRunSpeedOverride * multiplier, this.AccRunAccelerationMult, this.HasDownHoverStats, this.DownHoverSpeedOverride * multiplier, this.DownHoverAccelerationMult);
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static WingStats()
		{
		}

		// Token: 0x04005D6F RID: 23919
		public static readonly WingStats Default;

		// Token: 0x04005D70 RID: 23920
		public int FlyTime;

		// Token: 0x04005D71 RID: 23921
		public float AccRunSpeedOverride;

		// Token: 0x04005D72 RID: 23922
		public float AccRunAccelerationMult;

		// Token: 0x04005D73 RID: 23923
		public bool HasDownHoverStats;

		// Token: 0x04005D74 RID: 23924
		public float DownHoverSpeedOverride;

		// Token: 0x04005D75 RID: 23925
		public float DownHoverAccelerationMult;
	}
}
