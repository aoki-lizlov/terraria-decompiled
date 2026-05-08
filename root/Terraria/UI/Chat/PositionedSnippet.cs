using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
	// Token: 0x02000109 RID: 265
	public struct PositionedSnippet
	{
		// Token: 0x06001A85 RID: 6789 RVA: 0x004F6F27 File Offset: 0x004F5127
		public PositionedSnippet(TextSnippet snippet, int origIndex, int line, Vector2 position, Vector2 size)
		{
			this.Snippet = snippet;
			this.OrigIndex = origIndex;
			this.Line = line;
			this.Position = position;
			this.Size = size;
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x004F6F4E File Offset: 0x004F514E
		public void Scale(float scale)
		{
			this.Position *= scale;
			this.Size *= scale;
		}

		// Token: 0x040014F6 RID: 5366
		public readonly TextSnippet Snippet;

		// Token: 0x040014F7 RID: 5367
		public readonly int OrigIndex;

		// Token: 0x040014F8 RID: 5368
		public readonly int Line;

		// Token: 0x040014F9 RID: 5369
		public Vector2 Position;

		// Token: 0x040014FA RID: 5370
		public Vector2 Size;
	}
}
