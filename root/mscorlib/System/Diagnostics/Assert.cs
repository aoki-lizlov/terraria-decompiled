using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x02000A08 RID: 2568
	internal static class Assert
	{
		// Token: 0x06005F97 RID: 24471 RVA: 0x0014BEE2 File Offset: 0x0014A0E2
		static Assert()
		{
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x0014BEEE File Offset: 0x0014A0EE
		internal static void Check(bool condition, string conditionString, string message)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, -2146232797);
			}
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x0014BF00 File Offset: 0x0014A100
		internal static void Check(bool condition, string conditionString, string message, int exitCode)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, exitCode);
			}
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x0014BF0E File Offset: 0x0014A10E
		internal static void Fail(string conditionString, string message)
		{
			Assert.Fail(conditionString, message, null, -2146232797);
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x0014BF1D File Offset: 0x0014A11D
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode)
		{
			Assert.Fail(conditionString, message, windowTitle, exitCode, StackTrace.TraceFormat.Normal, 0);
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x0014BF2A File Offset: 0x0014A12A
		internal static void Fail(string conditionString, string message, int exitCode, StackTrace.TraceFormat stackTraceFormat)
		{
			Assert.Fail(conditionString, message, null, exitCode, stackTraceFormat, 0);
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x0014BF38 File Offset: 0x0014A138
		[SecuritySafeCritical]
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode, StackTrace.TraceFormat stackTraceFormat, int numStackFramesToSkip)
		{
			StackTrace stackTrace = new StackTrace(numStackFramesToSkip, true);
			AssertFilters assertFilters = Assert.Filter.AssertFailure(conditionString, message, stackTrace, stackTraceFormat, windowTitle);
			if (assertFilters == AssertFilters.FailDebug)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
					return;
				}
				if (!Debugger.Launch())
				{
					throw new InvalidOperationException(Environment.GetResourceString("Debugger unable to launch."));
				}
			}
			else if (assertFilters == AssertFilters.FailTerminate)
			{
				if (Debugger.IsAttached)
				{
					Environment._Exit(exitCode);
					return;
				}
				Environment.FailFast(message, (uint)exitCode);
			}
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x000174FB File Offset: 0x000156FB
		internal static int ShowDefaultAssertDialog(string conditionString, string message, string stackTrace, string windowTitle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400399A RID: 14746
		internal const int COR_E_FAILFAST = -2146232797;

		// Token: 0x0400399B RID: 14747
		private static AssertFilter Filter = new DefaultFilter();
	}
}
