using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000384 RID: 900
	public class ColorTagHandler : ITagHandler
	{
		// Token: 0x060029C0 RID: 10688 RVA: 0x0057EAA8 File Offset: 0x0057CCA8
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			TextSnippet textSnippet = new TextSnippet(text);
			int num;
			if (!int.TryParse(options, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out num))
			{
				return textSnippet;
			}
			textSnippet.Color = new Color((num >> 16) & 255, (num >> 8) & 255, num & 255);
			return textSnippet;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x0000357B File Offset: 0x0000177B
		public ColorTagHandler()
		{
		}
	}
}
