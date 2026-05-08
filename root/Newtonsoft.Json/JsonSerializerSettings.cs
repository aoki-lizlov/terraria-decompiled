using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200001C RID: 28
	public class JsonSerializerSettings
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000025CA File Offset: 0x000007CA
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				ReferenceLoopHandling? referenceLoopHandling = this._referenceLoopHandling;
				if (referenceLoopHandling == null)
				{
					return ReferenceLoopHandling.Error;
				}
				return referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000025D8 File Offset: 0x000007D8
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000025FE File Offset: 0x000007FE
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				MissingMemberHandling? missingMemberHandling = this._missingMemberHandling;
				if (missingMemberHandling == null)
				{
					return MissingMemberHandling.Ignore;
				}
				return missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000260C File Offset: 0x0000080C
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002632 File Offset: 0x00000832
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				ObjectCreationHandling? objectCreationHandling = this._objectCreationHandling;
				if (objectCreationHandling == null)
				{
					return ObjectCreationHandling.Auto;
				}
				return objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002640 File Offset: 0x00000840
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002666 File Offset: 0x00000866
		public NullValueHandling NullValueHandling
		{
			get
			{
				NullValueHandling? nullValueHandling = this._nullValueHandling;
				if (nullValueHandling == null)
				{
					return NullValueHandling.Include;
				}
				return nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002674 File Offset: 0x00000874
		// (set) Token: 0x0600004E RID: 78 RVA: 0x0000269A File Offset: 0x0000089A
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				DefaultValueHandling? defaultValueHandling = this._defaultValueHandling;
				if (defaultValueHandling == null)
				{
					return DefaultValueHandling.Include;
				}
				return defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000026B0 File Offset: 0x000008B0
		public IList<JsonConverter> Converters
		{
			[CompilerGenerated]
			get
			{
				return this.<Converters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Converters>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000026BC File Offset: 0x000008BC
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000026E2 File Offset: 0x000008E2
		public PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				PreserveReferencesHandling? preserveReferencesHandling = this._preserveReferencesHandling;
				if (preserveReferencesHandling == null)
				{
					return PreserveReferencesHandling.None;
				}
				return preserveReferencesHandling.GetValueOrDefault();
			}
			set
			{
				this._preserveReferencesHandling = new PreserveReferencesHandling?(value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000026F0 File Offset: 0x000008F0
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002716 File Offset: 0x00000916
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				TypeNameHandling? typeNameHandling = this._typeNameHandling;
				if (typeNameHandling == null)
				{
					return TypeNameHandling.None;
				}
				return typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002724 File Offset: 0x00000924
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000274A File Offset: 0x0000094A
		public MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				MetadataPropertyHandling? metadataPropertyHandling = this._metadataPropertyHandling;
				if (metadataPropertyHandling == null)
				{
					return MetadataPropertyHandling.Default;
				}
				return metadataPropertyHandling.GetValueOrDefault();
			}
			set
			{
				this._metadataPropertyHandling = new MetadataPropertyHandling?(value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002758 File Offset: 0x00000958
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002760 File Offset: 0x00000960
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this.TypeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000276C File Offset: 0x0000096C
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002792 File Offset: 0x00000992
		public TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				TypeNameAssemblyFormatHandling? typeNameAssemblyFormatHandling = this._typeNameAssemblyFormatHandling;
				if (typeNameAssemblyFormatHandling == null)
				{
					return TypeNameAssemblyFormatHandling.Simple;
				}
				return typeNameAssemblyFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameAssemblyFormatHandling = new TypeNameAssemblyFormatHandling?(value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000027A0 File Offset: 0x000009A0
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000027C6 File Offset: 0x000009C6
		public ConstructorHandling ConstructorHandling
		{
			get
			{
				ConstructorHandling? constructorHandling = this._constructorHandling;
				if (constructorHandling == null)
				{
					return ConstructorHandling.Default;
				}
				return constructorHandling.GetValueOrDefault();
			}
			set
			{
				this._constructorHandling = new ConstructorHandling?(value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000027D4 File Offset: 0x000009D4
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000027DC File Offset: 0x000009DC
		public IContractResolver ContractResolver
		{
			[CompilerGenerated]
			get
			{
				return this.<ContractResolver>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContractResolver>k__BackingField = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000027E5 File Offset: 0x000009E5
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000027ED File Offset: 0x000009ED
		public IEqualityComparer EqualityComparer
		{
			[CompilerGenerated]
			get
			{
				return this.<EqualityComparer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EqualityComparer>k__BackingField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000027F6 File Offset: 0x000009F6
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000280C File Offset: 0x00000A0C
		[Obsolete("ReferenceResolver property is obsolete. Use the ReferenceResolverProvider property to set the IReferenceResolver: settings.ReferenceResolverProvider = () => resolver")]
		public IReferenceResolver ReferenceResolver
		{
			get
			{
				Func<IReferenceResolver> referenceResolverProvider = this.ReferenceResolverProvider;
				if (referenceResolverProvider == null)
				{
					return null;
				}
				return referenceResolverProvider.Invoke();
			}
			set
			{
				this.ReferenceResolverProvider = ((value != null) ? (() => value) : null);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002843 File Offset: 0x00000A43
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000284B File Offset: 0x00000A4B
		public Func<IReferenceResolver> ReferenceResolverProvider
		{
			[CompilerGenerated]
			get
			{
				return this.<ReferenceResolverProvider>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReferenceResolverProvider>k__BackingField = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002854 File Offset: 0x00000A54
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000285C File Offset: 0x00000A5C
		public ITraceWriter TraceWriter
		{
			[CompilerGenerated]
			get
			{
				return this.<TraceWriter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TraceWriter>k__BackingField = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002868 File Offset: 0x00000A68
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000289F File Offset: 0x00000A9F
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public SerializationBinder Binder
		{
			get
			{
				if (this.SerializationBinder == null)
				{
					return null;
				}
				SerializationBinderAdapter serializationBinderAdapter = this.SerializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				this.SerializationBinder = ((value == null) ? null : new SerializationBinderAdapter(value));
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000028B3 File Offset: 0x00000AB3
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000028BB File Offset: 0x00000ABB
		public ISerializationBinder SerializationBinder
		{
			[CompilerGenerated]
			get
			{
				return this.<SerializationBinder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SerializationBinder>k__BackingField = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000028C4 File Offset: 0x00000AC4
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000028CC File Offset: 0x00000ACC
		public EventHandler<ErrorEventArgs> Error
		{
			[CompilerGenerated]
			get
			{
				return this.<Error>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Error>k__BackingField = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000028D8 File Offset: 0x00000AD8
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002902 File Offset: 0x00000B02
		public StreamingContext Context
		{
			get
			{
				StreamingContext? context = this._context;
				if (context == null)
				{
					return JsonSerializerSettings.DefaultContext;
				}
				return context.GetValueOrDefault();
			}
			set
			{
				this._context = new StreamingContext?(value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002910 File Offset: 0x00000B10
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002921 File Offset: 0x00000B21
		public string DateFormatString
		{
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002931 File Offset: 0x00000B31
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000293C File Offset: 0x00000B3C
		public int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
				this._maxDepthSet = true;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002984 File Offset: 0x00000B84
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000029AA File Offset: 0x00000BAA
		public Formatting Formatting
		{
			get
			{
				Formatting? formatting = this._formatting;
				if (formatting == null)
				{
					return Formatting.None;
				}
				return formatting.GetValueOrDefault();
			}
			set
			{
				this._formatting = new Formatting?(value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000029B8 File Offset: 0x00000BB8
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000029DE File Offset: 0x00000BDE
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				DateFormatHandling? dateFormatHandling = this._dateFormatHandling;
				if (dateFormatHandling == null)
				{
					return DateFormatHandling.IsoDateFormat;
				}
				return dateFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._dateFormatHandling = new DateFormatHandling?(value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000029EC File Offset: 0x00000BEC
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002A12 File Offset: 0x00000C12
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				DateTimeZoneHandling? dateTimeZoneHandling = this._dateTimeZoneHandling;
				if (dateTimeZoneHandling == null)
				{
					return DateTimeZoneHandling.RoundtripKind;
				}
				return dateTimeZoneHandling.GetValueOrDefault();
			}
			set
			{
				this._dateTimeZoneHandling = new DateTimeZoneHandling?(value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002A20 File Offset: 0x00000C20
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002A46 File Offset: 0x00000C46
		public DateParseHandling DateParseHandling
		{
			get
			{
				DateParseHandling? dateParseHandling = this._dateParseHandling;
				if (dateParseHandling == null)
				{
					return DateParseHandling.DateTime;
				}
				return dateParseHandling.GetValueOrDefault();
			}
			set
			{
				this._dateParseHandling = new DateParseHandling?(value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002A54 File Offset: 0x00000C54
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002A7A File Offset: 0x00000C7A
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				FloatFormatHandling? floatFormatHandling = this._floatFormatHandling;
				if (floatFormatHandling == null)
				{
					return FloatFormatHandling.String;
				}
				return floatFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._floatFormatHandling = new FloatFormatHandling?(value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002A88 File Offset: 0x00000C88
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002AAE File Offset: 0x00000CAE
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				FloatParseHandling? floatParseHandling = this._floatParseHandling;
				if (floatParseHandling == null)
				{
					return FloatParseHandling.Double;
				}
				return floatParseHandling.GetValueOrDefault();
			}
			set
			{
				this._floatParseHandling = new FloatParseHandling?(value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002ABC File Offset: 0x00000CBC
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002AE2 File Offset: 0x00000CE2
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				StringEscapeHandling? stringEscapeHandling = this._stringEscapeHandling;
				if (stringEscapeHandling == null)
				{
					return StringEscapeHandling.Default;
				}
				return stringEscapeHandling.GetValueOrDefault();
			}
			set
			{
				this._stringEscapeHandling = new StringEscapeHandling?(value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002AF0 File Offset: 0x00000CF0
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002B01 File Offset: 0x00000D01
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002B0C File Offset: 0x00000D0C
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002B32 File Offset: 0x00000D32
		public bool CheckAdditionalContent
		{
			get
			{
				return this._checkAdditionalContent ?? false;
			}
			set
			{
				this._checkAdditionalContent = new bool?(value);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002B40 File Offset: 0x00000D40
		static JsonSerializerSettings()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002B57 File Offset: 0x00000D57
		public JsonSerializerSettings()
		{
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x04000051 RID: 81
		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		// Token: 0x04000052 RID: 82
		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		// Token: 0x04000053 RID: 83
		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		// Token: 0x04000054 RID: 84
		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		// Token: 0x04000055 RID: 85
		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		// Token: 0x04000056 RID: 86
		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		// Token: 0x04000057 RID: 87
		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		// Token: 0x04000058 RID: 88
		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		// Token: 0x04000059 RID: 89
		internal const MetadataPropertyHandling DefaultMetadataPropertyHandling = MetadataPropertyHandling.Default;

		// Token: 0x0400005A RID: 90
		internal static readonly StreamingContext DefaultContext = default(StreamingContext);

		// Token: 0x0400005B RID: 91
		internal const Formatting DefaultFormatting = Formatting.None;

		// Token: 0x0400005C RID: 92
		internal const DateFormatHandling DefaultDateFormatHandling = DateFormatHandling.IsoDateFormat;

		// Token: 0x0400005D RID: 93
		internal const DateTimeZoneHandling DefaultDateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

		// Token: 0x0400005E RID: 94
		internal const DateParseHandling DefaultDateParseHandling = DateParseHandling.DateTime;

		// Token: 0x0400005F RID: 95
		internal const FloatParseHandling DefaultFloatParseHandling = FloatParseHandling.Double;

		// Token: 0x04000060 RID: 96
		internal const FloatFormatHandling DefaultFloatFormatHandling = FloatFormatHandling.String;

		// Token: 0x04000061 RID: 97
		internal const StringEscapeHandling DefaultStringEscapeHandling = StringEscapeHandling.Default;

		// Token: 0x04000062 RID: 98
		internal const TypeNameAssemblyFormatHandling DefaultTypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

		// Token: 0x04000063 RID: 99
		internal static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

		// Token: 0x04000064 RID: 100
		internal const bool DefaultCheckAdditionalContent = false;

		// Token: 0x04000065 RID: 101
		internal const string DefaultDateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x04000066 RID: 102
		internal Formatting? _formatting;

		// Token: 0x04000067 RID: 103
		internal DateFormatHandling? _dateFormatHandling;

		// Token: 0x04000068 RID: 104
		internal DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x04000069 RID: 105
		internal DateParseHandling? _dateParseHandling;

		// Token: 0x0400006A RID: 106
		internal FloatFormatHandling? _floatFormatHandling;

		// Token: 0x0400006B RID: 107
		internal FloatParseHandling? _floatParseHandling;

		// Token: 0x0400006C RID: 108
		internal StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x0400006D RID: 109
		internal CultureInfo _culture;

		// Token: 0x0400006E RID: 110
		internal bool? _checkAdditionalContent;

		// Token: 0x0400006F RID: 111
		internal int? _maxDepth;

		// Token: 0x04000070 RID: 112
		internal bool _maxDepthSet;

		// Token: 0x04000071 RID: 113
		internal string _dateFormatString;

		// Token: 0x04000072 RID: 114
		internal bool _dateFormatStringSet;

		// Token: 0x04000073 RID: 115
		internal TypeNameAssemblyFormatHandling? _typeNameAssemblyFormatHandling;

		// Token: 0x04000074 RID: 116
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x04000075 RID: 117
		internal PreserveReferencesHandling? _preserveReferencesHandling;

		// Token: 0x04000076 RID: 118
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x04000077 RID: 119
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04000078 RID: 120
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x04000079 RID: 121
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x0400007A RID: 122
		internal StreamingContext? _context;

		// Token: 0x0400007B RID: 123
		internal ConstructorHandling? _constructorHandling;

		// Token: 0x0400007C RID: 124
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x0400007D RID: 125
		internal MetadataPropertyHandling? _metadataPropertyHandling;

		// Token: 0x0400007E RID: 126
		[CompilerGenerated]
		private IList<JsonConverter> <Converters>k__BackingField;

		// Token: 0x0400007F RID: 127
		[CompilerGenerated]
		private IContractResolver <ContractResolver>k__BackingField;

		// Token: 0x04000080 RID: 128
		[CompilerGenerated]
		private IEqualityComparer <EqualityComparer>k__BackingField;

		// Token: 0x04000081 RID: 129
		[CompilerGenerated]
		private Func<IReferenceResolver> <ReferenceResolverProvider>k__BackingField;

		// Token: 0x04000082 RID: 130
		[CompilerGenerated]
		private ITraceWriter <TraceWriter>k__BackingField;

		// Token: 0x04000083 RID: 131
		[CompilerGenerated]
		private ISerializationBinder <SerializationBinder>k__BackingField;

		// Token: 0x04000084 RID: 132
		[CompilerGenerated]
		private EventHandler<ErrorEventArgs> <Error>k__BackingField;

		// Token: 0x02000103 RID: 259
		[CompilerGenerated]
		private sealed class <>c__DisplayClass92_0
		{
			// Token: 0x06000C45 RID: 3141 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass92_0()
			{
			}

			// Token: 0x06000C46 RID: 3142 RVA: 0x0003084C File Offset: 0x0002EA4C
			internal IReferenceResolver <set_ReferenceResolver>b__0()
			{
				return this.value;
			}

			// Token: 0x0400040F RID: 1039
			public IReferenceResolver value;
		}
	}
}
