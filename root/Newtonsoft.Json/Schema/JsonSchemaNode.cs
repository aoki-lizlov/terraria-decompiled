using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000039 RID: 57
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNode
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000CC22 File Offset: 0x0000AE22
		public string Id
		{
			[CompilerGenerated]
			get
			{
				return this.<Id>k__BackingField;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000CC2A File Offset: 0x0000AE2A
		public ReadOnlyCollection<JsonSchema> Schemas
		{
			[CompilerGenerated]
			get
			{
				return this.<Schemas>k__BackingField;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000CC32 File Offset: 0x0000AE32
		public Dictionary<string, JsonSchemaNode> Properties
		{
			[CompilerGenerated]
			get
			{
				return this.<Properties>k__BackingField;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000CC3A File Offset: 0x0000AE3A
		public Dictionary<string, JsonSchemaNode> PatternProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<PatternProperties>k__BackingField;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000CC42 File Offset: 0x0000AE42
		public List<JsonSchemaNode> Items
		{
			[CompilerGenerated]
			get
			{
				return this.<Items>k__BackingField;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000CC4A File Offset: 0x0000AE4A
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000CC52 File Offset: 0x0000AE52
		public JsonSchemaNode AdditionalProperties
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000CC5B File Offset: 0x0000AE5B
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000CC63 File Offset: 0x0000AE63
		public JsonSchemaNode AdditionalItems
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

		// Token: 0x0600032E RID: 814 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		public JsonSchemaNode(JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(new JsonSchema[] { schema });
			this.Properties = new Dictionary<string, JsonSchemaNode>();
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>();
			this.Items = new List<JsonSchemaNode>();
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(Enumerable.ToList<JsonSchema>(Enumerable.Union<JsonSchema>(source.Schemas, new JsonSchema[] { schema })));
			this.Properties = new Dictionary<string, JsonSchemaNode>(source.Properties);
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			this.Items = new List<JsonSchemaNode>(source.Items);
			this.AdditionalProperties = source.AdditionalProperties;
			this.AdditionalItems = source.AdditionalItems;
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", Enumerable.OrderBy<string, string>(Enumerable.Select<JsonSchema, string>(schemata, (JsonSchema s) => s.InternalId), (string id) => id, StringComparer.Ordinal));
		}

		// Token: 0x04000156 RID: 342
		[CompilerGenerated]
		private readonly string <Id>k__BackingField;

		// Token: 0x04000157 RID: 343
		[CompilerGenerated]
		private readonly ReadOnlyCollection<JsonSchema> <Schemas>k__BackingField;

		// Token: 0x04000158 RID: 344
		[CompilerGenerated]
		private readonly Dictionary<string, JsonSchemaNode> <Properties>k__BackingField;

		// Token: 0x04000159 RID: 345
		[CompilerGenerated]
		private readonly Dictionary<string, JsonSchemaNode> <PatternProperties>k__BackingField;

		// Token: 0x0400015A RID: 346
		[CompilerGenerated]
		private readonly List<JsonSchemaNode> <Items>k__BackingField;

		// Token: 0x0400015B RID: 347
		[CompilerGenerated]
		private JsonSchemaNode <AdditionalProperties>k__BackingField;

		// Token: 0x0400015C RID: 348
		[CompilerGenerated]
		private JsonSchemaNode <AdditionalItems>k__BackingField;

		// Token: 0x0200010A RID: 266
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000C60 RID: 3168 RVA: 0x00030A35 File Offset: 0x0002EC35
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000C61 RID: 3169 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000C62 RID: 3170 RVA: 0x00030A41 File Offset: 0x0002EC41
			internal string <GetId>b__26_0(JsonSchema s)
			{
				return s.InternalId;
			}

			// Token: 0x06000C63 RID: 3171 RVA: 0x00017F2F File Offset: 0x0001612F
			internal string <GetId>b__26_1(string id)
			{
				return id;
			}

			// Token: 0x0400043A RID: 1082
			public static readonly JsonSchemaNode.<>c <>9 = new JsonSchemaNode.<>c();

			// Token: 0x0400043B RID: 1083
			public static Func<JsonSchema, string> <>9__26_0;

			// Token: 0x0400043C RID: 1084
			public static Func<string, string> <>9__26_1;
		}
	}
}
