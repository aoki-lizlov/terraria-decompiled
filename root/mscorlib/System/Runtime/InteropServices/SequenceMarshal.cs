using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200069B RID: 1691
	public static class SequenceMarshal
	{
		// Token: 0x06003F93 RID: 16275 RVA: 0x000DFEBB File Offset: 0x000DE0BB
		public static bool TryGetReadOnlySequenceSegment<T>(ReadOnlySequence<T> sequence, out ReadOnlySequenceSegment<T> startSegment, out int startIndex, out ReadOnlySequenceSegment<T> endSegment, out int endIndex)
		{
			return sequence.TryGetReadOnlySequenceSegment(out startSegment, out startIndex, out endSegment, out endIndex);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000DFEC9 File Offset: 0x000DE0C9
		public static bool TryGetArray<T>(ReadOnlySequence<T> sequence, out ArraySegment<T> segment)
		{
			return sequence.TryGetArray(out segment);
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x000DFED3 File Offset: 0x000DE0D3
		public static bool TryGetReadOnlyMemory<T>(ReadOnlySequence<T> sequence, out ReadOnlyMemory<T> memory)
		{
			if (!sequence.IsSingleSegment)
			{
				memory = default(ReadOnlyMemory<T>);
				return false;
			}
			memory = sequence.First;
			return true;
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000DFEF5 File Offset: 0x000DE0F5
		internal static bool TryGetString(ReadOnlySequence<char> sequence, out string text, out int start, out int length)
		{
			return sequence.TryGetString(out text, out start, out length);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000DFF01 File Offset: 0x000DE101
		public static bool TryRead<[IsUnmanaged] T>(ref SequenceReader<byte> reader, out T value) where T : struct, ValueType
		{
			return (ref reader).TryRead(out value);
		}
	}
}
