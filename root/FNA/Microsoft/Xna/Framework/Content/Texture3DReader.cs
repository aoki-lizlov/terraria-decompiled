using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000132 RID: 306
	internal class Texture3DReader : ContentTypeReader<Texture3D>
	{
		// Token: 0x0600177B RID: 6011 RVA: 0x0003A5E0 File Offset: 0x000387E0
		protected internal override Texture3D Read(ContentReader reader, Texture3D existingInstance)
		{
			SurfaceFormat surfaceFormat = (SurfaceFormat)reader.ReadInt32();
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			int num3 = reader.ReadInt32();
			int num4 = reader.ReadInt32();
			Texture3D texture3D;
			if (existingInstance == null)
			{
				texture3D = new Texture3D(reader.ContentManager.GetGraphicsDevice(), num, num2, num3, num4 > 1, surfaceFormat);
			}
			else
			{
				texture3D = existingInstance;
			}
			for (int i = 0; i < num4; i++)
			{
				int num5 = reader.ReadInt32();
				byte[] array = reader.ReadBytes(num5);
				texture3D.SetData<byte>(i, 0, 0, num, num2, 0, num3, array, 0, num5);
				num = Math.Max(num >> 1, 1);
				num2 = Math.Max(num2 >> 1, 1);
				num3 = Math.Max(num3 >> 1, 1);
			}
			return texture3D;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0003A68C File Offset: 0x0003888C
		public Texture3DReader()
		{
		}
	}
}
