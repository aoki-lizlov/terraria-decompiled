using System;

namespace Terraria.GameContent
{
	// Token: 0x02000274 RID: 628
	public struct WellFedHelper
	{
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x0054B7F4 File Offset: 0x005499F4
		public int TimeLeft
		{
			get
			{
				return this._timeLeftRank1 + this._timeLeftRank2 + this._timeLeftRank3;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x0054B80A File Offset: 0x00549A0A
		public int Rank
		{
			get
			{
				if (this._timeLeftRank3 > 0)
				{
					return 3;
				}
				if (this._timeLeftRank2 > 0)
				{
					return 2;
				}
				if (this._timeLeftRank1 > 0)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x0054B830 File Offset: 0x00549A30
		public void Eat(int foodRank, int foodBuffTime)
		{
			int num = foodBuffTime;
			if (foodRank >= 3)
			{
				this.AddTimeTo(ref this._timeLeftRank3, ref num, 72000);
			}
			if (foodRank >= 2)
			{
				this.AddTimeTo(ref this._timeLeftRank2, ref num, 72000);
			}
			if (foodRank >= 1)
			{
				this.AddTimeTo(ref this._timeLeftRank1, ref num, 72000);
			}
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x0054B884 File Offset: 0x00549A84
		public void Update()
		{
			this.ReduceTimeLeft();
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0054B88C File Offset: 0x00549A8C
		public void Clear()
		{
			this._timeLeftRank1 = 0;
			this._timeLeftRank2 = 0;
			this._timeLeftRank3 = 0;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0054B8A4 File Offset: 0x00549AA4
		private void AddTimeTo(ref int foodTimeCounter, ref int timeLeftToAdd, int counterMaximumTime)
		{
			if (timeLeftToAdd == 0)
			{
				return;
			}
			int num = timeLeftToAdd;
			if (foodTimeCounter + num > counterMaximumTime)
			{
				num = counterMaximumTime - foodTimeCounter;
			}
			foodTimeCounter += num;
			timeLeftToAdd -= num;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x0054B8D4 File Offset: 0x00549AD4
		private void ReduceTimeLeft()
		{
			if (this._timeLeftRank3 > 0)
			{
				this._timeLeftRank3--;
				return;
			}
			if (this._timeLeftRank2 > 0)
			{
				this._timeLeftRank2--;
				return;
			}
			if (this._timeLeftRank1 > 0)
			{
				this._timeLeftRank1--;
			}
		}

		// Token: 0x04004DE2 RID: 19938
		private const int MAXIMUM_TIME_LEFT_PER_COUNTER = 72000;

		// Token: 0x04004DE3 RID: 19939
		private int _timeLeftRank1;

		// Token: 0x04004DE4 RID: 19940
		private int _timeLeftRank2;

		// Token: 0x04004DE5 RID: 19941
		private int _timeLeftRank3;
	}
}
