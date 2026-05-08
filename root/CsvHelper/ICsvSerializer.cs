using System;
using CsvHelper.Configuration;

namespace CsvHelper
{
	// Token: 0x02000015 RID: 21
	public interface ICsvSerializer : IDisposable
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F4 RID: 244
		CsvConfiguration Configuration { get; }

		// Token: 0x060000F5 RID: 245
		void Write(string[] record);
	}
}
