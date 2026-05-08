using System;

namespace System.Reflection
{
	// Token: 0x0200089D RID: 2205
	[Flags]
	public enum TypeAttributes
	{
		// Token: 0x04002EB5 RID: 11957
		VisibilityMask = 7,
		// Token: 0x04002EB6 RID: 11958
		NotPublic = 0,
		// Token: 0x04002EB7 RID: 11959
		Public = 1,
		// Token: 0x04002EB8 RID: 11960
		NestedPublic = 2,
		// Token: 0x04002EB9 RID: 11961
		NestedPrivate = 3,
		// Token: 0x04002EBA RID: 11962
		NestedFamily = 4,
		// Token: 0x04002EBB RID: 11963
		NestedAssembly = 5,
		// Token: 0x04002EBC RID: 11964
		NestedFamANDAssem = 6,
		// Token: 0x04002EBD RID: 11965
		NestedFamORAssem = 7,
		// Token: 0x04002EBE RID: 11966
		LayoutMask = 24,
		// Token: 0x04002EBF RID: 11967
		AutoLayout = 0,
		// Token: 0x04002EC0 RID: 11968
		SequentialLayout = 8,
		// Token: 0x04002EC1 RID: 11969
		ExplicitLayout = 16,
		// Token: 0x04002EC2 RID: 11970
		ClassSemanticsMask = 32,
		// Token: 0x04002EC3 RID: 11971
		Class = 0,
		// Token: 0x04002EC4 RID: 11972
		Interface = 32,
		// Token: 0x04002EC5 RID: 11973
		Abstract = 128,
		// Token: 0x04002EC6 RID: 11974
		Sealed = 256,
		// Token: 0x04002EC7 RID: 11975
		SpecialName = 1024,
		// Token: 0x04002EC8 RID: 11976
		Import = 4096,
		// Token: 0x04002EC9 RID: 11977
		Serializable = 8192,
		// Token: 0x04002ECA RID: 11978
		WindowsRuntime = 16384,
		// Token: 0x04002ECB RID: 11979
		StringFormatMask = 196608,
		// Token: 0x04002ECC RID: 11980
		AnsiClass = 0,
		// Token: 0x04002ECD RID: 11981
		UnicodeClass = 65536,
		// Token: 0x04002ECE RID: 11982
		AutoClass = 131072,
		// Token: 0x04002ECF RID: 11983
		CustomFormatClass = 196608,
		// Token: 0x04002ED0 RID: 11984
		CustomFormatMask = 12582912,
		// Token: 0x04002ED1 RID: 11985
		BeforeFieldInit = 1048576,
		// Token: 0x04002ED2 RID: 11986
		RTSpecialName = 2048,
		// Token: 0x04002ED3 RID: 11987
		HasSecurity = 262144,
		// Token: 0x04002ED4 RID: 11988
		ReservedMask = 264192
	}
}
