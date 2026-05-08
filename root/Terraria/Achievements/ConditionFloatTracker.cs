using System;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x020005E2 RID: 1506
	public class ConditionFloatTracker : AchievementTracker<float>
	{
		// Token: 0x06003B22 RID: 15138 RVA: 0x0065A43F File Offset: 0x0065863F
		public ConditionFloatTracker(float maxValue)
			: base(TrackerType.Float)
		{
			this._maxValue = maxValue;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x0065A44F File Offset: 0x0065864F
		public ConditionFloatTracker()
			: base(TrackerType.Float)
		{
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x0065A458 File Offset: 0x00658658
		public override void ReportUpdate()
		{
			if (SocialAPI.Achievements != null && this._name != null)
			{
				SocialAPI.Achievements.UpdateFloatStat(this._name, this._value);
			}
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x00009E46 File Offset: 0x00008046
		protected override void Load()
		{
		}
	}
}
