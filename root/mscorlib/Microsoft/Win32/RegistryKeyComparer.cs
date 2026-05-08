using System;
using System.Collections;

namespace Microsoft.Win32
{
	// Token: 0x0200008C RID: 140
	internal class RegistryKeyComparer : IEqualityComparer
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x00015E84 File Offset: 0x00014084
		public bool Equals(object x, object y)
		{
			return RegistryKey.IsEquals((RegistryKey)x, (RegistryKey)y);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00015E98 File Offset: 0x00014098
		public int GetHashCode(object obj)
		{
			string name = ((RegistryKey)obj).Name;
			if (name == null)
			{
				return 0;
			}
			return name.GetHashCode();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000025BE File Offset: 0x000007BE
		public RegistryKeyComparer()
		{
		}
	}
}
