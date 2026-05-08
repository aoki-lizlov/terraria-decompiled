using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000131 RID: 305
	internal class Texture2DReader : ContentTypeReader<Texture2D>
	{
		// Token: 0x06001779 RID: 6009 RVA: 0x0003A38A File Offset: 0x0003858A
		internal Texture2DReader()
		{
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0003A394 File Offset: 0x00038594
		protected internal override Texture2D Read(ContentReader reader, Texture2D existingInstance)
		{
			SurfaceFormat surfaceFormat;
			if (reader.version < 5)
			{
				int num = reader.ReadInt32();
				if (num == 1)
				{
					surfaceFormat = SurfaceFormat.ColorBgraEXT;
				}
				else if (num == 28)
				{
					surfaceFormat = SurfaceFormat.Dxt1;
				}
				else if (num == 30)
				{
					surfaceFormat = SurfaceFormat.Dxt3;
				}
				else
				{
					if (num != 32)
					{
						throw new NotSupportedException("Unsupported legacy surface format.");
					}
					surfaceFormat = SurfaceFormat.Dxt5;
				}
			}
			else
			{
				surfaceFormat = (SurfaceFormat)reader.ReadInt32();
			}
			int num2 = reader.ReadInt32();
			int num3 = reader.ReadInt32();
			int num4 = reader.ReadInt32();
			int num5 = num4;
			GraphicsDevice graphicsDevice = reader.ContentManager.GetGraphicsDevice();
			SurfaceFormat surfaceFormat2 = surfaceFormat;
			if (surfaceFormat == SurfaceFormat.Dxt1 && FNA3D.FNA3D_SupportsDXT1(graphicsDevice.GLDevice) == 0)
			{
				surfaceFormat2 = SurfaceFormat.Color;
			}
			else if ((surfaceFormat == SurfaceFormat.Dxt3 || surfaceFormat == SurfaceFormat.Dxt5) && FNA3D.FNA3D_SupportsS3TC(graphicsDevice.GLDevice) == 0)
			{
				surfaceFormat2 = SurfaceFormat.Color;
			}
			Texture2D texture2D;
			if (existingInstance == null)
			{
				texture2D = new Texture2D(graphicsDevice, num2, num3, num5 > 1, surfaceFormat2);
			}
			else
			{
				texture2D = existingInstance;
			}
			for (int i = 0; i < num4; i++)
			{
				int num6 = reader.ReadInt32();
				byte[] array = null;
				int num7 = num2 >> i;
				int num8 = num3 >> i;
				if (i < num5)
				{
					if (reader.platform == 'x')
					{
						if (surfaceFormat == SurfaceFormat.Color || surfaceFormat == SurfaceFormat.ColorBgraEXT)
						{
							array = X360TexUtil.SwapColor(reader.ReadBytes(num6));
							num6 = array.Length;
						}
						else if (surfaceFormat == SurfaceFormat.Dxt1)
						{
							array = X360TexUtil.SwapDxt1(reader.ReadBytes(num6), num7, num8);
							num6 = array.Length;
						}
						else if (surfaceFormat == SurfaceFormat.Dxt3)
						{
							array = X360TexUtil.SwapDxt3(reader.ReadBytes(num6), num7, num8);
							num6 = array.Length;
						}
						else if (surfaceFormat == SurfaceFormat.Dxt5)
						{
							array = X360TexUtil.SwapDxt5(reader.ReadBytes(num6), num7, num8);
							num6 = array.Length;
						}
					}
					if (surfaceFormat2 != surfaceFormat)
					{
						if (array == null)
						{
							array = reader.ReadBytes(num6);
						}
						if (surfaceFormat == SurfaceFormat.Dxt1)
						{
							array = DxtUtil.DecompressDxt1(array, num7, num8);
						}
						else if (surfaceFormat == SurfaceFormat.Dxt3)
						{
							array = DxtUtil.DecompressDxt3(array, num7, num8);
						}
						else if (surfaceFormat == SurfaceFormat.Dxt5)
						{
							array = DxtUtil.DecompressDxt5(array, num7, num8);
						}
						num6 = array.Length;
					}
					int num9 = 0;
					if (array == null)
					{
						if (reader.BaseStream is MemoryStream && ((MemoryStream)reader.BaseStream).TryGetBuffer(out array))
						{
							num9 = (int)reader.BaseStream.Seek(0L, SeekOrigin.Current);
							reader.BaseStream.Seek((long)num6, SeekOrigin.Current);
						}
						else
						{
							array = reader.ReadBytes(num6);
						}
					}
					texture2D.SetData<byte>(i, null, array, num9, num6);
				}
			}
			return texture2D;
		}
	}
}
