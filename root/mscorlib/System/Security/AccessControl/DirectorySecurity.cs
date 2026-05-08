using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004EF RID: 1263
	public sealed class DirectorySecurity : FileSystemSecurity
	{
		// Token: 0x0600338B RID: 13195 RVA: 0x000BE094 File Offset: 0x000BC294
		public DirectorySecurity()
			: base(true)
		{
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000BE09D File Offset: 0x000BC29D
		public DirectorySecurity(string name, AccessControlSections includeSections)
			: base(true, name, includeSections)
		{
		}
	}
}
