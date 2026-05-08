using System;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000284 RID: 644
	public class CustomFlagCondition : AchievementCondition
	{
		// Token: 0x060024E4 RID: 9444 RVA: 0x00552E5F File Offset: 0x0055105F
		private CustomFlagCondition(string name)
			: base(name)
		{
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x00552E68 File Offset: 0x00551068
		public static AchievementCondition Create(string name)
		{
			return new CustomFlagCondition(name);
		}
	}
}
