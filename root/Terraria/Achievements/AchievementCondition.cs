using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.Achievements
{
	// Token: 0x020005E1 RID: 1505
	[JsonObject(1)]
	public abstract class AchievementCondition
	{
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06003B19 RID: 15129 RVA: 0x0065A358 File Offset: 0x00658558
		// (remove) Token: 0x06003B1A RID: 15130 RVA: 0x0065A390 File Offset: 0x00658590
		public event AchievementCondition.AchievementUpdate OnComplete
		{
			[CompilerGenerated]
			add
			{
				AchievementCondition.AchievementUpdate achievementUpdate = this.OnComplete;
				AchievementCondition.AchievementUpdate achievementUpdate2;
				do
				{
					achievementUpdate2 = achievementUpdate;
					AchievementCondition.AchievementUpdate achievementUpdate3 = (AchievementCondition.AchievementUpdate)Delegate.Combine(achievementUpdate2, value);
					achievementUpdate = Interlocked.CompareExchange<AchievementCondition.AchievementUpdate>(ref this.OnComplete, achievementUpdate3, achievementUpdate2);
				}
				while (achievementUpdate != achievementUpdate2);
			}
			[CompilerGenerated]
			remove
			{
				AchievementCondition.AchievementUpdate achievementUpdate = this.OnComplete;
				AchievementCondition.AchievementUpdate achievementUpdate2;
				do
				{
					achievementUpdate2 = achievementUpdate;
					AchievementCondition.AchievementUpdate achievementUpdate3 = (AchievementCondition.AchievementUpdate)Delegate.Remove(achievementUpdate2, value);
					achievementUpdate = Interlocked.CompareExchange<AchievementCondition.AchievementUpdate>(ref this.OnComplete, achievementUpdate3, achievementUpdate2);
				}
				while (achievementUpdate != achievementUpdate2);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x0065A3C5 File Offset: 0x006585C5
		public bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x0065A3CD File Offset: 0x006585CD
		protected AchievementCondition(string name)
		{
			this.Name = name;
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x0065A3DC File Offset: 0x006585DC
		public virtual void Load(JObject state)
		{
			this._isCompleted = (bool)state["Completed"];
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x0065A3F4 File Offset: 0x006585F4
		public virtual void Clear()
		{
			this._isCompleted = false;
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x0065A3FD File Offset: 0x006585FD
		public virtual void Complete()
		{
			if (this._isCompleted)
			{
				return;
			}
			this._isCompleted = true;
			if (this.OnComplete != null)
			{
				this.OnComplete(this);
			}
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x00076333 File Offset: 0x00074533
		protected virtual IAchievementTracker CreateAchievementTracker()
		{
			return null;
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x0065A423 File Offset: 0x00658623
		public IAchievementTracker GetAchievementTracker()
		{
			if (this._tracker == null)
			{
				this._tracker = this.CreateAchievementTracker();
			}
			return this._tracker;
		}

		// Token: 0x04005E65 RID: 24165
		public readonly string Name;

		// Token: 0x04005E66 RID: 24166
		[CompilerGenerated]
		private AchievementCondition.AchievementUpdate OnComplete;

		// Token: 0x04005E67 RID: 24167
		protected IAchievementTracker _tracker;

		// Token: 0x04005E68 RID: 24168
		[JsonProperty("Completed")]
		private bool _isCompleted;

		// Token: 0x020009CE RID: 2510
		// (Invoke) Token: 0x06004A6B RID: 19051
		public delegate void AchievementUpdate(AchievementCondition condition);
	}
}
