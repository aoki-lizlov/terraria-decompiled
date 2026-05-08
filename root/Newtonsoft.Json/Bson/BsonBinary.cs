using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000FC RID: 252
	internal class BsonBinary : BsonValue
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00030263 File Offset: 0x0002E463
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x0003026B File Offset: 0x0002E46B
		public BsonBinaryType BinaryType
		{
			[CompilerGenerated]
			get
			{
				return this.<BinaryType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BinaryType>k__BackingField = value;
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00030274 File Offset: 0x0002E474
		public BsonBinary(byte[] value, BsonBinaryType binaryType)
			: base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}

		// Token: 0x040003EA RID: 1002
		[CompilerGenerated]
		private BsonBinaryType <BinaryType>k__BackingField;
	}
}
