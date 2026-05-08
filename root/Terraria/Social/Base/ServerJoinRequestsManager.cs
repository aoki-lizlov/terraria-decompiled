using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Terraria.Social.Base
{
	// Token: 0x02000152 RID: 338
	public class ServerJoinRequestsManager
	{
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06001D2A RID: 7466 RVA: 0x0050139C File Offset: 0x004FF59C
		// (remove) Token: 0x06001D2B RID: 7467 RVA: 0x005013D4 File Offset: 0x004FF5D4
		public event ServerJoinRequestEvent OnRequestAdded
		{
			[CompilerGenerated]
			add
			{
				ServerJoinRequestEvent serverJoinRequestEvent = this.OnRequestAdded;
				ServerJoinRequestEvent serverJoinRequestEvent2;
				do
				{
					serverJoinRequestEvent2 = serverJoinRequestEvent;
					ServerJoinRequestEvent serverJoinRequestEvent3 = (ServerJoinRequestEvent)Delegate.Combine(serverJoinRequestEvent2, value);
					serverJoinRequestEvent = Interlocked.CompareExchange<ServerJoinRequestEvent>(ref this.OnRequestAdded, serverJoinRequestEvent3, serverJoinRequestEvent2);
				}
				while (serverJoinRequestEvent != serverJoinRequestEvent2);
			}
			[CompilerGenerated]
			remove
			{
				ServerJoinRequestEvent serverJoinRequestEvent = this.OnRequestAdded;
				ServerJoinRequestEvent serverJoinRequestEvent2;
				do
				{
					serverJoinRequestEvent2 = serverJoinRequestEvent;
					ServerJoinRequestEvent serverJoinRequestEvent3 = (ServerJoinRequestEvent)Delegate.Remove(serverJoinRequestEvent2, value);
					serverJoinRequestEvent = Interlocked.CompareExchange<ServerJoinRequestEvent>(ref this.OnRequestAdded, serverJoinRequestEvent3, serverJoinRequestEvent2);
				}
				while (serverJoinRequestEvent != serverJoinRequestEvent2);
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06001D2C RID: 7468 RVA: 0x0050140C File Offset: 0x004FF60C
		// (remove) Token: 0x06001D2D RID: 7469 RVA: 0x00501444 File Offset: 0x004FF644
		public event ServerJoinRequestEvent OnRequestRemoved
		{
			[CompilerGenerated]
			add
			{
				ServerJoinRequestEvent serverJoinRequestEvent = this.OnRequestRemoved;
				ServerJoinRequestEvent serverJoinRequestEvent2;
				do
				{
					serverJoinRequestEvent2 = serverJoinRequestEvent;
					ServerJoinRequestEvent serverJoinRequestEvent3 = (ServerJoinRequestEvent)Delegate.Combine(serverJoinRequestEvent2, value);
					serverJoinRequestEvent = Interlocked.CompareExchange<ServerJoinRequestEvent>(ref this.OnRequestRemoved, serverJoinRequestEvent3, serverJoinRequestEvent2);
				}
				while (serverJoinRequestEvent != serverJoinRequestEvent2);
			}
			[CompilerGenerated]
			remove
			{
				ServerJoinRequestEvent serverJoinRequestEvent = this.OnRequestRemoved;
				ServerJoinRequestEvent serverJoinRequestEvent2;
				do
				{
					serverJoinRequestEvent2 = serverJoinRequestEvent;
					ServerJoinRequestEvent serverJoinRequestEvent3 = (ServerJoinRequestEvent)Delegate.Remove(serverJoinRequestEvent2, value);
					serverJoinRequestEvent = Interlocked.CompareExchange<ServerJoinRequestEvent>(ref this.OnRequestRemoved, serverJoinRequestEvent3, serverJoinRequestEvent2);
				}
				while (serverJoinRequestEvent != serverJoinRequestEvent2);
			}
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00501479 File Offset: 0x004FF679
		public ServerJoinRequestsManager()
		{
			this._requests = new List<UserJoinToServerRequest>();
			this.CurrentRequests = new ReadOnlyCollection<UserJoinToServerRequest>(this._requests);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x005014A0 File Offset: 0x004FF6A0
		public void Update()
		{
			for (int i = this._requests.Count - 1; i >= 0; i--)
			{
				if (!this._requests[i].IsValid())
				{
					this.RemoveRequestAtIndex(i);
				}
			}
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x005014E0 File Offset: 0x004FF6E0
		public void Add(UserJoinToServerRequest request)
		{
			for (int i = this._requests.Count - 1; i >= 0; i--)
			{
				if (this._requests[i].Equals(request))
				{
					this.RemoveRequestAtIndex(i);
				}
			}
			this._requests.Add(request);
			request.OnAccepted += delegate
			{
				this.RemoveRequest(request);
			};
			request.OnRejected += delegate
			{
				this.RemoveRequest(request);
			};
			if (this.OnRequestAdded != null)
			{
				this.OnRequestAdded(request);
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x00501594 File Offset: 0x004FF794
		private void RemoveRequestAtIndex(int i)
		{
			UserJoinToServerRequest userJoinToServerRequest = this._requests[i];
			this._requests.RemoveAt(i);
			if (this.OnRequestRemoved != null)
			{
				this.OnRequestRemoved(userJoinToServerRequest);
			}
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x005015CE File Offset: 0x004FF7CE
		private void RemoveRequest(UserJoinToServerRequest request)
		{
			if (this._requests.Remove(request) && this.OnRequestRemoved != null)
			{
				this.OnRequestRemoved(request);
			}
		}

		// Token: 0x0400162D RID: 5677
		private readonly List<UserJoinToServerRequest> _requests;

		// Token: 0x0400162E RID: 5678
		public readonly ReadOnlyCollection<UserJoinToServerRequest> CurrentRequests;

		// Token: 0x0400162F RID: 5679
		[CompilerGenerated]
		private ServerJoinRequestEvent OnRequestAdded;

		// Token: 0x04001630 RID: 5680
		[CompilerGenerated]
		private ServerJoinRequestEvent OnRequestRemoved;

		// Token: 0x02000744 RID: 1860
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x060040C1 RID: 16577 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x060040C2 RID: 16578 RVA: 0x0069ED42 File Offset: 0x0069CF42
			internal void <Add>b__0()
			{
				this.<>4__this.RemoveRequest(this.request);
			}

			// Token: 0x060040C3 RID: 16579 RVA: 0x0069ED42 File Offset: 0x0069CF42
			internal void <Add>b__1()
			{
				this.<>4__this.RemoveRequest(this.request);
			}

			// Token: 0x040069C8 RID: 27080
			public ServerJoinRequestsManager <>4__this;

			// Token: 0x040069C9 RID: 27081
			public UserJoinToServerRequest request;
		}
	}
}
