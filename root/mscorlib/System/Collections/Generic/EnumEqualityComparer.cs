using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000B22 RID: 2850
	[Serializable]
	internal class EnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060068B4 RID: 26804 RVA: 0x0016344C File Offset: 0x0016164C
		public override bool Equals(T x, T y)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(x);
			int num2 = JitHelpers.UnsafeEnumCast<T>(y);
			return num == num2;
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x0016346C File Offset: 0x0016166C
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCast<T>(obj).GetHashCode();
		}

		// Token: 0x060068B6 RID: 26806 RVA: 0x001630F1 File Offset: 0x001612F1
		public EnumEqualityComparer()
		{
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x001630F1 File Offset: 0x001612F1
		protected EnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x00163487 File Offset: 0x00161687
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T))) != TypeCode.Int32)
			{
				info.SetType(typeof(ObjectEqualityComparer<T>));
			}
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x001634B1 File Offset: 0x001616B1
		public override bool Equals(object obj)
		{
			return obj is EnumEqualityComparer<T>;
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x00162CCA File Offset: 0x00160ECA
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
