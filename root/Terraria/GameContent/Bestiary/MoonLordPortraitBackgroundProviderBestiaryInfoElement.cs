using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000352 RID: 850
	public class MoonLordPortraitBackgroundProviderBestiaryInfoElement : IBestiaryInfoElement, IBestiaryBackgroundImagePathAndColorProvider
	{
		// Token: 0x06002892 RID: 10386 RVA: 0x0000357B File Offset: 0x0000177B
		public MoonLordPortraitBackgroundProviderBestiaryInfoElement()
		{
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x00573995 File Offset: 0x00571B95
		public Asset<Texture2D> GetBackgroundImage()
		{
			return Main.Assets.Request<Texture2D>("Images/MapBG1", 1);
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x005739A7 File Offset: 0x00571BA7
		public Color? GetBackgroundColor()
		{
			return new Color?(Color.Black);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}
	}
}
