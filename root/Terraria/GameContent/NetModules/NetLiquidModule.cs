using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002E7 RID: 743
	public class NetLiquidModule : NetModule
	{
		// Token: 0x06002645 RID: 9797 RVA: 0x0055E378 File Offset: 0x0055C578
		public static NetPacket Serialize(HashSet<int> changes)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetLiquidModule>(65530);
			netPacket.Writer.Write((ushort)changes.Count);
			foreach (int num in changes)
			{
				int num2 = (num >> 16) & 65535;
				int num3 = num & 65535;
				netPacket.Writer.Write(num);
				netPacket.Writer.Write(Main.tile[num2, num3].liquid);
				netPacket.Writer.Write(Main.tile[num2, num3].liquidType());
			}
			return netPacket;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x0055E43C File Offset: 0x0055C63C
		public static NetPacket SerializeForPlayer(int playerIndex)
		{
			NetLiquidModule._changesForPlayerCache.Clear();
			foreach (KeyValuePair<Point, NetLiquidModule.ChunkChanges> keyValuePair in NetLiquidModule._changesByChunkCoords)
			{
				if (keyValuePair.Value.BroadcastingCondition(playerIndex))
				{
					NetLiquidModule._changesForPlayerCache.AddRange(keyValuePair.Value.DirtiedPackedTileCoords);
				}
			}
			NetPacket netPacket = NetModule.CreatePacket<NetLiquidModule>(65530);
			netPacket.Writer.Write((ushort)NetLiquidModule._changesForPlayerCache.Count);
			foreach (int num in NetLiquidModule._changesForPlayerCache)
			{
				int num2 = (num >> 16) & 65535;
				int num3 = num & 65535;
				netPacket.Writer.Write(num);
				netPacket.Writer.Write(Main.tile[num2, num3].liquid);
				netPacket.Writer.Write(Main.tile[num2, num3].liquidType());
			}
			return netPacket;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0055E578 File Offset: 0x0055C778
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			int num = (int)reader.ReadUInt16();
			for (int i = 0; i < num; i++)
			{
				int num2 = reader.ReadInt32();
				byte b = reader.ReadByte();
				byte b2 = reader.ReadByte();
				int num3 = (num2 >> 16) & 65535;
				int num4 = num2 & 65535;
				Tile tile = Main.tile[num3, num4];
				if (tile != null)
				{
					tile.liquid = b;
					tile.liquidType((int)b2);
				}
			}
			return true;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x0055E5E5 File Offset: 0x0055C7E5
		public static void CreateAndBroadcastByChunk(HashSet<int> dirtiedPackedTileCoords)
		{
			NetLiquidModule.PrepareChunks(dirtiedPackedTileCoords);
			NetLiquidModule.PrepareAndSendToEachPlayerSeparately();
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x0055E5F4 File Offset: 0x0055C7F4
		private static void PrepareAndSendToEachPlayerSeparately()
		{
			for (int i = 0; i < 256; i++)
			{
				if (Netplay.Clients[i].IsConnected())
				{
					NetManager.Instance.SendToClient(NetLiquidModule.SerializeForPlayer(i), i);
				}
			}
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x0055E630 File Offset: 0x0055C830
		private static void BroadcastEachChunkSeparately()
		{
			foreach (KeyValuePair<Point, NetLiquidModule.ChunkChanges> keyValuePair in NetLiquidModule._changesByChunkCoords)
			{
				NetManager.Instance.Broadcast(NetLiquidModule.Serialize(keyValuePair.Value.DirtiedPackedTileCoords), new NetManager.BroadcastCondition(keyValuePair.Value.BroadcastingCondition), -1);
			}
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x0055E6AC File Offset: 0x0055C8AC
		private static void PrepareChunks(HashSet<int> dirtiedPackedTileCoords)
		{
			foreach (KeyValuePair<Point, NetLiquidModule.ChunkChanges> keyValuePair in NetLiquidModule._changesByChunkCoords)
			{
				keyValuePair.Value.DirtiedPackedTileCoords.Clear();
			}
			NetLiquidModule.DistributeChangesIntoChunks(dirtiedPackedTileCoords);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0055E710 File Offset: 0x0055C910
		private static void BroadcastAllChanges(HashSet<int> dirtiedPackedTileCoords)
		{
			NetManager.Instance.Broadcast(NetLiquidModule.Serialize(dirtiedPackedTileCoords), -1);
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0055E724 File Offset: 0x0055C924
		private static void DistributeChangesIntoChunks(HashSet<int> dirtiedPackedTileCoords)
		{
			foreach (int num in dirtiedPackedTileCoords)
			{
				int num2 = (num >> 16) & 65535;
				int num3 = num & 65535;
				Point point;
				point.X = Netplay.GetSectionX(num2);
				point.Y = Netplay.GetSectionY(num3);
				NetLiquidModule.ChunkChanges chunkChanges;
				if (!NetLiquidModule._changesByChunkCoords.TryGetValue(point, out chunkChanges))
				{
					chunkChanges = new NetLiquidModule.ChunkChanges(point.X, point.Y);
					NetLiquidModule._changesByChunkCoords[point] = chunkChanges;
				}
				chunkChanges.DirtiedPackedTileCoords.Add(num);
			}
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetLiquidModule()
		{
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x0055E7D8 File Offset: 0x0055C9D8
		// Note: this type is marked as 'beforefieldinit'.
		static NetLiquidModule()
		{
		}

		// Token: 0x04005068 RID: 20584
		private static List<int> _changesForPlayerCache = new List<int>();

		// Token: 0x04005069 RID: 20585
		private static Dictionary<Point, NetLiquidModule.ChunkChanges> _changesByChunkCoords = new Dictionary<Point, NetLiquidModule.ChunkChanges>();

		// Token: 0x02000828 RID: 2088
		private class ChunkChanges
		{
			// Token: 0x06004319 RID: 17177 RVA: 0x006C0B30 File Offset: 0x006BED30
			public ChunkChanges(int x, int y)
			{
				this.ChunkX = x;
				this.ChunkY = y;
				this.DirtiedPackedTileCoords = new HashSet<int>();
			}

			// Token: 0x0600431A RID: 17178 RVA: 0x006C0B51 File Offset: 0x006BED51
			public bool BroadcastingCondition(int clientIndex)
			{
				return Netplay.Clients[clientIndex].TileSections[this.ChunkX, this.ChunkY];
			}

			// Token: 0x04007261 RID: 29281
			public HashSet<int> DirtiedPackedTileCoords;

			// Token: 0x04007262 RID: 29282
			public int ChunkX;

			// Token: 0x04007263 RID: 29283
			public int ChunkY;
		}
	}
}
