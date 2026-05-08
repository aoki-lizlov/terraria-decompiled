using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200011A RID: 282
	internal static class Marvin
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002A784 File Offset: 0x00028984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ComputeHash32(ReadOnlySpan<byte> data, ulong seed)
		{
			return Marvin.ComputeHash32(MemoryMarshal.GetReference<byte>(data), data.Length, seed);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002A79C File Offset: 0x0002899C
		public unsafe static int ComputeHash32(ref byte data, int count, ulong seed)
		{
			ulong num = (ulong)((long)count);
			uint num2 = (uint)seed;
			uint num3 = (uint)(seed >> 32);
			ulong num4 = 0UL;
			while (num >= 8UL)
			{
				num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
				Marvin.Block(ref num2, ref num3);
				num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4 + 4UL));
				Marvin.Block(ref num2, ref num3);
				num4 += 8UL;
				num -= 8UL;
			}
			ulong num5 = num;
			if (num5 <= 7UL)
			{
				switch ((uint)num5)
				{
				case 0U:
					break;
				case 1U:
					goto IL_00CC;
				case 2U:
					goto IL_00FC;
				case 3U:
					goto IL_0130;
				case 4U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					Marvin.Block(ref num2, ref num3);
					break;
				case 5U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					num4 += 4UL;
					Marvin.Block(ref num2, ref num3);
					goto IL_00CC;
				case 6U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					num4 += 4UL;
					Marvin.Block(ref num2, ref num3);
					goto IL_00FC;
				case 7U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					num4 += 4UL;
					Marvin.Block(ref num2, ref num3);
					goto IL_0130;
				default:
					goto IL_0154;
				}
				num2 += 128U;
				goto IL_0154;
				IL_00CC:
				num2 += 32768U | (uint)(*Unsafe.AddByteOffset<byte>(ref data, num4));
				goto IL_0154;
				IL_00FC:
				num2 += 8388608U | (uint)Unsafe.ReadUnaligned<ushort>(Unsafe.AddByteOffset<byte>(ref data, num4));
				goto IL_0154;
				IL_0130:
				num2 += (uint)(int.MinValue | ((int)(*Unsafe.AddByteOffset<byte>(ref data, num4 + 2UL)) << 16) | (int)Unsafe.ReadUnaligned<ushort>(Unsafe.AddByteOffset<byte>(ref data, num4)));
			}
			IL_0154:
			Marvin.Block(ref num2, ref num3);
			Marvin.Block(ref num2, ref num3);
			return (int)(num3 ^ num2);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002A914 File Offset: 0x00028B14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Block(ref uint rp0, ref uint rp1)
		{
			uint num = rp0;
			uint num2 = rp1;
			num2 ^= num;
			num = Marvin._rotl(num, 20);
			num += num2;
			num2 = Marvin._rotl(num2, 9);
			num2 ^= num;
			num = Marvin._rotl(num, 27);
			num += num2;
			num2 = Marvin._rotl(num2, 19);
			rp0 = num;
			rp1 = num2;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002949F File Offset: 0x0002769F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint _rotl(uint value, int shift)
		{
			return (value << shift) | (value >> 32 - shift);
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002A961 File Offset: 0x00028B61
		public static ulong DefaultSeed
		{
			[CompilerGenerated]
			get
			{
				return Marvin.<DefaultSeed>k__BackingField;
			}
		} = Marvin.GenerateSeed();

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002A968 File Offset: 0x00028B68
		private static ulong GenerateSeed()
		{
			return 12874512UL;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002A970 File Offset: 0x00028B70
		// Note: this type is marked as 'beforefieldinit'.
		static Marvin()
		{
		}

		// Token: 0x040010F4 RID: 4340
		[CompilerGenerated]
		private static readonly ulong <DefaultSeed>k__BackingField;
	}
}
