using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008E4 RID: 2276
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AssemblyBuilderAccess
	{
		// Token: 0x04003093 RID: 12435
		Run = 1,
		// Token: 0x04003094 RID: 12436
		Save = 2,
		// Token: 0x04003095 RID: 12437
		RunAndSave = 3,
		// Token: 0x04003096 RID: 12438
		ReflectionOnly = 6,
		// Token: 0x04003097 RID: 12439
		RunAndCollect = 9
	}
}
