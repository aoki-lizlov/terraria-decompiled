using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000286 RID: 646
	public class CustomIntCondition : AchievementCondition
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x00552F82 File Offset: 0x00551182
		// (set) Token: 0x060024EF RID: 9455 RVA: 0x00552F8C File Offset: 0x0055118C
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				int num = Utils.Clamp<int>(value, 0, this._maxValue);
				if (this._tracker != null)
				{
					((ConditionIntTracker)this._tracker).SetValue(num, true);
				}
				this._value = num;
				if (this._value == this._maxValue)
				{
					this.Complete();
				}
			}
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00552FDC File Offset: 0x005511DC
		private CustomIntCondition(string name, int maxValue)
			: base(name)
		{
			this._maxValue = maxValue;
			this._value = 0;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00552FF3 File Offset: 0x005511F3
		public override void Clear()
		{
			this._value = 0;
			base.Clear();
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00553002 File Offset: 0x00551202
		public override void Load(JObject state)
		{
			base.Load(state);
			this._value = (int)state["Value"];
			if (this._tracker != null)
			{
				((ConditionIntTracker)this._tracker).SetValue(this._value, false);
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00553040 File Offset: 0x00551240
		protected override IAchievementTracker CreateAchievementTracker()
		{
			return new ConditionIntTracker(this._maxValue);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x0055304D File Offset: 0x0055124D
		public static AchievementCondition Create(string name, int maxValue)
		{
			return new CustomIntCondition(name, maxValue);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00553056 File Offset: 0x00551256
		public override void Complete()
		{
			if (this._tracker != null)
			{
				((ConditionIntTracker)this._tracker).SetValue(this._maxValue, true);
			}
			this._value = this._maxValue;
			base.Complete();
		}

		// Token: 0x04004F71 RID: 20337
		[JsonProperty("Value")]
		private int _value;

		// Token: 0x04004F72 RID: 20338
		private int _maxValue;
	}
}
