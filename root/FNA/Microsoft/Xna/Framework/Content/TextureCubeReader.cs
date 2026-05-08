using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000133 RID: 307
	internal class TextureCubeReader : ContentTypeReader<TextureCube>
	{
		// Token: 0x0600177D RID: 6013 RVA: 0x0003A694 File Offset: 0x00038894
		protected internal override TextureCube Read(ContentReader reader, TextureCube existingInstance)
		{
			SurfaceFormat surfaceFormat = (SurfaceFormat)reader.ReadInt32();
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			TextureCube textureCube;
			if (existingInstance == null)
			{
				textureCube = new TextureCube(reader.ContentManager.GetGraphicsDevice(), num, num2 > 1, surfaceFormat);
			}
			else
			{
				textureCube = existingInstance;
			}
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					int num3 = reader.ReadInt32();
					byte[] array = reader.ReadBytes(num3);
					textureCube.SetData<byte>((CubeMapFace)i, j, null, array, 0, num3);
				}
			}
			return textureCube;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0003A720 File Offset: 0x00038920
		public TextureCubeReader()
		{
		}
	}
}
