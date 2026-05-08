using System;
using System.IO;
using CsvHelper.Configuration;

namespace CsvHelper
{
	// Token: 0x02000011 RID: 17
	public interface ICsvFactory
	{
		// Token: 0x060000B9 RID: 185
		ICsvParser CreateParser(TextReader reader, CsvConfiguration configuration);

		// Token: 0x060000BA RID: 186
		ICsvParser CreateParser(TextReader reader);

		// Token: 0x060000BB RID: 187
		ICsvReader CreateReader(TextReader reader, CsvConfiguration configuration);

		// Token: 0x060000BC RID: 188
		ICsvReader CreateReader(TextReader reader);

		// Token: 0x060000BD RID: 189
		ICsvReader CreateReader(ICsvParser parser);

		// Token: 0x060000BE RID: 190
		ICsvWriter CreateWriter(TextWriter writer, CsvConfiguration configuration);

		// Token: 0x060000BF RID: 191
		ICsvWriter CreateWriter(TextWriter writer);
	}
}
