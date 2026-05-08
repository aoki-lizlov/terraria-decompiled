using System;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x0200011C RID: 284
	[Flags]
	public enum CommandRequirement
	{
		// Token: 0x04001564 RID: 5476
		SinglePlayer = 1,
		// Token: 0x04001565 RID: 5477
		MultiplayerClient = 2,
		// Token: 0x04001566 RID: 5478
		MultiplayerRPC = 4,
		// Token: 0x04001567 RID: 5479
		LocalServer = 8,
		// Token: 0x04001568 RID: 5480
		ClientAuthority = 5,
		// Token: 0x04001569 RID: 5481
		AnyAuthority = 13,
		// Token: 0x0400156A RID: 5482
		Client = 3,
		// Token: 0x0400156B RID: 5483
		Local = 11,
		// Token: 0x0400156C RID: 5484
		All = 15
	}
}
