using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000FE RID: 254
	internal class BsonProperty
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x000302CD File Offset: 0x0002E4CD
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x000302D5 File Offset: 0x0002E4D5
		public BsonString Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x000302DE File Offset: 0x0002E4DE
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x000302E6 File Offset: 0x0002E4E6
		public BsonToken Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00008020 File Offset: 0x00006220
		public BsonProperty()
		{
		}

		// Token: 0x040003ED RID: 1005
		[CompilerGenerated]
		private BsonString <Name>k__BackingField;

		// Token: 0x040003EE RID: 1006
		[CompilerGenerated]
		private BsonToken <Value>k__BackingField;
	}
}
