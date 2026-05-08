using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000FA RID: 250
	internal class BsonBoolean : BsonValue
	{
		// Token: 0x06000C03 RID: 3075 RVA: 0x00030212 File Offset: 0x0002E412
		private BsonBoolean(bool value)
			: base(value, BsonType.Boolean)
		{
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00030221 File Offset: 0x0002E421
		// Note: this type is marked as 'beforefieldinit'.
		static BsonBoolean()
		{
		}

		// Token: 0x040003E6 RID: 998
		public static readonly BsonBoolean False = new BsonBoolean(false);

		// Token: 0x040003E7 RID: 999
		public static readonly BsonBoolean True = new BsonBoolean(true);
	}
}
