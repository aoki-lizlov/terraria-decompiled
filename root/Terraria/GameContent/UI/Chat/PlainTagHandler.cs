using System;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000387 RID: 903
	public class PlainTagHandler : ITagHandler
	{
		// Token: 0x060029C8 RID: 10696 RVA: 0x0057ECD9 File Offset: 0x0057CED9
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			return new PlainTagHandler.PlainSnippet(text);
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x0000357B File Offset: 0x0000177B
		public PlainTagHandler()
		{
		}

		// Token: 0x020008DF RID: 2271
		public class PlainSnippet : TextSnippet
		{
			// Token: 0x06004698 RID: 18072 RVA: 0x006C8873 File Offset: 0x006C6A73
			public PlainSnippet(string text = "")
				: base(text)
			{
			}

			// Token: 0x06004699 RID: 18073 RVA: 0x006C887C File Offset: 0x006C6A7C
			public PlainSnippet(string text, Color color)
				: base(text, color)
			{
			}

			// Token: 0x0600469A RID: 18074 RVA: 0x006C8886 File Offset: 0x006C6A86
			public override Color GetVisibleColor()
			{
				return this.Color;
			}
		}
	}
}
