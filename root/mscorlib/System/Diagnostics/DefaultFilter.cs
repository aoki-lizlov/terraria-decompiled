using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x02000A0A RID: 2570
	internal class DefaultFilter : AssertFilter
	{
		// Token: 0x06005FA1 RID: 24481 RVA: 0x0014BF9F File Offset: 0x0014A19F
		internal DefaultFilter()
		{
		}

		// Token: 0x06005FA2 RID: 24482 RVA: 0x0014BFA7 File Offset: 0x0014A1A7
		[SecuritySafeCritical]
		public override AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle)
		{
			return (AssertFilters)Assert.ShowDefaultAssertDialog(condition, message, location.ToString(stackTraceFormat), windowTitle);
		}
	}
}
