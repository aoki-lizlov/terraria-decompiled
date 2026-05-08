using System;
using System.Diagnostics;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.Base
{
	// Token: 0x02000163 RID: 355
	public abstract class NetSocialModule : ISocialModule
	{
		// Token: 0x06001D93 RID: 7571
		public abstract void Initialize();

		// Token: 0x06001D94 RID: 7572
		public abstract void Shutdown();

		// Token: 0x06001D95 RID: 7573
		public abstract void Close(RemoteAddress address);

		// Token: 0x06001D96 RID: 7574
		public abstract bool IsConnected(RemoteAddress address);

		// Token: 0x06001D97 RID: 7575
		public abstract void Connect(RemoteAddress address);

		// Token: 0x06001D98 RID: 7576
		public abstract bool Send(RemoteAddress address, byte[] data, int length);

		// Token: 0x06001D99 RID: 7577
		public abstract int Receive(RemoteAddress address, byte[] data, int offset, int length);

		// Token: 0x06001D9A RID: 7578
		public abstract bool IsDataAvailable(RemoteAddress address);

		// Token: 0x06001D9B RID: 7579
		public abstract void LaunchLocalServer(Process process, ServerMode mode);

		// Token: 0x06001D9C RID: 7580
		public abstract bool CanInvite();

		// Token: 0x06001D9D RID: 7581
		public abstract void OpenInviteInterface();

		// Token: 0x06001D9E RID: 7582
		public abstract void CancelJoin();

		// Token: 0x06001D9F RID: 7583
		public abstract bool StartListening(SocketConnectionAccepted callback);

		// Token: 0x06001DA0 RID: 7584
		public abstract void StopListening();

		// Token: 0x06001DA1 RID: 7585
		public abstract ulong GetLobbyId();

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0000357B File Offset: 0x0000177B
		protected NetSocialModule()
		{
		}
	}
}
