using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.CSharp.RuntimeBinder;

namespace CsvHelper
{
	// Token: 0x0200000B RID: 11
	public class CsvReader : ICsvReader, ICsvReaderRow, IDisposable
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002FE5 File Offset: 0x000011E5
		public virtual CsvConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002FED File Offset: 0x000011ED
		public virtual ICsvParser Parser
		{
			get
			{
				return this.parser;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002FF5 File Offset: 0x000011F5
		public virtual string[] FieldHeaders
		{
			get
			{
				this.CheckDisposed();
				if (this.headerRecord == null)
				{
					throw new CsvReaderException("You must call ReadHeader or Read before accessing the field headers.");
				}
				return this.headerRecord;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003016 File Offset: 0x00001216
		public virtual string[] CurrentRecord
		{
			get
			{
				this.CheckDisposed();
				this.CheckHasBeenRead();
				return this.currentRecord;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000302A File Offset: 0x0000122A
		public int Row
		{
			get
			{
				this.CheckDisposed();
				this.CheckHasBeenRead();
				return this.parser.Row;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003043 File Offset: 0x00001243
		public CsvReader(TextReader reader)
			: this(reader, new CsvConfiguration())
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003054 File Offset: 0x00001254
		public CsvReader(TextReader reader, CsvConfiguration configuration)
		{
			this.currentIndex = -1;
			this.namedIndexes = new Dictionary<string, List<int>>();
			this.recordFuncs = new Dictionary<Type, Delegate>();
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.parser = new CsvParser(reader, configuration);
			this.configuration = configuration;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000030B4 File Offset: 0x000012B4
		public CsvReader(ICsvParser parser)
		{
			this.currentIndex = -1;
			this.namedIndexes = new Dictionary<string, List<int>>();
			this.recordFuncs = new Dictionary<Type, Delegate>();
			base..ctor();
			if (parser == null)
			{
				throw new ArgumentNullException("parser");
			}
			if (parser.Configuration == null)
			{
				throw new CsvConfigurationException("The given parser has no configuration.");
			}
			this.parser = parser;
			this.configuration = parser.Configuration;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003118 File Offset: 0x00001318
		public virtual bool ReadHeader()
		{
			this.CheckDisposed();
			if (this.doneReading)
			{
				throw new CsvReaderException("The reader has already exhausted all records. If you would like to iterate the records more than once, store the records in memory. i.e. Use CsvReader.GetRecords<T>().ToList()");
			}
			if (!this.configuration.HasHeaderRecord)
			{
				throw new CsvReaderException("Configuration.HasHeaderRecord is false.");
			}
			if (this.headerRecord != null)
			{
				throw new CsvReaderException("Header record has already been read.");
			}
			do
			{
				this.currentRecord = this.parser.Read();
			}
			while (this.ShouldSkipRecord());
			this.headerRecord = this.currentRecord;
			this.currentRecord = null;
			this.ParseNamedIndexes();
			return this.headerRecord != null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000031A4 File Offset: 0x000013A4
		public virtual bool Read()
		{
			this.CheckDisposed();
			if (this.doneReading)
			{
				throw new CsvReaderException("The reader has already exhausted all records. If you would like to iterate the records more than once, store the records in memory. i.e. Use CsvReader.GetRecords<T>().ToList()");
			}
			if (this.configuration.HasHeaderRecord && this.headerRecord == null)
			{
				this.ReadHeader();
			}
			do
			{
				this.currentRecord = this.parser.Read();
			}
			while (this.ShouldSkipRecord());
			this.currentIndex = -1;
			this.hasBeenRead = true;
			if (this.currentRecord == null)
			{
				this.doneReading = true;
			}
			return this.currentRecord != null;
		}

		// Token: 0x17000014 RID: 20
		public virtual string this[int index]
		{
			get
			{
				this.CheckDisposed();
				this.CheckHasBeenRead();
				return this.GetField(index);
			}
		}

		// Token: 0x17000015 RID: 21
		public virtual string this[string name]
		{
			get
			{
				this.CheckDisposed();
				this.CheckHasBeenRead();
				return this.GetField(name);
			}
		}

		// Token: 0x17000016 RID: 22
		public virtual string this[string name, int index]
		{
			get
			{
				this.CheckDisposed();
				this.CheckHasBeenRead();
				return this.GetField(name, index);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003268 File Offset: 0x00001468
		public virtual string GetField(int index)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			this.currentIndex = index;
			if (index < this.currentRecord.Length)
			{
				string text = this.currentRecord[index];
				if (this.configuration.TrimFields && text != null)
				{
					text = text.Trim();
				}
				return text;
			}
			if (this.configuration.WillThrowOnMissingField)
			{
				CsvMissingFieldException ex = new CsvMissingFieldException(string.Format("Field at index '{0}' does not exist.", index));
				ExceptionHelper.AddExceptionDataMessage(ex, this.Parser, typeof(string), this.namedIndexes, new int?(index), this.currentRecord);
				throw ex;
			}
			return null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003304 File Offset: 0x00001504
		public virtual string GetField(string name)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, 0, false);
			if (fieldIndex < 0)
			{
				return null;
			}
			return this.GetField(fieldIndex);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003334 File Offset: 0x00001534
		public virtual string GetField(string name, int index)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, index, false);
			if (fieldIndex < 0)
			{
				return null;
			}
			return this.GetField(fieldIndex);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003364 File Offset: 0x00001564
		[Obsolete("This method is deprecated and will be removed in the next major release. Use GetField( Type, int, ITypeConverter ) instead.", false)]
		public virtual object GetField(int index, ITypeConverter converter)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			TypeConverterOptions typeConverterOptions = new TypeConverterOptions
			{
				CultureInfo = this.configuration.CultureInfo
			};
			string field = this.GetField(index);
			return converter.ConvertFromString(typeConverterOptions, field);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000033A4 File Offset: 0x000015A4
		[Obsolete("This method is deprecated and will be removed in the next major release. Use GetField( Type, string, ITypeConverter ) instead.", false)]
		public virtual object GetField(string name, ITypeConverter converter)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, 0, false);
			return this.GetField(fieldIndex, converter);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000033D0 File Offset: 0x000015D0
		[Obsolete("This method is deprecated and will be removed in the next major release. Use GetField( Type, string, int, ITypeConverter ) instead.", false)]
		public virtual object GetField(string name, int index, ITypeConverter converter)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, index, false);
			return this.GetField(fieldIndex, converter);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000033FC File Offset: 0x000015FC
		public virtual object GetField(Type type, int index)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			ITypeConverter converter = TypeConverterFactory.GetConverter(type);
			return this.GetField(type, index, converter);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003428 File Offset: 0x00001628
		public virtual object GetField(Type type, string name)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			ITypeConverter converter = TypeConverterFactory.GetConverter(type);
			return this.GetField(type, name, converter);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003454 File Offset: 0x00001654
		public virtual object GetField(Type type, string name, int index)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			ITypeConverter converter = TypeConverterFactory.GetConverter(type);
			return this.GetField(type, name, index, converter);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003480 File Offset: 0x00001680
		public virtual object GetField(Type type, int index, ITypeConverter converter)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			TypeConverterOptions options = TypeConverterOptionsFactory.GetOptions(type);
			if (options.CultureInfo == null)
			{
				options.CultureInfo = this.configuration.CultureInfo;
			}
			string field = this.GetField(index);
			return converter.ConvertFromString(options, field);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000034CC File Offset: 0x000016CC
		public virtual object GetField(Type type, string name, ITypeConverter converter)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, 0, false);
			return this.GetField(type, fieldIndex, converter);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000034F8 File Offset: 0x000016F8
		public virtual object GetField(Type type, string name, int index, ITypeConverter converter)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, index, false);
			return this.GetField(type, fieldIndex, converter);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003528 File Offset: 0x00001728
		public virtual T GetField<T>(int index)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			ITypeConverter converter = TypeConverterFactory.GetConverter<T>();
			return this.GetField<T>(index, converter);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003550 File Offset: 0x00001750
		public virtual T GetField<T>(string name)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			ITypeConverter converter = TypeConverterFactory.GetConverter<T>();
			return this.GetField<T>(name, converter);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003578 File Offset: 0x00001778
		public virtual T GetField<T>(string name, int index)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			ITypeConverter converter = TypeConverterFactory.GetConverter<T>();
			return this.GetField<T>(name, index, converter);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000035A0 File Offset: 0x000017A0
		public virtual T GetField<T>(int index, ITypeConverter converter)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			if (index < this.currentRecord.Length && index >= 0)
			{
				return (T)((object)this.GetField(typeof(T), index, converter));
			}
			if (this.configuration.WillThrowOnMissingField)
			{
				CsvMissingFieldException ex = new CsvMissingFieldException(string.Format("Field at index '{0}' does not exist.", index));
				ExceptionHelper.AddExceptionDataMessage(ex, this.Parser, typeof(T), this.namedIndexes, new int?(index), this.currentRecord);
				throw ex;
			}
			return default(T);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003634 File Offset: 0x00001834
		public virtual T GetField<T>(string name, ITypeConverter converter)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, 0, false);
			return this.GetField<T>(fieldIndex, converter);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003660 File Offset: 0x00001860
		public virtual T GetField<T>(string name, int index, ITypeConverter converter)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, index, false);
			return this.GetField<T>(fieldIndex, converter);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000368C File Offset: 0x0000188C
		public virtual T GetField<T, TConverter>(int index) where TConverter : ITypeConverter
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			TConverter tconverter = ReflectionHelper.CreateInstance<TConverter>(new object[0]);
			return this.GetField<T>(index, tconverter);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000036C0 File Offset: 0x000018C0
		public virtual T GetField<T, TConverter>(string name) where TConverter : ITypeConverter
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			TConverter tconverter = ReflectionHelper.CreateInstance<TConverter>(new object[0]);
			return this.GetField<T>(name, tconverter);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000036F4 File Offset: 0x000018F4
		public virtual T GetField<T, TConverter>(string name, int index) where TConverter : ITypeConverter
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			TConverter tconverter = ReflectionHelper.CreateInstance<TConverter>(new object[0]);
			return this.GetField<T>(name, index, tconverter);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003728 File Offset: 0x00001928
		public virtual bool TryGetField<T>(int index, out T field)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			ITypeConverter converter = TypeConverterFactory.GetConverter<T>();
			return this.TryGetField<T>(index, converter, out field);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003750 File Offset: 0x00001950
		public virtual bool TryGetField<T>(string name, out T field)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			ITypeConverter converter = TypeConverterFactory.GetConverter<T>();
			return this.TryGetField<T>(name, converter, out field);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003778 File Offset: 0x00001978
		public virtual bool TryGetField<T>(string name, int index, out T field)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			ITypeConverter converter = TypeConverterFactory.GetConverter<T>();
			return this.TryGetField<T>(name, index, converter, out field);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000037A4 File Offset: 0x000019A4
		public virtual bool TryGetField<T>(int index, ITypeConverter converter, out T field)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			if (converter is DateTimeConverter && StringHelper.IsNullOrWhiteSpace(this.currentRecord[index]))
			{
				field = default(T);
				return false;
			}
			bool flag;
			try
			{
				field = this.GetField<T>(index, converter);
				flag = true;
			}
			catch
			{
				field = default(T);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003810 File Offset: 0x00001A10
		public virtual bool TryGetField<T>(string name, ITypeConverter converter, out T field)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, 0, true);
			if (fieldIndex == -1)
			{
				field = default(T);
				return false;
			}
			return this.TryGetField<T>(fieldIndex, converter, out field);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000384C File Offset: 0x00001A4C
		public virtual bool TryGetField<T>(string name, int index, ITypeConverter converter, out T field)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			int fieldIndex = this.GetFieldIndex(name, index, true);
			if (fieldIndex == -1)
			{
				field = default(T);
				return false;
			}
			return this.TryGetField<T>(fieldIndex, converter, out field);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003888 File Offset: 0x00001A88
		public virtual bool TryGetField<T, TConverter>(int index, out T field) where TConverter : ITypeConverter
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			TConverter tconverter = ReflectionHelper.CreateInstance<TConverter>(new object[0]);
			return this.TryGetField<T>(index, tconverter, out field);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000038BC File Offset: 0x00001ABC
		public virtual bool TryGetField<T, TConverter>(string name, out T field) where TConverter : ITypeConverter
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			TConverter tconverter = ReflectionHelper.CreateInstance<TConverter>(new object[0]);
			return this.TryGetField<T>(name, tconverter, out field);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000038F0 File Offset: 0x00001AF0
		public virtual bool TryGetField<T, TConverter>(string name, int index, out T field) where TConverter : ITypeConverter
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			TConverter tconverter = ReflectionHelper.CreateInstance<TConverter>(new object[0]);
			return this.TryGetField<T>(name, index, tconverter, out field);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003924 File Offset: 0x00001B24
		public virtual bool IsRecordEmpty()
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			return this.IsRecordEmpty(true);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000393C File Offset: 0x00001B3C
		public virtual T GetRecord<T>()
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			T t;
			try
			{
				t = this.CreateRecord<T>();
			}
			catch (Exception ex)
			{
				ExceptionHelper.AddExceptionDataMessage(ex, this.parser, typeof(T), this.namedIndexes, new int?(this.currentIndex), this.currentRecord);
				throw;
			}
			return t;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000039A0 File Offset: 0x00001BA0
		public virtual object GetRecord(Type type)
		{
			this.CheckDisposed();
			this.CheckHasBeenRead();
			object obj;
			try
			{
				obj = this.CreateRecord(type);
			}
			catch (Exception ex)
			{
				ExceptionHelper.AddExceptionDataMessage(ex, this.parser, type, this.namedIndexes, new int?(this.currentIndex), this.currentRecord);
				throw;
			}
			return obj;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000039FC File Offset: 0x00001BFC
		public virtual IEnumerable<T> GetRecords<T>()
		{
			this.CheckDisposed();
			while (this.Read())
			{
				T record;
				try
				{
					record = this.CreateRecord<T>();
				}
				catch (Exception ex)
				{
					ExceptionHelper.AddExceptionDataMessage(ex, this.parser, typeof(T), this.namedIndexes, new int?(this.currentIndex), this.currentRecord);
					if (this.configuration.IgnoreReadingExceptions)
					{
						if (this.configuration.ReadingExceptionCallback != null)
						{
							this.configuration.ReadingExceptionCallback.Invoke(ex, this);
						}
						continue;
					}
					throw;
				}
				yield return record;
				record = default(T);
			}
			yield break;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A0C File Offset: 0x00001C0C
		public virtual IEnumerable<object> GetRecords(Type type)
		{
			this.CheckDisposed();
			while (this.Read())
			{
				object record;
				try
				{
					record = this.CreateRecord(type);
				}
				catch (Exception ex)
				{
					ExceptionHelper.AddExceptionDataMessage(ex, this.parser, type, this.namedIndexes, new int?(this.currentIndex), this.currentRecord);
					if (this.configuration.IgnoreReadingExceptions)
					{
						if (this.configuration.ReadingExceptionCallback != null)
						{
							this.configuration.ReadingExceptionCallback.Invoke(ex, this);
						}
						continue;
					}
					throw;
				}
				yield return record;
				record = null;
			}
			yield break;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003A23 File Offset: 0x00001C23
		public virtual void ClearRecordCache<T>()
		{
			this.CheckDisposed();
			this.ClearRecordCache(typeof(T));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A3B File Offset: 0x00001C3B
		public virtual void ClearRecordCache(Type type)
		{
			this.CheckDisposed();
			this.recordFuncs.Remove(type);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003A50 File Offset: 0x00001C50
		public virtual void ClearRecordCache()
		{
			this.CheckDisposed();
			this.recordFuncs.Clear();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003A63 File Offset: 0x00001C63
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003A72 File Offset: 0x00001C72
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing && this.parser != null)
			{
				this.parser.Dispose();
			}
			this.disposed = true;
			this.parser = null;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003AA1 File Offset: 0x00001CA1
		protected virtual void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003ABC File Offset: 0x00001CBC
		protected virtual void CheckHasBeenRead()
		{
			if (!this.hasBeenRead)
			{
				throw new CsvReaderException("You must call read on the reader before accessing its data.");
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003AD1 File Offset: 0x00001CD1
		protected virtual bool IsRecordEmpty(bool checkHasBeenRead)
		{
			this.CheckDisposed();
			if (checkHasBeenRead)
			{
				this.CheckHasBeenRead();
			}
			return this.currentRecord != null && Enumerable.All<string>(this.currentRecord, this.GetEmtpyStringMethod());
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003AFD File Offset: 0x00001CFD
		protected virtual Func<string, bool> GetEmtpyStringMethod()
		{
			if (!this.Configuration.TrimFields)
			{
				return new Func<string, bool>(string.IsNullOrEmpty);
			}
			return new Func<string, bool>(string.IsNullOrWhiteSpace);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003B25 File Offset: 0x00001D25
		protected virtual int GetFieldIndex(string name, int index = 0, bool isTryGet = false)
		{
			return this.GetFieldIndex(new string[] { name }, index, isTryGet);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003B3C File Offset: 0x00001D3C
		protected virtual int GetFieldIndex(string[] names, int index = 0, bool isTryGet = false)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (!this.configuration.HasHeaderRecord)
			{
				throw new CsvReaderException("There is no header record to determine the index by name.");
			}
			CompareOptions compareOptions = ((!this.Configuration.IsHeaderCaseSensitive) ? 1 : 0);
			string text = null;
			foreach (KeyValuePair<string, List<int>> keyValuePair in this.namedIndexes)
			{
				string text2 = keyValuePair.Key;
				if (this.configuration.IgnoreHeaderWhiteSpace)
				{
					text2 = Regex.Replace(text2, "\\s", string.Empty);
				}
				else if (this.configuration.TrimHeaders && text2 != null)
				{
					text2 = text2.Trim();
				}
				foreach (string text3 in names)
				{
					if (this.Configuration.CultureInfo.CompareInfo.Compare(text2, text3, compareOptions) == 0)
					{
						text = keyValuePair.Key;
					}
				}
			}
			if (text != null)
			{
				return this.namedIndexes[text][index];
			}
			if (this.configuration.WillThrowOnMissingField && !isTryGet)
			{
				string text4 = string.Format("'{0}'", string.Join("', '", names));
				CsvMissingFieldException ex = new CsvMissingFieldException(string.Format("Fields {0} do not exist in the CSV file.", text4));
				ExceptionHelper.AddExceptionDataMessage(ex, this.Parser, null, this.namedIndexes, new int?(this.currentIndex), this.currentRecord);
				throw ex;
			}
			return -1;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003CC4 File Offset: 0x00001EC4
		protected virtual void ParseNamedIndexes()
		{
			if (this.headerRecord == null)
			{
				throw new CsvReaderException("No header record was found.");
			}
			for (int i = 0; i < this.headerRecord.Length; i++)
			{
				string text = this.headerRecord[i];
				if (!this.Configuration.IsHeaderCaseSensitive)
				{
					text = text.ToLower();
				}
				if (this.namedIndexes.ContainsKey(text))
				{
					this.namedIndexes[text].Add(i);
				}
				else
				{
					Dictionary<string, List<int>> dictionary = this.namedIndexes;
					string text2 = text;
					List<int> list = new List<int>();
					list.Add(i);
					dictionary[text2] = list;
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003D50 File Offset: 0x00001F50
		protected virtual bool ShouldSkipRecord()
		{
			this.CheckDisposed();
			if (this.currentRecord == null)
			{
				return false;
			}
			if (this.configuration.ShouldSkipRecord == null)
			{
				return this.configuration.SkipEmptyRecords && this.IsRecordEmpty(false);
			}
			return this.configuration.ShouldSkipRecord.Invoke(this.currentRecord);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003DA8 File Offset: 0x00001FA8
		protected virtual T CreateRecord<T>()
		{
			if (typeof(T) == typeof(object))
			{
				if (CsvReader.<>o__80<T>.<>p__0 == null)
				{
					CsvReader.<>o__80<T>.<>p__0 = CallSite<Func<CallSite, object, T>>.Create(Binder.Convert(0, typeof(T), typeof(CsvReader)));
				}
				return CsvReader.<>o__80<T>.<>p__0.Target.Invoke(CsvReader.<>o__80<T>.<>p__0, this.CreateDynamic());
			}
			return this.GetReadRecordFunc<T>().Invoke();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003E24 File Offset: 0x00002024
		protected virtual object CreateRecord(Type type)
		{
			if (type == typeof(object))
			{
				return this.CreateDynamic();
			}
			object obj;
			try
			{
				obj = this.GetReadRecordFunc(type).DynamicInvoke(new object[0]);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return obj;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003E78 File Offset: 0x00002078
		protected virtual Func<T> GetReadRecordFunc<T>()
		{
			Type typeFromHandle = typeof(T);
			this.CreateReadRecordFunc(typeFromHandle);
			return (Func<T>)this.recordFuncs[typeFromHandle];
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003EA8 File Offset: 0x000020A8
		protected virtual Delegate GetReadRecordFunc(Type recordType)
		{
			this.CreateReadRecordFunc(recordType);
			return this.recordFuncs[recordType];
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003EC0 File Offset: 0x000020C0
		protected virtual void CreateReadRecordFunc(Type recordType)
		{
			if (this.recordFuncs.ContainsKey(recordType))
			{
				return;
			}
			if (this.configuration.Maps[recordType] == null)
			{
				this.configuration.Maps.Add(this.configuration.AutoMap(recordType));
			}
			if (recordType.GetTypeInfo().IsPrimitive)
			{
				this.CreateFuncForPrimitive(recordType);
				return;
			}
			this.CreateFuncForObject(recordType);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003F28 File Offset: 0x00002128
		protected virtual void CreateFuncForObject(Type recordType)
		{
			List<MemberBinding> list = new List<MemberBinding>();
			this.CreatePropertyBindingsForMapping(this.configuration.Maps[recordType], recordType, list);
			if (list.Count == 0)
			{
				throw new CsvReaderException(string.Format(string.Format("No properties are mapped for type '{0}'.", recordType.FullName), new object[0]));
			}
			MemberInitExpression memberInitExpression = Expression.MemberInit(this.configuration.Maps[recordType].Constructor ?? Expression.New(recordType), list);
			Type type = typeof(Func).MakeGenericType(new Type[] { recordType });
			this.recordFuncs[recordType] = Expression.Lambda(type, memberInitExpression, new ParameterExpression[0]).Compile();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003FDC File Offset: 0x000021DC
		protected virtual void CreateFuncForPrimitive(Type recordType)
		{
			MethodInfo getMethod = typeof(ICsvReaderRow).GetProperty("Item", typeof(string), new Type[] { typeof(int) }).GetGetMethod();
			Expression expression = Expression.Call(Expression.Constant(this), getMethod, new Expression[] { Expression.Constant(0, typeof(int)) });
			object converter = TypeConverterFactory.GetConverter(recordType);
			TypeConverterOptions options = TypeConverterOptionsFactory.GetOptions(recordType);
			if (options.CultureInfo == null)
			{
				options.CultureInfo = this.configuration.CultureInfo;
			}
			expression = Expression.Call(Expression.Constant(converter), "ConvertFromString", null, new Expression[]
			{
				Expression.Constant(options),
				expression
			});
			expression = Expression.Convert(expression, recordType);
			Type type = typeof(Func).MakeGenericType(new Type[] { recordType });
			this.recordFuncs[recordType] = Expression.Lambda(type, expression, new ParameterExpression[0]).Compile();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000040D8 File Offset: 0x000022D8
		protected virtual void CreatePropertyBindingsForMapping(CsvClassMap mapping, Type recordType, List<MemberBinding> bindings)
		{
			this.AddPropertyBindings(mapping.PropertyMaps, bindings);
			foreach (CsvPropertyReferenceMap csvPropertyReferenceMap in mapping.ReferenceMaps)
			{
				if (this.CanRead(csvPropertyReferenceMap))
				{
					List<MemberBinding> list = new List<MemberBinding>();
					this.CreatePropertyBindingsForMapping(csvPropertyReferenceMap.Data.Mapping, csvPropertyReferenceMap.Data.Property.PropertyType, list);
					MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(csvPropertyReferenceMap.Data.Property.PropertyType), list);
					bindings.Add(Expression.Bind(csvPropertyReferenceMap.Data.Property, memberInitExpression));
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004198 File Offset: 0x00002398
		protected virtual void AddPropertyBindings(CsvPropertyMapCollection properties, List<MemberBinding> bindings)
		{
			foreach (CsvPropertyMap csvPropertyMap in properties)
			{
				if (csvPropertyMap.Data.ConvertExpression != null)
				{
					Expression expression = Expression.Invoke(csvPropertyMap.Data.ConvertExpression, new Expression[] { Expression.Constant(this) });
					expression = Expression.Convert(expression, csvPropertyMap.Data.Property.PropertyType);
					bindings.Add(Expression.Bind(csvPropertyMap.Data.Property, expression));
				}
				else if (this.CanRead(csvPropertyMap) && csvPropertyMap.Data.TypeConverter != null && csvPropertyMap.Data.TypeConverter.CanConvertFrom(typeof(string)))
				{
					int num = -1;
					if (csvPropertyMap.Data.IsNameSet)
					{
						num = this.GetFieldIndex(Enumerable.ToArray<string>(csvPropertyMap.Data.Names), csvPropertyMap.Data.NameIndex, false);
						if (num == -1)
						{
							continue;
						}
					}
					else if (csvPropertyMap.Data.IsIndexSet)
					{
						num = csvPropertyMap.Data.Index;
					}
					else if (this.configuration.HasHeaderRecord)
					{
						num = this.GetFieldIndex(Enumerable.ToArray<string>(csvPropertyMap.Data.Names), csvPropertyMap.Data.NameIndex, false);
						if (num == -1)
						{
							continue;
						}
					}
					else if (num == -1)
					{
						num = csvPropertyMap.Data.Index;
					}
					MethodInfo getMethod = typeof(ICsvReaderRow).GetProperty("Item", typeof(string), new Type[] { typeof(int) }).GetGetMethod();
					Expression expression2 = Expression.Call(Expression.Constant(this), getMethod, new Expression[] { Expression.Constant(num, typeof(int)) });
					ConstantExpression constantExpression = Expression.Constant(csvPropertyMap.Data.TypeConverter);
					if (csvPropertyMap.Data.TypeConverterOptions.CultureInfo == null)
					{
						csvPropertyMap.Data.TypeConverterOptions.CultureInfo = this.configuration.CultureInfo;
					}
					ConstantExpression constantExpression2 = Expression.Constant(TypeConverterOptions.Merge(new TypeConverterOptions[]
					{
						TypeConverterOptionsFactory.GetOptions(csvPropertyMap.Data.Property.PropertyType),
						csvPropertyMap.Data.TypeConverterOptions
					}));
					Expression expression3 = Expression.Call(constantExpression, "ConvertFromString", null, new Expression[] { constantExpression2, expression2 });
					expression3 = Expression.Convert(expression3, csvPropertyMap.Data.Property.PropertyType);
					if (csvPropertyMap.Data.IsDefaultSet)
					{
						Expression expression4 = Expression.Convert(Expression.Constant(csvPropertyMap.Data.Default), csvPropertyMap.Data.Property.PropertyType);
						expression2 = Expression.Condition(Expression.Equal(Expression.Convert(Expression.Coalesce(expression2, Expression.Constant(string.Empty)), typeof(string)), Expression.Constant(string.Empty, typeof(string))), expression4, expression3);
					}
					else
					{
						expression2 = expression3;
					}
					bindings.Add(Expression.Bind(csvPropertyMap.Data.Property, expression2));
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000044D4 File Offset: 0x000026D4
		protected virtual bool CanRead(CsvPropertyMap propertyMap)
		{
			return !propertyMap.Data.Ignore && (!(propertyMap.Data.Property.GetSetMethod() == null) || this.configuration.IgnorePrivateAccessor) && !(propertyMap.Data.Property.GetSetMethod(true) == null);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004530 File Offset: 0x00002730
		protected virtual bool CanRead(CsvPropertyReferenceMap propertyReferenceMap)
		{
			return (!(propertyReferenceMap.Data.Property.GetSetMethod() == null) || this.configuration.IgnorePrivateAccessor) && !(propertyReferenceMap.Data.Property.GetSetMethod(true) == null);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004580 File Offset: 0x00002780
		[return: Dynamic]
		protected virtual dynamic CreateDynamic()
		{
			ExpandoObject expandoObject = new ExpandoObject();
			IDictionary<string, object> dictionary = expandoObject;
			if (this.headerRecord != null)
			{
				for (int i = 0; i < this.headerRecord.Length; i++)
				{
					string text = this.headerRecord[i];
					string text2 = this.currentRecord[i];
					dictionary.Add(text, text2);
				}
			}
			else
			{
				for (int j = 0; j < this.currentRecord.Length; j++)
				{
					string text3 = "Field" + (j + 1);
					string text4 = this.currentRecord[j];
					dictionary.Add(text3, text4);
				}
			}
			return expandoObject;
		}

		// Token: 0x04000015 RID: 21
		private bool disposed;

		// Token: 0x04000016 RID: 22
		private bool hasBeenRead;

		// Token: 0x04000017 RID: 23
		private string[] currentRecord;

		// Token: 0x04000018 RID: 24
		private string[] headerRecord;

		// Token: 0x04000019 RID: 25
		private ICsvParser parser;

		// Token: 0x0400001A RID: 26
		private int currentIndex;

		// Token: 0x0400001B RID: 27
		private bool doneReading;

		// Token: 0x0400001C RID: 28
		private readonly Dictionary<string, List<int>> namedIndexes;

		// Token: 0x0400001D RID: 29
		private readonly Dictionary<Type, Delegate> recordFuncs;

		// Token: 0x0400001E RID: 30
		private readonly CsvConfiguration configuration;

		// Token: 0x0400001F RID: 31
		private const string DoneReadingExceptionMessage = "The reader has already exhausted all records. If you would like to iterate the records more than once, store the records in memory. i.e. Use CsvReader.GetRecords<T>().ToList()";

		// Token: 0x02000042 RID: 66
		[CompilerGenerated]
		private sealed class <GetRecords>d__65<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000245 RID: 581 RVA: 0x00007D0E File Offset: 0x00005F0E
			[DebuggerHidden]
			public <GetRecords>d__65(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000246 RID: 582 RVA: 0x000069AC File Offset: 0x00004BAC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000247 RID: 583 RVA: 0x00007D30 File Offset: 0x00005F30
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					record = default(T);
				}
				else
				{
					this.<>1__state = -1;
					this.CheckDisposed();
				}
				while (this.Read())
				{
					try
					{
						record = this.CreateRecord<T>();
					}
					catch (Exception ex)
					{
						ExceptionHelper.AddExceptionDataMessage(ex, this.parser, typeof(T), this.namedIndexes, new int?(this.currentIndex), this.currentRecord);
						if (this.configuration.IgnoreReadingExceptions)
						{
							if (this.configuration.ReadingExceptionCallback != null)
							{
								this.configuration.ReadingExceptionCallback.Invoke(ex, this);
							}
							continue;
						}
						throw;
					}
					this.<>2__current = record;
					this.<>1__state = 1;
					return true;
				}
				return false;
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000248 RID: 584 RVA: 0x00007E50 File Offset: 0x00006050
			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000249 RID: 585 RVA: 0x00007E58 File Offset: 0x00006058
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600024A RID: 586 RVA: 0x00007E5F File Offset: 0x0000605F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600024B RID: 587 RVA: 0x00007E6C File Offset: 0x0000606C
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				CsvReader.<GetRecords>d__65<T> <GetRecords>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<GetRecords>d__ = this;
				}
				else
				{
					<GetRecords>d__ = new CsvReader.<GetRecords>d__65<T>(0);
					<GetRecords>d__.<>4__this = this;
				}
				return <GetRecords>d__;
			}

			// Token: 0x0600024C RID: 588 RVA: 0x00007EB4 File Offset: 0x000060B4
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000079 RID: 121
			private int <>1__state;

			// Token: 0x0400007A RID: 122
			private T <>2__current;

			// Token: 0x0400007B RID: 123
			private int <>l__initialThreadId;

			// Token: 0x0400007C RID: 124
			public CsvReader <>4__this;

			// Token: 0x0400007D RID: 125
			private T <record>5__1;
		}

		// Token: 0x02000043 RID: 67
		[CompilerGenerated]
		private sealed class <GetRecords>d__66 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x0600024D RID: 589 RVA: 0x00007EBC File Offset: 0x000060BC
			[DebuggerHidden]
			public <GetRecords>d__66(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x0600024E RID: 590 RVA: 0x000069AC File Offset: 0x00004BAC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600024F RID: 591 RVA: 0x00007EDC File Offset: 0x000060DC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					record = null;
				}
				else
				{
					this.<>1__state = -1;
					this.CheckDisposed();
				}
				while (this.Read())
				{
					try
					{
						record = this.CreateRecord(type);
					}
					catch (Exception ex)
					{
						ExceptionHelper.AddExceptionDataMessage(ex, this.parser, type, this.namedIndexes, new int?(this.currentIndex), this.currentRecord);
						if (this.configuration.IgnoreReadingExceptions)
						{
							if (this.configuration.ReadingExceptionCallback != null)
							{
								this.configuration.ReadingExceptionCallback.Invoke(ex, this);
							}
							continue;
						}
						throw;
					}
					this.<>2__current = record;
					this.<>1__state = 1;
					return true;
				}
				return false;
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000250 RID: 592 RVA: 0x00007FF8 File Offset: 0x000061F8
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000251 RID: 593 RVA: 0x00007E58 File Offset: 0x00006058
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000252 RID: 594 RVA: 0x00007FF8 File Offset: 0x000061F8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000253 RID: 595 RVA: 0x00008000 File Offset: 0x00006200
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				CsvReader.<GetRecords>d__66 <GetRecords>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<GetRecords>d__ = this;
				}
				else
				{
					<GetRecords>d__ = new CsvReader.<GetRecords>d__66(0);
					<GetRecords>d__.<>4__this = this;
				}
				<GetRecords>d__.type = type;
				return <GetRecords>d__;
			}

			// Token: 0x06000254 RID: 596 RVA: 0x00008054 File Offset: 0x00006254
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x0400007E RID: 126
			private int <>1__state;

			// Token: 0x0400007F RID: 127
			private object <>2__current;

			// Token: 0x04000080 RID: 128
			private int <>l__initialThreadId;

			// Token: 0x04000081 RID: 129
			public CsvReader <>4__this;

			// Token: 0x04000082 RID: 130
			private Type type;

			// Token: 0x04000083 RID: 131
			public Type <>3__type;

			// Token: 0x04000084 RID: 132
			private object <record>5__1;
		}

		// Token: 0x02000044 RID: 68
		[CompilerGenerated]
		private static class <>o__80<T>
		{
			// Token: 0x04000085 RID: 133
			public static CallSite<Func<CallSite, object, T>> <>p__0;
		}
	}
}
