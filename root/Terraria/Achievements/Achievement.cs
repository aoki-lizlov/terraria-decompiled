using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Localization;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x020005E0 RID: 1504
	[JsonObject(1)]
	public class Achievement
	{
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x00659F74 File Offset: 0x00658174
		public AchievementCategory Category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06003B06 RID: 15110 RVA: 0x00659F7C File Offset: 0x0065817C
		// (remove) Token: 0x06003B07 RID: 15111 RVA: 0x00659FB4 File Offset: 0x006581B4
		public event Achievement.AchievementCompleted OnCompleted
		{
			[CompilerGenerated]
			add
			{
				Achievement.AchievementCompleted achievementCompleted = this.OnCompleted;
				Achievement.AchievementCompleted achievementCompleted2;
				do
				{
					achievementCompleted2 = achievementCompleted;
					Achievement.AchievementCompleted achievementCompleted3 = (Achievement.AchievementCompleted)Delegate.Combine(achievementCompleted2, value);
					achievementCompleted = Interlocked.CompareExchange<Achievement.AchievementCompleted>(ref this.OnCompleted, achievementCompleted3, achievementCompleted2);
				}
				while (achievementCompleted != achievementCompleted2);
			}
			[CompilerGenerated]
			remove
			{
				Achievement.AchievementCompleted achievementCompleted = this.OnCompleted;
				Achievement.AchievementCompleted achievementCompleted2;
				do
				{
					achievementCompleted2 = achievementCompleted;
					Achievement.AchievementCompleted achievementCompleted3 = (Achievement.AchievementCompleted)Delegate.Remove(achievementCompleted2, value);
					achievementCompleted = Interlocked.CompareExchange<Achievement.AchievementCompleted>(ref this.OnCompleted, achievementCompleted3, achievementCompleted2);
				}
				while (achievementCompleted != achievementCompleted2);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x00659FE9 File Offset: 0x006581E9
		public bool HasTracker
		{
			get
			{
				return this._tracker != null;
			}
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x00659FF4 File Offset: 0x006581F4
		public IAchievementTracker GetTracker()
		{
			return this._tracker;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x00659FFC File Offset: 0x006581FC
		public bool IsCompleted
		{
			get
			{
				return this._completedCount == this._conditions.Count;
			}
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x0065A014 File Offset: 0x00658214
		public Achievement(string name)
		{
			this.Name = name;
			this.FriendlyName = Language.GetText("Achievements." + name + "_Name");
			this.Description = Language.GetText("Achievements." + name + "_Description");
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x0065A084 File Offset: 0x00658284
		public void ClearProgress()
		{
			this._completedCount = 0;
			foreach (KeyValuePair<string, AchievementCondition> keyValuePair in this._conditions)
			{
				keyValuePair.Value.Clear();
			}
			if (this._tracker != null)
			{
				this._tracker.Clear();
			}
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x0065A0F8 File Offset: 0x006582F8
		public void Load(Dictionary<string, JObject> conditions)
		{
			foreach (KeyValuePair<string, JObject> keyValuePair in conditions)
			{
				AchievementCondition achievementCondition;
				if (this._conditions.TryGetValue(keyValuePair.Key, out achievementCondition))
				{
					achievementCondition.Load(keyValuePair.Value);
					if (achievementCondition.IsCompleted)
					{
						this._completedCount++;
					}
				}
			}
			if (this._tracker != null)
			{
				this._tracker.Load();
			}
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x0065A18C File Offset: 0x0065838C
		public void AddCondition(AchievementCondition condition)
		{
			this._conditions[condition.Name] = condition;
			condition.OnComplete += this.OnConditionComplete;
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x0065A1B4 File Offset: 0x006583B4
		private void OnConditionComplete(AchievementCondition condition)
		{
			this._completedCount++;
			if (this._completedCount == this._conditions.Count)
			{
				if (this._tracker == null && SocialAPI.Achievements != null)
				{
					SocialAPI.Achievements.CompleteAchievement(this.Name);
				}
				if (this.OnCompleted != null)
				{
					this.OnCompleted(this);
				}
			}
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x0065A215 File Offset: 0x00658415
		private void UseTracker(IAchievementTracker tracker)
		{
			tracker.ReportAs("STAT_" + this.Name);
			this._tracker = tracker;
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x0065A234 File Offset: 0x00658434
		public void UseTrackerFromCondition(string conditionName)
		{
			this.UseTracker(this.GetConditionTracker(conditionName));
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x0065A244 File Offset: 0x00658444
		public void UseConditionsCompletedTracker()
		{
			ConditionsCompletedTracker conditionsCompletedTracker = new ConditionsCompletedTracker();
			foreach (KeyValuePair<string, AchievementCondition> keyValuePair in this._conditions)
			{
				conditionsCompletedTracker.AddCondition(keyValuePair.Value);
			}
			this.UseTracker(conditionsCompletedTracker);
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x0065A2AC File Offset: 0x006584AC
		public void UseConditionsCompletedTracker(params string[] conditions)
		{
			ConditionsCompletedTracker conditionsCompletedTracker = new ConditionsCompletedTracker();
			foreach (string text in conditions)
			{
				conditionsCompletedTracker.AddCondition(this._conditions[text]);
			}
			this.UseTracker(conditionsCompletedTracker);
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x0065A2EA File Offset: 0x006584EA
		public void ClearTracker()
		{
			this._tracker = null;
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x0065A2F3 File Offset: 0x006584F3
		private IAchievementTracker GetConditionTracker(string name)
		{
			return this._conditions[name].GetAchievementTracker();
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x0065A308 File Offset: 0x00658508
		public void AddConditions(params AchievementCondition[] conditions)
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				this.AddCondition(conditions[i]);
			}
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x0065A32C File Offset: 0x0065852C
		public AchievementCondition GetCondition(string conditionName)
		{
			AchievementCondition achievementCondition;
			if (this._conditions.TryGetValue(conditionName, out achievementCondition))
			{
				return achievementCondition;
			}
			return null;
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x0065A34C File Offset: 0x0065854C
		public void SetCategory(AchievementCategory category)
		{
			this._category = category;
		}

		// Token: 0x04005E5B RID: 24155
		private static int _totalAchievements;

		// Token: 0x04005E5C RID: 24156
		public readonly string Name;

		// Token: 0x04005E5D RID: 24157
		public readonly LocalizedText FriendlyName;

		// Token: 0x04005E5E RID: 24158
		public readonly LocalizedText Description;

		// Token: 0x04005E5F RID: 24159
		public readonly int Id = Achievement._totalAchievements++;

		// Token: 0x04005E60 RID: 24160
		private AchievementCategory _category;

		// Token: 0x04005E61 RID: 24161
		private IAchievementTracker _tracker;

		// Token: 0x04005E62 RID: 24162
		[CompilerGenerated]
		private Achievement.AchievementCompleted OnCompleted;

		// Token: 0x04005E63 RID: 24163
		[JsonProperty("Conditions")]
		private Dictionary<string, AchievementCondition> _conditions = new Dictionary<string, AchievementCondition>();

		// Token: 0x04005E64 RID: 24164
		private int _completedCount;

		// Token: 0x020009CD RID: 2509
		// (Invoke) Token: 0x06004A67 RID: 19047
		public delegate void AchievementCompleted(Achievement achievement);
	}
}
