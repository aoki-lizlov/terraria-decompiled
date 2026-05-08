using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200012F RID: 303
	internal class SpriteFontReader : ContentTypeReader<SpriteFont>
	{
		// Token: 0x06001775 RID: 6005 RVA: 0x0003A2BB File Offset: 0x000384BB
		internal SpriteFontReader()
		{
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0003A2C4 File Offset: 0x000384C4
		protected internal override SpriteFont Read(ContentReader input, SpriteFont existingInstance)
		{
			if (existingInstance != null)
			{
				input.ReadObject<Texture2D>(existingInstance.textureValue);
				input.ReadObject<List<Rectangle>>();
				input.ReadObject<List<Rectangle>>();
				input.ReadObject<List<char>>();
				input.ReadInt32();
				input.ReadSingle();
				input.ReadObject<List<Vector3>>();
				if (input.ReadBoolean())
				{
					input.ReadChar();
				}
				return existingInstance;
			}
			Texture2D texture2D = input.ReadObject<Texture2D>();
			List<Rectangle> list = input.ReadObject<List<Rectangle>>();
			List<Rectangle> list2 = input.ReadObject<List<Rectangle>>();
			List<char> list3 = input.ReadObject<List<char>>();
			int num = input.ReadInt32();
			float num2 = input.ReadSingle();
			List<Vector3> list4 = input.ReadObject<List<Vector3>>();
			char? c = null;
			if (input.ReadBoolean())
			{
				c = new char?(input.ReadChar());
			}
			return new SpriteFont(texture2D, list, list2, list3, num, num2, list4, c);
		}
	}
}
