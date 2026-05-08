using System;
using System.Collections.Generic;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E6 RID: 230
	internal interface IXmlNode
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000B3B RID: 2875
		XmlNodeType NodeType { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B3C RID: 2876
		string LocalName { get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B3D RID: 2877
		List<IXmlNode> ChildNodes { get; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B3E RID: 2878
		List<IXmlNode> Attributes { get; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B3F RID: 2879
		IXmlNode ParentNode { get; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000B40 RID: 2880
		// (set) Token: 0x06000B41 RID: 2881
		string Value { get; set; }

		// Token: 0x06000B42 RID: 2882
		IXmlNode AppendChild(IXmlNode newChild);

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000B43 RID: 2883
		string NamespaceUri { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000B44 RID: 2884
		object WrappedNode { get; }
	}
}
