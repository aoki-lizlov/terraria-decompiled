using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A3 RID: 163
	public class WorldSeedOption_Drunk : AWorldGenerationOption
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x004DDCF0 File Offset: 0x004DBEF0
		protected override string KeyName
		{
			get
			{
				return "Seed_Drunk";
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x004DDCF7 File Offset: 0x004DBEF7
		public override string ServerConfigName
		{
			get
			{
				return "drunk";
			}
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x004DDCFE File Offset: 0x004DBEFE
		public WorldSeedOption_Drunk()
		{
			base.SpecialSeedNames = new string[0];
			base.SpecialSeedValues = new int[] { 5162020 };
		}
	}
}
