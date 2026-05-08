using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A51 RID: 2641
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventSourceAttribute : Attribute
	{
		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x0600611F RID: 24863 RVA: 0x0014D80E File Offset: 0x0014BA0E
		// (set) Token: 0x06006120 RID: 24864 RVA: 0x0014D816 File Offset: 0x0014BA16
		public string Guid
		{
			[CompilerGenerated]
			get
			{
				return this.<Guid>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Guid>k__BackingField = value;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06006121 RID: 24865 RVA: 0x0014D81F File Offset: 0x0014BA1F
		// (set) Token: 0x06006122 RID: 24866 RVA: 0x0014D827 File Offset: 0x0014BA27
		public string LocalizationResources
		{
			[CompilerGenerated]
			get
			{
				return this.<LocalizationResources>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LocalizationResources>k__BackingField = value;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06006123 RID: 24867 RVA: 0x0014D830 File Offset: 0x0014BA30
		// (set) Token: 0x06006124 RID: 24868 RVA: 0x0014D838 File Offset: 0x0014BA38
		public string Name
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

		// Token: 0x06006125 RID: 24869 RVA: 0x00002050 File Offset: 0x00000250
		public EventSourceAttribute()
		{
		}

		// Token: 0x04003A61 RID: 14945
		[CompilerGenerated]
		private string <Guid>k__BackingField;

		// Token: 0x04003A62 RID: 14946
		[CompilerGenerated]
		private string <LocalizationResources>k__BackingField;

		// Token: 0x04003A63 RID: 14947
		[CompilerGenerated]
		private string <Name>k__BackingField;
	}
}
