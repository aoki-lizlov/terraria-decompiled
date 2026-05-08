using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Terraria.Social.Base
{
	// Token: 0x02000153 RID: 339
	public abstract class UserJoinToServerRequest
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x005015F2 File Offset: 0x004FF7F2
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x005015FA File Offset: 0x004FF7FA
		internal string UserDisplayName
		{
			[CompilerGenerated]
			get
			{
				return this.<UserDisplayName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UserDisplayName>k__BackingField = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00501603 File Offset: 0x004FF803
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x0050160B File Offset: 0x004FF80B
		internal string UserFullIdentifier
		{
			[CompilerGenerated]
			get
			{
				return this.<UserFullIdentifier>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UserFullIdentifier>k__BackingField = value;
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06001D37 RID: 7479 RVA: 0x00501614 File Offset: 0x004FF814
		// (remove) Token: 0x06001D38 RID: 7480 RVA: 0x0050164C File Offset: 0x004FF84C
		public event Action OnAccepted
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnAccepted;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnAccepted, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnAccepted;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnAccepted, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06001D39 RID: 7481 RVA: 0x00501684 File Offset: 0x004FF884
		// (remove) Token: 0x06001D3A RID: 7482 RVA: 0x005016BC File Offset: 0x004FF8BC
		public event Action OnRejected
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnRejected;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnRejected, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnRejected;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnRejected, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x005016F1 File Offset: 0x004FF8F1
		public UserJoinToServerRequest(string userDisplayName, string fullIdentifier)
		{
			this.UserDisplayName = userDisplayName;
			this.UserFullIdentifier = fullIdentifier;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00501707 File Offset: 0x004FF907
		public void Accept()
		{
			if (this.OnAccepted != null)
			{
				this.OnAccepted();
			}
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0050171C File Offset: 0x004FF91C
		public void Reject()
		{
			if (this.OnRejected != null)
			{
				this.OnRejected();
			}
		}

		// Token: 0x06001D3E RID: 7486
		public abstract bool IsValid();

		// Token: 0x06001D3F RID: 7487
		public abstract string GetUserWrapperText();

		// Token: 0x04001631 RID: 5681
		[CompilerGenerated]
		private string <UserDisplayName>k__BackingField;

		// Token: 0x04001632 RID: 5682
		[CompilerGenerated]
		private string <UserFullIdentifier>k__BackingField;

		// Token: 0x04001633 RID: 5683
		[CompilerGenerated]
		private Action OnAccepted;

		// Token: 0x04001634 RID: 5684
		[CompilerGenerated]
		private Action OnRejected;
	}
}
