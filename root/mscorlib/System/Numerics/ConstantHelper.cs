using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x0200091B RID: 2331
	internal class ConstantHelper
	{
		// Token: 0x060052C3 RID: 21187 RVA: 0x001057E8 File Offset: 0x001039E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static byte GetByteWithAllBitsSet()
		{
			byte b = 0;
			*(&b) = byte.MaxValue;
			return b;
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x00105804 File Offset: 0x00103A04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static sbyte GetSByteWithAllBitsSet()
		{
			sbyte b = 0;
			*(&b) = -1;
			return b;
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x0010581C File Offset: 0x00103A1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetUInt16WithAllBitsSet()
		{
			ushort num = 0;
			*(&num) = ushort.MaxValue;
			return num;
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x00105838 File Offset: 0x00103A38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static short GetInt16WithAllBitsSet()
		{
			short num = 0;
			*(&num) = -1;
			return num;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x00105850 File Offset: 0x00103A50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint GetUInt32WithAllBitsSet()
		{
			uint num = 0U;
			*(&num) = uint.MaxValue;
			return num;
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x00105868 File Offset: 0x00103A68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetInt32WithAllBitsSet()
		{
			int num = 0;
			*(&num) = -1;
			return num;
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x00105880 File Offset: 0x00103A80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong GetUInt64WithAllBitsSet()
		{
			ulong num = 0UL;
			*(&num) = ulong.MaxValue;
			return num;
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x00105898 File Offset: 0x00103A98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long GetInt64WithAllBitsSet()
		{
			long num = 0L;
			*(&num) = -1L;
			return num;
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x001058B0 File Offset: 0x00103AB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float GetSingleWithAllBitsSet()
		{
			float num = 0f;
			*(int*)(&num) = -1;
			return num;
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x001058CC File Offset: 0x00103ACC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double GetDoubleWithAllBitsSet()
		{
			double num = 0.0;
			*(long*)(&num) = -1L;
			return num;
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x000025BE File Offset: 0x000007BE
		public ConstantHelper()
		{
		}
	}
}
