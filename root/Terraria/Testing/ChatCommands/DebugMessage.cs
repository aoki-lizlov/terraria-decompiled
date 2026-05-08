using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x0200011F RID: 287
	public class DebugMessage
	{
		// Token: 0x06001B65 RID: 7013 RVA: 0x004FAF10 File Offset: 0x004F9110
		public DebugMessage(byte author, string message)
			: this(author, message, new Vector2((float)Main.mouseX, (float)Main.mouseY) + Main.screenPosition)
		{
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x004FAF38 File Offset: 0x004F9138
		private DebugMessage(byte author, string message, Vector2 mousePosition)
		{
			this.CommandName = "";
			this.Arguments = "";
			base..ctor();
			this.MousePosition = mousePosition;
			this.Author = author;
			if (message[0] != '/')
			{
				return;
			}
			string text = message.ToLower();
			int num = text.Length;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == ' ')
				{
					num = i;
					break;
				}
			}
			string text2 = text.Substring(1, num - 1);
			this.CommandName = text2;
			if (text2.Length == 0)
			{
				return;
			}
			if (num < message.Length - 1)
			{
				this.Arguments = message.Substring(num + 1);
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x004FAFDE File Offset: 0x004F91DE
		private DebugMessage(byte author, string commandName, string arguments, Vector2 mousePosition)
		{
			this.CommandName = "";
			this.Arguments = "";
			base..ctor();
			this.Author = author;
			this.CommandName = commandName;
			this.Arguments = arguments;
			this.MousePosition = mousePosition;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x004FB019 File Offset: 0x004F9219
		public void Reply(string message)
		{
			this.DisplayMessage(message, new Color(250, 250, 0));
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x004FB032 File Offset: 0x004F9232
		public void ReplyError(string message)
		{
			this.DisplayMessage(message, new Color(250, 0, 0));
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x004FB047 File Offset: 0x004F9247
		private void DisplayMessage(string message, Color color)
		{
			if (Main.dedServ && this.Author == 255)
			{
				Console.WriteLine(message);
				return;
			}
			ChatHelper.DisplayMessageOnClient(NetworkText.FromLiteral(message), color, (int)this.Author);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x004FB076 File Offset: 0x004F9276
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.CommandName);
			writer.Write(this.Arguments);
			writer.WriteVector2(this.MousePosition);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x004FB09C File Offset: 0x004F929C
		public static DebugMessage Deserialize(byte author, BinaryReader reader)
		{
			string text = reader.ReadString();
			string text2 = reader.ReadString();
			Vector2 vector = reader.ReadVector2();
			return new DebugMessage(author, text, text2, vector);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x004FB0C7 File Offset: 0x004F92C7
		public DebugMessage CreateSubMessage(string newMessage)
		{
			return new DebugMessage(this.Author, newMessage, this.MousePosition);
		}

		// Token: 0x04001573 RID: 5491
		private const char COMMAND_PREFIX = '/';

		// Token: 0x04001574 RID: 5492
		public readonly byte Author;

		// Token: 0x04001575 RID: 5493
		public readonly string CommandName;

		// Token: 0x04001576 RID: 5494
		public readonly string Arguments;

		// Token: 0x04001577 RID: 5495
		public readonly Vector2 MousePosition;
	}
}
