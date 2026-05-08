using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000558 RID: 1368
	internal class LeaseManager
	{
		// Token: 0x0600370B RID: 14091 RVA: 0x000C77B0 File Offset: 0x000C59B0
		public void SetPollTime(TimeSpan timeSpan)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				if (this._timer != null)
				{
					this._timer.Change(timeSpan, timeSpan);
				}
			}
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000C7808 File Offset: 0x000C5A08
		public void TrackLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				identity.Lease.Activate();
				this._objects.Add(identity);
				if (this._timer == null)
				{
					this.StartManager();
				}
			}
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000C7870 File Offset: 0x000C5A70
		public void StopTrackingLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				this._objects.Remove(identity);
			}
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000C78BC File Offset: 0x000C5ABC
		public void StartManager()
		{
			this._timer = new Timer(new TimerCallback(this.ManageLeases), null, LifetimeServices.LeaseManagerPollTime, LifetimeServices.LeaseManagerPollTime);
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000C78E0 File Offset: 0x000C5AE0
		public void StopManager()
		{
			Timer timer = this._timer;
			this._timer = null;
			if (timer != null)
			{
				timer.Dispose();
			}
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000C7904 File Offset: 0x000C5B04
		public void ManageLeases(object state)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				int i = 0;
				while (i < this._objects.Count)
				{
					ServerIdentity serverIdentity = (ServerIdentity)this._objects[i];
					serverIdentity.Lease.UpdateState();
					if (serverIdentity.Lease.CurrentState == LeaseState.Expired)
					{
						this._objects.RemoveAt(i);
						serverIdentity.OnLifetimeExpired();
					}
					else
					{
						i++;
					}
				}
				if (this._objects.Count == 0)
				{
					this.StopManager();
				}
			}
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000C79AC File Offset: 0x000C5BAC
		public LeaseManager()
		{
		}

		// Token: 0x04002516 RID: 9494
		private ArrayList _objects = new ArrayList();

		// Token: 0x04002517 RID: 9495
		private Timer _timer;
	}
}
