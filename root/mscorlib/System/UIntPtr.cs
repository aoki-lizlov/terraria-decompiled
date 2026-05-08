using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200023B RID: 571
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public readonly struct UIntPtr : ISerializable, IEquatable<UIntPtr>
	{
		// Token: 0x06001BD0 RID: 7120 RVA: 0x00069476 File Offset: 0x00067676
		public UIntPtr(ulong value)
		{
			if (value > (ulong)(-1) && UIntPtr.Size < 8)
			{
				throw new OverflowException(Locale.GetText("This isn't a 64bits machine."));
			}
			this._pointer = value;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0006949D File Offset: 0x0006769D
		public UIntPtr(uint value)
		{
			this._pointer = value;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000694A7 File Offset: 0x000676A7
		[CLSCompliant(false)]
		public unsafe UIntPtr(void* value)
		{
			this._pointer = value;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000694B0 File Offset: 0x000676B0
		public override bool Equals(object obj)
		{
			if (obj is UIntPtr)
			{
				UIntPtr uintPtr = (UIntPtr)obj;
				return this._pointer == uintPtr._pointer;
			}
			return false;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x000694DD File Offset: 0x000676DD
		public override int GetHashCode()
		{
			return this._pointer;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000694E6 File Offset: 0x000676E6
		public uint ToUInt32()
		{
			return this._pointer;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000694EF File Offset: 0x000676EF
		public ulong ToUInt64()
		{
			return this._pointer;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000694F8 File Offset: 0x000676F8
		[CLSCompliant(false)]
		public unsafe void* ToPointer()
		{
			return this._pointer;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00069500 File Offset: 0x00067700
		public override string ToString()
		{
			if (UIntPtr.Size >= 8)
			{
				return this._pointer.ToString();
			}
			return this._pointer.ToString();
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00069534 File Offset: 0x00067734
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("pointer", this._pointer);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x00069556 File Offset: 0x00067756
		public static bool operator ==(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer == value2._pointer;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x00069568 File Offset: 0x00067768
		public static bool operator !=(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer != value2._pointer;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0006957D File Offset: 0x0006777D
		public static explicit operator ulong(UIntPtr value)
		{
			return value._pointer;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00069587 File Offset: 0x00067787
		public static explicit operator uint(UIntPtr value)
		{
			return value._pointer;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00069591 File Offset: 0x00067791
		public static explicit operator UIntPtr(ulong value)
		{
			return new UIntPtr(value);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00069599 File Offset: 0x00067799
		[CLSCompliant(false)]
		public unsafe static explicit operator UIntPtr(void* value)
		{
			return new UIntPtr(value);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000695A1 File Offset: 0x000677A1
		[CLSCompliant(false)]
		public unsafe static explicit operator void*(UIntPtr value)
		{
			return value.ToPointer();
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000695AA File Offset: 0x000677AA
		public static explicit operator UIntPtr(uint value)
		{
			return new UIntPtr(value);
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000606F5 File Offset: 0x0005E8F5
		public unsafe static int Size
		{
			get
			{
				return sizeof(void*);
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000695B2 File Offset: 0x000677B2
		public unsafe static UIntPtr Add(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000695C1 File Offset: 0x000677C1
		public unsafe static UIntPtr Subtract(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000695B2 File Offset: 0x000677B2
		public unsafe static UIntPtr operator +(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000695C1 File Offset: 0x000677C1
		public unsafe static UIntPtr operator -(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000695D0 File Offset: 0x000677D0
		bool IEquatable<UIntPtr>.Equals(UIntPtr other)
		{
			return this._pointer == other._pointer;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000695E1 File Offset: 0x000677E1
		// Note: this type is marked as 'beforefieldinit'.
		static UIntPtr()
		{
		}

		// Token: 0x04001888 RID: 6280
		public static readonly UIntPtr Zero = new UIntPtr(0U);

		// Token: 0x04001889 RID: 6281
		private unsafe readonly void* _pointer;
	}
}
