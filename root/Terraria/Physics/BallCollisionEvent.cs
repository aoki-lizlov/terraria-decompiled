using System;
using Microsoft.Xna.Framework;

namespace Terraria.Physics
{
	// Token: 0x02000078 RID: 120
	public struct BallCollisionEvent
	{
		// Token: 0x06001566 RID: 5478 RVA: 0x004C4930 File Offset: 0x004C2B30
		public BallCollisionEvent(float timeScale, Vector2 normal, Vector2 impactPoint, Tile tile, Entity entity)
		{
			this.Normal = normal;
			this.ImpactPoint = impactPoint;
			this.Tile = tile;
			this.Entity = entity;
			this.TimeScale = timeScale;
		}

		// Token: 0x040010F0 RID: 4336
		public readonly Vector2 Normal;

		// Token: 0x040010F1 RID: 4337
		public readonly Vector2 ImpactPoint;

		// Token: 0x040010F2 RID: 4338
		public readonly Tile Tile;

		// Token: 0x040010F3 RID: 4339
		public readonly Entity Entity;

		// Token: 0x040010F4 RID: 4340
		public readonly float TimeScale;
	}
}
