using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002EC RID: 748
	public class ParallelOptions
	{
		// Token: 0x06002195 RID: 8597 RVA: 0x000794BE File Offset: 0x000776BE
		public ParallelOptions()
		{
			this._scheduler = TaskScheduler.Default;
			this._maxDegreeOfParallelism = -1;
			this._cancellationToken = CancellationToken.None;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x000794E3 File Offset: 0x000776E3
		// (set) Token: 0x06002197 RID: 8599 RVA: 0x000794EB File Offset: 0x000776EB
		public TaskScheduler TaskScheduler
		{
			get
			{
				return this._scheduler;
			}
			set
			{
				this._scheduler = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x000794F4 File Offset: 0x000776F4
		internal TaskScheduler EffectiveTaskScheduler
		{
			get
			{
				if (this._scheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this._scheduler;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0007950A File Offset: 0x0007770A
		// (set) Token: 0x0600219A RID: 8602 RVA: 0x00079512 File Offset: 0x00077712
		public int MaxDegreeOfParallelism
		{
			get
			{
				return this._maxDegreeOfParallelism;
			}
			set
			{
				if (value == 0 || value < -1)
				{
					throw new ArgumentOutOfRangeException("MaxDegreeOfParallelism");
				}
				this._maxDegreeOfParallelism = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x0007952D File Offset: 0x0007772D
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x00079535 File Offset: 0x00077735
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
			set
			{
				this._cancellationToken = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x00079540 File Offset: 0x00077740
		internal int EffectiveMaxConcurrencyLevel
		{
			get
			{
				int num = this.MaxDegreeOfParallelism;
				int maximumConcurrencyLevel = this.EffectiveTaskScheduler.MaximumConcurrencyLevel;
				if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel != 2147483647)
				{
					num = ((num == -1) ? maximumConcurrencyLevel : Math.Min(maximumConcurrencyLevel, num));
				}
				return num;
			}
		}

		// Token: 0x04001AB4 RID: 6836
		private TaskScheduler _scheduler;

		// Token: 0x04001AB5 RID: 6837
		private int _maxDegreeOfParallelism;

		// Token: 0x04001AB6 RID: 6838
		private CancellationToken _cancellationToken;
	}
}
