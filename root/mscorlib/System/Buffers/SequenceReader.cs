using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000B47 RID: 2887
	public ref struct SequenceReader<[IsUnmanaged] T> where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06006992 RID: 27026 RVA: 0x00165ECC File Offset: 0x001640CC
		public bool TryReadTo(out ReadOnlySpan<T> span, T delimiter, bool advancePastDelimiter = true)
		{
			ReadOnlySpan<T> unreadSpan = this.UnreadSpan;
			int num = unreadSpan.IndexOf(delimiter);
			if (num != -1)
			{
				span = ((num == 0) ? default(ReadOnlySpan<T>) : unreadSpan.Slice(0, num));
				this.AdvanceCurrentSpan((long)(num + (advancePastDelimiter ? 1 : 0)));
				return true;
			}
			return this.TryReadToSlow(out span, delimiter, advancePastDelimiter);
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x00165F24 File Offset: 0x00164124
		private bool TryReadToSlow(out ReadOnlySpan<T> span, T delimiter, bool advancePastDelimiter)
		{
			ReadOnlySequence<T> readOnlySequence;
			if (!this.TryReadToInternal(out readOnlySequence, delimiter, advancePastDelimiter, this.CurrentSpan.Length - this.CurrentSpanIndex))
			{
				span = default(ReadOnlySpan<T>);
				return false;
			}
			span = (readOnlySequence.IsSingleSegment ? readOnlySequence.First.Span : (in readOnlySequence).ToArray<T>());
			return true;
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x00165F88 File Offset: 0x00164188
		public unsafe bool TryReadTo(out ReadOnlySpan<T> span, T delimiter, T delimiterEscape, bool advancePastDelimiter = true)
		{
			ReadOnlySpan<T> unreadSpan = this.UnreadSpan;
			int num = unreadSpan.IndexOf(delimiter);
			if (num > 0)
			{
				T t = *unreadSpan[num - 1];
				if (!t.Equals(delimiterEscape))
				{
					goto IL_0036;
				}
			}
			if (num != 0)
			{
				return this.TryReadToSlow(out span, delimiter, delimiterEscape, num, advancePastDelimiter);
			}
			IL_0036:
			span = unreadSpan.Slice(0, num);
			this.AdvanceCurrentSpan((long)(num + (advancePastDelimiter ? 1 : 0)));
			return true;
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x00165FFC File Offset: 0x001641FC
		private bool TryReadToSlow(out ReadOnlySpan<T> span, T delimiter, T delimiterEscape, int index, bool advancePastDelimiter)
		{
			ReadOnlySequence<T> readOnlySequence;
			if (!this.TryReadToSlow(out readOnlySequence, delimiter, delimiterEscape, index, advancePastDelimiter))
			{
				span = default(ReadOnlySpan<T>);
				return false;
			}
			span = (readOnlySequence.IsSingleSegment ? readOnlySequence.First.Span : (in readOnlySequence).ToArray<T>());
			return true;
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x00166050 File Offset: 0x00164250
		private unsafe bool TryReadToSlow(out ReadOnlySequence<T> sequence, T delimiter, T delimiterEscape, int index, bool advancePastDelimiter)
		{
			SequenceReader<T> sequenceReader = this;
			ReadOnlySpan<T> readOnlySpan = this.UnreadSpan;
			bool flag = false;
			for (;;)
			{
				if (index < 0)
				{
					if (readOnlySpan.Length <= 0)
					{
						goto IL_01A6;
					}
					T t = *readOnlySpan[readOnlySpan.Length - 1];
					if (!t.Equals(delimiterEscape))
					{
						goto IL_01A6;
					}
					int num = 1;
					int i;
					for (i = readOnlySpan.Length - 2; i >= 0; i--)
					{
						t = *readOnlySpan[i];
						if (!t.Equals(delimiterEscape))
						{
							break;
						}
					}
					num += readOnlySpan.Length - 2 - i;
					if (i < 0 && flag)
					{
						flag = (num & 1) == 0;
					}
					else
					{
						flag = (num & 1) != 0;
					}
					IL_01A8:
					this.AdvanceCurrentSpan((long)readOnlySpan.Length);
					readOnlySpan = this.CurrentSpan;
					goto IL_01BD;
					IL_01A6:
					flag = false;
					goto IL_01A8;
				}
				if (index == 0 && flag)
				{
					flag = false;
					this.Advance((long)(index + 1));
					readOnlySpan = this.UnreadSpan;
				}
				else
				{
					if (index <= 0)
					{
						break;
					}
					T t = *readOnlySpan[index - 1];
					if (!t.Equals(delimiterEscape))
					{
						break;
					}
					int num2 = 1;
					int j;
					for (j = index - 2; j >= 0; j--)
					{
						t = *readOnlySpan[j];
						if (!t.Equals(delimiterEscape))
						{
							break;
						}
					}
					if (j < 0 && flag)
					{
						num2++;
					}
					num2 += index - 2 - j;
					if ((num2 & 1) == 0)
					{
						break;
					}
					this.Advance((long)(index + 1));
					flag = false;
					readOnlySpan = this.UnreadSpan;
				}
				IL_01BD:
				index = readOnlySpan.IndexOf(delimiter);
				if (this.End)
				{
					goto Block_13;
				}
			}
			this.AdvanceCurrentSpan((long)index);
			sequence = this.Sequence.Slice(sequenceReader.Position, this.Position);
			if (advancePastDelimiter)
			{
				this.Advance(1L);
			}
			return true;
			Block_13:
			this = sequenceReader;
			sequence = default(ReadOnlySequence<T>);
			return false;
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x0016623D File Offset: 0x0016443D
		public bool TryReadTo(out ReadOnlySequence<T> sequence, T delimiter, bool advancePastDelimiter = true)
		{
			return this.TryReadToInternal(out sequence, delimiter, advancePastDelimiter, 0);
		}

		// Token: 0x06006998 RID: 27032 RVA: 0x0016624C File Offset: 0x0016444C
		private bool TryReadToInternal(out ReadOnlySequence<T> sequence, T delimiter, bool advancePastDelimiter, int skip = 0)
		{
			SequenceReader<T> sequenceReader = this;
			if (skip > 0)
			{
				this.Advance((long)skip);
			}
			ReadOnlySpan<T> readOnlySpan = this.UnreadSpan;
			while (this._moreData)
			{
				int num = readOnlySpan.IndexOf(delimiter);
				if (num != -1)
				{
					if (num > 0)
					{
						this.AdvanceCurrentSpan((long)num);
					}
					sequence = this.Sequence.Slice(sequenceReader.Position, this.Position);
					if (advancePastDelimiter)
					{
						this.Advance(1L);
					}
					return true;
				}
				this.AdvanceCurrentSpan((long)readOnlySpan.Length);
				readOnlySpan = this.CurrentSpan;
			}
			this = sequenceReader;
			sequence = default(ReadOnlySequence<T>);
			return false;
		}

		// Token: 0x06006999 RID: 27033 RVA: 0x001662EC File Offset: 0x001644EC
		public unsafe bool TryReadTo(out ReadOnlySequence<T> sequence, T delimiter, T delimiterEscape, bool advancePastDelimiter = true)
		{
			SequenceReader<T> sequenceReader = this;
			ReadOnlySpan<T> readOnlySpan = this.UnreadSpan;
			bool flag = false;
			while (this._moreData)
			{
				int num = readOnlySpan.IndexOf(delimiter);
				if (num != -1)
				{
					if (num != 0 || !flag)
					{
						if (num > 0)
						{
							T t = *readOnlySpan[num - 1];
							if (t.Equals(delimiterEscape))
							{
								int num2 = 0;
								int i = num;
								while (i > 0)
								{
									t = *readOnlySpan[i - 1];
									if (!t.Equals(delimiterEscape))
									{
										break;
									}
									i--;
									num2++;
								}
								if (num2 == num && flag)
								{
									num2++;
								}
								flag = false;
								if ((num2 & 1) != 0)
								{
									this.Advance((long)(num + 1));
									readOnlySpan = this.UnreadSpan;
									continue;
								}
							}
						}
						if (num > 0)
						{
							this.Advance((long)num);
						}
						sequence = this.Sequence.Slice(sequenceReader.Position, this.Position);
						if (advancePastDelimiter)
						{
							this.Advance(1L);
						}
						return true;
					}
					flag = false;
					this.Advance((long)(num + 1));
					readOnlySpan = this.UnreadSpan;
				}
				else
				{
					int num3 = 0;
					int j = readOnlySpan.Length;
					while (j > 0)
					{
						T t = *readOnlySpan[j - 1];
						if (!t.Equals(delimiterEscape))
						{
							break;
						}
						j--;
						num3++;
					}
					if (flag && num3 == readOnlySpan.Length)
					{
						num3++;
					}
					flag = num3 % 2 != 0;
					this.Advance((long)readOnlySpan.Length);
					readOnlySpan = this.CurrentSpan;
				}
			}
			this = sequenceReader;
			sequence = default(ReadOnlySequence<T>);
			return false;
		}

		// Token: 0x0600699A RID: 27034 RVA: 0x00166498 File Offset: 0x00164698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe bool TryReadToAny(out ReadOnlySpan<T> span, ReadOnlySpan<T> delimiters, bool advancePastDelimiter = true)
		{
			ReadOnlySpan<T> unreadSpan = this.UnreadSpan;
			int num = ((delimiters.Length == 2) ? unreadSpan.IndexOfAny(*delimiters[0], *delimiters[1]) : unreadSpan.IndexOfAny(delimiters));
			if (num != -1)
			{
				span = unreadSpan.Slice(0, num);
				this.Advance((long)(num + (advancePastDelimiter ? 1 : 0)));
				return true;
			}
			return this.TryReadToAnySlow(out span, delimiters, advancePastDelimiter);
		}

		// Token: 0x0600699B RID: 27035 RVA: 0x00166510 File Offset: 0x00164710
		private bool TryReadToAnySlow(out ReadOnlySpan<T> span, ReadOnlySpan<T> delimiters, bool advancePastDelimiter)
		{
			ReadOnlySequence<T> readOnlySequence;
			if (!this.TryReadToAnyInternal(out readOnlySequence, delimiters, advancePastDelimiter, this.CurrentSpan.Length - this.CurrentSpanIndex))
			{
				span = default(ReadOnlySpan<T>);
				return false;
			}
			span = (readOnlySequence.IsSingleSegment ? readOnlySequence.First.Span : (in readOnlySequence).ToArray<T>());
			return true;
		}

		// Token: 0x0600699C RID: 27036 RVA: 0x00166574 File Offset: 0x00164774
		public bool TryReadToAny(out ReadOnlySequence<T> sequence, ReadOnlySpan<T> delimiters, bool advancePastDelimiter = true)
		{
			return this.TryReadToAnyInternal(out sequence, delimiters, advancePastDelimiter, 0);
		}

		// Token: 0x0600699D RID: 27037 RVA: 0x00166580 File Offset: 0x00164780
		private unsafe bool TryReadToAnyInternal(out ReadOnlySequence<T> sequence, ReadOnlySpan<T> delimiters, bool advancePastDelimiter, int skip = 0)
		{
			SequenceReader<T> sequenceReader = this;
			if (skip > 0)
			{
				this.Advance((long)skip);
			}
			ReadOnlySpan<T> readOnlySpan = this.UnreadSpan;
			while (!this.End)
			{
				int num = ((delimiters.Length == 2) ? readOnlySpan.IndexOfAny(*delimiters[0], *delimiters[1]) : readOnlySpan.IndexOfAny(delimiters));
				if (num != -1)
				{
					if (num > 0)
					{
						this.AdvanceCurrentSpan((long)num);
					}
					sequence = this.Sequence.Slice(sequenceReader.Position, this.Position);
					if (advancePastDelimiter)
					{
						this.Advance(1L);
					}
					return true;
				}
				this.Advance((long)readOnlySpan.Length);
				readOnlySpan = this.CurrentSpan;
			}
			this = sequenceReader;
			sequence = default(ReadOnlySequence<T>);
			return false;
		}

		// Token: 0x0600699E RID: 27038 RVA: 0x00166650 File Offset: 0x00164850
		public unsafe bool TryReadTo(out ReadOnlySequence<T> sequence, ReadOnlySpan<T> delimiter, bool advancePastDelimiter = true)
		{
			if (delimiter.Length == 0)
			{
				sequence = default(ReadOnlySequence<T>);
				return true;
			}
			SequenceReader<T> sequenceReader = this;
			bool flag = false;
			while (!this.End)
			{
				if (!this.TryReadTo(out sequence, *delimiter[0], false))
				{
					this = sequenceReader;
					return false;
				}
				if (delimiter.Length == 1)
				{
					if (advancePastDelimiter)
					{
						this.Advance(1L);
					}
					return true;
				}
				if (this.IsNext(delimiter, false))
				{
					if (flag)
					{
						sequence = sequenceReader.Sequence.Slice(sequenceReader.Consumed, this.Consumed - sequenceReader.Consumed);
					}
					if (advancePastDelimiter)
					{
						this.Advance((long)delimiter.Length);
					}
					return true;
				}
				this.Advance(1L);
				flag = true;
			}
			this = sequenceReader;
			sequence = default(ReadOnlySequence<T>);
			return false;
		}

		// Token: 0x0600699F RID: 27039 RVA: 0x00166724 File Offset: 0x00164924
		public bool TryAdvanceTo(T delimiter, bool advancePastDelimiter = true)
		{
			int num = this.UnreadSpan.IndexOf(delimiter);
			if (num != -1)
			{
				this.Advance((long)(advancePastDelimiter ? (num + 1) : num));
				return true;
			}
			ReadOnlySequence<T> readOnlySequence;
			return this.TryReadToInternal(out readOnlySequence, delimiter, advancePastDelimiter, 0);
		}

		// Token: 0x060069A0 RID: 27040 RVA: 0x00166760 File Offset: 0x00164960
		public bool TryAdvanceToAny(ReadOnlySpan<T> delimiters, bool advancePastDelimiter = true)
		{
			int num = this.UnreadSpan.IndexOfAny(delimiters);
			if (num != -1)
			{
				this.AdvanceCurrentSpan((long)(num + (advancePastDelimiter ? 1 : 0)));
				return true;
			}
			ReadOnlySequence<T> readOnlySequence;
			return this.TryReadToAnyInternal(out readOnlySequence, delimiters, advancePastDelimiter, 0);
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x0016679C File Offset: 0x0016499C
		public unsafe long AdvancePast(T value)
		{
			long consumed = this.Consumed;
			do
			{
				int i;
				for (i = this.CurrentSpanIndex; i < this.CurrentSpan.Length; i++)
				{
					T t = *this.CurrentSpan[i];
					if (!t.Equals(value))
					{
						break;
					}
				}
				int num = i - this.CurrentSpanIndex;
				if (num == 0)
				{
					break;
				}
				this.AdvanceCurrentSpan((long)num);
			}
			while (this.CurrentSpanIndex == 0 && !this.End);
			return this.Consumed - consumed;
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x00166820 File Offset: 0x00164A20
		public unsafe long AdvancePastAny(ReadOnlySpan<T> values)
		{
			long consumed = this.Consumed;
			do
			{
				int num = this.CurrentSpanIndex;
				while (num < this.CurrentSpan.Length && values.IndexOf(*this.CurrentSpan[num]) != -1)
				{
					num++;
				}
				int num2 = num - this.CurrentSpanIndex;
				if (num2 == 0)
				{
					break;
				}
				this.AdvanceCurrentSpan((long)num2);
			}
			while (this.CurrentSpanIndex == 0 && !this.End);
			return this.Consumed - consumed;
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x0016689C File Offset: 0x00164A9C
		public unsafe long AdvancePastAny(T value0, T value1, T value2, T value3)
		{
			long consumed = this.Consumed;
			do
			{
				int i;
				for (i = this.CurrentSpanIndex; i < this.CurrentSpan.Length; i++)
				{
					T t = *this.CurrentSpan[i];
					if (!t.Equals(value0) && !t.Equals(value1) && !t.Equals(value2) && !t.Equals(value3))
					{
						break;
					}
				}
				int num = i - this.CurrentSpanIndex;
				if (num == 0)
				{
					break;
				}
				this.AdvanceCurrentSpan((long)num);
			}
			while (this.CurrentSpanIndex == 0 && !this.End);
			return this.Consumed - consumed;
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x00166958 File Offset: 0x00164B58
		public unsafe long AdvancePastAny(T value0, T value1, T value2)
		{
			long consumed = this.Consumed;
			do
			{
				int i;
				for (i = this.CurrentSpanIndex; i < this.CurrentSpan.Length; i++)
				{
					T t = *this.CurrentSpan[i];
					if (!t.Equals(value0) && !t.Equals(value1) && !t.Equals(value2))
					{
						break;
					}
				}
				int num = i - this.CurrentSpanIndex;
				if (num == 0)
				{
					break;
				}
				this.AdvanceCurrentSpan((long)num);
			}
			while (this.CurrentSpanIndex == 0 && !this.End);
			return this.Consumed - consumed;
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x00166A00 File Offset: 0x00164C00
		public unsafe long AdvancePastAny(T value0, T value1)
		{
			long consumed = this.Consumed;
			do
			{
				int i;
				for (i = this.CurrentSpanIndex; i < this.CurrentSpan.Length; i++)
				{
					T t = *this.CurrentSpan[i];
					if (!t.Equals(value0) && !t.Equals(value1))
					{
						break;
					}
				}
				int num = i - this.CurrentSpanIndex;
				if (num == 0)
				{
					break;
				}
				this.AdvanceCurrentSpan((long)num);
			}
			while (this.CurrentSpanIndex == 0 && !this.End);
			return this.Consumed - consumed;
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x00166A98 File Offset: 0x00164C98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe bool IsNext(T next, bool advancePast = false)
		{
			if (this.End)
			{
				return false;
			}
			T t = *this.CurrentSpan[this.CurrentSpanIndex];
			if (t.Equals(next))
			{
				if (advancePast)
				{
					this.AdvanceCurrentSpan(1L);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060069A7 RID: 27047 RVA: 0x00166AE8 File Offset: 0x00164CE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsNext(ReadOnlySpan<T> next, bool advancePast = false)
		{
			ReadOnlySpan<T> unreadSpan = this.UnreadSpan;
			if (unreadSpan.StartsWith(next))
			{
				if (advancePast)
				{
					this.AdvanceCurrentSpan((long)next.Length);
				}
				return true;
			}
			return unreadSpan.Length < next.Length && this.IsNextSlow(next, advancePast);
		}

		// Token: 0x060069A8 RID: 27048 RVA: 0x00166B34 File Offset: 0x00164D34
		private bool IsNextSlow(ReadOnlySpan<T> next, bool advancePast)
		{
			ReadOnlySpan<T> readOnlySpan = this.UnreadSpan;
			int length = next.Length;
			SequencePosition nextPosition = this._nextPosition;
			IL_008F:
			while (next.StartsWith(readOnlySpan))
			{
				if (next.Length == readOnlySpan.Length)
				{
					if (advancePast)
					{
						this.Advance((long)length);
					}
					return true;
				}
				ReadOnlyMemory<T> readOnlyMemory;
				while (this.Sequence.TryGet(ref nextPosition, out readOnlyMemory, true))
				{
					if (readOnlyMemory.Length > 0)
					{
						next = next.Slice(readOnlySpan.Length);
						readOnlySpan = readOnlyMemory.Span;
						if (readOnlySpan.Length > next.Length)
						{
							readOnlySpan = readOnlySpan.Slice(0, next.Length);
							goto IL_008F;
						}
						goto IL_008F;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x00166BDC File Offset: 0x00164DDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public SequenceReader(ReadOnlySequence<T> sequence)
		{
			this.CurrentSpanIndex = 0;
			this.Consumed = 0L;
			this.Sequence = sequence;
			this._currentPosition = sequence.Start;
			this._length = -1L;
			ReadOnlySpan<T> readOnlySpan;
			sequence.GetFirstSpan(out readOnlySpan, out this._nextPosition);
			this.CurrentSpan = readOnlySpan;
			this._moreData = readOnlySpan.Length > 0;
			if (!this._moreData && !sequence.IsSingleSegment)
			{
				this._moreData = true;
				this.GetNextSpan();
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x060069AA RID: 27050 RVA: 0x00166C58 File Offset: 0x00164E58
		public readonly bool End
		{
			get
			{
				return !this._moreData;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x060069AB RID: 27051 RVA: 0x00166C63 File Offset: 0x00164E63
		public readonly ReadOnlySequence<T> Sequence
		{
			[CompilerGenerated]
			get
			{
				return this.<Sequence>k__BackingField;
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x060069AC RID: 27052 RVA: 0x00166C6C File Offset: 0x00164E6C
		public readonly SequencePosition Position
		{
			get
			{
				return this.Sequence.GetPosition((long)this.CurrentSpanIndex, this._currentPosition);
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x060069AD RID: 27053 RVA: 0x00166C94 File Offset: 0x00164E94
		// (set) Token: 0x060069AE RID: 27054 RVA: 0x00166C9C File Offset: 0x00164E9C
		public ReadOnlySpan<T> CurrentSpan
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<CurrentSpan>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CurrentSpan>k__BackingField = value;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x060069AF RID: 27055 RVA: 0x00166CA5 File Offset: 0x00164EA5
		// (set) Token: 0x060069B0 RID: 27056 RVA: 0x00166CAD File Offset: 0x00164EAD
		public int CurrentSpanIndex
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<CurrentSpanIndex>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CurrentSpanIndex>k__BackingField = value;
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x060069B1 RID: 27057 RVA: 0x00166CB8 File Offset: 0x00164EB8
		public readonly ReadOnlySpan<T> UnreadSpan
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.CurrentSpan.Slice(this.CurrentSpanIndex);
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x060069B2 RID: 27058 RVA: 0x00166CD9 File Offset: 0x00164ED9
		// (set) Token: 0x060069B3 RID: 27059 RVA: 0x00166CE1 File Offset: 0x00164EE1
		public long Consumed
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Consumed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Consumed>k__BackingField = value;
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x060069B4 RID: 27060 RVA: 0x00166CEA File Offset: 0x00164EEA
		public readonly long Remaining
		{
			get
			{
				return this.Length - this.Consumed;
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x060069B5 RID: 27061 RVA: 0x00166CFC File Offset: 0x00164EFC
		public unsafe readonly long Length
		{
			get
			{
				if (this._length < 0L)
				{
					fixed (long* ptr = &this._length)
					{
						Volatile.Write(Unsafe.AsRef<long>((void*)ptr), this.Sequence.Length);
					}
				}
				return this._length;
			}
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x00166D40 File Offset: 0x00164F40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe readonly bool TryPeek(out T value)
		{
			if (this._moreData)
			{
				value = *this.CurrentSpan[this.CurrentSpanIndex];
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x060069B7 RID: 27063 RVA: 0x00166D80 File Offset: 0x00164F80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe bool TryRead(out T value)
		{
			if (this.End)
			{
				value = default(T);
				return false;
			}
			value = *this.CurrentSpan[this.CurrentSpanIndex];
			int currentSpanIndex = this.CurrentSpanIndex;
			this.CurrentSpanIndex = currentSpanIndex + 1;
			long consumed = this.Consumed;
			this.Consumed = consumed + 1L;
			if (this.CurrentSpanIndex >= this.CurrentSpan.Length)
			{
				this.GetNextSpan();
			}
			return true;
		}

		// Token: 0x060069B8 RID: 27064 RVA: 0x00166DFC File Offset: 0x00164FFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Rewind(long count)
		{
			if (count > this.Consumed)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count);
			}
			this.Consumed -= count;
			if ((long)this.CurrentSpanIndex >= count)
			{
				this.CurrentSpanIndex -= (int)count;
				this._moreData = true;
				return;
			}
			this.RetreatToPreviousSpan(this.Consumed);
		}

		// Token: 0x060069B9 RID: 27065 RVA: 0x00166E54 File Offset: 0x00165054
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void RetreatToPreviousSpan(long consumed)
		{
			this.ResetReader();
			this.Advance(consumed);
		}

		// Token: 0x060069BA RID: 27066 RVA: 0x00166E64 File Offset: 0x00165064
		private void ResetReader()
		{
			this.CurrentSpanIndex = 0;
			this.Consumed = 0L;
			this._currentPosition = this.Sequence.Start;
			this._nextPosition = this._currentPosition;
			ReadOnlyMemory<T> readOnlyMemory;
			if (!this.Sequence.TryGet(ref this._nextPosition, out readOnlyMemory, true))
			{
				this._moreData = false;
				this.CurrentSpan = default(ReadOnlySpan<T>);
				return;
			}
			this._moreData = true;
			if (readOnlyMemory.Length == 0)
			{
				this.CurrentSpan = default(ReadOnlySpan<T>);
				this.GetNextSpan();
				return;
			}
			this.CurrentSpan = readOnlyMemory.Span;
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x00166F04 File Offset: 0x00165104
		private void GetNextSpan()
		{
			if (!this.Sequence.IsSingleSegment)
			{
				SequencePosition sequencePosition = this._nextPosition;
				ReadOnlyMemory<T> readOnlyMemory;
				while (this.Sequence.TryGet(ref this._nextPosition, out readOnlyMemory, true))
				{
					this._currentPosition = sequencePosition;
					if (readOnlyMemory.Length > 0)
					{
						this.CurrentSpan = readOnlyMemory.Span;
						this.CurrentSpanIndex = 0;
						return;
					}
					this.CurrentSpan = default(ReadOnlySpan<T>);
					this.CurrentSpanIndex = 0;
					sequencePosition = this._nextPosition;
				}
			}
			this._moreData = false;
		}

		// Token: 0x060069BC RID: 27068 RVA: 0x00166F90 File Offset: 0x00165190
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Advance(long count)
		{
			if ((count & -2147483648L) == 0L && this.CurrentSpan.Length - this.CurrentSpanIndex > (int)count)
			{
				this.CurrentSpanIndex += (int)count;
				this.Consumed += count;
				return;
			}
			this.AdvanceToNextSpan(count);
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x00166FE8 File Offset: 0x001651E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void AdvanceCurrentSpan(long count)
		{
			this.Consumed += count;
			this.CurrentSpanIndex += (int)count;
			if (this.CurrentSpanIndex >= this.CurrentSpan.Length)
			{
				this.GetNextSpan();
			}
		}

		// Token: 0x060069BE RID: 27070 RVA: 0x0016702E File Offset: 0x0016522E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void AdvanceWithinSpan(long count)
		{
			this.Consumed += count;
			this.CurrentSpanIndex += (int)count;
		}

		// Token: 0x060069BF RID: 27071 RVA: 0x00167050 File Offset: 0x00165250
		private void AdvanceToNextSpan(long count)
		{
			if (count < 0L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count);
			}
			this.Consumed += count;
			while (this._moreData)
			{
				int num = this.CurrentSpan.Length - this.CurrentSpanIndex;
				if ((long)num > count)
				{
					this.CurrentSpanIndex += (int)count;
					count = 0L;
					break;
				}
				this.CurrentSpanIndex += num;
				count -= (long)num;
				this.GetNextSpan();
				if (count == 0L)
				{
					break;
				}
			}
			if (count != 0L)
			{
				this.Consumed -= count;
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count);
			}
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x001670E8 File Offset: 0x001652E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool TryCopyTo(Span<T> destination)
		{
			ReadOnlySpan<T> unreadSpan = this.UnreadSpan;
			if (unreadSpan.Length >= destination.Length)
			{
				unreadSpan.Slice(0, destination.Length).CopyTo(destination);
				return true;
			}
			return this.TryCopyMultisegment(destination);
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x00167130 File Offset: 0x00165330
		internal readonly bool TryCopyMultisegment(Span<T> destination)
		{
			if (this.Remaining < (long)destination.Length)
			{
				return false;
			}
			ReadOnlySpan<T> unreadSpan = this.UnreadSpan;
			unreadSpan.CopyTo(destination);
			int num = unreadSpan.Length;
			SequencePosition nextPosition = this._nextPosition;
			ReadOnlyMemory<T> readOnlyMemory;
			while (this.Sequence.TryGet(ref nextPosition, out readOnlyMemory, true))
			{
				if (readOnlyMemory.Length > 0)
				{
					ReadOnlySpan<T> span = readOnlyMemory.Span;
					int num2 = Math.Min(span.Length, destination.Length - num);
					span.Slice(0, num2).CopyTo(destination.Slice(num));
					num += num2;
					if (num >= destination.Length)
					{
						break;
					}
				}
			}
			return true;
		}

		// Token: 0x04003CB6 RID: 15542
		private SequencePosition _currentPosition;

		// Token: 0x04003CB7 RID: 15543
		private SequencePosition _nextPosition;

		// Token: 0x04003CB8 RID: 15544
		private bool _moreData;

		// Token: 0x04003CB9 RID: 15545
		private readonly long _length;

		// Token: 0x04003CBA RID: 15546
		[CompilerGenerated]
		private readonly ReadOnlySequence<T> <Sequence>k__BackingField;

		// Token: 0x04003CBB RID: 15547
		[CompilerGenerated]
		private ReadOnlySpan<T> <CurrentSpan>k__BackingField;

		// Token: 0x04003CBC RID: 15548
		[CompilerGenerated]
		private int <CurrentSpanIndex>k__BackingField;

		// Token: 0x04003CBD RID: 15549
		[CompilerGenerated]
		private long <Consumed>k__BackingField;
	}
}
