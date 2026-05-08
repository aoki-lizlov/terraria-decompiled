using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000531 RID: 1329
	internal abstract class Identity
	{
		// Token: 0x06003581 RID: 13697 RVA: 0x000C1DC9 File Offset: 0x000BFFC9
		public Identity(string objectUri)
		{
			this._objectUri = objectUri;
		}

		// Token: 0x06003582 RID: 13698
		public abstract ObjRef CreateObjRef(Type requestedType);

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000C1DD8 File Offset: 0x000BFFD8
		public bool IsFromThisAppDomain
		{
			get
			{
				return this._channelSink == null;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000C1DE3 File Offset: 0x000BFFE3
		// (set) Token: 0x06003585 RID: 13701 RVA: 0x000C1DEB File Offset: 0x000BFFEB
		public IMessageSink ChannelSink
		{
			get
			{
				return this._channelSink;
			}
			set
			{
				this._channelSink = value;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x000C1DF4 File Offset: 0x000BFFF4
		public IMessageSink EnvoySink
		{
			get
			{
				return this._envoySink;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000C1DFC File Offset: 0x000BFFFC
		// (set) Token: 0x06003588 RID: 13704 RVA: 0x000C1E04 File Offset: 0x000C0004
		public string ObjectUri
		{
			get
			{
				return this._objectUri;
			}
			set
			{
				this._objectUri = value;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000C1E0D File Offset: 0x000C000D
		public bool IsConnected
		{
			get
			{
				return this._objectUri != null;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x000C1E18 File Offset: 0x000C0018
		// (set) Token: 0x0600358B RID: 13707 RVA: 0x000C1E20 File Offset: 0x000C0020
		public bool Disposed
		{
			get
			{
				return this._disposed;
			}
			set
			{
				this._disposed = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x000C1E29 File Offset: 0x000C0029
		public DynamicPropertyCollection ClientDynamicProperties
		{
			get
			{
				if (this._clientDynamicProperties == null)
				{
					this._clientDynamicProperties = new DynamicPropertyCollection();
				}
				return this._clientDynamicProperties;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000C1E44 File Offset: 0x000C0044
		public DynamicPropertyCollection ServerDynamicProperties
		{
			get
			{
				if (this._serverDynamicProperties == null)
				{
					this._serverDynamicProperties = new DynamicPropertyCollection();
				}
				return this._serverDynamicProperties;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x000C1E5F File Offset: 0x000C005F
		public bool HasClientDynamicSinks
		{
			get
			{
				return this._clientDynamicProperties != null && this._clientDynamicProperties.HasProperties;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x000C1E76 File Offset: 0x000C0076
		public bool HasServerDynamicSinks
		{
			get
			{
				return this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties;
			}
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000C1E8D File Offset: 0x000C008D
		public void NotifyClientDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._clientDynamicProperties != null && this._clientDynamicProperties.HasProperties)
			{
				this._clientDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000C1EB4 File Offset: 0x000C00B4
		public void NotifyServerDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties)
			{
				this._serverDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x040024A0 RID: 9376
		protected string _objectUri;

		// Token: 0x040024A1 RID: 9377
		protected IMessageSink _channelSink;

		// Token: 0x040024A2 RID: 9378
		protected IMessageSink _envoySink;

		// Token: 0x040024A3 RID: 9379
		private DynamicPropertyCollection _clientDynamicProperties;

		// Token: 0x040024A4 RID: 9380
		private DynamicPropertyCollection _serverDynamicProperties;

		// Token: 0x040024A5 RID: 9381
		protected ObjRef _objRef;

		// Token: 0x040024A6 RID: 9382
		private bool _disposed;
	}
}
