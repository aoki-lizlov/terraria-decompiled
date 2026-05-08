using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000465 RID: 1125
	internal class WaterStriderLeashedCritter : JumperLeashedCritter
	{
		// Token: 0x060032B4 RID: 12980 RVA: 0x005F1580 File Offset: 0x005EF780
		public WaterStriderLeashedCritter()
		{
			this.minWaitTime = 60;
			this.maxWaitTime = 120;
			this.strayingRangeInBlocks = 5;
			this.maxJumpWidth = 32f;
			this.minJumpWidth = 8f;
			this.maxJumpHeight = 0f;
			this.maxJumpDuration = 14f;
			this.jumpCooldown = 15;
			this.canStandOnWater = true;
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x005F15E8 File Offset: 0x005EF7E8
		public override Vector2 GetDrawOffset()
		{
			Vector2 drawOffset = base.GetDrawOffset();
			Point point = base.Center.ToTileCoordinates();
			for (int i = 0; i < 2; i++)
			{
				point.Y++;
				byte liquid = Framing.GetTileSafely(point).liquid;
				if (liquid != 0)
				{
					drawOffset.Y = (float)((byte.MaxValue - liquid) / 16);
					break;
				}
			}
			return drawOffset;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x005F1644 File Offset: 0x005EF844
		// Note: this type is marked as 'beforefieldinit'.
		static WaterStriderLeashedCritter()
		{
		}

		// Token: 0x04005846 RID: 22598
		public new static WaterStriderLeashedCritter Prototype = new WaterStriderLeashedCritter();
	}
}
