using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A57 RID: 2647
	public class IncrementingEventCounter : DiagnosticCounter
	{
		// Token: 0x0600614F RID: 24911 RVA: 0x0014D474 File Offset: 0x0014B674
		public IncrementingEventCounter(string name, EventSource eventSource)
			: base(name, eventSource)
		{
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x00004088 File Offset: 0x00002288
		public void Increment(double increment = 1.0)
		{
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06006151 RID: 24913 RVA: 0x0014D933 File Offset: 0x0014BB33
		// (set) Token: 0x06006152 RID: 24914 RVA: 0x0014D93B File Offset: 0x0014BB3B
		public TimeSpan DisplayRateTimeScale
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayRateTimeScale>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayRateTimeScale>k__BackingField = value;
			}
		}

		// Token: 0x04003A77 RID: 14967
		[CompilerGenerated]
		private TimeSpan <DisplayRateTimeScale>k__BackingField;
	}
}
