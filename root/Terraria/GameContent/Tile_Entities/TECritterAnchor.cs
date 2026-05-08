using System;
using Terraria.DataStructures;
using Terraria.GameContent.LeashedEntities;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000411 RID: 1041
	public class TECritterAnchor : TELeashedEntityAnchorWithItem
	{
		// Token: 0x06002FC3 RID: 12227 RVA: 0x005B50F6 File Offset: 0x005B32F6
		public TECritterAnchor()
		{
			this.type = TECritterAnchor._myEntityID;
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x005B5109 File Offset: 0x005B3309
		public override void RegisterTileEntityID(int assignedID)
		{
			this.type = (TECritterAnchor._myEntityID = (byte)assignedID);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x005B511C File Offset: 0x005B331C
		public override bool IsTileValidForEntity(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile.active() && tile.type == 724;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x005B514D File Offset: 0x005B334D
		public override TileEntity GenerateInstance()
		{
			return new TECritterAnchor();
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x005B5154 File Offset: 0x005B3354
		public static void Kill(int x, int y)
		{
			TileEntity.Kill(x, y, (int)TECritterAnchor._myEntityID);
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x005B5162 File Offset: 0x005B3362
		public static int Hook_AfterPlacement(int x, int y, int type, int style, int direction, int alternate)
		{
			return TELeashedEntityAnchorWithItem.PlaceFromPlayerPlacementHook(x, y, (int)TECritterAnchor._myEntityID);
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x005B5170 File Offset: 0x005B3370
		public override bool FitsItem(int itemType)
		{
			return ContentSamples.ItemsByType[itemType].makeNPC > 0;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x005B5185 File Offset: 0x005B3385
		public override LeashedEntity CreateLeashedEntity()
		{
			if (this.itemType <= 0)
			{
				return null;
			}
			LeashedCritter leashedCritter = (LeashedCritter)TECritterAnchor.GetLeashedCritterPrototype(this.itemType).NewInstance();
			leashedCritter.SetDefaults(this.itemType);
			return leashedCritter;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x005B51B4 File Offset: 0x005B33B4
		static TECritterAnchor()
		{
			TECritterAnchor.SetPrototypeCollection(FlyerLeashedCritter.Prototype, new int[] { 444, 653, 661 });
			TECritterAnchor.SetPrototypeCollection(NormalButterflyLeashedCritter.Prototype, new int[] { 356 });
			TECritterAnchor.SetPrototypeCollection(EmpressButterflyLeashedCritter.Prototype, new int[] { 661 });
			TECritterAnchor.SetPrototypeCollection(HellButterflyLeashedCritter.Prototype, new int[] { 653 });
			TECritterAnchor.SetPrototypeCollection(FireflyLeashedCritter.Prototype, new int[] { 355, 358, 654 });
			TECritterAnchor.SetPrototypeCollection(ShimmerFlyLeashedCritter.Prototype, new int[] { 677 });
			TECritterAnchor.SetPrototypeCollection(DragonflyLeashedCritter.Prototype, new int[] { 595, 596, 601, 597, 598, 599, 600 });
			TECritterAnchor.SetPrototypeCollection(CrawlingFlyLeashedCritter.Prototype, new int[] { 604, 605, 669 });
			TECritterAnchor.SetPrototypeCollection(FairyLeashedCritter.Prototype, new int[] { 585, 584, 583 });
			TECritterAnchor.SetPrototypeCollection(CrawlerLeashedCritter.Prototype, new int[] { 357, 448, 484, 485, 486, 487, 606, 616, 617 });
			TECritterAnchor.SetPrototypeCollection(SnailLeashedCritter.Prototype, new int[] { 359, 360, 655 });
			TECritterAnchor.SetPrototypeCollection(RunnerLeashedCritter.Prototype, new int[] { 300, 447, 610 });
			TECritterAnchor.SetPrototypeCollection(BirdLeashedCritter.Prototype, new int[] { 74, 297, 298, 442, 611, 671, 672, 673, 675, 674 });
			TECritterAnchor.SetPrototypeCollection(WaterfowlLeashedCritter.Prototype, new int[] { 362, 364, 602, 608 });
			TECritterAnchor.SetPrototypeCollection(FishLeashedCritter.Prototype, new int[] { 55, 592, 607, 626, 627, 688 });
			TECritterAnchor.SetPrototypeCollection(JumperLeashedCritter.Prototype, new int[] { 377, 446 });
			TECritterAnchor.SetPrototypeCollection(WaterStriderLeashedCritter.Prototype, new int[] { 612, 613 });
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x005B53A8 File Offset: 0x005B35A8
		public static void SetPrototypeCollection(LeashedCritter instance, params int[] targetIds)
		{
			foreach (int num in targetIds)
			{
				TECritterAnchor.CritterPrototypes[num] = instance;
			}
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x005B53D1 File Offset: 0x005B35D1
		public static LeashedCritter GetLeashedCritterPrototype(int itemType)
		{
			return TECritterAnchor.CritterPrototypes[(int)ContentSamples.ItemsByType[itemType].makeNPC];
		}

		// Token: 0x04005696 RID: 22166
		private static byte _myEntityID;

		// Token: 0x04005697 RID: 22167
		public static LeashedCritter[] CritterPrototypes = NPCID.Sets.Factory.CreateCustomSet<LeashedCritter>(WalkerLeashedCritter.Prototype, new object[0]);
	}
}
