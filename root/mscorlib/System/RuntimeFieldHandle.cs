using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000220 RID: 544
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeFieldHandle : ISerializable
	{
		// Token: 0x06001AC7 RID: 6855 RVA: 0x00064F58 File Offset: 0x00063158
		internal RuntimeFieldHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00064F64 File Offset: 0x00063164
		private RuntimeFieldHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeFieldInfo runtimeFieldInfo = (RuntimeFieldInfo)info.GetValue("FieldObj", typeof(RuntimeFieldInfo));
			this.value = runtimeFieldInfo.FieldHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Insufficient state.");
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x00064FCB File Offset: 0x000631CB
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00064FD3 File Offset: 0x000631D3
		internal bool IsNullHandle()
		{
			return this.value == IntPtr.Zero;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00064FE8 File Offset: 0x000631E8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Object fields may not be properly initialized");
			}
			info.AddValue("FieldObj", (RuntimeFieldInfo)FieldInfo.GetFieldFromHandle(this), typeof(RuntimeFieldInfo));
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00065048 File Offset: 0x00063248
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeFieldHandle)obj).Value;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00065090 File Offset: 0x00063290
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeFieldHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000650A4 File Offset: 0x000632A4
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000650B1 File Offset: 0x000632B1
		public static bool operator ==(RuntimeFieldHandle left, RuntimeFieldHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000650BB File Offset: 0x000632BB
		public static bool operator !=(RuntimeFieldHandle left, RuntimeFieldHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001AD1 RID: 6865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000650C8 File Offset: 0x000632C8
		internal static void SetValue(RuntimeFieldInfo field, object obj, object value, RuntimeType fieldType, FieldAttributes fieldAttr, RuntimeType declaringType, ref bool domainInitialized)
		{
			RuntimeFieldHandle.SetValueInternal(field, obj, value);
		}

		// Token: 0x06001AD3 RID: 6867
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern object GetValueDirect(RuntimeFieldInfo field, RuntimeType fieldType, void* pTypedRef, RuntimeType contextType);

		// Token: 0x06001AD4 RID: 6868
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetValueDirect(RuntimeFieldInfo field, RuntimeType fieldType, void* pTypedRef, object value, RuntimeType contextType);

		// Token: 0x0400165D RID: 5725
		private IntPtr value;
	}
}
