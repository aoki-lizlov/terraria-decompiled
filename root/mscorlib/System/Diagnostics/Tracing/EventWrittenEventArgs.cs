using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A54 RID: 2644
	public class EventWrittenEventArgs : EventArgs
	{
		// Token: 0x06006129 RID: 24873 RVA: 0x0014D852 File Offset: 0x0014BA52
		internal EventWrittenEventArgs(EventSource eventSource)
		{
			this.EventSource = eventSource;
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600612A RID: 24874 RVA: 0x0014D861 File Offset: 0x0014BA61
		public Guid ActivityId
		{
			get
			{
				return EventSource.CurrentThreadActivityId;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600612B RID: 24875 RVA: 0x0000408A File Offset: 0x0000228A
		public EventChannel Channel
		{
			get
			{
				return EventChannel.None;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600612C RID: 24876 RVA: 0x0014D868 File Offset: 0x0014BA68
		// (set) Token: 0x0600612D RID: 24877 RVA: 0x0014D870 File Offset: 0x0014BA70
		public int EventId
		{
			[CompilerGenerated]
			get
			{
				return this.<EventId>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<EventId>k__BackingField = value;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x0600612E RID: 24878 RVA: 0x0014D879 File Offset: 0x0014BA79
		// (set) Token: 0x0600612F RID: 24879 RVA: 0x0014D881 File Offset: 0x0014BA81
		public long OSThreadId
		{
			[CompilerGenerated]
			get
			{
				return this.<OSThreadId>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<OSThreadId>k__BackingField = value;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06006130 RID: 24880 RVA: 0x0014D88A File Offset: 0x0014BA8A
		// (set) Token: 0x06006131 RID: 24881 RVA: 0x0014D892 File Offset: 0x0014BA92
		public DateTime TimeStamp
		{
			[CompilerGenerated]
			get
			{
				return this.<TimeStamp>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<TimeStamp>k__BackingField = value;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06006132 RID: 24882 RVA: 0x0014D89B File Offset: 0x0014BA9B
		// (set) Token: 0x06006133 RID: 24883 RVA: 0x0014D8A3 File Offset: 0x0014BAA3
		public string EventName
		{
			[CompilerGenerated]
			get
			{
				return this.<EventName>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<EventName>k__BackingField = value;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06006134 RID: 24884 RVA: 0x0014D8AC File Offset: 0x0014BAAC
		// (set) Token: 0x06006135 RID: 24885 RVA: 0x0014D8B4 File Offset: 0x0014BAB4
		public EventSource EventSource
		{
			[CompilerGenerated]
			get
			{
				return this.<EventSource>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EventSource>k__BackingField = value;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06006136 RID: 24886 RVA: 0x0000408D File Offset: 0x0000228D
		public EventKeywords Keywords
		{
			get
			{
				return EventKeywords.None;
			}
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06006137 RID: 24887 RVA: 0x0000408A File Offset: 0x0000228A
		public EventLevel Level
		{
			get
			{
				return EventLevel.LogAlways;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06006138 RID: 24888 RVA: 0x0014D8BD File Offset: 0x0014BABD
		// (set) Token: 0x06006139 RID: 24889 RVA: 0x0014D8C5 File Offset: 0x0014BAC5
		public string Message
		{
			[CompilerGenerated]
			get
			{
				return this.<Message>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Message>k__BackingField = value;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x0600613A RID: 24890 RVA: 0x0000408A File Offset: 0x0000228A
		public EventOpcode Opcode
		{
			get
			{
				return EventOpcode.Info;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x0600613B RID: 24891 RVA: 0x0014D8CE File Offset: 0x0014BACE
		// (set) Token: 0x0600613C RID: 24892 RVA: 0x0014D8D6 File Offset: 0x0014BAD6
		public ReadOnlyCollection<object> Payload
		{
			[CompilerGenerated]
			get
			{
				return this.<Payload>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Payload>k__BackingField = value;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x0600613D RID: 24893 RVA: 0x0014D8DF File Offset: 0x0014BADF
		// (set) Token: 0x0600613E RID: 24894 RVA: 0x0014D8E7 File Offset: 0x0014BAE7
		public ReadOnlyCollection<string> PayloadNames
		{
			[CompilerGenerated]
			get
			{
				return this.<PayloadNames>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PayloadNames>k__BackingField = value;
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x0600613F RID: 24895 RVA: 0x0014D8F0 File Offset: 0x0014BAF0
		// (set) Token: 0x06006140 RID: 24896 RVA: 0x0014D8F8 File Offset: 0x0014BAF8
		public Guid RelatedActivityId
		{
			[CompilerGenerated]
			get
			{
				return this.<RelatedActivityId>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<RelatedActivityId>k__BackingField = value;
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06006141 RID: 24897 RVA: 0x0000408A File Offset: 0x0000228A
		public EventTags Tags
		{
			get
			{
				return EventTags.None;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06006142 RID: 24898 RVA: 0x0000408A File Offset: 0x0000228A
		public EventTask Task
		{
			get
			{
				return EventTask.None;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06006143 RID: 24899 RVA: 0x0000408A File Offset: 0x0000228A
		public byte Version
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04003A6A RID: 14954
		[CompilerGenerated]
		private int <EventId>k__BackingField;

		// Token: 0x04003A6B RID: 14955
		[CompilerGenerated]
		private long <OSThreadId>k__BackingField;

		// Token: 0x04003A6C RID: 14956
		[CompilerGenerated]
		private DateTime <TimeStamp>k__BackingField;

		// Token: 0x04003A6D RID: 14957
		[CompilerGenerated]
		private string <EventName>k__BackingField;

		// Token: 0x04003A6E RID: 14958
		[CompilerGenerated]
		private EventSource <EventSource>k__BackingField;

		// Token: 0x04003A6F RID: 14959
		[CompilerGenerated]
		private string <Message>k__BackingField;

		// Token: 0x04003A70 RID: 14960
		[CompilerGenerated]
		private ReadOnlyCollection<object> <Payload>k__BackingField;

		// Token: 0x04003A71 RID: 14961
		[CompilerGenerated]
		private ReadOnlyCollection<string> <PayloadNames>k__BackingField;

		// Token: 0x04003A72 RID: 14962
		[CompilerGenerated]
		private Guid <RelatedActivityId>k__BackingField;
	}
}
