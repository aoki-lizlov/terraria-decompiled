using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020005A0 RID: 1440
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x0600386A RID: 14442 RVA: 0x000CA77F File Offset: 0x000C897F
		public ContextLevelActivator(IActivator next)
		{
			this.m_NextActivator = next;
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x00048AA1 File Offset: 0x00046CA1
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Context;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x000CA78E File Offset: 0x000C898E
		// (set) Token: 0x0600386D RID: 14445 RVA: 0x000CA796 File Offset: 0x000C8996
		public IActivator NextActivator
		{
			get
			{
				return this.m_NextActivator;
			}
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000CA7A0 File Offset: 0x000C89A0
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			ServerIdentity serverIdentity = RemotingServices.CreateContextBoundObjectIdentity(ctorCall.ActivationType);
			RemotingServices.SetMessageTargetIdentity(ctorCall, serverIdentity);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (constructionCall == null || !constructionCall.IsContextOk)
			{
				serverIdentity.Context = Context.CreateNewContext(ctorCall);
				Context context = Context.SwitchToContext(serverIdentity.Context);
				try
				{
					return this.m_NextActivator.Activate(ctorCall);
				}
				finally
				{
					Context.SwitchToContext(context);
				}
			}
			return this.m_NextActivator.Activate(ctorCall);
		}

		// Token: 0x0400257F RID: 9599
		private IActivator m_NextActivator;
	}
}
