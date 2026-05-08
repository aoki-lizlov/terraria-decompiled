using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200038A RID: 906
	internal interface IBigProgressBar
	{
		// Token: 0x060029D4 RID: 10708
		bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info);

		// Token: 0x060029D5 RID: 10709
		void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch);
	}
}
