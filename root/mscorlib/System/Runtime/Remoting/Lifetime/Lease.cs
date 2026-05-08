using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000556 RID: 1366
	internal class Lease : MarshalByRefObject, ILease
	{
		// Token: 0x060036F6 RID: 14070 RVA: 0x000C73AC File Offset: 0x000C55AC
		public Lease()
		{
			this._currentState = LeaseState.Initial;
			this._initialLeaseTime = LifetimeServices.LeaseTime;
			this._renewOnCallTime = LifetimeServices.RenewOnCallTime;
			this._sponsorshipTimeout = LifetimeServices.SponsorshipTimeout;
			this._leaseExpireTime = DateTime.UtcNow + this._initialLeaseTime;
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000C73FD File Offset: 0x000C55FD
		public TimeSpan CurrentLeaseTime
		{
			get
			{
				return this._leaseExpireTime - DateTime.UtcNow;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000C740F File Offset: 0x000C560F
		public LeaseState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000C7417 File Offset: 0x000C5617
		public void Activate()
		{
			this._currentState = LeaseState.Active;
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000C7420 File Offset: 0x000C5620
		// (set) Token: 0x060036FB RID: 14075 RVA: 0x000C7428 File Offset: 0x000C5628
		public TimeSpan InitialLeaseTime
		{
			get
			{
				return this._initialLeaseTime;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("InitialLeaseTime property can only be set when the lease is in initial state; state is " + this._currentState.ToString() + ".");
				}
				this._initialLeaseTime = value;
				this._leaseExpireTime = DateTime.UtcNow + this._initialLeaseTime;
				if (value == TimeSpan.Zero)
				{
					this._currentState = LeaseState.Null;
				}
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000C7495 File Offset: 0x000C5695
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x000C749D File Offset: 0x000C569D
		public TimeSpan RenewOnCallTime
		{
			get
			{
				return this._renewOnCallTime;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("RenewOnCallTime property can only be set when the lease is in initial state; state is " + this._currentState.ToString() + ".");
				}
				this._renewOnCallTime = value;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000C74D5 File Offset: 0x000C56D5
		// (set) Token: 0x060036FF RID: 14079 RVA: 0x000C74DD File Offset: 0x000C56DD
		public TimeSpan SponsorshipTimeout
		{
			get
			{
				return this._sponsorshipTimeout;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("SponsorshipTimeout property can only be set when the lease is in initial state; state is " + this._currentState.ToString() + ".");
				}
				this._sponsorshipTimeout = value;
			}
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000C7515 File Offset: 0x000C5715
		public void Register(ISponsor obj)
		{
			this.Register(obj, TimeSpan.Zero);
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000C7524 File Offset: 0x000C5724
		public void Register(ISponsor obj, TimeSpan renewalTime)
		{
			lock (this)
			{
				if (this._sponsors == null)
				{
					this._sponsors = new ArrayList();
				}
				this._sponsors.Add(obj);
			}
			if (renewalTime != TimeSpan.Zero)
			{
				this.Renew(renewalTime);
			}
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000C7590 File Offset: 0x000C5790
		public TimeSpan Renew(TimeSpan renewalTime)
		{
			DateTime dateTime = DateTime.UtcNow + renewalTime;
			if (dateTime > this._leaseExpireTime)
			{
				this._leaseExpireTime = dateTime;
			}
			return this.CurrentLeaseTime;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000C75C4 File Offset: 0x000C57C4
		public void Unregister(ISponsor obj)
		{
			lock (this)
			{
				if (this._sponsors != null)
				{
					for (int i = 0; i < this._sponsors.Count; i++)
					{
						if (this._sponsors[i] == obj)
						{
							this._sponsors.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000C7638 File Offset: 0x000C5838
		internal void UpdateState()
		{
			if (this._currentState != LeaseState.Active)
			{
				return;
			}
			if (this.CurrentLeaseTime > TimeSpan.Zero)
			{
				return;
			}
			if (this._sponsors != null)
			{
				this._currentState = LeaseState.Renewing;
				lock (this)
				{
					this._renewingSponsors = new Queue(this._sponsors);
				}
				this.CheckNextSponsor();
				return;
			}
			this._currentState = LeaseState.Expired;
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000C76B8 File Offset: 0x000C58B8
		private void CheckNextSponsor()
		{
			if (this._renewingSponsors.Count == 0)
			{
				this._currentState = LeaseState.Expired;
				this._renewingSponsors = null;
				return;
			}
			ISponsor sponsor = (ISponsor)this._renewingSponsors.Peek();
			this._renewalDelegate = new Lease.RenewalDelegate(sponsor.Renewal);
			IAsyncResult asyncResult = this._renewalDelegate.BeginInvoke(this, null, null);
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, new WaitOrTimerCallback(this.ProcessSponsorResponse), asyncResult, this._sponsorshipTimeout, true);
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000C7734 File Offset: 0x000C5934
		private void ProcessSponsorResponse(object state, bool timedOut)
		{
			if (!timedOut)
			{
				try
				{
					IAsyncResult asyncResult = (IAsyncResult)state;
					TimeSpan timeSpan = this._renewalDelegate.EndInvoke(asyncResult);
					if (timeSpan != TimeSpan.Zero)
					{
						this.Renew(timeSpan);
						this._currentState = LeaseState.Active;
						this._renewingSponsors = null;
						return;
					}
				}
				catch
				{
				}
			}
			this.Unregister((ISponsor)this._renewingSponsors.Dequeue());
			this.CheckNextSponsor();
		}

		// Token: 0x0400250E RID: 9486
		private DateTime _leaseExpireTime;

		// Token: 0x0400250F RID: 9487
		private LeaseState _currentState;

		// Token: 0x04002510 RID: 9488
		private TimeSpan _initialLeaseTime;

		// Token: 0x04002511 RID: 9489
		private TimeSpan _renewOnCallTime;

		// Token: 0x04002512 RID: 9490
		private TimeSpan _sponsorshipTimeout;

		// Token: 0x04002513 RID: 9491
		private ArrayList _sponsors;

		// Token: 0x04002514 RID: 9492
		private Queue _renewingSponsors;

		// Token: 0x04002515 RID: 9493
		private Lease.RenewalDelegate _renewalDelegate;

		// Token: 0x02000557 RID: 1367
		// (Invoke) Token: 0x06003708 RID: 14088
		private delegate TimeSpan RenewalDelegate(ILease lease);
	}
}
