using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000B48 RID: 2888
	public static class SequenceReaderExtensions
	{
		// Token: 0x060069C2 RID: 27074 RVA: 0x001671D8 File Offset: 0x001653D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryRead<[IsUnmanaged] T>(this SequenceReader<byte> reader, out T value) where T : struct, ValueType
		{
			ReadOnlySpan<byte> unreadSpan = reader.UnreadSpan;
			if (unreadSpan.Length < sizeof(T))
			{
				return SequenceReaderExtensions.TryReadMultisegment<T>(ref reader, out value);
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(unreadSpan));
			reader.Advance((long)sizeof(T));
			return true;
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x00167224 File Offset: 0x00165424
		private unsafe static bool TryReadMultisegment<[IsUnmanaged] T>(ref SequenceReader<byte> reader, out T value) where T : struct, ValueType
		{
			T t = default(T);
			Span<byte> span = new Span<byte>((void*)(&t), sizeof(T));
			if (!reader.TryCopyTo(span))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(span));
			reader.Advance((long)sizeof(T));
			return true;
		}

		// Token: 0x060069C4 RID: 27076 RVA: 0x0016727A File Offset: 0x0016547A
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060069C5 RID: 27077 RVA: 0x00167292 File Offset: 0x00165492
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x001672AA File Offset: 0x001654AA
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out short value)
		{
			if ((ref reader).TryRead(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}

		// Token: 0x060069C7 RID: 27079 RVA: 0x001672C1 File Offset: 0x001654C1
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x001672D9 File Offset: 0x001654D9
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060069C9 RID: 27081 RVA: 0x001672F1 File Offset: 0x001654F1
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out int value)
		{
			if ((ref reader).TryRead(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x00167308 File Offset: 0x00165508
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060069CB RID: 27083 RVA: 0x00167320 File Offset: 0x00165520
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060069CC RID: 27084 RVA: 0x00167338 File Offset: 0x00165538
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out long value)
		{
			if ((ref reader).TryRead(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}
	}
}
