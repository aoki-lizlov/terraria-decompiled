using System;
using Microsoft.Xna.Framework;
using Terraria.Achievements;

namespace Terraria.UI
{
	// Token: 0x020000E4 RID: 228
	public class AchievementAdvisorCard
	{
		// Token: 0x060018D6 RID: 6358 RVA: 0x004E5344 File Offset: 0x004E3544
		public AchievementAdvisorCard(Achievement achievement, float order)
		{
			this.achievement = achievement;
			this.order = order;
			this.achievementIndex = Main.Achievements.GetIconIndex(achievement.Name);
			this.frame = new Rectangle(this.achievementIndex % 8 * 66, this.achievementIndex / 8 * 66, 64, 64);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x004E53A0 File Offset: 0x004E35A0
		public bool IsAchievableInWorld()
		{
			string name = this.achievement.Name;
			if (name == "MASTERMIND")
			{
				return WorldGen.crimson;
			}
			if (!(name == "WORM_FODDER"))
			{
				return !(name == "PLAY_ON_A_SPECIAL_SEED") || Main.specialSeedWorld;
			}
			return !WorldGen.crimson;
		}

		// Token: 0x040012FB RID: 4859
		private const int _iconSize = 64;

		// Token: 0x040012FC RID: 4860
		private const int _iconSizeWithSpace = 66;

		// Token: 0x040012FD RID: 4861
		private const int _iconsPerRow = 8;

		// Token: 0x040012FE RID: 4862
		public Achievement achievement;

		// Token: 0x040012FF RID: 4863
		public float order;

		// Token: 0x04001300 RID: 4864
		public Rectangle frame;

		// Token: 0x04001301 RID: 4865
		public int achievementIndex;
	}
}
