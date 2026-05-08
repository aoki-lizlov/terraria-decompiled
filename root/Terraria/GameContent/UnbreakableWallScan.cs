using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Net;

namespace Terraria.GameContent
{
	// Token: 0x0200024A RID: 586
	public static class UnbreakableWallScan
	{
		// Token: 0x06002304 RID: 8964 RVA: 0x0053C35A File Offset: 0x0053A55A
		public static void Update(Player player)
		{
			int netMode = Main.netMode;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0053C364 File Offset: 0x0053A564
		public static bool InsideUnbreakableWalls(Point pt)
		{
			int num = 0;
			for (int i = 0; i < UnbreakableWallScan.Directions.Length; i++)
			{
				if (UnbreakableWallScan.LineScan(pt, UnbreakableWallScan.Directions[i]))
				{
					num |= 1 << i;
				}
			}
			for (int j = 0; j < UnbreakableWallScan.Directions.Length; j++)
			{
				if ((num & 31) == 0)
				{
					return false;
				}
				num = ((num << 1) & 255) | (num >> 7);
			}
			return true;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x0053C3CC File Offset: 0x0053A5CC
		public static bool LineScan(Point pt, Point dir)
		{
			int i = 0;
			while (i < UnbreakableWallScan.ScanDistance)
			{
				if (!WorldGen.InWorld(pt, 0))
				{
					return false;
				}
				Tile tile = Main.tile[pt.X, pt.Y];
				if (tile == null)
				{
					return false;
				}
				if (tile.wall == 350)
				{
					return tile.wallColor() >= 16;
				}
				i++;
				pt.X += dir.X;
				pt.Y += dir.Y;
			}
			return false;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x0053C450 File Offset: 0x0053A650
		// Note: this type is marked as 'beforefieldinit'.
		static UnbreakableWallScan()
		{
		}

		// Token: 0x04004D42 RID: 19778
		public static readonly int ScanDistance = 250;

		// Token: 0x04004D43 RID: 19779
		public static readonly Point[] Directions = new Point[]
		{
			new Point(1, 0),
			new Point(1, 1),
			new Point(0, 1),
			new Point(-1, 1),
			new Point(-1, 0),
			new Point(-1, -1),
			new Point(0, -1),
			new Point(1, -1)
		};

		// Token: 0x020007D4 RID: 2004
		public class NetModule : Terraria.Net.NetModule
		{
			// Token: 0x0600423C RID: 16956 RVA: 0x006BE176 File Offset: 0x006BC376
			public override bool Deserialize(BinaryReader reader, int userId)
			{
				if (Main.netMode != 1)
				{
					return false;
				}
				Main.player[(int)reader.ReadByte()].insideUnbreakableWalls = reader.ReadBoolean();
				return true;
			}

			// Token: 0x0600423D RID: 16957 RVA: 0x006BE19C File Offset: 0x006BC39C
			internal static void BroadcastChange(Player player)
			{
				NetPacket netPacket = Terraria.Net.NetModule.CreatePacket<UnbreakableWallScan.NetModule>(65530);
				netPacket.Writer.Write((byte)player.whoAmI);
				netPacket.Writer.Write(player.insideUnbreakableWalls);
				NetManager.Instance.Broadcast(netPacket, -1);
			}

			// Token: 0x0600423E RID: 16958 RVA: 0x0055DC93 File Offset: 0x0055BE93
			public NetModule()
			{
			}
		}
	}
}
