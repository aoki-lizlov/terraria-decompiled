using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005C1 RID: 1473
	[ChatCommand("Emoji")]
	public class EmojiCommand : IChatCommand, ICommandAliasProvider
	{
		// Token: 0x06003A07 RID: 14855 RVA: 0x006548F8 File Offset: 0x00652AF8
		public EmojiCommand()
		{
			this.Initialize();
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x00654914 File Offset: 0x00652B14
		public void Initialize()
		{
			this._byName.Clear();
			for (int i = 0; i < EmoteID.Count; i++)
			{
				LocalizedText emojiName = Lang.GetEmojiName(i);
				if (emojiName != LocalizedText.Empty)
				{
					this._byName[emojiName] = i;
				}
			}
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x00654958 File Offset: 0x00652B58
		public void PrepareAliases(ChatCommandProcessor commandProcessor)
		{
			for (int i = 0; i < EmoteID.Count; i++)
			{
				string name = EmoteID.Search.GetName(i);
				commandProcessor.AddAlias(Language.GetText("EmojiCommand." + name), () => string.Format("{0} {1}", Language.GetTextValue("ChatCommand.Emoji_1"), Language.GetTextValue("EmojiName." + name)));
			}
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessIncomingMessage(string text, byte clientId)
		{
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x006549B4 File Offset: 0x00652BB4
		public void ProcessOutgoingMessage(ChatMessage message)
		{
			if (Main.netMode != 2 && Main.LocalPlayer.dead)
			{
				message.Consume();
				return;
			}
			int num = -1;
			if (int.TryParse(message.Text, out num))
			{
				if (num < 0 || num >= EmoteID.Count)
				{
					return;
				}
			}
			else
			{
				num = -1;
			}
			if (num == -1)
			{
				foreach (LocalizedText localizedText in this._byName.Keys)
				{
					if (localizedText.EqualsCommand(message.Text))
					{
						num = this._byName[localizedText];
						break;
					}
				}
			}
			if (num != -1)
			{
				if (Main.netMode == 0)
				{
					EmoteBubble.NewBubble(num, new WorldUIAnchor(Main.LocalPlayer), 360);
					EmoteBubble.CheckForNPCsToReactToEmoteBubble(num, Main.LocalPlayer);
				}
				else
				{
					NetMessage.SendData(120, -1, -1, null, Main.myPlayer, (float)num, 0f, 0f, 0, 0, 0);
				}
			}
			message.Consume();
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x00654AB4 File Offset: 0x00652CB4
		public void PrintWarning(string text)
		{
			throw new Exception("This needs localized text!");
		}

		// Token: 0x04005DC8 RID: 24008
		public const int PlayerEmojiDuration = 360;

		// Token: 0x04005DC9 RID: 24009
		private readonly Dictionary<LocalizedText, int> _byName = new Dictionary<LocalizedText, int>();

		// Token: 0x020009C8 RID: 2504
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06004A50 RID: 19024 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06004A51 RID: 19025 RVA: 0x006D4091 File Offset: 0x006D2291
			internal string <PrepareAliases>b__0()
			{
				return string.Format("{0} {1}", Language.GetTextValue("ChatCommand.Emoji_1"), Language.GetTextValue("EmojiName." + this.name));
			}

			// Token: 0x040076EC RID: 30444
			public string name;
		}
	}
}
