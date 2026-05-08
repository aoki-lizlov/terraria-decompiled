using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Mono.Security;

namespace System.IO
{
	// Token: 0x02000978 RID: 2424
	[ComVisible(true)]
	[Serializable]
	public class BinaryWriter : IDisposable, IAsyncDisposable
	{
		// Token: 0x060057EA RID: 22506 RVA: 0x00129BCF File Offset: 0x00127DCF
		protected BinaryWriter()
		{
			this.OutStream = Stream.Null;
			this._buffer = new byte[16];
			this._encoding = new UTF8Encoding(false, true);
			this._encoder = this._encoding.GetEncoder();
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x00129C0D File Offset: 0x00127E0D
		public BinaryWriter(Stream output)
			: this(output, new UTF8Encoding(false, true), false)
		{
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x00129C1E File Offset: 0x00127E1E
		public BinaryWriter(Stream output, Encoding encoding)
			: this(output, encoding, false)
		{
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x00129C2C File Offset: 0x00127E2C
		public BinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!output.CanWrite)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not writable."));
			}
			this.OutStream = output;
			this._buffer = new byte[16];
			this._encoding = encoding;
			this._encoder = this._encoding.GetEncoder();
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x00129CA6 File Offset: 0x00127EA6
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x00129CAF File Offset: 0x00127EAF
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._leaveOpen)
				{
					this.OutStream.Flush();
					return;
				}
				this.OutStream.Close();
			}
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x00129CA6 File Offset: 0x00127EA6
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x00129CD3 File Offset: 0x00127ED3
		public virtual Stream BaseStream
		{
			get
			{
				this.Flush();
				return this.OutStream;
			}
		}

		// Token: 0x060057F2 RID: 22514 RVA: 0x00129CE1 File Offset: 0x00127EE1
		public virtual void Flush()
		{
			this.OutStream.Flush();
		}

