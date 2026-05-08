using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000FC RID: 252
	public struct HashCode
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x00028E6C File Offset: 0x0002706C
		private unsafe static uint GenerateGlobalSeed()
		{
			uint num;
			Interop.GetRandomBytes((byte*)(&num), 4);
			return num;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00028E84 File Offset: 0x00027084
		public static int Combine<T1>(T1 value1)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.MixEmptyState() + 4U, num));
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00028EBC File Offset: 0x000270BC
		public static int Combine<T1, T2>(T1 value1, T2 value2)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixEmptyState() + 8U, num), num2));
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00028F14 File Offset: 0x00027114
		public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixEmptyState() + 12U, num), num2), num3));
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00028F8C File Offset: 0x0002718C
		public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5;
			uint num6;
			uint num7;
			uint num8;
			HashCode.Initialize(out num5, out num6, out num7, out num8);
			num5 = HashCode.Round(num5, num);
			num6 = HashCode.Round(num6, num2);
			num7 = HashCode.Round(num7, num3);
			num8 = HashCode.Round(num8, num4);
			return (int)HashCode.MixFinal(HashCode.MixState(num5, num6, num7, num8) + 16U);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00029048 File Offset: 0x00027248
		public static int Combine<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num6;
			uint num7;
			uint num8;
			uint num9;
			HashCode.Initialize(out num6, out num7, out num8, out num9);
			num6 = HashCode.Round(num6, num);
			num7 = HashCode.Round(num7, num2);
			num8 = HashCode.Round(num8, num3);
			num9 = HashCode.Round(num9, num4);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.MixState(num6, num7, num8, num9) + 20U, num5));
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00029128 File Offset: 0x00027328
		public static int Combine<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num6 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint num7;
			uint num8;
			uint num9;
			uint num10;
			HashCode.Initialize(out num7, out num8, out num9, out num10);
			num7 = HashCode.Round(num7, num);
			num8 = HashCode.Round(num8, num2);
			num9 = HashCode.Round(num9, num3);
			num10 = HashCode.Round(num10, num4);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixState(num7, num8, num9, num10) + 24U, num5), num6));
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00029228 File Offset: 0x00027428
		public static int Combine<T1, T2, T3, T4, T5, T6, T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num6 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint num7 = (uint)((value7 != null) ? value7.GetHashCode() : 0);
			uint num8;
			uint num9;
			uint num10;
			uint num11;
			HashCode.Initialize(out num8, out num9, out num10, out num11);
			num8 = HashCode.Round(num8, num);
			num9 = HashCode.Round(num9, num2);
			num10 = HashCode.Round(num10, num3);
			num11 = HashCode.Round(num11, num4);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixState(num8, num9, num10, num11) + 28U, num5), num6), num7));
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002934C File Offset: 0x0002754C
		public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
		{
			uint num = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num6 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint num7 = (uint)((value7 != null) ? value7.GetHashCode() : 0);
			uint num8 = (uint)((value8 != null) ? value8.GetHashCode() : 0);
			uint num9;
			uint num10;
			uint num11;
			uint num12;
			HashCode.Initialize(out num9, out num10, out num11, out num12);
			num9 = HashCode.Round(num9, num);
			num10 = HashCode.Round(num10, num2);
			num11 = HashCode.Round(num11, num3);
			num12 = HashCode.Round(num12, num4);
			num9 = HashCode.Round(num9, num5);
			num10 = HashCode.Round(num10, num6);
			num11 = HashCode.Round(num11, num7);
			num12 = HashCode.Round(num12, num8);
			return (int)HashCode.MixFinal(HashCode.MixState(num9, num10, num11, num12) + 32U);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002949F File Offset: 0x0002769F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Rol(uint value, int count)
		{
			return (value << count) | (value >> 32 - count);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000294B1 File Offset: 0x000276B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Initialize(out uint v1, out uint v2, out uint v3, out uint v4)
		{
			v1 = HashCode.s_seed + 2654435761U + 2246822519U;
			v2 = HashCode.s_seed + 2246822519U;
			v3 = HashCode.s_seed;
			v4 = HashCode.s_seed - 2654435761U;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000294E7 File Offset: 0x000276E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Round(uint hash, uint input)
		{
			hash += input * 2246822519U;
			hash = HashCode.Rol(hash, 13);
			hash *= 2654435761U;
			return hash;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00029508 File Offset: 0x00027708
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint QueueRound(uint hash, uint queuedValue)
		{
			hash += queuedValue * 3266489917U;
			return HashCode.Rol(hash, 17) * 668265263U;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00029523 File Offset: 0x00027723
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixState(uint v1, uint v2, uint v3, uint v4)
		{
			return HashCode.Rol(v1, 1) + HashCode.Rol(v2, 7) + HashCode.Rol(v3, 12) + HashCode.Rol(v4, 18);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00029546 File Offset: 0x00027746
		private static uint MixEmptyState()
		{
			return HashCode.s_seed + 374761393U;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00029553 File Offset: 0x00027753
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixFinal(uint hash)
		{
			hash ^= hash >> 15;
			hash *= 2246822519U;
			hash ^= hash >> 13;
			hash *= 3266489917U;
			hash ^= hash >> 16;
			return hash;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00029580 File Offset: 0x00027780
		public void Add<T>(T value)
		{
			this.Add((value != null) ? value.GetHashCode() : 0);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000295A0 File Offset: 0x000277A0
		public void Add<T>(T value, IEqualityComparer<T> comparer)
		{
			this.Add((comparer != null) ? comparer.GetHashCode(value) : ((value != null) ? value.GetHashCode() : 0));
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000295CC File Offset: 0x000277CC
		private void Add(int value)
		{
			uint length = this._length;
			this._length = length + 1U;
			uint num = length;
			uint num2 = num % 4U;
			if (num2 == 0U)
			{
				this._queue1 = (uint)value;
				return;
			}
			if (num2 == 1U)
			{
				this._queue2 = (uint)value;
				return;
			}
			if (num2 == 2U)
			{
				this._queue3 = (uint)value;
				return;
			}
			if (num == 3U)
			{
				HashCode.Initialize(out this._v1, out this._v2, out this._v3, out this._v4);
			}
			this._v1 = HashCode.Round(this._v1, this._queue1);
			this._v2 = HashCode.Round(this._v2, this._queue2);
			this._v3 = HashCode.Round(this._v3, this._queue3);
			this._v4 = HashCode.Round(this._v4, (uint)value);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002968C File Offset: 0x0002788C
		public int ToHashCode()
		{
			uint length = this._length;
			uint num = length % 4U;
			uint num2 = ((length < 4U) ? HashCode.MixEmptyState() : HashCode.MixState(this._v1, this._v2, this._v3, this._v4));
			num2 += length * 4U;
			if (num > 0U)
			{
				num2 = HashCode.QueueRound(num2, this._queue1);
				if (num > 1U)
				{
					num2 = HashCode.QueueRound(num2, this._queue2);
					if (num > 2U)
					{
						num2 = HashCode.QueueRound(num2, this._queue3);
					}
				}
			}
			return (int)HashCode.MixFinal(num2);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002970E File Offset: 0x0002790E
		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.", true)]
		public override int GetHashCode()
		{
			throw new NotSupportedException("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.");
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002971A File Offset: 0x0002791A
		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes.", true)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException("HashCode is a mutable struct and should not be compared with other HashCodes.");
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00029726 File Offset: 0x00027926
		// Note: this type is marked as 'beforefieldinit'.
		static HashCode()
		{
		}

		// Token: 0x040010C6 RID: 4294
		private static readonly uint s_seed = HashCode.GenerateGlobalSeed();

		// Token: 0x040010C7 RID: 4295
		private const uint Prime1 = 2654435761U;

		// Token: 0x040010C8 RID: 4296
		private const uint Prime2 = 2246822519U;

		// Token: 0x040010C9 RID: 4297
		private const uint Prime3 = 3266489917U;

		// Token: 0x040010CA RID: 4298
		private const uint Prime4 = 668265263U;

		// Token: 0x040010CB RID: 4299
		private const uint Prime5 = 374761393U;

		// Token: 0x040010CC RID: 4300
		private uint _v1;

		// Token: 0x040010CD RID: 4301
		private uint _v2;

		// Token: 0x040010CE RID: 4302
		private uint _v3;

		// Token: 0x040010CF RID: 4303
		private uint _v4;

		// Token: 0x040010D0 RID: 4304
		private uint _queue1;

		// Token: 0x040010D1 RID: 4305
		private uint _queue2;

		// Token: 0x040010D2 RID: 4306
		private uint _queue3;

		// Token: 0x040010D3 RID: 4307
		private uint _length;
	}
}
