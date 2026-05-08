using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A52 RID: 2642
	public class EventSourceCreatedEventArgs : EventArgs
	{
		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06006126 RID: 24870 RVA: 0x0014D841 File Offset: 0x0014BA41
		// (set) Token: 0x06006127 RID: 24871 RVA: 0x0014D849 File Offset: 0x0014BA49
		public EventSource EventSource
		{
			[CompilerGenerated]
			get
			{
				return this.<EventSource>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<EventSource>k__BackingField = value;
			}
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x0014D46C File Offset: 0x0014B66C
		public EventSourceCreatedEventArgs()
		{
		}

		// Token: 0x04003A64 RID: 14948
		[CompilerGenerated]
		private EventSource <EventSource>k__BackingField;
	}
}
