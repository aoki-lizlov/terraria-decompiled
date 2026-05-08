using System;

namespace Terraria.GameContent.FishDropRules
{
	// Token: 0x0200047C RID: 1148
	public class GameContentFishDropPopulator : AFishDropRulePopulator
	{
		// Token: 0x06003339 RID: 13113 RVA: 0x005F45E5 File Offset: 0x005F27E5
		public GameContentFishDropPopulator(FishDropRuleList list)
			: base(list)
		{
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x005F45F0 File Offset: 0x005F27F0
		public void Populate()
		{
			base.AddStopper(this.AnyEnemies);
			this.LavaDrops();
			this.HoneyDrops();
			this.JunkDrops();
			this.CrateDrops();
			this.RareDrops();
			this.RemixDrops();
			this.DungeonDrops();
			this.CorruptionDrops();
			this.CrimsonDrops();
			this.HallowedDrops();
			this.GlowingMushroomsDrops();
			this.SnowDrops();
			this.JungleDrops();
			this.OceanDrops();
			this.DesertDrops();
			this.FloatingIslandDrops();
			this.SurfaceDrops();
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x005F4670 File Offset: 0x005F2870
		private void RemixDrops()
		{
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2461, new AFishingCondition[0]);
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2458, new AFishingCondition[0]);
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2459, new AFishingCondition[0]);
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2479, new AFishingCondition[0]);
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2456, new AFishingCondition[0]);
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2474, new AFishingCondition[0]);
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2478, new AFishingCondition[0]);
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2450, new AFishingCondition[0]);
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2464, new AFishingCondition[0]);
			base.AddQuestFishForRemix(AFishDropRulePopulator.Rarity.Uncommon, 1, 2469, new AFishingCondition[0]);
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x005F4764 File Offset: 0x005F2964
		private void SurfaceDrops()
		{
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2455, new AFishingCondition[] { this.Height1And2 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2479, new AFishingCondition[] { this.Height1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2456, new AFishingCondition[] { this.Height1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2474, new AFishingCondition[] { this.Height1 });
			base.Add(AFishDropRulePopulator.Rarity.Rare, 10, 2437, new AFishingCondition[] { this.HeightAbove1, this.HardMode });
			base.Add(AFishDropRulePopulator.Rarity.Rare, 9, 2436, new AFishingCondition[] { this.HeightAbove1, this.HardMode });
			base.Add(AFishDropRulePopulator.Rarity.Rare, 5, 2436, new AFishingCondition[] { this.HeightAbove1, this.EarlyMode });
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 3, 2308, new AFishingCondition[] { this.HeightAbove1 });
			base.Add(AFishDropRulePopulator.Rarity.VeryRare, 2, 2320, new AFishingCondition[] { this.HeightAbove1 });
			base.Add(AFishDropRulePopulator.Rarity.Rare, 1, 2321, new AFishingCondition[] { this.HeightAbove1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2478, new AFishingCondition[] { this.HeightAbove1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2450, new AFishingCondition[] { this.HeightAbove1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2464, new AFishingCondition[] { this.HeightAbove1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2469, new AFishingCondition[] { this.HeightAbove1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2462, new AFishingCondition[] { this.HeightAbove2 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2482, new AFishingCondition[] { this.HeightAbove2 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2472, new AFishingCondition[] { this.HeightAbove2 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2460, new AFishingCondition[] { this.HeightAbove2 });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 3, 4, 2303, new AFishingCondition[] { this.HeightAbove1 });
			base.Add(AFishDropRulePopulator.Rarity.UncommonOrCommon, 4, base.Group(new int[] { 2303, 2309, 2309, 2309 }), new AFishingCondition[] { this.HeightAbove1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2487, new AFishingCondition[0]);
			base.Add(AFishDropRulePopulator.Rarity.Common, 1, 2298, new AFishingCondition[] { this.Water1000 });
			base.Add(AFishDropRulePopulator.Rarity.Any, 1, 2290, new AFishingCondition[0]);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x005F4A70 File Offset: 0x005F2C70
		private void FloatingIslandDrops()
		{
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2461, new AFishingCondition[] { this.HeightUnder2 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2453, new AFishingCondition[] { this.Height0 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2473, new AFishingCondition[] { this.Height0 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2476, new AFishingCondition[] { this.Height0 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2458, new AFishingCondition[] { this.HeightUnder2 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2459, new AFishingCondition[] { this.HeightUnder2 });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 2304, new AFishingCondition[] { this.Height0 });
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x005F4B60 File Offset: 0x005F2D60
		private void DesertDrops()
		{
			AFishingCondition desert = this.Desert;
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 3, 5490, new AFishingCondition[] { desert });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 4393, new AFishingCondition[] { desert });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 4394, new AFishingCondition[] { desert });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 4410, new AFishingCondition[] { desert });
			base.Add(AFishDropRulePopulator.Rarity.Any, 3, 4402, new AFishingCondition[] { desert });
			base.Add(AFishDropRulePopulator.Rarity.Any, 1, 4401, new AFishingCondition[] { desert });
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x005F4C18 File Offset: 0x005F2E18
		private void OceanDrops()
		{
			AFishingCondition ocean = this.Ocean;
			base.Add(AFishDropRulePopulator.Rarity.VeryRare, 2, 2341, new AFishingCondition[] { ocean });
			base.Add(AFishDropRulePopulator.Rarity.VeryRare, 1, 2342, new AFishingCondition[] { ocean });
			base.Add(AFishDropRulePopulator.Rarity.Rare, 5, 2438, new AFishingCondition[] { ocean });
			base.Add(AFishDropRulePopulator.Rarity.Rare, 3, 2332, new AFishingCondition[] { ocean });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2480, new AFishingCondition[] { ocean });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2481, new AFishingCondition[] { ocean });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 2316, new AFishingCondition[] { ocean });
			base.Add(AFishDropRulePopulator.Rarity.Common, 2, 2301, new AFishingCondition[] { ocean });
			base.Add(AFishDropRulePopulator.Rarity.Common, 1, 2300, new AFishingCondition[] { ocean });
			base.Add(AFishDropRulePopulator.Rarity.Any, 1, 2297, new AFishingCondition[] { ocean });
			base.AddStopper(ocean);
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x005F4D44 File Offset: 0x005F2F44
		private void JungleDrops()
		{
			AFishingCondition jungle = this.Jungle;
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 3, 5634, new AFishingCondition[] { jungle });
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 5463, new AFishingCondition[] { jungle, this.HardMode });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2452, new AFishingCondition[] { jungle, this.Height1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2483, new AFishingCondition[] { jungle, this.Height1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2488, new AFishingCondition[] { jungle, this.Height1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2486, new AFishingCondition[] { jungle, this.HeightAboveAnd1 });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 2311, new AFishingCondition[] { jungle, this.HeightAbove1 });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 2313, new AFishingCondition[] { jungle });
			base.Add(AFishDropRulePopulator.Rarity.Common, 1, 2302, new AFishingCondition[] { jungle });
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x005F4E84 File Offset: 0x005F3084
		private void SnowDrops()
		{
			AFishingCondition snow = this.Snow;
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2467, new AFishingCondition[] { snow, this.HeightUnder2 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2470, new AFishingCondition[] { snow, this.Height1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2484, new AFishingCondition[] { snow, this.HeightAbove1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2466, new AFishingCondition[] { snow, this.HeightAbove1 });
			base.Add(AFishDropRulePopulator.Rarity.Common, 12, 3197, new AFishingCondition[] { snow });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 6, 3197, new AFishingCondition[] { snow });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 2306, new AFishingCondition[] { snow });
			base.Add(AFishDropRulePopulator.Rarity.Common, 1, 2299, new AFishingCondition[] { snow });
			base.Add(AFishDropRulePopulator.Rarity.Any, 3, 2309, new AFishingCondition[] { snow, this.HeightAbove1 });
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x005F4FB9 File Offset: 0x005F31B9
		private void GlowingMushroomsDrops()
		{
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2475, new AFishingCondition[] { this.GlowingMushrooms });
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x005F4FDC File Offset: 0x005F31DC
		private void HallowedDrops()
		{
			AFishingCondition afishingCondition = this.RolledHallowDesert;
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 1, 5490, new AFishingCondition[] { afishingCondition });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 4393, new AFishingCondition[] { afishingCondition });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 4394, new AFishingCondition[] { afishingCondition });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 4410, new AFishingCondition[] { afishingCondition });
			base.Add(AFishDropRulePopulator.Rarity.Any, 3, 4402, new AFishingCondition[] { afishingCondition });
			base.Add(AFishDropRulePopulator.Rarity.Any, 1, 4401, new AFishingCondition[] { afishingCondition });
			afishingCondition = this.Hallow;
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 3, 2429, new AFishingCondition[] { this.TrueSnow, afishingCondition, this.HardMode, this.Height3 });
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 3209, new AFishingCondition[] { afishingCondition, this.HardMode });
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 3, 5274, new AFishingCondition[] { afishingCondition, this.HardMode });
			base.Add(AFishDropRulePopulator.Rarity.VeryRare, 1, 2317, new AFishingCondition[] { afishingCondition, this.HeightAbove1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2465, new AFishingCondition[] { afishingCondition, this.HeightAbove1 });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2468, new AFishingCondition[] { afishingCondition, this.HeightUnder2 });
			base.Add(AFishDropRulePopulator.Rarity.Rare, 1, 2310, new AFishingCondition[] { afishingCondition });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2471, new AFishingCondition[] { afishingCondition });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 2307, new AFishingCondition[] { afishingCondition });
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x005F51D8 File Offset: 0x005F33D8
		private void CrimsonDrops()
		{
			AFishingCondition crimson = this.Crimson;
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 3, 2429, new AFishingCondition[] { this.TrueSnow, crimson, this.HardMode, this.Height3 });
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 3211, new AFishingCondition[] { crimson, this.HardMode });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2477, new AFishingCondition[] { crimson });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2463, new AFishingCondition[] { crimson });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 2319, new AFishingCondition[] { crimson });
			base.Add(AFishDropRulePopulator.Rarity.Common, 1, 2305, new AFishingCondition[] { crimson });
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x005F52B4 File Offset: 0x005F34B4
		private void CorruptionDrops()
		{
			AFishingCondition corruption = this.Corruption;
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 3, 2429, new AFishingCondition[] { this.TrueSnow, corruption, this.HardMode, this.Height3 });
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 3210, new AFishingCondition[] { corruption, this.HardMode });
			base.Add(AFishDropRulePopulator.Rarity.Rare, 1, 2330, new AFishingCondition[] { corruption });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2454, new AFishingCondition[] { corruption });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2485, new AFishingCondition[] { corruption });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2457, new AFishingCondition[] { corruption });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 1, 2318, new AFishingCondition[] { corruption });
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x005F53AC File Offset: 0x005F35AC
		private void DungeonDrops()
		{
			base.Add(AFishDropRulePopulator.Rarity.VeryRare, 12, 3000, new AFishingCondition[] { this.Dungeon });
			base.Add(AFishDropRulePopulator.Rarity.VeryRare, 12, 2999, new AFishingCondition[] { this.Dungeon });
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x005F53FC File Offset: 0x005F35FC
		private void RareDrops()
		{
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 4382, new AFishingCondition[] { this.BloodMoon, this.DidNotUseCombatBook });
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 2, 5240, new AFishingCondition[] { this.BloodMoon });
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 5, 2423, new AFishingCondition[0]);
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 5, 3225, new AFishingCondition[0]);
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 10, 2420, new AFishingCondition[0]);
			base.Add(AFishDropRulePopulator.Rarity.BombRarityOfNotLegendaryAndNotVeryRareAndUncommon, 5, 3196, new AFishingCondition[0]);
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x005F54B0 File Offset: 0x005F36B0
		private void CrateDrops()
		{
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 3205, 3984, new AFishingCondition[] { this.Crate, this.Dungeon });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 5002, 5003, new AFishingCondition[] { this.Crate, this.Beach });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 3203, 3982, new AFishingCondition[] { this.Crate, this.Corruption });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 3204, 3983, new AFishingCondition[] { this.Crate, this.Crimson });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 3207, 3986, new AFishingCondition[] { this.Crate, this.Hallow });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 3208, 3987, new AFishingCondition[] { this.Crate, this.Jungle });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 4405, 4406, new AFishingCondition[] { this.Crate, this.Snow });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 4407, 4408, new AFishingCondition[] { this.Crate, this.TrueDesert });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 3206, 3985, new AFishingCondition[] { this.Crate, this.Height0 });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 5002, 5003, new AFishingCondition[] { this.Crate, this.Remix, this.Height1, this.UnderRockLayer });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Legendary, 1, 2336, 3981, new AFishingCondition[] { this.Crate });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.VeryRare, 1, 2336, 3981, new AFishingCondition[] { this.Crate });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Rare, 1, 2335, 3980, new AFishingCondition[] { this.Crate });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Uncommon, 1, 2335, 3980, new AFishingCondition[] { this.Crate });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Any, 1, 2334, 3979, new AFishingCondition[] { this.Crate });
			base.AddStopper(this.Crate);
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x005F5760 File Offset: 0x005F3960
		private void JunkDrops()
		{
			base.Add(AFishDropRulePopulator.Rarity.Any, 8, 5275, new AFishingCondition[] { this.Junk });
			base.Add(AFishDropRulePopulator.Rarity.Any, 1, base.Group(new int[] { 2337, 2338, 2339 }), new AFishingCondition[] { this.Junk });
			base.AddStopper(this.Junk);
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x005F57CC File Offset: 0x005F39CC
		private void HoneyDrops()
		{
			base.Add(AFishDropRulePopulator.Rarity.Rare, 1, 2314, new AFishingCondition[] { this.InHoney });
			base.Add(AFishDropRulePopulator.Rarity.Uncommon, 2, 2314, new AFishingCondition[] { this.InHoney });
			base.AddQuestFish(AFishDropRulePopulator.Rarity.Uncommon, 1, 2451, new AFishingCondition[] { this.InHoney });
			base.AddStopper(this.InHoney);
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x005F5848 File Offset: 0x005F3A48
		private void LavaDrops()
		{
			AFishingCondition[] array = base.Join(new AFishingCondition[] { this.InLava, this.CanFishInLava });
			base.AddWithHardmode(AFishDropRulePopulator.Rarity.Any, 6, 4877, 4878, base.Join(array, new AFishingCondition[] { this.Crate }));
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 3, base.Group(new int[] { 4819, 4820, 4872, 2331 }), base.Join(array, new AFishingCondition[] { this.HardMode }));
			base.Add(AFishDropRulePopulator.Rarity.Legendary, 3, base.Group(new int[] { 4819, 4820, 4872 }), base.Join(array, new AFishingCondition[] { this.EarlyMode }));
			base.Add(AFishDropRulePopulator.Rarity.VeryRare, 1, 2312, array);
			base.Add(AFishDropRulePopulator.Rarity.Rare, 1, 2315, array);
			base.AddStopper(this.InLava);
		}
	}
}
