using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000701 RID: 1793
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum FUNCKIND
	{
		// Token: 0x04002AFA RID: 11002
		FUNC_VIRTUAL,
		// Token: 0x04002AFB RID: 11003
		FUNC_PUREVIRTUAL,
		// Token: 0x04002AFC RID: 11004
		FUNC_NONVIRTUAL,
		// Token: 0x04002AFD RID: 11005
		FUNC_STATIC,
		// Token: 0x04002AFE RID: 11006
		FUNC_DISPATCH
	}
}
