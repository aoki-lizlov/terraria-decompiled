using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x0200036B RID: 875
	[Serializable]
	public abstract class Encoder
	{
		// Token: 0x0600259E RID: 9630 RVA: 0x000025BE File Offset: 0x000007BE
		protected Encoder()
		{
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x00086A0E File Offset: 0x00084C0E
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x00086A18 File Offset: 0x00084C18
		public EncoderFallback Fallback
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

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x00086A67 File Offset: 0x00084C67
		public EncoderFallbackBuffer FallbackBuffer
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
						this._fallbackBuffer = EncoderFallback.ReplacementFallback.CreateFallbackBuffer();
					}
				}
				return this._fallbackBuffer;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x00086AA2 File Offset: 0x00084CA2
		internal bool InternalHasFallbackBuffer
		{
			get
			{
				return this._fallbackBuffer != null;
			}
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00086AB0 File Offset: 0x00084CB0
		public virtual void Reset()
		{
			char[] array = new char[0];
			byte[] array2 = new byte[this.GetByteCount(array, 0, 0, true)];
			this.GetBytes(array, 0, 0, array2, 0, true);
			if (this._fallbackBuffer != null)
			{
				this._fallbackBuffer.Reset();
			}
		}

		// Token: 0x060025A4 RID: 9636
		public abstract int GetByteCount(char[] chars, int index, int count, bool flush);

		// Token: 0x060025A5 RID: 9637 RVA: 0x00086AF4 File Offset: 0x00084CF4
		[CLSCompliant(false)]
		public unsafe virtual int GetByteCount(char* chars, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array, 0, count, flush);
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x00086B54 File Offset: 0x00084D54
		public unsafe virtual int GetByteCount(ReadOnlySpan<char> chars, bool flush)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* ptr = nonNullPinnableReference;
				return this.GetByteCount(ptr, chars.Length, flush);
			}
		}

		// Token: 0x060025A7 RID: 9639
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush);

		// Token: 0x060025A8 RID: 9640 RVA: 0x00086B7C File Offset: 0x00084D7C
		[CLSCompliant(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", "Non-negative number required.");
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] array2 = new byte[byteCount];
			int bytes2 = this.GetBytes(array, 0, charCount, array2, 0, flush);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = array2[i];
			}
			return byteCount;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00086C24 File Offset: 0x00084E24
		public unsafe virtual int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, bool flush)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* ptr = nonNullPinnableReference;
				fixed (byte* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
				{
					byte* ptr2 = nonNullPinnableReference2;
					return this.GetBytes(ptr, chars.Length, ptr2, bytes.Length, flush);
				}
			}
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00086C5C File Offset: 0x00084E5C
		public virtual void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", "Non-negative number required.");
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			for (charsUsed = charCount; charsUsed > 0; charsUsed /= 2)
			{
				if (this.GetByteCount(chars, charIndex, charsUsed, flush) <= byteCount)
				{
					bytesUsed = this.GetBytes(chars, charIndex, charsUsed, bytes, byteIndex, flush);
					completed = charsUsed == charCount && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0);
					return;
				}
				flush = false;
			}
			throw new ArgumentException("Conversion buffer overflow.");
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x00086D74 File Offset: 0x00084F74
		[CLSCompliant(false)]
		public unsafe virtual void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", "Non-negative number required.");
			}
			for (charsUsed = charCount; charsUsed > 0; charsUsed /= 2)
			{
				if (this.GetByteCount(chars, charsUsed, flush) <= byteCount)
				{
					bytesUsed = this.GetBytes(chars, charsUsed, bytes, byteCount, flush);
					completed = charsUsed == charCount && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0);
					return;
				}
				flush = false;
			}
			throw new ArgumentException("Conversion buffer overflow.");
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00086E34 File Offset: 0x00085034
		public unsafe virtual void Convert(ReadOnlySpan<char> chars, Span<byte> bytes, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* ptr = nonNullPinnableReference;
				fixed (byte* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
				{
					byte* ptr2 = nonNullPinnableReference2;
					this.Convert(ptr, chars.Length, ptr2, bytes.Length, flush, out charsUsed, out bytesUsed, out completed);
				}
			}
		}

		// Token: 0x04001C6F RID: 7279
		internal EncoderFallback _fallback;

		// Token: 0x04001C70 RID: 7280
		internal EncoderFallbackBuffer _fallbackBuffer;
	}
}
