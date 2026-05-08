using System;
using System.Collections;
using System.Collections.Generic;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x0200041E RID: 1054
	public class BiomePreferenceListTrait : IShopPersonalityTrait, IEnumerable<BiomePreferenceListTrait.BiomePreference>, IEnumerable
	{
		// Token: 0x06003069 RID: 12393 RVA: 0x005B9F98 File Offset: 0x005B8198
		public BiomePreferenceListTrait()
		{
			this._preferences = new List<BiomePreferenceListTrait.BiomePreference>();
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x005B9FAB File Offset: 0x005B81AB
		public void Add(BiomePreferenceListTrait.BiomePreference preference)
		{
			this._preferences.Add(preference);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x005B9FB9 File Offset: 0x005B81B9
		public void Add(AffectionLevel level, AShoppingBiome biome)
		{
			this._preferences.Add(new BiomePreferenceListTrait.BiomePreference(level, biome));
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x005B9FD0 File Offset: 0x005B81D0
		public void ModifyShopPrice(HelperInfo info, ShopHelper shopHelperInstance)
		{
			BiomePreferenceListTrait.BiomePreference biomePreference = null;
			for (int i = 0; i < this._preferences.Count; i++)
			{
				BiomePreferenceListTrait.BiomePreference biomePreference2 = this._preferences[i];
				if (biomePreference2.Biome.IsInBiome(info.player) && (biomePreference == null || biomePreference.Affection < biomePreference2.Affection))
				{
					biomePreference = biomePreference2;
				}
			}
			if (biomePreference != null)
			{
				this.ApplyPreference(biomePreference, info, shopHelperInstance);
			}
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x005BA034 File Offset: 0x005B8234
		private void ApplyPreference(BiomePreferenceListTrait.BiomePreference preference, HelperInfo info, ShopHelper shopHelperInstance)
		{
			string nameKey = preference.Biome.NameKey;
			AffectionLevel affection = preference.Affection;
			if (affection <= AffectionLevel.Dislike)
			{
				if (affection != AffectionLevel.Hate)
				{
					if (affection != AffectionLevel.Dislike)
					{
						return;
					}
					shopHelperInstance.DislikeBiome(nameKey);
					return;
				}
				else
				{
					shopHelperInstance.HateBiome(nameKey);
				}
			}
			else
			{
				if (affection == AffectionLevel.Like)
				{
					shopHelperInstance.LikeBiome(nameKey);
					return;
				}
				if (affection == AffectionLevel.Love)
				{
					shopHelperInstance.LoveBiome(nameKey);
					return;
				}
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x005BA08D File Offset: 0x005B828D
		public IEnumerator<BiomePreferenceListTrait.BiomePreference> GetEnumerator()
		{
			return this._preferences.GetEnumerator();
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x005BA08D File Offset: 0x005B828D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._preferences.GetEnumerator();
		}

		// Token: 0x040056D9 RID: 22233
		private List<BiomePreferenceListTrait.BiomePreference> _preferences;

		// Token: 0x0200093D RID: 2365
		public class BiomePreference
		{
			// Token: 0x0600482B RID: 18475 RVA: 0x006CCC16 File Offset: 0x006CAE16
			public BiomePreference(AffectionLevel affection, AShoppingBiome biome)
			{
				this.Affection = affection;
				this.Biome = biome;
			}

			// Token: 0x04007542 RID: 30018
			public AffectionLevel Affection;

			// Token: 0x04007543 RID: 30019
			public AShoppingBiome Biome;
		}
	}
}
