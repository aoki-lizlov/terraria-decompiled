using System;
using System.Diagnostics;

namespace System.Runtime.ExceptionServices
{
	// Token: 0x020007A2 RID: 1954
	public sealed class ExceptionDispatchInfo
	{
		// Token: 0x06004540 RID: 17728 RVA: 0x000E4A8C File Offset: 0x000E2C8C
		private ExceptionDispatchInfo(Exception exception)
		{
			this.m_Exception = exception;
			StackTrace[] captured_traces = exception.captured_traces;
			int num = ((captured_traces == null) ? 0 : captured_traces.Length);
			StackTrace[] array = new StackTrace[num + 1];
			if (num != 0)
			{
				Array.Copy(captured_traces, 0, array, 0, num);
			}
			array[num] = new StackTrace(exception, 0, true);
			this.m_stackTrace = array;
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06004541 RID: 17729 RVA: 0x000E4ADF File Offset: 0x000E2CDF
		internal object BinaryStackTraceArray
		{
			get
			{
				return this.m_stackTrace;
			}
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x000E4AE7 File Offset: 0x000E2CE7
		public static ExceptionDispatchInfo Capture(Exception source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", Environment.GetResourceString("Object cannot be null."));
			}
			return new ExceptionDispatchInfo(source);
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x000E4B07 File Offset: 0x000E2D07
		public Exception SourceException
		{
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000E4B0F File Offset: 0x000E2D0F
		[StackTraceHidden]
		public void Throw()
		{
			this.m_Exception.RestoreExceptionDispatchInfo(this);
			throw this.m_Exception;
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x000E4B23 File Offset: 0x000E2D23
		[StackTraceHidden]
		public static void Throw(Exception source)
		{
			ExceptionDispatchInfo.Capture(source).Throw();
		}

		// Token: 0x04002C95 RID: 11413
		private Exception m_Exception;

		// Token: 0x04002C96 RID: 11414
		private object m_stackTrace;
	}
}
