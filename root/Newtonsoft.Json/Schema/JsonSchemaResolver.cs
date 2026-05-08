using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200003A RID: 58
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaResolver
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000CDCD File Offset: 0x0000AFCD
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000CDD5 File Offset: 0x0000AFD5
		public IList<JsonSchema> LoadedSchemas
		{
			[CompilerGenerated]
			get
			{
				return this.<LoadedSchemas>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<LoadedSchemas>k__BackingField = value;
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000CDDE File Offset: 0x0000AFDE
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public virtual JsonSchema GetSchema(string reference)
		{
			JsonSchema jsonSchema = Enumerable.SingleOrDefault<JsonSchema>(this.LoadedSchemas, (JsonSchema s) => string.Equals(s.Id, reference, 4));
			if (jsonSchema == null)
			{
				jsonSchema = Enumerable.SingleOrDefault<JsonSchema>(this.LoadedSchemas, (JsonSchema s) => string.Equals(s.Location, reference, 4));
			}
			return jsonSchema;
		}

		// Token: 0x0400015D RID: 349
		[CompilerGenerated]
		private IList<JsonSchema> <LoadedSchemas>k__BackingField;

		// Token: 0x0200010B RID: 267
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000C64 RID: 3172 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000C65 RID: 3173 RVA: 0x00030A49 File Offset: 0x0002EC49
			internal bool <GetSchema>b__0(JsonSchema s)
			{
				return string.Equals(s.Id, this.reference, 4);
			}

			// Token: 0x06000C66 RID: 3174 RVA: 0x00030A5D File Offset: 0x0002EC5D
			internal bool <GetSchema>b__1(JsonSchema s)
			{
				return string.Equals(s.Location, this.reference, 4);
			}

			// Token: 0x0400043D RID: 1085
			public string reference;
		}
	}
}
