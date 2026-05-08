using System;
using CsvHelper.Configuration;

namespace CsvHelper
{
	// Token: 0x02000013 RID: 19
	public interface ICsvReader : ICsvReaderRow, IDisposable
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C7 RID: 199
		CsvConfiguration Configuration { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C8 RID: 200
		ICsvParser Parser { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C9 RID: 201
		string[] FieldHeaders { get; }

		// Token: 0x060000CA RID: 202
		bool ReadHeader();

		// Token: 0x060000CB RID: 203
		bool Read();
	}
}
