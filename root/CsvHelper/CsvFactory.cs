using System;
using System.IO;
using CsvHelper.Configuration;

namespace CsvHelper
{
	// Token: 0x02000006 RID: 6
	public class CsvFactory : ICsvFactory
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002218 File Offset: 0x00000418
		public virtual ICsvParser CreateParser(TextReader reader, CsvConfiguration configuration)
		{
			return new CsvParser(reader, configuration);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002221 File Offset: 0x00000421
		public virtual ICsvParser CreateParser(TextReader reader)
		{
			return new CsvParser(reader);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002229 File Offset: 0x00000429
		public virtual ICsvReader CreateReader(TextReader reader, CsvConfiguration configuration)
		{
			return new CsvReader(reader, configuration);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002232 File Offset: 0x00000432
		public virtual ICsvReader CreateReader(TextReader reader)
		{
			return new CsvReader(reader);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000223A File Offset: 0x0000043A
		public virtual ICsvReader CreateReader(ICsvParser parser)
		{
			return new CsvReader(parser);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002242 File Offset: 0x00000442
		public virtual ICsvWriter CreateWriter(TextWriter writer, CsvConfiguration configuration)
		{
			return new CsvWriter(writer, configuration);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000224B File Offset: 0x0000044B
		public virtual ICsvWriter CreateWriter(TextWriter writer)
		{
			return new CsvWriter(writer);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002253 File Offset: 0x00000453
		public CsvFactory()
		{
		}
	}
}
