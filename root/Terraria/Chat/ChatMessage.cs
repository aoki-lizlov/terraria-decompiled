using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria.Chat.Commands;

namespace Terraria.Chat
{
	// Token: 0x020005B7 RID: 1463
	public sealed class ChatMessage
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x006543D9 File Offset: 0x006525D9
		// (set) Token: 0x060039E0 RID: 14816 RVA: 0x006543E1 File Offset: 0x006525E1
		public ChatCommandId CommandId
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CommandId>k__BackingField = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x006543EA File Offset: 0x006525EA
		// (set) Token: 0x060039E2 RID: 14818 RVA: 0x006543F2 File Offset: 0x006525F2
		public string Text
		{
			[CompilerGenerated]
			get
			{
				return this.<Text>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Text>k__BackingField = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x006543FB File Offset: 0x006525FB
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x00654403 File Offset: 0x00652603
		public bool IsConsumed
		{
			[CompilerGenerated]
			get
			{
				return this.<IsConsumed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsConsumed>k__BackingField = value;
			}
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x0065440C File Offset: 0x0065260C
		public ChatMessage(string message)
		{
			this.CommandId = ChatCommandId.FromType<SayChatCommand>();
			this.Text = message;
			this.IsConsumed = false;
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x0065442D File Offset: 0x0065262D
		private ChatMessage(string message, ChatCommandId commandId)
		{
			this.CommandId = commandId;
			this.Text = message;
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x00654444 File Offset: 0x00652644
		public void Serialize(BinaryWriter writer)
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			this.CommandId.Serialize(writer);
			writer.Write(this.Text);
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x00654480 File Offset: 0x00652680
		public int GetMaxSerializedSize()
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			return 0 + this.CommandId.GetMaxSerializedSize() + (4 + Encoding.UTF8.GetByteCount(this.Text));
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x006544C4 File Offset: 0x006526C4
		public static ChatMessage Deserialize(BinaryReader reader)
		{
			ChatCommandId chatCommandId = ChatCommandId.Deserialize(reader);
			return new ChatMessage(reader.ReadString(), chatCommandId);
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x006544E4 File Offset: 0x006526E4
		public void SetCommand(ChatCommandId commandId)
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			this.CommandId = commandId;
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x00654500 File Offset: 0x00652700
		public void SetCommand<T>() where T : IChatCommand
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			this.CommandId = ChatCommandId.FromType<T>();
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x00654520 File Offset: 0x00652720
		public void Consume()
		{
			this.IsConsumed = true;
		}

		// Token: 0x04005DBF RID: 23999
		[CompilerGenerated]
		private ChatCommandId <CommandId>k__BackingField;

		// Token: 0x04005DC0 RID: 24000
		[CompilerGenerated]
		private string <Text>k__BackingField;

		// Token: 0x04005DC1 RID: 24001
		[CompilerGenerated]
		private bool <IsConsumed>k__BackingField;
	}
}
