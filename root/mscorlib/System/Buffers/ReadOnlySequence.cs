using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000B3D RID: 2877
	[DebuggerTypeProxy(typeof(ReadOnlySequenceDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	public readonly struct ReadOnlySequence<T>
	{
		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06006927 RID: 26919 RVA: 0x001646C3 File Offset: 0x001628C3
		public long Length
		{
			get
			{
				return this.GetLength();
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06006928 RID: 26920 RVA: 0x001646CB File Offset: 0x001628CB
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0L;
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06006929 RID: 26921 RVA: 0x001646D7 File Offset: 0x001628D7
		public bool IsSingleSegment
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._startObject == this._endObject;
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x0600692A RID: 26922 RVA: 0x001646E7 File Offset: 0x001628E7
		public ReadOnlyMemory<T> First
		{
			get
			{
				return this.GetFirstBuffer();
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x0600692B RID: 26923 RVA: 0x001646EF File Offset: 0x001628EF
		public ReadOnlySpan<T> FirstSpan
		{
			get
			{
				return this.GetFirstSpan();
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x0600692C RID: 26924 RVA: 0x001646F7 File Offset: 0x001628F7
		public SequencePosition Start
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new SequencePosition(this._startObject, this._startInteger);
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x0600692D RID: 26925 RVA: 0x0016470A File Offset: 0x0016290A
		public SequencePosition End
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new SequencePosition(this._endObject, this._endInteger);
			}
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x0016471D File Offset: 0x0016291D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence(object startSegment, int startIndexAndFlags, object endSegment, int endIndexAndFlags)
		{
			this._startObject = startSegment;
			this._endObject = endSegment;
			this._startInteger = startIndexAndFlags;
			this._endInteger = endIndexAndFlags;
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x0016473C File Offset: 0x0016293C
		public ReadOnlySequence(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment, int endIndex)
		{
			if (startSegment == null || endSegment == null || (startSegment != endSegment && startSegment.RunningIndex > endSegment.RunningIndex) || startSegment.Memory.Length < startIndex || endSegment.Memory.Length < endIndex || (startSegment == endSegment && endIndex < startIndex))
			{
				ThrowHelper.ThrowArgumentValidationException<T>(startSegment, startIndex, endSegment);
			}
			this._startObject = startSegment;
			this._endObject = endSegment;
			this._startInteger = ReadOnlySequence.SegmentToSequenceStart(startIndex);
			this._endInteger = ReadOnlySequence.SegmentToSequenceEnd(endIndex);
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x001647BC File Offset: 0x001629BC
		public ReadOnlySequence(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this._startObject = array;
			this._endObject = array;
			this._startInteger = ReadOnlySequence.ArrayToSequenceStart(0);
			this._endInteger = ReadOnlySequence.ArrayToSequenceEnd(array.Length);
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x001647F0 File Offset: 0x001629F0
		public ReadOnlySequence(T[] array, int start, int length)
		{
			if (array == null || start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentValidationException(array, start);
			}
			this._startObject = array;
			this._endObject = array;
			this._startInteger = ReadOnlySequence.ArrayToSequenceStart(start);
			this._endInteger = ReadOnlySequence.ArrayToSequenceEnd(start + length);
		}

		// Token: 0x06006932 RID: 26930 RVA: 0x00164840 File Offset: 0x00162A40
		public ReadOnlySequence(ReadOnlyMemory<T> memory)
		{
			MemoryManager<T> memoryManager;
			int num;
			int num2;
			if (MemoryMarshal.TryGetMemoryManager<T, MemoryManager<T>>(memory, out memoryManager, out num, out num2))
			{
				this._startObject = memoryManager;
				this._endObject = memoryManager;
				this._startInteger = ReadOnlySequence.MemoryManagerToSequenceStart(num);
				this._endInteger = ReadOnlySequence.MemoryManagerToSequenceEnd(num2);
				return;
			}
			ArraySegment<T> arraySegment;
			if (MemoryMarshal.TryGetArray<T>(memory, out arraySegment))
			{
				T[] array = arraySegment.Array;
				int offset = arraySegment.Offset;
				this._startObject = array;
				this._endObject = array;
				this._startInteger = ReadOnlySequence.ArrayToSequenceStart(offset);
				this._endInteger = ReadOnlySequence.ArrayToSequenceEnd(offset + arraySegment.Count);
				return;
			}
			if (typeof(T) == typeof(char))
			{
				string text;
				int num3;
				if (!MemoryMarshal.TryGetString((ReadOnlyMemory<char>)memory, out text, out num3, out num2))
				{
					ThrowHelper.ThrowInvalidOperationException();
				}
				this._startObject = text;
				this._endObject = text;
				this._startInteger = ReadOnlySequence.StringToSequenceStart(num3);
				this._endInteger = ReadOnlySequence.StringToSequenceEnd(num3 + num2);
				return;
			}
			ThrowHelper.ThrowInvalidOperationException();
			this._startObject = null;
			this._endObject = null;
			this._startInteger = 0;
			this._endInteger = 0;
		}

		// Token: 0x06006933 RID: 26931 RVA: 0x00164958 File Offset: 0x00162B58
		public ReadOnlySequence<T> Slice(long start, long length)
		{
			if (start < 0L || length < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			int num = ReadOnlySequence<T>.GetIndex(this._startInteger);
			int index = ReadOnlySequence<T>.GetIndex(this._endInteger);
			object startObject = this._startObject;
			object endObject = this._endObject;
			SequencePosition sequencePosition;
			SequencePosition sequencePosition2;
			if (startObject != endObject)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)startObject;
				int num2 = readOnlySequenceSegment.Memory.Length - num;
				if ((long)num2 > start)
				{
					num += (int)start;
					sequencePosition = new SequencePosition(startObject, num);
					sequencePosition2 = ReadOnlySequence<T>.GetEndPosition(readOnlySequenceSegment, startObject, num, endObject, index, length);
				}
				else
				{
					if (num2 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, endObject, index, start - (long)num2, ExceptionArgument.start);
					int index2 = ReadOnlySequence<T>.GetIndex(in sequencePosition);
					object @object = sequencePosition.GetObject();
					if (@object != endObject)
					{
						sequencePosition2 = ReadOnlySequence<T>.GetEndPosition((ReadOnlySequenceSegment<T>)@object, @object, index2, endObject, index, length);
					}
					else
					{
						if ((long)(index - index2) < length)
						{
							ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
						}
						sequencePosition2 = new SequencePosition(@object, index2 + (int)length);
					}
				}
			}
			else
			{
				if ((long)(index - num) < start)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(-1L);
				}
				num += (int)start;
				sequencePosition = new SequencePosition(startObject, num);
				if ((long)(index - num) < length)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				sequencePosition2 = new SequencePosition(startObject, num + (int)length);
			}
			return this.SliceImpl(in sequencePosition, in sequencePosition2);
		}

		// Token: 0x06006934 RID: 26932 RVA: 0x00164AA0 File Offset: 0x00162CA0
		public ReadOnlySequence<T> Slice(long start, SequencePosition end)
		{
			if (start < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			uint index = (uint)ReadOnlySequence<T>.GetIndex(this._startInteger);
			object startObject = this._startObject;
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(this._endInteger);
			object endObject = this._endObject;
			uint num = (uint)ReadOnlySequence<T>.GetIndex(in end);
			object obj = end.GetObject();
			if (obj == null)
			{
				obj = this._startObject;
				num = index;
			}
			if (startObject == endObject)
			{
				if (!ReadOnlySequence<T>.InRange(num, index, index2))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if ((ulong)(num - index) < (ulong)start)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(-1L);
				}
			}
			else
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)startObject;
				ulong num2 = (ulong)(readOnlySequenceSegment.RunningIndex + (long)((ulong)index));
				ulong num3 = (ulong)(((ReadOnlySequenceSegment<T>)obj).RunningIndex + (long)((ulong)num));
				if (!ReadOnlySequence<T>.InRange(num3, num2, (ulong)(((ReadOnlySequenceSegment<T>)endObject).RunningIndex + (long)((ulong)index2))))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (num2 + (ulong)start > num3)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				int num4 = readOnlySequenceSegment.Memory.Length - (int)index;
				if ((long)num4 <= start)
				{
					if (num4 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					SequencePosition sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, obj, (int)num, start - (long)num4, ExceptionArgument.start);
					return this.SliceImpl(in sequencePosition, in end);
				}
			}
			SequencePosition sequencePosition2 = new SequencePosition(startObject, (int)(index + (uint)((int)start)));
			SequencePosition sequencePosition3 = new SequencePosition(obj, (int)num);
			return this.SliceImpl(in sequencePosition2, in sequencePosition3);
		}

		// Token: 0x06006935 RID: 26933 RVA: 0x00164BE8 File Offset: 0x00162DE8
		public ReadOnlySequence<T> Slice(SequencePosition start, long length)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(this._startInteger);
			object startObject = this._startObject;
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(this._endInteger);
			object endObject = this._endObject;
			uint num = (uint)ReadOnlySequence<T>.GetIndex(in start);
			object obj = start.GetObject();
			if (obj == null)
			{
				num = index;
				obj = this._startObject;
			}
			if (startObject == endObject)
			{
				if (!ReadOnlySequence<T>.InRange(num, index, index2))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (length < 0L)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				if ((ulong)(index2 - num) < (ulong)length)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
			}
			else
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)obj;
				long num2 = readOnlySequenceSegment.RunningIndex + (long)((ulong)num);
				ulong num3 = (ulong)(((ReadOnlySequenceSegment<T>)startObject).RunningIndex + (long)((ulong)index));
				ulong num4 = (ulong)(((ReadOnlySequenceSegment<T>)endObject).RunningIndex + (long)((ulong)index2));
				if (!ReadOnlySequence<T>.InRange((ulong)num2, num3, num4))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (length < 0L)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				if (num2 + length > (long)num4)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
				}
				int num5 = readOnlySequenceSegment.Memory.Length - (int)num;
				if ((long)num5 < length)
				{
					if (num5 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					SequencePosition sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, endObject, (int)index2, length - (long)num5, ExceptionArgument.length);
					return this.SliceImpl(in start, in sequencePosition);
				}
			}
			SequencePosition sequencePosition2 = new SequencePosition(obj, (int)num);
			SequencePosition sequencePosition3 = new SequencePosition(obj, (int)(num + (uint)((int)length)));
			return this.SliceImpl(in sequencePosition2, in sequencePosition3);
		}

		// Token: 0x06006936 RID: 26934 RVA: 0x00164D3B File Offset: 0x00162F3B
		public ReadOnlySequence<T> Slice(int start, int length)
		{
			return this.Slice((long)start, (long)length);
		}

		// Token: 0x06006937 RID: 26935 RVA: 0x00164D47 File Offset: 0x00162F47
		public ReadOnlySequence<T> Slice(int start, SequencePosition end)
		{
			return this.Slice((long)start, end);
		}

		// Token: 0x06006938 RID: 26936 RVA: 0x00164D52 File Offset: 0x00162F52
		public ReadOnlySequence<T> Slice(SequencePosition start, int length)
		{
			return this.Slice(start, (long)length);
		}

		// Token: 0x06006939 RID: 26937 RVA: 0x00164D5D File Offset: 0x00162F5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySequence<T> Slice(SequencePosition start, SequencePosition end)
		{
			this.BoundsCheck((uint)ReadOnlySequence<T>.GetIndex(in start), start.GetObject(), (uint)ReadOnlySequence<T>.GetIndex(in end), end.GetObject());
			return this.SliceImpl(in start, in end);
		}

		// Token: 0x0600693A RID: 26938 RVA: 0x00164D8C File Offset: 0x00162F8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySequence<T> Slice(SequencePosition start)
		{
			bool flag = start.GetObject() != null;
			this.BoundsCheck(in start, flag);
			SequencePosition sequencePosition = (flag ? start : this.Start);
			return this.SliceImpl(in sequencePosition);
		}

		// Token: 0x0600693B RID: 26939 RVA: 0x00164DC4 File Offset: 0x00162FC4
		public ReadOnlySequence<T> Slice(long start)
		{
			if (start < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			if (start == 0L)
			{
				return this;
			}
			SequencePosition sequencePosition = this.Seek(start, ExceptionArgument.start);
			return this.SliceImpl(in sequencePosition);
		}

		// Token: 0x0600693C RID: 26940 RVA: 0x00164DF8 File Offset: 0x00162FF8
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				ReadOnlySequence<T> readOnlySequence = this;
				ReadOnlySequence<char> readOnlySequence2 = *Unsafe.As<ReadOnlySequence<T>, ReadOnlySequence<char>>(ref readOnlySequence);
				string text;
				int num;
				int num2;
				if (SequenceMarshal.TryGetString(readOnlySequence2, out text, out num, out num2))
				{
					return text.Substring(num, num2);
				}
				if (this.Length < 2147483647L)
				{
					return string.Create<ReadOnlySequence<char>>((int)this.Length, readOnlySequence2, delegate(Span<char> span, ReadOnlySequence<char> sequence)
					{
						(in sequence).CopyTo(span);
					});
				}
			}
			return string.Format("System.Buffers.ReadOnlySequence<{0}>[{1}]", typeof(T).Name, this.Length);
		}

		// Token: 0x0600693D RID: 26941 RVA: 0x00164EAB File Offset: 0x001630AB
		public ReadOnlySequence<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySequence<T>.Enumerator(in this);
		}

		// Token: 0x0600693E RID: 26942 RVA: 0x00164EB3 File Offset: 0x001630B3
		public SequencePosition GetPosition(long offset)
		{
			if (offset < 0L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_OffsetOutOfRange();
			}
			return this.Seek(offset, ExceptionArgument.offset);
		}

		// Token: 0x0600693F RID: 26943 RVA: 0x00164EC8 File Offset: 0x001630C8
		public SequencePosition GetPosition(long offset, SequencePosition origin)
		{
			if (offset < 0L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_OffsetOutOfRange();
			}
			return this.Seek(in origin, offset);
		}

		// Token: 0x06006940 RID: 26944 RVA: 0x00164EE0 File Offset: 0x001630E0
		public bool TryGet(ref SequencePosition position, out ReadOnlyMemory<T> memory, bool advance = true)
		{
			SequencePosition sequencePosition;
			bool flag = this.TryGetBuffer(in position, out memory, out sequencePosition);
			if (advance)
			{
				position = sequencePosition;
			}
			return flag;
		}

		// Token: 0x06006941 RID: 26945 RVA: 0x00164F04 File Offset: 0x00163104
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool TryGetBuffer(in SequencePosition position, out ReadOnlyMemory<T> memory, out SequencePosition next)
		{
			object @object = position.GetObject();
			next = default(SequencePosition);
			if (@object == null)
			{
				memory = default(ReadOnlyMemory<T>);
				return false;
			}
			ReadOnlySequence<T>.SequenceType sequenceType = this.GetSequenceType();
			object endObject = this._endObject;
			int index = ReadOnlySequence<T>.GetIndex(in position);
			int index2 = ReadOnlySequence<T>.GetIndex(this._endInteger);
			if (sequenceType == ReadOnlySequence<T>.SequenceType.MultiSegment)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				if (readOnlySequenceSegment != endObject)
				{
					ReadOnlySequenceSegment<T> next2 = readOnlySequenceSegment.Next;
					if (next2 == null)
					{
						ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
					}
					next = new SequencePosition(next2, 0);
					memory = readOnlySequenceSegment.Memory.Slice(index);
				}
				else
				{
					memory = readOnlySequenceSegment.Memory.Slice(index, index2 - index);
				}
			}
			else
			{
				if (@object != endObject)
				{
					ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
				}
				if (sequenceType == ReadOnlySequence<T>.SequenceType.Array)
				{
					memory = new ReadOnlyMemory<T>((T[])@object, index, index2 - index);
				}
				else if (typeof(T) == typeof(char) && sequenceType == ReadOnlySequence<T>.SequenceType.String)
				{
					memory = (ReadOnlyMemory<T>)((string)@object).AsMemory(index, index2 - index);
				}
				else
				{
					memory = ((MemoryManager<T>)@object).Memory.Slice(index, index2 - index);
				}
			}
			return true;
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x00165044 File Offset: 0x00163244
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlyMemory<T> GetFirstBuffer()
		{
			object startObject = this._startObject;
			if (startObject == null)
			{
				return default(ReadOnlyMemory<T>);
			}
			int startInteger = this._startInteger;
			int endInteger = this._endInteger;
			bool flag = startObject != this._endObject;
			if ((startInteger | endInteger) < 0)
			{
				return this.GetFirstBufferSlow(startObject, flag);
			}
			ReadOnlyMemory<T> memory = ((ReadOnlySequenceSegment<T>)startObject).Memory;
			if (flag)
			{
				return memory.Slice(startInteger);
			}
			return memory.Slice(startInteger, endInteger - startInteger);
		}

		// Token: 0x06006943 RID: 26947 RVA: 0x001650B4 File Offset: 0x001632B4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private ReadOnlyMemory<T> GetFirstBufferSlow(object startObject, bool isMultiSegment)
		{
			if (isMultiSegment)
			{
				ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
			}
			int num = this._startInteger;
			int endInteger = this._endInteger;
			if (num >= 0)
			{
				return new ReadOnlyMemory<T>((T[])startObject, num, (endInteger & int.MaxValue) - num);
			}
			if (typeof(T) == typeof(char) && endInteger < 0)
			{
				return (ReadOnlyMemory<T>)((string)startObject).AsMemory(num & int.MaxValue, endInteger - num);
			}
			num &= int.MaxValue;
			return ((MemoryManager<T>)startObject).Memory.Slice(num, endInteger - num);
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x00165154 File Offset: 0x00163354
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySpan<T> GetFirstSpan()
		{
			object startObject = this._startObject;
			if (startObject == null)
			{
				return default(ReadOnlySpan<T>);
			}
			int startInteger = this._startInteger;
			int endInteger = this._endInteger;
			bool flag = startObject != this._endObject;
			if ((startInteger | endInteger) < 0)
			{
				return this.GetFirstSpanSlow(startObject, flag);
			}
			ReadOnlySpan<T> span = ((ReadOnlySequenceSegment<T>)startObject).Memory.Span;
			if (flag)
			{
				return span.Slice(startInteger);
			}
			return span.Slice(startInteger, endInteger - startInteger);
		}

		// Token: 0x06006945 RID: 26949 RVA: 0x001651D0 File Offset: 0x001633D0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private ReadOnlySpan<T> GetFirstSpanSlow(object startObject, bool isMultiSegment)
		{
			if (isMultiSegment)
			{
				ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
			}
			int num = this._startInteger;
			int endInteger = this._endInteger;
			if (num >= 0)
			{
				return ((T[])startObject).Slice(num, (endInteger & int.MaxValue) - num);
			}
			if (typeof(T) == typeof(char) && endInteger < 0)
			{
				return ((ReadOnlyMemory<T>)((string)startObject).AsMemory()).Span.Slice(num & int.MaxValue, endInteger - num);
			}
			num &= int.MaxValue;
			return ((MemoryManager<T>)startObject).Memory.Span.Slice(num, endInteger - num);
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x00165294 File Offset: 0x00163494
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal SequencePosition Seek(long offset, ExceptionArgument exceptionArgument = ExceptionArgument.offset)
		{
			object startObject = this._startObject;
			object endObject = this._endObject;
			int index = ReadOnlySequence<T>.GetIndex(this._startInteger);
			int index2 = ReadOnlySequence<T>.GetIndex(this._endInteger);
			if (startObject != endObject)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)startObject;
				int num = readOnlySequenceSegment.Memory.Length - index;
				if ((long)num <= offset)
				{
					if (num < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					return ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, endObject, index2, offset - (long)num, exceptionArgument);
				}
			}
			else if ((long)(index2 - index) < offset)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(exceptionArgument);
			}
			return new SequencePosition(startObject, index + (int)offset);
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x00165324 File Offset: 0x00163524
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private SequencePosition Seek(in SequencePosition start, long offset)
		{
			object @object = start.GetObject();
			object endObject = this._endObject;
			int index = ReadOnlySequence<T>.GetIndex(in start);
			int index2 = ReadOnlySequence<T>.GetIndex(this._endInteger);
			if (@object != endObject)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				int num = readOnlySequenceSegment.Memory.Length - index;
				if ((long)num <= offset)
				{
					if (num < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					return ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, endObject, index2, offset - (long)num, ExceptionArgument.offset);
				}
			}
			else if ((long)(index2 - index) < offset)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.offset);
			}
			return new SequencePosition(@object, index + (int)offset);
		}

		// Token: 0x06006948 RID: 26952 RVA: 0x001653B0 File Offset: 0x001635B0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static SequencePosition SeekMultiSegment(ReadOnlySequenceSegment<T> currentSegment, object endObject, int endIndex, long offset, ExceptionArgument argument)
		{
			while (currentSegment != null && currentSegment != endObject)
			{
				int length = currentSegment.Memory.Length;
				if ((long)length > offset)
				{
					IL_003A:
					return new SequencePosition(currentSegment, (int)offset);
				}
				offset -= (long)length;
				currentSegment = currentSegment.Next;
			}
			if (currentSegment == null || (long)endIndex < offset)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(argument);
				goto IL_003A;
			}
			goto IL_003A;
		}

		// Token: 0x06006949 RID: 26953 RVA: 0x00165400 File Offset: 0x00163600
		private void BoundsCheck(in SequencePosition position, bool positionIsNotNull)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(in position);
			object startObject = this._startObject;
			object endObject = this._endObject;
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(this._startInteger);
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(this._endInteger);
			if (startObject == endObject)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					return;
				}
			}
			else
			{
				ulong num = (ulong)(((ReadOnlySequenceSegment<T>)startObject).RunningIndex + (long)((ulong)index2));
				long num2 = 0L;
				if (positionIsNotNull)
				{
					num2 = ((ReadOnlySequenceSegment<T>)position.GetObject()).RunningIndex;
				}
				if (!ReadOnlySequence<T>.InRange((ulong)(num2 + (long)((ulong)index)), num, (ulong)(((ReadOnlySequenceSegment<T>)endObject).RunningIndex + (long)((ulong)index3))))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
			}
		}

		// Token: 0x0600694A RID: 26954 RVA: 0x0016549C File Offset: 0x0016369C
		private void BoundsCheck(uint sliceStartIndex, object sliceStartObject, uint sliceEndIndex, object sliceEndObject)
		{
			object startObject = this._startObject;
			object endObject = this._endObject;
			uint index = (uint)ReadOnlySequence<T>.GetIndex(this._startInteger);
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(this._endInteger);
			if (startObject == endObject)
			{
				if (sliceStartObject != sliceEndObject || sliceStartObject != startObject || sliceStartIndex > sliceEndIndex || sliceStartIndex < index || sliceEndIndex > index2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					return;
				}
			}
			else
			{
				ulong num = (ulong)sliceStartIndex;
				ulong num2 = (ulong)sliceEndIndex;
				if (sliceStartObject != null)
				{
					num += (ulong)((ReadOnlySequenceSegment<T>)sliceStartObject).RunningIndex;
				}
				if (sliceEndObject != null)
				{
					num2 += (ulong)((ReadOnlySequenceSegment<T>)sliceEndObject).RunningIndex;
				}
				if (num > num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (num < (ulong)(((ReadOnlySequenceSegment<T>)startObject).RunningIndex + (long)((ulong)index)) || num2 > (ulong)(((ReadOnlySequenceSegment<T>)endObject).RunningIndex + (long)((ulong)index2)))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
			}
		}

		// Token: 0x0600694B RID: 26955 RVA: 0x00165554 File Offset: 0x00163754
		private static SequencePosition GetEndPosition(ReadOnlySequenceSegment<T> startSegment, object startObject, int startIndex, object endObject, int endIndex, long length)
		{
			int num = startSegment.Memory.Length - startIndex;
			if ((long)num > length)
			{
				return new SequencePosition(startObject, startIndex + (int)length);
			}
			if (num < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
			}
			return ReadOnlySequence<T>.SeekMultiSegment(startSegment.Next, endObject, endIndex, length - (long)num, ExceptionArgument.length);
		}

		// Token: 0x0600694C RID: 26956 RVA: 0x001655A2 File Offset: 0x001637A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence<T>.SequenceType GetSequenceType()
		{
			return (ReadOnlySequence<T>.SequenceType)(-(ReadOnlySequence<T>.SequenceType)(2 * (this._startInteger >> 31) + (this._endInteger >> 31)));
		}

		// Token: 0x0600694D RID: 26957 RVA: 0x001655BA File Offset: 0x001637BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int GetIndex(in SequencePosition position)
		{
			return position.GetInteger() & int.MaxValue;
		}

		// Token: 0x0600694E RID: 26958 RVA: 0x001655C8 File Offset: 0x001637C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int GetIndex(int Integer)
		{
			return Integer & int.MaxValue;
		}

		// Token: 0x0600694F RID: 26959 RVA: 0x001655D1 File Offset: 0x001637D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence<T> SliceImpl(in SequencePosition start, in SequencePosition end)
		{
			return new ReadOnlySequence<T>(start.GetObject(), ReadOnlySequence<T>.GetIndex(in start) | (this._startInteger & int.MinValue), end.GetObject(), ReadOnlySequence<T>.GetIndex(in end) | (this._endInteger & int.MinValue));
		}

		// Token: 0x06006950 RID: 26960 RVA: 0x0016560A File Offset: 0x0016380A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence<T> SliceImpl(in SequencePosition start)
		{
			return new ReadOnlySequence<T>(start.GetObject(), ReadOnlySequence<T>.GetIndex(in start) | (this._startInteger & int.MinValue), this._endObject, this._endInteger);
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x00165638 File Offset: 0x00163838
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private long GetLength()
		{
			object startObject = this._startObject;
			object endObject = this._endObject;
			int index = ReadOnlySequence<T>.GetIndex(this._startInteger);
			int index2 = ReadOnlySequence<T>.GetIndex(this._endInteger);
			if (startObject != endObject)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)startObject;
				return ((ReadOnlySequenceSegment<T>)endObject).RunningIndex + (long)index2 - (readOnlySequenceSegment.RunningIndex + (long)index);
			}
			return (long)(index2 - index);
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x00165698 File Offset: 0x00163898
		internal bool TryGetReadOnlySequenceSegment(out ReadOnlySequenceSegment<T> startSegment, out int startIndex, out ReadOnlySequenceSegment<T> endSegment, out int endIndex)
		{
			object startObject = this._startObject;
			if (startObject == null || this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.MultiSegment)
			{
				startSegment = null;
				startIndex = 0;
				endSegment = null;
				endIndex = 0;
				return false;
			}
			startSegment = (ReadOnlySequenceSegment<T>)startObject;
			startIndex = ReadOnlySequence<T>.GetIndex(this._startInteger);
			endSegment = (ReadOnlySequenceSegment<T>)this._endObject;
			endIndex = ReadOnlySequence<T>.GetIndex(this._endInteger);
			return true;
		}

		// Token: 0x06006953 RID: 26963 RVA: 0x001656F8 File Offset: 0x001638F8
		internal bool TryGetArray(out ArraySegment<T> segment)
		{
			if (this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.Array)
			{
				segment = default(ArraySegment<T>);
				return false;
			}
			int index = ReadOnlySequence<T>.GetIndex(this._startInteger);
			segment = new ArraySegment<T>((T[])this._startObject, index, ReadOnlySequence<T>.GetIndex(this._endInteger) - index);
			return true;
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x00165748 File Offset: 0x00163948
		internal bool TryGetString(out string text, out int start, out int length)
		{
			if (typeof(T) != typeof(char) || this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.String)
			{
				start = 0;
				length = 0;
				text = null;
				return false;
			}
			start = ReadOnlySequence<T>.GetIndex(this._startInteger);
			length = ReadOnlySequence<T>.GetIndex(this._endInteger) - start;
			text = (string)this._startObject;
			return true;
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x0008D20D File Offset: 0x0008B40D
		private static bool InRange(uint value, uint start, uint end)
		{
			return value - start <= end - start;
		}

		// Token: 0x06006956 RID: 26966 RVA: 0x0008D20D File Offset: 0x0008B40D
		private static bool InRange(ulong value, ulong start, ulong end)
		{
			return value - start <= end - start;
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x001657B0 File Offset: 0x001639B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void GetFirstSpan(out ReadOnlySpan<T> first, out SequencePosition next)
		{
			first = default(ReadOnlySpan<T>);
			next = default(SequencePosition);
			object startObject = this._startObject;
			int startInteger = this._startInteger;
			if (startObject != null)
			{
				bool flag = startObject != this._endObject;
				int endInteger = this._endInteger;
				if (startInteger >= 0)
				{
					if (endInteger < 0)
					{
						if (flag)
						{
							ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
						}
						first = new ReadOnlySpan<T>((T[])startObject, startInteger, (endInteger & int.MaxValue) - startInteger);
						return;
					}
					ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)startObject;
					next = new SequencePosition(readOnlySequenceSegment.Next, 0);
					first = readOnlySequenceSegment.Memory.Span;
					if (flag)
					{
						first = first.Slice(startInteger);
						return;
					}
					first = first.Slice(startInteger, endInteger - startInteger);
					return;
				}
				else
				{
					first = ReadOnlySequence<T>.GetFirstSpanSlow(startObject, startInteger, endInteger, flag);
				}
			}
		}

		// Token: 0x06006958 RID: 26968 RVA: 0x00165884 File Offset: 0x00163A84
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static ReadOnlySpan<T> GetFirstSpanSlow(object startObject, int startIndex, int endIndex, bool hasMultipleSegments)
		{
			if (hasMultipleSegments)
			{
				ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
			}
			if (typeof(T) == typeof(char) && endIndex < 0)
			{
				ReadOnlySpan<char> readOnlySpan = ((string)startObject).AsSpan(startIndex & int.MaxValue, endIndex - startIndex);
				return MemoryMarshal.CreateReadOnlySpan<T>(Unsafe.As<char, T>(MemoryMarshal.GetReference<char>(readOnlySpan)), readOnlySpan.Length);
			}
			startIndex &= int.MaxValue;
			return ((MemoryManager<T>)startObject).Memory.Span.Slice(startIndex, endIndex - startIndex);
		}

		// Token: 0x06006959 RID: 26969 RVA: 0x00165913 File Offset: 0x00163B13
		// Note: this type is marked as 'beforefieldinit'.
		static ReadOnlySequence()
		{
		}

		// Token: 0x04003C8E RID: 15502
		private readonly object _startObject;

		// Token: 0x04003C8F RID: 15503
		private readonly object _endObject;

		// Token: 0x04003C90 RID: 15504
		private readonly int _startInteger;

		// Token: 0x04003C91 RID: 15505
		private readonly int _endInteger;

		// Token: 0x04003C92 RID: 15506
		public static readonly ReadOnlySequence<T> Empty = new ReadOnlySequence<T>(Array.Empty<T>());

		// Token: 0x02000B3E RID: 2878
		public struct Enumerator
		{
			// Token: 0x0600695A RID: 26970 RVA: 0x00165924 File Offset: 0x00163B24
			public Enumerator(in ReadOnlySequence<T> sequence)
			{
				this._currentMemory = default(ReadOnlyMemory<T>);
				this._next = sequence.Start;
				this._sequence = sequence;
			}

			// Token: 0x17001259 RID: 4697
			// (get) Token: 0x0600695B RID: 26971 RVA: 0x0016594A File Offset: 0x00163B4A
			public ReadOnlyMemory<T> Current
			{
				get
				{
					return this._currentMemory;
				}
			}

			// Token: 0x0600695C RID: 26972 RVA: 0x00165952 File Offset: 0x00163B52
			public bool MoveNext()
			{
				return this._next.GetObject() != null && this._sequence.TryGet(ref this._next, out this._currentMemory, true);
			}

			// Token: 0x04003C93 RID: 15507
			private readonly ReadOnlySequence<T> _sequence;

			// Token: 0x04003C94 RID: 15508
			private SequencePosition _next;

			// Token: 0x04003C95 RID: 15509
			private ReadOnlyMemory<T> _currentMemory;
		}

		// Token: 0x02000B3F RID: 2879
		private enum SequenceType
		{
			// Token: 0x04003C97 RID: 15511
			MultiSegment,
			// Token: 0x04003C98 RID: 15512
			Array,
			// Token: 0x04003C99 RID: 15513
			MemoryManager,
			// Token: 0x04003C9A RID: 15514
			String,
			// Token: 0x04003C9B RID: 15515
			Empty
		}

		// Token: 0x02000B40 RID: 2880
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600695D RID: 26973 RVA: 0x0016597B File Offset: 0x00163B7B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600695E RID: 26974 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x0600695F RID: 26975 RVA: 0x00165987 File Offset: 0x00163B87
			internal void <ToString>b__33_0(Span<char> span, ReadOnlySequence<char> sequence)
			{
				(in sequence).CopyTo(span);
			}

			// Token: 0x04003C9C RID: 15516
			public static readonly ReadOnlySequence<T>.<>c <>9 = new ReadOnlySequence<T>.<>c();

			// Token: 0x04003C9D RID: 15517
			public static SpanAction<char, ReadOnlySequence<char>> <>9__33_0;
		}
	}
}
