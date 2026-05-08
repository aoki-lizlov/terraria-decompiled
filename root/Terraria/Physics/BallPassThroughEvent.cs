using System;

namespace Terraria.Physics
{
	// Token: 0x0200007B RID: 123
	public struct BallPassThroughEvent
	{
		// Token: 0x06001569 RID: 5481 RVA: 0x004C4957 File Offset: 0x004C2B57
		public BallPassThroughEvent(float timeScale, Tile tile, Entity entity, BallPassThroughType type)
		{
			this.Tile = tile;
			this.Entity = entity;
			this.Type = type;
			this.TimeScale = timeScale;
		}

		// Token: 0x040010FB RID: 4347
		public readonly Tile Tile;

		// Token: 0x040010FC RID: 4348
		public readonly Entity Entity;

		// Token: 0x040010FD RID: 4349
		public readonly BallPassThroughType Type;

		// Token: 0x040010FE RID: 4350
		public readonly float TimeScale;
	}
}
