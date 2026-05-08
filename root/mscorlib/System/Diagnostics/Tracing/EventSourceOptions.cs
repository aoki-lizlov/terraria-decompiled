using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A3D RID: 2621
	public struct EventSourceOptions
	{
		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600609E RID: 24734 RVA: 0x0014D303 File Offset: 0x0014B503
		// (set) Token: 0x0600609F RID: 24735 RVA: 0x0014D30B File Offset: 0x0014B50B
		public EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
			set
			{
				this.level = checked((byte)value);
				this.valuesSet |= 4;
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x060060A0 RID: 24736 RVA: 0x0014D324 File Offset: 0x0014B524
		// (set) Token: 0x060060A1 RID: 24737 RVA: 0x0014D32C File Offset: 0x0014B52C
		public EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
			set
			{
				this.opcode = checked((byte)value);
				this.valuesSet |= 8;
			}
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x060060A2 RID: 24738 RVA: 0x0014D345 File Offset: 0x0014B545
		internal bool IsOpcodeSet
		{
			get
			{
				return (this.valuesSet & 8) > 0;
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x060060A3 RID: 24739 RVA: 0x0014D352 File Offset: 0x0014B552
		// (set) Token: 0x060060A4 RID: 24740 RVA: 0x0014D35A File Offset: 0x0014B55A
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
			set
			{
				this.keywords = value;
				this.valuesSet |= 1;
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x060060A5 RID: 24741 RVA: 0x0014D372 File Offset: 0x0014B572
		// (set) Token: 0x060060A6 RID: 24742 RVA: 0x0014D37A File Offset: 0x0014B57A
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
			set
			{
				this.tags = value;
				this.valuesSet |= 2;
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x060060A7 RID: 24743 RVA: 0x0014D392 File Offset: 0x0014B592
		// (set) Token: 0x060060A8 RID: 24744 RVA: 0x0014D39A File Offset: 0x0014B59A
		public EventActivityOptions ActivityOptions
		{
			get
			{
				return this.activityOptions;
			}
			set
			{
				this.activityOptions = value;
				this.valuesSet |= 16;
			}
		}

		// Token: 0x04003A08 RID: 14856
		internal EventKeywords keywords;

		// Token: 0x04003A09 RID: 14857
		internal EventTags tags;

		// Token: 0x04003A0A RID: 14858
		internal EventActivityOptions activityOptions;

		// Token: 0x04003A0B RID: 14859
		internal byte level;

		// Token: 0x04003A0C RID: 14860
		internal byte opcode;

		// Token: 0x04003A0D RID: 14861
		internal byte valuesSet;

		// Token: 0x04003A0E RID: 14862
		internal const byte keywordsSet = 1;

		// Token: 0x04003A0F RID: 14863
		internal const byte tagsSet = 2;

		// Token: 0x04003A10 RID: 14864
		internal const byte levelSet = 4;

		// Token: 0x04003A11 RID: 14865
		internal const byte opcodeSet = 8;

		// Token: 0x04003A12 RID: 14866
		internal const byte activityOptionsSet = 16;
	}
}
