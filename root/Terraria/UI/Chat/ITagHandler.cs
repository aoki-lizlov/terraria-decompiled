using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
	// Token: 0x0200010D RID: 269
	public interface ITagHandler
	{
		// Token: 0x06001AAA RID: 6826
		TextSnippet Parse(string text, Color baseColor = default(Color), string options = null);
	}
}
