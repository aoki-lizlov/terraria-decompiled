using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000CB RID: 203
	public class Texture2D : Texture
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00032A02 File Offset: 0x00030C02
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x00032A0A File Offset: 0x00030C0A
		public int Width
		{
			[CompilerGenerated]
			get
			{
				return this.<Width>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Width>k__BackingField = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00032A13 File Offset: 0x00030C13
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x00032A1B File Offset: 0x00030C1B
		public int Height
		{
			[CompilerGenerated]
			get
			{
				return this.<Height>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Height>k__BackingField = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00032A24 File Offset: 0x00030C24
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(0, 0, this.Width, this.Height);
			}
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00032A39 File Offset: 0x00030C39
		public Texture2D(GraphicsDevice graphicsDevice, int width, int height)
			: this(graphicsDevice, width, height, false, SurfaceFormat.Color)
		{
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00032A48 File Offset: 0x00030C48
		public Texture2D(GraphicsDevice graphicsDevice, int width, int height, bool mipMap, SurfaceFormat format)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			base.GraphicsDevice = graphicsDevice;
			this.Width = width;
			this.Height = height;
			base.LevelCount = (mipMap ? Texture.CalculateMipLevels(width, height, 0) : 1);
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
				else if (format != SurfaceFormat.Color && format != SurfaceFormat.Rgba1010102 && format != SurfaceFormat.Rg32 && format != SurfaceFormat.Rgba64 && format != SurfaceFormat.Single && format != SurfaceFormat.Vector2 && format != SurfaceFormat.Vector4 && format != SurfaceFormat.HalfSingle && format != SurfaceFormat.HalfVector2 && format != SurfaceFormat.HalfVector4 && format != SurfaceFormat.HdrBlendable && format != SurfaceFormat.ByteEXT && format != SurfaceFormat.UShortEXT)
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
			this.texture = FNA3D.FNA3D_CreateTexture2D(base.GraphicsDevice.GLDevice, base.Format, this.Width, this.Height, base.LevelCount, (this is IRenderTarget) ? 1 : 0);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00032B68 File Offset: 0x00030D68
		public void SetData<T>(T[] data) where T : struct
		{
			this.SetData<T>(0, null, data, 0, data.Length);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00032B8C File Offset: 0x00030D8C
		public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			this.SetData<T>(0, null, data, startIndex, elementCount);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00032BAC File Offset: 0x00030DAC
		public void SetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (data.Length < elementCount + startIndex)
			{
				throw new ArgumentOutOfRangeException("elementCount");
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
				num3 = Math.Max(this.Width >> level, 1);
				num4 = Math.Max(this.Height >> level, 1);
			}
			int num5 = MarshalHelper.SizeOf<T>();
			int num6 = num3 * num4 * Texture.GetFormatSizeEXT(base.Format) / Texture.GetBlockSizeSquaredEXT(base.Format);
			int num7 = elementCount * num5;
			if (num6 > num7)
			{
				throw new ArgumentOutOfRangeException("rect", "The region you are trying to upload is larger than the amount of data you provided.");
			}
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetTextureData2D(base.GraphicsDevice.GLDevice, this.texture, num, num2, num3, num4, level, gchandle.AddrOfPinnedObject() + startIndex * num5, elementCount * num5);
			gchandle.Free();
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00032CD4 File Offset: 0x00030ED4
		public void SetDataPointerEXT(int level, Rectangle? rect, IntPtr data, int dataLength)
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
				num3 = Math.Max(this.Width >> level, 1);
				num4 = Math.Max(this.Height >> level, 1);
			}
			FNA3D.FNA3D_SetTextureData2D(base.GraphicsDevice.GLDevice, this.texture, num, num2, num3, num4, level, data, dataLength);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00032D80 File Offset: 0x00030F80
		public void GetData<T>(T[] data) where T : struct
		{
			this.GetData<T>(0, null, data, 0, data.Length);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00032DA4 File Offset: 0x00030FA4
		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			this.GetData<T>(0, null, data, startIndex, elementCount);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00032DC4 File Offset: 0x00030FC4
		public void GetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
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
			int num = MarshalHelper.SizeOf<T>();
			Texture.ValidateGetDataFormat(base.Format, num);
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			this.GetDataPointerEXT(level, rect, gchandle.AddrOfPinnedObject() + startIndex * num, elementCount * num);
			gchandle.Free();
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00032E6C File Offset: 0x0003106C
		public void GetDataPointerEXT(int level, Rectangle? rect, IntPtr data, int dataLengthBytes)
		{
			int num;
			int num2;
			int num3;
			int num4;
			if (rect == null)
			{
				num = 0;
				num2 = 0;
				num3 = this.Width >> level;
				num4 = this.Height >> level;
			}
			else
			{
				num = rect.Value.X;
				num2 = rect.Value.Y;
				num3 = rect.Value.Width;
				num4 = rect.Value.Height;
			}
			FNA3D.FNA3D_GetTextureData2D(base.GraphicsDevice.GLDevice, this.texture, num, num2, num3, num4, level, data, dataLengthBytes);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00032EF4 File Offset: 0x000310F4
		public void SaveAsJpeg(Stream stream, int width, int height)
		{
			string environmentVariable = Environment.GetEnvironmentVariable("FNA_GRAPHICS_JPEG_SAVE_QUALITY");
			int num;
			if (string.IsNullOrEmpty(environmentVariable) || !int.TryParse(environmentVariable, out num))
			{
				num = 100;
			}
			int num2 = this.Width * this.Height * Texture.GetFormatSizeEXT(base.Format);
			IntPtr intPtr = FNAPlatform.Malloc(num2);
			FNA3D.FNA3D_GetTextureData2D(base.GraphicsDevice.GLDevice, this.texture, 0, 0, this.Width, this.Height, 0, intPtr, num2);
			FNA3D.WriteJPGStream(stream, this.Width, this.Height, width, height, intPtr, num);
			FNAPlatform.Free(intPtr);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00032F90 File Offset: 0x00031190
		public void SaveAsPng(Stream stream, int width, int height)
		{
			int num = this.Width * this.Height * Texture.GetFormatSizeEXT(base.Format);
			IntPtr intPtr = FNAPlatform.Malloc(num);
			FNA3D.FNA3D_GetTextureData2D(base.GraphicsDevice.GLDevice, this.texture, 0, 0, this.Width, this.Height, 0, intPtr, num);
			FNA3D.WritePNGStream(stream, this.Width, this.Height, width, height, intPtr);
			FNAPlatform.Free(intPtr);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0003300C File Offset: 0x0003120C
		public static Texture2D FromStream(GraphicsDevice graphicsDevice, Stream stream)
		{
			if (stream.CanSeek && stream.Position == stream.Length)
			{
				stream.Seek(0L, SeekOrigin.Begin);
			}
			int num;
			int num2;
			int num3;
			IntPtr intPtr = FNA3D.ReadImageStream(stream, out num, out num2, out num3, -1, -1, false);
			if (intPtr == IntPtr.Zero || num <= 0 || num2 <= 0)
			{
				throw new Exception("Decoding image failed!");
			}
			Texture2D texture2D = new Texture2D(graphicsDevice, num, num2);
			texture2D.SetDataPointerEXT(0, null, intPtr, num3);
			FNA3D.FNA3D_Image_Free(intPtr);
			return texture2D;
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0003308C File Offset: 0x0003128C
		public static Texture2D FromStream(GraphicsDevice graphicsDevice, Stream stream, int width, int height, bool zoom)
		{
			if (stream.CanSeek && stream.Position == stream.Length)
			{
				stream.Seek(0L, SeekOrigin.Begin);
			}
			int num;
			int num2;
			int num3;
			IntPtr intPtr = FNA3D.ReadImageStream(stream, out num, out num2, out num3, width, height, zoom);
			if (intPtr == IntPtr.Zero || num <= 0 || num2 <= 0)
			{
				throw new Exception("Decoding image failed!");
			}
			Texture2D texture2D = new Texture2D(graphicsDevice, num, num2);
			texture2D.SetDataPointerEXT(0, null, intPtr, num3);
			FNA3D.FNA3D_Image_Free(intPtr);
			return texture2D;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0003310C File Offset: 0x0003130C
		public static void TextureDataFromStreamEXT(Stream stream, out int width, out int height, out byte[] pixels, int requestedWidth = -1, int requestedHeight = -1, bool zoom = false)
		{
			if (stream.CanSeek && stream.Position == stream.Length)
			{
				stream.Seek(0L, SeekOrigin.Begin);
			}
			int num;
			IntPtr intPtr = FNA3D.ReadImageStream(stream, out width, out height, out num, requestedWidth, requestedHeight, zoom);
			pixels = new byte[num];
			Marshal.Copy(intPtr, pixels, 0, num);
			FNA3D.FNA3D_Image_Free(intPtr);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00033160 File Offset: 0x00031360
		public static Texture2D DDSFromStreamEXT(GraphicsDevice graphicsDevice, Stream stream)
		{
			Texture2D texture2D;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				SurfaceFormat surfaceFormat;
				int num;
				int num2;
				int num3;
				bool flag;
				Texture.ParseDDS(binaryReader, out surfaceFormat, out num, out num2, out num3, out flag);
				if (flag)
				{
					throw new FormatException("This file contains cube map data!");
				}
				texture2D = new Texture2D(graphicsDevice, num, num2, num3 > 1, surfaceFormat);
				byte[] array = null;
				if (stream is MemoryStream && ((MemoryStream)stream).TryGetBuffer(out array))
				{
					for (int i = 0; i < num3; i++)
					{
						int num4 = Texture.CalculateDDSLevelSize(num >> i, num2 >> i, surfaceFormat);
						texture2D.SetData<byte>(i, null, array, (int)stream.Seek(0L, SeekOrigin.Current), num4);
						stream.Seek((long)num4, SeekOrigin.Current);
					}
				}
				else
				{
					for (int j = 0; j < num3; j++)
					{
						array = binaryReader.ReadBytes(Texture.CalculateDDSLevelSize(num >> j, num2 >> j, surfaceFormat));
						texture2D.SetData<byte>(j, null, array, 0, array.Length);
					}
				}
			}
			return texture2D;
		}

		// Token: 0x04000A38 RID: 2616
		[CompilerGenerated]
		private int <Width>k__BackingField;

		// Token: 0x04000A39 RID: 2617
		[CompilerGenerated]
		private int <Height>k__BackingField;
	}
}
