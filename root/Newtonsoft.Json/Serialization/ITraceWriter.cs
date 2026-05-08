using System;
using System.Diagnostics;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007A RID: 122
	public interface ITraceWriter
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005A5 RID: 1445
		TraceLevel LevelFilter { get; }

		// Token: 0x060005A6 RID: 1446
		void Trace(TraceLevel level, string message, Exception ex);
	}
}
