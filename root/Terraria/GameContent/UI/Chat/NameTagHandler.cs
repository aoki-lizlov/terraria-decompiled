using System;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000386 RID: 902
	public class NameTagHandler : ITagHandler
	{
		// Token: 0x060029C5 RID: 10693 RVA: 0x0057EC73 File Offset: 0x0057CE73
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			return new TextSnippet("<" + text.Replace("\\[", "[").Replace("\\]", "]") + ">", baseColor);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x0057ECA9 File Offset: 0x0057CEA9
		public static string GenerateTag(string name)
		{
			return "[n:" + name.Replace("[", "\\[").Replace("]", "\\]") + "]";
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x0000357B File Offset: 0x0000177B
		public NameTagHandler()
		{
		}
	}
}
