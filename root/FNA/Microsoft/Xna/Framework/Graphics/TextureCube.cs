using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000CE RID: 206
	public class TextureCube : Texture
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00033603 File Offset: 0x00031803
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x0003360B File Offset: 0x0003180B
		public int Size
		{
			[CompilerGenerated]
			get
			{
				return this.<Size>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Size>k__BackingField = value;
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00033614 File Offset: 0x00031814
		public TextureCube(GraphicsDevice graphicsDevice, int size, bool mipMap, SurfaceFormat format)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			base.GraphicsDevice = graphicsDevice;
			this.Size = size;
			base.LevelCount = (mipMap ? Texture.CalculateMipLevels(size, 0, 0) : 1);
			if (this is IRenderTarget)
			{
				if (format == SurfaceFormat.ColorSrgbEXT)
				{
					if (FNA3D.FNA3D_SupportsSRGBRenderTargets(base.GraphicsDevice.GLDevice) == 0)
					{
						base.Format = SurfaceFormat.Color;
					}
					else
					{
						base.Format = format;
					}
				}
				else if (format != SurfaceFormat.Color && format != SurfaceFormat.Rgba1010102 && format != SurfaceFormat.Rg32 && format != SurfaceFormat.Rgba64 && format != SurfaceFormat.Single && format != SurfaceFormat.Vector2 && format != SurfaceFormat.Vector4 && format != SurfaceFormat.HalfSingle && format != SurfaceFormat.HalfVector2 && format != SurfaceFormat.HalfVector4 && format != SurfaceFormat.HdrBlendable)
				{
					base.Format = SurfaceFormat.Color;
				}
				else
				{
					base.Format = format;
				}
			}
			else
			{
				base.Format = format;
			}
			this.texture = FNA3D.FNA3D_CreateTextureCube(base.GraphicsDevice.GLDevice, base.Format, this.Size, base.LevelCount, (this is IRenderTarget) ? 1 : 0);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00033718 File Offset: 0x00031918
		public void SetData<T>(CubeMapFace cubeMapFace, T[] data) where T : struct
		{
			this.SetData<T>(cubeMapFace, 0, null, data, 0, data.Length);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0003373C File Offset: 0x0003193C
		public void SetData<T>(CubeMapFace cubeMapFace, T[] data, int startIndex, int elementCount) where T : struct
		{
			this.SetData<T>(cubeMapFace, 0, null, data, startIndex, elementCount);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00033760 File Offset: 0x00031960
		public void SetData<T>(CubeMapFace cubeMapFace, int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			int num;
			int num2;
			int num3;
			int num4;
			if (rect != null)
			{
				num = rect.Value.X;
				num2 = rect.Value.Y;
				num3 = rect.Value.Width;
				num4 = rect.Value.Height;
			}
			else
			{
				num = 0;
				num2 = 0;
				num3 = Math.Max(1, this.Size >> level);
				num4 = Math.Max(1, this.Size >> level);
			}
			int num5 = MarshalHelper.SizeOf<T>();
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetTextureDataCube(base.GraphicsDevice.GLDevice, this.texture, num, num2, num3, num4, cubeMapFace, level, gchandle.AddrOfPinnedObject() + startIndex * num5, elementCount * num5);
			gchandle.Free();
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00033830 File Offset: 0x00031A30
		public void SetDataPointerEXT(CubeMapFace cubeMapFace, int level, Rectangle? rect, IntPtr data, int dataLength)
		{
			if (data == IntPtr.Zero)
			{
				throw new ArgumentNullException("data");
			}
			int num;
			int num2;
			int num3;
			int num4;
			if (rect != null)
			{
				num = rect.Value.X;
				num2 = rect.Value.Y;
				num3 = rect.Value.Width;
				num4 = rect.Value.Height;
			}
			else
			{
				num = 0;
				num2 = 0;
				num3 = Math.Max(1, this.Size >> level);
				num4 = Math.Max(1, this.Size >> level);
			}
			FNA3D.FNA3D_SetTextureDataCube(base.GraphicsDevice.GLDevice, this.texture, num, num2, num3, num4, cubeMapFace, level, data, dataLength);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000338E0 File Offset: 0x00031AE0
		public void GetData<T>(CubeMapFace cubeMapFace, T[] data) where T : struct
		{
			this.GetData<T>(cubeMapFace, 0, null, data, 0, data.Length);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00033904 File Offset: 0x00031B04
		public void GetData<T>(CubeMapFace cubeMapFace, T[] data, int startIndex, int elementCount) where T : struct
		{
			this.GetData<T>(cubeMapFace, 0, null, data, startIndex, elementCount);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00033928 File Offset: 0x00031B28
		public void GetData<T>(CubeMapFace cubeMapFace, int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			if (data == null || data.Length == 0)
			{
				throw new ArgumentException("data cannot be null");
			}
			if (data.Length < startIndex + elementCount)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"The data passed has a length of ",
					data.Length.ToString(),
					" but ",
					elementCount.ToString(),
					" pixels have been requested."
				}));
			}
			int num;
			int num2;
			int num3;
			int num4;
			if (rect == null)
			{
				num = 0;
				num2 = 0;
				num3 = this.Size >> level;
				num4 = this.Size >> level;
			}
			else
			{
				num = rect.Value.X;
				num2 = rect.Value.Y;
				num3 = rect.Value.Width;
				num4 = rect.Value.Height;
			}
			int num5 = MarshalHelper.SizeOf<T>();
			Texture.ValidateGetDataFormat(base.Format, num5);
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_GetTextureDataCube(base.GraphicsDevice.GLDevice, this.texture, num, num2, num3, num4, cubeMapFace, level, gchandle.AddrOfPinnedObject() + startIndex * num5, elementCount * num5);
			gchandle.Free();
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00033A4C File Offset: 0x00031C4C
		public static TextureCube DDSFromStreamEXT(GraphicsDevice graphicsDevice, Stream stream)
		{
			TextureCube textureCube;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				SurfaceFormat surfaceFormat;
				int num;
				int num2;
				int num3;
				bool flag;
				Texture.ParseDDS(binaryReader, out surfaceFormat, out num, out num2, out num3, out flag);
				if (!flag)
				{
					throw new FormatException("This file does not contain cube data!");
				}
				textureCube = new TextureCube(graphicsDevice, num, num3 > 1, surfaceFormat);
				byte[] array = null;
				if (stream is MemoryStream && ((MemoryStream)stream).TryGetBuffer(out array))
				{
					for (int i = 0; i < 6; i++)
					{
						for (int j = 0; j < num3; j++)
						{
							int num4 = Texture.CalculateDDSLevelSize(num >> j, num >> j, surfaceFormat);
							textureCube.SetData<byte>((CubeMapFace)i, j, null, array, (int)stream.Seek(0L, SeekOrigin.Current), num4);
							stream.Seek((long)num4, SeekOrigin.Current);
						}
					}
				}
				else
				{
					for (int k = 0; k < 6; k++)
					{
						for (int l = 0; l < num3; l++)
						{
							array = binaryReader.ReadBytes(Texture.CalculateDDSLevelSize(num >> l, num >> l, surfaceFormat));
							textureCube.SetData<byte>((CubeMapFace)k, l, null, array, 0, array.Length);
						}
					}
				}
			}
			return textureCube;
		}

		// Token: 0x04000A40 RID: 2624
		[CompilerGenerated]
		private int <Size>k__BackingField;
	}
}
