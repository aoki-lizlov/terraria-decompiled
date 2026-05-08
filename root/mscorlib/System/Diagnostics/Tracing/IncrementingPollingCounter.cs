using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A58 RID: 2648
	public class IncrementingPollingCounter : DiagnosticCounter
	{
		// Token: 0x06006153 RID: 24915 RVA: 0x0014D474 File Offset: 0x0014B674
		public IncrementingPollingCounter(string name, EventSource eventSource, Func<double> totalValueProvider)
			: base(name, eventSource)
		{
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06006154 RID: 24916 RVA: 0x0014D944 File Offset: 0x0014BB44
		// (set) Token: 0x06006155 RID: 24917 RVA: 0x0014D94C File Offset: 0x0014BB4C
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

		// Token: 0x04003A78 RID: 14968
		[CompilerGenerated]
		private TimeSpan <DisplayRateTimeScale>k__BackingField;
	}
}
