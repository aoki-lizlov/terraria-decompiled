using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Mono.Security;

namespace System.IO
{
	// Token: 0x02000977 RID: 2423
	[ComVisible(true)]
	public class BinaryReader : IDisposable
	{
		// Token: 0x060057C9 RID: 22473 RVA: 0x00129063 File Offset: 0x00127263
		public BinaryReader(Stream input)
			: this(input, new UTF8Encoding(), false)
		{
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x00129072 File Offset: 0x00127272
		public BinaryReader(Stream input, Encoding encoding)
			: this(input, encoding, false)
		{
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x00129080 File Offset: 0x00127280
		public BinaryReader(Stream input, Encoding encoding, bool leaveOpen)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!input.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not readable."));
			}
			this.m_stream = input;
			this.m_decoder = encoding.GetDecoder();
			this.m_maxCharsSize = encoding.GetMaxCharCount(128);
			int num = encoding.GetMaxByteCount(1);
			if (num < 16)
			{
				num = 16;
			}
			this.m_buffer = new byte[num];
			this.m_2BytesPerChar = encoding is UnicodeEncoding;
			this.m_isMemoryStream = this.m_stream.GetType() == typeof(MemoryStream);
			this.m_leaveOpen = leaveOpen;
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060057CC RID: 22476 RVA: 0x0012913D File Offset: 0x0012733D
		public virtual Stream BaseStream
		{
			get
			{
				return this.m_stream;
			}
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x00129145 File Offset: 0x00127345
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x00129150 File Offset: 0x00127350
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				Stream stream = this.m_stream;
				this.m_stream = null;
				if (stream != null && !this.m_leaveOpen)
				{
					stream.Close();
				}
			}
			this.m_stream = null;
			this.m_buffer = null;
			this.m_decoder = null;
			this.m_charBytes = null;
			this.m_singleChar = null;
			this.m_charBuffer = null;
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x00129145 File Offset: 0x00127345
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x001291AC File Offset: 0x001273AC
		public virtual int PeekChar()
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			if (!this.m_stream.CanSeek)
			{
				return -1;
			}
			long position = this.m_stream.Position;
			int num = this.Read();
			this.m_stream.Position = position;
			return num;
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x001291F3 File Offset: 0x001273F3
		public virtual int Read()
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			return this.InternalReadOneChar();
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x00129208 File Offset: 0x00127408
		public virtual bool ReadBoolean()
		{
			this.FillBuffer(1);
			return this.m_buffer[0] > 0;
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x0012921C File Offset: 0x0012741C
		public virtual byte ReadByte()
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			int num = this.m_stream.ReadByte();
			if (num == -1)
			{
				__Error.EndOfFile();
			}
			return (byte)num;
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x00129240 File Offset: 0x00127440
		[CLSCompliant(false)]
		public virtual sbyte ReadSByte()
		{
			this.FillBuffer(1);
			return (sbyte)this.m_buffer[0];
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x00129252 File Offset: 0x00127452
		public virtual char ReadChar()
		{
			int num = this.Read();
			if (num == -1)
			{
				__Error.EndOfFile();
			}
			return (char)num;
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x00129264 File Offset: 0x00127464
		public virtual short ReadInt16()
		{
			this.FillBuffer(2);
			return (short)((int)this.m_buffer[0] | ((int)this.m_buffer[1] << 8));
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00129281 File Offset: 0x00127481
		[CLSCompliant(false)]
		public virtual ushort ReadUInt16()
		{
			this.FillBuffer(2);
			return (ushort)((int)this.m_buffer[0] | ((int)this.m_buffer[1] << 8));
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x001292A0 File Offset: 0x001274A0
		public virtual int ReadInt32()
		{
			if (this.m_isMemoryStream)
			{
				if (this.m_stream == null)
				{
					__Error.FileNotOpen();
				}
				return (this.m_stream as MemoryStream).InternalReadInt32();
			}
			this.FillBuffer(4);
			return (int)this.m_buffer[0] | ((int)this.m_buffer[1] << 8) | ((int)this.m_buffer[2] << 16) | ((int)this.m_buffer[3] << 24);
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x00129305 File Offset: 0x00127505
		[CLSCompliant(false)]
		public virtual uint ReadUInt32()
		{
			this.FillBuffer(4);
			return (uint)((int)this.m_buffer[0] | ((int)this.m_buffer[1] << 8) | ((int)this.m_buffer[2] << 16) | ((int)this.m_buffer[3] << 24));
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x0012933C File Offset: 0x0012753C
		public virtual long ReadInt64()
		{
			this.FillBuffer(8);
			uint num = (uint)((int)this.m_buffer[0] | ((int)this.m_buffer[1] << 8) | ((int)this.m_buffer[2] << 16) | ((int)this.m_buffer[3] << 24));
			return (long)(((ulong)((int)this.m_buffer[4] | ((int)this.m_buffer[5] << 8) | ((int)this.m_buffer[6] << 16) | ((int)this.m_buffer[7] << 24)) << 32) | (ulong)num);
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x001293B0 File Offset: 0x001275B0
		[CLSCompliant(false)]
		public virtual ulong ReadUInt64()
		{
			this.FillBuffer(8);
			uint num = (uint)((int)this.m_buffer[0] | ((int)this.m_buffer[1] << 8) | ((int)this.m_buffer[2] << 16) | ((int)this.m_buffer[3] << 24));
			return ((ulong)((int)this.m_buffer[4] | ((int)this.m_buffer[5] << 8) | ((int)this.m_buffer[6] << 16) | ((int)this.m_buffer[7] << 24)) << 32) | (ulong)num;
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x00129422 File Offset: 0x00127622
		[SecuritySafeCritical]
		public virtual float ReadSingle()
		{
			this.FillBuffer(4);
			return BitConverterLE.ToSingle(this.m_buffer, 0);
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x00129437 File Offset: 0x00127637
		[SecuritySafeCritical]
		public virtual double ReadDouble()
		{
			this.FillBuffer(8);
			return BitConverterLE.ToDouble(this.m_buffer, 0);
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x0012944C File Offset: 0x0012764C
		public virtual decimal ReadDecimal()
		{
			this.FillBuffer(16);
			decimal num;
			try
			{
				int[] array = new int[4];
				Buffer.BlockCopy(this.m_buffer, 0, array, 0, 16);
				if (!BitConverter.IsLittleEndian)
				{
					for (int i = 0; i < 4; i++)
					{
						array[i] = BinaryPrimitives.ReverseEndianness(array[i]);
					}
				}
				num = new decimal(array);
			}
			catch (ArgumentException ex)
			{
				throw new IOException(Environment.GetResourceString("Decimal byte array constructor requires an array of length four containing valid decimal bytes."), ex);
			}
			return num;
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x001294C4 File Offset: 0x001276C4
		public virtual string ReadString()
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			int num = 0;
			int num2 = this.Read7BitEncodedInt();
			if (num2 < 0)
			{
				throw new IOException(Environment.GetResourceString("BinaryReader encountered an invalid string length of {0} characters.", new object[] { num2 }));
			}
			if (num2 == 0)
			{
				return string.Empty;
			}
			if (this.m_charBytes == null)
			{
				this.m_charBytes = new byte[128];
			}
			if (this.m_charBuffer == null)
			{
				this.m_charBuffer = new char[this.m_maxCharsSize];
			}
			StringBuilder stringBuilder = null;
			int chars;
			for (;;)
			{
				int num3 = ((num2 - num > 128) ? 128 : (num2 - num));
				int num4 = this.m_stream.Read(this.m_charBytes, 0, num3);
				if (num4 == 0)
				{
					__Error.EndOfFile();
				}
				chars = this.m_decoder.GetChars(this.m_charBytes, 0, num4, this.m_charBuffer, 0);
				if (num == 0 && num4 == num2)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = StringBuilderCache.Acquire(num2);
				}
				stringBuilder.Append(this.m_charBuffer, 0, chars);
				num += num4;
				if (num >= num2)
				{
					goto Block_11;
				}
			}
			return new string(this.m_charBuffer, 0, chars);
			Block_11:
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x001295DC File Offset: 0x001277DC
		[SecuritySafeCritical]
		public virtual int Read(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			return this.InternalReadChars(buffer, index, count);
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x00129664 File Offset: 0x00127864
		[SecurityCritical]
		private unsafe int InternalReadChars(char[] buffer, int index, int count)
		{
			int i = count;
			if (this.m_charBytes == null)
			{
				this.m_charBytes = new byte[128];
			}
			while (i > 0)
			{
				int num = i;
				DecoderNLS decoderNLS = this.m_decoder as DecoderNLS;
				if (decoderNLS != null && decoderNLS.HasState && num > 1)
				{
					num--;
				}
				if (this.m_2BytesPerChar)
				{
					num <<= 1;
				}
				if (num > 128)
				{
					num = 128;
				}
				int num2 = 0;
				byte[] array;
				if (this.m_isMemoryStream)
				{
					MemoryStream memoryStream = this.m_stream as MemoryStream;
					num2 = memoryStream.InternalGetPosition();
					num = memoryStream.InternalEmulateRead(num);
					array = memoryStream.InternalGetBuffer();
				}
				else
				{
					num = this.m_stream.Read(this.m_charBytes, 0, num);
					array = this.m_charBytes;
				}
				if (num == 0)
				{
					return count - i;
				}
				int chars;
				checked
				{
					if (num2 < 0 || num < 0 || num2 + num > array.Length)
					{
						throw new ArgumentOutOfRangeException("byteCount");
					}
					if (index < 0 || i < 0 || index + i > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("charsRemaining");
					}
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					fixed (char[] array3 = buffer)
					{
						char* ptr2;
						if (buffer == null || array3.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array3[0];
						}
						chars = this.m_decoder.GetChars(ptr + num2, num, ptr2 + index, i, false);
					}
					array2 = null;
				}
				i -= chars;
				index += chars;
			}
			return count - i;
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x001297D0 File Offset: 0x001279D0
		private int InternalReadOneChar()
		{
			int num = 0;
			long num2 = 0L;
			if (this.m_stream.CanSeek)
			{
				num2 = this.m_stream.Position;
			}
			if (this.m_charBytes == null)
			{
				this.m_charBytes = new byte[128];
			}
			if (this.m_singleChar == null)
			{
				this.m_singleChar = new char[1];
			}
			while (num == 0)
			{
				int num3 = (this.m_2BytesPerChar ? 2 : 1);
				int num4 = this.m_stream.ReadByte();
				this.m_charBytes[0] = (byte)num4;
				if (num4 == -1)
				{
					num3 = 0;
				}
				if (num3 == 2)
				{
					num4 = this.m_stream.ReadByte();
					this.m_charBytes[1] = (byte)num4;
					if (num4 == -1)
					{
						num3 = 1;
					}
				}
				if (num3 == 0)
				{
					return -1;
				}
				try
				{
					num = this.m_decoder.GetChars(this.m_charBytes, 0, num3, this.m_singleChar, 0);
				}
				catch
				{
					if (this.m_stream.CanSeek)
					{
						this.m_stream.Seek(num2 - this.m_stream.Position, SeekOrigin.Current);
					}
					throw;
				}
			}
			if (num == 0)
			{
				return -1;
			}
			return (int)this.m_singleChar[0];
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x001298EC File Offset: 0x00127AEC
		[SecuritySafeCritical]
		public virtual char[] ReadChars(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			if (count == 0)
			{
				return EmptyArray<char>.Value;
			}
			char[] array = new char[count];
			int num = this.InternalReadChars(array, 0, count);
			if (num != count)
			{
				char[] array2 = new char[num];
				Buffer.InternalBlockCopy(array, 0, array2, 0, 2 * num);
				array = array2;
			}
			return array;
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x00129954 File Offset: 0x00127B54
		public virtual int Read(Span<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			int num2;
			try
			{
				int num = this.InternalReadChars(array, 0, buffer.Length);
				if (num > buffer.Length)
				{
					throw new IOException("Stream was too long.");
				}
				new ReadOnlySpan<char>(array, 0, num).CopyTo(buffer);
				num2 = num;
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return num2;
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x001299CC File Offset: 0x00127BCC
		public virtual int Read(Span<byte> buffer)
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			return this.m_stream.Read(buffer);
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x001299E8 File Offset: 0x00127BE8
		public virtual int Read(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			return this.m_stream.Read(buffer, index, count);
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x00129A74 File Offset: 0x00127C74
		public virtual byte[] ReadBytes(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			if (count == 0)
			{
				return EmptyArray<byte>.Value;
			}
			byte[] array = new byte[count];
			int num = 0;
			do
			{
				int num2 = this.m_stream.Read(array, num, count);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				count -= num2;
			}
			while (count > 0);
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.InternalBlockCopy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x00129AF4 File Offset: 0x00127CF4
		protected virtual void FillBuffer(int numBytes)
		{
			if (this.m_buffer != null && (numBytes < 0 || numBytes > this.m_buffer.Length))
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("The number of bytes requested does not fit into BinaryReader's internal buffer."));
			}
			int num = 0;
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			if (numBytes == 1)
			{
				int num2 = this.m_stream.ReadByte();
				if (num2 == -1)
				{
					__Error.EndOfFile();
				}
				this.m_buffer[0] = (byte)num2;
				return;
			}
			do
			{
				int num2 = this.m_stream.Read(this.m_buffer, num, numBytes - num);
				if (num2 == 0)
				{
					__Error.EndOfFile();
				}
				num += num2;
			}
			while (num < numBytes);
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x00129B88 File Offset: 0x00127D88
		protected internal int Read7BitEncodedInt()
		{
			int num = 0;
			int num2 = 0;
			while (num2 != 35)
			{
				byte b = this.ReadByte();
				num |= (int)(b & 127) << num2;
				num2 += 7;
				if ((b & 128) == 0)
				{
					return num;
				}
			}
			throw new FormatException(Environment.GetResourceString("Too many bytes in what should have been a 7 bit encoded Int32."));
		}

		// Token: 0x040034F2 RID: 13554
		private const int MaxCharBytesSize = 128;

		// Token: 0x040034F3 RID: 13555
		private Stream m_stream;

		// Token: 0x040034F4 RID: 13556
		private byte[] m_buffer;

		// Token: 0x040034F5 RID: 13557
		private Decoder m_decoder;

		// Token: 0x040034F6 RID: 13558
		private byte[] m_charBytes;

		// Token: 0x040034F7 RID: 13559
		private char[] m_singleChar;

		// Token: 0x040034F8 RID: 13560
		private char[] m_charBuffer;

		// Token: 0x040034F9 RID: 13561
		private int m_maxCharsSize;

		// Token: 0x040034FA RID: 13562
		private bool m_2BytesPerChar;

		// Token: 0x040034FB RID: 13563
		private bool m_isMemoryStream;

		// Token: 0x040034FC RID: 13564
		private bool m_leaveOpen;
	}
}
