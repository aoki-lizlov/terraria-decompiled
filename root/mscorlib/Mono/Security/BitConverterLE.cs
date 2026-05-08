using System;

namespace Mono.Security
{
	// Token: 0x0200005C RID: 92
	internal sealed class BitConverterLE
	{
		// Token: 0x060001DC RID: 476 RVA: 0x000025BE File Offset: 0x000007BE
		private BitConverterLE()
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A2FE File Offset: 0x000084FE
		private unsafe static byte[] GetUShortBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1]
				};
			}
			return new byte[]
			{
				bytes[1],
				*bytes
			};
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A32C File Offset: 0x0000852C
		private unsafe static byte[] GetUIntBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3]
				};
			}
			return new byte[]
			{
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A384 File Offset: 0x00008584
		private unsafe static byte[] GetULongBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3],
					bytes[4],
					bytes[5],
					bytes[6],
					bytes[7]
				};
			}
			return new byte[]
			{
				bytes[7],
				bytes[6],
				bytes[5],
				bytes[4],
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A411 File Offset: 0x00008611
		internal static byte[] GetBytes(bool value)
		{
			return new byte[] { value ? 1 : 0 };
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A423 File Offset: 0x00008623
		internal unsafe static byte[] GetBytes(char value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A423 File Offset: 0x00008623
		internal unsafe static byte[] GetBytes(short value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A42D File Offset: 0x0000862D
		internal unsafe static byte[] GetBytes(int value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A437 File Offset: 0x00008637
		internal unsafe static byte[] GetBytes(long value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A423 File Offset: 0x00008623
		internal unsafe static byte[] GetBytes(ushort value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A42D File Offset: 0x0000862D
		internal unsafe static byte[] GetBytes(uint value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A437 File Offset: 0x00008637
		internal unsafe static byte[] GetBytes(ulong value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A42D File Offset: 0x0000862D
		internal unsafe static byte[] GetBytes(float value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A437 File Offset: 0x00008637
		internal unsafe static byte[] GetBytes(double value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A441 File Offset: 0x00008641
		private unsafe static void UShortFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				return;
			}
			*dst = src[startIndex + 1];
			dst[1] = src[startIndex];
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A468 File Offset: 0x00008668
		private unsafe static void UIntFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				dst[2] = src[startIndex + 2];
				dst[3] = src[startIndex + 3];
				return;
			}
			*dst = src[startIndex + 3];
			dst[1] = src[startIndex + 2];
			dst[2] = src[startIndex + 1];
			dst[3] = src[startIndex];
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A4C0 File Offset: 0x000086C0
		private unsafe static void ULongFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 8; i++)
				{
					dst[i] = src[startIndex + i];
				}
				return;
			}
			for (int j = 0; j < 8; j++)
			{
				dst[j] = src[startIndex + (7 - j)];
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A501 File Offset: 0x00008701
		internal static bool ToBoolean(byte[] value, int startIndex)
		{
			return value[startIndex] > 0;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A50C File Offset: 0x0000870C
		internal unsafe static char ToChar(byte[] value, int startIndex)
		{
			char c;
			BitConverterLE.UShortFromBytes((byte*)(&c), value, startIndex);
			return c;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A524 File Offset: 0x00008724
		internal unsafe static short ToInt16(byte[] value, int startIndex)
		{
			short num;
			BitConverterLE.UShortFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A53C File Offset: 0x0000873C
		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A554 File Offset: 0x00008754
		internal unsafe static long ToInt64(byte[] value, int startIndex)
		{
			long num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000A56C File Offset: 0x0000876C
		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort num;
			BitConverterLE.UShortFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A584 File Offset: 0x00008784
		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A59C File Offset: 0x0000879C
		internal unsafe static ulong ToUInt64(byte[] value, int startIndex)
		{
			ulong num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000A5B4 File Offset: 0x000087B4
		internal unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A5CC File Offset: 0x000087CC
		internal unsafe static double ToDouble(byte[] value, int startIndex)
		{
			double num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}
	}
}
