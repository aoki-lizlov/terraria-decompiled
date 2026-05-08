using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E3 RID: 227
	internal interface IXmlDeclaration : IXmlNode
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000B2F RID: 2863
		string Version { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000B30 RID: 2864
		// (set) Token: 0x06000B31 RID: 2865
		string Encoding { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000B32 RID: 2866
		// (set) Token: 0x06000B33 RID: 2867
		string Standalone { get; set; }
	}
}
