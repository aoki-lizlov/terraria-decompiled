using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000031 RID: 49
	public enum JsonToken
	{
		// Token: 0x04000111 RID: 273
		None,
		// Token: 0x04000112 RID: 274
		StartObject,
		// Token: 0x04000113 RID: 275
		StartArray,
		// Token: 0x04000114 RID: 276
		StartConstructor,
		// Token: 0x04000115 RID: 277
		PropertyName,
		// Token: 0x04000116 RID: 278
		Comment,
		// Token: 0x04000117 RID: 279
		Raw,
		// Token: 0x04000118 RID: 280
		Integer,
		// Token: 0x04000119 RID: 281
		Float,
		// Token: 0x0400011A RID: 282
		String,
		// Token: 0x0400011B RID: 283
		Boolean,
		// Token: 0x0400011C RID: 284
		Null,
		// Token: 0x0400011D RID: 285
		Undefined,
		// Token: 0x0400011E RID: 286
		EndObject,
		// Token: 0x0400011F RID: 287
		EndArray,
		// Token: 0x04000120 RID: 288
		EndConstructor,
		// Token: 0x04000121 RID: 289
		Date,
		// Token: 0x04000122 RID: 290
		Bytes
	}
}
