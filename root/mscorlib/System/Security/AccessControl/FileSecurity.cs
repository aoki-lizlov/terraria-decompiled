using System;
using System.Runtime.InteropServices;

namespace System.Security.AccessControl
{
	// Token: 0x020004F5 RID: 1269
	public sealed class FileSecurity : FileSystemSecurity
	{
		// Token: 0x060033B8 RID: 13240 RVA: 0x000BE398 File Offset: 0x000BC598
		public FileSecurity()
			: base(false)
		{
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000BE3A1 File Offset: 0x000BC5A1
		public FileSecurity(string fileName, AccessControlSections includeSections)
			: base(false, fileName, includeSections)
		{
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000BE3AC File Offset: 0x000BC5AC
		internal FileSecurity(SafeHandle handle, AccessControlSections includeSections)
			: base(false, handle, includeSections)
		{
		}
	}
}
