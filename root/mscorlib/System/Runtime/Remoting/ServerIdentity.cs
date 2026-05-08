using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;

namespace System.Runtime.Remoting
{
	// Token: 0x02000540 RID: 1344
	internal abstract class ServerIdentity : Identity
	{
		// Token: 0x06003649 RID: 13897 RVA: 0x000C5386 File Offset: 0x000C3586
		public ServerIdentity(string objectUri, Context context, Type objectType)
			: base(objectUri)
		{
			this._objectType = objectType;
			this._context = context;
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x000C539D File Offset: 0x000C359D
		public Type ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000C53A5 File Offset: 0x000C35A5
		public void StartTrackingLifetime(ILease lease)
		{
			if (lease != null && lease.CurrentState == LeaseState.Null)
			{
				lease = null;
			}
			if (lease != null)
			{
				if (!(lease is Lease))
				{
					lease = new Lease();
				}
				this._lease = (Lease)lease;
				LifetimeServices.TrackLifetime(this);
			}
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000C53D9 File Offset: 0x000C35D9
		public virtual void OnLifetimeExpired()
		{
			this.DisposeServerObject();
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000C53E4 File Offset: 0x000C35E4
		public override ObjRef CreateObjRef(Type requestedType)
		{
			if (this._objRef != null)
			{
				this._objRef.UpdateChannelInfo();
				return this._objRef;
			}
			if (requestedType == null)
			{
				requestedType = this._objectType;
			}
			this._objRef = new ObjRef();
			this._objRef.TypeInfo = new TypeInfo(requestedType);
			this._objRef.URI = this._objectUri;
			if (this._envoySink != null && !(this._envoySink is EnvoyTerminatorSink))
			{
				this._objRef.EnvoyInfo = new EnvoyInfo(this._envoySink);
			}
			return this._objRef;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000C547C File Offset: 0x000C367C
		public void AttachServerObject(MarshalByRefObject serverObject, Context context)
		{
			this.DisposeServerObject();
			this._context = context;
			this._serverObject = serverObject;
			if (RemotingServices.IsTransparentProxy(serverObject))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(serverObject);
				if (realProxy.ObjectIdentity == null)
				{
					realProxy.ObjectIdentity = this;
					return;
				}
			}
			else
			{
				if (this._objectType.IsContextful)
				{
					this._envoySink = context.CreateEnvoySink(serverObject);
				}
				this._serverObject.ObjectIdentity = this;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600364F RID: 13903 RVA: 0x000C54E2 File Offset: 0x000C36E2
		public Lease Lease
		{
			get
			{
				return this._lease;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x000C54EA File Offset: 0x000C36EA
		// (set) Token: 0x06003651 RID: 13905 RVA: 0x000C54F2 File Offset: 0x000C36F2
		public Context Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x06003652 RID: 13906
		public abstract IMessage SyncObjectProcessMessage(IMessage msg);

		// Token: 0x06003653 RID: 13907
		public abstract IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x06003654 RID: 13908 RVA: 0x000C54FB File Offset: 0x000C36FB
		protected void DisposeServerObject()
		{
			if (this._serverObject != null)
			{
				object serverObject = this._serverObject;
				this._serverObject.ObjectIdentity = null;
				this._serverObject = null;
				this._serverSink = null;
				TrackingServices.NotifyDisconnectedObject(serverObject);
			}
		}

		// Token: 0x040024DE RID: 9438
		protected Type _objectType;

		// Token: 0x040024DF RID: 9439
		protected MarshalByRefObject _serverObject;

		// Token: 0x040024E0 RID: 9440
		protected IMessageSink _serverSink;

		// Token: 0x040024E1 RID: 9441
		protected Context _context;

		// Token: 0x040024E2 RID: 9442
		protected Lease _lease;
	}
}
