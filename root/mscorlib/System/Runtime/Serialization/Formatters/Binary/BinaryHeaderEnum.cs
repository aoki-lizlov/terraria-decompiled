using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000654 RID: 1620
	internal enum BinaryHeaderEnum
	{
		// Token: 0x04002742 RID: 10050
		SerializedStreamHeader,
		// Token: 0x04002743 RID: 10051
		Object,
		// Token: 0x04002744 RID: 10052
		ObjectWithMap,
		// Token: 0x04002745 RID: 10053
		ObjectWithMapAssemId,
		// Token: 0x04002746 RID: 10054
		ObjectWithMapTyped,
		// Token: 0x04002747 RID: 10055
		ObjectWithMapTypedAssemId,
		// Token: 0x04002748 RID: 10056
		ObjectString,
		// Token: 0x04002749 RID: 10057
		Array,
		// Token: 0x0400274A RID: 10058
		MemberPrimitiveTyped,
		// Token: 0x0400274B RID: 10059
		MemberReference,
		// Token: 0x0400274C RID: 10060
		ObjectNull,
		// Token: 0x0400274D RID: 10061
		MessageEnd,
		// Token: 0x0400274E RID: 10062
		Assembly,
		// Token: 0x0400274F RID: 10063
		ObjectNullMultiple256,
		// Token: 0x04002750 RID: 10064
		ObjectNullMultiple,
		// Token: 0x04002751 RID: 10065
		ArraySinglePrimitive,
		// Token: 0x04002752 RID: 10066
		ArraySingleObject,
		// Token: 0x04002753 RID: 10067
		ArraySingleString,
		// Token: 0x04002754 RID: 10068
		CrossAppDomainMap,
		// Token: 0x04002755 RID: 10069
		CrossAppDomainString,
		// Token: 0x04002756 RID: 10070
		CrossAppDomainAssembly,
		// Token: 0x04002757 RID: 10071
		MethodCall,
		// Token: 0x04002758 RID: 10072
		MethodReturn
	}
}
