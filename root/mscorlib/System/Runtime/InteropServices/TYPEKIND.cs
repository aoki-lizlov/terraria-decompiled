using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006F1 RID: 1777
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum TYPEKIND
	{
		// Token: 0x04002A90 RID: 10896
		TKIND_ENUM,
		// Token: 0x04002A91 RID: 10897
		TKIND_RECORD,
		// Token: 0x04002A92 RID: 10898
		TKIND_MODULE,
		// Token: 0x04002A93 RID: 10899
		TKIND_INTERFACE,
		// Token: 0x04002A94 RID: 10900
		TKIND_DISPATCH,
		// Token: 0x04002A95 RID: 10901
		TKIND_COCLASS,
		// Token: 0x04002A96 RID: 10902
		TKIND_ALIAS,
		// Token: 0x04002A97 RID: 10903
		TKIND_UNION,
		// Token: 0x04002A98 RID: 10904
		TKIND_MAX
	}
}
