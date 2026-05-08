using System;

namespace System.Diagnostics
{
	// Token: 0x02000A09 RID: 2569
	[Serializable]
	internal abstract class AssertFilter
	{
		// Token: 0x06005F9F RID: 24479
		public abstract AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle);

		// Token: 0x06005FA0 RID: 24480 RVA: 0x000025BE File Offset: 0x000007BE
		protected AssertFilter()
		{
		}
	}
}
