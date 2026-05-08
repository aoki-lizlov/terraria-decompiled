using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200053E RID: 1342
	public class CachedProjectileCounterBuffTextHandler : IBuffTextHandler
	{
		// Token: 0x06003762 RID: 14178 RVA: 0x0062EE5C File Offset: 0x0062D05C
		public CachedProjectileCounterBuffTextHandler(params int[] projectileTypesToLookFor)
		{
			this.projectilesToLookFor = projectileTypesToLookFor;
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x0062EE6C File Offset: 0x0062D06C
		public string HandleBuffText()
		{
			if (this.projectilesToLookFor == null)
			{
				return null;
			}
			int[] ownedProjectileCounts = Main.LocalPlayer.ownedProjectileCounts;
			float num = 0f;
			foreach (int num2 in this.projectilesToLookFor)
			{
				num += (float)ownedProjectileCounts[num2];
			}
			if (num > 0f)
			{
				return "x" + num;
			}
			return null;
		}

		// Token: 0x04005B96 RID: 23446
		private int[] projectilesToLookFor;
	}
}
