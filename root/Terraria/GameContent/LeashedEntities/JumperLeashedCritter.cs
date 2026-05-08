using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000464 RID: 1124
	internal class JumperLeashedCritter : LeashedCritter
	{
		// Token: 0x060032A8 RID: 12968 RVA: 0x005F0E20 File Offset: 0x005EF020
		public JumperLeashedCritter()
		{
			this.strayingRangeInBlocks = 12;
			this.minWaitTime = 180;
			this.maxWaitTime = 300;
			this.maxJumpWidth = 112f;
			this.minJumpWidth = 48f;
			this.maxJumpHeight = 64f;
			this.maxJumpDuration = 30f;
			this.jumpCooldown = 60;
			this.canStandOnWater = false;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x005F0E8C File Offset: 0x005EF08C
		public override void Spawn(bool newlyAdded)
		{
			base.Spawn(newlyAdded);
			this.PickNewTarget();
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x005F0E9C File Offset: 0x005EF09C
		public override void Update()
		{
			base.Update();
			this.WaitTime -= 1;
			if (this.WaitTime <= 0)
			{
				byte state = this.State;
				if (state != 0)
				{
					if (state == 1)
					{
						base.Recall();
						this.PickNewTarget();
						this.SetJumpCooldown();
						this.State = 0;
					}
				}
				else if (!this.TryStartJump())
				{
					this.PickNewTarget();
					this.SetJumpCooldown();
				}
			}
			bool flag;
			this.Move(out flag);
			if (flag && this.State != 1)
			{
				this.PickNewTarget();
				this.SetJumpCooldown();
			}
			if ((this.TargetPosition.ToWorldCoordinates(8f, 8f) - base.Center).Length() < 8f)
			{
				base.Center = this.TargetPosition.ToWorldCoordinates(8f, 8f);
				this.velocity = Vector2.Zero;
				this.PickNewTarget();
				this.SetJumpCooldown();
			}
			this.spriteDirection = this.direction;
			if (Main.netMode != 2)
			{
				this.VisualEffects();
			}
			this.CopyToDummy();
			LeashedCritter._dummy.FindFrame();
			base.CopyFromDummy();
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x005F0FB7 File Offset: 0x005EF1B7
		private void SetJumpCooldown()
		{
			this.WaitTime = (short)this.rand.Next(this.minWaitTime, this.maxWaitTime + 1);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x005F0FDC File Offset: 0x005EF1DC
		private bool TryStartJump()
		{
			Vector2 vector = this.TargetPosition.ToWorldCoordinates(8f, 8f) - base.Center;
			if (vector.Y * -1f > this.maxJumpHeight)
			{
				return false;
			}
			float num = Math.Min(Math.Abs(vector.X), this.maxJumpWidth);
			if (num <= this.minJumpWidth)
			{
				return false;
			}
			this.direction = Math.Sign(vector.X);
			float num2 = num / this.maxJumpWidth;
			float num3 = this.maxJumpDuration * num2;
			this.velocity.X = num / num3 * (float)this.direction;
			this.velocity.Y = vector.Y * num2 / num3 - 0.5f * LeashedCritter.gravity * num3;
			if (this.velocity.Y >= 0f)
			{
				return false;
			}
			this.WaitTime = (short)(num3 + (float)this.jumpCooldown);
			return true;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x005F10C4 File Offset: 0x005EF2C4
		private void Move(out bool hitSomething)
		{
			hitSomething = false;
			Point point = base.Center.ToTileCoordinates();
			int num = Math.Sign((int)this.velocity.X);
			if (num != 0)
			{
				this.direction = num;
			}
			int num2 = Math.Sign((int)this.velocity.Y);
			Vector2 vector = new Vector2((float)num, (float)num2) * base.Size * 0.5f;
			Vector2 vector2 = base.Center + vector + this.velocity;
			if (!WorldGen.SolidTile2(vector2.ToTileCoordinates()))
			{
				this.Move_NoObstruction(point, vector2.Y);
				return;
			}
			hitSomething = true;
			bool flag = false;
			if (num2 != 0)
			{
				Point point2 = point;
				point2.Y += num2;
				flag = WorldGen.SolidTile2(point2);
			}
			bool flag2 = false;
			if (num != 0)
			{
				Point point3 = point;
				point3.X += num;
				flag2 = WorldGen.SolidTile2(point3);
			}
			if (flag)
			{
				this.velocity.Y = 0f;
			}
			if (flag2)
			{
				this.velocity.X = 0f;
			}
			if (!flag && !flag2)
			{
				this.velocity = Vector2.Zero;
			}
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x005F11E0 File Offset: 0x005EF3E0
		private void Move_NoObstruction(Point currentTile, float nextY)
		{
			if (this.velocity.Y >= 0f && nextY % 16f >= 8f)
			{
				Point point = currentTile;
				point.Y++;
				if (WorldGen.SolidTile2(point) || (this.canStandOnWater && WorldGen.AnyLiquidAt(point.X, point.Y, 0)))
				{
					base.Center = currentTile.ToWorldCoordinates(8f, 8f);
					this.velocity = Vector2.Zero;
					return;
				}
			}
			base.Center += this.velocity;
			this.velocity.Y = this.velocity.Y + LeashedCritter.gravity;
			if (this.velocity.Y > LeashedCritter.maxFallSpeed)
			{
				this.velocity.Y = LeashedCritter.maxFallSpeed;
			}
			if (this.State != 1 && currentTile.Y - (int)base.AnchorPosition.Y > this.strayingRangeInBlocks)
			{
				this.State = 1;
				this.WaitTime = 20;
			}
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x005F12E0 File Offset: 0x005EF4E0
		private void PickNewTarget()
		{
			int num = (int)(this.maxJumpWidth / 16f);
			int num2 = (int)(this.minJumpWidth / 16f);
			int num3 = (int)this.TargetPosition.X - ((int)base.AnchorPosition.X - this.strayingRangeInBlocks);
			int num4 = (int)base.AnchorPosition.X + this.strayingRangeInBlocks - (int)this.TargetPosition.X;
			bool flag = num3 >= num2;
			bool flag2 = num4 >= num2;
			if (!flag && !flag2)
			{
				return;
			}
			int num5;
			if (flag && flag2)
			{
				num5 = this.rand.Next(2) * 2 - 1;
			}
			else
			{
				num5 = (flag ? (-1) : 1);
			}
			int num6 = ((num5 < 1) ? num3 : num4);
			int num7 = this.rand.Next(1, num6 / num + 1);
			int num8 = num6 % num;
			if (num8 < num2)
			{
				num8 = 0;
			}
			int num9 = (int)this.TargetPosition.X + (num7 * num + num8) * num5;
			Point16 point;
			if (!this.TryGetReachableTile(num9, out point))
			{
				return;
			}
			this.TargetPosition = point;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x005F13E4 File Offset: 0x005EF5E4
		private bool TryGetReachableTile(int startX, out Point16 tile)
		{
			tile = Point16.Zero;
			int num = Math.Sign((int)base.AnchorPosition.X - startX);
			if (num == 0)
			{
				return false;
			}
			for (int num2 = startX; num2 != (int)base.AnchorPosition.X; num2 += num)
			{
				tile = new Point16(num2, (int)base.AnchorPosition.Y);
				if (WorldGen.SolidTile2(tile))
				{
					float num3 = this.maxJumpHeight / 16f;
					int num4 = 0;
					while ((float)num4 < num3)
					{
						tile.Y -= 1;
						if (!WorldGen.SolidTile2(tile))
						{
							return true;
						}
						num4++;
					}
				}
				else
				{
					for (int i = 0; i < this.strayingRangeInBlocks; i++)
					{
						tile.Y += 1;
						if (WorldGen.SolidTile2(tile) || (this.canStandOnWater && WorldGen.AnyLiquidAt((int)tile.X, (int)tile.Y, 0)))
						{
							tile.Y -= 1;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x005F14F3 File Offset: 0x005EF6F3
		protected override void CopyToDummy()
		{
			base.CopyToDummy();
			if (this.State == 1)
			{
				LeashedCritter._dummy.Opacity = (float)this.WaitTime / 20f;
			}
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x005F151C File Offset: 0x005EF71C
		public override Vector2 GetDrawOffset()
		{
			Point16 point = base.Center.ToTileCoordinates16();
			if (Framing.GetTileSafely((int)point.X, (int)(point.Y + 1)).halfBrick())
			{
				return new Vector2(0f, base.Center.Y % 16f);
			}
			return base.GetDrawOffset();
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x005F1571 File Offset: 0x005EF771
		// Note: this type is marked as 'beforefieldinit'.
		static JumperLeashedCritter()
		{
		}

		// Token: 0x0400583B RID: 22587
		public static JumperLeashedCritter Prototype = new JumperLeashedCritter();

		// Token: 0x0400583C RID: 22588
		private const int State_Normal = 0;

		// Token: 0x0400583D RID: 22589
		private const int State_Recalling = 1;

		// Token: 0x0400583E RID: 22590
		protected int minWaitTime;

		// Token: 0x0400583F RID: 22591
		protected int maxWaitTime;

		// Token: 0x04005840 RID: 22592
		protected float maxJumpWidth;

		// Token: 0x04005841 RID: 22593
		protected float minJumpWidth;

		// Token: 0x04005842 RID: 22594
		protected float maxJumpHeight;

		// Token: 0x04005843 RID: 22595
		protected float maxJumpDuration;

		// Token: 0x04005844 RID: 22596
		protected int jumpCooldown;

		// Token: 0x04005845 RID: 22597
		protected bool canStandOnWater;
	}
}
