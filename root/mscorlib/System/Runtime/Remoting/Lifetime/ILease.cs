using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000554 RID: 1364
	[ComVisible(true)]
	public interface ILease
	{
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060036E9 RID: 14057
		TimeSpan CurrentLeaseTime { get; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060036EA RID: 14058
		LeaseState CurrentState { get; }

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060036EB RID: 14059
		// (set) Token: 0x060036EC RID: 14060
		TimeSpan InitialLeaseTime { get; set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060036ED RID: 14061
		// (set) Token: 0x060036EE RID: 14062
		TimeSpan RenewOnCallTime { get; set; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060036EF RID: 14063
		// (set) Token: 0x060036F0 RID: 14064
		TimeSpan SponsorshipTimeout { get; set; }

		// Token: 0x060036F1 RID: 14065
		void Register(ISponsor obj);

		// Token: 0x060036F2 RID: 14066
		void Register(ISponsor obj, TimeSpan renewalTime);

		// Token: 0x060036F3 RID: 14067
		TimeSpan Renew(TimeSpan renewalTime);

		// Token: 0x060036F4 RID: 14068
		void Unregister(ISponsor obj);
	}
}
