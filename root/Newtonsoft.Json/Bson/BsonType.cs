using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000FF RID: 255
	internal enum BsonType : sbyte
	{
		// Token: 0x040003F0 RID: 1008
		Number = 1,
		// Token: 0x040003F1 RID: 1009
		String,
		// Token: 0x040003F2 RID: 1010
		Object,
		// Token: 0x040003F3 RID: 1011
		Array,
		// Token: 0x040003F4 RID: 1012
		Binary,
		// Token: 0x040003F5 RID: 1013
		Undefined,
		// Token: 0x040003F6 RID: 1014
		Oid,
		// Token: 0x040003F7 RID: 1015
		Boolean,
		// Token: 0x040003F8 RID: 1016
		Date,
		// Token: 0x040003F9 RID: 1017
		Null,
		// Token: 0x040003FA RID: 1018
		Regex,
		// Token: 0x040003FB RID: 1019
		Reference,
		// Token: 0x040003FC RID: 1020
		Code,
		// Token: 0x040003FD RID: 1021
		Symbol,
		// Token: 0x040003FE RID: 1022
		CodeWScope,
		// Token: 0x040003FF RID: 1023
		Integer,
		// Token: 0x04000400 RID: 1024
		TimeStamp,
		// Token: 0x04000401 RID: 1025
		Long,
		// Token: 0x04000402 RID: 1026
		MinKey = -1,
		// Token: 0x04000403 RID: 1027
		MaxKey = 127
	}
}
