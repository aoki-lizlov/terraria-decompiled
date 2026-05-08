using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000467 RID: 1127
	public class LeashedKite : LeashedEntity
	{
		// Token: 0x060032C6 RID: 12998 RVA: 0x005F1E50 File Offset: 0x005F0050
		public void SetDefaults(int projType)
		{
			this.projType = projType;
			LeashedKite._dummy.SetDefaults(projType);
			base.Size = LeashedKite._dummy.Size;
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x005F1E74 File Offset: 0x005F0074
		public override void NetSend(BinaryWriter writer, bool full)
		{
			if (full)
			{
				writer.Write7BitEncodedInt(this.projType);
			}
			writer.WriteVector2(this.position);
			writer.WritePackedVector2(this.velocity);
			writer.Write((byte)((double)(this.rotation * 256f) / 6.283185307179586));
			writer.Write(this.windTarget);
			writer.Write(this.cloudAlpha);
			writer.Write(this.timeCounter);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x005F1EEC File Offset: 0x005F00EC
		public override void NetReceive(BinaryReader reader, bool full)
		{
			if (full)
			{
				this.SetDefaults(reader.Read7BitEncodedInt());
			}
			Vector2 position = this.position;
			this.position = reader.ReadVector2();
			this.velocity = reader.ReadPackedVector2();
			this.rotation = (float)((double)reader.ReadByte() * 3.141592653589793 * 2.0 / 256.0);
			this.windTarget = reader.ReadSingle();
			this.cloudAlpha = reader.ReadSingle();
			this.timeCounter = reader.ReadSingle();
			if (full)
			{
				this.netOffset = Vector2.Zero;
			}
			else
			{
				this.netOffset += position - this.position;
			}
			if (full)
			{
				this.Update();
				this.FixFirstTimeAppearance();
			}
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x005F1FB3 File Offset: 0x005F01B3
		private void FixFirstTimeAppearance()
		{
			if (!WorldGen.InAPlaceWithWind(this.position, this.width, this.height))
			{
				this.projectileLocalAI0 = 300f;
				this.projectileLocalAI1 = 1f;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x005F1FE4 File Offset: 0x005F01E4
		private Vector2 AnchorWorldPosition
		{
			get
			{
				return base.AnchorPosition.ToWorldCoordinates(8f, 8f);
			}
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x005F1FFC File Offset: 0x005F01FC
		public override void Draw()
		{
			Main.instance.LoadProjectile(this.projType);
			this.CopyToDummy();
			LeashedKite._dummy.position += this.netOffset;
			Main.DrawKite(LeashedKite._dummy, this.AnchorWorldPosition);
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x005F204A File Offset: 0x005F024A
		public override void Update()
		{
			this.Update(false);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x005F2054 File Offset: 0x005F0254
		public void Update(bool fastForward)
		{
			if (this.oldPos == null)
			{
				int num = ProjectileID.Sets.TrailCacheLength[this.projType];
				this.oldPos = new Vector2[num];
				this.oldRot = new float[num];
				this.oldSpriteDirection = new int[num];
			}
			if (base.NearbySectionsMissing(3))
			{
				return;
			}
			if (fastForward || Vector2.DistanceSquared(this.position, this.oldPos[0]) > 256f)
			{
				for (int i = 0; i < this.oldPos.Length; i++)
				{
					this.oldPos[i] = this.position;
					this.oldRot[i] = this.rotation;
					this.oldSpriteDirection[i] = this.spriteDirection;
				}
			}
			if (Main.netMode != 1)
			{
				this.windTarget = Main.WindForVisuals;
				this.cloudAlpha = Main.cloudAlpha;
			}
			this.windCurrent = 0f;
			if (WorldGen.InAPlaceWithWind(this.position, this.width, this.height))
			{
				this.windCurrent = (fastForward ? this.windTarget : MathHelper.Lerp(this.windCurrent, this.windTarget, 0.05f));
			}
			else
			{
				this.windTarget = 0f;
			}
			this.timeWithoutWind = ((Math.Abs(this.windCurrent) >= 0.2f) ? 0 : (fastForward ? 3600 : (this.timeWithoutWind + 1)));
			this.kiteDistance = Utils.Remap((float)this.timeWithoutWind, 120f, 420f, 250f, 48f, true);
			this.MoveKite(fastForward);
			this.netOffset = this.netOffset.MoveTowards(Vector2.Zero, 2f);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x005F21FC File Offset: 0x005F03FC
		private void MoveKite(bool fastForward = false)
		{
			this.CopyToDummy();
			LeashedKite._dummy.owner = 255;
			Player player = Main.player[255];
			Vector2 anchorWorldPosition = this.AnchorWorldPosition;
			player.Center = anchorWorldPosition;
			if (this.timeWithoutWind == 0)
			{
				int num = ((LeashedKite._dummy.Center.X - anchorWorldPosition.X < 0f) ? (-1) : 1);
				LeashedKite._dummy.spriteDirection = num;
				player.direction = num;
			}
			this.timeCounter += 0.016666668f;
			KiteFlyingInfo kiteFlyingInfo = new KiteFlyingInfo
			{
				BobOffset = (anchorWorldPosition.X + anchorWorldPosition.Y * 0.92f) * 0.0025f,
				WindInWorld = this.windCurrent,
				CloudAlpha = this.cloudAlpha,
				GlobalTime = this.timeCounter,
				CanReelThroughBlocks = false
			};
			if (fastForward)
			{
				LeashedKite._dummy.KiteLogic(anchorWorldPosition, kiteFlyingInfo);
				this.timeCounter = 6f;
				Vector2 vector = new Vector2(kiteFlyingInfo.WindInWorld, (float)((kiteFlyingInfo.WindInWorld > 0f) ? (-2) : 2)).SafeNormalize(Vector2.Zero) * this.kiteDistance;
				Vector2 position = LeashedKite._dummy.position;
				LeashedKite._dummy.velocity = vector;
				LeashedKite._dummy.HandleMovement(LeashedKite._dummy.velocity);
				LeashedKite._dummy.position = LeashedKite._dummy.position.MoveTowards(position, 1f);
				if (LeashedKite._dummy.velocity.Length() > 4f)
				{
					LeashedKite._dummy.velocity = LeashedKite._dummy.velocity.SafeNormalize(Vector2.Zero) * 4f;
				}
				LeashedKite._dummy.KiteLogic(anchorWorldPosition, kiteFlyingInfo);
				if (kiteFlyingInfo.WindInWorld == 0f)
				{
					LeashedKite._dummy.rotation = 0f;
					LeashedKite._dummy.localAI[0] = 300f;
					LeashedKite._dummy.localAI[1] = 1f;
				}
				for (int i = this.oldPos.Length - 1; i >= 0; i--)
				{
					this.oldPos[i] = LeashedKite._dummy.position;
					this.oldRot[i] = LeashedKite._dummy.rotation;
					this.oldSpriteDirection[i] = LeashedKite._dummy.spriteDirection;
				}
			}
			else
			{
				Utils.Shift<Vector2>(this.oldPos, 1);
				Utils.Shift<float>(this.oldRot, 1);
				Utils.Shift<int>(this.oldSpriteDirection, 1);
				this.oldPos[0] = this.position;
				this.oldRot[0] = this.rotation;
				this.oldSpriteDirection[0] = this.spriteDirection;
				LeashedKite._dummy.KiteLogic(anchorWorldPosition, kiteFlyingInfo);
				LeashedKite._dummy.HandleMovement(LeashedKite._dummy.velocity);
				Vector2 vector2;
				int num2;
				int num3;
				LeashedKite._dummy.GetCollisionParams(out vector2, out num2, out num3);
				if (Collision.SolidFullTiles(LeashedKite._dummy.position + LeashedKite._dummy.Size / 2f - new Vector2((float)num2, (float)num3) * vector2, new Vector2((float)num2, (float)num3)))
				{
					LeashedKite._dummy.Bottom = LeashedKite._dummy.Bottom.MoveTowards(anchorWorldPosition, 2f);
				}
			}
			this.CopyFromDummy();
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x005F2554 File Offset: 0x005F0754
		public override void Spawn(bool newlyAdded)
		{
			base.Center = this.AnchorWorldPosition;
			this.velocity = new Vector2(0f, -5f);
			this.Update(!newlyAdded);
			this.windCurrent = (this.windTarget = Main.WindForVisuals);
			this.cloudAlpha = Main.cloudAlpha;
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x005F25AC File Offset: 0x005F07AC
		private void CopyToDummy()
		{
			LeashedKite._dummy.type = this.projType;
			LeashedKite._dummy.Size = base.Size;
			LeashedKite._dummy.frame = this.frame;
			LeashedKite._dummy.frameCounter = this.frameCounter;
			LeashedKite._dummy.position = this.position;
			LeashedKite._dummy.velocity = this.velocity;
			LeashedKite._dummy.rotation = this.rotation;
			LeashedKite._dummy.spriteDirection = this.spriteDirection;
			LeashedKite._dummy.oldPos = this.oldPos;
			LeashedKite._dummy.oldRot = this.oldRot;
			LeashedKite._dummy.oldSpriteDirection = this.oldSpriteDirection;
			LeashedKite._dummy.scale = 1f;
			LeashedKite._dummy.ai[0] = this.kiteDistance;
			LeashedKite._dummy.localAI[0] = this.projectileLocalAI0;
			LeashedKite._dummy.localAI[1] = this.projectileLocalAI1;
			LeashedKite._dummy.extraUpdates = 0;
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x005F26BC File Offset: 0x005F08BC
		private void CopyFromDummy()
		{
			this.frame = LeashedKite._dummy.frame;
			this.frameCounter = LeashedKite._dummy.frameCounter;
			this.position = LeashedKite._dummy.position;
			this.velocity = LeashedKite._dummy.velocity;
			this.rotation = LeashedKite._dummy.rotation;
			this.spriteDirection = LeashedKite._dummy.spriteDirection;
			this.projectileLocalAI0 = LeashedKite._dummy.localAI[0];
			this.projectileLocalAI1 = LeashedKite._dummy.localAI[1];
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x005F274D File Offset: 0x005F094D
		public LeashedKite()
		{
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x005F2767 File Offset: 0x005F0967
		// Note: this type is marked as 'beforefieldinit'.
		static LeashedKite()
		{
		}

		// Token: 0x04005858 RID: 22616
		public static LeashedKite Prototype;

		// Token: 0x04005859 RID: 22617
		private static Projectile _dummy = new Projectile();

		// Token: 0x0400585A RID: 22618
		public int projType;

		// Token: 0x0400585B RID: 22619
		public int frame;

		// Token: 0x0400585C RID: 22620
		public int frameCounter;

		// Token: 0x0400585D RID: 22621
		public float rotation;

		// Token: 0x0400585E RID: 22622
		public int spriteDirection = 1;

		// Token: 0x0400585F RID: 22623
		public float kiteDistance = 250f;

		// Token: 0x04005860 RID: 22624
		public float windTarget;

		// Token: 0x04005861 RID: 22625
		public float windCurrent;

		// Token: 0x04005862 RID: 22626
		public float timeCounter;

		// Token: 0x04005863 RID: 22627
		public float cloudAlpha;

		// Token: 0x04005864 RID: 22628
		public int timeWithoutWind;

		// Token: 0x04005865 RID: 22629
		public float projectileLocalAI0;

		// Token: 0x04005866 RID: 22630
		public float projectileLocalAI1;

		// Token: 0x04005867 RID: 22631
		public Vector2[] oldPos;

		// Token: 0x04005868 RID: 22632
		public float[] oldRot;

		// Token: 0x04005869 RID: 22633
		public int[] oldSpriteDirection;

		// Token: 0x0400586A RID: 22634
		public Vector2 netOffset;
	}
}
