using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002A2 RID: 674
	internal class ThreadHelper
	{
		// Token: 0x06001F0E RID: 7950 RVA: 0x00073D3D File Offset: 0x00071F3D
		internal ThreadHelper(Delegate start)
		{
			this._start = start;
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x00073D4C File Offset: 0x00071F4C
		internal void SetExecutionContextHelper(ExecutionContext ec)
		{
			this._executionContext = ec;
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x00073D58 File Offset: 0x00071F58
		[SecurityCritical]
		private static void ThreadStart_Context(object state)
		{
			ThreadHelper threadHelper = (ThreadHelper)state;
			if (threadHelper._start is ThreadStart)
			{
				((ThreadStart)threadHelper._start)();
				return;
			}
			((ParameterizedThreadStart)threadHelper._start)(threadHelper._startArg);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x00073DA0 File Offset: 0x00071FA0
		[SecurityCritical]
		internal void ThreadStart(object obj)
		{
			this._startArg = obj;
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ParameterizedThreadStart)this._start)(obj);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x00073DD4 File Offset: 0x00071FD4
		[SecurityCritical]
		internal void ThreadStart()
		{
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ThreadStart)this._start)();
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00073E00 File Offset: 0x00072000
		// Note: this type is marked as 'beforefieldinit'.
		static ThreadHelper()
		{
		}

		// Token: 0x040019DC RID: 6620
		private Delegate _start;

		// Token: 0x040019DD RID: 6621
		private object _startArg;

		// Token: 0x040019DE RID: 6622
		private ExecutionContext _executionContext;

		// Token: 0x040019DF RID: 6623
		[SecurityCritical]
		internal static ContextCallback _ccb = new ContextCallback(ThreadHelper.ThreadStart_Context);
	}
}
