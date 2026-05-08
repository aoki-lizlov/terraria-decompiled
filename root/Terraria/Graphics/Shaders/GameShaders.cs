using System;
using System.Collections.Generic;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020001E5 RID: 485
	public class GameShaders
	{
		// Token: 0x0600207E RID: 8318 RVA: 0x0000357B File Offset: 0x0000177B
		public GameShaders()
		{
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00522C8F File Offset: 0x00520E8F
		// Note: this type is marked as 'beforefieldinit'.
		static GameShaders()
		{
		}

		// Token: 0x04004AFF RID: 19199
		public static ArmorShaderDataSet Armor = new ArmorShaderDataSet();

		// Token: 0x04004B00 RID: 19200
		public static HairShaderDataSet Hair = new HairShaderDataSet();

		// Token: 0x04004B01 RID: 19201
		public static Dictionary<string, MiscShaderData> Misc = new Dictionary<string, MiscShaderData>();
	}
}
