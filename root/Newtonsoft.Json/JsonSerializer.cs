using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200002C RID: 44
	public class JsonSerializer
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000213 RID: 531 RVA: 0x00009BE8 File Offset: 0x00007DE8
		// (remove) Token: 0x06000214 RID: 532 RVA: 0x00009C20 File Offset: 0x00007E20
		public virtual event EventHandler<ErrorEventArgs> Error
		{
			[CompilerGenerated]
			add
			{
				EventHandler<ErrorEventArgs> eventHandler = this.Error;
				EventHandler<ErrorEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ErrorEventArgs> eventHandler3 = (EventHandler<ErrorEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ErrorEventArgs>>(ref this.Error, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<ErrorEventArgs> eventHandler = this.Error;
				EventHandler<ErrorEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ErrorEventArgs> eventHandler3 = (EventHandler<ErrorEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ErrorEventArgs>>(ref this.Error, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009C55 File Offset: 0x00007E55
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00009C5D File Offset: 0x00007E5D
		public virtual IReferenceResolver ReferenceResolver
		{
			get
			{
				return this.GetReferenceResolver();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Reference resolver cannot be null.");
				}
				this._referenceResolver = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009C7C File Offset: 0x00007E7C
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00009CB3 File Offset: 0x00007EB3
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public virtual SerializationBinder Binder
		{
			get
			{
				if (this._serializationBinder == null)
				{
					return null;
				}
				SerializationBinderAdapter serializationBinderAdapter = this._serializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._serializationBinder = new SerializationBinderAdapter(value);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009CD4 File Offset: 0x00007ED4
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00009CDC File Offset: 0x00007EDC
		public virtual ISerializationBinder SerializationBinder
		{
			get
			{
				return this._serializationBinder;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._serializationBinder = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009CF8 File Offset: 0x00007EF8
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00009D00 File Offset: 0x00007F00
		public virtual ITraceWriter TraceWriter
		{
			get
			{
				return this._traceWriter;
			}
			set
			{
				this._traceWriter = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009D09 File Offset: 0x00007F09
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00009D11 File Offset: 0x00007F11
		public virtual IEqualityComparer EqualityComparer
		{
			get
			{
				return this._equalityComparer;
			}
			set
			{
				this._equalityComparer = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00009D1A File Offset: 0x00007F1A
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00009D22 File Offset: 0x00007F22
		public virtual TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling;
			}
			set
			{
				if (value < TypeNameHandling.None || value > TypeNameHandling.Auto)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameHandling = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00009D3E File Offset: 0x00007F3E
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00009D46 File Offset: 0x00007F46
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public virtual FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < 0 || value > 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00009D3E File Offset: 0x00007F3E
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00009D46 File Offset: 0x00007F46
		public virtual TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < TypeNameAssemblyFormatHandling.Simple || value > TypeNameAssemblyFormatHandling.Full)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00009D62 File Offset: 0x00007F62
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00009D6A File Offset: 0x00007F6A
		public virtual PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling;
			}
			set
			{
				if (value < PreserveReferencesHandling.None || value > PreserveReferencesHandling.All)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._preserveReferencesHandling = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009D86 File Offset: 0x00007F86
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00009D8E File Offset: 0x00007F8E
		public virtual ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling;
			}
			set
			{
				if (value < ReferenceLoopHandling.Error || value > ReferenceLoopHandling.Serialize)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._referenceLoopHandling = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009DAA File Offset: 0x00007FAA
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00009DB2 File Offset: 0x00007FB2
		public virtual MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling;
			}
			set
			{
				if (value < MissingMemberHandling.Ignore || value > MissingMemberHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._missingMemberHandling = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009DCE File Offset: 0x00007FCE
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00009DD6 File Offset: 0x00007FD6
		public virtual NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling;
			}
			set
			{
				if (value < NullValueHandling.Include || value > NullValueHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._nullValueHandling = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009DF2 File Offset: 0x00007FF2
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00009DFA File Offset: 0x00007FFA
		public virtual DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling;
			}
			set
			{
				if (value < DefaultValueHandling.Include || value > DefaultValueHandling.IgnoreAndPopulate)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._defaultValueHandling = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009E16 File Offset: 0x00008016
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00009E1E File Offset: 0x0000801E
		public virtual ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling;
			}
			set
			{
				if (value < ObjectCreationHandling.Auto || value > ObjectCreationHandling.Replace)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._objectCreationHandling = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009E3A File Offset: 0x0000803A
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00009E42 File Offset: 0x00008042
		public virtual ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling;
			}
			set
			{
				if (value < ConstructorHandling.Default || value > ConstructorHandling.AllowNonPublicDefaultConstructor)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._constructorHandling = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00009E5E File Offset: 0x0000805E
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00009E66 File Offset: 0x00008066
		public virtual MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling;
			}
			set
			{
				if (value < MetadataPropertyHandling.Default || value > MetadataPropertyHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._metadataPropertyHandling = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00009E82 File Offset: 0x00008082
		public virtual JsonConverterCollection Converters
		{
			get
			{
				if (this._converters == null)
				{
					this._converters = new JsonConverterCollection();
				}
				return this._converters;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00009E9D File Offset: 0x0000809D
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00009EA5 File Offset: 0x000080A5
		public virtual IContractResolver ContractResolver
		{
			get
			{
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = value ?? DefaultContractResolver.Instance;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00009EB7 File Offset: 0x000080B7
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00009EBF File Offset: 0x000080BF
		public virtual StreamingContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00009EC8 File Offset: 0x000080C8
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00009EEE File Offset: 0x000080EE
		public virtual Formatting Formatting
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00009EFC File Offset: 0x000080FC
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00009F22 File Offset: 0x00008122
		public virtual DateFormatHandling DateFormatHandling
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009F30 File Offset: 0x00008130
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00009F56 File Offset: 0x00008156
		public virtual DateTimeZoneHandling DateTimeZoneHandling
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009F64 File Offset: 0x00008164
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00009F8A File Offset: 0x0000818A
		public virtual DateParseHandling DateParseHandling
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009F98 File Offset: 0x00008198
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00009FBE File Offset: 0x000081BE
		public virtual FloatParseHandling FloatParseHandling
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00009FCC File Offset: 0x000081CC
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00009FF2 File Offset: 0x000081F2
		public virtual FloatFormatHandling FloatFormatHandling
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A000 File Offset: 0x00008200
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000A026 File Offset: 0x00008226
		public virtual StringEscapeHandling StringEscapeHandling
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A034 File Offset: 0x00008234
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000A045 File Offset: 0x00008245
		public virtual string DateFormatString
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A055 File Offset: 0x00008255
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000A066 File Offset: 0x00008266
		public virtual CultureInfo Culture
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A06F File Offset: 0x0000826F
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000A078 File Offset: 0x00008278
		public virtual int? MaxDepth
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

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A0C0 File Offset: 0x000082C0
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000A0E6 File Offset: 0x000082E6
		public virtual bool CheckAdditionalContent
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

		// Token: 0x06000250 RID: 592 RVA: 0x0000A0F4 File Offset: 0x000082F4
		internal bool IsCheckAdditionalContentSet()
		{
			return this._checkAdditionalContent != null;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A104 File Offset: 0x00008304
		public JsonSerializer()
		{
			this._referenceLoopHandling = ReferenceLoopHandling.Error;
			this._missingMemberHandling = MissingMemberHandling.Ignore;
			this._nullValueHandling = NullValueHandling.Include;
			this._defaultValueHandling = DefaultValueHandling.Include;
			this._objectCreationHandling = ObjectCreationHandling.Auto;
			this._preserveReferencesHandling = PreserveReferencesHandling.None;
			this._constructorHandling = ConstructorHandling.Default;
			this._typeNameHandling = TypeNameHandling.None;
			this._metadataPropertyHandling = MetadataPropertyHandling.Default;
			this._context = JsonSerializerSettings.DefaultContext;
			this._serializationBinder = DefaultSerializationBinder.Instance;
			this._culture = JsonSerializerSettings.DefaultCulture;
			this._contractResolver = DefaultContractResolver.Instance;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A182 File Offset: 0x00008382
		public static JsonSerializer Create()
		{
			return new JsonSerializer();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A18C File Offset: 0x0000838C
		public static JsonSerializer Create(JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A1AA File Offset: 0x000083AA
		public static JsonSerializer CreateDefault()
		{
			Func<JsonSerializerSettings> defaultSettings = JsonConvert.DefaultSettings;
			return JsonSerializer.Create((defaultSettings != null) ? defaultSettings.Invoke() : null);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A1C4 File Offset: 0x000083C4
		public static JsonSerializer CreateDefault(JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A1E4 File Offset: 0x000083E4
		private static void ApplySerializerSettings(JsonSerializer serializer, JsonSerializerSettings settings)
		{
			if (!CollectionUtils.IsNullOrEmpty<JsonConverter>(settings.Converters))
			{
				for (int i = 0; i < settings.Converters.Count; i++)
				{
					serializer.Converters.Insert(i, settings.Converters[i]);
				}
			}
			if (settings._typeNameHandling != null)
			{
				serializer.TypeNameHandling = settings.TypeNameHandling;
			}
			if (settings._metadataPropertyHandling != null)
			{
				serializer.MetadataPropertyHandling = settings.MetadataPropertyHandling;
			}
			if (settings._typeNameAssemblyFormatHandling != null)
			{
				serializer.TypeNameAssemblyFormatHandling = settings.TypeNameAssemblyFormatHandling;
			}
			if (settings._preserveReferencesHandling != null)
			{
				serializer.PreserveReferencesHandling = settings.PreserveReferencesHandling;
			}
			if (settings._referenceLoopHandling != null)
			{
				serializer.ReferenceLoopHandling = settings.ReferenceLoopHandling;
			}
			if (settings._missingMemberHandling != null)
			{
				serializer.MissingMemberHandling = settings.MissingMemberHandling;
			}
			if (settings._objectCreationHandling != null)
			{
				serializer.ObjectCreationHandling = settings.ObjectCreationHandling;
			}
			if (settings._nullValueHandling != null)
			{
				serializer.NullValueHandling = settings.NullValueHandling;
			}
			if (settings._defaultValueHandling != null)
			{
				serializer.DefaultValueHandling = settings.DefaultValueHandling;
			}
			if (settings._constructorHandling != null)
			{
				serializer.ConstructorHandling = settings.ConstructorHandling;
			}
			if (settings._context != null)
			{
				serializer.Context = settings.Context;
			}
			if (settings._checkAdditionalContent != null)
			{
				serializer._checkAdditionalContent = settings._checkAdditionalContent;
			}
			if (settings.Error != null)
			{
				serializer.Error += settings.Error;
			}
			if (settings.ContractResolver != null)
			{
				serializer.ContractResolver = settings.ContractResolver;
			}
			if (settings.ReferenceResolverProvider != null)
			{
				serializer.ReferenceResolver = settings.ReferenceResolverProvider.Invoke();
			}
			if (settings.TraceWriter != null)
			{
				serializer.TraceWriter = settings.TraceWriter;
			}
			if (settings.EqualityComparer != null)
			{
				serializer.EqualityComparer = settings.EqualityComparer;
			}
			if (settings.SerializationBinder != null)
			{
				serializer.SerializationBinder = settings.SerializationBinder;
			}
			if (settings._formatting != null)
			{
				serializer._formatting = settings._formatting;
			}
			if (settings._dateFormatHandling != null)
			{
				serializer._dateFormatHandling = settings._dateFormatHandling;
			}
			if (settings._dateTimeZoneHandling != null)
			{
				serializer._dateTimeZoneHandling = settings._dateTimeZoneHandling;
			}
			if (settings._dateParseHandling != null)
			{
				serializer._dateParseHandling = settings._dateParseHandling;
			}
			if (settings._dateFormatStringSet)
			{
				serializer._dateFormatString = settings._dateFormatString;
				serializer._dateFormatStringSet = settings._dateFormatStringSet;
			}
			if (settings._floatFormatHandling != null)
			{
				serializer._floatFormatHandling = settings._floatFormatHandling;
			}
			if (settings._floatParseHandling != null)
			{
				serializer._floatParseHandling = settings._floatParseHandling;
			}
			if (settings._stringEscapeHandling != null)
			{
				serializer._stringEscapeHandling = settings._stringEscapeHandling;
			}
			if (settings._culture != null)
			{
				serializer._culture = settings._culture;
			}
			if (settings._maxDepthSet)
			{
				serializer._maxDepth = settings._maxDepth;
				serializer._maxDepthSet = settings._maxDepthSet;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A4D8 File Offset: 0x000086D8
		public void Populate(TextReader reader, object target)
		{
			this.Populate(new JsonTextReader(reader), target);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A4E7 File Offset: 0x000086E7
		public void Populate(JsonReader reader, object target)
		{
			this.PopulateInternal(reader, target);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A4F4 File Offset: 0x000086F4
		internal virtual void PopulateInternal(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(target, "target");
			CultureInfo cultureInfo;
			DateTimeZoneHandling? dateTimeZoneHandling;
			DateParseHandling? dateParseHandling;
			FloatParseHandling? floatParseHandling;
			int? num;
			string text;
			this.SetupReader(reader, out cultureInfo, out dateTimeZoneHandling, out dateParseHandling, out floatParseHandling, out num, out text);
			TraceJsonReader traceJsonReader = ((this.TraceWriter != null && this.TraceWriter.LevelFilter >= 4) ? new TraceJsonReader(reader) : null);
			new JsonSerializerInternalReader(this).Populate(traceJsonReader ?? reader, target);
			if (traceJsonReader != null)
			{
				this.TraceWriter.Trace(4, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			this.ResetReader(reader, cultureInfo, dateTimeZoneHandling, dateParseHandling, floatParseHandling, num, text);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A585 File Offset: 0x00008785
		public object Deserialize(JsonReader reader)
		{
			return this.Deserialize(reader, null);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A58F File Offset: 0x0000878F
		public object Deserialize(TextReader reader, Type objectType)
		{
			return this.Deserialize(new JsonTextReader(reader), objectType);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A59E File Offset: 0x0000879E
		public T Deserialize<T>(JsonReader reader)
		{
			return (T)((object)this.Deserialize(reader, typeof(T)));
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A5B6 File Offset: 0x000087B6
		public object Deserialize(JsonReader reader, Type objectType)
		{
			return this.DeserializeInternal(reader, objectType);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A5C0 File Offset: 0x000087C0
		internal virtual object DeserializeInternal(JsonReader reader, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			CultureInfo cultureInfo;
			DateTimeZoneHandling? dateTimeZoneHandling;
			DateParseHandling? dateParseHandling;
			FloatParseHandling? floatParseHandling;
			int? num;
			string text;
			this.SetupReader(reader, out cultureInfo, out dateTimeZoneHandling, out dateParseHandling, out floatParseHandling, out num, out text);
			TraceJsonReader traceJsonReader = ((this.TraceWriter != null && this.TraceWriter.LevelFilter >= 4) ? new TraceJsonReader(reader) : null);
			object obj = new JsonSerializerInternalReader(this).Deserialize(traceJsonReader ?? reader, objectType, this.CheckAdditionalContent);
			if (traceJsonReader != null)
			{
				this.TraceWriter.Trace(4, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			this.ResetReader(reader, cultureInfo, dateTimeZoneHandling, dateParseHandling, floatParseHandling, num, text);
			return obj;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A64C File Offset: 0x0000884C
		private void SetupReader(JsonReader reader, out CultureInfo previousCulture, out DateTimeZoneHandling? previousDateTimeZoneHandling, out DateParseHandling? previousDateParseHandling, out FloatParseHandling? previousFloatParseHandling, out int? previousMaxDepth, out string previousDateFormatString)
		{
			if (this._culture != null && !this._culture.Equals(reader.Culture))
			{
				previousCulture = reader.Culture;
				reader.Culture = this._culture;
			}
			else
			{
				previousCulture = null;
			}
			if (this._dateTimeZoneHandling != null && reader.DateTimeZoneHandling != this._dateTimeZoneHandling)
			{
				previousDateTimeZoneHandling = new DateTimeZoneHandling?(reader.DateTimeZoneHandling);
				reader.DateTimeZoneHandling = this._dateTimeZoneHandling.GetValueOrDefault();
			}
			else
			{
				previousDateTimeZoneHandling = default(DateTimeZoneHandling?);
			}
			if (this._dateParseHandling != null && reader.DateParseHandling != this._dateParseHandling)
			{
				previousDateParseHandling = new DateParseHandling?(reader.DateParseHandling);
				reader.DateParseHandling = this._dateParseHandling.GetValueOrDefault();
			}
			else
			{
				previousDateParseHandling = default(DateParseHandling?);
			}
			if (this._floatParseHandling != null && reader.FloatParseHandling != this._floatParseHandling)
			{
				previousFloatParseHandling = new FloatParseHandling?(reader.FloatParseHandling);
				reader.FloatParseHandling = this._floatParseHandling.GetValueOrDefault();
			}
			else
			{
				previousFloatParseHandling = default(FloatParseHandling?);
			}
			if (this._maxDepthSet && reader.MaxDepth != this._maxDepth)
			{
				previousMaxDepth = reader.MaxDepth;
				reader.MaxDepth = this._maxDepth;
			}
			else
			{
				previousMaxDepth = default(int?);
			}
			if (this._dateFormatStringSet && reader.DateFormatString != this._dateFormatString)
			{
				previousDateFormatString = reader.DateFormatString;
				reader.DateFormatString = this._dateFormatString;
			}
			else
			{
				previousDateFormatString = null;
			}
			JsonTextReader jsonTextReader = reader as JsonTextReader;
			if (jsonTextReader != null)
			{
				DefaultContractResolver defaultContractResolver = this._contractResolver as DefaultContractResolver;
				if (defaultContractResolver != null)
				{
					jsonTextReader.NameTable = defaultContractResolver.GetNameTable();
				}
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A86C File Offset: 0x00008A6C
		private void ResetReader(JsonReader reader, CultureInfo previousCulture, DateTimeZoneHandling? previousDateTimeZoneHandling, DateParseHandling? previousDateParseHandling, FloatParseHandling? previousFloatParseHandling, int? previousMaxDepth, string previousDateFormatString)
		{
			if (previousCulture != null)
			{
				reader.Culture = previousCulture;
			}
			if (previousDateTimeZoneHandling != null)
			{
				reader.DateTimeZoneHandling = previousDateTimeZoneHandling.GetValueOrDefault();
			}
			if (previousDateParseHandling != null)
			{
				reader.DateParseHandling = previousDateParseHandling.GetValueOrDefault();
			}
			if (previousFloatParseHandling != null)
			{
				reader.FloatParseHandling = previousFloatParseHandling.GetValueOrDefault();
			}
			if (this._maxDepthSet)
			{
				reader.MaxDepth = previousMaxDepth;
			}
			if (this._dateFormatStringSet)
			{
				reader.DateFormatString = previousDateFormatString;
			}
			JsonTextReader jsonTextReader = reader as JsonTextReader;
			if (jsonTextReader != null)
			{
				jsonTextReader.NameTable = null;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A8F6 File Offset: 0x00008AF6
		public void Serialize(TextWriter textWriter, object value)
		{
			this.Serialize(new JsonTextWriter(textWriter), value);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A905 File Offset: 0x00008B05
		public void Serialize(JsonWriter jsonWriter, object value, Type objectType)
		{
			this.SerializeInternal(jsonWriter, value, objectType);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A910 File Offset: 0x00008B10
		public void Serialize(TextWriter textWriter, object value, Type objectType)
		{
			this.Serialize(new JsonTextWriter(textWriter), value, objectType);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A920 File Offset: 0x00008B20
		public void Serialize(JsonWriter jsonWriter, object value)
		{
			this.SerializeInternal(jsonWriter, value, null);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A92C File Offset: 0x00008B2C
		internal virtual void SerializeInternal(JsonWriter jsonWriter, object value, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(jsonWriter, "jsonWriter");
			Formatting? formatting = default(Formatting?);
			if (this._formatting != null && jsonWriter.Formatting != this._formatting)
			{
				formatting = new Formatting?(jsonWriter.Formatting);
				jsonWriter.Formatting = this._formatting.GetValueOrDefault();
			}
			DateFormatHandling? dateFormatHandling = default(DateFormatHandling?);
			if (this._dateFormatHandling != null && jsonWriter.DateFormatHandling != this._dateFormatHandling)
			{
				dateFormatHandling = new DateFormatHandling?(jsonWriter.DateFormatHandling);
				jsonWriter.DateFormatHandling = this._dateFormatHandling.GetValueOrDefault();
			}
			DateTimeZoneHandling? dateTimeZoneHandling = default(DateTimeZoneHandling?);
			if (this._dateTimeZoneHandling != null && jsonWriter.DateTimeZoneHandling != this._dateTimeZoneHandling)
			{
				dateTimeZoneHandling = new DateTimeZoneHandling?(jsonWriter.DateTimeZoneHandling);
				jsonWriter.DateTimeZoneHandling = this._dateTimeZoneHandling.GetValueOrDefault();
			}
			FloatFormatHandling? floatFormatHandling = default(FloatFormatHandling?);
			if (this._floatFormatHandling != null && jsonWriter.FloatFormatHandling != this._floatFormatHandling)
			{
				floatFormatHandling = new FloatFormatHandling?(jsonWriter.FloatFormatHandling);
				jsonWriter.FloatFormatHandling = this._floatFormatHandling.GetValueOrDefault();
			}
			StringEscapeHandling? stringEscapeHandling = default(StringEscapeHandling?);
			if (this._stringEscapeHandling != null && jsonWriter.StringEscapeHandling != this._stringEscapeHandling)
			{
				stringEscapeHandling = new StringEscapeHandling?(jsonWriter.StringEscapeHandling);
				jsonWriter.StringEscapeHandling = this._stringEscapeHandling.GetValueOrDefault();
			}
			CultureInfo cultureInfo = null;
			if (this._culture != null && !this._culture.Equals(jsonWriter.Culture))
			{
				cultureInfo = jsonWriter.Culture;
				jsonWriter.Culture = this._culture;
			}
			string text = null;
			if (this._dateFormatStringSet && jsonWriter.DateFormatString != this._dateFormatString)
			{
				text = jsonWriter.DateFormatString;
				jsonWriter.DateFormatString = this._dateFormatString;
			}
			TraceJsonWriter traceJsonWriter = ((this.TraceWriter != null && this.TraceWriter.LevelFilter >= 4) ? new TraceJsonWriter(jsonWriter) : null);
			new JsonSerializerInternalWriter(this).Serialize(traceJsonWriter ?? jsonWriter, value, objectType);
			if (traceJsonWriter != null)
			{
				this.TraceWriter.Trace(4, traceJsonWriter.GetSerializedJsonMessage(), null);
			}
			if (formatting != null)
			{
				jsonWriter.Formatting = formatting.GetValueOrDefault();
			}
			if (dateFormatHandling != null)
			{
				jsonWriter.DateFormatHandling = dateFormatHandling.GetValueOrDefault();
			}
			if (dateTimeZoneHandling != null)
			{
				jsonWriter.DateTimeZoneHandling = dateTimeZoneHandling.GetValueOrDefault();
			}
			if (floatFormatHandling != null)
			{
				jsonWriter.FloatFormatHandling = floatFormatHandling.GetValueOrDefault();
			}
			if (stringEscapeHandling != null)
			{
				jsonWriter.StringEscapeHandling = stringEscapeHandling.GetValueOrDefault();
			}
			if (this._dateFormatStringSet)
			{
				jsonWriter.DateFormatString = text;
			}
			if (cultureInfo != null)
			{
				jsonWriter.Culture = cultureInfo;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000AC3C File Offset: 0x00008E3C
		internal IReferenceResolver GetReferenceResolver()
		{
			if (this._referenceResolver == null)
			{
				this._referenceResolver = new DefaultReferenceResolver();
			}
			return this._referenceResolver;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000AC57 File Offset: 0x00008E57
		internal JsonConverter GetMatchingConverter(Type type)
		{
			return JsonSerializer.GetMatchingConverter(this._converters, type);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000AC68 File Offset: 0x00008E68
		internal static JsonConverter GetMatchingConverter(IList<JsonConverter> converters, Type objectType)
		{
			if (converters != null)
			{
				for (int i = 0; i < converters.Count; i++)
				{
					JsonConverter jsonConverter = converters[i];
					if (jsonConverter.CanConvert(objectType))
					{
						return jsonConverter;
					}
				}
			}
			return null;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000AC9D File Offset: 0x00008E9D
		internal void OnError(ErrorEventArgs e)
		{
			EventHandler<ErrorEventArgs> error = this.Error;
			if (error == null)
			{
				return;
			}
			error.Invoke(this, e);
		}

		// Token: 0x040000E1 RID: 225
		internal TypeNameHandling _typeNameHandling;

		// Token: 0x040000E2 RID: 226
		internal TypeNameAssemblyFormatHandling _typeNameAssemblyFormatHandling;

		// Token: 0x040000E3 RID: 227
		internal PreserveReferencesHandling _preserveReferencesHandling;

		// Token: 0x040000E4 RID: 228
		internal ReferenceLoopHandling _referenceLoopHandling;

		// Token: 0x040000E5 RID: 229
		internal MissingMemberHandling _missingMemberHandling;

		// Token: 0x040000E6 RID: 230
		internal ObjectCreationHandling _objectCreationHandling;

		// Token: 0x040000E7 RID: 231
		internal NullValueHandling _nullValueHandling;

		// Token: 0x040000E8 RID: 232
		internal DefaultValueHandling _defaultValueHandling;

		// Token: 0x040000E9 RID: 233
		internal ConstructorHandling _constructorHandling;

		// Token: 0x040000EA RID: 234
		internal MetadataPropertyHandling _metadataPropertyHandling;

		// Token: 0x040000EB RID: 235
		internal JsonConverterCollection _converters;

		// Token: 0x040000EC RID: 236
		internal IContractResolver _contractResolver;

		// Token: 0x040000ED RID: 237
		internal ITraceWriter _traceWriter;

		// Token: 0x040000EE RID: 238
		internal IEqualityComparer _equalityComparer;

		// Token: 0x040000EF RID: 239
		internal ISerializationBinder _serializationBinder;

		// Token: 0x040000F0 RID: 240
		internal StreamingContext _context;

		// Token: 0x040000F1 RID: 241
		private IReferenceResolver _referenceResolver;

		// Token: 0x040000F2 RID: 242
		private Formatting? _formatting;

		// Token: 0x040000F3 RID: 243
		private DateFormatHandling? _dateFormatHandling;

		// Token: 0x040000F4 RID: 244
		private DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x040000F5 RID: 245
		private DateParseHandling? _dateParseHandling;

		// Token: 0x040000F6 RID: 246
		private FloatFormatHandling? _floatFormatHandling;

		// Token: 0x040000F7 RID: 247
		private FloatParseHandling? _floatParseHandling;

		// Token: 0x040000F8 RID: 248
		private StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x040000F9 RID: 249
		private CultureInfo _culture;

		// Token: 0x040000FA RID: 250
		private int? _maxDepth;

		// Token: 0x040000FB RID: 251
		private bool _maxDepthSet;

		// Token: 0x040000FC RID: 252
		private bool? _checkAdditionalContent;

		// Token: 0x040000FD RID: 253
		private string _dateFormatString;

		// Token: 0x040000FE RID: 254
		private bool _dateFormatStringSet;

		// Token: 0x040000FF RID: 255
		[CompilerGenerated]
		private EventHandler<ErrorEventArgs> Error;
	}
}
