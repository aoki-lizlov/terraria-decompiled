using System;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005B9 RID: 1465
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	public sealed class ChatCommandAttribute : Attribute
	{
		// Token: 0x060039EF RID: 14831 RVA: 0x00654529 File Offset: 0x00652729
		public ChatCommandAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x04005DC2 RID: 24002
		public readonly string Name;
	}
}
