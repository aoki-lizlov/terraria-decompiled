using System;
using CsvHelper.Configuration;

namespace CsvHelper
{
	// Token: 0x02000012 RID: 18
	public interface ICsvParser : IDisposable
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C0 RID: 192
		CsvConfiguration Configuration { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C1 RID: 193
		int FieldCount { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C2 RID: 194
		long CharPosition { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C3 RID: 195
		long BytePosition { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C4 RID: 196
		int Row { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C5 RID: 197
		string RawRecord { get; }

		// Token: 0x060000C6 RID: 198
		string[] Read();
	}
}
