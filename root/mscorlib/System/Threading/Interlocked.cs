using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020002BE RID: 702
	public static class Interlocked
	{
		// Token: 0x06002063 RID: 8291
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CompareExchange(ref int location1, int value, int comparand);

		// Token: 0x06002064 RID: 8292
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int CompareExchange(ref int location1, int value, int comparand, ref bool succeeded);

		// Token: 0x06002065 RID: 8293
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CompareExchange(ref object location1, ref object value, ref object comparand, ref object result);

		// Token: 0x06002066 RID: 8294 RVA: 0x00076B20 File Offset: 0x00074D20
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static object CompareExchange(ref object location1, object value, object comparand)
		{
			object obj = null;
			Interlocked.CompareExchange(ref location1, ref value, ref comparand, ref obj);
			return obj;
		}

		// Token: 0x06002067 RID: 8295
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CompareExchange(ref float location1, float value, float comparand);

		// Token: 0x06002068 RID: 8296
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Decrement(ref int location);

		// Token: 0x06002069 RID: 8297
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Decrement(ref long location);

		// Token: 0x0600206A RID: 8298
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Increment(ref int location);

		// Token: 0x0600206B RID: 8299
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Increment(ref long location);

		// Token: 0x0600206C RID: 8300
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Exchange(ref int location1, int value);

		// Token: 0x0600206D RID: 8301
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Exchange(ref object location1, ref object value, ref object result);

		// Token: 0x0600206E RID: 8302 RVA: 0x00076B3C File Offset: 0x00074D3C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static object Exchange(ref object location1, object value)
		{
			object obj = null;
			Interlocked.Exchange(ref location1, ref value, ref obj);
			return obj;
		}

		// Token: 0x0600206F RID: 8303
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Exchange(ref float location1, float value);

		// Token: 0x06002070 RID: 8304
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CompareExchange(ref long location1, long value, long comparand);

		// Token: 0x06002071 RID: 8305
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CompareExchange(ref IntPtr location1, IntPtr value, IntPtr comparand);

		// Token: 0x06002072 RID: 8306
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CompareExchange(ref double location1, double value, double comparand);

		// Token: 0x06002073 RID: 8307 RVA: 0x00076B58 File Offset: 0x00074D58
		[ComVisible(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[Intrinsic]
		public static T CompareExchange<T>(ref T location1, T value, T comparand) where T : class
		{
			if (Unsafe.AsPointer<T>(ref location1) == null)
			{
				throw new NullReferenceException();
			}
			T t = default(T);
			Interlocked.CompareExchange(Unsafe.As<T, object>(ref location1), Unsafe.As<T, object>(ref value), Unsafe.As<T, object>(ref comparand), Unsafe.As<T, object>(ref t));
			return t;
		}

		// Token: 0x06002074 RID: 8308
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Exchange(ref long location1, long value);

		// Token: 0x06002075 RID: 8309
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Exchange(ref IntPtr location1, IntPtr value);

		// Token: 0x06002076 RID: 8310
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exchange(ref double location1, double value);

		// Token: 0x06002077 RID: 8311 RVA: 0x00076BA0 File Offset: 0x00074DA0
		[ComVisible(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[Intrinsic]
		public static T Exchange<T>(ref T location1, T value) where T : class
		{
			if (Unsafe.AsPointer<T>(ref location1) == null)
			{
				throw new NullReferenceException();
			}
			T t = default(T);
			Interlocked.Exchange(Unsafe.As<T, object>(ref location1), Unsafe.As<T, object>(ref value), Unsafe.As<T, object>(ref t));
			return t;
		}

		// Token: 0x06002078 RID: 8312
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Read(ref long location);

		// Token: 0x06002079 RID: 8313
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Add(ref int location1, int value);

		// Token: 0x0600207A RID: 8314
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Add(ref long location1, long value);

		// Token: 0x0600207B RID: 8315 RVA: 0x00076BDF File Offset: 0x00074DDF
		public static void MemoryBarrier()
		{
			Thread.MemoryBarrier();
		}

		// Token: 0x0600207C RID: 8316
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrierProcessWide();
	}
}
