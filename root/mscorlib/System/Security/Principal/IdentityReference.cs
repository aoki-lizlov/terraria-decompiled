using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020004B5 RID: 1205
	[ComVisible(false)]
	public abstract class IdentityReference
	{
		// Token: 0x0600319C RID: 12700 RVA: 0x000025BE File Offset: 0x000007BE
		internal IdentityReference()
		{
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600319D RID: 12701
		public abstract string Value { get; }

		// Token: 0x0600319E RID: 12702
		public abstract override bool Equals(object o);

		// Token: 0x0600319F RID: 12703
		public abstract override int GetHashCode();

		// Token: 0x060031A0 RID: 12704
		public abstract bool IsValidTargetType(Type targetType);

		// Token: 0x060031A1 RID: 12705
		public abstract override string ToString();

		// Token: 0x060031A2 RID: 12706
		public abstract IdentityReference Translate(Type targetType);

		// Token: 0x060031A3 RID: 12707 RVA: 0x000B76D8 File Offset: 0x000B58D8
		public static bool operator ==(IdentityReference left, IdentityReference right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Value == right.Value;
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000B76F8 File Offset: 0x000B58F8
		public static bool operator !=(IdentityReference left, IdentityReference right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.Value != right.Value;
		}
	}
}
