using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CsvHelper
{
	// Token: 0x0200000E RID: 14
	public class CsvWriter : ICsvWriter, IDisposable
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000046E2 File Offset: 0x000028E2
		public virtual CsvConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000046EA File Offset: 0x000028EA
		public CsvWriter(TextWriter writer)
			: this(writer, new CsvConfiguration())
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000046F8 File Offset: 0x000028F8
		public CsvWriter(TextWriter writer, CsvConfiguration configuration)
		{
			this.currentRecord = new List<string>();
			this.typeActions = new Dictionary<Type, Delegate>();
			base..ctor();
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.configuration = configuration;
			this.serializer = new CsvSerializer(writer, configuration);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004754 File Offset: 0x00002954
		public CsvWriter(ICsvSerializer serializer)
		{
			this.currentRecord = new List<string>();
			this.typeActions = new Dictionary<Type, Delegate>();
			base..ctor();
			if (serializer == null)
			{
				throw new ArgumentNullException("serializer");
			}
			if (serializer.Configuration == null)
			{
				throw new CsvConfigurationException("The given serializer has no configuration.");
			}
			this.serializer = serializer;
			this.configuration = serializer.Configuration;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000047B4 File Offset: 0x000029B4
		public virtual void WriteField(string field)
		{
			this.CheckDisposed();
			bool flag = this.configuration.QuoteAllFields;
			if (this.configuration.TrimFields)
			{
				field = field.Trim();
			}
			if (!this.configuration.QuoteNoFields && !string.IsNullOrEmpty(field))
			{
				bool flag2 = field.Contains(this.configuration.QuoteString);
				if (flag || flag2 || field.get_Chars(0) == ' ' || field.get_Chars(field.Length - 1) == ' ' || field.IndexOfAny(this.configuration.QuoteRequiredChars) > -1 || (this.configuration.Delimiter.Length > 1 && field.Contains(this.configuration.Delimiter)))
				{
					flag = true;
				}
			}
			this.WriteField(field, flag);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004878 File Offset: 0x00002A78
		public virtual void WriteField(string field, bool shouldQuote)
		{
			this.CheckDisposed();
			if (shouldQuote && !string.IsNullOrEmpty(field))
			{
				field = field.Replace(this.configuration.QuoteString, this.configuration.DoubleQuoteString);
			}
			if (this.configuration.UseExcelLeadingZerosFormatForNumerics && !string.IsNullOrEmpty(field) && field.get_Chars(0) == '0' && Enumerable.All<char>(field, new Func<char, bool>(char.IsDigit)))
			{
				field = "=" + this.configuration.Quote.ToString() + field + this.configuration.Quote.ToString();
			}
			else if (shouldQuote)
			{
				field = this.configuration.Quote.ToString() + field + this.configuration.Quote.ToString();
			}
			this.currentRecord.Add(field);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000495C File Offset: 0x00002B5C
		public virtual void WriteField<T>(T field)
		{
			this.CheckDisposed();
			Type type = field.GetType();
			if (type == typeof(string))
			{
				this.WriteField(field as string);
				return;
			}
			ITypeConverter converter = TypeConverterFactory.GetConverter(type);
			this.WriteField<T>(field, converter);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000049B0 File Offset: 0x00002BB0
		public virtual void WriteField<T>(T field, ITypeConverter converter)
		{
			this.CheckDisposed();
			TypeConverterOptions options = TypeConverterOptionsFactory.GetOptions(field.GetType());
			if (options.CultureInfo == null)
			{
				options.CultureInfo = this.configuration.CultureInfo;
			}
			string text = converter.ConvertToString(options, field);
			this.WriteField(text);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004A04 File Offset: 0x00002C04
		public virtual void WriteField<T, TConverter>(T field)
		{
			this.CheckDisposed();
			ITypeConverter converter = TypeConverterFactory.GetConverter<TConverter>();
			this.WriteField<T>(field, converter);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004A28 File Offset: 0x00002C28
		[Obsolete("This method is deprecated and will be removed in the next major release. Use WriteField<T>( T field ) instead.", false)]
		public virtual void WriteField(Type type, object field)
		{
			this.CheckDisposed();
			if (type == typeof(string))
			{
				this.WriteField(field as string);
				return;
			}
			ITypeConverter converter = TypeConverterFactory.GetConverter(type);
			this.WriteField(type, field, converter);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004A6C File Offset: 0x00002C6C
		[Obsolete("This method is deprecated and will be removed in the next major release. Use WriteField<T>( T field, ITypeConverter converter ) instead.", false)]
		public virtual void WriteField(Type type, object field, ITypeConverter converter)
		{
			this.CheckDisposed();
			TypeConverterOptions options = TypeConverterOptionsFactory.GetOptions(type);
			if (options.CultureInfo == null)
			{
				options.CultureInfo = this.configuration.CultureInfo;
			}
			string text = converter.ConvertToString(options, field);
			this.WriteField(text);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004AAF File Offset: 0x00002CAF
		public virtual void NextRecord()
		{
			this.CheckDisposed();
			this.serializer.Write(this.currentRecord.ToArray());
			this.currentRecord.Clear();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public virtual void WriteExcelSeparator()
		{
			this.CheckDisposed();
			if (this.hasHeaderBeenWritten)
			{
				throw new CsvWriterException("The Excel seperator record must be the first record written in the file.");
			}
			if (this.hasRecordBeenWritten)
			{
				throw new CsvWriterException("The Excel seperator record must be the first record written in the file.");
			}
			this.WriteField("sep=" + this.configuration.Delimiter, false);
			this.NextRecord();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004B33 File Offset: 0x00002D33
		public virtual void WriteHeader<T>()
		{
			this.CheckDisposed();
			this.WriteHeader(typeof(T));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004B4C File Offset: 0x00002D4C
		public virtual void WriteHeader(Type type)
		{
			this.CheckDisposed();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!this.configuration.HasHeaderRecord)
			{
				throw new CsvWriterException("Configuration.HasHeaderRecord is false. This will need to be enabled to write the header.");
			}
			if (this.hasHeaderBeenWritten)
			{
				throw new CsvWriterException("The header record has already been written. You can't write it more than once.");
			}
			if (this.hasRecordBeenWritten)
			{
				throw new CsvWriterException("Records have already been written. You can't write the header after writing records has started.");
			}
			if (type == typeof(object))
			{
				return;
			}
			if (this.configuration.Maps[type] == null)
			{
				this.configuration.Maps.Add(this.configuration.AutoMap(type));
			}
			CsvPropertyMapCollection csvPropertyMapCollection = new CsvPropertyMapCollection();
			this.AddProperties(csvPropertyMapCollection, this.configuration.Maps[type]);
			foreach (CsvPropertyMap csvPropertyMap in csvPropertyMapCollection)
			{
				if (this.CanWrite(csvPropertyMap))
				{
					this.WriteField(Enumerable.FirstOrDefault<string>(csvPropertyMap.Data.Names));
				}
			}
			this.NextRecord();
			this.hasHeaderBeenWritten = true;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004C74 File Offset: 0x00002E74
		public virtual void WriteRecord<T>(T record)
		{
			this.CheckDisposed();
			try
			{
				this.GetWriteRecordAction<T>().Invoke(record);
				this.hasRecordBeenWritten = true;
				this.NextRecord();
			}
			catch (Exception ex)
			{
				ExceptionHelper.AddExceptionDataMessage(ex, null, record.GetType(), null, default(int?), null);
				throw;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004CD4 File Offset: 0x00002ED4
		[Obsolete("This method is deprecated and will be removed in the next major release. Use WriteRecord<T>( T record ) instead.", false)]
		public virtual void WriteRecord(Type type, object record)
		{
			this.CheckDisposed();
			try
			{
				try
				{
					this.GetWriteRecordAction(type).DynamicInvoke(new object[] { record });
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				this.hasRecordBeenWritten = true;
				this.NextRecord();
			}
			catch (Exception ex2)
			{
				ExceptionHelper.AddExceptionDataMessage(ex2, null, type, null, default(int?), null);
				throw;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004D48 File Offset: 0x00002F48
		public virtual void WriteRecords(IEnumerable records)
		{
			this.CheckDisposed();
			Type type = null;
			try
			{
				if (this.configuration.HasExcelSeparator && !this.hasExcelSeperatorBeenRead)
				{
					this.WriteExcelSeparator();
					this.hasExcelSeperatorBeenRead = true;
				}
				Type type2 = Enumerable.FirstOrDefault<Type>(records.GetType().GetInterfaces(), (Type t) => t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable));
				if (type2 != null)
				{
					type = Enumerable.Single<Type>(type2.GetGenericArguments());
					bool isPrimitive = type.GetTypeInfo().IsPrimitive;
					if (this.configuration.HasHeaderRecord && !this.hasHeaderBeenWritten && !isPrimitive)
					{
						this.WriteHeader(type);
					}
				}
				foreach (object obj in records)
				{
					type = obj.GetType();
					bool isPrimitive2 = type.GetTypeInfo().IsPrimitive;
					if (this.configuration.HasHeaderRecord && !this.hasHeaderBeenWritten && !isPrimitive2)
					{
						this.WriteHeader(type);
					}
					try
					{
						this.GetWriteRecordAction(obj.GetType()).DynamicInvoke(new object[] { obj });
					}
					catch (TargetInvocationException ex)
					{
						throw ex.InnerException;
					}
					this.NextRecord();
				}
			}
			catch (Exception ex2)
			{
				ExceptionHelper.AddExceptionDataMessage(ex2, null, type, null, default(int?), null);
				throw;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004EE4 File Offset: 0x000030E4
		public virtual void ClearRecordCache<T>()
		{
			this.CheckDisposed();
			this.ClearRecordCache(typeof(T));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004EFC File Offset: 0x000030FC
		public virtual void ClearRecordCache(Type type)
		{
			this.CheckDisposed();
			this.typeActions.Remove(type);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004F11 File Offset: 0x00003111
		public virtual void ClearRecordCache()
		{
			this.CheckDisposed();
			this.typeActions.Clear();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004F24 File Offset: 0x00003124
		protected virtual void AddProperties(CsvPropertyMapCollection properties, CsvClassMap mapping)
		{
			properties.AddRange(mapping.PropertyMaps);
			foreach (CsvPropertyReferenceMap csvPropertyReferenceMap in mapping.ReferenceMaps)
			{
				this.AddProperties(properties, csvPropertyReferenceMap.Data.Mapping);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004F90 File Offset: 0x00003190
		protected virtual Expression CreatePropertyExpression(Expression recordExpression, CsvClassMap mapping, CsvPropertyMap propertyMap)
		{
			if (Enumerable.Any<CsvPropertyMap>(mapping.PropertyMaps, (CsvPropertyMap pm) => pm == propertyMap))
			{
				return Expression.Property(recordExpression, propertyMap.Data.Property);
			}
			foreach (CsvPropertyReferenceMap csvPropertyReferenceMap in mapping.ReferenceMaps)
			{
				MemberExpression memberExpression = Expression.Property(recordExpression, csvPropertyReferenceMap.Data.Property);
				Expression expression = this.CreatePropertyExpression(memberExpression, csvPropertyReferenceMap.Data.Mapping, propertyMap);
				if (expression != null)
				{
					if (csvPropertyReferenceMap.Data.Property.PropertyType.GetTypeInfo().IsValueType)
					{
						return expression;
					}
					BinaryExpression binaryExpression = Expression.Equal(memberExpression, Expression.Constant(null));
					bool isValueType = propertyMap.Data.Property.PropertyType.GetTypeInfo().IsValueType;
					bool flag = isValueType && propertyMap.Data.Property.PropertyType.GetTypeInfo().IsGenericType;
					Type type;
					if (isValueType && !flag && !this.configuration.UseNewObjectForNullReferenceProperties)
					{
						type = typeof(Nullable).MakeGenericType(new Type[] { propertyMap.Data.Property.PropertyType });
						expression = Expression.Convert(expression, type);
					}
					else
					{
						type = propertyMap.Data.Property.PropertyType;
					}
					Expression expression2 = ((isValueType && !flag) ? Expression.New(type) : Expression.Constant(null, type));
					return Expression.Condition(binaryExpression, expression2, expression);
				}
			}
			return null;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005170 File Offset: 0x00003370
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000517F File Offset: 0x0000337F
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing && this.serializer != null)
			{
				this.serializer.Dispose();
			}
			this.disposed = true;
			this.serializer = null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000051AE File Offset: 0x000033AE
		protected virtual void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000051CC File Offset: 0x000033CC
		protected virtual Action<T> GetWriteRecordAction<T>()
		{
			Type typeFromHandle = typeof(T);
			this.CreateWriteRecordAction(typeFromHandle);
			return (Action<T>)this.typeActions[typeFromHandle];
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000051FC File Offset: 0x000033FC
		protected virtual Delegate GetWriteRecordAction(Type type)
		{
			this.CreateWriteRecordAction(type);
			return this.typeActions[type];
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005214 File Offset: 0x00003414
		protected virtual void CreateWriteRecordAction(Type type)
		{
			if (this.typeActions.ContainsKey(type))
			{
				return;
			}
			if (this.configuration.Maps[type] == null)
			{
				this.configuration.Maps.Add(this.configuration.AutoMap(type));
			}
			if (type.GetTypeInfo().IsPrimitive)
			{
				this.CreateActionForPrimitive(type);
				return;
			}
			this.CreateActionForObject(type);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000527C File Offset: 0x0000347C
		protected virtual void CreateActionForObject(Type type)
		{
			ParameterExpression parameterExpression = Expression.Parameter(type, "record");
			CsvPropertyMapCollection csvPropertyMapCollection = new CsvPropertyMapCollection();
			this.AddProperties(csvPropertyMapCollection, this.configuration.Maps[type]);
			if (csvPropertyMapCollection.Count == 0)
			{
				throw new CsvWriterException(string.Format("No properties are mapped for type '{0}'.", type.FullName));
			}
			List<Delegate> list = new List<Delegate>();
			foreach (CsvPropertyMap csvPropertyMap in csvPropertyMapCollection)
			{
				if (this.CanWrite(csvPropertyMap) && csvPropertyMap.Data.TypeConverter != null && csvPropertyMap.Data.TypeConverter.CanConvertTo(typeof(string)))
				{
					Expression expression = this.CreatePropertyExpression(parameterExpression, this.configuration.Maps[type], csvPropertyMap);
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
					MethodInfo method = csvPropertyMap.Data.TypeConverter.GetType().GetMethod("ConvertToString");
					expression = Expression.Convert(expression, typeof(object));
					expression = Expression.Call(constantExpression, method, constantExpression2, expression);
					if (type.GetTypeInfo().IsClass)
					{
						expression = Expression.Condition(Expression.Equal(parameterExpression, Expression.Constant(null)), Expression.Constant(string.Empty), expression);
					}
					MethodCallExpression methodCallExpression = Expression.Call(Expression.Constant(this), "WriteField", new Type[] { typeof(string) }, new Expression[] { expression });
					Type type2 = typeof(Action).MakeGenericType(new Type[] { type });
					list.Add(Expression.Lambda(type2, methodCallExpression, new ParameterExpression[] { parameterExpression }).Compile());
				}
			}
			this.typeActions[type] = this.CombineDelegates(list);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000054DC File Offset: 0x000036DC
		protected virtual void CreateActionForPrimitive(Type type)
		{
			ParameterExpression parameterExpression = Expression.Parameter(type, "record");
			Expression expression = Expression.Convert(parameterExpression, typeof(object));
			ITypeConverter converter = TypeConverterFactory.GetConverter(type);
			ConstantExpression constantExpression = Expression.Constant(converter);
			MethodInfo method = converter.GetType().GetMethod("ConvertToString");
			TypeConverterOptions options = TypeConverterOptionsFactory.GetOptions(type);
			if (options.CultureInfo == null)
			{
				options.CultureInfo = this.configuration.CultureInfo;
			}
			expression = Expression.Call(constantExpression, method, Expression.Constant(options), expression);
			expression = Expression.Call(Expression.Constant(this), "WriteField", new Type[] { typeof(string) }, new Expression[] { expression });
			Type type2 = typeof(Action).MakeGenericType(new Type[] { type });
			this.typeActions[type] = Expression.Lambda(type2, expression, new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000055C2 File Offset: 0x000037C2
		protected virtual Delegate CombineDelegates(IEnumerable<Delegate> delegates)
		{
			return Enumerable.Aggregate<Delegate, Delegate>(delegates, null, new Func<Delegate, Delegate, Delegate>(Delegate.Combine));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000055D8 File Offset: 0x000037D8
		protected virtual bool CanWrite(CsvPropertyMap propertyMap)
		{
			return !propertyMap.Data.Ignore && (!(propertyMap.Data.Property.GetGetMethod() == null) || this.configuration.IgnorePrivateAccessor) && !(propertyMap.Data.Property.GetGetMethod(true) == null);
		}

		// Token: 0x04000023 RID: 35
		private bool disposed;

		// Token: 0x04000024 RID: 36
		private readonly List<string> currentRecord;

		// Token: 0x04000025 RID: 37
		private ICsvSerializer serializer;

		// Token: 0x04000026 RID: 38
		private bool hasHeaderBeenWritten;

		// Token: 0x04000027 RID: 39
		private bool hasRecordBeenWritten;

		// Token: 0x04000028 RID: 40
		private readonly Dictionary<Type, Delegate> typeActions;

		// Token: 0x04000029 RID: 41
		private readonly CsvConfiguration configuration;

		// Token: 0x0400002A RID: 42
		private bool hasExcelSeperatorBeenRead;

		// Token: 0x02000045 RID: 69
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000255 RID: 597 RVA: 0x0000805C File Offset: 0x0000625C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000256 RID: 598 RVA: 0x00002253 File Offset: 0x00000453
			public <>c()
			{
			}

			// Token: 0x06000257 RID: 599 RVA: 0x00008068 File Offset: 0x00006268
			internal bool <WriteRecords>b__26_0(Type t)
			{
				return t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable);
			}

			// Token: 0x04000086 RID: 134
			public static readonly CsvWriter.<>c <>9 = new CsvWriter.<>c();

			// Token: 0x04000087 RID: 135
			public static Func<Type, bool> <>9__26_0;
		}

		// Token: 0x02000046 RID: 70
		[CompilerGenerated]
		private sealed class <>c__DisplayClass31_0
		{
			// Token: 0x06000258 RID: 600 RVA: 0x00002253 File Offset: 0x00000453
			public <>c__DisplayClass31_0()
			{
			}

			// Token: 0x06000259 RID: 601 RVA: 0x0000808E File Offset: 0x0000628E
			internal bool <CreatePropertyExpression>b__0(CsvPropertyMap pm)
			{
				return pm == this.propertyMap;
			}

			// Token: 0x04000088 RID: 136
			public CsvPropertyMap propertyMap;
		}
	}
}
