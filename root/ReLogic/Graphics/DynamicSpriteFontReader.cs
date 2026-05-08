using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ReLogic.Graphics
{
	// Token: 0x02000085 RID: 133
	public class DynamicSpriteFontReader : ContentTypeReader<DynamicSpriteFont>
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000BB94 File Offset: 0x00009D94
		protected override DynamicSpriteFont Read(ContentReader input, DynamicSpriteFont existingInstance)
		{
			float num = input.ReadSingle();
			int num2 = input.ReadInt32();
			char c = input.ReadChar();
			DynamicSpriteFont dynamicSpriteFont = new DynamicSpriteFont(num, num2, c);
			int num3 = input.ReadInt32();
			FontPage[] array = new FontPage[num3];
			for (int i = 0; i < num3; i++)
			{
				Texture2D texture2D = input.ReadObject<Texture2D>();
				List<Rectangle> list = input.ReadObject<List<Rectangle>>();
				List<Rectangle> list2 = input.ReadObject<List<Rectangle>>();
				List<char> list3 = input.ReadObject<List<char>>();
				List<Vector3> list4 = input.ReadObject<List<Vector3>>();
				array[i] = new FontPage(texture2D, list, list2, list3, list4);
			}
			dynamicSpriteFont.SetPages(array);
			return dynamicSpriteFont;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000BC21 File Offset: 0x00009E21
		public DynamicSpriteFontReader()
		{
		}
	}
}
