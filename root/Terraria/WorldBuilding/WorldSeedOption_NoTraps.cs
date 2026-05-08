using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200009E RID: 158
	public class WorldSeedOption_NoTraps : AWorldGenerationOption
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x004DD9FB File Offset: 0x004DBBFB
		protected override string KeyName
		{
			get
			{
				return "Seed_NoTraps";
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x004DDA02 File Offset: 0x004DBC02
		public override string ServerConfigName
		{
			get
			{
				return "notraps";
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x004DDA09 File Offset: 0x004DBC09
		public WorldSeedOption_NoTraps()
		{
			base.SpecialSeedNames = new string[] { "notraps" };
			base.SpecialSeedValues = new int[0];
		}
	}
}
