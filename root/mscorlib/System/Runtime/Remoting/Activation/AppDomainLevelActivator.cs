using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200059E RID: 1438
	internal class AppDomainLevelActivator : IActivator
	{
		// Token: 0x06003860 RID: 14432 RVA: 0x000CA6AC File Offset: 0x000C88AC
		public AppDomainLevelActivator(string activationUrl, IActivator next)
		{
			this._activationUrl = activationUrl;
			this._next = next;
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x00048F49 File Offset: 0x00047149
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000CA6C2 File Offset: 0x000C88C2
		// (set) Token: 0x06003863 RID: 14435 RVA: 0x000CA6CA File Offset: 0x000C88CA
		public IActivator NextActivator
		{
			get
			{
				return this._next;
			}
			set
			{
				this._next = value;
			}
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000CA6D4 File Offset: 0x000C88D4
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			IActivator activator = (IActivator)RemotingServices.Connect(typeof(IActivator), this._activationUrl);
			ctorCall.Activator = ctorCall.Activator.NextActivator;
			IConstructionReturnMessage constructionReturnMessage;
			try
			{
				constructionReturnMessage = activator.Activate(ctorCall);
			}
			catch (Exception ex)
			{
				return new ConstructionResponse(ex, ctorCall);
			}
			ObjRef objRef = (ObjRef)constructionReturnMessage.ReturnValue;
			if (RemotingServices.GetIdentityForUri(objRef.URI) != null)
			{
				throw new RemotingException("Inconsistent state during activation; there may be two proxies for the same object");
			}
			object obj;
			Identity orCreateClientIdentity = RemotingServices.GetOrCreateClientIdentity(objRef, null, out obj);
			RemotingServices.SetMessageTargetIdentity(ctorCall, orCreateClientIdentity);
			return constructionReturnMessage;
		}

		// Token: 0x0400257D RID: 9597
		private string _activationUrl;

		// Token: 0x0400257E RID: 9598
		private IActivator _next;
	}
}
