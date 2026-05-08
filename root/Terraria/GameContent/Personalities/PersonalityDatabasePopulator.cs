using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000421 RID: 1057
	public class PersonalityDatabasePopulator
	{
		// Token: 0x06003076 RID: 12406 RVA: 0x005BA1BD File Offset: 0x005B83BD
		public void Populate(PersonalityDatabase database)
		{
			this._currentDatabase = database;
			this.Populate_BiomePreferences(database);
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x005BA1D0 File Offset: 0x005B83D0
		private void Populate_BiomePreferences(PersonalityDatabase database)
		{
			OceanBiome oceanBiome = new OceanBiome();
			ForestBiome forestBiome = new ForestBiome();
			SnowBiome snowBiome = new SnowBiome();
			DesertBiome desertBiome = new DesertBiome();
			JungleBiome jungleBiome = new JungleBiome();
			UndergroundBiome undergroundBiome = new UndergroundBiome();
			HallowBiome hallowBiome = new HallowBiome();
			MushroomBiome mushroomBiome = new MushroomBiome();
			AffectionLevel affectionLevel = AffectionLevel.Love;
			AffectionLevel affectionLevel2 = AffectionLevel.Like;
			AffectionLevel affectionLevel3 = AffectionLevel.Dislike;
			AffectionLevel affectionLevel4 = AffectionLevel.Hate;
			database.Register(22, new BiomePreferenceListTrait
			{
				{ affectionLevel2, forestBiome },
				{ affectionLevel3, oceanBiome }
			});
			database.Register(17, new BiomePreferenceListTrait
			{
				{ affectionLevel2, forestBiome },
				{ affectionLevel3, desertBiome }
			});
			database.Register(588, new BiomePreferenceListTrait
			{
				{ affectionLevel2, forestBiome },
				{ affectionLevel3, undergroundBiome }
			});
			database.Register(633, new BiomePreferenceListTrait
			{
				{ affectionLevel2, forestBiome },
				{ affectionLevel3, desertBiome }
			});
			database.Register(441, new BiomePreferenceListTrait
			{
				{ affectionLevel2, snowBiome },
				{ affectionLevel3, hallowBiome }
			});
			database.Register(124, new BiomePreferenceListTrait
			{
				{ affectionLevel2, snowBiome },
				{ affectionLevel3, undergroundBiome }
			});
			database.Register(209, new BiomePreferenceListTrait
			{
				{ affectionLevel2, snowBiome },
				{ affectionLevel3, jungleBiome }
			});
			database.Register(142, new BiomePreferenceListTrait
			{
				{ affectionLevel, snowBiome },
				{ affectionLevel4, desertBiome }
			});
			database.Register(207, new BiomePreferenceListTrait
			{
				{ affectionLevel2, desertBiome },
				{ affectionLevel3, forestBiome }
			});
			database.Register(19, new BiomePreferenceListTrait
			{
				{ affectionLevel2, desertBiome },
				{ affectionLevel3, snowBiome }
			});
			database.Register(178, new BiomePreferenceListTrait
			{
				{ affectionLevel2, desertBiome },
				{ affectionLevel3, jungleBiome }
			});
			database.Register(20, new BiomePreferenceListTrait
			{
				{ affectionLevel2, jungleBiome },
				{ affectionLevel3, desertBiome }
			});
			database.Register(228, new BiomePreferenceListTrait
			{
				{ affectionLevel2, jungleBiome },
				{ affectionLevel3, hallowBiome }
			});
			database.Register(227, new BiomePreferenceListTrait
			{
				{ affectionLevel2, jungleBiome },
				{ affectionLevel3, forestBiome }
			});
			database.Register(369, new BiomePreferenceListTrait
			{
				{ affectionLevel2, oceanBiome },
				{ affectionLevel3, desertBiome }
			});
			database.Register(229, new BiomePreferenceListTrait
			{
				{ affectionLevel2, oceanBiome },
				{ affectionLevel3, undergroundBiome }
			});
			database.Register(353, new BiomePreferenceListTrait
			{
				{ affectionLevel2, oceanBiome },
				{ affectionLevel3, snowBiome }
			});
			database.Register(38, new BiomePreferenceListTrait
			{
				{ affectionLevel2, undergroundBiome },
				{ affectionLevel3, oceanBiome }
			});
			database.Register(107, new BiomePreferenceListTrait
			{
				{ affectionLevel2, undergroundBiome },
				{ affectionLevel3, jungleBiome }
			});
			database.Register(54, new BiomePreferenceListTrait
			{
				{ affectionLevel2, undergroundBiome },
				{ affectionLevel3, hallowBiome }
			});
			database.Register(108, new BiomePreferenceListTrait
			{
				{ affectionLevel2, hallowBiome },
				{ affectionLevel3, oceanBiome }
			});
			database.Register(18, new BiomePreferenceListTrait
			{
				{ affectionLevel2, hallowBiome },
				{ affectionLevel3, snowBiome }
			});
			database.Register(208, new BiomePreferenceListTrait
			{
				{ affectionLevel2, hallowBiome },
				{ affectionLevel3, undergroundBiome }
			});
			database.Register(550, new BiomePreferenceListTrait
			{
				{ affectionLevel2, hallowBiome },
				{ affectionLevel3, snowBiome }
			});
			database.Register(160, new BiomePreferenceListTrait { { affectionLevel2, mushroomBiome } });
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0000357B File Offset: 0x0000177B
		public PersonalityDatabasePopulator()
		{
		}

		// Token: 0x040056DE RID: 22238
		private PersonalityDatabase _currentDatabase;
	}
}
