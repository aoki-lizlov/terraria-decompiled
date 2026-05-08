using System;
using System.Collections.Generic;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000420 RID: 1056
	public class PersonalityDatabase
	{
		// Token: 0x06003072 RID: 12402 RVA: 0x005BA111 File Offset: 0x005B8311
		public PersonalityDatabase()
		{
			this._personalityProfiles = new Dictionary<int, PersonalityProfile>();
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x005BA12F File Offset: 0x005B832F
		public void Register(int npcId, IShopPersonalityTrait trait)
		{
			if (!this._personalityProfiles.ContainsKey(npcId))
			{
				this._personalityProfiles[npcId] = new PersonalityProfile();
			}
			this._personalityProfiles[npcId].ShopModifiers.Add(trait);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x005BA168 File Offset: 0x005B8368
		public void Register(IShopPersonalityTrait trait, params int[] npcIds)
		{
			for (int i = 0; i < npcIds.Length; i++)
			{
				this.Register(trait, new int[] { npcIds[i] });
			}
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x005BA198 File Offset: 0x005B8398
		public PersonalityProfile GetByNPCID(int npcId)
		{
			PersonalityProfile personalityProfile;
			if (this._personalityProfiles.TryGetValue(npcId, out personalityProfile))
			{
				return personalityProfile;
			}
			return this._trashEntry;
		}

		// Token: 0x040056DC RID: 22236
		private Dictionary<int, PersonalityProfile> _personalityProfiles;

		// Token: 0x040056DD RID: 22237
		private PersonalityProfile _trashEntry = new PersonalityProfile();
	}
}
