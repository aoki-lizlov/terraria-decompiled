using System;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005BA RID: 1466
	public interface IChatCommand
	{
		// Token: 0x060039F0 RID: 14832
		void ProcessIncomingMessage(string text, byte clientId);

		// Token: 0x060039F1 RID: 14833
		void ProcessOutgoingMessage(ChatMessage message);
	}
}
