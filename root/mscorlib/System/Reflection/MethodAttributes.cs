using System;

namespace System.Reflection
{
	// Token: 0x0200087C RID: 2172
	[Flags]
	public enum MethodAttributes
	{
		// Token: 0x04002E45 RID: 11845
		MemberAccessMask = 7,
		// Token: 0x04002E46 RID: 11846
		PrivateScope = 0,
		// Token: 0x04002E47 RID: 11847
		Private = 1,
		// Token: 0x04002E48 RID: 11848
		FamANDAssem = 2,
		// Token: 0x04002E49 RID: 11849
		Assembly = 3,
		// Token: 0x04002E4A RID: 11850
		Family = 4,
		// Token: 0x04002E4B RID: 11851
		FamORAssem = 5,
		// Token: 0x04002E4C RID: 11852
		Public = 6,
		// Token: 0x04002E4D RID: 11853
		Static = 16,
		// Token: 0x04002E4E RID: 11854
		Final = 32,
		// Token: 0x04002E4F RID: 11855
		Virtual = 64,
		// Token: 0x04002E50 RID: 11856
		HideBySig = 128,
		// Token: 0x04002E51 RID: 11857
		CheckAccessOnOverride = 512,
		// Token: 0x04002E52 RID: 11858
		VtableLayoutMask = 256,
		// Token: 0x04002E53 RID: 11859
		ReuseSlot = 0,
		// Token: 0x04002E54 RID: 11860
		NewSlot = 256,
		// Token: 0x04002E55 RID: 11861
		Abstract = 1024,
		// Token: 0x04002E56 RID: 11862
		SpecialName = 2048,
		// Token: 0x04002E57 RID: 11863
		PinvokeImpl = 8192,
		// Token: 0x04002E58 RID: 11864
		UnmanagedExport = 8,
		// Token: 0x04002E59 RID: 11865
		RTSpecialName = 4096,
		// Token: 0x04002E5A RID: 11866
		HasSecurity = 16384,
		// Token: 0x04002E5B RID: 11867
		RequireSecObject = 32768,
		// Token: 0x04002E5C RID: 11868
		ReservedMask = 53248
	}
}
