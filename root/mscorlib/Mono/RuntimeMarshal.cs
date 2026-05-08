using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono
{
	// Token: 0x0200002F RID: 47
	internal static class RuntimeMarshal
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00004428 File Offset: 0x00002628
		internal unsafe static string PtrToUtf8String(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return string.Empty;
			}
			byte* ptr2 = (byte*)(void*)ptr;
			int num = 0;
			try
			{
				while (*(ptr2++) != 0)
				{
					num++;
				}
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", "Value does not refer to a valid string.");
			}
			return new string((sbyte*)(void*)ptr, 0, num, Encoding.UTF8);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004494 File Offset: 0x00002694
		internal static SafeStringMarshal MarshalString(string str)
		{
			return new SafeStringMarshal(str);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000449C File Offset: 0x0000269C
		private unsafe static int DecodeBlobSize(IntPtr in_ptr, out IntPtr out_ptr)
		{
			byte* ptr = (byte*)(void*)in_ptr;
			uint num;
			if ((*ptr & 128) == 0)
			{
				num = (uint)(*ptr & 127);
				ptr++;
			}
			else if ((*ptr & 64) == 0)
			{
				num = (uint)(((int)(*ptr & 63) << 8) + (int)ptr[1]);
				ptr += 2;
			}
			else
			{
				num = (uint)(((int)(*ptr & 31) << 24) + ((int)ptr[1] << 16) + ((int)ptr[2] << 8) + (int)ptr[3]);
				ptr += 4;
			}
			out_ptr = (IntPtr)((void*)ptr);
			return (int)num;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000450C File Offset: 0x0000270C
		internal static byte[] DecodeBlobArray(IntPtr ptr)
		{
			IntPtr intPtr;
			int num = RuntimeMarshal.DecodeBlobSize(ptr, out intPtr);
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			return array;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004533 File Offset: 0x00002733
		internal static int AsciHexDigitValue(int c)
		{
			if (c >= 48 && c <= 57)
			{
				return c - 48;
			}
			if (c >= 97 && c <= 102)
			{
				return c - 97 + 10;
			}
			return c - 65 + 10;
		}

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FreeAssemblyName(ref MonoAssemblyName name, bool freeStruct);
	}
}
