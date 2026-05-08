using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200021D RID: 541
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Serializable]
	public class Object
	{
		// Token: 0x06001ABB RID: 6843 RVA: 0x0001FDBA File Offset: 0x0001DFBA
		public virtual bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00064F2C File Offset: 0x0006312C
		public static bool Equals(object objA, object objB)
		{
			return objA == objB || (objA != null && objB != null && objA.Equals(objB));
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00004088 File Offset: 0x00002288
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Object()
		{
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00004088 File Offset: 0x00002288
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Finalize()
		{
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00064F43 File Offset: 0x00063143
		public virtual int GetHashCode()
		{
			return object.InternalGetHashCode(this);
		}

		// Token: 0x06001AC0 RID: 6848
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Type GetType();

		// Token: 0x06001AC1 RID: 6849
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected extern object MemberwiseClone();

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00064F4B File Offset: 0x0006314B
		public virtual string ToString()
		{
			return this.GetType().ToString();
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0001FDBA File Offset: 0x0001DFBA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool ReferenceEquals(object objA, object objB)
		{
			return objA == objB;
		}

		// Token: 0x06001AC4 RID: 6852
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o);

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00004088 File Offset: 0x00002288
		private void FieldGetter(string typeName, string fieldName, ref object val)
		{
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00004088 File Offset: 0x00002288
		private void FieldSetter(string typeName, string fieldName, object val)
		{
		}
	}
}
