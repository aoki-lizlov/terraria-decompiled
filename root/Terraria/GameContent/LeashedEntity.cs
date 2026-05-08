using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.LeashedEntities;
using Terraria.Net;

namespace Terraria.GameContent
{
	// Token: 0x02000235 RID: 565
	public class LeashedEntity
	{
		// Token: 0x0600224B RID: 8779 RVA: 0x00537834 File Offset: 0x00535A34
		static LeashedEntity()
		{
			ActiveSections.SectionActivated += delegate(Point sectionCoordinates)
			{
				LeashedEntity.GetSection(sectionCoordinates).Activate();
			};
			RemoteClient.NetSectionActivated += LeashedEntity.SyncEntitiesInSection;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x0053789F File Offset: 0x00535A9F
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x005378A7 File Offset: 0x00535AA7
		public int Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x005378B0 File Offset: 0x00535AB0
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x005378B8 File Offset: 0x00535AB8
		public Point16 AnchorPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<AnchorPosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<AnchorPosition>k__BackingField = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x005378C1 File Offset: 0x00535AC1
		public Point SectionCoordinates
		{
			get
			{
				return new Point(Netplay.GetSectionX((int)this.AnchorPosition.X), Netplay.GetSectionY((int)this.AnchorPosition.Y));
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x005378E8 File Offset: 0x00535AE8
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x00537919 File Offset: 0x00535B19
		public Vector2 Center
		{
			get
			{
				return new Vector2(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2));
			}
			set
			{
				this.position = new Vector2(value.X - (float)(this.width / 2), value.Y - (float)(this.height / 2));
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x00537946 File Offset: 0x00535B46
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x0053795B File Offset: 0x00535B5B
		public Vector2 Size
		{
			get
			{
				return new Vector2((float)this.width, (float)this.height);
			}
			set
			{
				this.width = (int)value.X;
				this.height = (int)value.Y;
			}
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00537978 File Offset: 0x00535B78
		public static void Clear(bool keepActiveSections = false)
		{
			Array.Clear(LeashedEntity.BySection, 0, LeashedEntity.BySection.Length);
			LeashedEntity.ByWhoAmI.Clear();
			LeashedEntity.ByWhoAmI.Capacity = 10000;
			LeashedEntity.ActiveSectionList.Clear();
			LeashedEntity.ActiveSectionList.Capacity = LeashedEntity.BySection.Length;
			if (keepActiveSections)
			{
				for (int i = 0; i < LeashedEntity.BySection.GetLength(0); i++)
				{
					for (int j = 0; j < LeashedEntity.BySection.GetLength(1); j++)
					{
						if (ActiveSections.IsSectionActive(new Point(i, j)))
						{
							LeashedEntity.GetSection(new Point(i, j)).Activate();
						}
					}
				}
			}
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00537A20 File Offset: 0x00535C20
		public static void AddNewEntity(LeashedEntity e, Point16 anchorPos)
		{
			if (e == null)
			{
				return;
			}
			if (Main.netMode == 1)
			{
				return;
			}
			int num = LeashedEntity.ByWhoAmI.IndexOf(null);
			if (num < 0)
			{
				num = LeashedEntity.ByWhoAmI.Count;
				LeashedEntity.ByWhoAmI.Add(null);
			}
			LeashedEntity.AddNewEntity(e, anchorPos, num);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00537A68 File Offset: 0x00535C68
		private static void AddNewEntity(LeashedEntity e, Point16 anchorPos, int slot)
		{
			e.AnchorPosition = anchorPos;
			e.active = true;
			e.whoAmI = slot;
			LeashedEntity.ByWhoAmI[slot] = e;
			LeashedEntity.SectionEntityList section = LeashedEntity.GetSection(e.SectionCoordinates);
			section.Add(e);
			if (Main.netMode != 1 && section.active)
			{
				e.Spawn(true);
			}
			if (Main.netMode == 2)
			{
				LeashedEntity.NetModule.Sync(e, true, -1);
			}
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00537AD0 File Offset: 0x00535CD0
		private static LeashedEntity.SectionEntityList GetSection(Point sectionCoordinates)
		{
			LeashedEntity.SectionEntityList sectionEntityList = LeashedEntity.BySection[sectionCoordinates.X, sectionCoordinates.Y];
			if (sectionEntityList == null)
			{
				sectionEntityList = (LeashedEntity.BySection[sectionCoordinates.X, sectionCoordinates.Y] = new LeashedEntity.SectionEntityList(sectionCoordinates));
			}
			return sectionEntityList;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00537B18 File Offset: 0x00535D18
		private static void Remove(LeashedEntity e)
		{
			e.active = false;
			LeashedEntity.ByWhoAmI[e.whoAmI] = null;
			while (LeashedEntity.ByWhoAmI.Count > 0 && LeashedEntity.ByWhoAmI[LeashedEntity.ByWhoAmI.Count - 1] == null)
			{
				LeashedEntity.ByWhoAmI.RemoveAt(LeashedEntity.ByWhoAmI.Count - 1);
			}
			LeashedEntity.GetSection(e.SectionCoordinates).Remove(e);
			if (Main.netMode == 2)
			{
				LeashedEntity.NetModule.Remove(e.whoAmI);
			}
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x00537B9E File Offset: 0x00535D9E
		public static bool TryGet(int slot, out LeashedEntity entity)
		{
			entity = null;
			if (slot < 0 || slot >= LeashedEntity.ByWhoAmI.Count)
			{
				return false;
			}
			entity = LeashedEntity.ByWhoAmI[slot];
			return entity != null;
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00537BC8 File Offset: 0x00535DC8
		public static void UpdateEntities()
		{
			LeashedEntity.RecheckActiveSections();
			LeashedEntity._UpdateEntities();
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x00537BD4 File Offset: 0x00535DD4
		private static void RecheckActiveSections()
		{
			int num = 0;
			for (int i = 0; i < LeashedEntity.ActiveSectionList.Count; i++)
			{
				LeashedEntity.SectionEntityList sectionEntityList = LeashedEntity.ActiveSectionList[i];
				sectionEntityList.CompactIfNecesary();
				if (!ActiveSections.IsSectionActive(sectionEntityList.coordinates))
				{
					sectionEntityList.Deactivate();
				}
				else
				{
					LeashedEntity.ActiveSectionList[num++] = sectionEntityList;
				}
			}
			LeashedEntity.ActiveSectionList.RemoveRange(num, LeashedEntity.ActiveSectionList.Count - num);
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x00537C48 File Offset: 0x00535E48
		private static void _UpdateEntities()
		{
			foreach (LeashedEntity.SectionEntityList sectionEntityList in LeashedEntity.ActiveSectionList)
			{
				LeashedEntity[] list = sectionEntityList.list;
				int count = sectionEntityList.count;
				for (int i = 0; i < count; i++)
				{
					LeashedEntity leashedEntity = list[i];
					if (leashedEntity != null)
					{
						if (leashedEntity.active)
						{
							leashedEntity.Update();
							leashedEntity.StreamNetUpdates();
						}
						if (!leashedEntity.active)
						{
							LeashedEntity.Remove(leashedEntity);
						}
					}
				}
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00537CDC File Offset: 0x00535EDC
		private void StreamNetUpdates()
		{
			if (Main.netMode != 2)
			{
				return;
			}
			if ((((ulong)Main.GameUpdateCount + (ulong)((long)this.whoAmI)) & 1023UL) == 0UL)
			{
				LeashedEntity.NetModule.Sync(this, false, -1);
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00537D06 File Offset: 0x00535F06
		private static void SyncEntitiesInSection(int toClient, Point sectionCoordinates)
		{
			LeashedEntity.GetSection(sectionCoordinates).Sync(toClient);
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00537D14 File Offset: 0x00535F14
		public static void DrawEntities()
		{
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
			rectangle.Inflate(512, 512);
			foreach (LeashedEntity.SectionEntityList sectionEntityList in LeashedEntity.ActiveSectionList)
			{
				LeashedEntity[] list = sectionEntityList.list;
				int count = sectionEntityList.count;
				for (int i = 0; i < count; i++)
				{
					LeashedEntity leashedEntity = list[i];
					if (leashedEntity != null && rectangle.Contains(leashedEntity.Center.ToPoint()))
					{
						leashedEntity.Draw();
					}
				}
			}
			TimeLogger.LeashedEntities.AddTime(startTimestamp);
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x00537DEC File Offset: 0x00535FEC
		public virtual LeashedEntity NewInstance()
		{
			LeashedEntity leashedEntity = (LeashedEntity)Activator.CreateInstance(base.GetType(), true);
			leashedEntity.Type = this.Type;
			return leashedEntity;
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void Spawn(bool newlyAdded)
		{
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void Despawn()
		{
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void Update()
		{
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void Draw()
		{
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void NetSend(BinaryWriter writer, bool full)
		{
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void NetReceive(BinaryReader reader, bool full)
		{
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x00537E0C File Offset: 0x0053600C
		public bool NearbySectionsMissing(int fluff = 3)
		{
			if (Main.netMode != 1)
			{
				return false;
			}
			Point point = this.position.ToTileCoordinates().ClampedInWorld(fluff);
			return Main.tile[point.X - fluff, point.Y] == null || Main.tile[point.X + fluff, point.Y] == null || Main.tile[point.X, point.Y - fluff] == null || Main.tile[point.X, point.Y + fluff] == null;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x0000357B File Offset: 0x0000177B
		public LeashedEntity()
		{
		}

		// Token: 0x04004CD7 RID: 19671
		private static readonly LeashedEntity.SectionEntityList[,] BySection = new LeashedEntity.SectionEntityList[Main.maxTilesX / 200 + 1, Main.maxTilesY / 150 + 1];

		// Token: 0x04004CD8 RID: 19672
		private static readonly List<LeashedEntity.SectionEntityList> ActiveSectionList = new List<LeashedEntity.SectionEntityList>();

		// Token: 0x04004CD9 RID: 19673
		private static readonly List<LeashedEntity> ByWhoAmI = new List<LeashedEntity>();

		// Token: 0x04004CDA RID: 19674
		private int sectionSlot;

		// Token: 0x04004CDB RID: 19675
		public bool active;

		// Token: 0x04004CDC RID: 19676
		public int whoAmI;

		// Token: 0x04004CDD RID: 19677
		[CompilerGenerated]
		private int <Type>k__BackingField;

		// Token: 0x04004CDE RID: 19678
		[CompilerGenerated]
		private Point16 <AnchorPosition>k__BackingField;

		// Token: 0x04004CDF RID: 19679
		public Vector2 position;

		// Token: 0x04004CE0 RID: 19680
		public Vector2 velocity;

		// Token: 0x04004CE1 RID: 19681
		public int direction;

		// Token: 0x04004CE2 RID: 19682
		public int width;

		// Token: 0x04004CE3 RID: 19683
		public int height;

		// Token: 0x04004CE4 RID: 19684
		private const int StreamingRate = 1024;

		// Token: 0x020007BE RID: 1982
		public class NetModule : Terraria.Net.NetModule
		{
			// Token: 0x060041F3 RID: 16883 RVA: 0x006BD264 File Offset: 0x006BB464
			public override bool Deserialize(BinaryReader reader, int userId)
			{
				LeashedEntity.NetModule.MessageType messageType = (LeashedEntity.NetModule.MessageType)reader.ReadByte();
				int num = reader.Read7BitEncodedInt();
				switch (messageType)
				{
				case LeashedEntity.NetModule.MessageType.Remove:
					this.HandleRemove(num);
					break;
				case LeashedEntity.NetModule.MessageType.FullSync:
					LeashedEntity.NetModule.HandleFullSync(num, reader.Read7BitEncodedInt(), new Point16(reader.ReadInt16(), reader.ReadInt16()), reader);
					break;
				case LeashedEntity.NetModule.MessageType.PartialSync:
					LeashedEntity.NetModule.HandlePartialSync(num, reader.Read7BitEncodedInt(), reader);
					break;
				default:
					return false;
				}
				return true;
			}

			// Token: 0x060041F4 RID: 16884 RVA: 0x006BD2D0 File Offset: 0x006BB4D0
			public static void Remove(int slot)
			{
				NetPacket netPacket = Terraria.Net.NetModule.CreatePacket<LeashedEntity.NetModule>(65530);
				netPacket.Writer.Write(0);
				netPacket.Writer.Write7BitEncodedInt(slot);
				NetManager.Instance.Broadcast(netPacket, -1);
			}

			// Token: 0x060041F5 RID: 16885 RVA: 0x006BD310 File Offset: 0x006BB510
			public static void Sync(LeashedEntity entity, bool full, int toClient = -1)
			{
				NetPacket netPacket = Terraria.Net.NetModule.CreatePacket<LeashedEntity.NetModule>(65530);
				netPacket.Writer.Write(full ? 1 : 2);
				netPacket.Writer.Write7BitEncodedInt(entity.whoAmI);
				netPacket.Writer.Write7BitEncodedInt(entity.Type);
				if (full)
				{
					netPacket.Writer.Write(entity.AnchorPosition.X);
					netPacket.Writer.Write(entity.AnchorPosition.Y);
				}
				entity.NetSend(netPacket.Writer, full);
				if (toClient >= 0)
				{
					NetManager.Instance.SendToClient(netPacket, toClient);
					return;
				}
				NetManager.Instance.Broadcast(netPacket, (int i) => Netplay.Clients[i].IsSectionActive(entity.SectionCoordinates), -1);
			}

			// Token: 0x060041F6 RID: 16886 RVA: 0x006BD3F0 File Offset: 0x006BB5F0
			private void HandleRemove(int slot)
			{
				LeashedEntity leashedEntity;
				if (LeashedEntity.TryGet(slot, out leashedEntity))
				{
					LeashedEntity.Remove(leashedEntity);
				}
			}

			// Token: 0x060041F7 RID: 16887 RVA: 0x006BD410 File Offset: 0x006BB610
			private static void HandleFullSync(int slot, int type, Point16 anchorPos, BinaryReader reader)
			{
				while (slot >= LeashedEntity.ByWhoAmI.Count)
				{
					LeashedEntity.ByWhoAmI.Add(null);
				}
				LeashedEntity leashedEntity = LeashedEntity.ByWhoAmI[slot];
				if (leashedEntity == null)
				{
					leashedEntity = LeashedEntity.Registry.Get(type).NewInstance();
					LeashedEntity.AddNewEntity(leashedEntity, anchorPos, slot);
				}
				else if (leashedEntity.Type != type || leashedEntity.AnchorPosition != anchorPos)
				{
					throw new Exception(string.Concat(new object[] { "LeashedEntity type mismatch for full sync. Slot: ", slot, " Existing: ", leashedEntity.Type, " @ ", leashedEntity.AnchorPosition, " New: ", type, " @ ", anchorPos }));
				}
				leashedEntity.NetReceive(reader, true);
			}

			// Token: 0x060041F8 RID: 16888 RVA: 0x006BD4F0 File Offset: 0x006BB6F0
			private static void HandlePartialSync(int slot, int type, BinaryReader reader)
			{
				LeashedEntity leashedEntity = LeashedEntity.ByWhoAmI[slot];
				if (leashedEntity.Type != type)
				{
					throw new Exception(string.Concat(new object[] { "LeashedEntity type mismatch for full sync. Slot: ", slot, " Existing: ", leashedEntity.Type, " Synced: ", type }));
				}
				leashedEntity.NetReceive(reader, false);
			}

			// Token: 0x060041F9 RID: 16889 RVA: 0x0055DC93 File Offset: 0x0055BE93
			public NetModule()
			{
			}

			// Token: 0x02000AB6 RID: 2742
			private enum MessageType
			{
				// Token: 0x04007873 RID: 30835
				Remove,
				// Token: 0x04007874 RID: 30836
				FullSync,
				// Token: 0x04007875 RID: 30837
				PartialSync
			}

			// Token: 0x02000AB7 RID: 2743
			[CompilerGenerated]
			private sealed class <>c__DisplayClass3_0
			{
				// Token: 0x06004C22 RID: 19490 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass3_0()
				{
				}

				// Token: 0x06004C23 RID: 19491 RVA: 0x006DA80E File Offset: 0x006D8A0E
				internal bool <Sync>b__0(int i)
				{
					return Netplay.Clients[i].IsSectionActive(this.entity.SectionCoordinates);
				}

				// Token: 0x04007876 RID: 30838
				public LeashedEntity entity;
			}
		}

		// Token: 0x020007BF RID: 1983
		public class Registry
		{
			// Token: 0x060041FA RID: 16890 RVA: 0x006BD564 File Offset: 0x006BB764
			public static void RegisterAll()
			{
				LeashedEntity.Registry.Prototypes.Add(null);
				LeashedKite.Prototype = LeashedEntity.Registry.Register<LeashedKite>();
				LeashedEntity.Registry.Register(WalkerLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(CrawlerLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(SnailLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(RunnerLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(FlyerLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(NormalButterflyLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(EmpressButterflyLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(HellButterflyLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(FireflyLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(ShimmerFlyLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(DragonflyLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(CrawlingFlyLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(BirdLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(WaterfowlLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(FishLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(FairyLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(JumperLeashedCritter.Prototype);
				LeashedEntity.Registry.Register(WaterStriderLeashedCritter.Prototype);
			}

			// Token: 0x060041FB RID: 16891 RVA: 0x006BD63A File Offset: 0x006BB83A
			public static void Register(LeashedEntity prototype)
			{
				prototype.Type = LeashedEntity.Registry.Prototypes.Count;
				LeashedEntity.Registry.Prototypes.Add(prototype);
			}

			// Token: 0x060041FC RID: 16892 RVA: 0x006BD658 File Offset: 0x006BB858
			public static T Register<T>() where T : LeashedEntity, new()
			{
				T t = new T();
				t.Type = LeashedEntity.Registry.Prototypes.Count;
				T t2 = t;
				LeashedEntity.Registry.Prototypes.Add(t2);
				return t2;
			}

			// Token: 0x060041FD RID: 16893 RVA: 0x006BD691 File Offset: 0x006BB891
			public static LeashedEntity Get(int type)
			{
				return LeashedEntity.Registry.Prototypes[type];
			}

			// Token: 0x060041FE RID: 16894 RVA: 0x0000357B File Offset: 0x0000177B
			public Registry()
			{
			}

			// Token: 0x060041FF RID: 16895 RVA: 0x006BD69E File Offset: 0x006BB89E
			// Note: this type is marked as 'beforefieldinit'.
			static Registry()
			{
			}

			// Token: 0x040070D7 RID: 28887
			private static readonly List<LeashedEntity> Prototypes = new List<LeashedEntity>();
		}

		// Token: 0x020007C0 RID: 1984
		private class SectionEntityList
		{
			// Token: 0x06004200 RID: 16896 RVA: 0x006BD6AA File Offset: 0x006BB8AA
			public SectionEntityList(Point coordinates)
			{
				this.coordinates = coordinates;
			}

			// Token: 0x06004201 RID: 16897 RVA: 0x006BD6C8 File Offset: 0x006BB8C8
			public void Add(LeashedEntity e)
			{
				if (this.count == this.list.Length)
				{
					Array.Resize<LeashedEntity>(ref this.list, this.list.Length * 2);
				}
				e.sectionSlot = this.count;
				LeashedEntity[] array = this.list;
				int num = this.count;
				this.count = num + 1;
				array[num] = e;
			}

			// Token: 0x06004202 RID: 16898 RVA: 0x006BD71F File Offset: 0x006BB91F
			public void Remove(LeashedEntity e)
			{
				this.list[e.sectionSlot] = null;
				this.emptySlots++;
			}

			// Token: 0x06004203 RID: 16899 RVA: 0x006BD740 File Offset: 0x006BB940
			public void CompactIfNecesary()
			{
				if (this.emptySlots < this.count / 2)
				{
					return;
				}
				int num = 0;
				for (int i = 0; i < this.count; i++)
				{
					LeashedEntity leashedEntity = this.list[i];
					if (leashedEntity != null)
					{
						leashedEntity.sectionSlot = num;
						this.list[num++] = leashedEntity;
					}
				}
				Array.Clear(this.list, num, this.count - num);
				this.count = num;
				this.emptySlots = 0;
			}

			// Token: 0x06004204 RID: 16900 RVA: 0x006BD7B4 File Offset: 0x006BB9B4
			public void Activate()
			{
				this.active = true;
				if (Main.netMode != 1)
				{
					foreach (LeashedEntity leashedEntity in this.list)
					{
						if (leashedEntity != null)
						{
							leashedEntity.Spawn(false);
						}
					}
				}
				LeashedEntity.ActiveSectionList.Add(this);
			}

			// Token: 0x06004205 RID: 16901 RVA: 0x006BD800 File Offset: 0x006BBA00
			public void Deactivate()
			{
				this.active = false;
				if (Main.netMode != 1)
				{
					foreach (LeashedEntity leashedEntity in this.list)
					{
						if (leashedEntity != null)
						{
							leashedEntity.Despawn();
						}
					}
				}
			}

			// Token: 0x06004206 RID: 16902 RVA: 0x006BD840 File Offset: 0x006BBA40
			public void Sync(int toClient)
			{
				foreach (LeashedEntity leashedEntity in this.list)
				{
					if (leashedEntity != null)
					{
						LeashedEntity.NetModule.Sync(leashedEntity, true, toClient);
					}
				}
			}

			// Token: 0x040070D8 RID: 28888
			public readonly Point coordinates;

			// Token: 0x040070D9 RID: 28889
			public bool active;

			// Token: 0x040070DA RID: 28890
			public LeashedEntity[] list = new LeashedEntity[32];

			// Token: 0x040070DB RID: 28891
			public int count;

			// Token: 0x040070DC RID: 28892
			private int emptySlots;
		}

		// Token: 0x020007C1 RID: 1985
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004207 RID: 16903 RVA: 0x006BD871 File Offset: 0x006BBA71
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004208 RID: 16904 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004209 RID: 16905 RVA: 0x006BD87D File Offset: 0x006BBA7D
			internal void <.cctor>b__3_0(Point sectionCoordinates)
			{
				LeashedEntity.GetSection(sectionCoordinates).Activate();
			}

			// Token: 0x040070DD RID: 28893
			public static readonly LeashedEntity.<>c <>9 = new LeashedEntity.<>c();
		}
	}
}
