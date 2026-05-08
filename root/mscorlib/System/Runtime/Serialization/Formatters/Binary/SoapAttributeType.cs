using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000693 RID: 1683
	[Serializable]
	internal enum SoapAttributeType
	{
		// Token: 0x0400296D RID: 10605
		None,
		// Token: 0x0400296E RID: 10606
		SchemaType,
		// Token: 0x0400296F RID: 10607
		Embedded,
		// Token: 0x04002970 RID: 10608
		XmlElement = 4,
		// Token: 0x04002971 RID: 10609
		XmlAttribute = 8
	}
}
