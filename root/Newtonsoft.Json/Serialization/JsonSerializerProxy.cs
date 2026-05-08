using System;
using System.Collections;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A1 RID: 161
	internal class JsonSerializerProxy : JsonSerializer
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060007A5 RID: 1957 RVA: 0x00021FA5 File Offset: 0x000201A5
		// (remove) Token: 0x060007A6 RID: 1958 RVA: 0x00021FB3 File Offset: 0x000201B3
		public override event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this._serializer.Error += value;
			}
			remove
			{
				this._serializer.Error -= value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00021FC1 File Offset: 0x000201C1
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x00021FCE File Offset: 0x000201CE
		public override IReferenceResolver ReferenceResolver
		{
			get
			{
				return this._serializer.ReferenceResolver;
			}
			set
			{
				this._serializer.ReferenceResolver = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00021FDC File Offset: 0x000201DC
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x00021FE9 File Offset: 0x000201E9
		public override ITraceWriter TraceWriter
		{
			get
			{
				return this._serializer.TraceWriter;
			}
			set
			{
				this._serializer.TraceWriter = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00021FF7 File Offset: 0x000201F7
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x00022004 File Offset: 0x00020204
		public override IEqualityComparer EqualityComparer
		{
			get
			{
				return this._serializer.EqualityComparer;
			}
			set
			{
				this._serializer.EqualityComparer = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00022012 File Offset: 0x00020212
		public override JsonConverterCollection Converters
		{
			get
			{
				return this._serializer.Converters;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0002201F File Offset: 0x0002021F
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0002202C File Offset: 0x0002022C
		public override DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._serializer.DefaultValueHandling;
			}
			set
			{
				this._serializer.DefaultValueHandling = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0002203A File Offset: 0x0002023A
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00022047 File Offset: 0x00020247
		public override IContractResolver ContractResolver
		{
			get
			{
				return this._serializer.ContractResolver;
			}
			set
			{
				this._serializer.ContractResolver = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00022055 File Offset: 0x00020255
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x00022062 File Offset: 0x00020262
		public override MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._serializer.MissingMemberHandling;
			}
			set
			{
				this._serializer.MissingMemberHandling = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00022070 File Offset: 0x00020270
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0002207D File Offset: 0x0002027D
		public override NullValueHandling NullValueHandling
		{
			get
			{
				return this._serializer.NullValueHandling;
			}
			set
			{
				this._serializer.NullValueHandling = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0002208B File Offset: 0x0002028B
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00022098 File Offset: 0x00020298
		public override ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._serializer.ObjectCreationHandling;
			}
			set
			{
				this._serializer.ObjectCreationHandling = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000220A6 File Offset: 0x000202A6
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x000220B3 File Offset: 0x000202B3
		public override ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._serializer.ReferenceLoopHandling;
			}
			set
			{
				this._serializer.ReferenceLoopHandling = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x000220C1 File Offset: 0x000202C1
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x000220CE File Offset: 0x000202CE
		public override PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._serializer.PreserveReferencesHandling;
			}
			set
			{
				this._serializer.PreserveReferencesHandling = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x000220DC File Offset: 0x000202DC
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x000220E9 File Offset: 0x000202E9
		public override TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._serializer.TypeNameHandling;
			}
			set
			{
				this._serializer.TypeNameHandling = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x000220F7 File Offset: 0x000202F7
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00022104 File Offset: 0x00020304
		public override MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._serializer.MetadataPropertyHandling;
			}
			set
			{
				this._serializer.MetadataPropertyHandling = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00022112 File Offset: 0x00020312
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0002211F File Offset: 0x0002031F
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public override FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormat;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormat = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0002212D File Offset: 0x0002032D
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0002213A File Offset: 0x0002033A
		public override TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00022148 File Offset: 0x00020348
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00022155 File Offset: 0x00020355
		public override ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._serializer.ConstructorHandling;
			}
			set
			{
				this._serializer.ConstructorHandling = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00022163 File Offset: 0x00020363
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00022170 File Offset: 0x00020370
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public override SerializationBinder Binder
		{
			get
			{
				return this._serializer.Binder;
			}
			set
			{
				this._serializer.Binder = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0002217E File Offset: 0x0002037E
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0002218B File Offset: 0x0002038B
		public override ISerializationBinder SerializationBinder
		{
			get
			{
				return this._serializer.SerializationBinder;
			}
			set
			{
				this._serializer.SerializationBinder = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00022199 File Offset: 0x00020399
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x000221A6 File Offset: 0x000203A6
		public override StreamingContext Context
		{
			get
			{
				return this._serializer.Context;
			}
			set
			{
				this._serializer.Context = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000221B4 File Offset: 0x000203B4
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x000221C1 File Offset: 0x000203C1
		public override Formatting Formatting
		{
			get
			{
				return this._serializer.Formatting;
			}
			set
			{
				this._serializer.Formatting = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x000221CF File Offset: 0x000203CF
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x000221DC File Offset: 0x000203DC
		public override DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._serializer.DateFormatHandling;
			}
			set
			{
				this._serializer.DateFormatHandling = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000221EA File Offset: 0x000203EA
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x000221F7 File Offset: 0x000203F7
		public override DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._serializer.DateTimeZoneHandling;
			}
			set
			{
				this._serializer.DateTimeZoneHandling = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00022205 File Offset: 0x00020405
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00022212 File Offset: 0x00020412
		public override DateParseHandling DateParseHandling
		{
			get
			{
				return this._serializer.DateParseHandling;
			}
			set
			{
				this._serializer.DateParseHandling = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00022220 File Offset: 0x00020420
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0002222D File Offset: 0x0002042D
		public override FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._serializer.FloatFormatHandling;
			}
			set
			{
				this._serializer.FloatFormatHandling = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0002223B File Offset: 0x0002043B
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00022248 File Offset: 0x00020448
		public override FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._serializer.FloatParseHandling;
			}
			set
			{
				this._serializer.FloatParseHandling = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00022256 File Offset: 0x00020456
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00022263 File Offset: 0x00020463
		public override StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._serializer.StringEscapeHandling;
			}
			set
			{
				this._serializer.StringEscapeHandling = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00022271 File Offset: 0x00020471
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0002227E File Offset: 0x0002047E
		public override string DateFormatString
		{
			get
			{
				return this._serializer.DateFormatString;
			}
			set
			{
				this._serializer.DateFormatString = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0002228C File Offset: 0x0002048C
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x00022299 File Offset: 0x00020499
		public override CultureInfo Culture
		{
			get
			{
				return this._serializer.Culture;
			}
			set
			{
				this._serializer.Culture = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x000222A7 File Offset: 0x000204A7
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x000222B4 File Offset: 0x000204B4
		public override int? MaxDepth
		{
			get
			{
				return this._serializer.MaxDepth;
			}
			set
			{
				this._serializer.MaxDepth = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x000222C2 File Offset: 0x000204C2
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x000222CF File Offset: 0x000204CF
		public override bool CheckAdditionalContent
		{
			get
			{
				return this._serializer.CheckAdditionalContent;
			}
			set
			{
				this._serializer.CheckAdditionalContent = value;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000222DD File Offset: 0x000204DD
		internal JsonSerializerInternalBase GetInternalSerializer()
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader;
			}
			return this._serializerWriter;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000222F4 File Offset: 0x000204F4
		public JsonSerializerProxy(JsonSerializerInternalReader serializerReader)
		{
			ValidationUtils.ArgumentNotNull(serializerReader, "serializerReader");
			this._serializerReader = serializerReader;
			this._serializer = serializerReader.Serializer;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002231A File Offset: 0x0002051A
		public JsonSerializerProxy(JsonSerializerInternalWriter serializerWriter)
		{
			ValidationUtils.ArgumentNotNull(serializerWriter, "serializerWriter");
			this._serializerWriter = serializerWriter;
			this._serializer = serializerWriter.Serializer;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00022340 File Offset: 0x00020540
		internal override object DeserializeInternal(JsonReader reader, Type objectType)
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader.Deserialize(reader, objectType, false);
			}
			return this._serializer.Deserialize(reader, objectType);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00022366 File Offset: 0x00020566
		internal override void PopulateInternal(JsonReader reader, object target)
		{
			if (this._serializerReader != null)
			{
				this._serializerReader.Populate(reader, target);
				return;
			}
			this._serializer.Populate(reader, target);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002238B File Offset: 0x0002058B
		internal override void SerializeInternal(JsonWriter jsonWriter, object value, Type rootType)
		{
			if (this._serializerWriter != null)
			{
				this._serializerWriter.Serialize(jsonWriter, value, rootType);
				return;
			}
			this._serializer.Serialize(jsonWriter, value);
		}

		// Token: 0x04000317 RID: 791
		private readonly JsonSerializerInternalReader _serializerReader;

		// Token: 0x04000318 RID: 792
		private readonly JsonSerializerInternalWriter _serializerWriter;

		// Token: 0x04000319 RID: 793
		private readonly JsonSerializer _serializer;
	}
}
