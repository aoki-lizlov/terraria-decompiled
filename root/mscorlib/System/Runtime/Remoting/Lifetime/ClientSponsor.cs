using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000553 RID: 1363
	[ComVisible(true)]
	public class ClientSponsor : MarshalByRefObject, ISponsor
	{
		// Token: 0x060036DF RID: 14047 RVA: 0x000C724C File Offset: 0x000C544C
		public ClientSponsor()
		{
			this.renewal_time = new TimeSpan(0, 2, 0);
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000C726D File Offset: 0x000C546D
		public ClientSponsor(TimeSpan renewalTime)
		{
			this.renewal_time = renewalTime;
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x000C7287 File Offset: 0x000C5487
		// (set) Token: 0x060036E2 RID: 14050 RVA: 0x000C728F File Offset: 0x000C548F
		public TimeSpan RenewalTime
		{
			get
			{
				return this.renewal_time;
			}
			set
			{
				this.renewal_time = value;
			}
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000C7298 File Offset: 0x000C5498
		public void Close()
		{
			foreach (object obj in this.registered_objects.Values)
			{
				(((MarshalByRefObject)obj).GetLifetimeService() as ILease).Unregister(this);
			}
			this.registered_objects.Clear();
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000C730C File Offset: 0x000C550C
		~ClientSponsor()
		{
			this.Close();
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000C24F0 File Offset: 0x000C06F0
		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService();
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000C7338 File Offset: 0x000C5538
		public bool Register(MarshalByRefObject obj)
		{
			if (this.registered_objects.ContainsKey(obj))
			{
				return false;
			}
			ILease lease = obj.GetLifetimeService() as ILease;
			if (lease == null)
			{
				return false;
			}
			lease.Register(this);
			this.registered_objects.Add(obj, obj);
			return true;
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000C7287 File Offset: 0x000C5487
		public TimeSpan Renewal(ILease lease)
		{
			return this.renewal_time;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000C737B File Offset: 0x000C557B
		public void Unregister(MarshalByRefObject obj)
		{
			if (!this.registered_objects.ContainsKey(obj))
			{
				return;
			}
			(obj.GetLifetimeService() as ILease).Unregister(this);
			this.registered_objects.Remove(obj);
		}

		// Token: 0x0400250C RID: 9484
		private TimeSpan renewal_time;

		// Token: 0x0400250D RID: 9485
		private Hashtable registered_objects = new Hashtable();
	}
}
