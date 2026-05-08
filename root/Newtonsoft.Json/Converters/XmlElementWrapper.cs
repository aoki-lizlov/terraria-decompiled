using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000DE RID: 222
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x0002C893 File Offset: 0x0002AA93
		public XmlElementWrapper(XmlElement element)
			: base(element)
		{
			this._element = element;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002C8A4 File Offset: 0x0002AAA4
		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002C8CF File Offset: 0x0002AACF
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this._element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0002C8DD File Offset: 0x0002AADD
		public bool IsEmpty
		{
			get
			{
				return this._element.IsEmpty;
			}
		}

		// Token: 0x040003AC RID: 940
		private readonly XmlElement _element;
	}
}
