using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000FD RID: 253
	internal class BsonRegex : BsonToken
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00030285 File Offset: 0x0002E485
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0003028D File Offset: 0x0002E48D
		public BsonString Pattern
		{
			[CompilerGenerated]
			get
			{
				return this.<Pattern>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Pattern>k__BackingField = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00030296 File Offset: 0x0002E496
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0003029E File Offset: 0x0002E49E
		public BsonString Options
		{
			[CompilerGenerated]
			get
			{
				return this.<Options>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Options>k__BackingField = value;
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x000302A7 File Offset: 0x0002E4A7
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x000302C9 File Offset: 0x0002E4C9
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}

		// Token: 0x040003EB RID: 1003
		[CompilerGenerated]
		private BsonString <Pattern>k__BackingField;

		// Token: 0x040003EC RID: 1004
		[CompilerGenerated]
		private BsonString <Options>k__BackingField;
	}
}
