using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000466 RID: 1126
	public abstract class LeashedCritter : LeashedEntity
	{
		// Token: 0x060032B7 RID: 12983 RVA: 0x005F1650 File Offset: 0x005EF850
		public void SetDefaults(int itemType)
		{
			this.SetDefaults(ContentSamples.ItemsByType[itemType]);
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x005F1664 File Offset: 0x005EF864
		protected virtual void SetDefaults(Item sample)
		{
			this.npcType = (int)sample.makeNPC;
			LeashedCritter._dummy.SetDefaults(this.npcType, default(NPCSpawnParams));
			base.Size = LeashedCritter._dummy.Size;
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x005F16A8 File Offset: 0x005EF8A8
		public override void NetSend(BinaryWriter writer, bool full)
		{
			if (full)
			{
				writer.Write7BitEncodedInt(this.npcType);
				writer.WriteVector2(base.Size);
			}
			writer.WritePackedVector2(this.position - base.AnchorPosition.ToWorldCoordinates(8f, 8f));
			writer.Write(this.direction > 0);
			writer.Write(this.rand.state);
			writer.Write(this.WaitTime);
			writer.Write(this.State);
			writer.Write((sbyte)(this.TargetPosition.X - base.AnchorPosition.X));
			writer.Write((sbyte)(this.TargetPosition.Y - base.AnchorPosition.Y));
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x005F176C File Offset: 0x005EF96C
		public override void NetReceive(BinaryReader reader, bool full)
		{
			if (full)
			{
				this.npcType = reader.Read7BitEncodedInt();
				base.Size = reader.ReadVector2();
			}
			Vector2 position = this.position;
			this.position = reader.ReadPackedVector2() + base.AnchorPosition.ToWorldCoordinates(8f, 8f);
			this.direction = (reader.ReadBoolean() ? 1 : (-1));
			this.rand.state = reader.ReadUInt32();
			this.WaitTime = reader.ReadInt16();
			this.State = reader.ReadByte();
			this.TargetPosition = new Point16((int)(base.AnchorPosition.X + (short)reader.ReadSByte()), (int)(base.AnchorPosition.Y + (short)reader.ReadSByte()));
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
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x005F1861 File Offset: 0x005EFA61
		public override void Spawn(bool newlyAdded)
		{
			base.Center = base.AnchorPosition.ToWorldCoordinates(8f, 8f);
			this.TargetPosition = base.AnchorPosition;
			this.rand = new LCG32Random((uint)Main.rand.Next());
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x005F189F File Offset: 0x005EFA9F
		public override void Update()
		{
			this.netOffset = this.netOffset.MoveTowards(Vector2.Zero, 2f);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x005F18BC File Offset: 0x005EFABC
		protected void Recall()
		{
			bool flag = Main.netMode != 2;
			if (flag)
			{
				for (int i = 0; i < 10; i++)
				{
					Dust.NewDustDirect(this.position, this.width, this.height, 15, 0f, 0f, 150, default(Color), 1.1f);
				}
			}
			base.Center = base.AnchorPosition.ToWorldCoordinates(8f, 8f) - new Vector2(0f, 16f);
			this.velocity = Vector2.Zero;
			if (flag)
			{
				for (int j = 0; j < 10; j++)
				{
					Dust.NewDustDirect(this.position, this.width, this.height, 15, 0f, 0f, 150, default(Color), 1.1f);
				}
			}
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x005F19A0 File Offset: 0x005EFBA0
		protected virtual void VisualEffects()
		{
			if (this.npcType >= 0 && NPCID.Sets.IsGoldCritter[this.npcType])
			{
				this.position += this.netOffset;
				Color color = Lighting.GetColor((int)base.Center.X / 16, (int)base.Center.Y / 16);
				if (color.R > 20 || color.B > 20 || color.G > 20)
				{
					int num = (int)color.R;
					if ((int)color.G > num)
					{
						num = (int)color.G;
					}
					if ((int)color.B > num)
					{
						num = (int)color.B;
					}
					num /= 30;
					if (Main.rand.Next(300) < num)
					{
						int num2 = Dust.NewDust(this.position, this.width, this.height, 43, 0f, 0f, 254, new Color(255, 255, 0), 0.5f);
						Main.dust[num2].velocity *= 0f;
					}
				}
				this.position -= this.netOffset;
			}
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x005F1AE0 File Offset: 0x005EFCE0
		protected virtual void CopyToDummy()
		{
			LeashedCritter._dummy.type = this.npcType;
			LeashedCritter._dummy.Size = base.Size;
			LeashedCritter._dummy.frame = this.frame;
			LeashedCritter._dummy.frameCounter = this.frameCounter;
			LeashedCritter._dummy.position = base.Center + new Vector2(0f, 8f) - new Vector2(base.Size.X / 2f, base.Size.Y);
			LeashedCritter._dummy.velocity = this.velocity;
			LeashedCritter._dummy.direction = this.direction;
			LeashedCritter._dummy.spriteDirection = this.spriteDirection;
			LeashedCritter._dummy.scale = this.scale;
			LeashedCritter._dummy.rotation = 0f;
			LeashedCritter._dummy.alpha = 0;
			LeashedCritter._dummy.wet = false;
			Array.Clear(LeashedCritter._dummy.ai, 0, LeashedCritter._dummy.ai.Length);
			Array.Clear(LeashedCritter._dummy.localAI, 0, LeashedCritter._dummy.localAI.Length);
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x005F1C14 File Offset: 0x005EFE14
		protected void CopyFromDummy()
		{
			this.frame = LeashedCritter._dummy.frame;
			this.frameCounter = LeashedCritter._dummy.frameCounter;
			this.spriteDirection = LeashedCritter._dummy.spriteDirection;
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x005F1C48 File Offset: 0x005EFE48
		public override void Draw()
		{
			Main.instance.LoadNPC(this.npcType);
			if (this.frame.Width == 0 || this.frame.Height == 0)
			{
				this.frame = new Rectangle(0, 0, TextureAssets.Npc[this.npcType].Width(), TextureAssets.Npc[this.npcType].Height() / Main.npcFrameCount[this.npcType]);
			}
			this.CopyToDummy();
			LeashedCritter._dummy.position += this.netOffset + this.GetDrawOffset();
			Main.instance.DrawNPCDirect(Main.spriteBatch, LeashedCritter._dummy, true, Main.screenPosition);
			Point point = LeashedCritter._dummy.Center.ToTileCoordinates();
			byte liquid = Framing.GetTileSafely(point.X, point.Y).liquid;
			if ((this.isAquatic && liquid < 255) || (!this.isAquatic && liquid > 0))
			{
				this.DrawBubble();
			}
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x005F1D4A File Offset: 0x005EFF4A
		public virtual Vector2 GetDrawOffset()
		{
			return Vector2.Zero;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x005F1D54 File Offset: 0x005EFF54
		protected void DrawBubble()
		{
			Main.instance.LoadGore(413);
			Texture2D value = TextureAssets.Gore[413].Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			Vector2 vector = rectangle.Size() / 2f;
			Vector2 vector2 = this.position;
			vector2 += this.netOffset + this.GetDrawOffset() + LeashedCritter._dummy.Size * new Vector2(0.5f, 0.5f);
			Point point = vector2.ToTileCoordinates();
			Main.spriteBatch.Draw(value, vector2 - Main.screenPosition, new Rectangle?(rectangle), Lighting.GetColor(point), 0f, vector, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x005F1E1D File Offset: 0x005F001D
		protected LeashedCritter()
		{
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x005F1E30 File Offset: 0x005F0030
		// Note: this type is marked as 'beforefieldinit'.
		static LeashedCritter()
		{
		}

		// Token: 0x04005847 RID: 22599
		protected static NPC _dummy = new NPC();

		// Token: 0x04005848 RID: 22600
		public int anchorStyle;

		// Token: 0x04005849 RID: 22601
		protected int npcType;

		// Token: 0x0400584A RID: 22602
		protected int spriteDirection;

		// Token: 0x0400584B RID: 22603
		protected Rectangle frame;

		// Token: 0x0400584C RID: 22604
		protected double frameCounter;

		// Token: 0x0400584D RID: 22605
		protected LCG32Random rand;

		// Token: 0x0400584E RID: 22606
		protected short WaitTime;

		// Token: 0x0400584F RID: 22607
		protected byte State;

		// Token: 0x04005850 RID: 22608
		protected Point16 TargetPosition;

		// Token: 0x04005851 RID: 22609
		protected Vector2 netOffset;

		// Token: 0x04005852 RID: 22610
		protected float scale = 1f;

		// Token: 0x04005853 RID: 22611
		protected int strayingRangeInBlocks;

		// Token: 0x04005854 RID: 22612
		protected bool isAquatic;

		// Token: 0x04005855 RID: 22613
		protected static readonly float gravity = 0.3f;

		// Token: 0x04005856 RID: 22614
		protected static readonly float maxFallSpeed = 10f;

		// Token: 0x04005857 RID: 22615
		protected const int RecallDuration = 20;
	}
}