		// Token: 0x060057F3 RID: 22515 RVA: 0x00129CEE File Offset: 0x00127EEE
		public virtual long Seek(int offset, SeekOrigin origin)
		{
			return this.OutStream.Seek((long)offset, origin);
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x00129CFE File Offset: 0x00127EFE
		public virtual void Write(ReadOnlySpan<byte> buffer)
		{
			this.Write(buffer.ToArray());
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x00129D0D File Offset: 0x00127F0D
		public virtual void Write(ReadOnlySpan<char> buffer)
		{
			this.Write(buffer.ToArray());
		}

		// Token: 0x060057F6 RID: 22518 RVA: 0x00129D1C File Offset: 0x00127F1C
		public virtual ValueTask DisposeAsync()
		{
			ValueTask valueTask;
			try
			{
				if (base.GetType() == typeof(BinaryWriter))
				{
					if (this._leaveOpen)
					{
						return new ValueTask(this.OutStream.FlushAsync());
					}
					this.OutStream.Close();
				}
				else
				{
					this.Dispose();
				}
				valueTask = default(ValueTask);
			}
			catch (Exception ex)
			{
				valueTask = new ValueTask(Task.FromException(ex));
			}
			return valueTask;
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x00129D98 File Offset: 0x00127F98
		public virtual void Write(bool value)
		{
			this._buffer[0] = (value ? 1 : 0);
			this.OutStream.Write(this._buffer, 0, 1);
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x00129DBD File Offset: 0x00127FBD
		public virtual void Write(byte value)
		{
			this.OutStream.WriteByte(value);
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x00129DCB File Offset: 0x00127FCB
		[CLSCompliant(false)]
		public virtual void Write(sbyte value)
		{
			this.OutStream.WriteByte((byte)value);
		}

		// Token: 0x060057FA RID: 22522 RVA: 0x00129DDA File Offset: 0x00127FDA
		public virtual void Write(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.OutStream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x00129DFA File Offset: 0x00127FFA
		public virtual void Write(byte[] buffer, int index, int count)
		{
			this.OutStream.Write(buffer, index, count);
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x00129E0C File Offset: 0x0012800C
		[SecuritySafeCritical]
		public unsafe virtual void Write(char ch)
		{
			if (char.IsSurrogate(ch))
			{
				throw new ArgumentException(Environment.GetResourceString("Unicode surrogate characters must be written out as pairs together in the same call, not individually. Consider passing in a character array instead."));
			}
			byte[] array;
			byte* ptr;
			if ((array = this._buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			int bytes = this._encoder.GetBytes(&ch, 1, ptr, this._buffer.Length, true);
			array = null;
			this.OutStream.Write(this._buffer, 0, bytes);
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x00129E80 File Offset: 0x00128080
		public virtual void Write(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			byte[] bytes = this._encoding.GetBytes(chars, 0, chars.Length);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x00129EBC File Offset: 0x001280BC
		public virtual void Write(char[] chars, int index, int count)
		{
			byte[] bytes = this._encoding.GetBytes(chars, index, count);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x00129EE8 File Offset: 0x001280E8
		[SecuritySafeCritical]
		public virtual void Write(double value)
		{
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 8);
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x00129EFD File Offset: 0x001280FD
		public virtual void Write(decimal value)
		{
			decimal.GetBytes(in value, this._buffer);
			this.OutStream.Write(this._buffer, 0, 16);
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x00129F20 File Offset: 0x00128120
		public virtual void Write(short value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this._buffer, 0, 2);
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x00129F20 File Offset: 0x00128120
		[CLSCompliant(false)]
		public virtual void Write(ushort value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this._buffer, 0, 2);
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x00129F4C File Offset: 0x0012814C
		public virtual void Write(int value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this._buffer, 0, 4);
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x00129F9C File Offset: 0x0012819C
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this._buffer, 0, 4);
		}

		// Token: 0x06005805 RID: 22533 RVA: 0x00129FEC File Offset: 0x001281EC
		public virtual void Write(long value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this._buffer[4] = (byte)(value >> 32);
			this._buffer[5] = (byte)(value >> 40);
			this._buffer[6] = (byte)(value >> 48);
			this._buffer[7] = (byte)(value >> 56);
			this.OutStream.Write(this._buffer, 0, 8);
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x0012A070 File Offset: 0x00128270
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this._buffer[4] = (byte)(value >> 32);
			this._buffer[5] = (byte)(value >> 40);
			this._buffer[6] = (byte)(value >> 48);
			this._buffer[7] = (byte)(value >> 56);
			this.OutStream.Write(this._buffer, 0, 8);
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x0012A0F4 File Offset: 0x001282F4
		[SecuritySafeCritical]
		public virtual void Write(float value)
		{
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 4);
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x0012A10C File Offset: 0x0012830C
		[SecuritySafeCritical]
		public unsafe virtual void Write(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int byteCount = this._encoding.GetByteCount(value);
			this.Write7BitEncodedInt(byteCount);
			if (this._largeByteBuffer == null)
			{
				this._largeByteBuffer = new byte[256];
				this._maxChars = this._largeByteBuffer.Length / this._encoding.GetMaxByteCount(1);
			}
			if (byteCount <= this._largeByteBuffer.Length)
			{
				this._encoding.GetBytes(value, 0, value.Length, this._largeByteBuffer, 0);
				this.OutStream.Write(this._largeByteBuffer, 0, byteCount);
				return;
			}
			int num = 0;
			int num2;
			for (int i = value.Length; i > 0; i -= num2)
			{
				num2 = ((i > this._maxChars) ? this._maxChars : i);
				if (num < 0 || num2 < 0 || checked(num + num2) > value.Length)
				{
					throw new ArgumentOutOfRangeException("charCount");
				}
				int bytes;
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					byte[] array;
					byte* ptr2;
					if ((array = this._largeByteBuffer) == null || array.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array[0];
					}
					bytes = this._encoder.GetBytes(checked(ptr + num), num2, ptr2, this._largeByteBuffer.Length, num2 == i);
					array = null;
				}
				this.OutStream.Write(this._largeByteBuffer, 0, bytes);
				num += num2;
			}
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x0012A26C File Offset: 0x0012846C
		protected void Write7BitEncodedInt(int value)
		{
			uint num;
			for (num = (uint)value; num >= 128U; num >>= 7)
			{
				this.Write((byte)(num | 128U));
			}
			this.Write((byte)num);
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x0012A29F File Offset: 0x0012849F
		// Note: this type is marked as 'beforefieldinit'.
		static BinaryWriter()
		{
		}

		// Token: 0x040034FD RID: 13565
		public static readonly BinaryWriter Null = new BinaryWriter();

		// Token: 0x040034FE RID: 13566
		protected Stream OutStream;

		// Token: 0x040034FF RID: 13567
		private byte[] _buffer;

		// Token: 0x04003500 RID: 13568
		private Encoding _encoding;

		// Token: 0x04003501 RID: 13569
		private Encoder _encoder;

		// Token: 0x04003502 RID: 13570
		[OptionalField]
		private bool _leaveOpen;

		// Token: 0x04003503 RID: 13571
		[OptionalField]
		private char[] _tmpOneCharBuffer;

		// Token: 0x04003504 RID: 13572
		private byte[] _largeByteBuffer;

		// Token: 0x04003505 RID: 13573
		private int _maxChars;

		// Token: 0x04003506 RID: 13574
		private const int LargeByteBufferSize = 256;
	}
}
