using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Threading
{
	// Token: 0x020002CB RID: 715
	public static class Volatile
	{
		// Token: 0x060020F9 RID: 8441 RVA: 0x0007845A File Offset: 0x0007665A
		[Intrinsic]
		public static bool Read(ref bool location)
		{
			return Unsafe.As<bool, Volatile.VolatileBoolean>(ref location).Value;
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00078469 File Offset: 0x00076669
		[Intrinsic]
		public static void Write(ref bool location, bool value)
		{
			Unsafe.As<bool, Volatile.VolatileBoolean>(ref location).Value = value;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00078479 File Offset: 0x00076679
		[Intrinsic]
		public static byte Read(ref byte location)
		{
			return Unsafe.As<byte, Volatile.VolatileByte>(ref location).Value;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00078488 File Offset: 0x00076688
		[Intrinsic]
		public static void Write(ref byte location, byte value)
		{
			Unsafe.As<byte, Volatile.VolatileByte>(ref location).Value = value;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x00078498 File Offset: 0x00076698
		[Intrinsic]
		public static short Read(ref short location)
		{
			return Unsafe.As<short, Volatile.VolatileInt16>(ref location).Value;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000784A7 File Offset: 0x000766A7
		[Intrinsic]
		public static void Write(ref short location, short value)
		{
			Unsafe.As<short, Volatile.VolatileInt16>(ref location).Value = value;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000784B7 File Offset: 0x000766B7
		[Intrinsic]
		public static int Read(ref int location)
		{
			return Unsafe.As<int, Volatile.VolatileInt32>(ref location).Value;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000784C6 File Offset: 0x000766C6
		[Intrinsic]
		public static void Write(ref int location, int value)
		{
			Unsafe.As<int, Volatile.VolatileInt32>(ref location).Value = value;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000784D6 File Offset: 0x000766D6
		[Intrinsic]
		public static IntPtr Read(ref IntPtr location)
		{
			return Unsafe.As<IntPtr, Volatile.VolatileIntPtr>(ref location).Value;
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000784E5 File Offset: 0x000766E5
		[Intrinsic]
		public static void Write(ref IntPtr location, IntPtr value)
		{
			Unsafe.As<IntPtr, Volatile.VolatileIntPtr>(ref location).Value = value;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000784F5 File Offset: 0x000766F5
		[CLSCompliant(false)]
		[Intrinsic]
		public static sbyte Read(ref sbyte location)
		{
			return Unsafe.As<sbyte, Volatile.VolatileSByte>(ref location).Value;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00078504 File Offset: 0x00076704
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref sbyte location, sbyte value)
		{
			Unsafe.As<sbyte, Volatile.VolatileSByte>(ref location).Value = value;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00078514 File Offset: 0x00076714
		[Intrinsic]
		public static float Read(ref float location)
		{
			return Unsafe.As<float, Volatile.VolatileSingle>(ref location).Value;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x00078523 File Offset: 0x00076723
		[Intrinsic]
		public static void Write(ref float location, float value)
		{
			Unsafe.As<float, Volatile.VolatileSingle>(ref location).Value = value;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00078533 File Offset: 0x00076733
		[CLSCompliant(false)]
		[Intrinsic]
		public static ushort Read(ref ushort location)
		{
			return Unsafe.As<ushort, Volatile.VolatileUInt16>(ref location).Value;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00078542 File Offset: 0x00076742
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref ushort location, ushort value)
		{
			Unsafe.As<ushort, Volatile.VolatileUInt16>(ref location).Value = value;
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x00078552 File Offset: 0x00076752
		[CLSCompliant(false)]
		[Intrinsic]
		public static uint Read(ref uint location)
		{
			return Unsafe.As<uint, Volatile.VolatileUInt32>(ref location).Value;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00078561 File Offset: 0x00076761
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref uint location, uint value)
		{
			Unsafe.As<uint, Volatile.VolatileUInt32>(ref location).Value = value;
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00078571 File Offset: 0x00076771
		[CLSCompliant(false)]
		[Intrinsic]
		public static UIntPtr Read(ref UIntPtr location)
		{
			return Unsafe.As<UIntPtr, Volatile.VolatileUIntPtr>(ref location).Value;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00078580 File Offset: 0x00076780
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref UIntPtr location, UIntPtr value)
		{
			Unsafe.As<UIntPtr, Volatile.VolatileUIntPtr>(ref location).Value = value;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00078590 File Offset: 0x00076790
		[Intrinsic]
		public static T Read<T>(ref T location) where T : class
		{
			return Unsafe.As<T>(Unsafe.As<T, Volatile.VolatileObject>(ref location).Value);
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000785A4 File Offset: 0x000767A4
		[Intrinsic]
		public static void Write<T>(ref T location, T value) where T : class
		{
			Unsafe.As<T, Volatile.VolatileObject>(ref location).Value = value;
		}

		// Token: 0x0600210F RID: 8463
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Read(ref long location);

		// Token: 0x06002110 RID: 8464
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong Read(ref ulong location);

		// Token: 0x06002111 RID: 8465
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Read(ref double location);

		// Token: 0x06002112 RID: 8466
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref long location, long value);

		// Token: 0x06002113 RID: 8467
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref ulong location, ulong value);

		// Token: 0x06002114 RID: 8468
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref double location, double value);

		// Token: 0x020002CC RID: 716
		private struct VolatileBoolean
		{
			// Token: 0x04001A87 RID: 6791
			public volatile bool Value;
		}

		// Token: 0x020002CD RID: 717
		private struct VolatileByte
		{
			// Token: 0x04001A88 RID: 6792
			public volatile byte Value;
		}

		// Token: 0x020002CE RID: 718
		private struct VolatileInt16
		{
			// Token: 0x04001A89 RID: 6793
			public volatile short Value;
		}

		// Token: 0x020002CF RID: 719
		private struct VolatileInt32
		{
			// Token: 0x04001A8A RID: 6794
			public volatile int Value;
		}

		// Token: 0x020002D0 RID: 720
		private struct VolatileIntPtr
		{
			// Token: 0x04001A8B RID: 6795
			public volatile IntPtr Value;
		}

		// Token: 0x020002D1 RID: 721
		private struct VolatileSByte
		{
			// Token: 0x04001A8C RID: 6796
			public volatile sbyte Value;
		}

		// Token: 0x020002D2 RID: 722
		private struct VolatileSingle
		{
			// Token: 0x04001A8D RID: 6797
			public volatile float Value;
		}

		// Token: 0x020002D3 RID: 723
		private struct VolatileUInt16
		{
			// Token: 0x04001A8E RID: 6798
			public volatile ushort Value;
		}

		// Token: 0x020002D4 RID: 724
		private struct VolatileUInt32
		{
			// Token: 0x04001A8F RID: 6799
			public volatile uint Value;
		}

		// Token: 0x020002D5 RID: 725
		private struct VolatileUIntPtr
		{
			// Token: 0x04001A90 RID: 6800
			public volatile UIntPtr Value;
		}

		// Token: 0x020002D6 RID: 726
		private struct VolatileObject
		{
			// Token: 0x04001A91 RID: 6801
			public volatile object Value;
		}
	}
}
