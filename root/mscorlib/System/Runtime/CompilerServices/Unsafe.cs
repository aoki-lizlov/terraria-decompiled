using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000825 RID: 2085
	internal static class Unsafe
	{
		// Token: 0x060046A4 RID: 18084 RVA: 0x000E7926 File Offset: 0x000E5B26
		public static ref T Add<T>(ref T source, int elementOffset)
		{
			return (ref source) + (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x000E7933 File Offset: 0x000E5B33
		public static ref T Add<T>(ref T source, IntPtr elementOffset)
		{
			return (ref source) + elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x000E7926 File Offset: 0x000E5B26
		public unsafe static void* Add<T>(void* source, int elementOffset)
		{
			return (void*)((byte*)source + (IntPtr)elementOffset * (IntPtr)sizeof(T));
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x000E793F File Offset: 0x000E5B3F
		public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
		{
			return (ref source) + byteOffset;
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x0001FDBA File Offset: 0x0001DFBA
		public static bool AreSame<T>(ref T left, ref T right)
		{
			return (ref left) == (ref right);
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x000025CE File Offset: 0x000007CE
		public static T As<T>(object o) where T : class
		{
			return o;
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x000025CE File Offset: 0x000007CE
		public static ref TTo As<TFrom, TTo>(ref TFrom source)
		{
			return ref source;
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x000E7944 File Offset: 0x000E5B44
		public unsafe static void* AsPointer<T>(ref T value)
		{
			return (void*)(&value);
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000025CE File Offset: 0x000007CE
		public unsafe static ref T AsRef<T>(void* source)
		{
			return ref *(T*)source;
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x000025CE File Offset: 0x000007CE
		public static ref T AsRef<T>(in T source)
		{
			return ref source;
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000E7948 File Offset: 0x000E5B48
		public static IntPtr ByteOffset<T>(ref T origin, ref T target)
		{
			return (ref target) - (ref origin);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000E794D File Offset: 0x000E5B4D
		public static void CopyBlock(ref byte destination, ref byte source, uint byteCount)
		{
			cpblk(ref destination, ref source, byteCount);
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000E7954 File Offset: 0x000E5B54
		public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
		{
			initblk(ref startAddress, value, byteCount);
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x000E7954 File Offset: 0x000E5B54
		public unsafe static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
		{
			initblk(startAddress, value, byteCount);
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000E795E File Offset: 0x000E5B5E
		public unsafe static T Read<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x000E7966 File Offset: 0x000E5B66
		public unsafe static T ReadUnaligned<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x000E7966 File Offset: 0x000E5B66
		public static T ReadUnaligned<T>(ref byte source)
		{
			return source;
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x000E7971 File Offset: 0x000E5B71
		public static int SizeOf<T>()
		{
			return sizeof(T);
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x000E7979 File Offset: 0x000E5B79
		public static ref T Subtract<T>(ref T source, int elementOffset)
		{
			return (ref source) - (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x000E7986 File Offset: 0x000E5B86
		public static void WriteUnaligned<T>(ref byte destination, T value)
		{
			destination = value;
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x000E7986 File Offset: 0x000E5B86
		public unsafe static void WriteUnaligned<T>(void* destination, T value)
		{
			*(T*)destination = value;
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x000E7992 File Offset: 0x000E5B92
		public static bool IsAddressGreaterThan<T>(ref T left, ref T right)
		{
			return (ref left) != (ref right);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x000E7998 File Offset: 0x000E5B98
		public static bool IsAddressLessThan<T>(ref T left, ref T right)
		{
			return (ref left) < (ref right);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x000E799E File Offset: 0x000E5B9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T AddByteOffset<T>(ref T source, ulong byteOffset)
		{
			return Unsafe.AddByteOffset<T>(ref source, (IntPtr)byteOffset);
		}
	}
}
