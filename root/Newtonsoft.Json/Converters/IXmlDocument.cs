using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E2 RID: 226
	internal interface IXmlDocument : IXmlNode
	{
		// Token: 0x06000B22 RID: 2850
		IXmlNode CreateComment(string text);

		// Token: 0x06000B23 RID: 2851
		IXmlNode CreateTextNode(string text);

		// Token: 0x06000B24 RID: 2852
		IXmlNode CreateCDataSection(string data);

		// Token: 0x06000B25 RID: 2853
		IXmlNode CreateWhitespace(string text);

		// Token: 0x06000B26 RID: 2854
		IXmlNode CreateSignificantWhitespace(string text);

		// Token: 0x06000B27 RID: 2855
		IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone);

		// Token: 0x06000B28 RID: 2856
		IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset);

		// Token: 0x06000B29 RID: 2857
		IXmlNode CreateProcessingInstruction(string target, string data);

		// Token: 0x06000B2A RID: 2858
		IXmlElement CreateElement(string elementName);

		// Token: 0x06000B2B RID: 2859
		IXmlElement CreateElement(string qualifiedName, string namespaceUri);

		// Token: 0x06000B2C RID: 2860
		IXmlNode CreateAttribute(string name, string value);

		// Token: 0x06000B2D RID: 2861
		IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value);

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000B2E RID: 2862
		IXmlElement DocumentElement { get; }
	}
}
