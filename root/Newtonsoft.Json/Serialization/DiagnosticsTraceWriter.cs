using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000076 RID: 118
	public class DiagnosticsTraceWriter : ITraceWriter
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00017F32 File Offset: 0x00016132
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00017F3A File Offset: 0x0001613A
		public TraceLevel LevelFilter
		{
			[CompilerGenerated]
			get
			{
				return this.<LevelFilter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LevelFilter>k__BackingField = value;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00017F43 File Offset: 0x00016143
		private TraceEventType GetTraceEventType(TraceLevel level)
		{
			switch (level)
			{
			case 1:
				return 2;
			case 2:
				return 4;
			case 3:
				return 8;
			case 4:
				return 16;
			default:
				throw new ArgumentOutOfRangeException("level");
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00017F74 File Offset: 0x00016174
		public void Trace(TraceLevel level, string message, Exception ex)
		{
			if (level == null)
			{
				return;
			}
			TraceEventCache traceEventCache = new TraceEventCache();
			TraceEventType traceEventType = this.GetTraceEventType(level);
			foreach (object obj in global::System.Diagnostics.Trace.Listeners)
			{
				TraceListener traceListener = (TraceListener)obj;
				if (!traceListener.IsThreadSafe)
				{
					TraceListener traceListener2 = traceListener;
					lock (traceListener2)
					{
						traceListener.TraceEvent(traceEventCache, "Newtonsoft.Json", traceEventType, 0, message);
						goto IL_006E;
					}
					goto IL_005F;
				}
				goto IL_005F;
				IL_006E:
				if (global::System.Diagnostics.Trace.AutoFlush)
				{
					traceListener.Flush();
					continue;
				}
				continue;
				IL_005F:
				traceListener.TraceEvent(traceEventCache, "Newtonsoft.Json", traceEventType, 0, message);
				goto IL_006E;
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00008020 File Offset: 0x00006220
		public DiagnosticsTraceWriter()
		{
		}

		// Token: 0x04000269 RID: 617
		[CompilerGenerated]
		private TraceLevel <LevelFilter>k__BackingField;
	}
}
