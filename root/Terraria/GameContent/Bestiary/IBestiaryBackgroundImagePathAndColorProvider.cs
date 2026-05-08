using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000342 RID: 834
	public interface IBestiaryBackgroundImagePathAndColorProvider
	{
		// Token: 0x06002855 RID: 10325
		Asset<Texture2D> GetBackgroundImage();

		// Token: 0x06002856 RID: 10326
		Color? GetBackgroundColor();
	}
}
