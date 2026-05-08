using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005C4 RID: 1476
	[ChatCommand("Help")]
	public class HelpCommand : IChatCommand
	{
		// Token: 0x06003A14 RID: 14868 RVA: 0x00654B9F File Offset: 0x00652D9F
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			ChatHelper.SendChatMessageToClient(HelpCommand.ComposeMessage(HelpCommand.GetCommandAliasesByID()), HelpCommand.RESPONSE_COLOR, (int)clientId);
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x00654BB8 File Offset: 0x00652DB8
		private static Dictionary<string, List<LocalizedText>> GetCommandAliasesByID()
		{
			LocalizedText[] array = Language.FindAll(Lang.CreateDialogFilter("ChatCommandDescription.", true));
			Dictionary<string, List<LocalizedText>> dictionary = new Dictionary<string, List<LocalizedText>>();
			foreach (LocalizedText localizedText in array)
			{
				string text = localizedText.Key;
				text = text.Replace("ChatCommandDescription.", "");
				int num = text.IndexOf('_');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
				List<LocalizedText> list;
				if (!dictionary.TryGetValue(text, out list))
				{
					list = new List<LocalizedText>();
					dictionary[text] = list;
				}
				list.Add(localizedText);
			}
			return dictionary;
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x00654C4C File Offset: 0x00652E4C
		private static NetworkText ComposeMessage(Dictionary<string, List<LocalizedText>> aliases)
		{
			string text = "";
			for (int i = 0; i < aliases.Count; i++)
			{
				text = string.Concat(new object[] { text, "{", i, "}\n" });
			}
			List<NetworkText> list = new List<NetworkText>();
			foreach (KeyValuePair<string, List<LocalizedText>> keyValuePair in aliases)
			{
				list.Add(Language.GetText("ChatCommandDescription." + keyValuePair.Key).ToNetworkText());
			}
			string text2 = text;
			object[] array = list.ToArray();
			return NetworkText.FromFormattable(text2, array);
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x0000357B File Offset: 0x0000177B
		public HelpCommand()
		{
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x00654D0C File Offset: 0x00652F0C
		// Note: this type is marked as 'beforefieldinit'.
		static HelpCommand()
		{
		}

		// Token: 0x04005DCB RID: 24011
		private static readonly Color RESPONSE_COLOR = ChatColors.ServerMessage;
	}
}
