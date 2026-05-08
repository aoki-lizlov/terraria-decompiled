using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.UI.Chat;
using Terraria.Localization;
using Terraria.Net;

namespace Terraria.Chat
{
	// Token: 0x020005B6 RID: 1462
	public static class ChatHelper
	{
		// Token: 0x060039D2 RID: 14802 RVA: 0x0065417C File Offset: 0x0065237C
		public static void DisplayMessageOnClient(NetworkText text, Color color, int playerId)
		{
			if (Main.dedServ)
			{
				NetPacket netPacket = NetTextModule.SerializeServerMessage(text, color, byte.MaxValue);
				NetManager.Instance.SendToClient(netPacket, playerId);
				return;
			}
			ChatHelper.DisplayMessage(text, color, byte.MaxValue);
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x006541B6 File Offset: 0x006523B6
		public static void SendChatMessageToClient(NetworkText text, Color color, int playerId)
		{
			ChatHelper.SendChatMessageToClientAs(byte.MaxValue, text, color, playerId);
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x006541C8 File Offset: 0x006523C8
		public static void SendChatMessageToClientAs(byte messageAuthor, NetworkText text, Color color, int playerId)
		{
			if (Main.dedServ)
			{
				NetPacket netPacket = NetTextModule.SerializeServerMessage(text, color, messageAuthor);
				NetManager.Instance.SendToClient(netPacket, playerId);
			}
			if (playerId == Main.myPlayer)
			{
				ChatHelper.DisplayMessage(text, color, messageAuthor);
			}
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x00654201 File Offset: 0x00652401
		public static void BroadcastChatMessage(NetworkText text, Color color, int excludedPlayer = -1)
		{
			ChatHelper.BroadcastChatMessageAs(byte.MaxValue, text, color, excludedPlayer);
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x00654210 File Offset: 0x00652410
		public static void BroadcastChatMessageAs(byte messageAuthor, NetworkText text, Color color, int excludedPlayer = -1)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (Main.dedServ)
			{
				NetPacket netPacket = NetTextModule.SerializeServerMessage(text, color, messageAuthor);
				NetManager.Instance.Broadcast(netPacket, new NetManager.BroadcastCondition(ChatHelper.OnlySendToPlayersWhoAreLoggedIn), excludedPlayer);
				return;
			}
			if (excludedPlayer != Main.myPlayer)
			{
				ChatHelper.DisplayMessage(text, color, messageAuthor);
			}
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x0065425F File Offset: 0x0065245F
		public static bool OnlySendToPlayersWhoAreLoggedIn(int clientIndex)
		{
			return Netplay.Clients[clientIndex].State == 10;
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x00654274 File Offset: 0x00652474
		public static void SendChatMessageFromClient(ChatMessage message)
		{
			if (!message.IsConsumed)
			{
				NetPacket netPacket = NetTextModule.SerializeClientMessage(message);
				NetManager.Instance.SendToServer(netPacket);
			}
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x0065429C File Offset: 0x0065249C
		public static void DisplayMessage(NetworkText text, Color color, byte messageAuthor)
		{
			string text2 = text.ToString();
			if (messageAuthor < 255)
			{
				Main.player[(int)messageAuthor].chatOverhead.NewMessage(text2, Main.PlayerOverheadChatMessageDisplayTime);
				Main.player[(int)messageAuthor].chatOverhead.color = color;
				text2 = NameTagHandler.GenerateTag(Main.player[(int)messageAuthor].name) + " " + text2;
			}
			if (ChatHelper.ShouldCacheMessage())
			{
				ChatHelper.CacheMessage(text2, color);
				return;
			}
			Main.NewTextMultiline(text2, false, color, -1);
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x00654316 File Offset: 0x00652516
		private static void CacheMessage(string message, Color color)
		{
			ChatHelper._cachedMessages.Add(new Tuple<string, Color>(message, color));
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x0065432C File Offset: 0x0065252C
		public static void ShowCachedMessages()
		{
			List<Tuple<string, Color>> cachedMessages = ChatHelper._cachedMessages;
			lock (cachedMessages)
			{
				foreach (Tuple<string, Color> tuple in ChatHelper._cachedMessages)
				{
					Main.NewTextMultiline(tuple.Item1, false, tuple.Item2, -1);
				}
			}
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x006543B0 File Offset: 0x006525B0
		public static void ClearDelayedMessagesCache()
		{
			ChatHelper._cachedMessages.Clear();
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x006543BC File Offset: 0x006525BC
		private static bool ShouldCacheMessage()
		{
			return Main.netMode == 1 && Main.gameMenu;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x006543CD File Offset: 0x006525CD
		// Note: this type is marked as 'beforefieldinit'.
		static ChatHelper()
		{
		}

		// Token: 0x04005DBE RID: 23998
		private static List<Tuple<string, Color>> _cachedMessages = new List<Tuple<string, Color>>();
	}
}
