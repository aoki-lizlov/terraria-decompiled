using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006A0 RID: 1696
	public abstract class SafeBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003FA5 RID: 16293 RVA: 0x000DFFF8 File Offset: 0x000DE1F8
		protected SafeBuffer(bool ownsHandle)
			: base(ownsHandle)
		{
			this._numBytes = SafeBuffer.Uninitialized;
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000E000C File Offset: 0x000DE20C
		[CLSCompliant(false)]
		public void Initialize(ulong numBytes)
		{
			if (IntPtr.Size == 4 && numBytes > (ulong)(-1))
			{
				throw new ArgumentOutOfRangeException("numBytes", "The number of bytes cannot exceed the virtual address space on a 32 bit machine.");
			}
			if (numBytes >= (ulong)SafeBuffer.Uninitialized)
			{
				throw new ArgumentOutOfRangeException("numBytes", "The length of the buffer must be less than the maximum UIntPtr value for your platform.");
			}
			this._numBytes = (UIntPtr)numBytes;
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000E0060 File Offset: 0x000DE260
		[CLSCompliant(false)]
		public void Initialize(uint numElements, uint sizeOfEachElement)
		{
			if (IntPtr.Size == 4 && numElements * sizeOfEachElement > 4294967295U)
			{
				throw new ArgumentOutOfRangeException("numBytes", "The number of bytes cannot exceed the virtual address space on a 32 bit machine.");
			}
			if ((ulong)(numElements * sizeOfEachElement) >= (ulong)SafeBuffer.Uninitialized)
			{
				throw new ArgumentOutOfRangeException("numElements", "The length of the buffer must be less than the maximum UIntPtr value for your platform.");
			}
			this._numBytes = (UIntPtr)(checked(numElements * sizeOfEachElement));
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000E00B9 File Offset: 0x000DE2B9
		[CLSCompliant(false)]
		public void Initialize<T>(uint numElements) where T : struct
		{
			this.Initialize(numElements, SafeBuffer.AlignedSizeOf<T>());
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000E00C8 File Offset: 0x000DE2C8
		[CLSCompliant(false)]
		public unsafe void AcquirePointer(ref byte* pointer)
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			pointer = (IntPtr)((UIntPtr)0);
			bool flag = false;
			base.DangerousAddRef(ref flag);
			pointer = (void*)this.handle;
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x000E0108 File Offset: 0x000DE308
		public void ReleasePointer()
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			base.DangerousRelease();
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000E0128 File Offset: 0x000DE328
		[CLSCompliant(false)]
		public unsafe T Read<T>(ulong byteOffset) where T : struct
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = SafeBuffer.SizeOf<T>();
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			this.SpaceCheck(ptr, (ulong)num);
			T t = default(T);
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
				try
				{
					fixed (byte* ptr2 = Unsafe.As<T, byte>(ref t))
					{
						Buffer.Memmove(ptr2, ptr, num);
					}
				}
				finally
				{
					byte* ptr2 = null;
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return t;
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000E01C0 File Offset: 0x000DE3C0
		[CLSCompliant(false)]
		public unsafe void ReadArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = SafeBuffer.SizeOf<T>();
			uint num2 = SafeBuffer.AlignedSizeOf<T>();
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			bool flag;
			checked
			{
				this.SpaceCheck(ptr, unchecked((ulong)num2) * (ulong)(unchecked((long)count)));
				flag = false;
			}
			try
			{
				base.DangerousAddRef(ref flag);
				if (count > 0)
				{
					try
					{
						fixed (byte* ptr2 = Unsafe.As<T, byte>(ref array[index]))
						{
							byte* ptr3 = ptr2;
							for (int i = 0; i < count; i++)
							{
								Buffer.Memmove(ptr3 + (ulong)num * (ulong)((long)i), ptr + (ulong)num2 * (ulong)((long)i), num);
							}
						}
					}
					finally
					{
						byte* ptr2 = null;
					}
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000E02DC File Offset: 0x000DE4DC
		[CLSCompliant(false)]
		public unsafe void Write<T>(ulong byteOffset, T value) where T : struct
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = SafeBuffer.SizeOf<T>();
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			this.SpaceCheck(ptr, (ulong)num);
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
				try
				{
					fixed (byte* ptr2 = Unsafe.As<T, byte>(ref value))
					{
						byte* ptr3 = ptr2;
						Buffer.Memmove(ptr, ptr3, num);
					}
				}
				finally
				{
					byte* ptr2 = null;
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x000E036C File Offset: 0x000DE56C
		[CLSCompliant(false)]
		public unsafe void WriteArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = SafeBuffer.SizeOf<T>();
			uint num2 = SafeBuffer.AlignedSizeOf<T>();
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			bool flag;
			checked
			{
				this.SpaceCheck(ptr, unchecked((ulong)num2) * (ulong)(unchecked((long)count)));
				flag = false;
			}
			try
			{
				base.DangerousAddRef(ref flag);
				if (count > 0)
				{
					try
					{
						fixed (byte* ptr2 = Unsafe.As<T, byte>(ref array[index]))
						{
							byte* ptr3 = ptr2;
							for (int i = 0; i < count; i++)
							{
								Buffer.Memmove(ptr + (ulong)num2 * (ulong)((long)i), ptr3 + (ulong)num * (ulong)((long)i), num);
							}
						}
					}
					finally
					{
						byte* ptr2 = null;
					}
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06003FAF RID: 16303 RVA: 0x000E0488 File Offset: 0x000DE688
		[CLSCompliant(false)]
		public ulong ByteLength
		{
			get
			{
				if (this._numBytes == SafeBuffer.Uninitialized)
				{
					throw SafeBuffer.NotInitialized();
				}
				return (ulong)this._numBytes;
			}
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x000E04AD File Offset: 0x000DE6AD
		private unsafe void SpaceCheck(byte* ptr, ulong sizeInBytes)
		{
			if ((ulong)this._numBytes < sizeInBytes)
			{
				SafeBuffer.NotEnoughRoom();
			}
			if ((long)((byte*)ptr - (byte*)(void*)this.handle) > (long)((ulong)this._numBytes - sizeInBytes))
			{
				SafeBuffer.NotEnoughRoom();
			}
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x000E04E6 File Offset: 0x000DE6E6
		private static void NotEnoughRoom()
		{
			throw new ArgumentException("Not enough space available in the buffer.");
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x000E04F2 File Offset: 0x000DE6F2
		private static InvalidOperationException NotInitialized()
		{
			return new InvalidOperationException("You must call Initialize on this object instance before using it.");
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x000E0500 File Offset: 0x000DE700
		internal static uint AlignedSizeOf<T>() where T : struct
		{
			uint num = SafeBuffer.SizeOf<T>();
			if (num == 1U || num == 2U)
			{
				return num;
			}
			return (uint)((ulong)(num + 3U) & 18446744073709551612UL);
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x000E0526 File Offset: 0x000DE726
		internal static uint SizeOf<T>() where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				throw new ArgumentException("The specified Type must be a struct containing no references.");
			}
			return (uint)Unsafe.SizeOf<T>();
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x000E053F File Offset: 0x000DE73F
		// Note: this type is marked as 'beforefieldinit'.
		static SafeBuffer()
		{
		}

		// Token: 0x04002997 RID: 10647
		private static readonly UIntPtr Uninitialized = ((UIntPtr.Size == 4) ? ((UIntPtr)uint.MaxValue) : ((UIntPtr)ulong.MaxValue));

		// Token: 0x04002998 RID: 10648
		private UIntPtr _numBytes;
	}
}
