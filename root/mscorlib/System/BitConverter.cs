using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000C7 RID: 199
	public static class BitConverter
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x0000A411 File Offset: 0x00008611
		public static byte[] GetBytes(bool value)
		{
			return new byte[] { value ? 1 : 0 };
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00019362 File Offset: 0x00017562
		public static bool TryWriteBytes(Span<byte> destination, bool value)
		{
			if (destination.Length < 1)
			{
				return false;
			}
			Unsafe.WriteUnaligned<byte>(MemoryMarshal.GetReference<byte>(destination), value ? 1 : 0);
			return true;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00019383 File Offset: 0x00017583
		public unsafe static byte[] GetBytes(char value)
		{
			byte[] array = new byte[2];
			*Unsafe.As<byte, char>(ref array[0]) = value;
			return array;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00019399 File Offset: 0x00017599
		public static bool TryWriteBytes(Span<byte> destination, char value)
		{
			if (destination.Length < 2)
			{
				return false;
			}
			Unsafe.WriteUnaligned<char>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000193B4 File Offset: 0x000175B4
		public unsafe static byte[] GetBytes(short value)
		{
			byte[] array = new byte[2];
			*Unsafe.As<byte, short>(ref array[0]) = value;
			return array;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000193CA File Offset: 0x000175CA
		public static bool TryWriteBytes(Span<byte> destination, short value)
		{
			if (destination.Length < 2)
			{
				return false;
			}
			Unsafe.WriteUnaligned<short>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000193E5 File Offset: 0x000175E5
		public unsafe static byte[] GetBytes(int value)
		{
			byte[] array = new byte[4];
			*Unsafe.As<byte, int>(ref array[0]) = value;
			return array;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000193FB File Offset: 0x000175FB
		public static bool TryWriteBytes(Span<byte> destination, int value)
		{
			if (destination.Length < 4)
			{
				return false;
			}
			Unsafe.WriteUnaligned<int>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00019416 File Offset: 0x00017616
		public unsafe static byte[] GetBytes(long value)
		{
			byte[] array = new byte[8];
			*Unsafe.As<byte, long>(ref array[0]) = value;
			return array;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001942C File Offset: 0x0001762C
		public static bool TryWriteBytes(Span<byte> destination, long value)
		{
			if (destination.Length < 8)
			{
				return false;
			}
			Unsafe.WriteUnaligned<long>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00019447 File Offset: 0x00017647
		[CLSCompliant(false)]
		public unsafe static byte[] GetBytes(ushort value)
		{
			byte[] array = new byte[2];
			*Unsafe.As<byte, ushort>(ref array[0]) = value;
			return array;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001945D File Offset: 0x0001765D
		[CLSCompliant(false)]
		public static bool TryWriteBytes(Span<byte> destination, ushort value)
		{
			if (destination.Length < 2)
			{
				return false;
			}
			Unsafe.WriteUnaligned<ushort>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00019478 File Offset: 0x00017678
		[CLSCompliant(false)]
		public unsafe static byte[] GetBytes(uint value)
		{
			byte[] array = new byte[4];
			*Unsafe.As<byte, uint>(ref array[0]) = value;
			return array;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001948E File Offset: 0x0001768E
		[CLSCompliant(false)]
		public static bool TryWriteBytes(Span<byte> destination, uint value)
		{
			if (destination.Length < 4)
			{
				return false;
			}
			Unsafe.WriteUnaligned<uint>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000194A9 File Offset: 0x000176A9
		[CLSCompliant(false)]
		public unsafe static byte[] GetBytes(ulong value)
		{
			byte[] array = new byte[8];
			*Unsafe.As<byte, ulong>(ref array[0]) = value;
			return array;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000194BF File Offset: 0x000176BF
		[CLSCompliant(false)]
		public static bool TryWriteBytes(Span<byte> destination, ulong value)
		{
			if (destination.Length < 8)
			{
				return false;
			}
			Unsafe.WriteUnaligned<ulong>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000194DA File Offset: 0x000176DA
		public unsafe static byte[] GetBytes(float value)
		{
			byte[] array = new byte[4];
			*Unsafe.As<byte, float>(ref array[0]) = value;
			return array;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000194F0 File Offset: 0x000176F0
		public static bool TryWriteBytes(Span<byte> destination, float value)
		{
			if (destination.Length < 4)
			{
				return false;
			}
			Unsafe.WriteUnaligned<float>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001950B File Offset: 0x0001770B
		public unsafe static byte[] GetBytes(double value)
		{
			byte[] array = new byte[8];
			*Unsafe.As<byte, double>(ref array[0]) = value;
			return array;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00019521 File Offset: 0x00017721
		public static bool TryWriteBytes(Span<byte> destination, double value)
		{
			if (destination.Length < 8)
			{
				return false;
			}
			Unsafe.WriteUnaligned<double>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001953C File Offset: 0x0001773C
		public static char ToChar(byte[] value, int startIndex)
		{
			return (char)BitConverter.ToInt16(value, startIndex);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00019546 File Offset: 0x00017746
		public static char ToChar(ReadOnlySpan<byte> value)
		{
			if (value.Length < 2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<char>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00019564 File Offset: 0x00017764
		public static short ToInt16(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex >= value.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 2)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<short>(ref value[startIndex]);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001959B File Offset: 0x0001779B
		public static short ToInt16(ReadOnlySpan<byte> value)
		{
			if (value.Length < 2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<short>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000195B9 File Offset: 0x000177B9
		public static int ToInt32(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex >= value.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 4)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<int>(ref value[startIndex]);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000195F0 File Offset: 0x000177F0
		public static int ToInt32(ReadOnlySpan<byte> value)
		{
			if (value.Length < 4)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<int>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001960E File Offset: 0x0001780E
		public static long ToInt64(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex >= value.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 8)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<long>(ref value[startIndex]);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00019645 File Offset: 0x00017845
		public static long ToInt64(ReadOnlySpan<byte> value)
		{
			if (value.Length < 8)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<long>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001953C File Offset: 0x0001773C
		[CLSCompliant(false)]
		public static ushort ToUInt16(byte[] value, int startIndex)
		{
			return (ushort)BitConverter.ToInt16(value, startIndex);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00019663 File Offset: 0x00017863
		[CLSCompliant(false)]
		public static ushort ToUInt16(ReadOnlySpan<byte> value)
		{
			if (value.Length < 2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<ushort>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00019681 File Offset: 0x00017881
		[CLSCompliant(false)]
		public static uint ToUInt32(byte[] value, int startIndex)
		{
			return (uint)BitConverter.ToInt32(value, startIndex);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001968A File Offset: 0x0001788A
		[CLSCompliant(false)]
		public static uint ToUInt32(ReadOnlySpan<byte> value)
		{
			if (value.Length < 4)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<uint>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000196A8 File Offset: 0x000178A8
		[CLSCompliant(false)]
		public static ulong ToUInt64(byte[] value, int startIndex)
		{
			return (ulong)BitConverter.ToInt64(value, startIndex);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000196B1 File Offset: 0x000178B1
		[CLSCompliant(false)]
		public static ulong ToUInt64(ReadOnlySpan<byte> value)
		{
			if (value.Length < 8)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<ulong>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000196CF File Offset: 0x000178CF
		public static float ToSingle(byte[] value, int startIndex)
		{
			return BitConverter.Int32BitsToSingle(BitConverter.ToInt32(value, startIndex));
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000196DD File Offset: 0x000178DD
		public static float ToSingle(ReadOnlySpan<byte> value)
		{
			if (value.Length < 4)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<float>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000196FB File Offset: 0x000178FB
		public static double ToDouble(byte[] value, int startIndex)
		{
			return BitConverter.Int64BitsToDouble(BitConverter.ToInt64(value, startIndex));
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00019709 File Offset: 0x00017909
		public static double ToDouble(ReadOnlySpan<byte> value)
		{
			if (value.Length < 8)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<double>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00019728 File Offset: 0x00017928
		public unsafe static string ToString(byte[] value, int startIndex, int length)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex < 0 || (startIndex >= value.Length && startIndex > 0))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Value must be positive.");
			}
			if (startIndex > value.Length - length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (length > 715827882)
			{
				throw new ArgumentOutOfRangeException("length", SR.Format("The specified length exceeds the maximum value of {0}.", 715827882));
			}
			return string.Create<ValueTuple<byte[], int, int>>(length * 3 - 1, new ValueTuple<byte[], int, int>(value, startIndex, length), delegate(Span<char> dst, [TupleElementNames(new string[] { "value", "startIndex", "length" })] ValueTuple<byte[], int, int> state)
			{
				ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(state.Item1, state.Item2, state.Item3);
				int i = 0;
				int num = 0;
				byte b = *readOnlySpan[i++];
				*dst[num++] = "0123456789ABCDEF"[b >> 4];
				*dst[num++] = "0123456789ABCDEF"[(int)(b & 15)];
				while (i < readOnlySpan.Length)
				{
					b = *readOnlySpan[i++];
					*dst[num++] = '-';
					*dst[num++] = "0123456789ABCDEF"[b >> 4];
					*dst[num++] = "0123456789ABCDEF"[(int)(b & 15)];
				}
			});
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000197DB File Offset: 0x000179DB
		public static string ToString(byte[] value)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			return BitConverter.ToString(value, 0, value.Length);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000197F1 File Offset: 0x000179F1
		public static string ToString(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			return BitConverter.ToString(value, startIndex, value.Length - startIndex);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00019809 File Offset: 0x00017A09
		public static bool ToBoolean(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return value[startIndex] > 0;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00019839 File Offset: 0x00017A39
		public static bool ToBoolean(ReadOnlySpan<byte> value)
		{
			if (value.Length < 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<byte>(MemoryMarshal.GetReference<byte>(value)) > 0;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001985A File Offset: 0x00017A5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long DoubleToInt64Bits(double value)
		{
			return *(long*)(&value);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00019860 File Offset: 0x00017A60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double Int64BitsToDouble(long value)
		{
			return *(double*)(&value);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00019866 File Offset: 0x00017A66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int SingleToInt32Bits(float value)
		{
			return *(int*)(&value);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001986C File Offset: 0x00017A6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float Int32BitsToSingle(int value)
		{
			return *(float*)(&value);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00019874 File Offset: 0x00017A74
		unsafe static BitConverter()
		{
			ushort num = 4660;
			byte* ptr = (byte*)(&num);
			BitConverter.IsLittleEndian = *ptr == 52;
		}

		// Token: 0x04000EED RID: 3821
		[Intrinsic]
		public static readonly bool IsLittleEndian;

		// Token: 0x020000C8 RID: 200
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005C8 RID: 1480 RVA: 0x00019896 File Offset: 0x00017A96
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x000198A4 File Offset: 0x00017AA4
			internal unsafe void <ToString>b__38_0(Span<char> dst, [TupleElementNames(new string[] { "value", "startIndex", "length" })] ValueTuple<byte[], int, int> state)
			{
				ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(state.Item1, state.Item2, state.Item3);
				int i = 0;
				int num = 0;
				byte b = *readOnlySpan[i++];
				*dst[num++] = "0123456789ABCDEF"[b >> 4];
				*dst[num++] = "0123456789ABCDEF"[(int)(b & 15)];
				while (i < readOnlySpan.Length)
				{
					b = *readOnlySpan[i++];
					*dst[num++] = '-';
					*dst[num++] = "0123456789ABCDEF"[b >> 4];
					*dst[num++] = "0123456789ABCDEF"[(int)(b & 15)];
				}
			}

			// Token: 0x04000EEE RID: 3822
			public static readonly BitConverter.<>c <>9 = new BitConverter.<>c();

			// Token: 0x04000EEF RID: 3823
			[TupleElementNames(new string[] { "value", "startIndex", "length" })]
			public static SpanAction<char, ValueTuple<byte[], int, int>> <>9__38_0;
		}
	}
}
