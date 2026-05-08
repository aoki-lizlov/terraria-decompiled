using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000F8 RID: 248
	internal class BsonEmpty : BsonToken
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x000301BC File Offset: 0x0002E3BC
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x000301CB File Offset: 0x0002E3CB
		public override BsonType Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000301D3 File Offset: 0x0002E3D3
		// Note: this type is marked as 'beforefieldinit'.
		static BsonEmpty()
		{
		}

		// Token: 0x040003E1 RID: 993
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		// Token: 0x040003E2 RID: 994
		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);

		// Token: 0x040003E3 RID: 995
		[CompilerGenerated]
		private readonly BsonType <Type>k__BackingField;
	}
}
