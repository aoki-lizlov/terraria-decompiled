using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007C RID: 124
	public class MemoryTraceWriter : ITraceWriter
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00018233 File Offset: 0x00016433
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0001823B File Offset: 0x0001643B
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

		// Token: 0x060005B5 RID: 1461 RVA: 0x00018244 File Offset: 0x00016444
		public MemoryTraceWriter()
		{
			this.LevelFilter = 4;
			this._traceMessages = new Queue<string>();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00018260 File Offset: 0x00016460
		public void Trace(TraceLevel level, string message, Exception ex)
		{
			if (this._traceMessages.Count >= 1000)
			{
				this._traceMessages.Dequeue();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture));
			stringBuilder.Append(" ");
			stringBuilder.Append(level.ToString("g"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			this._traceMessages.Enqueue(stringBuilder.ToString());
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000182F7 File Offset: 0x000164F7
		public IEnumerable<string> GetTraceMessages()
		{
			return this._traceMessages;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00018300 File Offset: 0x00016500
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this._traceMessages)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000273 RID: 627
		private readonly Queue<string> _traceMessages;

		// Token: 0x04000274 RID: 628
		[CompilerGenerated]
		private TraceLevel <LevelFilter>k__BackingField;
	}
}
