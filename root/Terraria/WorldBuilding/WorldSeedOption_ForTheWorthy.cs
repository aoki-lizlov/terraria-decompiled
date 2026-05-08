using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200009F RID: 159
	public class WorldSeedOption_ForTheWorthy : AWorldGenerationOption
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x004DDA31 File Offset: 0x004DBC31
		protected override string KeyName
		{
			get
			{
				return "Seed_ForTheWorthy";
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x004DDA38 File Offset: 0x004DBC38
		public override string ServerConfigName
		{
			get
			{
				return "fortheworthy";
			}
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x004DDA3F File Offset: 0x004DBC3F
		public WorldSeedOption_ForTheWorthy()
		{
			base.SpecialSeedNames = new string[] { "fortheworthy" };
			base.SpecialSeedValues = new int[0];
		}
	}
}
