using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000043 RID: 67
	[Flags]
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	public enum JsonSchemaType
	{
		// Token: 0x040001B6 RID: 438
		None = 0,
		// Token: 0x040001B7 RID: 439
		String = 1,
		// Token: 0x040001B8 RID: 440
		Float = 2,
		// Token: 0x040001B9 RID: 441
		Integer = 4,
		// Token: 0x040001BA RID: 442
		Boolean = 8,
		// Token: 0x040001BB RID: 443
		Object = 16,
		// Token: 0x040001BC RID: 444
		Array = 32,
		// Token: 0x040001BD RID: 445
		Null = 64,
		// Token: 0x040001BE RID: 446
		Any = 127
	}
}
