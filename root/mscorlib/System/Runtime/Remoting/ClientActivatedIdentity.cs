using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000541 RID: 1345
	internal class ClientActivatedIdentity : ServerIdentity
	{
		// Token: 0x06003655 RID: 13909 RVA: 0x000C552A File Offset: 0x000C372A
		public ClientActivatedIdentity(string objectUri, Type objectType)
			: base(objectUri, null, objectType)
		{
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000C5535 File Offset: 0x000C3735
		public MarshalByRefObject GetServerObject()
		{
			return this._serverObject;
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000C553D File Offset: 0x000C373D
		public MarshalByRefObject GetClientProxy()
		{
			return this._targetThis;
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000C5545 File Offset: 0x000C3745
		public void SetClientProxy(MarshalByRefObject obj)
		{
			this._targetThis = obj;
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000C554E File Offset: 0x000C374E
		public override void OnLifetimeExpired()
		{
			base.OnLifetimeExpired();
			RemotingServices.DisposeIdentity(this);
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000C555C File Offset: 0x000C375C
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain(flag ? this._targetThis : this._serverObject, flag);
			}
			return this._serverSink.SyncProcessMessage(msg);
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000C55AC File Offset: 0x000C37AC
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain(flag ? this._targetThis : this._serverObject, flag);
			}
			return this._serverSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x040024E3 RID: 9443
		private MarshalByRefObject _targetThis;
	}
}
