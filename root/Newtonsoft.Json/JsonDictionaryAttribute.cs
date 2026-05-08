using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(1028, AllowMultiple = false)]
	public sealed class JsonDictionaryAttribute : JsonContainerAttribute
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		public JsonDictionaryAttribute()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002058 File Offset: 0x00000258
		public JsonDictionaryAttribute(string id)
			: base(id)
		{
		}
	}
}
