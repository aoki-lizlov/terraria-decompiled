using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI
{
	// Token: 0x020000F0 RID: 240
	public interface INetDiagnosticsUI
	{
		// Token: 0x0600191B RID: 6427
		void Reset();

		// Token: 0x0600191C RID: 6428
		void Draw(SpriteBatch spriteBatch);

		// Token: 0x0600191D RID: 6429
		void CountReadMessage(int messageId, int messageLength);

		// Token: 0x0600191E RID: 6430
		void CountSentMessage(int messageId, int messageLength);

		// Token: 0x0600191F RID: 6431
		void CountReadModuleMessage(int moduleMessageId, int messageLength);

		// Token: 0x06001920 RID: 6432
		void CountSentModuleMessage(int moduleMessageId, int messageLength);

		// Token: 0x06001921 RID: 6433
		void RotateSendRecvCounters();

		// Token: 0x06001922 RID: 6434
		void GetLastSentRecvBytes(out int sent, out int recv);
	}
}
