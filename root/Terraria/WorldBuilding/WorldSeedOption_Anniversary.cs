using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200009B RID: 155
	public class WorldSeedOption_Anniversary : AWorldGenerationOption
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x004DD931 File Offset: 0x004DBB31
		protected override string KeyName
		{
			get
			{
				return "Seed_Celebration";
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x004DD938 File Offset: 0x004DBB38
		public override string ServerConfigName
		{
			get
			{
				return "celebration";
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x004DD93F File Offset: 0x004DBB3F
		public WorldSeedOption_Anniversary()
		{
			base.SpecialSeedNames = new string[] { "celebrationmk10" };
			base.SpecialSeedValues = new int[] { 5162021, 5162011 };
		}
	}
}
