using System;

namespace rail
{
	// Token: 0x020000BC RID: 188
	public class CreateGameServerResult : EventBase
	{
		// Token: 0x06001703 RID: 5891 RVA: 0x00010B03 File Offset: 0x0000ED03
		public CreateGameServerResult()
		{
		}

		// Token: 0x040001FD RID: 509
		public RailID game_server_id = new RailID();
	}
}
