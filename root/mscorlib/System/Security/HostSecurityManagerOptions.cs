using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020003B0 RID: 944
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostSecurityManagerOptions
	{
		// Token: 0x04001D83 RID: 7555
		None = 0,
		// Token: 0x04001D84 RID: 7556
		HostAppDomainEvidence = 1,
		// Token: 0x04001D85 RID: 7557
		HostPolicyLevel = 2,
		// Token: 0x04001D86 RID: 7558
		HostAssemblyEvidence = 4,
		// Token: 0x04001D87 RID: 7559
		HostDetermineApplicationTrust = 8,
		// Token: 0x04001D88 RID: 7560
		HostResolvePolicy = 16,
		// Token: 0x04001D89 RID: 7561
		AllFlags = 31
	}
}
