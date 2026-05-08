using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000B41 RID: 2881
	internal static class ReadOnlySequence
	{
		// Token: 0x06006960 RID: 26976 RVA: 0x00165991 File Offset: 0x00163B91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x00165991 File Offset: 0x00163B91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x00165991 File Offset: 0x00163B91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x06006963 RID: 26979 RVA: 0x00165996 File Offset: 0x00163B96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x06006964 RID: 26980 RVA: 0x00165996 File Offset: 0x00163B96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x06006965 RID: 26981 RVA: 0x00165991 File Offset: 0x00163B91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x06006966 RID: 26982 RVA: 0x00165996 File Offset: 0x00163B96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x06006967 RID: 26983 RVA: 0x00165996 File Offset: 0x00163B96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x04003C9E RID: 15518
		public const int FlagBitMask = -2147483648;

		// Token: 0x04003C9F RID: 15519
		public const int IndexBitMask = 2147483647;

		// Token: 0x04003CA0 RID: 15520
		public const int SegmentStartMask = 0;

		// Token: 0x04003CA1 RID: 15521
		public const int SegmentEndMask = 0;

		// Token: 0x04003CA2 RID: 15522
		public const int ArrayStartMask = 0;

		// Token: 0x04003CA3 RID: 15523
		public const int ArrayEndMask = -2147483648;

		// Token: 0x04003CA4 RID: 15524
		public const int MemoryManagerStartMask = -2147483648;

		// Token: 0x04003CA5 RID: 15525
		public const int MemoryManagerEndMask = 0;

		// Token: 0x04003CA6 RID: 15526
		public const int StringStartMask = -2147483648;

		// Token: 0x04003CA7 RID: 15527
		public const int StringEndMask = -2147483648;
	}
}
