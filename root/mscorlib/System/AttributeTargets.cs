using System;

namespace System
{
	// Token: 0x020000C4 RID: 196
	[Flags]
	public enum AttributeTargets
	{
		// Token: 0x04000ED7 RID: 3799
		Assembly = 1,
		// Token: 0x04000ED8 RID: 3800
		Module = 2,
		// Token: 0x04000ED9 RID: 3801
		Class = 4,
		// Token: 0x04000EDA RID: 3802
		Struct = 8,
		// Token: 0x04000EDB RID: 3803
		Enum = 16,
		// Token: 0x04000EDC RID: 3804
		Constructor = 32,
		// Token: 0x04000EDD RID: 3805
		Method = 64,
		// Token: 0x04000EDE RID: 3806
		Property = 128,
		// Token: 0x04000EDF RID: 3807
		Field = 256,
		// Token: 0x04000EE0 RID: 3808
		Event = 512,
		// Token: 0x04000EE1 RID: 3809
		Interface = 1024,
		// Token: 0x04000EE2 RID: 3810
		Parameter = 2048,
		// Token: 0x04000EE3 RID: 3811
		Delegate = 4096,
		// Token: 0x04000EE4 RID: 3812
		ReturnValue = 8192,
		// Token: 0x04000EE5 RID: 3813
		GenericParameter = 16384,
		// Token: 0x04000EE6 RID: 3814
		All = 32767
	}
}
