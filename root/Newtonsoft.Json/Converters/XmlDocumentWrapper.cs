using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000DD RID: 221
	internal class XmlDocumentWrapper : XmlNodeWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002C768 File Offset: 0x0002A968
		public XmlDocumentWrapper(XmlDocument document)
			: base(document)
		{
			this._document = document;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002C778 File Offset: 0x0002A978
		public IXmlNode CreateComment(string data)
		{
			return new XmlNodeWrapper(this._document.CreateComment(data));
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002C78B File Offset: 0x0002A98B
		public IXmlNode CreateTextNode(string text)
		{
			return new XmlNodeWrapper(this._document.CreateTextNode(text));
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002C79E File Offset: 0x0002A99E
		public IXmlNode CreateCDataSection(string data)
		{
			return new XmlNodeWrapper(this._document.CreateCDataSection(data));
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002C7B1 File Offset: 0x0002A9B1
		public IXmlNode CreateWhitespace(string text)
		{
			return new XmlNodeWrapper(this._document.CreateWhitespace(text));
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
		public IXmlNode CreateSignificantWhitespace(string text)
		{
			return new XmlNodeWrapper(this._document.CreateSignificantWhitespace(text));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002C7D7 File Offset: 0x0002A9D7
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XmlDeclarationWrapper(this._document.CreateXmlDeclaration(version, encoding, standalone));
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002C7EC File Offset: 0x0002A9EC
		public IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XmlDocumentTypeWrapper(this._document.CreateDocumentType(name, publicId, systemId, null));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002C802 File Offset: 0x0002AA02
		public IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new XmlNodeWrapper(this._document.CreateProcessingInstruction(target, data));
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002C816 File Offset: 0x0002AA16
		public IXmlElement CreateElement(string elementName)
		{
			return new XmlElementWrapper(this._document.CreateElement(elementName));
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002C829 File Offset: 0x0002AA29
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XmlElementWrapper(this._document.CreateElement(qualifiedName, namespaceUri));
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002C83D File Offset: 0x0002AA3D
		public IXmlNode CreateAttribute(string name, string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(name))
			{
				Value = value
			};
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002C857 File Offset: 0x0002AA57
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(qualifiedName, namespaceUri))
			{
				Value = value
			};
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0002C872 File Offset: 0x0002AA72
		public IXmlElement DocumentElement
		{
			get
			{
				if (this._document.DocumentElement == null)
				{
					return null;
				}
				return new XmlElementWrapper(this._document.DocumentElement);
			}
		}

		// Token: 0x040003AB RID: 939
		private readonly XmlDocument _document;
	}
}
