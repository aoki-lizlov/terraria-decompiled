using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.Chat;
using Terraria.GameContent.NetModules;
using Terraria.Localization;
using Terraria.Net;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x0200011E RID: 286
	public class DebugCommandProcessor
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x004FABAF File Offset: 0x004F8DAF
		public IEnumerable<IDebugCommand> Commands
		{
			get
			{
				return this._commands.Values;
			}
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x004FABBC File Offset: 0x004F8DBC
		public DebugCommandProcessor()
		{
			if (DebugOptions.enableDebugCommands)
			{
				this.AddAttributeCommandsFromType(typeof(ToolkitDebugCommands));
			}
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x004FABE8 File Offset: 0x004F8DE8
		public void AddAttributeCommandsFromType(Type type)
		{
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				DebugCommandAttribute attribute = AttributeUtilities.GetAttribute<DebugCommandAttribute>(methodInfo);
				if (attribute != null)
				{
					IDebugCommand debugCommand = attribute.ToDebugCommand(methodInfo);
					this.AddCommand(debugCommand);
				}
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x004FAC2B File Offset: 0x004F8E2B
		public void AddCommand(IDebugCommand debugCommand)
		{
			this._commands[debugCommand.Name.ToLower()] = debugCommand;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x004FAC44 File Offset: 0x004F8E44
		public bool Process(byte playerId, string message)
		{
			return this.Process(new DebugMessage(playerId, message));
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x004FAC54 File Offset: 0x004F8E54
		public bool Process(DebugMessage message)
		{
			if (!DebugOptions.enableDebugCommands && !message.CommandName.Equals("toggledebugcommands"))
			{
				return false;
			}
			IDebugCommand debugCommand;
			if (!this._commands.TryGetValue(message.CommandName, out debugCommand))
			{
				return (int)message.Author == Main.myPlayer && this.TryProcessMemo(message);
			}
			if ((debugCommand.Requirements & CommandRequirement.MultiplayerRPC) != (CommandRequirement)0 && Main.netMode == 1)
			{
				NetPacket netPacket = NetDebugModule.Serialize(message);
				NetManager.Instance.SendToServer(netPacket);
				return true;
			}
			if (!DebugCommandProcessor.CanRunCommandLocally((int)message.Author, debugCommand.Requirements))
			{
				return false;
			}
			bool flag = debugCommand.Process(message);
			if (!flag && debugCommand.HelpText != null)
			{
				message.Reply(debugCommand.HelpText);
			}
			if ((DebugOptions.Shared_ReportCommandUsage || debugCommand.Name == "showdebug") && flag && Main.netMode != 0)
			{
				string text = ((message.Author == byte.MaxValue) ? "server" : Main.player[(int)message.Author].name);
				string text2 = string.Format("{0} debugged: /{1} {2}", text, message.CommandName, message.Arguments);
				if (Main.netMode != 1)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text2), new Color(250, 250, 0), -1);
				}
				else
				{
					ChatHelper.SendChatMessageFromClient(new ChatMessage(text2));
				}
			}
			return true;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x004FAD9A File Offset: 0x004F8F9A
		public bool ExecuteSubMessage(DebugMessage baseMessage, string newMessage)
		{
			return this.Process(baseMessage.CreateSubMessage(newMessage));
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x004FADAC File Offset: 0x004F8FAC
		private static bool CanRunCommandLocally(int playerId, CommandRequirement requirements)
		{
			return (Main.netMode == 0 && (requirements & CommandRequirement.SinglePlayer) != (CommandRequirement)0) || (Main.netMode == 1 && (requirements & CommandRequirement.MultiplayerClient) != (CommandRequirement)0) || (Main.netMode == 2 && (requirements & CommandRequirement.LocalServer) != (CommandRequirement)0 && playerId == 255) || (Main.netMode == 2 && (requirements & CommandRequirement.MultiplayerRPC) != (CommandRequirement)0 && playerId < 255);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x004FAE00 File Offset: 0x004F9000
		private bool TryProcessMemo(DebugMessage message)
		{
			string text = Path.Combine(DebugCommandProcessor.MemoCommandsPath, message.CommandName.ToLower() + ".txt");
			if (!File.Exists(text))
			{
				return false;
			}
			try
			{
				string[] array = message.Arguments.Split(new char[] { ' ' });
				foreach (string text2 in File.ReadAllLines(text))
				{
					string text3 = text2;
					object[] array3 = array;
					this.ExecuteSubMessage(message, string.Format(text3, array3));
				}
			}
			catch (FormatException)
			{
				message.ReplyError("Memo formatting error. Perhaps you forgot to pass arguments?");
			}
			return true;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x004FAEA0 File Offset: 0x004F90A0
		public static void OpenMemo(string name)
		{
			Utils.TryCreatingDirectory(DebugCommandProcessor.MemoCommandsPath);
			string text = Path.Combine(DebugCommandProcessor.MemoCommandsPath, name.ToLower() + ".txt");
			if (!File.Exists(text))
			{
				File.WriteAllBytes(text, new byte[0]);
			}
			global::System.Diagnostics.Process.Start(new ProcessStartInfo(text)
			{
				UseShellExecute = true
			});
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x004FAEFA File Offset: 0x004F90FA
		// Note: this type is marked as 'beforefieldinit'.
		static DebugCommandProcessor()
		{
		}

		// Token: 0x04001571 RID: 5489
		private readonly Dictionary<string, IDebugCommand> _commands = new Dictionary<string, IDebugCommand>();

		// Token: 0x04001572 RID: 5490
		private static string MemoCommandsPath = Path.Combine(Main.SavePath, "MemoCommands");
	}
}
