using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000658 RID: 1624
	internal enum InternalParseTypeE
	{
		// Token: 0x0400276D RID: 10093
		Empty,
		// Token: 0x0400276E RID: 10094
		SerializedStreamHeader,
		// Token: 0x0400276F RID: 10095
		Object,
		// Token: 0x04002770 RID: 10096
		Member,
		// Token: 0x04002771 RID: 10097
		ObjectEnd,
		// Token: 0x04002772 RID: 10098
		MemberEnd,
		// Token: 0x04002773 RID: 10099
		Headers,
		// Token: 0x04002774 RID: 10100
		HeadersEnd,
		// Token: 0x04002775 RID: 10101
		SerializedStreamHeaderEnd,
		// Token: 0x04002776 RID: 10102
		Envelope,
		// Token: 0x04002777 RID: 10103
		EnvelopeEnd,
		// Token: 0x04002778 RID: 10104
		Body,
		// Token: 0x04002779 RID: 10105
		BodyEnd
	}
}
