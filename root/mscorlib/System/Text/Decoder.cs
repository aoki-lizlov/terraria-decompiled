using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000360 RID: 864
	[Serializable]
	public abstract class Decoder
	{
		// Token: 0x0600253F RID: 9535 RVA: 0x000025BE File Offset: 0x000007BE
		protected Decoder()
		{
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x00085920 File Offset: 0x00083B20
		// (set) Token: 0x06002541 RID: 9537 RVA: 0x00085928 File Offset: 0x00083B28
		public DecoderFallback Fallback
		{
			get
			{
				return this._fallback;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._fallbackBuffer != null && this._fallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException("Cannot change fallback when buffer is not empty. Previous Convert() call left data in the fallback buffer.", "value");
				}
				this._fallback = value;
				this._fallbackBuffer = null;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x00085977 File Offset: 0x00083B77
		public DecoderFallbackBuffer FallbackBuffer
		{
			get
			{
				if (this._fallbackBuffer == null)
				{
					if (this._fallback != null)
					{
						this._fallbackBuffer = this._fallback.CreateFallbackBuffer();
					}
					else
					{
						this._fallbackBuffer = DecoderFallback.ReplacementFallback.CreateFallbackBuffer();
					}
				}
				return this._fallbackBuffer;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x000859B2 File Offset: 0x00083BB2
		internal bool InternalHasFallbackBuffer
		{
			get
			{
				return this._fallbackBuffer != null;
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000859C0 File Offset: 0x00083BC0
		public virtual void Reset()
		{
			byte[] array = Array.Empty<byte>();
			char[] array2 = new char[this.GetCharCount(array, 0, 0, true)];
			this.GetChars(array, 0, 0, array2, 0, true);
			DecoderFallbackBuffer fallbackBuffer = this._fallbackBuffer;
			if (fallbackBuffer == null)
			{
				return;
			}
			fallbackBuffer.Reset();
		}

		// Token: 0x06002545 RID: 9541
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		// Token: 0x06002546 RID: 9542 RVA: 0x00085A00 File Offset: 0x00083C00
		public virtual int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			return this.GetCharCount(bytes, index, count);
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00085A0C File Offset: 0x00083C0C
		[CLSCompliant(false)]
		public unsafe virtual int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x00085A68 File Offset: 0x00083C68
		public unsafe virtual int GetCharCount(ReadOnlySpan<byte> bytes, bool flush)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* ptr = nonNullPinnableReference;
				return this.GetCharCount(ptr, bytes.Length, flush);
			}
		}

		// Token: 0x06002549 RID: 9545
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		// Token: 0x0600254A RID: 9546 RVA: 0x00085A8E File Offset: 0x00083C8E
		public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x00085AA0 File Offset: 0x00083CA0
		[CLSCompliant(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", "Non-negative number required.");
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] array2 = new char[charCount];
			int chars2 = this.GetChars(array, 0, byteCount, array2, 0, flush);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = array2[i];
			}
			return charCount;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00085B48 File Offset: 0x00083D48
		public unsafe virtual int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars, bool flush)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* ptr = nonNullPinnableReference;
				fixed (char* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
				{
					char* ptr2 = nonNullPinnableReference2;
					return this.GetChars(ptr, bytes.Length, ptr2, chars.Length, flush);
				}
			}
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x00085B80 File Offset: 0x00083D80
		public virtual void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
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
			for (bytesUsed = byteCount; bytesUsed > 0; bytesUsed /= 2)
			{
				if (this.GetCharCount(bytes, byteIndex, bytesUsed, flush) <= charCount)
				{
					charsUsed = this.GetChars(bytes, byteIndex, bytesUsed, chars, charIndex, flush);
					completed = bytesUsed == byteCount && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0);
					return;
				}
				flush = false;
			}
			throw new ArgumentException("Conversion buffer overflow.");
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00085C98 File Offset: 0x00083E98
		[CLSCompliant(false)]
		public unsafe virtual void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", "Non-negative number required.");
			}
			for (bytesUsed = byteCount; bytesUsed > 0; bytesUsed /= 2)
			{
				if (this.GetCharCount(bytes, bytesUsed, flush) <= charCount)
				{
					charsUsed = this.GetChars(bytes, bytesUsed, chars, charCount, flush);
					completed = bytesUsed == byteCount && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0);
					return;
				}
				flush = false;
			}
			throw new ArgumentException("Conversion buffer overflow.");
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00085D58 File Offset: 0x00083F58
		public unsafe virtual void Convert(ReadOnlySpan<byte> bytes, Span<char> chars, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* ptr = nonNullPinnableReference;
				fixed (char* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
				{
					char* ptr2 = nonNullPinnableReference2;
					this.Convert(ptr, bytes.Length, ptr2, chars.Length, flush, out bytesUsed, out charsUsed, out completed);
				}
			}
		}

		// Token: 0x04001C57 RID: 7255
		internal DecoderFallback _fallback;

		// Token: 0x04001C58 RID: 7256
		internal DecoderFallbackBuffer _fallbackBuffer;
	}
}
