using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200070F RID: 1807
	[ComVisible(true)]
	public struct GCHandle
	{
		// Token: 0x060040B7 RID: 16567 RVA: 0x000E181B File Offset: 0x000DFA1B
		private GCHandle(IntPtr h)
		{
			this.handle = h;
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x000E1824 File Offset: 0x000DFA24
		private GCHandle(object obj)
		{
			this = new GCHandle(obj, GCHandleType.Normal);
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x000E182E File Offset: 0x000DFA2E
		internal GCHandle(object value, GCHandleType type)
		{
			if (type < GCHandleType.Weak || type > GCHandleType.Pinned)
			{
				type = GCHandleType.Normal;
			}
			this.handle = GCHandle.GetTargetHandle(value, IntPtr.Zero, type);
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x000E184D File Offset: 0x000DFA4D
		public bool IsAllocated
		{
			get
			{
				return this.handle != IntPtr.Zero;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x000E185F File Offset: 0x000DFA5F
		// (set) Token: 0x060040BC RID: 16572 RVA: 0x000E187F File Offset: 0x000DFA7F
		public object Target
		{
			get
			{
				if (!this.IsAllocated)
				{
					throw new InvalidOperationException("Handle is not allocated");
				}
				return GCHandle.GetTarget(this.handle);
			}
			set
			{
				this.handle = GCHandle.GetTargetHandle(value, this.handle, (GCHandleType)(-1));
			}
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x000E1894 File Offset: 0x000DFA94
		public IntPtr AddrOfPinnedObject()
		{
			IntPtr addrOfPinnedObject = GCHandle.GetAddrOfPinnedObject(this.handle);
			if (addrOfPinnedObject == (IntPtr)(-1))
			{
				throw new ArgumentException("Object contains non-primitive or non-blittable data.");
			}
			if (addrOfPinnedObject == (IntPtr)(-2))
			{
				throw new InvalidOperationException("Handle is not pinned.");
			}
			return addrOfPinnedObject;
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x000E18D4 File Offset: 0x000DFAD4
		public static GCHandle Alloc(object value)
		{
			return new GCHandle(value);
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x000E18DC File Offset: 0x000DFADC
		public static GCHandle Alloc(object value, GCHandleType type)
		{
			return new GCHandle(value, type);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x000E18E8 File Offset: 0x000DFAE8
		public void Free()
		{
			IntPtr intPtr = this.handle;
			if (intPtr != IntPtr.Zero && Interlocked.CompareExchange(ref this.handle, IntPtr.Zero, intPtr) == intPtr)
			{
				GCHandle.FreeHandle(intPtr);
				return;
			}
			throw new InvalidOperationException("Handle is not initialized.");
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x000E1933 File Offset: 0x000DFB33
		public static explicit operator IntPtr(GCHandle value)
		{
			return value.handle;
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x000E193B File Offset: 0x000DFB3B
		public static explicit operator GCHandle(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				throw new InvalidOperationException("GCHandle value cannot be zero");
			}
			if (!GCHandle.CheckCurrentDomain(value))
			{
				throw new ArgumentException("GCHandle value belongs to a different domain");
			}
			return new GCHandle(value);
		}

		// Token: 0x060040C3 RID: 16579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckCurrentDomain(IntPtr handle);

		// Token: 0x060040C4 RID: 16580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetTarget(IntPtr handle);

		// Token: 0x060040C5 RID: 16581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTargetHandle(object obj, IntPtr handle, GCHandleType type);

		// Token: 0x060040C6 RID: 16582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeHandle(IntPtr handle);

		// Token: 0x060040C7 RID: 16583
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetAddrOfPinnedObject(IntPtr handle);

		// Token: 0x060040C8 RID: 16584 RVA: 0x000E196E File Offset: 0x000DFB6E
		public static bool operator ==(GCHandle a, GCHandle b)
		{
			return a.handle == b.handle;
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x000E1981 File Offset: 0x000DFB81
		public static bool operator !=(GCHandle a, GCHandle b)
		{
			return !(a == b);
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x000E198D File Offset: 0x000DFB8D
		public override bool Equals(object o)
		{
			return o is GCHandle && this == (GCHandle)o;
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x000E19AA File Offset: 0x000DFBAA
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x000E19B7 File Offset: 0x000DFBB7
		public static GCHandle FromIntPtr(IntPtr value)
		{
			return (GCHandle)value;
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x000E19BF File Offset: 0x000DFBBF
		public static IntPtr ToIntPtr(GCHandle value)
		{
			return (IntPtr)value;
		}

		// Token: 0x04002B43 RID: 11075
		private IntPtr handle;
	}
}
