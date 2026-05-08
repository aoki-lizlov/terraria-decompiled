using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000155 RID: 341
	[Serializable]
	internal sealed class OrdinalIgnoreCaseComparer : OrdinalComparer, ISerializable
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003D52E File Offset: 0x0003B72E
		public OrdinalIgnoreCaseComparer()
			: base(true)
		{
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003D537 File Offset: 0x0003B737
		public override int Compare(string x, string y)
		{
			return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003D541 File Offset: 0x0003B741
		public override bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003D54B File Offset: 0x0003B74B
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			return CompareInfo.GetIgnoreCaseHash(obj);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003D55C File Offset: 0x0003B75C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(OrdinalComparer));
			info.AddValue("_ignoreCase", true);
		}
	}
}
