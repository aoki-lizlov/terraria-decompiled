using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000F2 RID: 242
	internal enum BsonBinaryType : byte
	{
		// Token: 0x040003C3 RID: 963
		Binary,
		// Token: 0x040003C4 RID: 964
		Function,
		// Token: 0x040003C5 RID: 965
		[Obsolete("This type has been deprecated in the BSON specification. Use Binary instead.")]
		BinaryOld,
		// Token: 0x040003C6 RID: 966
		[Obsolete("This type has been deprecated in the BSON specification. Use Uuid instead.")]
		UuidOld,
		// Token: 0x040003C7 RID: 967
		Uuid,
		// Token: 0x040003C8 RID: 968
		Md5,
		// Token: 0x040003C9 RID: 969
		UserDefined = 128
	}
}
