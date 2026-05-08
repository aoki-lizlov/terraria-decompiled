using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI
{
	// Token: 0x020000F1 RID: 241
	public class EmptyDiagnosticsUI : INetDiagnosticsUI
	{
		// Token: 0x06001923 RID: 6435 RVA: 0x00009E46 File Offset: 0x00008046
		public void Reset()
		{
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00009E46 File Offset: 0x00008046
		public void CountReadMessage(int messageId, int messageLength)
		{
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00009E46 File Offset: 0x00008046
		public void CountSentMessage(int messageId, int messageLength)
		{
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00009E46 File Offset: 0x00008046
		public void CountReadModuleMessage(int moduleMessageId, int messageLength)
		{
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00009E46 File Offset: 0x00008046
		public void CountSentModuleMessage(int moduleMessageId, int messageLength)
		{
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00009E46 File Offset: 0x00008046
		public void Draw(SpriteBatch spriteBatch)
		{
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00009E46 File Offset: 0x00008046
		public void RotateSendRecvCounters()
		{
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x004E7878 File Offset: 0x004E5A78
		public void GetLastSentRecvBytes(out int sent, out int recv)
		{
			sent = 0;
			recv = 0;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0000357B File Offset: 0x0000177B
		public EmptyDiagnosticsUI()
		{
		}
	}
}
