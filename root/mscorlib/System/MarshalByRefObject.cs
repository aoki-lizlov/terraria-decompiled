using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;

namespace System
{
	// Token: 0x02000211 RID: 529
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class MarshalByRefObject
	{
		// Token: 0x060019F1 RID: 6641 RVA: 0x000025BE File Offset: 0x000007BE
		protected MarshalByRefObject()
		{
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00060888 File Offset: 0x0005EA88
		internal Identity GetObjectIdentity(MarshalByRefObject obj, out bool IsClient)
		{
			IsClient = false;
			Identity identity;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				identity = RemotingServices.GetRealProxy(obj).ObjectIdentity;
				IsClient = true;
			}
			else
			{
				identity = obj.ObjectIdentity;
			}
			return identity;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x000608BB File Offset: 0x0005EABB
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x000608C3 File Offset: 0x0005EAC3
		internal ServerIdentity ObjectIdentity
		{
			get
			{
				return this._identity;
			}
			set
			{
				this._identity = value;
			}
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000608CC File Offset: 0x0005EACC
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public virtual ObjRef CreateObjRef(Type requestedType)
		{
			if (this._identity == null)
			{
				throw new RemotingException(Locale.GetText("No remoting information was found for the object."));
			}
			return this._identity.CreateObjRef(requestedType);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000608F2 File Offset: 0x0005EAF2
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public object GetLifetimeService()
		{
			if (this._identity == null)
			{
				return null;
			}
			return this._identity.Lease;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00060909 File Offset: 0x0005EB09
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public virtual object InitializeLifetimeService()
		{
			if (this._identity != null && this._identity.Lease != null)
			{
				return this._identity.Lease;
			}
			return new Lease();
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00060934 File Offset: 0x0005EB34
		protected MarshalByRefObject MemberwiseClone(bool cloneIdentity)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)base.MemberwiseClone();
			if (!cloneIdentity)
			{
				marshalByRefObject._identity = null;
			}
			return marshalByRefObject;
		}

		// Token: 0x04001603 RID: 5635
		[NonSerialized]
		private ServerIdentity _identity;
	}
}
