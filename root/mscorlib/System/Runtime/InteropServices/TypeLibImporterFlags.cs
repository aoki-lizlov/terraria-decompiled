using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000724 RID: 1828
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeLibImporterFlags
	{
		// Token: 0x04002B91 RID: 11153
		PrimaryInteropAssembly = 1,
		// Token: 0x04002B92 RID: 11154
		UnsafeInterfaces = 2,
		// Token: 0x04002B93 RID: 11155
		SafeArrayAsSystemArray = 4,
		// Token: 0x04002B94 RID: 11156
		TransformDispRetVals = 8,
		// Token: 0x04002B95 RID: 11157
		None = 0,
		// Token: 0x04002B96 RID: 11158
		PreventClassMembers = 16,
		// Token: 0x04002B97 RID: 11159
		ImportAsAgnostic = 2048,
		// Token: 0x04002B98 RID: 11160
		ImportAsItanium = 1024,
		// Token: 0x04002B99 RID: 11161
		ImportAsX64 = 512,
		// Token: 0x04002B9A RID: 11162
		ImportAsX86 = 256,
		// Token: 0x04002B9B RID: 11163
		ReflectionOnlyLoading = 4096,
		// Token: 0x04002B9C RID: 11164
		SerializableValueClasses = 32,
		// Token: 0x04002B9D RID: 11165
		NoDefineVersionResource = 8192
	}
}
