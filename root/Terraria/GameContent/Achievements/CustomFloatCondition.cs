using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000285 RID: 645
	public class CustomFloatCondition : AchievementCondition
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x00552E70 File Offset: 0x00551070
		// (set) Token: 0x060024E7 RID: 9447 RVA: 0x00552E78 File Offset: 0x00551078
		public float Value
		{
			get
			{
				return this._value;
			}
			set
			{
				float num = Utils.Clamp<float>(value, 0f, this._maxValue);
				if (this._tracker != null)
				{
					((ConditionFloatTracker)this._tracker).SetValue(num, true);
				}
				this._value = num;
				if (this._value == this._maxValue)
				{
					this.Complete();
				}
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00552ECC File Offset: 0x005510CC
		private CustomFloatCondition(string name, float maxValue)
			: base(name)
		{
			this._maxValue = maxValue;
			this._value = 0f;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x00552EE7 File Offset: 0x005510E7
		public override void Clear()
		{
			this._value = 0f;
			base.Clear();
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00552EFA File Offset: 0x005510FA
		public override void Load(JObject state)
		{
			base.Load(state);
			this._value = (float)state["Value"];
			if (this._tracker != null)
			{
				((ConditionFloatTracker)this._tracker).SetValue(this._value, false);
			}
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00552F39 File Offset: 0x00551139
		protected override IAchievementTracker CreateAchievementTracker()
		{
			return new ConditionFloatTracker(this._maxValue);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x00552F46 File Offset: 0x00551146
		public static AchievementCondition Create(string name, float maxValue)
		{
			return new CustomFloatCondition(name, maxValue);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00552F4F File Offset: 0x0055114F
		public override void Complete()
		{
			if (this._tracker != null)
			{
				((ConditionFloatTracker)this._tracker).SetValue(this._maxValue, true);
			}
			this._value = this._maxValue;
			base.Complete();
		}

		// Token: 0x04004F6F RID: 20335
		[JsonProperty("Value")]
		private float _value;

		// Token: 0x04004F70 RID: 20336
		private float _maxValue;
	}
}
