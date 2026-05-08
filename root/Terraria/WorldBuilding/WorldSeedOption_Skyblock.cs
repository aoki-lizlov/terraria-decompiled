using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A1 RID: 161
	public class WorldSeedOption_Skyblock : AWorldGenerationOption
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x004DDA9D File Offset: 0x004DBC9D
		protected override string KeyName
		{
			get
			{
				return "Seed_Skyblock";
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x004DDAA4 File Offset: 0x004DBCA4
		public override string ServerConfigName
		{
			get
			{
				return "skyblock";
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x004DDAAB File Offset: 0x004DBCAB
		public WorldSeedOption_Skyblock()
		{
			base.SpecialSeedNames = new string[] { "skyblock" };
			base.SpecialSeedValues = new int[0];
		}
	}
}
