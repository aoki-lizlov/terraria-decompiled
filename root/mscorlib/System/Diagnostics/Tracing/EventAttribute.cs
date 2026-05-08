using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A44 RID: 2628
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class EventAttribute : Attribute
	{
		// Token: 0x060060A9 RID: 24745 RVA: 0x0014D3B3 File Offset: 0x0014B5B3
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x060060AA RID: 24746 RVA: 0x0014D3C2 File Offset: 0x0014B5C2
		// (set) Token: 0x060060AB RID: 24747 RVA: 0x0014D3CA File Offset: 0x0014B5CA
		public int EventId
		{
			[CompilerGenerated]
			get
			{
				return this.<EventId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EventId>k__BackingField = value;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060060AC RID: 24748 RVA: 0x0014D3D3 File Offset: 0x0014B5D3
		// (set) Token: 0x060060AD RID: 24749 RVA: 0x0014D3DB File Offset: 0x0014B5DB
		public EventActivityOptions ActivityOptions
		{
			[CompilerGenerated]
			get
			{
				return this.<ActivityOptions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ActivityOptions>k__BackingField = value;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060060AE RID: 24750 RVA: 0x0014D3E4 File Offset: 0x0014B5E4
		// (set) Token: 0x060060AF RID: 24751 RVA: 0x0014D3EC File Offset: 0x0014B5EC
		public EventLevel Level
		{
			[CompilerGenerated]
			get
			{
				return this.<Level>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Level>k__BackingField = value;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060060B0 RID: 24752 RVA: 0x0014D3F5 File Offset: 0x0014B5F5
		// (set) Token: 0x060060B1 RID: 24753 RVA: 0x0014D3FD File Offset: 0x0014B5FD
		public EventKeywords Keywords
		{
			[CompilerGenerated]
			get
			{
				return this.<Keywords>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Keywords>k__BackingField = value;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060060B2 RID: 24754 RVA: 0x0014D406 File Offset: 0x0014B606
		// (set) Token: 0x060060B3 RID: 24755 RVA: 0x0014D40E File Offset: 0x0014B60E
		public EventOpcode Opcode
		{
			[CompilerGenerated]
			get
			{
				return this.<Opcode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Opcode>k__BackingField = value;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x060060B4 RID: 24756 RVA: 0x0014D417 File Offset: 0x0014B617
		// (set) Token: 0x060060B5 RID: 24757 RVA: 0x0014D41F File Offset: 0x0014B61F
		public EventChannel Channel
		{
			[CompilerGenerated]
			get
			{
				return this.<Channel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Channel>k__BackingField = value;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x060060B6 RID: 24758 RVA: 0x0014D428 File Offset: 0x0014B628
		// (set) Token: 0x060060B7 RID: 24759 RVA: 0x0014D430 File Offset: 0x0014B630
		public string Message
		{
			[CompilerGenerated]
			get
			{
				return this.<Message>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Message>k__BackingField = value;
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060060B8 RID: 24760 RVA: 0x0014D439 File Offset: 0x0014B639
		// (set) Token: 0x060060B9 RID: 24761 RVA: 0x0014D441 File Offset: 0x0014B641
		public EventTask Task
		{
			[CompilerGenerated]
			get
			{
				return this.<Task>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Task>k__BackingField = value;
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060060BA RID: 24762 RVA: 0x0014D44A File Offset: 0x0014B64A
		// (set) Token: 0x060060BB RID: 24763 RVA: 0x0014D452 File Offset: 0x0014B652
		public EventTags Tags
		{
			[CompilerGenerated]
			get
			{
				return this.<Tags>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tags>k__BackingField = value;
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060060BC RID: 24764 RVA: 0x0014D45B File Offset: 0x0014B65B
		// (set) Token: 0x060060BD RID: 24765 RVA: 0x0014D463 File Offset: 0x0014B663
		public byte Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Version>k__BackingField = value;
			}
		}

		// Token: 0x04003A3B RID: 14907
		[CompilerGenerated]
		private int <EventId>k__BackingField;

		// Token: 0x04003A3C RID: 14908
		[CompilerGenerated]
		private EventActivityOptions <ActivityOptions>k__BackingField;

		// Token: 0x04003A3D RID: 14909
		[CompilerGenerated]
		private EventLevel <Level>k__BackingField;

		// Token: 0x04003A3E RID: 14910
		[CompilerGenerated]
		private EventKeywords <Keywords>k__BackingField;

		// Token: 0x04003A3F RID: 14911
		[CompilerGenerated]
		private EventOpcode <Opcode>k__BackingField;

		// Token: 0x04003A40 RID: 14912
		[CompilerGenerated]
		private EventChannel <Channel>k__BackingField;

		// Token: 0x04003A41 RID: 14913
		[CompilerGenerated]
		private string <Message>k__BackingField;

		// Token: 0x04003A42 RID: 14914
		[CompilerGenerated]
		private EventTask <Task>k__BackingField;

		// Token: 0x04003A43 RID: 14915
		[CompilerGenerated]
		private EventTags <Tags>k__BackingField;

		// Token: 0x04003A44 RID: 14916
		[CompilerGenerated]
		private byte <Version>k__BackingField;
	}
}
