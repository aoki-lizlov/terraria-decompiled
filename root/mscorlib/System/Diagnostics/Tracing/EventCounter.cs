using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A47 RID: 2631
	public class EventCounter : DiagnosticCounter
	{
		// Token: 0x060060C3 RID: 24771 RVA: 0x0014D474 File Offset: 0x0014B674
		public EventCounter(string name, EventSource eventSource)
			: base(name, eventSource)
		{
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x00004088 File Offset: 0x00002288
		public void WriteMetric(float value)
		{
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x00004088 File Offset: 0x00002288
		public void WriteMetric(double value)
		{
		}
	}
}
