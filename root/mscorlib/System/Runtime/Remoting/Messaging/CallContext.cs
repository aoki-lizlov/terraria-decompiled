using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005D1 RID: 1489
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class CallContext
	{
		// Token: 0x060039CA RID: 14794 RVA: 0x000025BE File Offset: 0x000007BE
		private CallContext()
		{
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		internal static object SetCurrentCallContext(LogicalCallContext ctx)
		{
			return null;
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000CBF28 File Offset: 0x000CA128
		internal static LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			LogicalCallContext logicalCallContext = mutableExecutionContext.LogicalCallContext;
			mutableExecutionContext.LogicalCallContext = callCtx;
			return logicalCallContext;
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000CBF4D File Offset: 0x000CA14D
		[SecurityCritical]
		public static void FreeNamedDataSlot(string name)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000CBF70 File Offset: 0x000CA170
		[SecurityCritical]
		public static object LogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.GetData(name);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000CBF98 File Offset: 0x000CA198
		private static object IllogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().IllogicalCallContext.GetData(name);
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000CBFC0 File Offset: 0x000CA1C0
		// (set) Token: 0x060039D1 RID: 14801 RVA: 0x000CBFE7 File Offset: 0x000CA1E7
		internal static IPrincipal Principal
		{
			[SecurityCritical]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.Principal;
			}
			[SecurityCritical]
			set
			{
				Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Principal = value;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x000CC000 File Offset: 0x000CA200
		// (set) Token: 0x060039D3 RID: 14803 RVA: 0x000CC03C File Offset: 0x000CA23C
		public static object HostContext
		{
			[SecurityCritical]
			get
			{
				ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
				object obj = executionContextReader.IllogicalCallContext.HostContext;
				if (obj == null)
				{
					obj = executionContextReader.LogicalCallContext.HostContext;
				}
				return obj;
			}
			[SecurityCritical]
			set
			{
				ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
				if (value is ILogicalThreadAffinative)
				{
					mutableExecutionContext.IllogicalCallContext.HostContext = null;
					mutableExecutionContext.LogicalCallContext.HostContext = value;
					return;
				}
				mutableExecutionContext.IllogicalCallContext.HostContext = value;
				mutableExecutionContext.LogicalCallContext.HostContext = null;
			}
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000CC090 File Offset: 0x000CA290
		[SecurityCritical]
		public static object GetData(string name)
		{
			object obj = CallContext.LogicalGetData(name);
			if (obj == null)
			{
				return CallContext.IllogicalGetData(name);
			}
			return obj;
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x000CC0AF File Offset: 0x000CA2AF
		[SecurityCritical]
		public static void SetData(string name, object data)
		{
			if (data is ILogicalThreadAffinative)
			{
				CallContext.LogicalSetData(name, data);
				return;
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.SetData(name, data);
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x000CC0E3 File Offset: 0x000CA2E3
		[SecurityCritical]
		public static void LogicalSetData(string name, object data)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.LogicalCallContext.SetData(name, data);
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000CC107 File Offset: 0x000CA307
		[SecurityCritical]
		public static Header[] GetHeaders()
		{
			return Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.InternalGetHeaders();
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000CC11D File Offset: 0x000CA31D
		[SecurityCritical]
		public static void SetHeaders(Header[] headers)
		{
			Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.InternalSetHeaders(headers);
		}
	}
}
