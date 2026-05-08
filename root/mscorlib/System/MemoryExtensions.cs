using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000120 RID: 288
	public static class MemoryExtensions
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x0002B827 File Offset: 0x00029A27
		public static bool Contains(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			return span.IndexOf(value, comparisonType) >= 0;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002B838 File Offset: 0x00029A38
		public static bool Equals(this ReadOnlySpan<char> span, ReadOnlySpan<char> other, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.CompareOptionNone(span, other) == 0;
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.CompareOptionIgnoreCase(span, other) == 0;
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.CompareOptionNone(span, other) == 0;
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.CompareOptionIgnoreCase(span, other) == 0;
			case StringComparison.Ordinal:
				return span.EqualsOrdinal(other);
			case StringComparison.OrdinalIgnoreCase:
				return span.EqualsOrdinalIgnoreCase(other);
			default:
				return false;
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002B8C6 File Offset: 0x00029AC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool EqualsOrdinal(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length == value.Length && (value.Length == 0 || span.SequenceEqual(value));
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002B8EC File Offset: 0x00029AEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool EqualsOrdinalIgnoreCase(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length == value.Length && (value.Length == 0 || CompareInfo.CompareOrdinalIgnoreCase(span, value) == 0);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002B918 File Offset: 0x00029B18
		internal unsafe static bool Contains(this ReadOnlySpan<char> source, char value)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (*source[i] == (ushort)value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002B948 File Offset: 0x00029B48
		public static int CompareTo(this ReadOnlySpan<char> span, ReadOnlySpan<char> other, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.CompareOptionNone(span, other);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.CompareOptionIgnoreCase(span, other);
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.CompareOptionNone(span, other);
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.CompareOptionIgnoreCase(span, other);
			case StringComparison.Ordinal:
				if (span.Length == 0 || other.Length == 0)
				{
					return span.Length - other.Length;
				}
				return string.CompareOrdinal(span, other);
			case StringComparison.OrdinalIgnoreCase:
				return CompareInfo.CompareOrdinalIgnoreCase(span, other);
			default:
				return 0;
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002B9EC File Offset: 0x00029BEC
		public static int IndexOf(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			if (value.Length == 0)
			{
				return 0;
			}
			if (span.Length == 0)
			{
				return -1;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return SpanHelpers.IndexOfCultureHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.CurrentCultureIgnoreCase:
				return SpanHelpers.IndexOfCultureIgnoreCaseHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.InvariantCulture:
				return SpanHelpers.IndexOfCultureHelper(span, value, CompareInfo.Invariant);
			case StringComparison.InvariantCultureIgnoreCase:
				return SpanHelpers.IndexOfCultureIgnoreCaseHelper(span, value, CompareInfo.Invariant);
			case StringComparison.Ordinal:
				return SpanHelpers.IndexOfOrdinalHelper(span, value, false);
			case StringComparison.OrdinalIgnoreCase:
				return SpanHelpers.IndexOfOrdinalHelper(span, value, true);
			default:
				return -1;
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002BA88 File Offset: 0x00029C88
		public static int ToLower(this ReadOnlySpan<char> source, Span<char> destination, CultureInfo culture)
		{
			if (culture == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				culture.TextInfo.ToLowerAsciiInvariant(source, destination);
			}
			else
			{
				culture.TextInfo.ChangeCase(source, destination, false);
			}
			return source.Length;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002BADC File Offset: 0x00029CDC
		public static int ToLowerInvariant(this ReadOnlySpan<char> source, Span<char> destination)
		{
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				CultureInfo.InvariantCulture.TextInfo.ToLowerAsciiInvariant(source, destination);
			}
			else
			{
				CultureInfo.InvariantCulture.TextInfo.ChangeCase(source, destination, false);
			}
			return source.Length;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002BB30 File Offset: 0x00029D30
		public static int ToUpper(this ReadOnlySpan<char> source, Span<char> destination, CultureInfo culture)
		{
			if (culture == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				culture.TextInfo.ToUpperAsciiInvariant(source, destination);
			}
			else
			{
				culture.TextInfo.ChangeCase(source, destination, true);
			}
			return source.Length;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002BB84 File Offset: 0x00029D84
		public static int ToUpperInvariant(this ReadOnlySpan<char> source, Span<char> destination)
		{
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				CultureInfo.InvariantCulture.TextInfo.ToUpperAsciiInvariant(source, destination);
			}
			else
			{
				CultureInfo.InvariantCulture.TextInfo.ChangeCase(source, destination, true);
			}
			return source.Length;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002BBD8 File Offset: 0x00029DD8
		public static bool EndsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			if (value.Length == 0)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return SpanHelpers.EndsWithCultureHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.CurrentCultureIgnoreCase:
				return SpanHelpers.EndsWithCultureIgnoreCaseHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.InvariantCulture:
				return SpanHelpers.EndsWithCultureHelper(span, value, CompareInfo.Invariant);
			case StringComparison.InvariantCultureIgnoreCase:
				return SpanHelpers.EndsWithCultureIgnoreCaseHelper(span, value, CompareInfo.Invariant);
			case StringComparison.Ordinal:
				return span.EndsWith(value);
			case StringComparison.OrdinalIgnoreCase:
				return SpanHelpers.EndsWithOrdinalIgnoreCaseHelper(span, value);
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002BC74 File Offset: 0x00029E74
		public static bool StartsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			if (value.Length == 0)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return SpanHelpers.StartsWithCultureHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.CurrentCultureIgnoreCase:
				return SpanHelpers.StartsWithCultureIgnoreCaseHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.InvariantCulture:
				return SpanHelpers.StartsWithCultureHelper(span, value, CompareInfo.Invariant);
			case StringComparison.InvariantCultureIgnoreCase:
				return SpanHelpers.StartsWithCultureIgnoreCaseHelper(span, value, CompareInfo.Invariant);
			case StringComparison.Ordinal:
				return span.StartsWith(value);
			case StringComparison.OrdinalIgnoreCase:
				return SpanHelpers.StartsWithOrdinalIgnoreCaseHelper(span, value);
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002BD10 File Offset: 0x00029F10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), start), array.Length - start);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002BD84 File Offset: 0x00029F84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, Index startIndex)
		{
			if (array == null)
			{
				if (!startIndex.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			int offset = startIndex.GetOffset(array.Length);
			if (offset > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), offset), array.Length - offset);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002BE10 File Offset: 0x0002A010
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, Range range)
		{
			if (array == null)
			{
				Index start = range.Start;
				Index end = range.End;
				if (!start.Equals(Index.Start) || !end.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(array.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new Span<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), item), item2);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002BEB8 File Offset: 0x0002A0B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text)
		{
			if (text == null)
			{
				return default(ReadOnlySpan<char>);
			}
			return new ReadOnlySpan<char>(text.GetRawStringData(), text.Length);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002BEE4 File Offset: 0x0002A0E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text, int start)
		{
			if (text == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if (start > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.Add<char>(text.GetRawStringData(), start), text.Length - start);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002BF34 File Offset: 0x0002A134
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text, int start, int length)
		{
			if (text == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if (start > text.Length || length > text.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.Add<char>(text.GetRawStringData(), start), length);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002BF88 File Offset: 0x0002A188
		public static ReadOnlyMemory<char> AsMemory(this string text)
		{
			if (text == null)
			{
				return default(ReadOnlyMemory<char>);
			}
			return new ReadOnlyMemory<char>(text, 0, text.Length);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002BFB0 File Offset: 0x0002A1B0
		public static ReadOnlyMemory<char> AsMemory(this string text, int start)
		{
			if (text == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlyMemory<char>);
			}
			if (start > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<char>(text, start, text.Length - start);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002BFF4 File Offset: 0x0002A1F4
		public static ReadOnlyMemory<char> AsMemory(this string text, Index startIndex)
		{
			if (text == null)
			{
				if (!startIndex.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);
				}
				return default(ReadOnlyMemory<char>);
			}
			int offset = startIndex.GetOffset(text.Length);
			if (offset > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new ReadOnlyMemory<char>(text, offset, text.Length - offset);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002C050 File Offset: 0x0002A250
		public static ReadOnlyMemory<char> AsMemory(this string text, int start, int length)
		{
			if (text == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlyMemory<char>);
			}
			if (start > text.Length || length > text.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<char>(text, start, length);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002C09C File Offset: 0x0002A29C
		public static ReadOnlyMemory<char> AsMemory(this string text, Range range)
		{
			if (text == null)
			{
				Index start = range.Start;
				Index end = range.End;
				if (!start.Equals(Index.Start) || !end.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);
				}
				return default(ReadOnlyMemory<char>);
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(text.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new ReadOnlyMemory<char>(text, item, item2);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002C10C File Offset: 0x0002A30C
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span)
		{
			return span.TrimStart().TrimEnd();
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002C11C File Offset: 0x0002A31C
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span)
		{
			int num = 0;
			while (num < span.Length && char.IsWhiteSpace((char)(*span[num])))
			{
				num++;
			}
			return span.Slice(num);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002C154 File Offset: 0x0002A354
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span)
		{
			int num = span.Length - 1;
			while (num >= 0 && char.IsWhiteSpace((char)(*span[num])))
			{
				num--;
			}
			return span.Slice(0, num + 1);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002C190 File Offset: 0x0002A390
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span, char trimChar)
		{
			return span.TrimStart(trimChar).TrimEnd(trimChar);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span, char trimChar)
		{
			int num = 0;
			while (num < span.Length && *span[num] == (ushort)trimChar)
			{
				num++;
			}
			return span.Slice(num);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002C1D4 File Offset: 0x0002A3D4
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span, char trimChar)
		{
			int num = span.Length - 1;
			while (num >= 0 && *span[num] == (ushort)trimChar)
			{
				num--;
			}
			return span.Slice(0, num + 1);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002C20C File Offset: 0x0002A40C
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			return span.TrimStart(trimChars).TrimEnd(trimChars);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002C21C File Offset: 0x0002A41C
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			if (trimChars.IsEmpty)
			{
				return span.TrimStart();
			}
			int i = 0;
			IL_0040:
			while (i < span.Length)
			{
				for (int j = 0; j < trimChars.Length; j++)
				{
					if (*span[i] == *trimChars[j])
					{
						i++;
						goto IL_0040;
					}
				}
				break;
			}
			return span.Slice(i);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002C27C File Offset: 0x0002A47C
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			if (trimChars.IsEmpty)
			{
				return span.TrimEnd();
			}
			int i = span.Length - 1;
			IL_0048:
			while (i >= 0)
			{
				for (int j = 0; j < trimChars.Length; j++)
				{
					if (*span[i] == *trimChars[j])
					{
						i--;
						goto IL_0048;
					}
				}
				break;
			}
			return span.Slice(0, i + 1);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		public unsafe static bool IsWhiteSpace(this ReadOnlySpan<char> span)
		{
			for (int i = 0; i < span.Length; i++)
			{
				if (!char.IsWhiteSpace((char)(*span[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002C314 File Offset: 0x0002A514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<T>(this Span<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002C3AC File Offset: 0x0002A5AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002C420 File Offset: 0x0002A620
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOf<T>(this Span<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002C4B8 File Offset: 0x0002A6B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOf<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002C52C File Offset: 0x0002A72C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>(this Span<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>
		{
			int length = span.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length == other.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), (ulong)((long)length * (long)num));
			}
			return length == other.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other), length);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002C5A4 File Offset: 0x0002A7A4
		public static int SequenceCompareTo<T>(this Span<T> span, ReadOnlySpan<T> other) where T : IComparable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			return SpanHelpers.SequenceCompareTo<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(other), other.Length);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002C65C File Offset: 0x0002A85C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002C6F4 File Offset: 0x0002A8F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002C768 File Offset: 0x0002A968
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOf<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002C800 File Offset: 0x0002AA00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOf<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002C874 File Offset: 0x0002AA74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this Span<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002C8D8 File Offset: 0x0002AAD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002C948 File Offset: 0x0002AB48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002C9BC File Offset: 0x0002ABBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002CA20 File Offset: 0x0002AC20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002CA90 File Offset: 0x0002AC90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002CB04 File Offset: 0x0002AD04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this Span<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002CB68 File Offset: 0x0002AD68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002CBD8 File Offset: 0x0002ADD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfAny<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002CC4C File Offset: 0x0002AE4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002CCB0 File Offset: 0x0002AEB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002CD20 File Offset: 0x0002AF20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002CD94 File Offset: 0x0002AF94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>
		{
			int length = span.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length == other.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), (ulong)((long)length * (long)num));
			}
			return length == other.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other), length);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002CE0C File Offset: 0x0002B00C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SequenceCompareTo<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IComparable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			return SpanHelpers.SequenceCompareTo<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(other), other.Length);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002CEC4 File Offset: 0x0002B0C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length <= span.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length * (long)num));
			}
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(value), length);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002CF3C File Offset: 0x0002B13C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length <= span.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length * (long)num));
			}
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(value), length);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002CFB4 File Offset: 0x0002B1B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWith<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = span.Length;
			int length2 = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length2 <= length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length2 * (long)num));
			}
			return length2 <= length && SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2), MemoryMarshal.GetReference<T>(value), length2);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002D038 File Offset: 0x0002B238
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWith<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = span.Length;
			int length2 = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length2 <= length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length2 * (long)num));
			}
			return length2 <= length && SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2), MemoryMarshal.GetReference<T>(value), length2);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002D0BC File Offset: 0x0002B2BC
		public unsafe static void Reverse<T>(this Span<T> span)
		{
			ref T reference = ref MemoryMarshal.GetReference<T>(span);
			int i = 0;
			int num = span.Length - 1;
			while (i < num)
			{
				T t = *Unsafe.Add<T>(ref reference, i);
				*Unsafe.Add<T>(ref reference, i) = *Unsafe.Add<T>(ref reference, num);
				*Unsafe.Add<T>(ref reference, num) = t;
				i++;
				num--;
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002D11C File Offset: 0x0002B31C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002D124 File Offset: 0x0002B324
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, int start, int length)
		{
			return new Span<T>(array, start, length);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002D12E File Offset: 0x0002B32E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002D14A File Offset: 0x0002B34A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, int start)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Span<T>(segment.Array, segment.Offset + start, segment.Count - start);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002D180 File Offset: 0x0002B380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, Index startIndex)
		{
			int offset = startIndex.GetOffset(segment.Count);
			return segment.AsSpan(offset);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002D1A3 File Offset: 0x0002B3A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, int start, int length)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if ((ulong)length > (ulong)((long)(segment.Count - start)))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new Span<T>(segment.Array, segment.Offset + start, length);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, Range range)
		{
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(segment.Count);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new Span<T>(segment.Array, segment.Offset + item, item2);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002D222 File Offset: 0x0002B422
		public static Memory<T> AsMemory<T>(this T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002D22A File Offset: 0x0002B42A
		public static Memory<T> AsMemory<T>(this T[] array, int start)
		{
			return new Memory<T>(array, start);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002D234 File Offset: 0x0002B434
		public static Memory<T> AsMemory<T>(this T[] array, Index startIndex)
		{
			if (array == null)
			{
				if (!startIndex.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Memory<T>);
			}
			int offset = startIndex.GetOffset(array.Length);
			return new Memory<T>(array, offset);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002D274 File Offset: 0x0002B474
		public static Memory<T> AsMemory<T>(this T[] array, int start, int length)
		{
			return new Memory<T>(array, start, length);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002D280 File Offset: 0x0002B480
		public static Memory<T> AsMemory<T>(this T[] array, Range range)
		{
			if (array == null)
			{
				Index start = range.Start;
				Index end = range.End;
				if (!start.Equals(Index.Start) || !end.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Memory<T>);
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(array.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new Memory<T>(array, item, item2);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002D2EC File Offset: 0x0002B4EC
		public static Memory<T> AsMemory<T>(this ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002D308 File Offset: 0x0002B508
		public static Memory<T> AsMemory<T>(this ArraySegment<T> segment, int start)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(segment.Array, segment.Offset + start, segment.Count - start);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002D33B File Offset: 0x0002B53B
		public static Memory<T> AsMemory<T>(this ArraySegment<T> segment, int start, int length)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if ((ulong)length > (ulong)((long)(segment.Count - start)))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new Memory<T>(segment.Array, segment.Offset + start, length);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002D37C File Offset: 0x0002B57C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this T[] source, Span<T> destination)
		{
			new ReadOnlySpan<T>(source).CopyTo(destination);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002D398 File Offset: 0x0002B598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this T[] source, Memory<T> destination)
		{
			source.CopyTo(destination.Span);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002D3A7 File Offset: 0x0002B5A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Overlaps<T>(this Span<T> span, ReadOnlySpan<T> other)
		{
			return span.Overlaps(other);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002D3B5 File Offset: 0x0002B5B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Overlaps<T>(this Span<T> span, ReadOnlySpan<T> other, out int elementOffset)
		{
			return span.Overlaps(other, out elementOffset);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002D3C4 File Offset: 0x0002B5C4
		public static bool Overlaps<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other)
		{
			if (span.IsEmpty || other.IsEmpty)
			{
				return false;
			}
			IntPtr intPtr = Unsafe.ByteOffset<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other));
			if (Unsafe.SizeOf<IntPtr>() == 4)
			{
				return (int)intPtr < span.Length * Unsafe.SizeOf<T>() || (int)intPtr > -(other.Length * Unsafe.SizeOf<T>());
			}
			return (long)intPtr < (long)span.Length * (long)Unsafe.SizeOf<T>() || (long)intPtr > -((long)other.Length * (long)Unsafe.SizeOf<T>());
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002D460 File Offset: 0x0002B660
		public static bool Overlaps<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other, out int elementOffset)
		{
			if (span.IsEmpty || other.IsEmpty)
			{
				elementOffset = 0;
				return false;
			}
			IntPtr intPtr = Unsafe.ByteOffset<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other));
			if (Unsafe.SizeOf<IntPtr>() == 4)
			{
				if ((int)intPtr < span.Length * Unsafe.SizeOf<T>() || (int)intPtr > -(other.Length * Unsafe.SizeOf<T>()))
				{
					if ((int)intPtr % Unsafe.SizeOf<T>() != 0)
					{
						ThrowHelper.ThrowArgumentException_OverlapAlignmentMismatch();
					}
					elementOffset = (int)intPtr / Unsafe.SizeOf<T>();
					return true;
				}
				elementOffset = 0;
				return false;
			}
			else
			{
				if ((long)intPtr < (long)span.Length * (long)Unsafe.SizeOf<T>() || (long)intPtr > -((long)other.Length * (long)Unsafe.SizeOf<T>()))
				{
					if ((long)intPtr % (long)Unsafe.SizeOf<T>() != 0L)
					{
						ThrowHelper.ThrowArgumentException_OverlapAlignmentMismatch();
					}
					elementOffset = (int)((long)intPtr / (long)Unsafe.SizeOf<T>());
					return true;
				}
				elementOffset = 0;
				return false;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002D54A File Offset: 0x0002B74A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T>(this Span<T> span, IComparable<T> comparable)
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002D553 File Offset: 0x0002B753
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this Span<T> span, TComparable comparable) where TComparable : IComparable<T>
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002D561 File Offset: 0x0002B761
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparer>(this Span<T> span, T value, TComparer comparer) where TComparer : IComparer<T>
		{
			return span.BinarySearch(value, comparer);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002D570 File Offset: 0x0002B770
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T>(this ReadOnlySpan<T> span, IComparable<T> comparable)
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002D579 File Offset: 0x0002B779
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this ReadOnlySpan<T> span, TComparable comparable) where TComparable : IComparable<T>
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002D584 File Offset: 0x0002B784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparer>(this ReadOnlySpan<T> span, T value, TComparer comparer) where TComparer : IComparer<T>
		{
			if (comparer == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparer);
			}
			SpanHelpers.ComparerComparable<T, TComparer> comparerComparable = new SpanHelpers.ComparerComparable<T, TComparer>(value, comparer);
			return span.BinarySearch(comparerComparable);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002D5B0 File Offset: 0x0002B7B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsTypeComparableAsBytes<T>(out ulong size)
		{
			if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte))
			{
				size = 1UL;
				return true;
			}
			if (typeof(T) == typeof(char) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
			{
				size = 2UL;
				return true;
			}
			if (typeof(T) == typeof(int) || typeof(T) == typeof(uint))
			{
				size = 4UL;
				return true;
			}
			if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
			{
				size = 8UL;
				return true;
			}
			size = 0UL;
			return false;
		}
	}
}
