using System;
using System.IO;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200007F RID: 127
	internal static class DxtUtil
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x000245D8 File Offset: 0x000227D8
		internal static byte[] DecompressDxt1(byte[] imageData, int width, int height)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(imageData))
			{
				array = DxtUtil.DecompressDxt1(memoryStream, width, height);
			}
			return array;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00024614 File Offset: 0x00022814
		internal static byte[] DecompressDxt1(Stream imageStream, int width, int height)
		{
			byte[] array = new byte[width * height * 4];
			using (BinaryReader binaryReader = new BinaryReader(imageStream))
			{
				int num = (width + 3) / 4;
				int num2 = (height + 3) / 4;
				for (int i = 0; i < num2; i++)
				{
					for (int j = 0; j < num; j++)
					{
						DxtUtil.DecompressDxt1Block(binaryReader, j, i, num, width, height, array);
					}
				}
			}
			return array;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0002468C File Offset: 0x0002288C
		internal static byte[] DecompressDxt3(byte[] imageData, int width, int height)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(imageData))
			{
				array = DxtUtil.DecompressDxt3(memoryStream, width, height);
			}
			return array;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000246C8 File Offset: 0x000228C8
		internal static byte[] DecompressDxt3(Stream imageStream, int width, int height)
		{
			byte[] array = new byte[width * height * 4];
			using (BinaryReader binaryReader = new BinaryReader(imageStream))
			{
				int num = (width + 3) / 4;
				int num2 = (height + 3) / 4;
				for (int i = 0; i < num2; i++)
				{
					for (int j = 0; j < num; j++)
					{
						DxtUtil.DecompressDxt3Block(binaryReader, j, i, num, width, height, array);
					}
				}
			}
			return array;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00024740 File Offset: 0x00022940
		internal static byte[] DecompressDxt5(byte[] imageData, int width, int height)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(imageData))
			{
				array = DxtUtil.DecompressDxt5(memoryStream, width, height);
			}
			return array;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0002477C File Offset: 0x0002297C
		internal static byte[] DecompressDxt5(Stream imageStream, int width, int height)
		{
			byte[] array = new byte[width * height * 4];
			using (BinaryReader binaryReader = new BinaryReader(imageStream))
			{
				int num = (width + 3) / 4;
				int num2 = (height + 3) / 4;
				for (int i = 0; i < num2; i++)
				{
					for (int j = 0; j < num; j++)
					{
						DxtUtil.DecompressDxt5Block(binaryReader, j, i, num, width, height, array);
					}
				}
			}
			return array;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000247F4 File Offset: 0x000229F4
		private static void DecompressDxt1Block(BinaryReader imageReader, int x, int y, int blockCountX, int width, int height, byte[] imageData)
		{
			ushort num = imageReader.ReadUInt16();
			ushort num2 = imageReader.ReadUInt16();
			byte b;
			byte b2;
			byte b3;
			DxtUtil.ConvertRgb565ToRgb888(num, out b, out b2, out b3);
			byte b4;
			byte b5;
			byte b6;
			DxtUtil.ConvertRgb565ToRgb888(num2, out b4, out b5, out b6);
			uint num3 = imageReader.ReadUInt32();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					byte b7 = 0;
					byte b8 = 0;
					byte b9 = 0;
					byte b10 = byte.MaxValue;
					uint num4 = (num3 >> 2 * (4 * i + j)) & 3U;
					if (num > num2)
					{
						switch (num4)
						{
						case 0U:
							b7 = b;
							b8 = b2;
							b9 = b3;
							break;
						case 1U:
							b7 = b4;
							b8 = b5;
							b9 = b6;
							break;
						case 2U:
							b7 = (2 * b + b4) / 3;
							b8 = (2 * b2 + b5) / 3;
							b9 = (2 * b3 + b6) / 3;
							break;
						case 3U:
							b7 = (b + 2 * b4) / 3;
							b8 = (b2 + 2 * b5) / 3;
							b9 = (b3 + 2 * b6) / 3;
							break;
						}
					}
					else
					{
						switch (num4)
						{
						case 0U:
							b7 = b;
							b8 = b2;
							b9 = b3;
							break;
						case 1U:
							b7 = b4;
							b8 = b5;
							b9 = b6;
							break;
						case 2U:
							b7 = (b + b4) / 2;
							b8 = (b2 + b5) / 2;
							b9 = (b3 + b6) / 2;
							break;
						case 3U:
							b7 = 0;
							b8 = 0;
							b9 = 0;
							b10 = 0;
							break;
						}
					}
					int num5 = (x << 2) + j;
					int num6 = (y << 2) + i;
					if (num5 < width && num6 < height)
					{
						int num7 = num6 * width + num5 << 2;
						imageData[num7] = b7;
						imageData[num7 + 1] = b8;
						imageData[num7 + 2] = b9;
						imageData[num7 + 3] = b10;
					}
				}
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000249B4 File Offset: 0x00022BB4
		private static void DecompressDxt3Block(BinaryReader imageReader, int x, int y, int blockCountX, int width, int height, byte[] imageData)
		{
			byte b = imageReader.ReadByte();
			byte b2 = imageReader.ReadByte();
			byte b3 = imageReader.ReadByte();
			byte b4 = imageReader.ReadByte();
			byte b5 = imageReader.ReadByte();
			byte b6 = imageReader.ReadByte();
			byte b7 = imageReader.ReadByte();
			byte b8 = imageReader.ReadByte();
			ushort num = imageReader.ReadUInt16();
			ushort num2 = imageReader.ReadUInt16();
			byte b9;
			byte b10;
			byte b11;
			DxtUtil.ConvertRgb565ToRgb888(num, out b9, out b10, out b11);
			byte b12;
			byte b13;
			byte b14;
			DxtUtil.ConvertRgb565ToRgb888(num2, out b12, out b13, out b14);
			uint num3 = imageReader.ReadUInt32();
			int num4 = 0;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					byte b15 = 0;
					byte b16 = 0;
					byte b17 = 0;
					byte b18 = 0;
					uint num5 = (num3 >> 2 * (4 * i + j)) & 3U;
					switch (num4)
					{
					case 0:
						b18 = (byte)((int)(b & 15) | ((int)(b & 15) << 4));
						break;
					case 1:
						b18 = (byte)((int)(b & 240) | ((b & 240) >> 4));
						break;
					case 2:
						b18 = (byte)((int)(b2 & 15) | ((int)(b2 & 15) << 4));
						break;
					case 3:
						b18 = (byte)((int)(b2 & 240) | ((b2 & 240) >> 4));
						break;
					case 4:
						b18 = (byte)((int)(b3 & 15) | ((int)(b3 & 15) << 4));
						break;
					case 5:
						b18 = (byte)((int)(b3 & 240) | ((b3 & 240) >> 4));
						break;
					case 6:
						b18 = (byte)((int)(b4 & 15) | ((int)(b4 & 15) << 4));
						break;
					case 7:
						b18 = (byte)((int)(b4 & 240) | ((b4 & 240) >> 4));
						break;
					case 8:
						b18 = (byte)((int)(b5 & 15) | ((int)(b5 & 15) << 4));
						break;
					case 9:
						b18 = (byte)((int)(b5 & 240) | ((b5 & 240) >> 4));
						break;
					case 10:
						b18 = (byte)((int)(b6 & 15) | ((int)(b6 & 15) << 4));
						break;
					case 11:
						b18 = (byte)((int)(b6 & 240) | ((b6 & 240) >> 4));
						break;
					case 12:
						b18 = (byte)((int)(b7 & 15) | ((int)(b7 & 15) << 4));
						break;
					case 13:
						b18 = (byte)((int)(b7 & 240) | ((b7 & 240) >> 4));
						break;
					case 14:
						b18 = (byte)((int)(b8 & 15) | ((int)(b8 & 15) << 4));
						break;
					case 15:
						b18 = (byte)((int)(b8 & 240) | ((b8 & 240) >> 4));
						break;
					}
					num4++;
					switch (num5)
					{
					case 0U:
						b15 = b9;
						b16 = b10;
						b17 = b11;
						break;
					case 1U:
						b15 = b12;
						b16 = b13;
						b17 = b14;
						break;
					case 2U:
						b15 = (2 * b9 + b12) / 3;
						b16 = (2 * b10 + b13) / 3;
						b17 = (2 * b11 + b14) / 3;
						break;
					case 3U:
						b15 = (b9 + 2 * b12) / 3;
						b16 = (b10 + 2 * b13) / 3;
						b17 = (b11 + 2 * b14) / 3;
						break;
					}
					int num6 = (x << 2) + j;
					int num7 = (y << 2) + i;
					if (num6 < width && num7 < height)
					{
						int num8 = num7 * width + num6 << 2;
						imageData[num8] = b15;
						imageData[num8 + 1] = b16;
						imageData[num8 + 2] = b17;
						imageData[num8 + 3] = b18;
					}
				}
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00024CF0 File Offset: 0x00022EF0
		private static void DecompressDxt5Block(BinaryReader imageReader, int x, int y, int blockCountX, int width, int height, byte[] imageData)
		{
			byte b = imageReader.ReadByte();
			byte b2 = imageReader.ReadByte();
			ulong num = (ulong)imageReader.ReadByte();
			num += (ulong)imageReader.ReadByte() << 8;
			num += (ulong)imageReader.ReadByte() << 16;
			num += (ulong)imageReader.ReadByte() << 24;
			num += (ulong)imageReader.ReadByte() << 32;
			num += (ulong)imageReader.ReadByte() << 40;
			ushort num2 = imageReader.ReadUInt16();
			ushort num3 = imageReader.ReadUInt16();
			byte b3;
			byte b4;
			byte b5;
			DxtUtil.ConvertRgb565ToRgb888(num2, out b3, out b4, out b5);
			byte b6;
			byte b7;
			byte b8;
			DxtUtil.ConvertRgb565ToRgb888(num3, out b6, out b7, out b8);
			uint num4 = imageReader.ReadUInt32();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					byte b9 = 0;
					byte b10 = 0;
					byte b11 = 0;
					uint num5 = (num4 >> 2 * (4 * i + j)) & 3U;
					uint num6 = (uint)((num >> 3 * (4 * i + j)) & 7UL);
					byte b12;
					if (num6 == 0U)
					{
						b12 = b;
					}
					else if (num6 == 1U)
					{
						b12 = b2;
					}
					else if (b > b2)
					{
						b12 = (byte)(((8U - num6) * (uint)b + (num6 - 1U) * (uint)b2) / 7U);
					}
					else if (num6 == 6U)
					{
						b12 = 0;
					}
					else if (num6 == 7U)
					{
						b12 = byte.MaxValue;
					}
					else
					{
						b12 = (byte)(((6U - num6) * (uint)b + (num6 - 1U) * (uint)b2) / 5U);
					}
					switch (num5)
					{
					case 0U:
						b9 = b3;
						b10 = b4;
						b11 = b5;
						break;
					case 1U:
						b9 = b6;
						b10 = b7;
						b11 = b8;
						break;
					case 2U:
						b9 = (2 * b3 + b6) / 3;
						b10 = (2 * b4 + b7) / 3;
						b11 = (2 * b5 + b8) / 3;
						break;
					case 3U:
						b9 = (b3 + 2 * b6) / 3;
						b10 = (b4 + 2 * b7) / 3;
						b11 = (b5 + 2 * b8) / 3;
						break;
					}
					int num7 = (x << 2) + j;
					int num8 = (y << 2) + i;
					if (num7 < width && num8 < height)
					{
						int num9 = num8 * width + num7 << 2;
						imageData[num9] = b9;
						imageData[num9 + 1] = b10;
						imageData[num9 + 2] = b11;
						imageData[num9 + 3] = b12;
					}
				}
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00024F00 File Offset: 0x00023100
		private static void ConvertRgb565ToRgb888(ushort color, out byte r, out byte g, out byte b)
		{
			int num = (color >> 11) * 255 + 16;
			r = (byte)((num / 32 + num) / 32);
			num = ((color & 2016) >> 5) * 255 + 32;
			g = (byte)((num / 64 + num) / 64);
			num = (int)((color & 31) * 255 + 16);
			b = (byte)((num / 32 + num) / 32);
		}
	}
}
