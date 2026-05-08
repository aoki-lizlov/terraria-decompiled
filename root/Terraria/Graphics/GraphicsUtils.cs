using System;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020001C9 RID: 457
	public static class GraphicsUtils
	{
		// Token: 0x06001F66 RID: 8038 RVA: 0x0051B688 File Offset: 0x00519888
		public static int PendingDrawCallCount(this SpriteBatch spriteBatch)
		{
			if (GraphicsUtils.SpriteBatch_spriteQueueCount == null)
			{
				bool flag = typeof(SpriteBatch).Assembly.GetName().Name == "Microsoft.Xna.Framework.Graphics";
				GraphicsUtils.SpriteBatch_spriteQueueCount = typeof(SpriteBatch).GetField(flag ? "spriteQueueCount" : "numSprites", BindingFlags.Instance | BindingFlags.NonPublic);
				GraphicsUtils.SpriteBatch_spriteTextures = typeof(SpriteBatch).GetField(flag ? "spriteTextures" : "textureInfo", BindingFlags.Instance | BindingFlags.NonPublic);
			}
			int num = (int)GraphicsUtils.SpriteBatch_spriteQueueCount.GetValue(spriteBatch);
			Texture[] array = (Texture2D[])GraphicsUtils.SpriteBatch_spriteTextures.GetValue(spriteBatch);
			return GraphicsUtils.DrawCallCount(array, num);
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0051B73C File Offset: 0x0051993C
		private static int DrawCallCount(Texture[] textures, int n)
		{
			int num = 0;
			if (Program.IsXna)
			{
				Texture texture = null;
				int num2 = 0;
				int num3 = 0;
				for (int i = 0; i < n; i++)
				{
					Texture texture2 = textures[i];
					if (texture2 != texture)
					{
						if (i > num2)
						{
							num += GraphicsUtils.DrawCallCountXNA(i - num2, ref num3);
						}
						num2 = i;
						texture = texture2;
					}
				}
				num += GraphicsUtils.DrawCallCountXNA(n - num2, ref num3);
			}
			else
			{
				int num4 = 0;
				for (;;)
				{
					int num5 = Math.Min(n, 2048);
					Texture texture3 = textures[num4];
					for (int j = 1; j < num5; j++)
					{
						Texture texture4 = textures[num4 + j];
						if (texture4 != texture3)
						{
							num++;
							texture3 = texture4;
						}
					}
					num++;
					if (n <= 2048)
					{
						break;
					}
					n -= 2048;
					num4 += 2048;
				}
			}
			return num;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0051B7FC File Offset: 0x005199FC
		private static int DrawCallCountXNA(int count, ref int vbPos)
		{
			int num = 0;
			while (count > 0)
			{
				int num2 = count;
				if (num2 > 2048 - vbPos)
				{
					num2 = 2048 - vbPos;
					if (num2 < 256)
					{
						vbPos = 0;
						num2 = count;
						if (num2 > 2048)
						{
							num2 = 2048;
						}
					}
				}
				vbPos += num2;
				count -= num2;
				num++;
			}
			return num;
		}

		// Token: 0x04004A0D RID: 18957
		private static FieldInfo SpriteBatch_spriteQueueCount;

		// Token: 0x04004A0E RID: 18958
		private static FieldInfo SpriteBatch_spriteTextures;
	}
}
