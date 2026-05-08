using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200003F RID: 63
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchema
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000D5CF File Offset: 0x0000B7CF
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000D5D7 File Offset: 0x0000B7D7
		public string Id
		{
			[CompilerGenerated]
			get
			{
				return this.<Id>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Id>k__BackingField = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		public string Title
		{
			[CompilerGenerated]
			get
			{
				return this.<Title>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Title>k__BackingField = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000D5F1 File Offset: 0x0000B7F1
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000D5F9 File Offset: 0x0000B7F9
		public bool? Required
		{
			[CompilerGenerated]
			get
			{
				return this.<Required>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Required>k__BackingField = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000D602 File Offset: 0x0000B802
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000D60A File Offset: 0x0000B80A
		public bool? ReadOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<ReadOnly>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReadOnly>k__BackingField = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000D613 File Offset: 0x0000B813
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000D61B File Offset: 0x0000B81B
		public bool? Hidden
		{
			[CompilerGenerated]
			get
			{
				return this.<Hidden>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Hidden>k__BackingField = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000D624 File Offset: 0x0000B824
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000D62C File Offset: 0x0000B82C
		public bool? Transient
		{
			[CompilerGenerated]
			get
			{
				return this.<Transient>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Transient>k__BackingField = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000D635 File Offset: 0x0000B835
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000D63D File Offset: 0x0000B83D
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Description>k__BackingField = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000D646 File Offset: 0x0000B846
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000D64E File Offset: 0x0000B84E
		public JsonSchemaType? Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000D657 File Offset: 0x0000B857
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000D65F File Offset: 0x0000B85F
		public string Pattern
		{
			[CompilerGenerated]
			get
			{
				return this.<Pattern>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Pattern>k__BackingField = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000D668 File Offset: 0x0000B868
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000D670 File Offset: 0x0000B870
		public int? MinimumLength
		{
			[CompilerGenerated]
			get
			{
				return this.<MinimumLength>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MinimumLength>k__BackingField = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000D679 File Offset: 0x0000B879
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000D681 File Offset: 0x0000B881
		public int? MaximumLength
		{
			[CompilerGenerated]
			get
			{
				return this.<MaximumLength>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MaximumLength>k__BackingField = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000D68A File Offset: 0x0000B88A
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000D692 File Offset: 0x0000B892
		public double? DivisibleBy
		{
			[CompilerGenerated]
			get
			{
				return this.<DivisibleBy>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DivisibleBy>k__BackingField = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000D69B File Offset: 0x0000B89B
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000D6A3 File Offset: 0x0000B8A3
		public double? Minimum
		{
			[CompilerGenerated]
			get
			{
				return this.<Minimum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Minimum>k__BackingField = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		public double? Maximum
		{
			[CompilerGenerated]
			get
			{
				return this.<Maximum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Maximum>k__BackingField = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000D6BD File Offset: 0x0000B8BD
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000D6C5 File Offset: 0x0000B8C5
		public bool? ExclusiveMinimum
		{
			[CompilerGenerated]
			get
			{
				return this.<ExclusiveMinimum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExclusiveMinimum>k__BackingField = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000D6CE File Offset: 0x0000B8CE
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000D6D6 File Offset: 0x0000B8D6
		public bool? ExclusiveMaximum
		{
			[CompilerGenerated]
			get
			{
				return this.<ExclusiveMaximum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExclusiveMaximum>k__BackingField = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000D6DF File Offset: 0x0000B8DF
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000D6E7 File Offset: 0x0000B8E7
		public int? MinimumItems
		{
			[CompilerGenerated]
			get
			{
				return this.<MinimumItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MinimumItems>k__BackingField = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public int? MaximumItems
		{
			[CompilerGenerated]
			get
			{
				return this.<MaximumItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MaximumItems>k__BackingField = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000D701 File Offset: 0x0000B901
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000D709 File Offset: 0x0000B909
		public IList<JsonSchema> Items
		{
			[CompilerGenerated]
			get
			{
				return this.<Items>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Items>k__BackingField = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000D712 File Offset: 0x0000B912
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000D71A File Offset: 0x0000B91A
		public bool PositionalItemsValidation
		{
			[CompilerGenerated]
			get
			{
				return this.<PositionalItemsValidation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PositionalItemsValidation>k__BackingField = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000D723 File Offset: 0x0000B923
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000D72B File Offset: 0x0000B92B
		public JsonSchema AdditionalItems
		{
			[CompilerGenerated]
			get
			{
				return this.<AdditionalItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AdditionalItems>k__BackingField = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000D734 File Offset: 0x0000B934
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000D73C File Offset: 0x0000B93C
		public bool AllowAdditionalItems
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowAdditionalItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowAdditionalItems>k__BackingField = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000D745 File Offset: 0x0000B945
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000D74D File Offset: 0x0000B94D
		public bool UniqueItems
		{
			[CompilerGenerated]
			get
			{
				return this.<UniqueItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UniqueItems>k__BackingField = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000D756 File Offset: 0x0000B956
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000D75E File Offset: 0x0000B95E
		public IDictionary<string, JsonSchema> Properties
		{
			[CompilerGenerated]
			get
			{
				return this.<Properties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Properties>k__BackingField = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000D767 File Offset: 0x0000B967
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000D76F File Offset: 0x0000B96F
		public JsonSchema AdditionalProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<AdditionalProperties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AdditionalProperties>k__BackingField = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000D778 File Offset: 0x0000B978
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000D780 File Offset: 0x0000B980
		public IDictionary<string, JsonSchema> PatternProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<PatternProperties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PatternProperties>k__BackingField = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000D789 File Offset: 0x0000B989
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000D791 File Offset: 0x0000B991
		public bool AllowAdditionalProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowAdditionalProperties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowAdditionalProperties>k__BackingField = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000D79A File Offset: 0x0000B99A
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000D7A2 File Offset: 0x0000B9A2
		public string Requires
		{
			[CompilerGenerated]
			get
			{
				return this.<Requires>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Requires>k__BackingField = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000D7AB File Offset: 0x0000B9AB
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000D7B3 File Offset: 0x0000B9B3
		public IList<JToken> Enum
		{
			[CompilerGenerated]
			get
			{
				return this.<Enum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Enum>k__BackingField = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		public JsonSchemaType? Disallow
		{
			[CompilerGenerated]
			get
			{
				return this.<Disallow>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Disallow>k__BackingField = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000D7CD File Offset: 0x0000B9CD
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000D7D5 File Offset: 0x0000B9D5
		public JToken Default
		{
			[CompilerGenerated]
			get
			{
				return this.<Default>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Default>k__BackingField = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000D7DE File Offset: 0x0000B9DE
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000D7E6 File Offset: 0x0000B9E6
		public IList<JsonSchema> Extends
		{
			[CompilerGenerated]
			get
			{
				return this.<Extends>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Extends>k__BackingField = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000D7EF File Offset: 0x0000B9EF
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000D7F7 File Offset: 0x0000B9F7
		public string Format
		{
			[CompilerGenerated]
			get
			{
				return this.<Format>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Format>k__BackingField = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000D800 File Offset: 0x0000BA00
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000D808 File Offset: 0x0000BA08
		internal string Location
		{
			[CompilerGenerated]
			get
			{
				return this.<Location>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Location>k__BackingField = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000D811 File Offset: 0x0000BA11
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000D819 File Offset: 0x0000BA19
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000D821 File Offset: 0x0000BA21
		internal string DeferredReference
		{
			[CompilerGenerated]
			get
			{
				return this.<DeferredReference>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DeferredReference>k__BackingField = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000D82A File Offset: 0x0000BA2A
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000D832 File Offset: 0x0000BA32
		internal bool ReferencesResolved
		{
			[CompilerGenerated]
			get
			{
				return this.<ReferencesResolved>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReferencesResolved>k__BackingField = value;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D83C File Offset: 0x0000BA3C
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D875 File Offset: 0x0000BA75
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D882 File Offset: 0x0000BA82
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D8A6 File Offset: 0x0000BAA6
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, "json");
			JsonSchema jsonSchema;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				jsonSchema = JsonSchema.Read(jsonReader, resolver);
			}
			return jsonSchema;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D900 File Offset: 0x0000BB00
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D90E File Offset: 0x0000BB0E
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D934 File Offset: 0x0000BB34
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x04000165 RID: 357
		[CompilerGenerated]
		private string <Id>k__BackingField;

		// Token: 0x04000166 RID: 358
		[CompilerGenerated]
		private string <Title>k__BackingField;

		// Token: 0x04000167 RID: 359
		[CompilerGenerated]
		private bool? <Required>k__BackingField;

		// Token: 0x04000168 RID: 360
		[CompilerGenerated]
		private bool? <ReadOnly>k__BackingField;

		// Token: 0x04000169 RID: 361
		[CompilerGenerated]
		private bool? <Hidden>k__BackingField;

		// Token: 0x0400016A RID: 362
		[CompilerGenerated]
		private bool? <Transient>k__BackingField;

		// Token: 0x0400016B RID: 363
		[CompilerGenerated]
		private string <Description>k__BackingField;

		// Token: 0x0400016C RID: 364
		[CompilerGenerated]
		private JsonSchemaType? <Type>k__BackingField;

		// Token: 0x0400016D RID: 365
		[CompilerGenerated]
		private string <Pattern>k__BackingField;

		// Token: 0x0400016E RID: 366
		[CompilerGenerated]
		private int? <MinimumLength>k__BackingField;

		// Token: 0x0400016F RID: 367
		[CompilerGenerated]
		private int? <MaximumLength>k__BackingField;

		// Token: 0x04000170 RID: 368
		[CompilerGenerated]
		private double? <DivisibleBy>k__BackingField;

		// Token: 0x04000171 RID: 369
		[CompilerGenerated]
		private double? <Minimum>k__BackingField;

		// Token: 0x04000172 RID: 370
		[CompilerGenerated]
		private double? <Maximum>k__BackingField;

		// Token: 0x04000173 RID: 371
		[CompilerGenerated]
		private bool? <ExclusiveMinimum>k__BackingField;

		// Token: 0x04000174 RID: 372
		[CompilerGenerated]
		private bool? <ExclusiveMaximum>k__BackingField;

		// Token: 0x04000175 RID: 373
		[CompilerGenerated]
		private int? <MinimumItems>k__BackingField;

		// Token: 0x04000176 RID: 374
		[CompilerGenerated]
		private int? <MaximumItems>k__BackingField;

		// Token: 0x04000177 RID: 375
		[CompilerGenerated]
		private IList<JsonSchema> <Items>k__BackingField;

		// Token: 0x04000178 RID: 376
		[CompilerGenerated]
		private bool <PositionalItemsValidation>k__BackingField;

		// Token: 0x04000179 RID: 377
		[CompilerGenerated]
		private JsonSchema <AdditionalItems>k__BackingField;

		// Token: 0x0400017A RID: 378
		[CompilerGenerated]
		private bool <AllowAdditionalItems>k__BackingField;

		// Token: 0x0400017B RID: 379
		[CompilerGenerated]
		private bool <UniqueItems>k__BackingField;

		// Token: 0x0400017C RID: 380
		[CompilerGenerated]
		private IDictionary<string, JsonSchema> <Properties>k__BackingField;

		// Token: 0x0400017D RID: 381
		[CompilerGenerated]
		private JsonSchema <AdditionalProperties>k__BackingField;

		// Token: 0x0400017E RID: 382
		[CompilerGenerated]
		private IDictionary<string, JsonSchema> <PatternProperties>k__BackingField;

		// Token: 0x0400017F RID: 383
		[CompilerGenerated]
		private bool <AllowAdditionalProperties>k__BackingField;

		// Token: 0x04000180 RID: 384
		[CompilerGenerated]
		private string <Requires>k__BackingField;

		// Token: 0x04000181 RID: 385
		[CompilerGenerated]
		private IList<JToken> <Enum>k__BackingField;

		// Token: 0x04000182 RID: 386
		[CompilerGenerated]
		private JsonSchemaType? <Disallow>k__BackingField;

		// Token: 0x04000183 RID: 387
		[CompilerGenerated]
		private JToken <Default>k__BackingField;

		// Token: 0x04000184 RID: 388
		[CompilerGenerated]
		private IList<JsonSchema> <Extends>k__BackingField;

		// Token: 0x04000185 RID: 389
		[CompilerGenerated]
		private string <Format>k__BackingField;

		// Token: 0x04000186 RID: 390
		[CompilerGenerated]
		private string <Location>k__BackingField;

		// Token: 0x04000187 RID: 391
		private readonly string _internalId = Guid.NewGuid().ToString("N");

		// Token: 0x04000188 RID: 392
		[CompilerGenerated]
		private string <DeferredReference>k__BackingField;

		// Token: 0x04000189 RID: 393
		[CompilerGenerated]
		private bool <ReferencesResolved>k__BackingField;
	}
}
