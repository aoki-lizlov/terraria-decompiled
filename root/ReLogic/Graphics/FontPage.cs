using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReLogic.Graphics
{
	// Token: 0x02000086 RID: 134
	internal sealed class FontPage
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000BC29 File Offset: 0x00009E29
		public FontPage(Texture2D texture, List<Rectangle> glyphs, List<Rectangle> padding, List<char> characters, List<Vector3> kerning)
		{
			this.Texture = texture;
			this.Glyphs = glyphs;
			this.Padding = padding;
			this.Characters = characters;
			this.Kerning = kerning;
		}

		// Token: 0x040004EF RID: 1263
		public readonly Texture2D Texture;

		// Token: 0x040004F0 RID: 1264
		public readonly List<Rectangle> Glyphs;

		// Token: 0x040004F1 RID: 1265
		public readonly List<Rectangle> Padding;

		// Token: 0x040004F2 RID: 1266
		public readonly List<char> Characters;

		// Token: 0x040004F3 RID: 1267
		public readonly List<Vector3> Kerning;
	}
}
