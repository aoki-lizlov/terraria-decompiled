using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000440 RID: 1088
	public static class CryptographicOperations
	{
		// Token: 0x06002DA3 RID: 11683 RVA: 0x000A5AC8 File Offset: 0x000A3CC8
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public unsafe static bool FixedTimeEquals(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			int length = left.Length;
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				num |= (int)(*left[i] - *right[i]);
			}
			return num == 0;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000A5B17 File Offset: 0x000A3D17
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static void ZeroMemory(Span<byte> buffer)
		{
			buffer.Clear();
		}
	}
}
