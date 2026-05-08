using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ReLogic.Utilities;
using Terraria.Chat.Commands;
using Terraria.Localization;

namespace Terraria.Chat
{
	// Token: 0x020005B4 RID: 1460
	public class ChatCommandProcessor : IChatProcessor
	{
		// Token: 0x060039C9 RID: 14793 RVA: 0x00653E28 File Offset: 0x00652028
		public ChatCommandProcessor AddCommand<T>() where T : IChatCommand, new()
		{
			ChatCommandAttribute cacheableAttribute = AttributeUtilities.GetCacheableAttribute<T, ChatCommandAttribute>();
			string commandKey = "ChatCommand." + cacheableAttribute.Name;
			ChatCommandId chatCommandId = ChatCommandId.FromType<T>();
			this._commands[chatCommandId] = new T();
			if (Language.Exists(commandKey))
			{
				this._localizedCommands.Add(Language.GetText(commandKey), chatCommandId);
			}
			else
			{
				commandKey += "_";
				foreach (LocalizedText localizedText in Language.FindAll((string key, LocalizedText text) => key.StartsWith(commandKey)))
				{
					this._localizedCommands.Add(localizedText, chatCommandId);
				}
			}
			return this;
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x00653EE8 File Offset: 0x006520E8
		public void AddAlias(LocalizedText alias, Func<string> result)
		{
			this._aliases[alias] = result;
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x00653EF8 File Offset: 0x006520F8
		public void PrepareAliases()
		{
			foreach (IChatCommand chatCommand in this._commands.Values)
			{
				if (chatCommand is ICommandAliasProvider)
				{
					((ICommandAliasProvider)chatCommand).PrepareAliases(this);
				}
			}
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x00653F60 File Offset: 0x00652160
		public ChatCommandProcessor AddDefaultCommand<T>() where T : IChatCommand, new()
		{
			this.AddCommand<T>();
			ChatCommandId chatCommandId = ChatCommandId.FromType<T>();
			this._defaultCommand = this._commands[chatCommandId];
			return this;
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x00653F90 File Offset: 0x00652190
		private static bool ParseCommandPrefix<T>(string text, Dictionary<LocalizedText, T> commands, out string remainder, out T value)
		{
			foreach (KeyValuePair<LocalizedText, T> keyValuePair in commands)
			{
				if (keyValuePair.Key.ParseCommandPrefix(text, out remainder))
				{
					value = keyValuePair.Value;
					return true;
				}
			}
			remainder = "";
			value = default(T);
			return false;
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x0065400C File Offset: 0x0065220C
		public ChatMessage CreateOutgoingMessage(string text)
		{
			ChatMessage chatMessage = new ChatMessage(text);
			string text2;
			ChatCommandId chatCommandId;
			if (ChatCommandProcessor.ParseCommandPrefix<ChatCommandId>(chatMessage.Text, this._localizedCommands, out text2, out chatCommandId))
			{
				chatMessage.Text = text2;
				chatMessage.SetCommand(chatCommandId);
				this._commands[chatCommandId].ProcessOutgoingMessage(chatMessage);
				return chatMessage;
			}
			Func<string> func;
			if (ChatCommandProcessor.ParseCommandPrefix<Func<string>>(chatMessage.Text, this._aliases, out text2, out func))
			{
				return this.CreateOutgoingMessage(func());
			}
			return chatMessage;
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x00654080 File Offset: 0x00652280
		public void ProcessIncomingMessage(ChatMessage message, int clientId)
		{
			IChatCommand chatCommand;
			if (this._commands.TryGetValue(message.CommandId, out chatCommand))
			{
				chatCommand.ProcessIncomingMessage(message.Text, (byte)clientId);
				message.Consume();
				return;
			}
			if (this._defaultCommand != null)
			{
				this._defaultCommand.ProcessIncomingMessage(message.Text, (byte)clientId);
				message.Consume();
			}
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x006540D8 File Offset: 0x006522D8
		public ChatCommandProcessor()
		{
		}

		// Token: 0x04005DB5 RID: 23989
		private readonly Dictionary<LocalizedText, ChatCommandId> _localizedCommands = new Dictionary<LocalizedText, ChatCommandId>();

		// Token: 0x04005DB6 RID: 23990
		private readonly Dictionary<ChatCommandId, IChatCommand> _commands = new Dictionary<ChatCommandId, IChatCommand>();

		// Token: 0x04005DB7 RID: 23991
		private Dictionary<LocalizedText, Func<string>> _aliases = new Dictionary<LocalizedText, Func<string>>();

		// Token: 0x04005DB8 RID: 23992
		private IChatCommand _defaultCommand;

		// Token: 0x020009C5 RID: 2501
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0<T> where T : IChatCommand, new()
		{
			// Token: 0x06004A46 RID: 19014 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06004A47 RID: 19015 RVA: 0x006D404E File Offset: 0x006D224E
			internal bool <AddCommand>b__0(string key, LocalizedText text)
			{
				return key.StartsWith(this.commandKey);
			}

			// Token: 0x040076E5 RID: 30437
			public string commandKey;
		}
	}
}
