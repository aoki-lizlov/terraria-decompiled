using System;
using System.Collections;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CsvHelper
{
	// Token: 0x02000016 RID: 22
	public interface ICsvWriter : IDisposable
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F6 RID: 246
		CsvConfiguration Configuration { get; }

		// Token: 0x060000F7 RID: 247
		void WriteField(string field);

		// Token: 0x060000F8 RID: 248
		void WriteField(string field, bool shouldQuote);

		// Token: 0x060000F9 RID: 249
		void WriteField<T>(T field);

		// Token: 0x060000FA RID: 250
		void WriteField<T>(T field, ITypeConverter converter);

		// Token: 0x060000FB RID: 251
		void WriteField<T, TConverter>(T field);

		// Token: 0x060000FC RID: 252
		[Obsolete("This method is deprecated and will be removed in the next major release. Use WriteField<T>( T field ) instead.", false)]
		void WriteField(Type type, object field);

		// Token: 0x060000FD RID: 253
		[Obsolete("This method is deprecated and will be removed in the next major release. Use WriteField<T>( T field, ITypeConverter converter ) instead.", false)]
		void WriteField(Type type, object field, ITypeConverter converter);

		// Token: 0x060000FE RID: 254
		void NextRecord();

		// Token: 0x060000FF RID: 255
		void WriteExcelSeparator();

		// Token: 0x06000100 RID: 256
		void WriteHeader<T>();

		// Token: 0x06000101 RID: 257
		void WriteHeader(Type type);

		// Token: 0x06000102 RID: 258
		void WriteRecord<T>(T record);

		// Token: 0x06000103 RID: 259
		[Obsolete("This method is deprecated and will be removed in the next major release. Use WriteRecord<T>( T record ) instead.", false)]
		void WriteRecord(Type type, object record);

		// Token: 0x06000104 RID: 260
		void WriteRecords(IEnumerable records);

		// Token: 0x06000105 RID: 261
		void ClearRecordCache<T>();

		// Token: 0x06000106 RID: 262
		void ClearRecordCache(Type type);

		// Token: 0x06000107 RID: 263
		void ClearRecordCache();
	}
}
