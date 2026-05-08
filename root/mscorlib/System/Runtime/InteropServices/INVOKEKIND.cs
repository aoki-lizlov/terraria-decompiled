using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000702 RID: 1794
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.INVOKEKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum INVOKEKIND
	{
		// Token: 0x04002B00 RID: 11008
		INVOKE_FUNC = 1,
		// Token: 0x04002B01 RID: 11009
		INVOKE_PROPERTYGET,
		// Token: 0x04002B02 RID: 11010
		INVOKE_PROPERTYPUT = 4,
		// Token: 0x04002B03 RID: 11011
		INVOKE_PROPERTYPUTREF = 8
	}
}
