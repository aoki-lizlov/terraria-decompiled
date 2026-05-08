using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000368 RID: 872
	internal class DecoderNLS : Decoder
	{
		// Token: 0x06002580 RID: 9600 RVA: 0x00086381 File Offset: 0x00084581
		internal DecoderNLS(Encoding encoding)
		{
			this._encoding = encoding;
			this._fallback = this._encoding.DecoderFallback;
			this.Reset();
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000863A7 File Offset: 0x000845A7
		internal DecoderNLS()
		{
			this._encoding = null;
			this.Reset();
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000863BC File Offset: 0x000845BC
		public override void Reset()
		{
			DecoderFallbackBuffer fallbackBuffer = this._fallbackBuffer;
			if (fallbackBuffer == null)
			{
				return;
			}
			fallbackBuffer.Reset();
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000863CE File Offset: 0x000845CE
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000863DC File Offset: 0x000845DC
		public unsafe override int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "Array cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				return this.GetCharCount(ptr + index, count, flush);
			}
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x00086454 File Offset: 0x00084654
		public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetCharCount(bytes, count, this);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000864A6 File Offset: 0x000846A6
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000864B8 File Offset: 0x000846B8
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", "Non-negative number required.");
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = chars.Length - charIndex;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr2 = reference2;
					return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, num, flush);
				}
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0008657C File Offset: 0x0008477C
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetChars(bytes, byteCount, chars, charCount, this);
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000865F4 File Offset: 0x000847F4
		public unsafe override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", "Non-negative number required.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr2 = reference2;
					this.Convert(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
				}
			}
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000866E0 File Offset: 0x000848E0
		public unsafe override void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = false;
			this._bytesUsed = 0;
			charsUsed = this._encoding.GetChars(bytes, byteCount, chars, charCount, this);
			bytesUsed = this._bytesUsed;
			completed = bytesUsed == byteCount && (!flush || !this.HasState) && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0);
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x0008679B File Offset: 0x0008499B
		public bool MustFlush
		{
			get
			{
				return this._mustFlush;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x0000408A File Offset: 0x0000228A
		internal virtual bool HasState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000867A3 File Offset: 0x000849A3
		internal void ClearMustFlush()
		{
			this._mustFlush = false;
		}

		// Token: 0x04001C67 RID: 7271
		private Encoding _encoding;

		// Token: 0x04001C68 RID: 7272
		private bool _mustFlush;

		// Token: 0x04001C69 RID: 7273
		internal bool _throwOnOverflow;

		// Token: 0x04001C6A RID: 7274
		internal int _bytesUsed;
	}
}
