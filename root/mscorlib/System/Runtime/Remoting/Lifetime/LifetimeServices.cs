using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x0200055B RID: 1371
	[ComVisible(true)]
	public sealed class LifetimeServices
	{
		// Token: 0x06003717 RID: 14103 RVA: 0x000C7A48 File Offset: 0x000C5C48
		static LifetimeServices()
		{
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000025BE File Offset: 0x000007BE
		[Obsolete("Call the static methods directly on this type instead", true)]
		public LifetimeServices()
		{
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000C7AAB File Offset: 0x000C5CAB
		// (set) Token: 0x0600371A RID: 14106 RVA: 0x000C7AB2 File Offset: 0x000C5CB2
		public static TimeSpan LeaseManagerPollTime
		{
			get
			{
				return LifetimeServices._leaseManagerPollTime;
			}
			set
			{
				LifetimeServices._leaseManagerPollTime = value;
				LifetimeServices._leaseManager.SetPollTime(value);
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600371B RID: 14107 RVA: 0x000C7AC5 File Offset: 0x000C5CC5
		// (set) Token: 0x0600371C RID: 14108 RVA: 0x000C7ACC File Offset: 0x000C5CCC
		public static TimeSpan LeaseTime
		{
			get
			{
				return LifetimeServices._leaseTime;
			}
			set
			{
				LifetimeServices._leaseTime = value;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x000C7AD4 File Offset: 0x000C5CD4
		// (set) Token: 0x0600371E RID: 14110 RVA: 0x000C7ADB File Offset: 0x000C5CDB
		public static TimeSpan RenewOnCallTime
		{
			get
			{
				return LifetimeServices._renewOnCallTime;
			}
			set
			{
				LifetimeServices._renewOnCallTime = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x000C7AE3 File Offset: 0x000C5CE3
		// (set) Token: 0x06003720 RID: 14112 RVA: 0x000C7AEA File Offset: 0x000C5CEA
		public static TimeSpan SponsorshipTimeout
		{
			get
			{
				return LifetimeServices._sponsorshipTimeout;
			}
			set
			{
				LifetimeServices._sponsorshipTimeout = value;
			}
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000C7AF2 File Offset: 0x000C5CF2
		internal static void TrackLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.TrackLifetime(identity);
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000C7AFF File Offset: 0x000C5CFF
		internal static void StopTrackingLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.StopTrackingLifetime(identity);
		}

		// Token: 0x0400251F RID: 9503
		private static TimeSpan _leaseManagerPollTime = TimeSpan.FromSeconds(10.0);

		// Token: 0x04002520 RID: 9504
		private static TimeSpan _leaseTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x04002521 RID: 9505
		private static TimeSpan _renewOnCallTime = TimeSpan.FromMinutes(2.0);

		// Token: 0x04002522 RID: 9506
		private static TimeSpan _sponsorshipTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04002523 RID: 9507
		private static LeaseManager _leaseManager = new LeaseManager();
	}
}
