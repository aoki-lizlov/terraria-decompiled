using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E5 RID: 229
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x06000B38 RID: 2872
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x06000B39 RID: 2873
		string GetPrefixOfNamespace(string namespaceUri);

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B3A RID: 2874
		bool IsEmpty { get; }
	}
}
