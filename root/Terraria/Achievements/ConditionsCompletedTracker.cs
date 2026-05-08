using System;
using System.Collections.Generic;

namespace Terraria.Achievements
{
	// Token: 0x020005E6 RID: 1510
	public class ConditionsCompletedTracker : ConditionIntTracker
	{
		// Token: 0x06003B49 RID: 15177 RVA: 0x0065ABD2 File Offset: 0x00658DD2
		public void AddCondition(AchievementCondition condition)
		{
			this._maxValue++;
			condition.OnComplete += this.OnConditionCompleted;
			this._conditions.Add(condition);
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x0065AC00 File Offset: 0x00658E00
		private void OnConditionCompleted(AchievementCondition condition)
		{
			base.SetValue(Math.Min(this._value + 1, this._maxValue), true);
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x0065AC1C File Offset: 0x00658E1C
		protected override void Load()
		{
			for (int i = 0; i < this._conditions.Count; i++)
			{
				if (this._conditions[i].IsCompleted)
				{
					this._value++;
				}
			}
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x0065AC60 File Offset: 0x00658E60
		public ConditionsCompletedTracker()
		{
		}

		// Token: 0x04005E75 RID: 24181
		private List<AchievementCondition> _conditions = new List<AchievementCondition>();
	}
}
