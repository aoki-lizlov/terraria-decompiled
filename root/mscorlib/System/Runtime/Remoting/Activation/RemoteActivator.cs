using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020005A5 RID: 1445
	internal class RemoteActivator : MarshalByRefObject, IActivator
	{
		// Token: 0x0600387D RID: 14461 RVA: 0x000CA894 File Offset: 0x000C8A94
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			if (!RemotingConfiguration.IsActivationAllowed(msg.ActivationType))
			{
				throw new RemotingException("The type " + msg.ActivationTypeName + " is not allowed to be client activated");
			}
			object[] array = null;
			if (msg.ActivationType.IsContextful)
			{
				array = new object[]
				{
					new RemoteActivationAttribute(msg.ContextProperties)
				};
			}
			return new ConstructionResponse(RemotingServices.Marshal((MarshalByRefObject)Activator.CreateInstance(msg.ActivationType, msg.Args, array)), null, msg);
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000CA910 File Offset: 0x000C8B10
		public override object InitializeLifetimeService()
		{
			ILease lease = (ILease)base.InitializeLifetimeService();
			if (lease.CurrentState == LeaseState.Initial)
			{
				lease.InitialLeaseTime = TimeSpan.FromMinutes(30.0);
				lease.SponsorshipTimeout = TimeSpan.FromMinutes(1.0);
				lease.RenewOnCallTime = TimeSpan.FromMinutes(10.0);
			}
			return lease;
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x00047E00 File Offset: 0x00046000
		public ActivatorLevel Level
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x00047E00 File Offset: 0x00046000
		// (set) Token: 0x06003881 RID: 14465 RVA: 0x00047E00 File Offset: 0x00046000
		public IActivator NextActivator
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000543BD File Offset: 0x000525BD
		public RemoteActivator()
		{
		}
	}
}
