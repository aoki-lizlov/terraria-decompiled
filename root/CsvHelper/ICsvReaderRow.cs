using System;
using System.Collections.Generic;
using CsvHelper.TypeConversion;

namespace CsvHelper
{
	// Token: 0x02000014 RID: 20
	public interface ICsvReaderRow
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CC RID: 204
		string[] CurrentRecord { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CD RID: 205
		int Row { get; }

		// Token: 0x17000024 RID: 36
		string this[int index] { get; }

		// Token: 0x17000025 RID: 37
		string this[string name] { get; }

		// Token: 0x17000026 RID: 38
		string this[string name, int index] { get; }

		// Token: 0x060000D1 RID: 209
		string GetField(int index);

		// Token: 0x060000D2 RID: 210
		string GetField(string name);

		// Token: 0x060000D3 RID: 211
		string GetField(string name, int index);

		// Token: 0x060000D4 RID: 212
		object GetField(Type type, int index);

		// Token: 0x060000D5 RID: 213
		object GetField(Type type, string name);

		// Token: 0x060000D6 RID: 214
		object GetField(Type type, string name, int index);

		// Token: 0x060000D7 RID: 215
		object GetField(Type type, int index, ITypeConverter converter);

		// Token: 0x060000D8 RID: 216
		object GetField(Type type, string name, ITypeConverter converter);

		// Token: 0x060000D9 RID: 217
		object GetField(Type type, string name, int index, ITypeConverter converter);

		// Token: 0x060000DA RID: 218
		T GetField<T>(int index);

		// Token: 0x060000DB RID: 219
		T GetField<T>(string name);

		// Token: 0x060000DC RID: 220
		T GetField<T>(string name, int index);

		// Token: 0x060000DD RID: 221
		T GetField<T>(int index, ITypeConverter converter);

		// Token: 0x060000DE RID: 222
		T GetField<T>(string name, ITypeConverter converter);

		// Token: 0x060000DF RID: 223
		T GetField<T>(string name, int index, ITypeConverter converter);

		// Token: 0x060000E0 RID: 224
		T GetField<T, TConverter>(int index) where TConverter : ITypeConverter;

		// Token: 0x060000E1 RID: 225
		T GetField<T, TConverter>(string name) where TConverter : ITypeConverter;

		// Token: 0x060000E2 RID: 226
		T GetField<T, TConverter>(string name, int index) where TConverter : ITypeConverter;

		// Token: 0x060000E3 RID: 227
		bool TryGetField<T>(int index, out T field);

		// Token: 0x060000E4 RID: 228
		bool TryGetField<T>(string name, out T field);

		// Token: 0x060000E5 RID: 229
		bool TryGetField<T>(string name, int index, out T field);

		// Token: 0x060000E6 RID: 230
		bool TryGetField<T>(int index, ITypeConverter converter, out T field);

		// Token: 0x060000E7 RID: 231
		bool TryGetField<T>(string name, ITypeConverter converter, out T field);

		// Token: 0x060000E8 RID: 232
		bool TryGetField<T>(string name, int index, ITypeConverter converter, out T field);

		// Token: 0x060000E9 RID: 233
		bool TryGetField<T, TConverter>(int index, out T field) where TConverter : ITypeConverter;

		// Token: 0x060000EA RID: 234
		bool TryGetField<T, TConverter>(string name, out T field) where TConverter : ITypeConverter;

		// Token: 0x060000EB RID: 235
		bool TryGetField<T, TConverter>(string name, int index, out T field) where TConverter : ITypeConverter;

		// Token: 0x060000EC RID: 236
		bool IsRecordEmpty();

		// Token: 0x060000ED RID: 237
		T GetRecord<T>();

		// Token: 0x060000EE RID: 238
		object GetRecord(Type type);

		// Token: 0x060000EF RID: 239
		IEnumerable<T> GetRecords<T>();

		// Token: 0x060000F0 RID: 240
		IEnumerable<object> GetRecords(Type type);

		// Token: 0x060000F1 RID: 241
		void ClearRecordCache<T>();

		// Token: 0x060000F2 RID: 242
		void ClearRecordCache(Type type);

		// Token: 0x060000F3 RID: 243
		void ClearRecordCache();
	}
}
