using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200013A RID: 314
	public readonly struct Range : IEquatable<Range>
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x000339D0 File Offset: 0x00031BD0
		public Index Start
		{
			[CompilerGenerated]
			get
			{
				return this.<Start>k__BackingField;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x000339D8 File Offset: 0x00031BD8
		public Index End
		{
			[CompilerGenerated]
			get
			{
				return this.<End>k__BackingField;
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000339E0 File Offset: 0x00031BE0
		public Range(Index start, Index end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000339F0 File Offset: 0x00031BF0
		public override bool Equals(object value)
		{
			if (value is Range)
			{
				Range range = (Range)value;
				return range.Start.Equals(this.Start) && range.End.Equals(this.End);
			}
			return false;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00033A3C File Offset: 0x00031C3C
		public bool Equals(Range other)
		{
			return other.Start.Equals(this.Start) && other.End.Equals(this.End);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00033A78 File Offset: 0x00031C78
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int>(this.Start.GetHashCode(), this.End.GetHashCode());
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00033AB4 File Offset: 0x00031CB4
		public unsafe override string ToString()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)48], 24);
			int num = 0;
			if (this.Start.IsFromEnd)
			{
				*span[0] = '^';
				num = 1;
			}
			int num2;
			((uint)this.Start.Value).TryFormat(span.Slice(num), out num2, default(ReadOnlySpan<char>), null);
			num += num2;
			*span[num++] = '.';
			*span[num++] = '.';
			if (this.End.IsFromEnd)
			{
				*span[num++] = '^';
			}
			((uint)this.End.Value).TryFormat(span.Slice(num), out num2, default(ReadOnlySpan<char>), null);
			num += num2;
			return new string(span.Slice(0, num));
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00033B9F File Offset: 0x00031D9F
		public static Range StartAt(Index start)
		{
			return new Range(start, Index.End);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00033BAC File Offset: 0x00031DAC
		public static Range EndAt(Index end)
		{
			return new Range(Index.Start, end);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00033BB9 File Offset: 0x00031DB9
		public static Range All
		{
			get
			{
				return new Range(Index.Start, Index.End);
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00033BCC File Offset: 0x00031DCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: TupleElementNames(new string[] { "Offset", "Length" })]
		public ValueTuple<int, int> GetOffsetAndLength(int length)
		{
			Index start = this.Start;
			int num;
			if (start.IsFromEnd)
			{
				num = length - start.Value;
			}
			else
			{
				num = start.Value;
			}
			Index end = this.End;
			int num2;
			if (end.IsFromEnd)
			{
				num2 = length - end.Value;
			}
			else
			{
				num2 = end.Value;
			}
			if (num2 > length || num > num2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new ValueTuple<int, int>(num, num2 - num);
		}

		// Token: 0x04001150 RID: 4432
		[CompilerGenerated]
		private readonly Index <Start>k__BackingField;

		// Token: 0x04001151 RID: 4433
		[CompilerGenerated]
		private readonly Index <End>k__BackingField;
	}
}
