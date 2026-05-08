using System;

namespace Terraria.GameContent.Ambience
{
	// Token: 0x02000363 RID: 867
	public class AmbientSkyDrawCache
	{
		// Token: 0x060028E3 RID: 10467 RVA: 0x005759DC File Offset: 0x00573BDC
		public void SetUnderworldInfo(int drawIndex, float scale)
		{
			this.Underworld[drawIndex] = new AmbientSkyDrawCache.UnderworldCache
			{
				Scale = scale
			};
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x00575A08 File Offset: 0x00573C08
		public void SetOceanLineInfo(float yScreenPosition, float oceanOpacity)
		{
			this.OceanLineInfo = new AmbientSkyDrawCache.OceanLineCache
			{
				YScreenPosition = yScreenPosition,
				OceanOpacity = oceanOpacity
			};
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x00575A34 File Offset: 0x00573C34
		public AmbientSkyDrawCache()
		{
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x00575A48 File Offset: 0x00573C48
		// Note: this type is marked as 'beforefieldinit'.
		static AmbientSkyDrawCache()
		{
		}

		// Token: 0x04005179 RID: 20857
		public static AmbientSkyDrawCache Instance = new AmbientSkyDrawCache();

		// Token: 0x0400517A RID: 20858
		public AmbientSkyDrawCache.UnderworldCache[] Underworld = new AmbientSkyDrawCache.UnderworldCache[5];

		// Token: 0x0400517B RID: 20859
		public AmbientSkyDrawCache.OceanLineCache OceanLineInfo;

		// Token: 0x020008C8 RID: 2248
		public struct UnderworldCache
		{
			// Token: 0x0400735E RID: 29534
			public float Scale;
		}

		// Token: 0x020008C9 RID: 2249
		public struct OceanLineCache
		{
			// Token: 0x0400735F RID: 29535
			public float YScreenPosition;

			// Token: 0x04007360 RID: 29536
			public float OceanOpacity;
		}
	}
}
