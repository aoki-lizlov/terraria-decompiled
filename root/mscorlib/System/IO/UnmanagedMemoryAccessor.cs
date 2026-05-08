using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000947 RID: 2375
	public class UnmanagedMemoryAccessor : IDisposable
	{
		// Token: 0x06005514 RID: 21780 RVA: 0x0011F46E File Offset: 0x0011D66E
		protected UnmanagedMemoryAccessor()
		{
			this._isOpen = false;
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x0011F47D File Offset: 0x0011D67D
		public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity)
		{
			this.Initialize(buffer, offset, capacity, FileAccess.Read);
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x0011F48F File Offset: 0x0011D68F
		public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity, FileAccess access)
		{
			this.Initialize(buffer, offset, capacity, access);
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x0011F4A4 File Offset: 0x0011D6A4
		protected unsafe void Initialize(SafeBuffer buffer, long offset, long capacity, FileAccess access)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (capacity < 0L)
			{
				throw new ArgumentOutOfRangeException("capacity", "Non-negative number required.");
			}
			if (buffer.ByteLength < (ulong)(offset + capacity))
			{
				throw new ArgumentException("Offset and capacity were greater than the size of the view.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException("The method cannot be called twice on the same instance.");
			}
			byte* ptr = null;
			try
			{
				buffer.AcquirePointer(ref ptr);
				if (ptr + offset + capacity < ptr)
				{
					throw new ArgumentException("The UnmanagedMemoryAccessor capacity and offset would wrap around the high end of the address space.");
				}
			}
			finally
			{
				if (ptr != null)
				{
					buffer.ReleasePointer();
				}
			}
			this._offset = offset;
			this._buffer = buffer;
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
			this._canRead = (this._access & FileAccess.Read) > (FileAccess)0;
			this._canWrite = (this._access & FileAccess.Write) > (FileAccess)0;
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06005518 RID: 21784 RVA: 0x0011F5AC File Offset: 0x0011D7AC
		public long Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06005519 RID: 21785 RVA: 0x0011F5B4 File Offset: 0x0011D7B4
		public bool CanRead
		{
			get
			{
				return this._isOpen && this._canRead;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x0600551A RID: 21786 RVA: 0x0011F5C6 File Offset: 0x0011D7C6
		public bool CanWrite
		{
			get
			{
				return this._isOpen && this._canWrite;
			}
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0011F5D8 File Offset: 0x0011D7D8
		protected virtual void Dispose(bool disposing)
		{
			this._isOpen = false;
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0011F5E1 File Offset: 0x0011D7E1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x0600551D RID: 21789 RVA: 0x0011F5F0 File Offset: 0x0011D7F0
		protected bool IsOpen
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0011F5F8 File Offset: 0x0011D7F8
		public bool ReadBoolean(long position)
		{
			return this.ReadByte(position) > 0;
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x0011F604 File Offset: 0x0011D804
		public unsafe byte ReadByte(long position)
		{
			this.EnsureSafeToRead(position, 1);
			byte* ptr = null;
			byte b;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				b = (ptr + this._offset)[position];
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return b;
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x0011F65C File Offset: 0x0011D85C
		public char ReadChar(long position)
		{
			return (char)this.ReadInt16(position);
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0011F668 File Offset: 0x0011D868
		public unsafe short ReadInt16(long position)
		{
			this.EnsureSafeToRead(position, 2);
			byte* ptr = null;
			short num;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				num = Unsafe.ReadUnaligned<short>((void*)(ptr + this._offset + position));
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num;
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0011F6C4 File Offset: 0x0011D8C4
		public unsafe int ReadInt32(long position)
		{
			this.EnsureSafeToRead(position, 4);
			byte* ptr = null;
			int num;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				num = Unsafe.ReadUnaligned<int>((void*)(ptr + this._offset + position));
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num;
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x0011F720 File Offset: 0x0011D920
		public unsafe long ReadInt64(long position)
		{
			this.EnsureSafeToRead(position, 8);
			byte* ptr = null;
			long num;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				num = Unsafe.ReadUnaligned<long>((void*)(ptr + this._offset + position));
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num;
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x0011F77C File Offset: 0x0011D97C
		public unsafe decimal ReadDecimal(long position)
		{
			this.EnsureSafeToRead(position, 16);
			byte* ptr = null;
			int num;
			int num2;
			int num3;
			int num4;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				num = Unsafe.ReadUnaligned<int>((void*)ptr);
				num2 = Unsafe.ReadUnaligned<int>((void*)(ptr + 4));
				num3 = Unsafe.ReadUnaligned<int>((void*)(ptr + 8));
				num4 = Unsafe.ReadUnaligned<int>((void*)(ptr + 12));
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			if ((num4 & 2130771967) != 0 || (num4 & 16711680) > 1835008)
			{
				throw new ArgumentException("Read an invalid decimal value from the buffer.");
			}
			bool flag = (num4 & int.MinValue) != 0;
			byte b = (byte)(num4 >> 16);
			return new decimal(num, num2, num3, flag, b);
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x0011F840 File Offset: 0x0011DA40
		public float ReadSingle(long position)
		{
			return BitConverter.Int32BitsToSingle(this.ReadInt32(position));
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x0011F84E File Offset: 0x0011DA4E
		public double ReadDouble(long position)
		{
			return BitConverter.Int64BitsToDouble(this.ReadInt64(position));
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x0011F85C File Offset: 0x0011DA5C
		[CLSCompliant(false)]
		public sbyte ReadSByte(long position)
		{
			return (sbyte)this.ReadByte(position);
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x0011F65C File Offset: 0x0011D85C
		[CLSCompliant(false)]
		public ushort ReadUInt16(long position)
		{
			return (ushort)this.ReadInt16(position);
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x0011F866 File Offset: 0x0011DA66
		[CLSCompliant(false)]
		public uint ReadUInt32(long position)
		{
			return (uint)this.ReadInt32(position);
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x0011F86F File Offset: 0x0011DA6F
		[CLSCompliant(false)]
		public ulong ReadUInt64(long position)
		{
			return (ulong)this.ReadInt64(position);
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x0011F878 File Offset: 0x0011DA78
		public void Read<T>(long position, out T structure) where T : struct
		{
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", "Non-negative number required.");
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", "Cannot access a closed accessor.");
			}
			if (!this._canRead)
			{
				throw new NotSupportedException("Accessor does not support reading.");
			}
			uint num = SafeBuffer.SizeOf<T>();
			if (position <= this._capacity - (long)((ulong)num))
			{
				structure = this._buffer.Read<T>((ulong)(this._offset + position));
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", "The position may not be greater or equal to the capacity of the accessor.");
			}
			throw new ArgumentException(SR.Format("There are not enough bytes remaining in the accessor to read at this position.", typeof(T)), "position");
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x0011F928 File Offset: 0x0011DB28
		public int ReadArray<T>(long position, T[] array, int offset, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", "Cannot access a closed accessor.");
			}
			if (!this._canRead)
			{
				throw new NotSupportedException("Accessor does not support reading.");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", "Non-negative number required.");
			}
			uint num = SafeBuffer.AlignedSizeOf<T>();
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", "The position may not be greater or equal to the capacity of the accessor.");
			}
			int num2 = count;
			long num3 = this._capacity - position;
			if (num3 < 0L)
			{
				num2 = 0;
			}
			else
			{
				ulong num4 = (ulong)num * (ulong)((long)count);
				if (num3 < (long)num4)
				{
					num2 = (int)(num3 / (long)((ulong)num));
				}
			}
			this._buffer.ReadArray<T>((ulong)(this._offset + position), array, offset, num2);
			return num2;
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x0011FA21 File Offset: 0x0011DC21
		public void Write(long position, bool value)
		{
			this.Write(position, value ? 1 : 0);
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x0011FA34 File Offset: 0x0011DC34
		public unsafe void Write(long position, byte value)
		{
			this.EnsureSafeToWrite(position, 1);
			byte* ptr = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				(ptr + this._offset)[position] = value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x0011FA8C File Offset: 0x0011DC8C
		public void Write(long position, char value)
		{
			this.Write(position, (short)value);
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x0011FA98 File Offset: 0x0011DC98
		public unsafe void Write(long position, short value)
		{
			this.EnsureSafeToWrite(position, 2);
			byte* ptr = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				Unsafe.WriteUnaligned<short>((void*)(ptr + this._offset + position), value);
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x0011FAF4 File Offset: 0x0011DCF4
		public unsafe void Write(long position, int value)
		{
			this.EnsureSafeToWrite(position, 4);
			byte* ptr = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				Unsafe.WriteUnaligned<int>((void*)(ptr + this._offset + position), value);
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x0011FB50 File Offset: 0x0011DD50
		public unsafe void Write(long position, long value)
		{
			this.EnsureSafeToWrite(position, 8);
			byte* ptr = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				Unsafe.WriteUnaligned<long>((void*)(ptr + this._offset + position), value);
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x0011FBAC File Offset: 0x0011DDAC
		public unsafe void Write(long position, decimal value)
		{
			this.EnsureSafeToWrite(position, 16);
			int* ptr = (int*)(&value);
			int num = *ptr;
			int num2 = ptr[1];
			int num3 = ptr[2];
			int num4 = ptr[3];
			byte* ptr2 = null;
			try
			{
				this._buffer.AcquirePointer(ref ptr2);
				ptr2 += this._offset + position;
				Unsafe.WriteUnaligned<int>((void*)ptr2, num3);
				Unsafe.WriteUnaligned<int>((void*)(ptr2 + 4), num4);
				Unsafe.WriteUnaligned<int>((void*)(ptr2 + 8), num2);
				Unsafe.WriteUnaligned<int>((void*)(ptr2 + 12), num);
			}
			finally
			{
				if (ptr2 != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x0011FC4C File Offset: 0x0011DE4C
		public void Write(long position, float value)
		{
			this.Write(position, BitConverter.SingleToInt32Bits(value));
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x0011FC5B File Offset: 0x0011DE5B
		public void Write(long position, double value)
		{
			this.Write(position, BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x0011FC6A File Offset: 0x0011DE6A
		[CLSCompliant(false)]
		public void Write(long position, sbyte value)
		{
			this.Write(position, (byte)value);
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0011FA8C File Offset: 0x0011DC8C
		[CLSCompliant(false)]
		public void Write(long position, ushort value)
		{
			this.Write(position, (short)value);
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x0011FC75 File Offset: 0x0011DE75
		[CLSCompliant(false)]
		public void Write(long position, uint value)
		{
			this.Write(position, (int)value);
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0011FC7F File Offset: 0x0011DE7F
		[CLSCompliant(false)]
		public void Write(long position, ulong value)
		{
			this.Write(position, (long)value);
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x0011FC8C File Offset: 0x0011DE8C
		public void Write<T>(long position, ref T structure) where T : struct
		{
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", "Non-negative number required.");
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", "Cannot access a closed accessor.");
			}
			if (!this._canWrite)
			{
				throw new NotSupportedException("Accessor does not support writing.");
			}
			uint num = SafeBuffer.SizeOf<T>();
			if (position <= this._capacity - (long)((ulong)num))
			{
				this._buffer.Write<T>((ulong)(this._offset + position), structure);
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", "The position may not be greater or equal to the capacity of the accessor.");
			}
			throw new ArgumentException(SR.Format("There are not enough bytes remaining in the accessor to write at this position.", typeof(T)), "position");
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x0011FD3C File Offset: 0x0011DF3C
		public void WriteArray<T>(long position, T[] array, int offset, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", "Non-negative number required.");
			}
			if (position >= this.Capacity)
			{
				throw new ArgumentOutOfRangeException("position", "The position may not be greater or equal to the capacity of the accessor.");
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", "Cannot access a closed accessor.");
			}
			if (!this._canWrite)
			{
				throw new NotSupportedException("Accessor does not support writing.");
			}
			this._buffer.WriteArray<T>((ulong)(this._offset + position), array, offset, count);
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x0011FE0C File Offset: 0x0011E00C
		private void EnsureSafeToRead(long position, int sizeOfType)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", "Cannot access a closed accessor.");
			}
			if (!this._canRead)
			{
				throw new NotSupportedException("Accessor does not support reading.");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", "Non-negative number required.");
			}
			if (position <= this._capacity - (long)sizeOfType)
			{
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", "The position may not be greater or equal to the capacity of the accessor.");
			}
			throw new ArgumentException("There are not enough bytes remaining in the accessor to read at this position.", "position");
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x0011FE90 File Offset: 0x0011E090
		private void EnsureSafeToWrite(long position, int sizeOfType)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", "Cannot access a closed accessor.");
			}
			if (!this._canWrite)
			{
				throw new NotSupportedException("Accessor does not support writing.");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", "Non-negative number required.");
			}
			if (position <= this._capacity - (long)sizeOfType)
			{
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", "The position may not be greater or equal to the capacity of the accessor.");
			}
			throw new ArgumentException("There are not enough bytes remaining in the accessor to write at this position.", "position");
		}

		// Token: 0x040033E8 RID: 13288
		private SafeBuffer _buffer;

		// Token: 0x040033E9 RID: 13289
		private long _offset;

		// Token: 0x040033EA RID: 13290
		private long _capacity;

		// Token: 0x040033EB RID: 13291
		private FileAccess _access;

		// Token: 0x040033EC RID: 13292
		private bool _isOpen;

		// Token: 0x040033ED RID: 13293
		private bool _canRead;

		// Token: 0x040033EE RID: 13294
		private bool _canWrite;
	}
}
