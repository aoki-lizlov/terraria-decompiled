using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A0 RID: 160
	public class WorldSeedOption_Remix : AWorldGenerationOption
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x004DDA67 File Offset: 0x004DBC67
		protected override string KeyName
		{
			get
			{
				return "Seed_Remix";
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x004DDA6E File Offset: 0x004DBC6E
		public override string ServerConfigName
		{
			get
			{
				return "remix";
			}
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x004DDA75 File Offset: 0x004DBC75
		public WorldSeedOption_Remix()
		{
			base.SpecialSeedNames = new string[] { "dontdigup" };
			base.SpecialSeedValues = new int[0];
		}
	}
}
