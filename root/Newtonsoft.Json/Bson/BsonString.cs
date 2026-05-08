using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000FB RID: 251
	internal class BsonString : BsonValue
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00030239 File Offset: 0x0002E439
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00030241 File Offset: 0x0002E441
		public int ByteCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ByteCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ByteCount>k__BackingField = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0003024A File Offset: 0x0002E44A
		public bool IncludeLength
		{
			[CompilerGenerated]
			get
			{
				return this.<IncludeLength>k__BackingField;
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00030252 File Offset: 0x0002E452
		public BsonString(object value, bool includeLength)
			: base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}

		// Token: 0x040003E8 RID: 1000
		[CompilerGenerated]
		private int <ByteCount>k__BackingField;

		// Token: 0x040003E9 RID: 1001
		[CompilerGenerated]
		private readonly bool <IncludeLength>k__BackingField;
	}
}
