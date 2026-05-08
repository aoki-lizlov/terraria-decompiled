using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200009D RID: 157
	public class WorldSeedOption_NotTheBees : AWorldGenerationOption
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x004DD9C5 File Offset: 0x004DBBC5
		protected override string KeyName
		{
			get
			{
				return "Seed_NotTheBees";
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x004DD9CC File Offset: 0x004DBBCC
		public override string ServerConfigName
		{
			get
			{
				return "notthebees";
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x004DD9D3 File Offset: 0x004DBBD3
		public WorldSeedOption_NotTheBees()
		{
			base.SpecialSeedNames = new string[] { "notthebees" };
			base.SpecialSeedValues = new int[0];
		}
	}
}
