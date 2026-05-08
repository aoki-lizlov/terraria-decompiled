using System;
using System.Collections.ObjectModel;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000038 RID: 56
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNodeCollection : KeyedCollection<string, JsonSchemaNode>
	{
		// Token: 0x06000323 RID: 803 RVA: 0x0000CC12 File Offset: 0x0000AE12
		protected override string GetKeyForItem(JsonSchemaNode item)
		{
			return item.Id;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000CC1A File Offset: 0x0000AE1A
		public JsonSchemaNodeCollection()
		{
		}
	}
}
