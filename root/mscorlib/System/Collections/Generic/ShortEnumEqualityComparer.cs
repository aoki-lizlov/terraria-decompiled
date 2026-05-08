using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000B24 RID: 2852
	[Serializable]
	internal sealed class ShortEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060068BE RID: 26814 RVA: 0x001634BC File Offset: 0x001616BC
		public ShortEnumEqualityComparer()
		{
		}

		// Token: 0x060068BF RID: 26815 RVA: 0x001634BC File Offset: 0x001616BC
		public ShortEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x001634E0 File Offset: 0x001616E0
		public override int GetHashCode(T obj)
		{
			return ((short)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}
