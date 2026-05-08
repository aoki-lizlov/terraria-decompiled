using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x0200027D RID: 637
	public class TownRoomManager
	{
		// Token: 0x0600246D RID: 9325 RVA: 0x0054D689 File Offset: 0x0054B889
		public void AddOccupantsToList(int x, int y, List<int> occupantsList)
		{
			this.AddOccupantsToList(new Point(x, y), occupantsList);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x0054D69C File Offset: 0x0054B89C
		public void AddOccupantsToList(Point tilePosition, List<int> occupants)
		{
			foreach (Tuple<int, Point> tuple in this._roomLocationPairs)
			{
				if (tuple.Item2 == tilePosition)
				{
					occupants.Add(tuple.Item1);
				}
			}
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x0054D704 File Offset: 0x0054B904
		public bool HasRoomQuick(int npcID)
		{
			return this._hasRoom[npcID];
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x0054D710 File Offset: 0x0054B910
		public bool HasRoom(int npcID, out Point roomPosition)
		{
			if (!this._hasRoom[npcID])
			{
				roomPosition = new Point(0, 0);
				return false;
			}
			foreach (Tuple<int, Point> tuple in this._roomLocationPairs)
			{
				if (tuple.Item1 == npcID)
				{
					roomPosition = tuple.Item2;
					return true;
				}
			}
			roomPosition = new Point(0, 0);
			return false;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x0054D7A0 File Offset: 0x0054B9A0
		public void SetRoom(int npcID, int x, int y)
		{
			this._hasRoom[npcID] = true;
			this.SetRoom(npcID, new Point(x, y));
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x0054D7BC File Offset: 0x0054B9BC
		public void SetRoom(int npcID, Point pt)
		{
			object entityCreationLock = TownRoomManager.EntityCreationLock;
			lock (entityCreationLock)
			{
				this._roomLocationPairs.RemoveAll((Tuple<int, Point> x) => x.Item1 == npcID);
				this._roomLocationPairs.Add(Tuple.Create<int, Point>(npcID, pt));
			}
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x0054D834 File Offset: 0x0054BA34
		public void KickOut(NPC n)
		{
			this.KickOut(n.type);
			this._hasRoom[n.type] = false;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x0054D850 File Offset: 0x0054BA50
		public void KickOut(int npcType)
		{
			object entityCreationLock = TownRoomManager.EntityCreationLock;
			lock (entityCreationLock)
			{
				this._roomLocationPairs.RemoveAll((Tuple<int, Point> x) => x.Item1 == npcType);
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x0054D8B0 File Offset: 0x0054BAB0
		public void DisplayRooms()
		{
			foreach (Tuple<int, Point> tuple in this._roomLocationPairs)
			{
				Dust.QuickDust(tuple.Item2, Main.hslToRgb((float)tuple.Item1 * 0.05f % 1f, 1f, 0.5f, byte.MaxValue));
			}
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x0054D930 File Offset: 0x0054BB30
		public void Save(BinaryWriter writer)
		{
			object entityCreationLock = TownRoomManager.EntityCreationLock;
			lock (entityCreationLock)
			{
				writer.Write(this._roomLocationPairs.Count);
				foreach (Tuple<int, Point> tuple in this._roomLocationPairs)
				{
					writer.Write(tuple.Item1);
					writer.Write(tuple.Item2.X);
					writer.Write(tuple.Item2.Y);
				}
			}
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x0054D9E4 File Offset: 0x0054BBE4
		public void Load(BinaryReader reader)
		{
			this.Clear();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int num2 = reader.ReadInt32();
				Point point = new Point(reader.ReadInt32(), reader.ReadInt32());
				this._roomLocationPairs.Add(Tuple.Create<int, Point>(num2, point));
				this._hasRoom[num2] = true;
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x0054DA40 File Offset: 0x0054BC40
		public void Clear()
		{
			this._roomLocationPairs.Clear();
			for (int i = 0; i < this._hasRoom.Length; i++)
			{
				this._hasRoom[i] = false;
			}
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x0054DA74 File Offset: 0x0054BC74
		public byte GetHouseholdStatus(NPC n)
		{
			byte b = 0;
			if (n.homeless)
			{
				b = 1;
			}
			else if (this.HasRoomQuick(n.type))
			{
				b = 2;
			}
			return b;
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x0054DAA0 File Offset: 0x0054BCA0
		public bool CanNPCsLiveWithEachOther(int npc1ByType, NPC npc2)
		{
			NPC npc3;
			return !ContentSamples.NpcsByNetId.TryGetValue(npc1ByType, out npc3) || this.CanNPCsLiveWithEachOther(npc3, npc2);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x0054DAC6 File Offset: 0x0054BCC6
		public bool CanNPCsLiveWithEachOther(NPC npc1, NPC npc2)
		{
			return npc1.housingCategory != npc2.housingCategory;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x0054DAD9 File Offset: 0x0054BCD9
		public bool CanNPCsLiveWithEachOther_ShopHelper(NPC npc1, NPC npc2)
		{
			return this.CanNPCsLiveWithEachOther(npc1, npc2);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x0054DAE3 File Offset: 0x0054BCE3
		public TownRoomManager()
		{
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0054DB06 File Offset: 0x0054BD06
		// Note: this type is marked as 'beforefieldinit'.
		static TownRoomManager()
		{
		}

		// Token: 0x04004E13 RID: 19987
		public static object EntityCreationLock = new object();

		// Token: 0x04004E14 RID: 19988
		private List<Tuple<int, Point>> _roomLocationPairs = new List<Tuple<int, Point>>();

		// Token: 0x04004E15 RID: 19989
		private bool[] _hasRoom = new bool[(int)NPCID.Count];

		// Token: 0x02000803 RID: 2051
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x060042CF RID: 17103 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x060042D0 RID: 17104 RVA: 0x006BFF37 File Offset: 0x006BE137
			internal bool <SetRoom>b__0(Tuple<int, Point> x)
			{
				return x.Item1 == this.npcID;
			}

			// Token: 0x040071BA RID: 29114
			public int npcID;
		}

		// Token: 0x02000804 RID: 2052
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x060042D1 RID: 17105 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x060042D2 RID: 17106 RVA: 0x006BFF47 File Offset: 0x006BE147
			internal bool <KickOut>b__0(Tuple<int, Point> x)
			{
				return x.Item1 == this.npcType;
			}

			// Token: 0x040071BB RID: 29115
			public int npcType;
		}
	}
}
