using System;

namespace System.Security.Permissions
{
	// Token: 0x020003FC RID: 1020
	public enum IsolatedStorageContainment
	{
		// Token: 0x04001EAA RID: 7850
		None,
		// Token: 0x04001EAB RID: 7851
		DomainIsolationByUser = 16,
		// Token: 0x04001EAC RID: 7852
		ApplicationIsolationByUser = 21,
		// Token: 0x04001EAD RID: 7853
		AssemblyIsolationByUser = 32,
		// Token: 0x04001EAE RID: 7854
		DomainIsolationByMachine = 48,
		// Token: 0x04001EAF RID: 7855
		AssemblyIsolationByMachine = 64,
		// Token: 0x04001EB0 RID: 7856
		ApplicationIsolationByMachine = 69,
		// Token: 0x04001EB1 RID: 7857
		DomainIsolationByRoamingUser = 80,
		// Token: 0x04001EB2 RID: 7858
		AssemblyIsolationByRoamingUser = 96,
		// Token: 0x04001EB3 RID: 7859
		ApplicationIsolationByRoamingUser = 101,
		// Token: 0x04001EB4 RID: 7860
		AdministerIsolatedStorageByUser = 112,
		// Token: 0x04001EB5 RID: 7861
		UnrestrictedIsolatedStorage = 240
	}
}
