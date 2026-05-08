using System;

namespace Terraria.Chat
{
	// Token: 0x020005B8 RID: 1464
	public interface IChatProcessor
	{
		// Token: 0x060039ED RID: 14829
		void ProcessIncomingMessage(ChatMessage message, int clientId);

		// Token: 0x060039EE RID: 14830
		ChatMessage CreateOutgoingMessage(string text);
	}
}
