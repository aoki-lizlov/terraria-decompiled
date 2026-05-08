using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x0200038A RID: 906
	internal ref struct ValueStringBuilder
	{
		// Token: 0x06002733 RID: 10035 RVA: 0x0008FF86 File Offset: 0x0008E186
		public ValueStringBuilder(Span<char> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._chars = initialBuffer;
			this._pos = 0;
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x0008FF9D File Offset: 0x0008E19D
		// (set) Token: 0x06002735 RID: 10037 RVA: 0x0008FFA5 File Offset: 0x0008E1A5
		public int Length
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x0008FFAE File Offset: 0x0008E1AE
		public int Capacity
		{
			get
			{
				return this._chars.Length;
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x0008FFBB File Offset: 0x0008E1BB
		public void EnsureCapacity(int capacity)
		{
			if (capacity > this._chars.Length)
			{
				this.Grow(capacity - this._chars.Length);
			}
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x0008FFDE File Offset: 0x0008E1DE
		public unsafe ref char GetPinnableReference(bool terminate = false)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return MemoryMarshal.GetReference<char>(this._chars);
		}

		// Token: 0x170004C7 RID: 1223
		public ref char this[int index]
		{
			get
			{
				return this._chars[index];
			}
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0009001D File Offset: 0x0008E21D
		public override string ToString()
		{
			string text = new string(this._chars.Slice(0, this._pos));
			this.Dispose();
			return text;
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x00090041 File Offset: 0x0008E241
		public Span<char> RawChars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x00090049 File Offset: 0x0008E249
		public unsafe ReadOnlySpan<char> AsSpan(bool terminate)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x00090086 File Offset: 0x0008E286
		public ReadOnlySpan<char> AsSpan()
		{
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x0009009F File Offset: 0x0008E29F
		public ReadOnlySpan<char> AsSpan(int start)
		{
			return this._chars.Slice(start, this._pos - start);
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000900BA File Offset: 0x0008E2BA
		public ReadOnlySpan<char> AsSpan(int start, int length)
		{
			return this._chars.Slice(start, length);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000900D0 File Offset: 0x0008E2D0
		public bool TryCopyTo(Span<char> destination, out int charsWritten)
		{
			if (this._chars.Slice(0, this._pos).TryCopyTo(destination))
			{
				charsWritten = this._pos;
				this.Dispose();
				return true;
			}
			charsWritten = 0;
			this.Dispose();
			return false;
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x00090114 File Offset: 0x0008E314
		public void Insert(int index, char value, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			int num = this._pos - index;
			this._chars.Slice(index, num).CopyTo(this._chars.Slice(index + count));
			this._chars.Slice(index, count).Fill(value);
			this._pos += count;
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x00090190 File Offset: 0x0008E390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(char c)
		{
			int pos = this._pos;
			if (pos < this._chars.Length)
			{
				*this._chars[pos] = c;
				this._pos = pos + 1;
				return;
			}
			this.GrowAndAppend(c);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000901D4 File Offset: 0x0008E3D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(string s)
		{
			int pos = this._pos;
			if (s.Length == 1 && pos < this._chars.Length)
			{
				*this._chars[pos] = s[0];
				this._pos = pos + 1;
				return;
			}
			this.AppendSlow(s);
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x00090224 File Offset: 0x0008E424
		private void AppendSlow(string s)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - s.Length)
			{
				this.Grow(s.Length);
			}
			s.AsSpan().CopyTo(this._chars.Slice(pos));
			this._pos += s.Length;
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x00090288 File Offset: 0x0008E488
		public unsafe void Append(char c, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			Span<char> span = this._chars.Slice(this._pos, count);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = c;
			}
			this._pos += count;
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000902F0 File Offset: 0x0008E4F0
		public unsafe void Append(char* value, int length)
		{
			if (this._pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			Span<char> span = this._chars.Slice(this._pos, length);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = *(value++);
			}
			this._pos += length;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0009035C File Offset: 0x0008E55C
		public void Append(ReadOnlySpan<char> value)
		{
			if (this._pos > this._chars.Length - value.Length)
			{
				this.Grow(value.Length);
			}
			value.CopyTo(this._chars.Slice(this._pos));
			this._pos += value.Length;
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000903C0 File Offset: 0x0008E5C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AppendSpan(int length)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			this._pos = pos + length;
			return this._chars.Slice(pos, length);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x00090401 File Offset: 0x0008E601
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GrowAndAppend(char c)
		{
			this.Grow(1);
			this.Append(c);
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x00090414 File Offset: 0x0008E614
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int requiredAdditionalCapacity)
		{
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(this._pos + requiredAdditionalCapacity, this._chars.Length * 2));
			this._chars.CopyTo(array);
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this._chars = (this._arrayToReturnToPool = array);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x00090484 File Offset: 0x0008E684
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this = default(ValueStringBuilder);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x04001CDA RID: 7386
		private char[] _arrayToReturnToPool;

		// Token: 0x04001CDB RID: 7387
		private Span<char> _chars;

		// Token: 0x04001CDC RID: 7388
		private int _pos;
	}
}
