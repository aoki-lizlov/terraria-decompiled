using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200020F RID: 527
	[ComVisible(true)]
	[Serializable]
	public readonly struct IntPtr : ISerializable, IEquatable<IntPtr>
	{
		// Token: 0x060019D3 RID: 6611 RVA: 0x000606B4 File Offset: 0x0005E8B4
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public IntPtr(int value)
		{
			this.m_value = value;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000606BE File Offset: 0x0005E8BE
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public IntPtr(long value)
		{
			this.m_value = value;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000606C8 File Offset: 0x0005E8C8
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe IntPtr(void* value)
		{
			this.m_value = value;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000606D4 File Offset: 0x0005E8D4
		private IntPtr(SerializationInfo info, StreamingContext context)
		{
			long @int = info.GetInt64("value");
			this.m_value = @int;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x000606F5 File Offset: 0x0005E8F5
		public unsafe static int Size
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return sizeof(void*);
			}
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000606FD File Offset: 0x0005E8FD
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("value", this.ToInt64());
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0006071E File Offset: 0x0005E91E
		public override bool Equals(object obj)
		{
			return obj is IntPtr && ((IntPtr)obj).m_value == this.m_value;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0006073D File Offset: 0x0005E93D
		public override int GetHashCode()
		{
			return this.m_value;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0006073D File Offset: 0x0005E93D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public int ToInt32()
		{
			return this.m_value;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00060746 File Offset: 0x0005E946
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public long ToInt64()
		{
			if (IntPtr.Size == 4)
			{
				return (long)this.m_value;
			}
			return this.m_value;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00060760 File Offset: 0x0005E960
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe void* ToPointer()
		{
			return this.m_value;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00060768 File Offset: 0x0005E968
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00060774 File Offset: 0x0005E974
		public string ToString(string format)
		{
			if (IntPtr.Size == 4)
			{
				return this.m_value.ToString(format, null);
			}
			return this.m_value.ToString(format, null);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000607AC File Offset: 0x0005E9AC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool operator ==(IntPtr value1, IntPtr value2)
		{
			return value1.m_value == value2.m_value;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000607BE File Offset: 0x0005E9BE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool operator !=(IntPtr value1, IntPtr value2)
		{
			return value1.m_value != value2.m_value;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000607D3 File Offset: 0x0005E9D3
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static explicit operator IntPtr(int value)
		{
			return new IntPtr(value);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000607DB File Offset: 0x0005E9DB
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static explicit operator IntPtr(long value)
		{
			return new IntPtr(value);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000607E3 File Offset: 0x0005E9E3
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[CLSCompliant(false)]
		public unsafe static explicit operator IntPtr(void* value)
		{
			return new IntPtr(value);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000607EB File Offset: 0x0005E9EB
		public static explicit operator int(IntPtr value)
		{
			return value.m_value;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000607F5 File Offset: 0x0005E9F5
		public static explicit operator long(IntPtr value)
		{
			return value.ToInt64();
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000607FE File Offset: 0x0005E9FE
		[CLSCompliant(false)]
		public unsafe static explicit operator void*(IntPtr value)
		{
			return value.m_value;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00060807 File Offset: 0x0005EA07
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr Add(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00060816 File Offset: 0x0005EA16
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr Subtract(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00060807 File Offset: 0x0005EA07
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr operator +(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00060816 File Offset: 0x0005EA16
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr operator -(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00060825 File Offset: 0x0005EA25
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal bool IsNull()
		{
			return this.m_value == null;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00060831 File Offset: 0x0005EA31
		bool IEquatable<IntPtr>.Equals(IntPtr other)
		{
			return this.m_value == other.m_value;
		}

		// Token: 0x04001601 RID: 5633
		private unsafe readonly void* m_value;

		// Token: 0x04001602 RID: 5634
		public static readonly IntPtr Zero;
	}
}
