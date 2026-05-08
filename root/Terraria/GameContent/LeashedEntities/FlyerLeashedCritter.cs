using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000457 RID: 1111
	public class FlyerLeashedCritter : LeashedCritter
	{
		// Token: 0x06003272 RID: 12914 RVA: 0x005EFEE4 File Offset: 0x005EE0E4
		public FlyerLeashedCritter()
		{
			this.anchorStyle = 4;
			this.strayingRangeInBlocks = 7;
			this.minWaitTime = 60;
			this.maxWaitTime = 300;
			this.maxFlySpeed = 1f;
			this.acceleration = 0.2f;
			this.brakeDuration = 10;
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x005EFF36 File Offset: 0x005EE136
		public override void Spawn(bool newlyAdded)
		{
			base.Spawn(newlyAdded);
			if (!WorldGen.SolidTile2((int)base.AnchorPosition.X, (int)(base.AnchorPosition.Y + 1)))
			{
				this.velocity.Y = 0.0001f;
			}
			this.PickNewTarget();
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x005EFF74 File Offset: 0x005EE174
		protected void PickNewTarget()
		{
			bool flag = this.hasGroundBias && base.AnchorPosition.Y == this.TargetPosition.Y && this.rand.Next(4) != 0;
			this.TargetPosition = new Point16((int)base.AnchorPosition.X + this.rand.Next(-this.strayingRangeInBlocks, this.strayingRangeInBlocks + 1), (int)base.AnchorPosition.Y + this.rand.Next(-this.strayingRangeInBlocks, 1));
			if (flag)
			{
				this.TargetPosition.Y = base.AnchorPosition.Y;
			}
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x005F001B File Offset: 0x005EE21B
		protected override void CopyToDummy()
		{
			base.CopyToDummy();
			if (this.velocity.Y != 0f)
			{
				LeashedCritter._dummy.rotation = this.velocity.X * this.rotationScalar;
			}
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x005F0054 File Offset: 0x005EE254
		public override void Update()
		{
			base.Update();
			this.WaitTime -= 1;
			if (this.WaitTime <= 0)
			{
				this.WaitTime = (short)this.rand.Next(this.minWaitTime, this.maxWaitTime + 1);
				this.PickNewTarget();
			}
			Vector2 vector = this.TargetPosition.ToWorldCoordinates(8f, 8f);
			Vector2 vector2 = vector - base.Center;
			float num = vector2.Length();
			Vector2 vector3 = vector2 / num;
			if (vector3.HasNaNs())
			{
				vector3 = Vector2.Zero;
			}
			this.velocity += vector3 * this.acceleration;
			float num2 = this.velocity.Length();
			float num3 = Math.Min(1f, num / ((float)this.brakeDuration * this.maxFlySpeed));
			float num4 = this.maxFlySpeed * Math.Max(num3, 0.25f);
			if (num2 > num4)
			{
				this.velocity *= num4 / num2;
			}
			bool flag = num < this.maxFlySpeed;
			bool flag2 = flag;
			if (!flag2)
			{
				flag2 = WorldGen.SolidTile2((base.Center + base.Size * 0.5f * vector3 + this.velocity).ToTileCoordinates());
			}
			if (flag2)
			{
				if (flag)
				{
					base.Center = vector;
				}
				Point point = base.Center.ToTileCoordinates();
				this.velocity.X = 0f;
				this.velocity.Y = (WorldGen.SolidTile2(point.X, point.Y + 1) ? 0f : 0.0001f);
			}
			else
			{
				base.Center += this.velocity;
				Point point2 = base.Center.ToTileCoordinates();
				if (this.velocity.Y == 0f && !WorldGen.SolidTile2(point2.X, point2.Y + 1))
				{
					this.velocity.Y = 0.0001f;
				}
			}
			int num5 = Math.Sign(this.velocity.X);
			if (num5 != 0 && num5 != this.direction)
			{
				this.direction = num5;
				this.spriteDirection = -this.direction;
			}
			if (Main.netMode != 2)
			{
				this.VisualEffects();
			}
			this.CopyToDummy();
			LeashedCritter._dummy.FindFrame();
			base.CopyFromDummy();
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x005F02C0 File Offset: 0x005EE4C0
		public override Vector2 GetDrawOffset()
		{
			if (this.velocity.Y == 0f)
			{
				Point16 point = base.Center.ToTileCoordinates16();
				if (Framing.GetTileSafely((int)point.X, (int)(point.Y + 1)).halfBrick())
				{
					return new Vector2(0f, 8f);
				}
				return Vector2.Zero;
			}
			else
			{
				if (this.hoverPeriod == 0f || this.hoverAmplitude == 0f)
				{
					return Vector2.Zero;
				}
				return this.GetBobbingOffset();
			}
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x005F0344 File Offset: 0x005EE544
		protected Vector2 GetBobbingOffset()
		{
			double num = Main.timeForVisualEffects + (double)(this.whoAmI * this.npcType);
			num *= (double)(this.hoverPeriod * 6.2831855f);
			return new Vector2(0f, (float)Math.Sin(num) * this.hoverAmplitude);
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x005F038E File Offset: 0x005EE58E
		// Note: this type is marked as 'beforefieldinit'.
		static FlyerLeashedCritter()
		{
		}

		// Token: 0x0400581E RID: 22558
		public static FlyerLeashedCritter Prototype = new FlyerLeashedCritter();

		// Token: 0x0400581F RID: 22559
		protected int minWaitTime;

		// Token: 0x04005820 RID: 22560
		protected int maxWaitTime;

		// Token: 0x04005821 RID: 22561
		protected float maxFlySpeed;

		// Token: 0x04005822 RID: 22562
		protected float acceleration;

		// Token: 0x04005823 RID: 22563
		protected int brakeDuration;

		// Token: 0x04005824 RID: 22564
		protected float rotationScalar;

		// Token: 0x04005825 RID: 22565
		protected float hoverAmplitude;

		// Token: 0x04005826 RID: 22566
		protected float hoverPeriod;

		// Token: 0x04005827 RID: 22567
		protected bool hasGroundBias;

		// Token: 0x04005828 RID: 22568
		private const float HoverYVelocity = 0.0001f;
	}
}
