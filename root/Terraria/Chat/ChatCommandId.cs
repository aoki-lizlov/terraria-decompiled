using System;
using System.IO;
using System.Text;
using ReLogic.Utilities;
using Terraria.Chat.Commands;

namespace Terraria.Chat
{
	// Token: 0x020005B3 RID: 1459
	public struct ChatCommandId
	{
		// Token: 0x060039C4 RID: 14788 RVA: 0x00653DB1 File Offset: 0x00651FB1
		private ChatCommandId(string name)
		{
			this._name = name;
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x00653DBC File Offset: 0x00651FBC
		public static ChatCommandId FromType<T>() where T : IChatCommand
		{
			ChatCommandAttribute cacheableAttribute = AttributeUtilities.GetCacheableAttribute<T, ChatCommandAttribute>();
			if (cacheableAttribute != null)
			{
				return new ChatCommandId(cacheableAttribute.Name);
			}
			return new ChatCommandId(null);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x00653DE4 File Offset: 0x00651FE4
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this._name ?? "");
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x00653DFB File Offset: 0x00651FFB
		public static ChatCommandId Deserialize(BinaryReader reader)
		{
			return new ChatCommandId(reader.ReadString());
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x00653E08 File Offset: 0x00652008
		public int GetMaxSerializedSize()
		{
			return 4 + Encoding.UTF8.GetByteCount(this._name ?? "");
		}

		// Token: 0x04005DB4 RID: 23988
		private readonly string _name;
	}
}
