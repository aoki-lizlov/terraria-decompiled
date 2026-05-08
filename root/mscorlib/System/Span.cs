using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000144 RID: 324
	[NonVersionable]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	public readonly ref struct Span<T>
	{
		// Token: 0x06000D74 RID: 3444 RVA: 0x00034DA0 File Offset: 0x00032FA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array)
		{
			if (array == null)
			{
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._pointer = new ByReference<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()));
			this._length = array.Length;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00034E04 File Offset: 0x00033004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), start));
			this._length = length;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00034E89 File Offset: 0x00033089
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Span(void* pointer, int length)
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.As<byte, T>(ref *(byte*)pointer));
			this._length = length;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00034EC2 File Offset: 0x000330C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Span(ref T ptr, int length)
		{
			this._pointer = new ByReference<T>(ref ptr);
			this._length = length;
		}

		// Token: 0x170000FD RID: 253
		public ref T this[int index]
		{
			[Intrinsic]
			[NonVersionable]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (index >= this._length)
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				return Unsafe.Add<T>(this._pointer.Value, index);
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00034F08 File Offset: 0x00033108
		public ref T GetPinnableReference()
		{
			if (this._length == 0)
			{
				return Unsafe.AsRef<T>(null);
			}
			return this._pointer.Value;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00034F34 File Offset: 0x00033134
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				SpanHelpers.ClearWithReferences(Unsafe.As<T, IntPtr>(this._pointer.Value), (ulong)((long)this._length * (long)(Unsafe.SizeOf<T>() / IntPtr.Size)));
				return;
			}
			SpanHelpers.ClearWithoutReferences(Unsafe.As<T, byte>(this._pointer.Value), (ulong)((long)this._length * (long)Unsafe.SizeOf<T>()));
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00034F9C File Offset: 0x0003319C
		public unsafe void Fill(T value)
		{
			if (Unsafe.SizeOf<T>() == 1)
			{
				uint length = (uint)this._length;
				if (length == 0U)
				{
					return;
				}
				T t = value;
				Unsafe.InitBlockUnaligned(Unsafe.As<T, byte>(this._pointer.Value), *Unsafe.As<T, byte>(ref t), length);
				return;
			}
			else
			{
				ulong num = (ulong)this._length;
				if (num == 0UL)
				{
					return;
				}
				ref T value2 = ref this._pointer.Value;
				ulong num2 = (ulong)Unsafe.SizeOf<T>();
				ulong num3;
				for (num3 = 0UL; num3 < (num & 18446744073709551608UL); num3 += 8UL)
				{
					*Unsafe.AddByteOffset<T>(ref value2, num3 * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 1UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 2UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 3UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 4UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 5UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 6UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 7UL) * num2) = value;
				}
				if (num3 < (num & 18446744073709551612UL))
				{
					*Unsafe.AddByteOffset<T>(ref value2, num3 * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 1UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 2UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 3UL) * num2) = value;
					num3 += 4UL;
				}
				while (num3 < num)
				{
					*Unsafe.AddByteOffset<T>(ref value2, num3 * num2) = value;
					num3 += 1UL;
				}
				return;
			}
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00035148 File Offset: 0x00033348
		public void CopyTo(Span<T> destination)
		{
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (ulong)((long)this._length));
				return;
			}
			ThrowHelper.ThrowArgumentException_DestinationTooShort();
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00035194 File Offset: 0x00033394
		public bool TryCopyTo(Span<T> destination)
		{
			bool flag = false;
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (ulong)((long)this._length));
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000351E0 File Offset: 0x000333E0
		public static bool operator ==(Span<T> left, Span<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left._pointer.Value, right._pointer.Value);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00035220 File Offset: 0x00033420
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._pointer.Value, span._length);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00035248 File Offset: 0x00033448
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				fixed (char* ptr = Unsafe.As<T, char>(this._pointer.Value))
				{
					return new string(ptr, 0, this._length);
				}
			}
			return string.Format("System.Span<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000352B8 File Offset: 0x000334B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(this._pointer.Value, start), this._length - start);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000352F4 File Offset: 0x000334F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(this._pointer.Value, start), length);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00035334 File Offset: 0x00033534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T[] ToArray()
		{
			if (this._length == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._length];
			Buffer.Memmove<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), this._pointer.Value, (ulong)((long)this._length));
			return array;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0003537F File Offset: 0x0003357F
		public int Length
		{
			[NonVersionable]
			get
			{
				return this._length;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00035387 File Offset: 0x00033587
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00035392 File Offset: 0x00033592
		public static bool operator !=(Span<T> left, Span<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000344C2 File Offset: 0x000326C2
		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException("Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.");
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000344CE File Offset: 0x000326CE
		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		public override int GetHashCode()
		{
			throw new NotSupportedException("GetHashCode() on Span and ReadOnlySpan is not supported.");
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0003539E File Offset: 0x0003359E
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000353A6 File Offset: 0x000335A6
		public static implicit operator Span<T>(ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x000353C4 File Offset: 0x000335C4
		public static Span<T> Empty
		{
			get
			{
				return default(Span<T>);
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000353DA File Offset: 0x000335DA
		public Span<T>.Enumerator GetEnumerator()
		{
			return new Span<T>.Enumerator(this);
		}

		// Token: 0x04001167 RID: 4455
		internal readonly ByReference<T> _pointer;

		// Token: 0x04001168 RID: 4456
		private readonly int _length;

		// Token: 0x02000145 RID: 325
		public ref struct Enumerator
		{
			// Token: 0x06000D8D RID: 3469 RVA: 0x000353E7 File Offset: 0x000335E7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(Span<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x06000D8E RID: 3470 RVA: 0x000353F8 File Offset: 0x000335F8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this._index + 1;
				if (num < this._span.Length)
				{
					this._index = num;
					return true;
				}
				return false;
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00035426 File Offset: 0x00033626
			public ref T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x04001169 RID: 4457
			private readonly Span<T> _span;

			// Token: 0x0400116A RID: 4458
			private int _index;
		}
	}
}
