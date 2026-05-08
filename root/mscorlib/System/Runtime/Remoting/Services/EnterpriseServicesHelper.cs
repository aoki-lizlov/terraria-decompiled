using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x0200054C RID: 1356
	[ComVisible(true)]
	public sealed class EnterpriseServicesHelper
	{
		// Token: 0x060036A0 RID: 13984 RVA: 0x000025BE File Offset: 0x000007BE
		public EnterpriseServicesHelper()
		{
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000C6471 File Offset: 0x000C4671
		[ComVisible(true)]
		public static IConstructionReturnMessage CreateConstructionReturnMessage(IConstructionCallMessage ctorMsg, MarshalByRefObject retObj)
		{
			return new ConstructionResponse(retObj, null, ctorMsg);
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x00047E00 File Offset: 0x00046000
		[MonoTODO]
		public static void SwitchWrappers(RealProxy oldcp, RealProxy newcp)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x00047E00 File Offset: 0x00046000
		[MonoTODO]
		public static object WrapIUnknownWithComObject(IntPtr punk)
		{
			throw new NotSupportedException();
		}
	}
}
