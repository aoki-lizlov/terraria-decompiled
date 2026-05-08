using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000699 RID: 1689
	public static class MemoryMarshal
	{
		// Token: 0x06003F76 RID: 16246 RVA: 0x000DF8D4 File Offset: 0x000DDAD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<byte> AsBytes<T>(Span<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new Span<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x000DF909 File Offset: 0x000DDB09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<byte> AsBytes<T>(ReadOnlySpan<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new ReadOnlySpan<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x000DF93E File Offset: 0x000DDB3E
		public unsafe static Memory<T> AsMemory<T>(ReadOnlyMemory<T> memory)
		{
			return *Unsafe.As<ReadOnlyMemory<T>, Memory<T>>(ref memory);
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x000DF94C File Offset: 0x000DDB4C
		public static ref T GetReference<T>(Span<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x000DF968 File Offset: 0x000DDB68
		public static ref T GetReference<T>(ReadOnlySpan<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x000DF984 File Offset: 0x000DDB84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(Span<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x000DF9B0 File Offset: 0x000DDBB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(ReadOnlySpan<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x000DF9DC File Offset: 0x000DDBDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<TTo> Cast<TFrom, TTo>(Span<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			uint num = (uint)Unsafe.SizeOf<TFrom>();
			uint num2 = (uint)Unsafe.SizeOf<TTo>();
			uint length = (uint)span.Length;
			checked
			{
				int num3;
				if (num == num2)
				{
					num3 = (int)length;
				}
				else if (num == 1U)
				{
					num3 = (int)(length / num2);
				}
				else
				{
					num3 = (int)(unchecked((ulong)length * (ulong)num / (ulong)num2));
				}
				return new Span<TTo>(Unsafe.As<TFrom, TTo>(span._pointer.Value), num3);
			}
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000DFA60 File Offset: 0x000DDC60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<TTo> Cast<TFrom, TTo>(ReadOnlySpan<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			uint num = (uint)Unsafe.SizeOf<TFrom>();
			uint num2 = (uint)Unsafe.SizeOf<TTo>();
			uint length = (uint)span.Length;
			checked
			{
				int num3;
				if (num == num2)
				{
					num3 = (int)length;
				}
				else if (num == 1U)
				{
					num3 = (int)(length / num2);
				}
				else
				{
					num3 = (int)(unchecked((ulong)length * (ulong)num / (ulong)num2));
				}
				return new ReadOnlySpan<TTo>(Unsafe.As<TFrom, TTo>(MemoryMarshal.GetReference<TFrom>(span)), num3);
			}
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x000DFADA File Offset: 0x000DDCDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> CreateSpan<T>(ref T reference, int length)
		{
			return new Span<T>(ref reference, length);
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x000DFAE3 File Offset: 0x000DDCE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<T> CreateReadOnlySpan<T>(ref T reference, int length)
		{
			return new ReadOnlySpan<T>(ref reference, length);
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x000DFAEC File Offset: 0x000DDCEC
		public static bool TryGetArray<T>(ReadOnlyMemory<T> memory, out ArraySegment<T> segment)
		{
			int num;
			int num2;
			object objectStartLength = memory.GetObjectStartLength(out num, out num2);
			if (num < 0)
			{
				ArraySegment<T> arraySegment;
				if (((MemoryManager<T>)objectStartLength).TryGetArray(out arraySegment))
				{
					segment = new ArraySegment<T>(arraySegment.Array, arraySegment.Offset + (num & int.MaxValue), num2);
					return true;
				}
			}
			else
			{
				T[] array = objectStartLength as T[];
				if (array != null)
				{
					segment = new ArraySegment<T>(array, num, num2 & int.MaxValue);
					return true;
				}
			}
			if ((num2 & 2147483647) == 0)
			{
				segment = ArraySegment<T>.Empty;
				return true;
			}
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x000DFB80 File Offset: 0x000DDD80
		public static bool TryGetMemoryManager<T, TManager>(ReadOnlyMemory<T> memory, out TManager manager) where TManager : MemoryManager<T>
		{
			int num;
			int num2;
			manager = memory.GetObjectStartLength(out num, out num2) as TManager;
			return manager != null;
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000DFBB8 File Offset: 0x000DDDB8
		public static bool TryGetMemoryManager<T, TManager>(ReadOnlyMemory<T> memory, out TManager manager, out int start, out int length) where TManager : MemoryManager<T>
		{
			manager = memory.GetObjectStartLength(out start, out length) as TManager;
			start &= int.MaxValue;
			if (manager == null)
			{
				start = 0;
				length = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x000DFC00 File Offset: 0x000DDE00
		public unsafe static IEnumerable<T> ToEnumerable<T>(ReadOnlyMemory<T> memory)
		{
			int num;
			for (int i = 0; i < memory.Length; i = num + 1)
			{
				yield return *memory.Span[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x000DFC10 File Offset: 0x000DDE10
		public static bool TryGetString(ReadOnlyMemory<char> memory, out string text, out int start, out int length)
		{
			int num;
			int num2;
			string text2 = memory.GetObjectStartLength(out num, out num2) as string;
			if (text2 != null)
			{
				text = text2;
				start = num;
				length = num2;
				return true;
			}
			text = null;
			start = 0;
			length = 0;
			return false;
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x000DFC46 File Offset: 0x000DDE46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Read<T>(ReadOnlySpan<byte> source) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > source.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x000DFC80 File Offset: 0x000DDE80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryRead<T>(ReadOnlySpan<byte> source, out T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)source.Length))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
			return true;
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x000DFCCE File Offset: 0x000DDECE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Write<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > destination.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x000DFD0C File Offset: 0x000DDF0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWrite<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)destination.Length))
			{
				return false;
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x000DFD48 File Offset: 0x000DDF48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Memory<T> CreateFromPinnedArray<T>(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return default(Memory<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(array, start, length | int.MinValue);
		}

		// Token: 0x0200069A RID: 1690
		[CompilerGenerated]
		private sealed class <ToEnumerable>d__14<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06003F8B RID: 16267 RVA: 0x000DFDBA File Offset: 0x000DDFBA
			[DebuggerHidden]
			public <ToEnumerable>d__14(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06003F8C RID: 16268 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06003F8D RID: 16269 RVA: 0x000DFDD4 File Offset: 0x000DDFD4
			unsafe bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= memory.Length)
				{
					return false;
				}
				this.<>2__current = *memory.Span[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170009B5 RID: 2485
			// (get) Token: 0x06003F8E RID: 16270 RVA: 0x000DFE59 File Offset: 0x000DE059
			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003F8F RID: 16271 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170009B6 RID: 2486
			// (get) Token: 0x06003F90 RID: 16272 RVA: 0x000DFE61 File Offset: 0x000DE061
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003F91 RID: 16273 RVA: 0x000DFE70 File Offset: 0x000DE070
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				MemoryMarshal.<ToEnumerable>d__14<T> <ToEnumerable>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<ToEnumerable>d__ = this;
				}
				else
				{
					<ToEnumerable>d__ = new MemoryMarshal.<ToEnumerable>d__14<T>(0);
				}
				<ToEnumerable>d__.memory = memory;
				return <ToEnumerable>d__;
			}

			// Token: 0x06003F92 RID: 16274 RVA: 0x000DFEB3 File Offset: 0x000DE0B3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x0400297D RID: 10621
			private int <>1__state;

			// Token: 0x0400297E RID: 10622
			private T <>2__current;

			// Token: 0x0400297F RID: 10623
			private int <>l__initialThreadId;

			// Token: 0x04002980 RID: 10624
			private ReadOnlyMemory<T> memory;

			// Token: 0x04002981 RID: 10625
			public ReadOnlyMemory<T> <>3__memory;

			// Token: 0x04002982 RID: 10626
			private int <i>5__2;
		}
	}
}
