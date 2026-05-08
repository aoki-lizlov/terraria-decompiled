using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A59 RID: 2649
	public class PollingCounter : DiagnosticCounter
	{
		// Token: 0x06006156 RID: 24918 RVA: 0x0014D474 File Offset: 0x0014B674
		public PollingCounter(string name, EventSource eventSource, Func<double> metricProvider)
			: base(name, eventSource)
		{
		}
	}
}
