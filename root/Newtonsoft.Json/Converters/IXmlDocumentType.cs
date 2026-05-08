using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E4 RID: 228
	internal interface IXmlDocumentType : IXmlNode
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000B34 RID: 2868
		string Name { get; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B35 RID: 2869
		string System { get; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000B36 RID: 2870
		string Public { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B37 RID: 2871
		string InternalSubset { get; }
	}
}
