using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	internal sealed class OrdinalCaseSensitiveComparer : OrdinalComparer, ISerializable
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x0003D4E4 File Offset: 0x0003B6E4
		public OrdinalCaseSensitiveComparer()
			: base(false)
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003D4ED File Offset: 0x0003B6ED
		public override int Compare(string x, string y)
		{
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003D4F6 File Offset: 0x0003B6F6
		public override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003D4FF File Offset: 0x0003B6FF
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003D510 File Offset: 0x0003B710
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(OrdinalComparer));
			info.AddValue("_ignoreCase", false);
		}
	}
}
