using System;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x020005E3 RID: 1507
	public class ConditionIntTracker : AchievementTracker<int>
	{
		// Token: 0x06003B26 RID: 15142 RVA: 0x0065A47F File Offset: 0x0065867F
		public ConditionIntTracker()
			: base(TrackerType.Int)
		{
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x0065A488 File Offset: 0x00658688
		public ConditionIntTracker(int maxValue)
			: base(TrackerType.Int)
		{
			this._maxValue = maxValue;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x0065A498 File Offset: 0x00658698
		public override void ReportUpdate()
		{
			if (SocialAPI.Achievements != null && this._name != null)
			{
				SocialAPI.Achievements.UpdateIntStat(this._name, this._value);
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x00009E46 File Offset: 0x00008046
		protected override void Load()
		{
		}
	}
}
