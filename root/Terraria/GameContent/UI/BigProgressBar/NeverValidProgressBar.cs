using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000392 RID: 914
	public class NeverValidProgressBar : IBigProgressBar
	{
		// Token: 0x060029F1 RID: 10737 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			return false;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00009E46 File Offset: 0x00008046
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x0000357B File Offset: 0x0000177B
		public NeverValidProgressBar()
		{
		}
	}
}
