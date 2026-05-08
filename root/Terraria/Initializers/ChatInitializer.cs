using System;
using Terraria.Chat.Commands;
using Terraria.GameContent.UI.Chat;
using Terraria.UI.Chat;

namespace Terraria.Initializers
{
	// Token: 0x02000085 RID: 133
	public static class ChatInitializer
	{
		// Token: 0x0600159A RID: 5530 RVA: 0x004CC094 File Offset: 0x004CA294
		public static void Load()
		{
			ChatManager.Register<ColorTagHandler>(new string[] { "c", "color" });
			ChatManager.Register<ItemTagHandler>(new string[] { "i", "item" });
			ChatManager.Register<NameTagHandler>(new string[] { "n", "name" });
			ChatManager.Register<AchievementTagHandler>(new string[] { "a", "achievement" });
			ChatManager.Register<GlyphTagHandler>(new string[] { "g", "glyph" });
			ChatManager.Register<GlyphTagHandler.GlyphXboxTagHandler>(new string[] { "gx", "glyph" });
			ChatManager.Register<GlyphTagHandler.GlyphPSTagHandler>(new string[] { "gp", "glyph" });
			ChatManager.Register<GlyphTagHandler.GlyphSwitchTagHandler>(new string[] { "gn", "glyph" });
			ChatManager.Commands.AddCommand<PartyChatCommand>().AddCommand<RollCommand>().AddCommand<EmoteCommand>()
				.AddCommand<ListPlayersCommand>()
				.AddCommand<RockPaperScissorsCommand>()
				.AddCommand<EmojiCommand>()
				.AddCommand<HelpCommand>()
				.AddCommand<DeathCommand>()
				.AddCommand<PVPDeathCommand>()
				.AddCommand<AllDeathCommand>()
				.AddCommand<AllPVPDeathCommand>()
				.AddCommand<BossDamageCommand>()
				.AddDefaultCommand<SayChatCommand>();
			ChatManager.Commands.PrepareAliases();
		}
	}
}
