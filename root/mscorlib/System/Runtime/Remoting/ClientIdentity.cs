using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000532 RID: 1330
	internal class ClientIdentity : Identity
	{
		// Token: 0x06003592 RID: 13714 RVA: 0x000C1EDB File Offset: 0x000C00DB
		public ClientIdentity(string objectUri, ObjRef objRef)
			: base(objectUri)
		{
			this._objRef = objRef;
			this._envoySink = ((this._objRef.EnvoyInfo != null) ? this._objRef.EnvoyInfo.EnvoySinks : null);
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000C1F11 File Offset: 0x000C0111
		// (set) Token: 0x06003594 RID: 13716 RVA: 0x000C1F2A File Offset: 0x000C012A
		public MarshalByRefObject ClientProxy
		{
			get
			{
				WeakReference proxyReference = this._proxyReference;
				return (MarshalByRefObject)((proxyReference != null) ? proxyReference.Target : null);
			}
			set
			{
				this._proxyReference = new WeakReference(value);
			}
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000C1F38 File Offset: 0x000C0138
		public override ObjRef CreateObjRef(Type requestedType)
		{
			return this._objRef;
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x000C1F40 File Offset: 0x000C0140
		public string TargetUri
		{
			get
			{
				return this._objRef.URI;
			}
		}

		// Token: 0x040024A7 RID: 9383
		private WeakReference _proxyReference;
	}
}
