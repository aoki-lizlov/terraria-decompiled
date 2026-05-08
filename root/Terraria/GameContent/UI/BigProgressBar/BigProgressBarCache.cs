using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000393 RID: 915
	public struct BigProgressBarCache
	{
		// Token: 0x060029F4 RID: 10740 RVA: 0x00580237 File Offset: 0x0057E437
		public void SetLife(float current, float max)
		{
			this.LifeCurrent = current;
			this.LifeMax = max;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00580247 File Offset: 0x0057E447
		public void SetShield(float current, float max)
		{
			this.ShieldCurrent = current;
			this.ShieldMax = max;
		}

		// Token: 0x040052DA RID: 21210
		public float LifeCurrent;

		// Token: 0x040052DB RID: 21211
		public float LifeMax;

		// Token: 0x040052DC RID: 21212
		public float ShieldCurrent;

		// Token: 0x040052DD RID: 21213
		public float ShieldMax;
	}
}
