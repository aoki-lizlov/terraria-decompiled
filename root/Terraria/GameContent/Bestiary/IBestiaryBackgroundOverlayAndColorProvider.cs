using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000344 RID: 836
	public interface IBestiaryBackgroundOverlayAndColorProvider
	{
		// Token: 0x06002858 RID: 10328
		Asset<Texture2D> GetBackgroundOverlayImage();

		// Token: 0x06002859 RID: 10329
		Color? GetBackgroundOverlayColor();

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600285A RID: 10330
		float DisplayPriority { get; }
	}
}
