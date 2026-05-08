using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004E8 RID: 1256
	[Flags]
	public enum ControlFlags
	{
		// Token: 0x040023E6 RID: 9190
		None = 0,
		// Token: 0x040023E7 RID: 9191
		OwnerDefaulted = 1,
		// Token: 0x040023E8 RID: 9192
		GroupDefaulted = 2,
		// Token: 0x040023E9 RID: 9193
		DiscretionaryAclPresent = 4,
		// Token: 0x040023EA RID: 9194
		DiscretionaryAclDefaulted = 8,
		// Token: 0x040023EB RID: 9195
		SystemAclPresent = 16,
		// Token: 0x040023EC RID: 9196
		SystemAclDefaulted = 32,
		// Token: 0x040023ED RID: 9197
		DiscretionaryAclUntrusted = 64,
		// Token: 0x040023EE RID: 9198
		ServerSecurity = 128,
		// Token: 0x040023EF RID: 9199
		DiscretionaryAclAutoInheritRequired = 256,
		// Token: 0x040023F0 RID: 9200
		SystemAclAutoInheritRequired = 512,
		// Token: 0x040023F1 RID: 9201
		DiscretionaryAclAutoInherited = 1024,
		// Token: 0x040023F2 RID: 9202
		SystemAclAutoInherited = 2048,
		// Token: 0x040023F3 RID: 9203
		DiscretionaryAclProtected = 4096,
		// Token: 0x040023F4 RID: 9204
		SystemAclProtected = 8192,
		// Token: 0x040023F5 RID: 9205
		RMControlValid = 16384,
		// Token: 0x040023F6 RID: 9206
		SelfRelative = 32768
	}
}
