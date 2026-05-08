using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200010C RID: 268
	public readonly struct Index : IEquatable<Index>
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x00029732 File Offset: 0x00027932
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Index(int value, bool fromEnd = false)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			if (fromEnd)
			{
				this._value = ~value;
				return;
			}
			this._value = value;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00029750 File Offset: 0x00027950
		private Index(int value)
		{
			this._value = value;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00029759 File Offset: 0x00027959
		public static Index Start
		{
			get
			{
				return new Index(0);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00029761 File Offset: 0x00027961
		public static Index End
		{
			get
			{
				return new Index(-1);
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00029769 File Offset: 0x00027969
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Index FromStart(int value)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			return new Index(value);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002977A File Offset: 0x0002797A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Index FromEnd(int value)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			return new Index(~value);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002978C File Offset: 0x0002798C
		public int Value
		{
			get
			{
				if (this._value < 0)
				{
					return ~this._value;
				}
				return this._value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000297A5 File Offset: 0x000279A5
		public bool IsFromEnd
		{
			get
			{
				return this._value < 0;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000297B0 File Offset: 0x000279B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetOffset(int length)
		{
			int num;
			if (this.IsFromEnd)
			{
				num = length - ~this._value;
			}
			else
			{
				num = this._value;
			}
			return num;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000297D9 File Offset: 0x000279D9
		public override bool Equals(object value)
		{
			return value is Index && this._value == ((Index)value)._value;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000297F8 File Offset: 0x000279F8
		public bool Equals(Index other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00029808 File Offset: 0x00027A08
		public override int GetHashCode()
		{
			return this._value;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00029810 File Offset: 0x00027A10
		public static implicit operator Index(int value)
		{
			return Index.FromStart(value);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00029818 File Offset: 0x00027A18
		public override string ToString()
		{
			if (this.IsFromEnd)
			{
				return this.ToStringFromEnd();
			}
			return ((uint)this.Value).ToString();
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00029844 File Offset: 0x00027A44
		private unsafe string ToStringFromEnd()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)22], 11);
			int num;
			((uint)this.Value).TryFormat(span.Slice(1), out num, default(ReadOnlySpan<char>), null);
			*span[0] = '^';
			return new string(span.Slice(0, num + 1));
		}

		// Token: 0x040010D4 RID: 4308
		private readonly int _value;
	}
}
