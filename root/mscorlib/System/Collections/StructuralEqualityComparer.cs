using System;

namespace System.Collections
{
	// Token: 0x02000A91 RID: 2705
	[Serializable]
	internal sealed class StructuralEqualityComparer : IEqualityComparer
	{
		// Token: 0x06006305 RID: 25349 RVA: 0x00151D90 File Offset: 0x0014FF90
		public bool Equals(object x, object y)
		{
			if (x == null)
			{
				return y == null;
			}
			IStructuralEquatable structuralEquatable = x as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.Equals(y, this);
			}
			return y != null && x.Equals(y);
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x00151DC8 File Offset: 0x0014FFC8
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			IStructuralEquatable structuralEquatable = obj as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.GetHashCode(this);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x000025BE File Offset: 0x000007BE
		public StructuralEqualityComparer()
		{
		}
	}
}
