using System;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000147 RID: 327
	public class FriendsSocialModule : FriendsSocialModule
	{
		// Token: 0x06001CD3 RID: 7379 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Initialize()
		{
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Shutdown()
		{
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x004FFD0D File Offset: 0x004FDF0D
		public override string GetUsername()
		{
			return SteamFriends.GetPersonaName();
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x004FFD14 File Offset: 0x004FDF14
		public override void OpenJoinInterface()
		{
			SteamFriends.ActivateGameOverlay("Friends");
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x004FBBB7 File Offset: 0x004F9DB7
		public FriendsSocialModule()
		{
		}
	}
}
