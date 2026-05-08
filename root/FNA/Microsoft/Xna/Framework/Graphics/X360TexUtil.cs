using System;
using System.IO;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000E1 RID: 225
	internal static class X360TexUtil
	{
		// Token: 0x060015B9 RID: 5561 RVA: 0x0003523C File Offset: 0x0003343C
		internal static byte[] SwapColor(byte[] imageData)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(imageData))
			{
				array = X360TexUtil.SwapColor(memoryStream, imageData.Length);
			}
			return array;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00035278 File Offset: 0x00033478
		internal static byte[] SwapColor(Stream imageStream, int imageLength)
		{
			byte[] array = new byte[imageLength];
			using (BinaryReader binaryReader = new BinaryReader(imageStream))
			{
				for (int i = 0; i < imageLength; i += 4)
				{
					uint num = binaryReader.ReadUInt32();
					array[i] = (byte)((num >> 24) & 255U);
					array[i + 1] = (byte)((num >> 16) & 255U);
					array[i + 2] = (byte)((num >> 8) & 255U);
					array[i + 3] = (byte)(num & 255U);
				}
			}
			return array;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00035300 File Offset: 0x00033500
		internal static byte[] SwapDxt1(byte[] imageData, int width, int height)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(imageData))
			{
				array = X360TexUtil.SwapDxt1(memoryStream, imageData.Length, width, height);
			}
			return array;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00035340 File Offset: 0x00033540
		internal static byte[] SwapDxt1(Stream imageStream, int imageLength, int width, int height)
		{
			byte[] array = new byte[imageLength];
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					using (BinaryReader binaryReader = new BinaryReader(imageStream))
					{
						int num = (width + 3) / 4;
						int num2 = (height + 3) / 4;
						for (int i = 0; i < num2; i++)
						{
							for (int j = 0; j < num; j++)
							{
								X360TexUtil.SwapDxt1Block(binaryReader, binaryWriter);
							}
						}
					}
				}
			}
			return array;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000353EC File Offset: 0x000335EC
		internal static byte[] SwapDxt3(byte[] imageData, int width, int height)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(imageData))
			{
				array = X360TexUtil.SwapDxt3(memoryStream, imageData.Length, width, height);
			}
			return array;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0003542C File Offset: 0x0003362C
		internal static byte[] SwapDxt3(Stream imageStream, int imageLength, int width, int height)
		{
			byte[] array = new byte[imageLength];
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					using (BinaryReader binaryReader = new BinaryReader(imageStream))
					{
						int num = (width + 3) / 4;
						int num2 = (height + 3) / 4;
						for (int i = 0; i < num2; i++)
						{
							for (int j = 0; j < num; j++)
							{
								X360TexUtil.SwapDxt3Block(binaryReader, binaryWriter);
							}
						}
					}
				}
			}
			return array;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000354D8 File Offset: 0x000336D8
		internal static byte[] SwapDxt5(byte[] imageData, int width, int height)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(imageData))
			{
				array = X360TexUtil.SwapDxt5(memoryStream, imageData.Length, width, height);
			}
			return array;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00035518 File Offset: 0x00033718
		internal static byte[] SwapDxt5(Stream imageStream, int imageLength, int width, int height)
		{
			byte[] array = new byte[imageLength];
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					using (BinaryReader binaryReader = new BinaryReader(imageStream))
					{
						int num = (width + 3) / 4;
						int num2 = (height + 3) / 4;
						for (int i = 0; i < num2; i++)
						{
							for (int j = 0; j < num; j++)
							{
								X360TexUtil.SwapDxt5Block(binaryReader, binaryWriter);
							}
						}
					}
				}
			}
			return array;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000355C4 File Offset: 0x000337C4
		public static ushort SwapEndian(ushort data)
		{
			return (ushort)((data & 255) << 8) | (ushort)((data >> 8) & 255);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000355DC File Offset: 0x000337DC
		private static void SwapDxt1Block(BinaryReader imageReader, BinaryWriter imageWriter)
		{
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00035630 File Offset: 0x00033830
		private static void SwapDxt3Block(BinaryReader imageReader, BinaryWriter imageWriter)
		{
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			X360TexUtil.SwapDxt1Block(imageReader, imageWriter);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00035688 File Offset: 0x00033888
		private static void SwapDxt5Block(BinaryReader imageReader, BinaryWriter imageWriter)
		{
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			imageWriter.Write(X360TexUtil.SwapEndian(imageReader.ReadUInt16()));
			X360TexUtil.SwapDxt1Block(imageReader, imageWriter);
		}
	}
}
