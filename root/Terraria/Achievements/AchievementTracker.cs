using System;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x020005E5 RID: 1509
	public abstract class AchievementTracker<T> : IAchievementTracker
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x0065AB0C File Offset: 0x00658D0C
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x0065AB14 File Offset: 0x00658D14
		public T MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x0065AB1C File Offset: 0x00658D1C
		protected AchievementTracker(TrackerType type)
		{
			this._type = type;
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x0065AB2B File Offset: 0x00658D2B
		void IAchievementTracker.ReportAs(string name)
		{
			this._name = name;
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x0065AB34 File Offset: 0x00658D34
		TrackerType IAchievementTracker.GetTrackerType()
		{
			return this._type;
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x0065AB3C File Offset: 0x00658D3C
		void IAchievementTracker.Clear()
		{
			this.SetValue(default(T), true);
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x0065AB5C File Offset: 0x00658D5C
		public void SetValue(T newValue, bool reportUpdate = true)
		{
			if (!newValue.Equals(this._value))
			{
				this._value = newValue;
				if (reportUpdate)
				{
					this.ReportUpdate();
					if (this._value.Equals(this._maxValue))
					{
						this.OnComplete();
					}
				}
			}
		}

		// Token: 0x06003B45 RID: 15173
		public abstract void ReportUpdate();

		// Token: 0x06003B46 RID: 15174
		protected abstract void Load();

		// Token: 0x06003B47 RID: 15175 RVA: 0x0065ABB7 File Offset: 0x00658DB7
		void IAchievementTracker.Load()
		{
			this.Load();
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x0065ABBF File Offset: 0x00658DBF
		protected void OnComplete()
		{
			if (SocialAPI.Achievements != null)
			{
				SocialAPI.Achievements.StoreStats();
			}
		}

		// Token: 0x04005E71 RID: 24177
		protected T _value;

		// Token: 0x04005E72 RID: 24178
		protected T _maxValue;

		// Token: 0x04005E73 RID: 24179
		protected string _name;

		// Token: 0x04005E74 RID: 24180
		private TrackerType _type;
	}
}
