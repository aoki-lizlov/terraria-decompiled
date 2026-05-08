using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	// Token: 0x020001C2 RID: 450
	[ComVisible(true)]
	public static class Buffer
	{
		// Token: 0x06001520 RID: 5408
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalBlockCopy(Array src, int srcOffsetBytes, Array dst, int dstOffsetBytes, int byteCount);

		// Token: 0x06001521 RID: 5409 RVA: 0x00053E28 File Offset: 0x00052028
		[SecurityCritical]
		internal unsafe static int IndexOfByte(byte* src, byte value, int index, int count)
		{
			byte* ptr = src + index;
			while ((ptr & 3) != 0)
			{
				if (count == 0)
				{
					return -1;
				}
				if (*ptr == value)
				{
					return (int)((long)(ptr - src));
				}
				count--;
				ptr++;
			}
			uint num = (uint)(((int)value << 8) + (int)value);
			num = (num << 16) + num;
			while (count > 3)
			{
				uint num2 = *(uint*)ptr;
				num2 ^= num;
				uint num3 = 2130640639U + num2;
				num2 ^= uint.MaxValue;
				num2 ^= num3;
				num2 &= 2164326656U;
				if (num2 != 0U)
				{
					int num4 = (int)((long)(ptr - src));
					if (*ptr == value)
					{
						return num4;
					}
					if (ptr[1] == value)
					{
						return num4 + 1;
					}
					if (ptr[2] == value)
					{
						return num4 + 2;
					}
					if (ptr[3] == value)
					{
						return num4 + 3;
					}
				}
				count -= 4;
				ptr += 4;
			}
			while (count > 0)
			{
				if (*ptr == value)
				{
					return (int)((long)(ptr - src));
				}
				count--;
				ptr++;
			}
			return -1;
		}

		// Token: 0x06001522 RID: 5410
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _ByteLength(Array array);

		// Token: 0x06001523 RID: 5411 RVA: 0x00053EEC File Offset: 0x000520EC
		[SecurityCritical]
		internal unsafe static void ZeroMemory(byte* src, long len)
		{
			for (;;)
			{
				long num = len;
				len = num - 1L;
				if (num <= 0L)
				{
					break;
				}
				src[len] = 0;
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00053F04 File Offset: 0x00052104
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void Memcpy(byte[] dest, int destIndex, byte* src, int srcIndex, int len)
		{
			if (len == 0)
			{
				return;
			}
			fixed (byte[] array = dest)
			{
				byte* ptr;
				if (dest == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Buffer.Memcpy(ptr + destIndex, src + srcIndex, len);
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00053F40 File Offset: 0x00052140
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void Memcpy(byte* pDest, int destIndex, byte[] src, int srcIndex, int len)
		{
			if (len == 0)
			{
				return;
			}
			fixed (byte[] array = src)
			{
				byte* ptr;
				if (src == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Buffer.Memcpy(pDest + destIndex, ptr + srcIndex, len);
			}
		}

		// Token: 0x06001526 RID: 5414
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void InternalMemcpy(byte* dest, byte* src, int count);

		// Token: 0x06001527 RID: 5415 RVA: 0x00053F79 File Offset: 0x00052179
		public static int ByteLength(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = Buffer._ByteLength(array);
			if (num < 0)
			{
				throw new ArgumentException("Object must be an array of primitives.");
			}
			return num;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00053F9E File Offset: 0x0005219E
		public unsafe static byte GetByte(Array array, int index)
		{
			if (index < 0 || index >= Buffer.ByteLength(array))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return *(byte*)Unsafe.AsPointer<byte>(Unsafe.Add<byte>(array.GetRawSzArrayData(), index));
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00053FCA File Offset: 0x000521CA
		public unsafe static void SetByte(Array array, int index, byte value)
		{
			if (index < 0 || index >= Buffer.ByteLength(array))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			*(byte*)Unsafe.AsPointer<byte>(Unsafe.Add<byte>(array.GetRawSzArrayData(), index)) = value;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00053FF8 File Offset: 0x000521F8
		public static void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			if (dst == null)
			{
				throw new ArgumentNullException("dst");
			}
			if (srcOffset < 0)
			{
				throw new ArgumentOutOfRangeException("srcOffset", "Non-negative number required.");
			}
			if (dstOffset < 0)
			{
				throw new ArgumentOutOfRangeException("dstOffset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (!Buffer.InternalBlockCopy(src, srcOffset, dst, dstOffset, count) && (srcOffset > Buffer.ByteLength(src) - count || dstOffset > Buffer.ByteLength(dst) - count))
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00054090 File Offset: 0x00052290
		[CLSCompliant(false)]
		public unsafe static void MemoryCopy(void* source, void* destination, long destinationSizeInBytes, long sourceBytesToCopy)
		{
			if (sourceBytesToCopy > destinationSizeInBytes)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
			}
			byte* ptr = (byte*)source;
			byte* ptr2 = (byte*)destination;
			while (sourceBytesToCopy > (long)((ulong)(-1)))
			{
				Buffer.Memmove(ptr2, ptr, uint.MaxValue);
				sourceBytesToCopy -= (long)((ulong)(-1));
				ptr += -1;
				ptr2 += -1;
			}
			Buffer.Memmove(ptr2, ptr, (uint)sourceBytesToCopy);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x000540D4 File Offset: 0x000522D4
		[CLSCompliant(false)]
		public unsafe static void MemoryCopy(void* source, void* destination, ulong destinationSizeInBytes, ulong sourceBytesToCopy)
		{
			if (sourceBytesToCopy > destinationSizeInBytes)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
			}
			byte* ptr = (byte*)source;
			byte* ptr2 = (byte*)destination;
			while (sourceBytesToCopy > (ulong)(-1))
			{
				Buffer.Memmove(ptr2, ptr, uint.MaxValue);
				sourceBytesToCopy -= (ulong)(-1);
				ptr += -1;
				ptr2 += -1;
			}
			Buffer.Memmove(ptr2, ptr, (uint)sourceBytesToCopy);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00054118 File Offset: 0x00052318
		internal unsafe static void memcpy4(byte* dest, byte* src, int size)
		{
			while (size >= 16)
			{
				*(int*)dest = *(int*)src;
				*(int*)(dest + 4) = *(int*)(src + 4);
				*(int*)(dest + (IntPtr)2 * 4) = *(int*)(src + (IntPtr)2 * 4);
				*(int*)(dest + (IntPtr)3 * 4) = *(int*)(src + (IntPtr)3 * 4);
				dest += 16;
				src += 16;
				size -= 16;
			}
			while (size >= 4)
			{
				*(int*)dest = *(int*)src;
				dest += 4;
				src += 4;
				size -= 4;
			}
			while (size > 0)
			{
				*dest = *src;
				dest++;
				src++;
				size--;
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00054198 File Offset: 0x00052398
		internal unsafe static void memcpy2(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*(short*)dest = *(short*)src;
				*(short*)(dest + 2) = *(short*)(src + 2);
				*(short*)(dest + (IntPtr)2 * 2) = *(short*)(src + (IntPtr)2 * 2);
				*(short*)(dest + (IntPtr)3 * 2) = *(short*)(src + (IntPtr)3 * 2);
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*(short*)dest = *(short*)src;
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00054204 File Offset: 0x00052404
		private unsafe static void memcpy1(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*dest = *src;
				dest[1] = src[1];
				dest[2] = src[2];
				dest[3] = src[3];
				dest[4] = src[4];
				dest[5] = src[5];
				dest[6] = src[6];
				dest[7] = src[7];
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*dest = *src;
				dest[1] = src[1];
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0005428C File Offset: 0x0005248C
		internal unsafe static void Memcpy(byte* dest, byte* src, int len)
		{
			if (len > 32)
			{
				Buffer.InternalMemcpy(dest, src, len);
				return;
			}
			if (((dest | src) & 3) != 0)
			{
				if ((dest & 1) != 0 && (src & 1) != 0 && len >= 1)
				{
					*dest = *src;
					dest++;
					src++;
					len--;
				}
				if ((dest & 2) != 0 && (src & 2) != 0 && len >= 2)
				{
					*(short*)dest = *(short*)src;
					dest += 2;
					src += 2;
					len -= 2;
				}
				if (((dest | src) & 1) != 0)
				{
					Buffer.memcpy1(dest, src, len);
					return;
				}
				if (((dest | src) & 2) != 0)
				{
					Buffer.memcpy2(dest, src, len);
					return;
				}
			}
			Buffer.memcpy4(dest, src, len);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00054322 File Offset: 0x00052522
		internal unsafe static void Memmove(byte* dest, byte* src, uint len)
		{
			if ((ulong)(dest - src) >= (ulong)len && (ulong)(src - dest) >= (ulong)len)
			{
				Buffer.Memcpy(dest, src, (int)len);
				return;
			}
			RuntimeImports.Memmove(dest, src, len);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00054348 File Offset: 0x00052548
		internal unsafe static void Memmove<T>(ref T destination, ref T source, ulong elementCount)
		{
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				fixed (byte* ptr = Unsafe.As<T, byte>(ref destination))
				{
					byte* ptr2 = ptr;
					fixed (byte* ptr3 = Unsafe.As<T, byte>(ref source))
					{
						byte* ptr4 = ptr3;
						Buffer.Memmove(ptr2, ptr4, (uint)elementCount * (uint)Unsafe.SizeOf<T>());
						ptr = null;
					}
					return;
				}
			}
			fixed (byte* ptr3 = Unsafe.As<T, byte>(ref destination))
			{
				byte* ptr5 = ptr3;
				fixed (byte* ptr = Unsafe.As<T, byte>(ref source))
				{
					byte* ptr6 = ptr;
					RuntimeImports.Memmove_wbarrier(ptr5, ptr6, (uint)elementCount, typeof(T).TypeHandle.Value);
					ptr3 = null;
				}
				return;
			}
		}
	}
}
