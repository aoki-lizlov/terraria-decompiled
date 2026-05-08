using System;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x02000120 RID: 288
	public interface IDebugCommand
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001B6E RID: 7022
		string Name { get; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001B6F RID: 7023
		string Description { get; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001B70 RID: 7024
		string HelpText { get; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001B71 RID: 7025
		CommandRequirement Requirements { get; }

		// Token: 0x06001B72 RID: 7026
		bool Process(DebugMessage message);
	}
}
