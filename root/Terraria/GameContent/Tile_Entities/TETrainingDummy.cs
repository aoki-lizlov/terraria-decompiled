using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200041C RID: 1052
	public class TETrainingDummy : TileEntityType<TETrainingDummy>
	{
		// Token: 0x06003059 RID: 12377 RVA: 0x005B9B2B File Offset: 0x005B7D2B
		public override void RegisterTileEntityID(int assignedID)
		{
			base.RegisterTileEntityID(assignedID);
			TileEntity._UpdateStart += TETrainingDummy.ClearBoxes;
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x005B9B45 File Offset: 0x005B7D45
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TileEntityType<TETrainingDummy>.Place(x, y);
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x005B9B4F File Offset: 0x005B7D4F
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TETrainingDummy.ValidTile(x, y);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x005B9B58 File Offset: 0x005B7D58
		public static void ClearBoxes()
		{
			TETrainingDummy.playerBoxes.Clear();
			TETrainingDummy.playerBoxFilled = false;
			TETrainingDummy.npcSlotsFull = false;
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x005B9B70 File Offset: 0x005B7D70
		public override void Update()
		{
			if (this.npc != -1)
			{
				if (!Main.npc[this.npc].active || Main.npc[this.npc].type != 488 || Main.npc[this.npc].ai[0] != (float)this.Position.X || Main.npc[this.npc].ai[1] != (float)this.Position.Y)
				{
					this.Deactivate();
					return;
				}
			}
			else if (!TETrainingDummy.npcSlotsFull)
			{
				TETrainingDummy.FillPlayerHitboxes();
				Rectangle rectangle = new Rectangle((int)(this.Position.X * 16), (int)(this.Position.Y * 16), 32, 48);
				rectangle.Inflate(1600, 1600);
				bool flag = false;
				foreach (Rectangle rectangle2 in TETrainingDummy.playerBoxes)
				{
					if (rectangle2.Intersects(rectangle))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					this.Activate();
				}
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x005B9C9C File Offset: 0x005B7E9C
		private static void FillPlayerHitboxes()
		{
			if (!TETrainingDummy.playerBoxFilled)
			{
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active)
					{
						TETrainingDummy.playerBoxes.Add(Main.player[i].getRect());
					}
				}
				TETrainingDummy.playerBoxFilled = true;
			}
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x005B9CEC File Offset: 0x005B7EEC
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 378 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 36 == 0;
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x005B9D50 File Offset: 0x005B7F50
		public TETrainingDummy()
		{
			this.npc = -1;
			this.RequiresUpdates = true;
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x005B9D68 File Offset: 0x005B7F68
		public static int Hook_AfterPlacement(int x, int y, int type = 378, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x - 1, y - 2, 2, 3, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x - 1, (float)(y - 2), (float)TileEntityType<TETrainingDummy>.EntityTypeID, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntityType<TETrainingDummy>.Place(x - 1, y - 2);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x005B9DBC File Offset: 0x005B7FBC
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.npc);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x005B9DCB File Offset: 0x005B7FCB
		public override void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
			this.npc = (int)reader.ReadInt16();
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x005B9DDC File Offset: 0x005B7FDC
		private void Activate()
		{
			int num = NPC.NewNPC(new EntitySource_TileEntity(this), (int)(this.Position.X * 16 + 16), (int)(this.Position.Y * 16 + 48), 488, 100, 0f, 0f, 0f, 0f, 255);
			if (num == Main.maxNPCs)
			{
				TETrainingDummy.npcSlotsFull = true;
				return;
			}
			Main.npc[num].ai[0] = (float)this.Position.X;
			Main.npc[num].ai[1] = (float)this.Position.Y;
			Main.npc[num].netUpdate = true;
			this.npc = num;
			if (Main.netMode != 1)
			{
				NetMessage.SendData(86, -1, -1, null, this.ID, (float)this.Position.X, (float)this.Position.Y, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x005B9EC8 File Offset: 0x005B80C8
		public void Deactivate()
		{
			if (this.npc != -1)
			{
				Main.npc[this.npc].active = false;
			}
			this.npc = -1;
			if (Main.netMode != 1)
			{
				NetMessage.SendData(86, -1, -1, null, this.ID, (float)this.Position.X, (float)this.Position.Y, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x005B9F30 File Offset: 0x005B8130
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y npc: ",
				this.npc
			});
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x005B9F8C File Offset: 0x005B818C
		// Note: this type is marked as 'beforefieldinit'.
		static TETrainingDummy()
		{
		}

		// Token: 0x040056D4 RID: 22228
		private static List<Rectangle> playerBoxes = new List<Rectangle>();

		// Token: 0x040056D5 RID: 22229
		private static bool playerBoxFilled;

		// Token: 0x040056D6 RID: 22230
		private static bool npcSlotsFull;

		// Token: 0x040056D7 RID: 22231
		public int npc;

		// Token: 0x040056D8 RID: 22232
		public int activationRetryCooldown;
	}
}
