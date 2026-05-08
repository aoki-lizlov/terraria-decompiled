using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A56 RID: 2646
	public abstract class DiagnosticCounter : IDisposable
	{
		// Token: 0x06006145 RID: 24901 RVA: 0x000025BE File Offset: 0x000007BE
		internal DiagnosticCounter(string name, EventSource eventSource)
		{
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x000025BE File Offset: 0x000007BE
		internal DiagnosticCounter()
		{
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06006147 RID: 24903 RVA: 0x0014D901 File Offset: 0x0014BB01
		// (set) Token: 0x06006148 RID: 24904 RVA: 0x0014D909 File Offset: 0x0014BB09
		public string DisplayName
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayName>k__BackingField = value;
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06006149 RID: 24905 RVA: 0x0014D912 File Offset: 0x0014BB12
		// (set) Token: 0x0600614A RID: 24906 RVA: 0x0014D91A File Offset: 0x0014BB1A
		public string DisplayUnits
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayUnits>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayUnits>k__BackingField = value;
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x0600614B RID: 24907 RVA: 0x0014D923 File Offset: 0x0014BB23
		public EventSource EventSource
		{
			[CompilerGenerated]
			get
			{
				return this.<EventSource>k__BackingField;
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x0600614C RID: 24908 RVA: 0x0014D92B File Offset: 0x0014BB2B
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x00004088 File Offset: 0x00002288
		public void AddMetadata(string key, string value)
		{
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x00004088 File Offset: 0x00002288
		public void Dispose()
		{
		}

		// Token: 0x04003A73 RID: 14963
		[CompilerGenerated]
		private string <DisplayName>k__BackingField;

		// Token: 0x04003A74 RID: 14964
		[CompilerGenerated]
		private string <DisplayUnits>k__BackingField;

		// Token: 0x04003A75 RID: 14965
		[CompilerGenerated]
		private readonly EventSource <EventSource>k__BackingField;

		// Token: 0x04003A76 RID: 14966
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;
	}
}
