using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x0200054F RID: 1359
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	public class ProxyAttribute : Attribute, IContextAttribute
	{
		// Token: 0x060036AF RID: 13999 RVA: 0x00002050 File Offset: 0x00000250
		public ProxyAttribute()
		{
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000C6760 File Offset: 0x000C4960
		public virtual MarshalByRefObject CreateInstance(Type serverType)
		{
			return (MarshalByRefObject)new RemotingProxy(serverType, ChannelServices.CrossContextUrl, null).GetTransparentProxy();
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000C6778 File Offset: 0x000C4978
		public virtual RealProxy CreateProxy(ObjRef objRef, Type serverType, object serverObject, Context serverContext)
		{
			return RemotingServices.GetRealProxy(RemotingServices.GetProxyForRemoteObject(objRef, serverType));
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x00004088 File Offset: 0x00002288
		[ComVisible(true)]
		public void GetPropertiesForNewContext(IConstructionCallMessage msg)
		{
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x00003FB7 File Offset: 0x000021B7
		[ComVisible(true)]
		public bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}
	}
}
