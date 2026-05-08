using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000384 RID: 900
	[Serializable]
	public class UTF8Encoding : Encoding
	{
		// Token: 0x060026EA RID: 9962 RVA: 0x0008C81D File Offset: 0x0008AA1D
		public UTF8Encoding()
			: this(false)
		{
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x0008C826 File Offset: 0x0008AA26
		public UTF8Encoding(bool encoderShouldEmitUTF8Identifier)
			: this(encoderShouldEmitUTF8Identifier, false)
		{
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x0008C830 File Offset: 0x0008AA30
		public UTF8Encoding(bool encoderShouldEmitUTF8Identifier, bool throwOnInvalidBytes)
			: base(65001)
		{
			this._emitUTF8Identifier = encoderShouldEmitUTF8Identifier;
			this._isThrowException = throwOnInvalidBytes;
			if (this._isThrowException)
			{
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x0008C85C File Offset: 0x0008AA5C
		internal override void SetDefaultFallbacks()
		{
			if (this._isThrowException)
			{
				this.encoderFallback = EncoderFallback.ExceptionFallback;
				this.decoderFallback = DecoderFallback.ExceptionFallback;
				return;
			}
			this.encoderFallback = new EncoderReplacementFallback("\ufffd");
			this.decoderFallback = new DecoderReplacementFallback("\ufffd");
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0008C8A8 File Offset: 0x0008AAA8
		public unsafe override int GetByteCount(char[] chars, int index, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "Array cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (chars.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			if (count == 0)
			{
				return 0;
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			return this.GetByteCount(ptr + index, count, null);
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0008C930 File Offset: 0x0008AB30
		public unsafe override int GetByteCount(string chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = chars;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.GetByteCount(ptr, chars.Length, null);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00084DE5 File Offset: 0x00082FE5
		[CLSCompliant(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			return this.GetByteCount(chars, count, null);
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0008C96C File Offset: 0x0008AB6C
		public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null || bytes == null)
			{
				throw new ArgumentNullException((s == null) ? "s" : "bytes", "Array cannot be null.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (s.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("s", "Index and count must refer to a location within the string.");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = bytes.Length - byteIndex;
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr2 = reference;
				return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, null);
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0008CA34 File Offset: 0x0008AC34
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (charCount == 0)
			{
				return 0;
			}
			int num = bytes.Length - byteIndex;
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr2 = reference;
				return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, null);
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0008CB04 File Offset: 0x0008AD04
		[CLSCompliant(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", "Non-negative number required.");
			}
			return this.GetBytes(chars, charCount, bytes, byteCount, null);
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0008CB68 File Offset: 0x0008AD68
		public unsafe override int GetCharCount(byte[] bytes, int index, int count)
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
			if (count == 0)
			{
				return 0;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return this.GetCharCount(ptr + index, count, null);
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0008509B File Offset: 0x0008329B
		[CLSCompliant(false)]
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			return this.GetCharCount(bytes, count, null);
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0008CBEC File Offset: 0x0008ADEC
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
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
			if (byteCount == 0)
			{
				return 0;
			}
			int num = chars.Length - charIndex;
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr2 = reference;
				return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, num, null);
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0008CCBC File Offset: 0x0008AEBC
		[CLSCompliant(false)]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", "Non-negative number required.");
			}
			return this.GetChars(bytes, byteCount, chars, charCount, null);
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0008CD20 File Offset: 0x0008AF20
		public unsafe override string GetString(byte[] bytes, int index, int count)
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
			if (count == 0)
			{
				return string.Empty;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return string.CreateStringFromEncoding(ptr + index, count, this);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0008CDA8 File Offset: 0x0008AFA8
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
		{
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char* ptr = chars;
			char* ptr2 = ptr + count;
			int num = count;
			int num2 = 0;
			if (baseEncoder != null)
			{
				UTF8Encoding.UTF8Encoder utf8Encoder = (UTF8Encoding.UTF8Encoder)baseEncoder;
				num2 = utf8Encoder.surrogateChar;
				if (utf8Encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = utf8Encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(SR.Format("Must complete Convert() operation or call Encoder.Reset() before calling GetBytes() or GetByteCount(). Encoder '{0}' fallback '{1}'.", this.EncodingName, utf8Encoder.Fallback.GetType()));
					}
					encoderFallbackBuffer.InternalInitialize(chars, ptr2, utf8Encoder, false);
				}
			}
			for (;;)
			{
				if (ptr >= ptr2)
				{
					if (num2 == 0)
					{
						num2 = (int)((encoderFallbackBuffer != null) ? encoderFallbackBuffer.InternalGetNextChar() : '\0');
						if (num2 > 0)
						{
							num++;
							goto IL_014C;
						}
					}
					else if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
					{
						num2 = (int)encoderFallbackBuffer.InternalGetNextChar();
						num++;
						if (UTF8Encoding.InRange(num2, 56320, 57343))
						{
							num2 = 65533;
							num++;
							goto IL_016A;
						}
						if (num2 <= 0)
						{
							break;
						}
						goto IL_014C;
					}
					if (num2 > 0 && (baseEncoder == null || baseEncoder.MustFlush))
					{
						num++;
						goto IL_016A;
					}
					goto IL_0432;
				}
				else if (num2 > 0)
				{
					int num3 = (int)(*ptr);
					num++;
					if (UTF8Encoding.InRange(num3, 56320, 57343))
					{
						num2 = 65533;
						ptr++;
						goto IL_016A;
					}
					goto IL_016A;
				}
				else
				{
					if (encoderFallbackBuffer != null)
					{
						num2 = (int)encoderFallbackBuffer.InternalGetNextChar();
						if (num2 > 0)
						{
							num++;
							goto IL_014C;
						}
					}
					num2 = (int)(*ptr);
					ptr++;
				}
				IL_014C:
				if (UTF8Encoding.InRange(num2, 55296, 56319))
				{
					num--;
					continue;
				}
				IL_016A:
				if (UTF8Encoding.InRange(num2, 55296, 57343))
				{
					if (encoderFallbackBuffer == null)
					{
						if (baseEncoder == null)
						{
							encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						}
						else
						{
							encoderFallbackBuffer = baseEncoder.FallbackBuffer;
						}
						encoderFallbackBuffer.InternalInitialize(chars, chars + count, baseEncoder, false);
					}
					char* ptr3 = ptr;
					encoderFallbackBuffer.InternalFallback((char)num2, ref ptr3);
					ptr = ptr3;
					num--;
					num2 = 0;
				}
				else
				{
					if (num2 > 127)
					{
						if (num2 > 2047)
						{
							num++;
						}
						num++;
					}
					if (num < 0)
					{
						goto IL_0432;
					}
					if (encoderFallbackBuffer != null && (num2 = (int)encoderFallbackBuffer.InternalGetNextChar()) != 0)
					{
						num++;
						goto IL_014C;
					}
					int num4 = UTF8Encoding.PtrDiff(ptr2, ptr);
					if (num4 <= 13)
					{
						char* ptr4 = ptr2;
						while (ptr < ptr4)
						{
							num2 = (int)(*ptr);
							ptr++;
							if (num2 > 127)
							{
								goto IL_014C;
							}
						}
						goto Block_28;
					}
					num4 &= 268435455;
					char* ptr5 = ptr + num4 - 7;
					IL_0422:
					while (ptr < ptr5)
					{
						num2 = (int)(*ptr);
						ptr++;
						if (num2 > 127)
						{
							if (num2 > 2047)
							{
								if ((num2 & 63488) == 55296)
								{
									goto IL_03D0;
								}
								num++;
							}
							num++;
						}
						if ((ptr & 2) != 0)
						{
							num2 = (int)(*ptr);
							ptr++;
							if (num2 > 127)
							{
								if (num2 > 2047)
								{
									if ((num2 & 63488) == 55296)
									{
										goto IL_03D0;
									}
									num++;
								}
								num++;
							}
						}
						while (ptr < ptr5)
						{
							num2 = *(int*)ptr;
							int num5 = *(int*)(ptr + 2);
							if (((num2 | num5) & -8323200) != 0)
							{
								if (((num2 | num5) & -134154240) != 0)
								{
									goto IL_03B1;
								}
								if ((num2 & -8388608) != 0)
								{
									num++;
								}
								if ((num2 & 65408) != 0)
								{
									num++;
								}
								if ((num5 & -8388608) != 0)
								{
									num++;
								}
								if ((num5 & 65408) != 0)
								{
									num++;
								}
							}
							ptr += 4;
							num2 = *(int*)ptr;
							num5 = *(int*)(ptr + 2);
							if (((num2 | num5) & -8323200) != 0)
							{
								if (((num2 | num5) & -134154240) != 0)
								{
									goto IL_03B1;
								}
								if ((num2 & -8388608) != 0)
								{
									num++;
								}
								if ((num2 & 65408) != 0)
								{
									num++;
								}
								if ((num5 & -8388608) != 0)
								{
									num++;
								}
								if ((num5 & 65408) != 0)
								{
									num++;
								}
							}
							ptr += 4;
							continue;
							IL_03B1:
							if (BitConverter.IsLittleEndian)
							{
								num2 = (int)((ushort)num2);
							}
							else
							{
								num2 = (int)((uint)num2 >> 16);
							}
							ptr++;
							if (num2 > 127)
							{
								goto IL_03D0;
							}
							goto IL_0422;
						}
						break;
						IL_03D0:
						if (num2 > 2047)
						{
							if (UTF8Encoding.InRange(num2, 55296, 57343))
							{
								int num6 = (int)(*ptr);
								if (num2 > 56319 || !UTF8Encoding.InRange(num6, 56320, 57343))
								{
									ptr--;
									break;
								}
								ptr++;
							}
							num++;
						}
						num++;
					}
					num2 = 0;
				}
			}
			num--;
			Block_28:
			IL_0432:
			if (num < 0)
			{
				throw new ArgumentException("Conversion buffer overflow.");
			}
			return num;
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0008D1F9 File Offset: 0x0008B3F9
		private unsafe static int PtrDiff(char* a, char* b)
		{
			return (int)((uint)((long)((a - b) / 1 * 2)) >> 1);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0008D204 File Offset: 0x0008B404
		private unsafe static int PtrDiff(byte* a, byte* b)
		{
			return (int)((long)(a - b));
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x0008D20D File Offset: 0x0008B40D
		private static bool InRange(int ch, int start, int end)
		{
			return ch - start <= end - start;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x0008D21C File Offset: 0x0008B41C
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
		{
			UTF8Encoding.UTF8Encoder utf8Encoder = null;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char* ptr = chars;
			byte* ptr2 = bytes;
			char* ptr3 = ptr + charCount;
			byte* ptr4 = ptr2 + byteCount;
			int num = 0;
			if (baseEncoder != null)
			{
				utf8Encoder = (UTF8Encoding.UTF8Encoder)baseEncoder;
				num = utf8Encoder.surrogateChar;
				if (utf8Encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = utf8Encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0 && utf8Encoder._throwOnOverflow)
					{
						throw new ArgumentException(SR.Format("Must complete Convert() operation or call Encoder.Reset() before calling GetBytes() or GetByteCount(). Encoder '{0}' fallback '{1}'.", this.EncodingName, utf8Encoder.Fallback.GetType()));
					}
					encoderFallbackBuffer.InternalInitialize(chars, ptr3, utf8Encoder, true);
				}
			}
			for (;;)
			{
				if (ptr >= ptr3)
				{
					if (num == 0)
					{
						num = (int)((encoderFallbackBuffer != null) ? encoderFallbackBuffer.InternalGetNextChar() : '\0');
						if (num > 0)
						{
							goto IL_0147;
						}
					}
					else if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
					{
						int num2 = num;
						num = (int)encoderFallbackBuffer.InternalGetNextChar();
						if (UTF8Encoding.InRange(num, 56320, 57343))
						{
							num = num + (num2 << 10) + -56613888;
							goto IL_015D;
						}
						if (num > 0)
						{
							goto IL_0147;
						}
						goto IL_053C;
					}
					if (num <= 0)
					{
						goto IL_053C;
					}
					if (utf8Encoder == null)
					{
						goto IL_015D;
					}
					if (utf8Encoder.MustFlush)
					{
						goto IL_015D;
					}
					goto IL_053C;
				}
				else if (num > 0)
				{
					int num3 = (int)(*ptr);
					if (UTF8Encoding.InRange(num3, 56320, 57343))
					{
						num = num3 + (num << 10) + -56613888;
						ptr++;
						goto IL_015D;
					}
					goto IL_015D;
				}
				else
				{
					if (encoderFallbackBuffer != null)
					{
						num = (int)encoderFallbackBuffer.InternalGetNextChar();
						if (num > 0)
						{
							goto IL_0147;
						}
					}
					num = (int)(*ptr);
					ptr++;
				}
				IL_0147:
				if (UTF8Encoding.InRange(num, 55296, 56319))
				{
					continue;
				}
				IL_015D:
				if (UTF8Encoding.InRange(num, 55296, 57343))
				{
					if (encoderFallbackBuffer == null)
					{
						if (baseEncoder == null)
						{
							encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						}
						else
						{
							encoderFallbackBuffer = baseEncoder.FallbackBuffer;
						}
						encoderFallbackBuffer.InternalInitialize(chars, ptr3, baseEncoder, true);
					}
					char* ptr5 = ptr;
					encoderFallbackBuffer.InternalFallback((char)num, ref ptr5);
					ptr = ptr5;
					num = 0;
				}
				else
				{
					int num4 = 1;
					if (num > 127)
					{
						if (num > 2047)
						{
							if (num > 65535)
							{
								num4++;
							}
							num4++;
						}
						num4++;
					}
					if (ptr2 != ptr4 - num4)
					{
						break;
					}
					if (num <= 127)
					{
						*ptr2 = (byte)num;
					}
					else
					{
						int num5;
						if (num <= 2047)
						{
							num5 = (int)((byte)(-64 | (num >> 6)));
						}
						else
						{
							if (num <= 65535)
							{
								num5 = (int)((byte)(-32 | (num >> 12)));
							}
							else
							{
								*ptr2 = (byte)(-16 | (num >> 18));
								ptr2++;
								num5 = -128 | ((num >> 12) & 63);
							}
							*ptr2 = (byte)num5;
							ptr2++;
							num5 = -128 | ((num >> 6) & 63);
						}
						*ptr2 = (byte)num5;
						ptr2++;
						*ptr2 = (byte)(-128 | (num & 63));
					}
					ptr2++;
					if (encoderFallbackBuffer != null && (num = (int)encoderFallbackBuffer.InternalGetNextChar()) != 0)
					{
						goto IL_0147;
					}
					int num6 = UTF8Encoding.PtrDiff(ptr3, ptr);
					int num7 = UTF8Encoding.PtrDiff(ptr4, ptr2);
					if (num6 <= 13)
					{
						if (num7 >= num6)
						{
							char* ptr6 = ptr3;
							while (ptr < ptr6)
							{
								num = (int)(*ptr);
								ptr++;
								if (num > 127)
								{
									goto IL_0147;
								}
								*ptr2 = (byte)num;
								ptr2++;
							}
							goto Block_37;
						}
						num = 0;
					}
					else
					{
						if (num7 < num6)
						{
							num6 = num7;
						}
						char* ptr7 = ptr + num6 - 5;
						while (ptr < ptr7)
						{
							num = (int)(*ptr);
							ptr++;
							if (num <= 127)
							{
								*ptr2 = (byte)num;
								ptr2++;
								if ((ptr & 2) != 0)
								{
									num = (int)(*ptr);
									ptr++;
									if (num > 127)
									{
										goto IL_044F;
									}
									*ptr2 = (byte)num;
									ptr2++;
								}
								while (ptr < ptr7)
								{
									num = *(int*)ptr;
									int num8 = *(int*)(ptr + 2);
									if (((num | num8) & -8323200) == 0)
									{
										if (BitConverter.IsLittleEndian)
										{
											*ptr2 = (byte)num;
											ptr2[1] = (byte)(num >> 16);
											ptr += 4;
											ptr2[2] = (byte)num8;
											ptr2[3] = (byte)(num8 >> 16);
											ptr2 += 4;
										}
										else
										{
											*ptr2 = (byte)(num >> 16);
											ptr2[1] = (byte)num;
											ptr += 4;
											ptr2[2] = (byte)(num8 >> 16);
											ptr2[3] = (byte)num8;
											ptr2 += 4;
										}
									}
									else
									{
										if (BitConverter.IsLittleEndian)
										{
											num = (int)((ushort)num);
										}
										else
										{
											num = (int)((uint)num >> 16);
										}
										ptr++;
										if (num <= 127)
										{
											*ptr2 = (byte)num;
											ptr2++;
											break;
										}
										goto IL_044F;
									}
								}
								continue;
							}
							IL_044F:
							int num9;
							if (num <= 2047)
							{
								num9 = -64 | (num >> 6);
							}
							else
							{
								if (!UTF8Encoding.InRange(num, 55296, 57343))
								{
									num9 = -32 | (num >> 12);
								}
								else
								{
									if (num > 56319)
									{
										ptr--;
										break;
									}
									num9 = (int)(*ptr);
									ptr++;
									if (!UTF8Encoding.InRange(num9, 56320, 57343))
									{
										ptr -= 2;
										break;
									}
									num = num9 + (num << 10) + -56613888;
									*ptr2 = (byte)(-16 | (num >> 18));
									ptr2++;
									num9 = -128 | ((num >> 12) & 63);
								}
								*ptr2 = (byte)num9;
								ptr7--;
								ptr2++;
								num9 = -128 | ((num >> 6) & 63);
							}
							*ptr2 = (byte)num9;
							ptr7--;
							ptr2++;
							*ptr2 = (byte)(-128 | (num & 63));
							ptr2++;
						}
						num = 0;
					}
				}
			}
			if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
			{
				encoderFallbackBuffer.MovePrevious();
				if (num > 65535)
				{
					encoderFallbackBuffer.MovePrevious();
				}
			}
			else
			{
				ptr--;
				if (num > 65535)
				{
					ptr--;
				}
			}
			base.ThrowBytesOverflow(utf8Encoder, ptr2 == bytes);
			num = 0;
			goto IL_053C;
			Block_37:
			num = 0;
			IL_053C:
			if (utf8Encoder != null)
			{
				utf8Encoder.surrogateChar = num;
				utf8Encoder._charsUsed = (int)((long)(ptr - chars));
			}
			return (int)((long)(ptr2 - bytes));
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0008D788 File Offset: 0x0008B988
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			byte* ptr = bytes;
			byte* ptr2 = ptr + count;
			int num = count;
			int num2 = 0;
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			if (baseDecoder != null)
			{
				num2 = ((UTF8Encoding.UTF8Decoder)baseDecoder).bits;
				num -= num2 >> 30;
			}
			IL_0023:
			while (ptr < ptr2)
			{
				if (num2 == 0)
				{
					num2 = (int)(*ptr);
					ptr++;
					goto IL_0106;
				}
				int num3 = (int)(*ptr);
				ptr++;
				if ((num3 & -64) != 128)
				{
					ptr--;
					num += num2 >> 30;
				}
				else
				{
					num2 = (num2 << 6) | (num3 & 63);
					if ((num2 & 536870912) == 0)
					{
						if ((num2 & 268435456) != 0)
						{
							if ((num2 & 8388608) != 0 || UTF8Encoding.InRange(num2 & 496, 16, 256))
							{
								continue;
							}
						}
						else if ((num2 & 992) != 0)
						{
							if ((num2 & 992) != 864)
							{
								continue;
							}
						}
					}
					else
					{
						if ((num2 & 270467072) == 268435456)
						{
							num--;
							goto IL_017C;
						}
						goto IL_017C;
					}
				}
				IL_00C3:
				if (decoderFallbackBuffer == null)
				{
					if (baseDecoder == null)
					{
						decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
					}
					else
					{
						decoderFallbackBuffer = baseDecoder.FallbackBuffer;
					}
					decoderFallbackBuffer.InternalInitialize(bytes, null);
				}
				num += this.FallbackInvalidByteSequence(ptr, num2, decoderFallbackBuffer);
				num2 = 0;
				continue;
				IL_017C:
				int num4 = UTF8Encoding.PtrDiff(ptr2, ptr);
				if (num4 <= 13)
				{
					byte* ptr3 = ptr2;
					while (ptr < ptr3)
					{
						num2 = (int)(*ptr);
						ptr++;
						if (num2 > 127)
						{
							goto IL_0106;
						}
					}
					num2 = 0;
					break;
				}
				byte* ptr4 = ptr + num4 - 7;
				while (ptr < ptr4)
				{
					num2 = (int)(*ptr);
					ptr++;
					if (num2 <= 127)
					{
						if ((ptr & 1) != 0)
						{
							num2 = (int)(*ptr);
							ptr++;
							if (num2 > 127)
							{
								goto IL_026F;
							}
						}
						if ((ptr & 2) != 0)
						{
							num2 = (int)(*(ushort*)ptr);
							if ((num2 & 32896) != 0)
							{
								goto IL_024E;
							}
							ptr += 2;
						}
						while (ptr < ptr4)
						{
							num2 = *(int*)ptr;
							int num5 = *(int*)(ptr + 4);
							if (((num2 | num5) & -2139062144) == 0)
							{
								ptr += 8;
								if (ptr >= ptr4)
								{
									break;
								}
								num2 = *(int*)ptr;
								num5 = *(int*)(ptr + 4);
								if (((num2 | num5) & -2139062144) == 0)
								{
									ptr += 8;
									continue;
								}
							}
							if (BitConverter.IsLittleEndian)
							{
								num2 &= 255;
								goto IL_024E;
							}
							num2 = (int)((uint)num2 >> 16);
							goto IL_024E;
						}
						break;
						IL_024E:
						if (BitConverter.IsLittleEndian)
						{
							num2 &= 255;
						}
						else
						{
							num2 = (int)((uint)num2 >> 8);
						}
						ptr++;
						if (num2 <= 127)
						{
							continue;
						}
					}
					IL_026F:
					int num6 = (int)(*ptr);
					ptr++;
					if ((num2 & 64) != 0 && (num6 & -64) == 128)
					{
						num6 &= 63;
						if ((num2 & 32) != 0)
						{
							num6 |= (num2 & 15) << 6;
							if ((num2 & 16) != 0)
							{
								num2 = (int)(*ptr);
								if (!UTF8Encoding.InRange(num6 >> 4, 1, 16) || (num2 & -64) != 128)
								{
									goto IL_033B;
								}
								num6 = (num6 << 6) | (num2 & 63);
								num2 = (int)ptr[1];
								if ((num2 & -64) != 128)
								{
									goto IL_033B;
								}
								ptr += 2;
								num--;
							}
							else
							{
								num2 = (int)(*ptr);
								if ((num6 & 992) == 0 || (num6 & 992) == 864 || (num2 & -64) != 128)
								{
									goto IL_033B;
								}
								ptr++;
								num--;
							}
						}
						else if ((num2 & 30) == 0)
						{
							goto IL_033B;
						}
						num--;
						continue;
					}
					IL_033B:
					ptr -= 2;
					num2 = 0;
					goto IL_0023;
				}
				num2 = 0;
				continue;
				IL_0106:
				if (num2 <= 127)
				{
					goto IL_017C;
				}
				num--;
				if ((num2 & 64) == 0)
				{
					goto IL_00C3;
				}
				if ((num2 & 32) != 0)
				{
					if ((num2 & 16) != 0)
					{
						num2 &= 15;
						if (num2 > 4)
						{
							num2 |= 240;
							goto IL_00C3;
						}
						num2 |= 1347226624;
						num--;
					}
					else
					{
						num2 = (num2 & 15) | 1210220544;
						num--;
					}
				}
				else
				{
					num2 &= 31;
					if (num2 <= 1)
					{
						num2 |= 192;
						goto IL_00C3;
					}
					num2 |= 8388608;
				}
			}
			if (num2 != 0)
			{
				num += num2 >> 30;
				if (baseDecoder == null || baseDecoder.MustFlush)
				{
					if (decoderFallbackBuffer == null)
					{
						if (baseDecoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = baseDecoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(bytes, null);
					}
					num += this.FallbackInvalidByteSequence(ptr, num2, decoderFallbackBuffer);
				}
			}
			return num;
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x0008DB28 File Offset: 0x0008BD28
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			byte* ptr = bytes;
			char* ptr2 = chars;
			byte* ptr3 = ptr + byteCount;
			char* ptr4 = ptr2 + charCount;
			int num = 0;
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			if (baseDecoder != null)
			{
				num = ((UTF8Encoding.UTF8Decoder)baseDecoder).bits;
			}
			IL_0028:
			while (ptr < ptr3)
			{
				if (num == 0)
				{
					num = (int)(*ptr);
					ptr++;
					goto IL_0169;
				}
				int num2 = (int)(*ptr);
				ptr++;
				if ((num2 & -64) != 128)
				{
					ptr--;
				}
				else
				{
					num = (num << 6) | (num2 & 63);
					if ((num & 536870912) == 0)
					{
						if ((num & 268435456) != 0)
						{
							if ((num & 8388608) != 0 || UTF8Encoding.InRange(num & 496, 16, 256))
							{
								continue;
							}
						}
						else if ((num & 992) != 0)
						{
							if ((num & 992) != 864)
							{
								continue;
							}
						}
					}
					else
					{
						if ((num & 270467072) > 268435456 && ptr2 < ptr4)
						{
							*ptr2 = (char)(((num >> 10) & 2047) + -10304);
							ptr2++;
							num = (num & 1023) + 56320;
							goto IL_01EA;
						}
						goto IL_01EA;
					}
				}
				IL_00F9:
				if (decoderFallbackBuffer == null)
				{
					if (baseDecoder == null)
					{
						decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
					}
					else
					{
						decoderFallbackBuffer = baseDecoder.FallbackBuffer;
					}
					decoderFallbackBuffer.InternalInitialize(bytes, ptr4);
				}
				byte* ptr5 = ptr;
				char* ptr6 = ptr2;
				bool flag = this.FallbackInvalidByteSequence(ref ptr5, num, decoderFallbackBuffer, ref ptr6);
				ptr = ptr5;
				ptr2 = ptr6;
				if (!flag)
				{
					decoderFallbackBuffer.InternalReset();
					base.ThrowCharsOverflow(baseDecoder, ptr2 == chars);
					num = 0;
					break;
				}
				num = 0;
				continue;
				IL_01EA:
				if (ptr2 >= ptr4)
				{
					num &= 2097151;
					if (num > 127)
					{
						if (num > 2047)
						{
							if (num >= 56320 && num <= 57343)
							{
								ptr--;
								ptr2--;
							}
							else if (num > 65535)
							{
								ptr--;
							}
							ptr--;
						}
						ptr--;
					}
					ptr--;
					base.ThrowCharsOverflow(baseDecoder, ptr2 == chars);
					num = 0;
					break;
				}
				*ptr2 = (char)num;
				ptr2++;
				int num3 = UTF8Encoding.PtrDiff(ptr4, ptr2);
				int num4 = UTF8Encoding.PtrDiff(ptr3, ptr);
				if (num4 > 13)
				{
					if (num3 < num4)
					{
						num4 = num3;
					}
					char* ptr7 = ptr2 + num4 - 7;
					while (ptr2 < ptr7)
					{
						num = (int)(*ptr);
						ptr++;
						if (num <= 127)
						{
							*ptr2 = (char)num;
							ptr2++;
							if ((ptr & 1) != 0)
							{
								num = (int)(*ptr);
								ptr++;
								if (num > 127)
								{
									goto IL_04DC;
								}
								*ptr2 = (char)num;
								ptr2++;
							}
							if ((ptr & 2) != 0)
							{
								num = (int)(*(ushort*)ptr);
								if ((num & 32896) != 0)
								{
									goto IL_04AB;
								}
								if (BitConverter.IsLittleEndian)
								{
									*ptr2 = (char)(num & 127);
									ptr += 2;
									ptr2[1] = (char)((num >> 8) & 127);
									ptr2 += 2;
								}
								else
								{
									*ptr2 = (char)((num >> 8) & 127);
									ptr += 2;
									ptr2[1] = (char)(num & 127);
									ptr2 += 2;
								}
							}
							while (ptr2 < ptr7)
							{
								num = *(int*)ptr;
								int num5 = *(int*)(ptr + 4);
								if (((num | num5) & -2139062144) == 0)
								{
									if (BitConverter.IsLittleEndian)
									{
										*ptr2 = (char)(num & 127);
										ptr2[1] = (char)((num >> 8) & 127);
										ptr2[2] = (char)((num >> 16) & 127);
										ptr2[3] = (char)((num >> 24) & 127);
										ptr += 8;
										ptr2[4] = (char)(num5 & 127);
										ptr2[5] = (char)((num5 >> 8) & 127);
										ptr2[6] = (char)((num5 >> 16) & 127);
										ptr2[7] = (char)((num5 >> 24) & 127);
										ptr2 += 8;
									}
									else
									{
										*ptr2 = (char)((num >> 24) & 127);
										ptr2[1] = (char)((num >> 16) & 127);
										ptr2[2] = (char)((num >> 8) & 127);
										ptr2[3] = (char)(num & 127);
										ptr += 8;
										ptr2[4] = (char)((num5 >> 24) & 127);
										ptr2[5] = (char)((num5 >> 16) & 127);
										ptr2[6] = (char)((num5 >> 8) & 127);
										ptr2[7] = (char)(num5 & 127);
										ptr2 += 8;
									}
								}
								else
								{
									if (BitConverter.IsLittleEndian)
									{
										num &= 255;
										goto IL_04AB;
									}
									num = (int)((uint)num >> 16);
									goto IL_04AB;
								}
							}
							break;
							IL_04AB:
							if (BitConverter.IsLittleEndian)
							{
								num &= 255;
							}
							else
							{
								num = (int)((uint)num >> 8);
							}
							ptr++;
							if (num <= 127)
							{
								*ptr2 = (char)num;
								ptr2++;
								continue;
							}
						}
						IL_04DC:
						int num6 = (int)(*ptr);
						ptr++;
						if ((num & 64) != 0 && (num6 & -64) == 128)
						{
							num6 &= 63;
							if ((num & 32) != 0)
							{
								num6 |= (num & 15) << 6;
								if ((num & 16) != 0)
								{
									num = (int)(*ptr);
									if (!UTF8Encoding.InRange(num6 >> 4, 1, 16) || (num & -64) != 128)
									{
										goto IL_061E;
									}
									num6 = (num6 << 6) | (num & 63);
									num = (int)ptr[1];
									if ((num & -64) != 128)
									{
										goto IL_061E;
									}
									ptr += 2;
									num = (num6 << 6) | (num & 63);
									*ptr2 = (char)(((num >> 10) & 2047) + -10304);
									ptr2++;
									num = (num & 1023) + -9216;
									ptr7--;
								}
								else
								{
									num = (int)(*ptr);
									if ((num6 & 992) == 0 || (num6 & 992) == 864 || (num & -64) != 128)
									{
										goto IL_061E;
									}
									ptr++;
									num = (num6 << 6) | (num & 63);
									ptr7--;
								}
							}
							else
							{
								num &= 31;
								if (num <= 1)
								{
									goto IL_061E;
								}
								num = (num << 6) | num6;
							}
							*ptr2 = (char)num;
							ptr2++;
							ptr7--;
							continue;
						}
						IL_061E:
						ptr -= 2;
						num = 0;
						goto IL_0028;
					}
					num = 0;
					continue;
				}
				if (num3 < num4)
				{
					num = 0;
					continue;
				}
				byte* ptr8 = ptr3;
				while (ptr < ptr8)
				{
					num = (int)(*ptr);
					ptr++;
					if (num > 127)
					{
						goto IL_0169;
					}
					*ptr2 = (char)num;
					ptr2++;
				}
				num = 0;
				break;
				IL_0169:
				if (num <= 127)
				{
					goto IL_01EA;
				}
				if ((num & 64) == 0)
				{
					goto IL_00F9;
				}
				if ((num & 32) != 0)
				{
					if ((num & 16) != 0)
					{
						num &= 15;
						if (num > 4)
						{
							num |= 240;
							goto IL_00F9;
						}
						num |= 1347226624;
					}
					else
					{
						num = (num & 15) | 1210220544;
					}
				}
				else
				{
					num &= 31;
					if (num <= 1)
					{
						num |= 192;
						goto IL_00F9;
					}
					num |= 8388608;
				}
			}
			if (num != 0 && (baseDecoder == null || baseDecoder.MustFlush))
			{
				if (decoderFallbackBuffer == null)
				{
					if (baseDecoder == null)
					{
						decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
					}
					else
					{
						decoderFallbackBuffer = baseDecoder.FallbackBuffer;
					}
					decoderFallbackBuffer.InternalInitialize(bytes, ptr4);
				}
				byte* ptr5 = ptr;
				char* ptr6 = ptr2;
				bool flag2 = this.FallbackInvalidByteSequence(ref ptr5, num, decoderFallbackBuffer, ref ptr6);
				ptr = ptr5;
				ptr2 = ptr6;
				if (!flag2)
				{
					decoderFallbackBuffer.InternalReset();
					base.ThrowCharsOverflow(baseDecoder, ptr2 == chars);
				}
				num = 0;
			}
			if (baseDecoder != null)
			{
				((UTF8Encoding.UTF8Decoder)baseDecoder).bits = num;
				baseDecoder._bytesUsed = (int)((long)(ptr - bytes));
			}
			return UTF8Encoding.PtrDiff(ptr2, chars);
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x0008E1F4 File Offset: 0x0008C3F4
		private unsafe bool FallbackInvalidByteSequence(ref byte* pSrc, int ch, DecoderFallbackBuffer fallback, ref char* pTarget)
		{
			byte* ptr = pSrc;
			byte[] bytesUnknown = this.GetBytesUnknown(ref ptr, ch);
			if (!fallback.InternalFallback(bytesUnknown, pSrc, ref pTarget))
			{
				pSrc = ptr;
				return false;
			}
			return true;
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x0008E224 File Offset: 0x0008C424
		private unsafe int FallbackInvalidByteSequence(byte* pSrc, int ch, DecoderFallbackBuffer fallback)
		{
			byte[] bytesUnknown = this.GetBytesUnknown(ref pSrc, ch);
			return fallback.InternalFallback(bytesUnknown, pSrc);
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x0008E244 File Offset: 0x0008C444
		private unsafe byte[] GetBytesUnknown(ref byte* pSrc, int ch)
		{
			byte[] array;
			if (ch < 256 && ch >= 0)
			{
				pSrc--;
				array = new byte[] { (byte)ch };
			}
			else if ((ch & 402653184) == 0)
			{
				pSrc--;
				array = new byte[] { (byte)((ch & 31) | 192) };
			}
			else if ((ch & 268435456) != 0)
			{
				if ((ch & 8388608) != 0)
				{
					pSrc -= 3;
					array = new byte[]
					{
						(byte)(((ch >> 12) & 7) | 240),
						(byte)(((ch >> 6) & 63) | 128),
						(byte)((ch & 63) | 128)
					};
				}
				else if ((ch & 131072) != 0)
				{
					pSrc -= 2;
					array = new byte[]
					{
						(byte)(((ch >> 6) & 7) | 240),
						(byte)((ch & 63) | 128)
					};
				}
				else
				{
					pSrc--;
					array = new byte[] { (byte)((ch & 7) | 240) };
				}
			}
			else if ((ch & 8388608) != 0)
			{
				pSrc -= 2;
				array = new byte[]
				{
					(byte)(((ch >> 6) & 15) | 224),
					(byte)((ch & 63) | 128)
				};
			}
			else
			{
				pSrc--;
				array = new byte[] { (byte)((ch & 15) | 224) };
			}
			return array;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x0008E397 File Offset: 0x0008C597
		public override Decoder GetDecoder()
		{
			return new UTF8Encoding.UTF8Decoder(this);
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x0008E39F File Offset: 0x0008C59F
		public override Encoder GetEncoder()
		{
			return new UTF8Encoding.UTF8Encoder(this);
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x0008E3A8 File Offset: 0x0008C5A8
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", "Non-negative number required.");
			}
			long num = (long)charCount + 1L;
			if (base.EncoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.EncoderFallback.MaxCharCount;
			}
			num *= 3L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", "Too many characters. The resulting number of bytes is larger than what can be returned as an int.");
			}
			return (int)num;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x0008E40C File Offset: 0x0008C60C
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", "Non-negative number required.");
			}
			long num = (long)byteCount + 1L;
			if (base.DecoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.DecoderFallback.MaxCharCount;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("byteCount", "Too many bytes. The resulting number of chars is larger than what can be returned as an int.");
			}
			return (int)num;
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0008E46B File Offset: 0x0008C66B
		public override byte[] GetPreamble()
		{
			if (this._emitUTF8Identifier)
			{
				return new byte[] { 239, 187, 191 };
			}
			return Array.Empty<byte>();
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x0008E48C File Offset: 0x0008C68C
		public override ReadOnlySpan<byte> Preamble
		{
			get
			{
				return (base.GetType() != typeof(UTF8Encoding)) ? this.GetPreamble() : (this._emitUTF8Identifier ? UTF8Encoding.s_preamble : Array.Empty<byte>());
			}
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0008E4C8 File Offset: 0x0008C6C8
		public override bool Equals(object value)
		{
			UTF8Encoding utf8Encoding = value as UTF8Encoding;
			return utf8Encoding != null && (this._emitUTF8Identifier == utf8Encoding._emitUTF8Identifier && base.EncoderFallback.Equals(utf8Encoding.EncoderFallback)) && base.DecoderFallback.Equals(utf8Encoding.DecoderFallback);
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x0008E515 File Offset: 0x0008C715
		public override int GetHashCode()
		{
			return base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode() + 65001 + (this._emitUTF8Identifier ? 1 : 0);
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0008E541 File Offset: 0x0008C741
		// Note: this type is marked as 'beforefieldinit'.
		static UTF8Encoding()
		{
		}

		// Token: 0x04001CC5 RID: 7365
		private const int UTF8_CODEPAGE = 65001;

		// Token: 0x04001CC6 RID: 7366
		internal static readonly UTF8Encoding.UTF8EncodingSealed s_default = new UTF8Encoding.UTF8EncodingSealed(true);

		// Token: 0x04001CC7 RID: 7367
		internal static readonly byte[] s_preamble = new byte[] { 239, 187, 191 };

		// Token: 0x04001CC8 RID: 7368
		internal readonly bool _emitUTF8Identifier;

		// Token: 0x04001CC9 RID: 7369
		private bool _isThrowException;

		// Token: 0x04001CCA RID: 7370
		private const int FinalByte = 536870912;

		// Token: 0x04001CCB RID: 7371
		private const int SupplimentarySeq = 268435456;

		// Token: 0x04001CCC RID: 7372
		private const int ThreeByteSeq = 134217728;

		// Token: 0x02000385 RID: 901
		internal sealed class UTF8EncodingSealed : UTF8Encoding
		{
			// Token: 0x0600270C RID: 9996 RVA: 0x0008E564 File Offset: 0x0008C764
			public UTF8EncodingSealed(bool encoderShouldEmitUTF8Identifier)
				: base(encoderShouldEmitUTF8Identifier)
			{
			}

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x0600270D RID: 9997 RVA: 0x0008E56D File Offset: 0x0008C76D
			public override ReadOnlySpan<byte> Preamble
			{
				get
				{
					return this._emitUTF8Identifier ? UTF8Encoding.s_preamble : Array.Empty<byte>();
				}
			}
		}

		// Token: 0x02000386 RID: 902
		[Serializable]
		private sealed class UTF8Encoder : EncoderNLS
		{
			// Token: 0x0600270E RID: 9998 RVA: 0x0008C6DD File Offset: 0x0008A8DD
			public UTF8Encoder(UTF8Encoding encoding)
				: base(encoding)
			{
			}

			// Token: 0x0600270F RID: 9999 RVA: 0x0008E588 File Offset: 0x0008C788
			public override void Reset()
			{
				this.surrogateChar = 0;
				if (this._fallbackBuffer != null)
				{
					this._fallbackBuffer.Reset();
				}
			}

			// Token: 0x170004C1 RID: 1217
			// (get) Token: 0x06002710 RID: 10000 RVA: 0x0008E5A4 File Offset: 0x0008C7A4
			internal override bool HasState
			{
				get
				{
					return this.surrogateChar != 0;
				}
			}

			// Token: 0x04001CCD RID: 7373
			internal int surrogateChar;
		}

		// Token: 0x02000387 RID: 903
		[Serializable]
		private sealed class UTF8Decoder : DecoderNLS
		{
			// Token: 0x06002711 RID: 10001 RVA: 0x0008BBF3 File Offset: 0x00089DF3
			public UTF8Decoder(UTF8Encoding encoding)
				: base(encoding)
			{
			}

			// Token: 0x06002712 RID: 10002 RVA: 0x0008E5AF File Offset: 0x0008C7AF
			public override void Reset()
			{
				this.bits = 0;
				if (this._fallbackBuffer != null)
				{
					this._fallbackBuffer.Reset();
				}
			}

			// Token: 0x170004C2 RID: 1218
			// (get) Token: 0x06002713 RID: 10003 RVA: 0x0008E5CB File Offset: 0x0008C7CB
			internal override bool HasState
			{
				get
				{
					return this.bits != 0;
				}
			}

			// Token: 0x04001CCE RID: 7374
			internal int bits;
		}
	}
}
