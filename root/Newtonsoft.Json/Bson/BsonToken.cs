using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000F5 RID: 245
	internal abstract class BsonToken
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000BED RID: 3053
		public abstract BsonType Type { get; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x000300FE File Offset: 0x0002E2FE
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x00030106 File Offset: 0x0002E306
		public BsonToken Parent
		{
			[CompilerGenerated]
			get
			{
				return this.<Parent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Parent>k__BackingField = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0003010F File Offset: 0x0002E30F
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00030117 File Offset: 0x0002E317
		public int CalculatedSize
		{
			[CompilerGenerated]
			get
			{
				return this.<CalculatedSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CalculatedSize>k__BackingField = value;
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00008020 File Offset: 0x00006220
		protected BsonToken()
		{
		}

		// Token: 0x040003DD RID: 989
		[CompilerGenerated]
		private BsonToken <Parent>k__BackingField;

		// Token: 0x040003DE RID: 990
		[CompilerGenerated]
		private int <CalculatedSize>k__BackingField;
	}
}
