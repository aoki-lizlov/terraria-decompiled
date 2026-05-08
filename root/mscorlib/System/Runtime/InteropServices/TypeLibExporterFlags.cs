using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000723 RID: 1827
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeLibExporterFlags
	{
		// Token: 0x04002B8A RID: 11146
		OnlyReferenceRegistered = 1,
		// Token: 0x04002B8B RID: 11147
		None = 0,
		// Token: 0x04002B8C RID: 11148
		CallerResolvedReferences = 2,
		// Token: 0x04002B8D RID: 11149
		OldNames = 4,
		// Token: 0x04002B8E RID: 11150
		ExportAs32Bit = 16,
		// Token: 0x04002B8F RID: 11151
		ExportAs64Bit = 32
	}
}
