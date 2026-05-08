using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000468 RID: 1128
	public class WalkerLeashedCritter : LeashedCritter
	{
		// Token: 0x060032D4 RID: 13012 RVA: 0x005F2773 File Offset: 0x005F0973
		public WalkerLeashedCritter()
		{
			this.walkingPace = 0.8f;
			this.strayingRangeInBlocks = 3;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x005F2790 File Offset: 0x005F0990
		protected bool AdvanceTargetPosition()
		{
			if (Math.Abs((int)(this.TargetPosition.X - base.AnchorPosition.X)) >= this.strayingRangeInBlocks)
			{
				this.direction = Math.Sign((int)(base.AnchorPosition.X - this.TargetPosition.X));
			}
			if (!WorldGen.InWorld((int)this.TargetPosition.X + this.direction, (int)this.TargetPosition.Y, 0))
			{
				this.direction *= -1;
			}
			this.spriteDirection = this.direction;
			int num = (int)this.TargetPosition.X + this.direction;
			short y = this.TargetPosition.Y;
			bool flag = !WorldGen.SolidTile2(num, (int)(y - 1));
			bool flag2 = !WorldGen.SolidTile2(num, (int)y);
			bool flag3 = !WorldGen.SolidTile2(num, (int)(y + 1));
			bool flag4 = WorldGen.AnyLiquidAt(num, (int)(y + 1), -1);
			bool flag5 = !WorldGen.SolidTile2(num, (int)(y + 2));
			bool flag6 = flag && !flag2;
			bool flag7 = flag2 && flag3 && !flag4 && !flag5;
			bool flag8 = flag2 && !flag3;
			if (flag6)
			{
				this.TargetPosition = new Point16(num, (int)(y - 1));
			}
			else if (flag7)
			{
				this.TargetPosition = new Point16(num, (int)(y + 1));
			}
			else
			{
				if (!flag8)
				{
					return false;
				}
				this.TargetPosition = new Point16(num, (int)y);
			}
			return true;
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x005F28EC File Offset: 0x005F0AEC
		public override void Update()
		{
			base.Update();
			Point16 point = base.Center.ToTileCoordinates16();
			this.HandleFalling(point);
			this.WaitTime -= 1;
			if (this.WaitTime <= 0)
			{
				if (this.State == 4)
				{
					base.Recall();
				}
				this.WaitTime = (short)this.rand.Next(60, 61);
				this.State = (byte)this.rand.Next(2);
			}
			this.HandleWalking();
			int num = (int)(this.TargetPosition.X - point.X);
			int num2 = (int)(this.TargetPosition.Y - point.Y);
			if (Math.Abs(num) == 1 && Math.Abs(num2) == 1)
			{
				this.velocity.Y = (float)(num2 * 2);
			}
			float num3 = this.velocity.Length();
			Vector2 vector = this.TargetPosition.ToWorldCoordinates(8f, 8f);
			base.Center = base.Center.MoveTowards(vector, num3);
			if (base.Center == vector && this.State == 0)
			{
				this.velocity = Vector2.Zero;
			}
			if (Main.netMode != 2)
			{
				this.VisualEffects();
			}
			this.CopyToDummy();
			LeashedCritter._dummy.FindFrame();
			base.CopyFromDummy();
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x005F2A2C File Offset: 0x005F0C2C
		private void HandleFalling(Point16 tilePosition)
		{
			if (WorldGen.SolidTile2((int)tilePosition.X, (int)(tilePosition.Y + 1)))
			{
				this.velocity.Y = 0f;
				if (this.State == 3 || this.State == 4)
				{
					base.Center = this.TargetPosition.ToWorldCoordinates(8f, 8f);
				}
				if (this.State != 3)
				{
					return;
				}
				this.State = 0;
				this.WaitTime = 0;
				return;
			}
			else
			{
				this.velocity.Y = this.velocity.Y + LeashedCritter.gravity;
				if (this.velocity.Y > LeashedCritter.maxFallSpeed)
				{
					this.velocity.Y = LeashedCritter.maxFallSpeed;
				}
				this.TargetPosition.X = tilePosition.X;
				this.TargetPosition.Y = (short)Math.Min((int)(tilePosition.Y + 1), Main.maxTilesY - 1);
				if (this.State == 4)
				{
					return;
				}
				if ((int)(this.TargetPosition.Y - base.AnchorPosition.Y) > this.strayingRangeInBlocks)
				{
					this.State = 4;
					this.WaitTime = 20;
					return;
				}
				this.State = 3;
				return;
			}
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x005F2B4C File Offset: 0x005F0D4C
		private void HandleWalking()
		{
			if (this.State == 3 || this.State == 4)
			{
				return;
			}
			this.velocity.X = this.walkingPace * (float)this.direction;
			if (this.State == 0)
			{
				return;
			}
			if (base.Center.Distance(this.TargetPosition.ToWorldCoordinates(8f, 8f)) >= 1f)
			{
				return;
			}
			if (this.State == 1)
			{
				this.direction = this.rand.Next(2) * 2 - 1;
				this.State = 2;
			}
			if (this.AdvanceTargetPosition())
			{
				return;
			}
			this.WaitTime = 30;
			this.State = 0;
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x005F2BF4 File Offset: 0x005F0DF4
		protected override void CopyToDummy()
		{
			base.CopyToDummy();
			if (this.State == 4)
			{
				LeashedCritter._dummy.Opacity = (float)this.WaitTime / 20f;
			}
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x005F2C1C File Offset: 0x005F0E1C
		public override Vector2 GetDrawOffset()
		{
			Point16 point = base.Center.ToTileCoordinates16();
			if (Framing.GetTileSafely((int)point.X, (int)(point.Y + 1)).halfBrick())
			{
				return new Vector2(0f, 8f);
			}
			return base.GetDrawOffset();
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x005F2C65 File Offset: 0x005F0E65
		// Note: this type is marked as 'beforefieldinit'.
		static WalkerLeashedCritter()
		{
		}

		// Token: 0x0400586B RID: 22635
		public static WalkerLeashedCritter Prototype = new WalkerLeashedCritter();

		// Token: 0x0400586C RID: 22636
		private const int State_Standing = 0;

		// Token: 0x0400586D RID: 22637
		private const int State_PickDirection = 1;

		// Token: 0x0400586E RID: 22638
		private const int State_Walking = 2;

		// Token: 0x0400586F RID: 22639
		private const int State_Falling = 3;

		// Token: 0x04005870 RID: 22640
		private const int State_Recalling = 4;

		// Token: 0x04005871 RID: 22641
		protected float walkingPace;
	}
}
