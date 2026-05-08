using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200041A RID: 1050
	public class TELogicSensor : TileEntityType<TELogicSensor>
	{
		// Token: 0x06003037 RID: 12343 RVA: 0x005B8CBB File Offset: 0x005B6EBB
		public override void RegisterTileEntityID(int assignedID)
		{
			base.RegisterTileEntityID(assignedID);
			TileEntity._UpdateStart += TELogicSensor.UpdateStartInternal;
			TileEntity._UpdateEnd += TELogicSensor.UpdateEndInternal;
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x005B8CE6 File Offset: 0x005B6EE6
		public override void OnPlaced()
		{
			this.FigureCheckState();
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x005B8CEE File Offset: 0x005B6EEE
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TELogicSensor.ValidTile(x, y);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x005B8CF7 File Offset: 0x005B6EF7
		private static void UpdateStartInternal()
		{
			TELogicSensor.inUpdateLoop = true;
			TELogicSensor.markedIDsForRemoval.Clear();
			TELogicSensor.playerBox.Clear();
			TELogicSensor.playerBoxFilled = false;
			TELogicSensor.FillPlayerHitboxes();
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x005B8D20 File Offset: 0x005B6F20
		private static void FillPlayerHitboxes()
		{
			if (!TELogicSensor.playerBoxFilled)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active && !player.dead && !player.ghost)
					{
						TELogicSensor.playerBox[i] = player.getRect();
					}
				}
				TELogicSensor.playerBoxFilled = true;
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x005B8D7C File Offset: 0x005B6F7C
		private static void UpdateEndInternal()
		{
			TELogicSensor.inUpdateLoop = false;
			foreach (Tuple<Point16, bool> tuple in TELogicSensor.tripPoints)
			{
				Wiring.blockPlayerTeleportationForOneIteration = tuple.Item2;
				Wiring.HitSwitch((int)tuple.Item1.X, (int)tuple.Item1.Y);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(59, -1, -1, null, (int)tuple.Item1.X, (float)tuple.Item1.Y, 0f, 0f, 0, 0, 0);
				}
			}
			Wiring.blockPlayerTeleportationForOneIteration = false;
			TELogicSensor.tripPoints.Clear();
			using (List<int>.Enumerator enumerator2 = TELogicSensor.markedIDsForRemoval.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					TELogicSensor telogicSensor;
					if (TileEntity.TryGet<TELogicSensor>(enumerator2.Current, out telogicSensor))
					{
						TileEntity.Remove(telogicSensor, false);
					}
				}
			}
			TELogicSensor.markedIDsForRemoval.Clear();
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x005B8E8C File Offset: 0x005B708C
		public override void Update()
		{
			bool state = TELogicSensor.GetState((int)this.Position.X, (int)this.Position.Y, this.logicCheck, this);
			TELogicSensor.LogicCheckType logicCheckType = this.logicCheck;
			if (logicCheckType - TELogicSensor.LogicCheckType.Day > 1)
			{
				if (logicCheckType - TELogicSensor.LogicCheckType.PlayerAbove > 4)
				{
					return;
				}
				if (this.On != state)
				{
					this.ChangeState(state, true);
				}
			}
			else
			{
				if (!this.On && state)
				{
					this.ChangeState(true, true);
				}
				if (this.On && !state)
				{
					this.ChangeState(false, false);
					return;
				}
			}
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x005B8F0C File Offset: 0x005B710C
		public void ChangeState(bool onState, bool TripWire)
		{
			if (onState != this.On && !TELogicSensor.SanityCheck((int)this.Position.X, (int)this.Position.Y))
			{
				return;
			}
			Main.tile[(int)this.Position.X, (int)this.Position.Y].frameX = (onState ? 18 : 0);
			this.On = onState;
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, (int)this.Position.X, (int)this.Position.Y, TileChangeType.None);
			}
			if (TripWire && Main.netMode != 1)
			{
				TELogicSensor.tripPoints.Add(Tuple.Create<Point16, bool>(this.Position, this.logicCheck == TELogicSensor.LogicCheckType.PlayerAbove));
			}
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x005B8FC4 File Offset: 0x005B71C4
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 423 && Main.tile[x, y].frameY % 18 == 0 && Main.tile[x, y].frameX % 18 == 0;
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x005B902B File Offset: 0x005B722B
		public TELogicSensor()
		{
			this.logicCheck = TELogicSensor.LogicCheckType.None;
			this.On = false;
			this.RequiresUpdates = true;
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x005B9048 File Offset: 0x005B7248
		public static TELogicSensor.LogicCheckType FigureCheckType(int x, int y, out bool on)
		{
			on = false;
			if (!WorldGen.InWorld(x, y, 0))
			{
				return TELogicSensor.LogicCheckType.None;
			}
			Tile tile = Main.tile[x, y];
			if (tile == null)
			{
				return TELogicSensor.LogicCheckType.None;
			}
			TELogicSensor.LogicCheckType logicCheckType = TELogicSensor.LogicCheckType.None;
			switch (tile.frameY / 18)
			{
			case 0:
				logicCheckType = TELogicSensor.LogicCheckType.Day;
				break;
			case 1:
				logicCheckType = TELogicSensor.LogicCheckType.Night;
				break;
			case 2:
				logicCheckType = TELogicSensor.LogicCheckType.PlayerAbove;
				break;
			case 3:
				logicCheckType = TELogicSensor.LogicCheckType.Water;
				break;
			case 4:
				logicCheckType = TELogicSensor.LogicCheckType.Lava;
				break;
			case 5:
				logicCheckType = TELogicSensor.LogicCheckType.Honey;
				break;
			case 6:
				logicCheckType = TELogicSensor.LogicCheckType.Liquid;
				break;
			}
			on = TELogicSensor.GetState(x, y, logicCheckType, null);
			return logicCheckType;
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x005B90CC File Offset: 0x005B72CC
		public static bool GetState(int x, int y, TELogicSensor.LogicCheckType type, TELogicSensor instance = null)
		{
			switch (type)
			{
			case TELogicSensor.LogicCheckType.Day:
				return Main.dayTime;
			case TELogicSensor.LogicCheckType.Night:
				return !Main.dayTime;
			case TELogicSensor.LogicCheckType.PlayerAbove:
			{
				bool flag = false;
				Rectangle rectangle = new Rectangle(x * 16 - 32 - 1, y * 16 - 160 - 1, 82, 162);
				foreach (KeyValuePair<int, Rectangle> keyValuePair in TELogicSensor.playerBox)
				{
					if (keyValuePair.Value.Intersects(rectangle))
					{
						flag = true;
						break;
					}
				}
				return flag;
			}
			case TELogicSensor.LogicCheckType.Water:
			case TELogicSensor.LogicCheckType.Lava:
			case TELogicSensor.LogicCheckType.Honey:
			case TELogicSensor.LogicCheckType.Liquid:
			{
				if (instance == null)
				{
					return false;
				}
				Tile tile = Main.tile[x, y];
				bool flag2 = true;
				if (tile == null || tile.liquid == 0)
				{
					flag2 = false;
				}
				if (!tile.lava() && type == TELogicSensor.LogicCheckType.Lava)
				{
					flag2 = false;
				}
				if (!tile.honey() && type == TELogicSensor.LogicCheckType.Honey)
				{
					flag2 = false;
				}
				if ((tile.honey() || tile.lava() || tile.shimmer()) && type == TELogicSensor.LogicCheckType.Water)
				{
					flag2 = false;
				}
				if (!flag2 && instance.On)
				{
					if (instance.CountedData == 0)
					{
						instance.CountedData = 15;
					}
					else if (instance.CountedData > 0)
					{
						instance.CountedData--;
					}
					flag2 = instance.CountedData > 0;
				}
				return flag2;
			}
			default:
				return false;
			}
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x005B923C File Offset: 0x005B743C
		public void FigureCheckState()
		{
			this.logicCheck = TELogicSensor.FigureCheckType((int)this.Position.X, (int)this.Position.Y, out this.On);
			TELogicSensor.GetFrame((int)this.Position.X, (int)this.Position.Y, this.logicCheck, this.On);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x005B9298 File Offset: 0x005B7498
		public static void GetFrame(int x, int y, TELogicSensor.LogicCheckType type, bool on)
		{
			Main.tile[x, y].frameX = (on ? 18 : 0);
			switch (type)
			{
			case TELogicSensor.LogicCheckType.Day:
				Main.tile[x, y].frameY = 0;
				return;
			case TELogicSensor.LogicCheckType.Night:
				Main.tile[x, y].frameY = 18;
				return;
			case TELogicSensor.LogicCheckType.PlayerAbove:
				Main.tile[x, y].frameY = 36;
				return;
			case TELogicSensor.LogicCheckType.Water:
				Main.tile[x, y].frameY = 54;
				return;
			case TELogicSensor.LogicCheckType.Lava:
				Main.tile[x, y].frameY = 72;
				return;
			case TELogicSensor.LogicCheckType.Honey:
				Main.tile[x, y].frameY = 90;
				return;
			case TELogicSensor.LogicCheckType.Liquid:
				Main.tile[x, y].frameY = 108;
				return;
			default:
				Main.tile[x, y].frameY = 0;
				return;
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x005B9385 File Offset: 0x005B7585
		public static bool SanityCheck(int x, int y)
		{
			if (!Main.tile[x, y].active() || Main.tile[x, y].type != 423)
			{
				TELogicSensor.Kill(x, y);
				return false;
			}
			return true;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x005B93BC File Offset: 0x005B75BC
		public static int Hook_AfterPlacement(int x, int y, int type = 423, int style = 0, int direction = 1, int alternate = 0)
		{
			bool flag;
			TELogicSensor.LogicCheckType logicCheckType = TELogicSensor.FigureCheckType(x, y, out flag);
			TELogicSensor.GetFrame(x, y, logicCheckType, flag);
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)TileEntityType<TELogicSensor>.EntityTypeID, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntityType<TELogicSensor>.Place(x, y);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x005B9418 File Offset: 0x005B7618
		public new static void Kill(int x, int y)
		{
			TELogicSensor telogicSensor;
			if (TileEntity.TryGetAt<TELogicSensor>(x, y, out telogicSensor))
			{
				Wiring.blockPlayerTeleportationForOneIteration = telogicSensor.logicCheck == TELogicSensor.LogicCheckType.PlayerAbove;
				bool flag = false;
				if (telogicSensor.logicCheck == TELogicSensor.LogicCheckType.PlayerAbove && telogicSensor.On)
				{
					flag = true;
				}
				else if (telogicSensor.logicCheck == TELogicSensor.LogicCheckType.Water && telogicSensor.On)
				{
					flag = true;
				}
				else if (telogicSensor.logicCheck == TELogicSensor.LogicCheckType.Lava && telogicSensor.On)
				{
					flag = true;
				}
				else if (telogicSensor.logicCheck == TELogicSensor.LogicCheckType.Honey && telogicSensor.On)
				{
					flag = true;
				}
				else if (telogicSensor.logicCheck == TELogicSensor.LogicCheckType.Liquid && telogicSensor.On)
				{
					flag = true;
				}
				if (flag)
				{
					Wiring.HitSwitch((int)telogicSensor.Position.X, (int)telogicSensor.Position.Y);
					NetMessage.SendData(59, -1, -1, null, (int)telogicSensor.Position.X, (float)telogicSensor.Position.Y, 0f, 0f, 0, 0, 0);
				}
				Wiring.blockPlayerTeleportationForOneIteration = false;
				if (TELogicSensor.inUpdateLoop)
				{
					TELogicSensor.markedIDsForRemoval.Add(telogicSensor.ID);
					return;
				}
				TileEntity.Remove(telogicSensor, false);
			}
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x005B951B File Offset: 0x005B771B
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			if (!networkSend)
			{
				writer.Write((byte)this.logicCheck);
				writer.Write(this.On);
			}
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x005B9539 File Offset: 0x005B7739
		public override void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
			if (!networkSend)
			{
				this.logicCheck = (TELogicSensor.LogicCheckType)reader.ReadByte();
				this.On = reader.ReadBoolean();
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x005B9558 File Offset: 0x005B7758
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y ",
				this.logicCheck
			});
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x005B95B4 File Offset: 0x005B77B4
		// Note: this type is marked as 'beforefieldinit'.
		static TELogicSensor()
		{
		}

		// Token: 0x040056CB RID: 22219
		private static Dictionary<int, Rectangle> playerBox = new Dictionary<int, Rectangle>();

		// Token: 0x040056CC RID: 22220
		private static List<Tuple<Point16, bool>> tripPoints = new List<Tuple<Point16, bool>>();

		// Token: 0x040056CD RID: 22221
		private static List<int> markedIDsForRemoval = new List<int>();

		// Token: 0x040056CE RID: 22222
		private static bool inUpdateLoop;

		// Token: 0x040056CF RID: 22223
		private static bool playerBoxFilled;

		// Token: 0x040056D0 RID: 22224
		public TELogicSensor.LogicCheckType logicCheck;

		// Token: 0x040056D1 RID: 22225
		public bool On;

		// Token: 0x040056D2 RID: 22226
		public int CountedData;

		// Token: 0x0200093A RID: 2362
		public enum LogicCheckType
		{
			// Token: 0x0400752A RID: 29994
			None,
			// Token: 0x0400752B RID: 29995
			Day,
			// Token: 0x0400752C RID: 29996
			Night,
			// Token: 0x0400752D RID: 29997
			PlayerAbove,
			// Token: 0x0400752E RID: 29998
			Water,
			// Token: 0x0400752F RID: 29999
			Lava,
			// Token: 0x04007530 RID: 30000
			Honey,
			// Token: 0x04007531 RID: 30001
			Liquid
		}
	}
}
