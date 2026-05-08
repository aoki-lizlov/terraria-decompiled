using System;

namespace System.Reflection
{
	// Token: 0x020008B5 RID: 2229
	[Serializable]
	internal enum MetadataTokenType
	{
		// Token: 0x04002F65 RID: 12133
		Module,
		// Token: 0x04002F66 RID: 12134
		TypeRef = 16777216,
		// Token: 0x04002F67 RID: 12135
		TypeDef = 33554432,
		// Token: 0x04002F68 RID: 12136
		FieldDef = 67108864,
		// Token: 0x04002F69 RID: 12137
		MethodDef = 100663296,
		// Token: 0x04002F6A RID: 12138
		ParamDef = 134217728,
		// Token: 0x04002F6B RID: 12139
		InterfaceImpl = 150994944,
		// Token: 0x04002F6C RID: 12140
		MemberRef = 167772160,
		// Token: 0x04002F6D RID: 12141
		CustomAttribute = 201326592,
		// Token: 0x04002F6E RID: 12142
		Permission = 234881024,
		// Token: 0x04002F6F RID: 12143
		Signature = 285212672,
		// Token: 0x04002F70 RID: 12144
		Event = 335544320,
		// Token: 0x04002F71 RID: 12145
		Property = 385875968,
		// Token: 0x04002F72 RID: 12146
		ModuleRef = 436207616,
		// Token: 0x04002F73 RID: 12147
		TypeSpec = 452984832,
		// Token: 0x04002F74 RID: 12148
		Assembly = 536870912,
		// Token: 0x04002F75 RID: 12149
		AssemblyRef = 587202560,
		// Token: 0x04002F76 RID: 12150
		File = 637534208,
		// Token: 0x04002F77 RID: 12151
		ExportedType = 654311424,
		// Token: 0x04002F78 RID: 12152
		ManifestResource = 671088640,
		// Token: 0x04002F79 RID: 12153
		GenericPar = 704643072,
		// Token: 0x04002F7A RID: 12154
		MethodSpec = 721420288,
		// Token: 0x04002F7B RID: 12155
		String = 1879048192,
		// Token: 0x04002F7C RID: 12156
		Name = 1895825408,
		// Token: 0x04002F7D RID: 12157
		BaseType = 1912602624,
		// Token: 0x04002F7E RID: 12158
		Invalid = 2147483647
	}
}
