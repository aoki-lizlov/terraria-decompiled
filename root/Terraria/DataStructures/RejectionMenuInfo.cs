using System;
using Terraria.Audio;

namespace Terraria.DataStructures
{
	// Token: 0x02000587 RID: 1415
	public class RejectionMenuInfo
	{
		// Token: 0x0600380D RID: 14349 RVA: 0x00630783 File Offset: 0x0062E983
		public void DefaultExitAction()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = 0;
			Main.netMode = 0;
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x0000357B File Offset: 0x0000177B
		public RejectionMenuInfo()
		{
		}

		// Token: 0x04005C3A RID: 23610
		public ReturnFromRejectionMenuAction ExitAction;

		// Token: 0x04005C3B RID: 23611
		public string TextToShow;
	}
}
