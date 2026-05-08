using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200009C RID: 156
	public class WorldSeedOption_DontStarve : AWorldGenerationOption
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x004DD977 File Offset: 0x004DBB77
		protected override string KeyName
		{
			get
			{
				return "Seed_TheConstant";
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x004DD97E File Offset: 0x004DBB7E
		public override string ServerConfigName
		{
			get
			{
				return "theconstant";
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x004DD985 File Offset: 0x004DBB85
		public WorldSeedOption_DontStarve()
		{
			base.SpecialSeedNames = new string[] { "constant", "theconstant", "eye4aneye", "eyeforaneye" };
			base.SpecialSeedValues = new int[0];
		}
	}
}
