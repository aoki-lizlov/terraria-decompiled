using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000B23 RID: 2851
	[Serializable]
	internal sealed class SByteEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060068BB RID: 26811 RVA: 0x001634BC File Offset: 0x001616BC
		public SByteEnumEqualityComparer()
		{
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x001634BC File Offset: 0x001616BC
		public SByteEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x001634C4 File Offset: 0x001616C4
		public override int GetHashCode(T obj)
		{
			return ((sbyte)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}
