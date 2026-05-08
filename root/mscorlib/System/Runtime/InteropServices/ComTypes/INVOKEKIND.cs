using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000795 RID: 1941
	[Flags]
	[Serializable]
	public enum INVOKEKIND
	{
		// Token: 0x04002C59 RID: 11353
		INVOKE_FUNC = 1,
		// Token: 0x04002C5A RID: 11354
		INVOKE_PROPERTYGET = 2,
		// Token: 0x04002C5B RID: 11355
		INVOKE_PROPERTYPUT = 4,
		// Token: 0x04002C5C RID: 11356
		INVOKE_PROPERTYPUTREF = 8
	}
}
