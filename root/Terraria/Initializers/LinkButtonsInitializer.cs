using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Initializers
{
	// Token: 0x0200007F RID: 127
	public class LinkButtonsInitializer
	{
		// Token: 0x0600156F RID: 5487 RVA: 0x004C49B0 File Offset: 0x004C2BB0
		public static void Load()
		{
			List<TitleLinkButton> titleLinks = Main.TitleLinks;
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Discord", "https://discord.gg/terraria", 0));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Instagram", "https://www.instagram.com/terraria_logic/", 1));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Reddit", "https://www.reddit.com/r/Terraria/", 2));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Twitter", "https://twitter.com/Terraria_Logic", 3));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Bluesky", "https://bsky.app/profile/terraria.bsky.social", 4));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Forums", "https://forums.terraria.org/index.php", 5));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Merch", "https://terraria.org/store", 6));
			titleLinks.Add(LinkButtonsInitializer.MakeSimpleButton("TitleLinks.Wiki", "https://terraria.wiki.gg/", 7));
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x004C4A74 File Offset: 0x004C2C74
		private static TitleLinkButton MakeSimpleButton(string textKey, string linkUrl, int horizontalFrameIndex)
		{
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/TitleLinkButtons", 1);
			Rectangle rectangle = asset.Frame(8, 2, horizontalFrameIndex, 0, 0, 0);
			Rectangle rectangle2 = asset.Frame(8, 2, horizontalFrameIndex, 1, 0, 0);
			rectangle.Width--;
			rectangle.Height--;
			rectangle2.Width--;
			rectangle2.Height--;
			return new TitleLinkButton
			{
				TooltipTextKey = textKey,
				LinkUrl = linkUrl,
				FrameWehnSelected = new Rectangle?(rectangle2),
				FrameWhenNotSelected = new Rectangle?(rectangle),
				Image = asset
			};
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0000357B File Offset: 0x0000177B
		public LinkButtonsInitializer()
		{
		}
	}
}
