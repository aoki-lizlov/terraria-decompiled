using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020002BD RID: 701
	public class HostExecutionContextManager
	{
		// Token: 0x0600205F RID: 8287 RVA: 0x000025BE File Offset: 0x000007BE
		public HostExecutionContextManager()
		{
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public virtual HostExecutionContext Capture()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Revert(object previousState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public virtual object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
		{
			throw new NotImplementedException();
		}
	}
}
