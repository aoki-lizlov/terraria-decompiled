using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000B25 RID: 2853
	[Serializable]
	internal sealed class LongEnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060068C1 RID: 26817 RVA: 0x001634FC File Offset: 0x001616FC
		public override bool Equals(T x, T y)
		{
			long num = JitHelpers.UnsafeEnumCastLong<T>(x);
			long num2 = JitHelpers.UnsafeEnumCastLong<T>(y);
			return num == num2;
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x0016351C File Offset: 0x0016171C
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCastLong<T>(obj).GetHashCode();
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x00163537 File Offset: 0x00161737
		public override bool Equals(object obj)
		{
			return obj is LongEnumEqualityComparer<T>;
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x00162CCA File Offset: 0x00160ECA
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x001630F1 File Offset: 0x001612F1
		public LongEnumEqualityComparer()
		{
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x001630F1 File Offset: 0x001612F1
		public LongEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060068C7 RID: 26823 RVA: 0x00163542 File Offset: 0x00161742
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ObjectEqualityComparer<T>));
		}
	}
}
