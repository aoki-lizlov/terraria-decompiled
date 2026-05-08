using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E9 RID: 233
	internal class XDocumentWrapper : XContainerWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0002CCED File Offset: 0x0002AEED
		private XDocument Document
		{
			get
			{
				return (XDocument)base.WrappedNode;
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002CCFA File Offset: 0x0002AEFA
		public XDocumentWrapper(XDocument document)
			: base(document)
		{
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0002CD04 File Offset: 0x0002AF04
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				List<IXmlNode> childNodes = base.ChildNodes;
				if (this.Document.Declaration != null && (childNodes.Count == 0 || childNodes[0].NodeType != 17))
				{
					childNodes.Insert(0, new XDeclarationWrapper(this.Document.Declaration));
				}
				return childNodes;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002CD55 File Offset: 0x0002AF55
		protected override bool HasChildNodes
		{
			get
			{
				return base.HasChildNodes || this.Document.Declaration != null;
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002CD6F File Offset: 0x0002AF6F
		public IXmlNode CreateComment(string text)
		{
			return new XObjectWrapper(new XComment(text));
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002CD7C File Offset: 0x0002AF7C
		public IXmlNode CreateTextNode(string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002CD89 File Offset: 0x0002AF89
		public IXmlNode CreateCDataSection(string data)
		{
			return new XObjectWrapper(new XCData(data));
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002CD7C File Offset: 0x0002AF7C
		public IXmlNode CreateWhitespace(string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002CD7C File Offset: 0x0002AF7C
		public IXmlNode CreateSignificantWhitespace(string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002CD96 File Offset: 0x0002AF96
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XDeclarationWrapper(new XDeclaration(version, encoding, standalone));
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002CDA5 File Offset: 0x0002AFA5
		public IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XDocumentTypeWrapper(new XDocumentType(name, publicId, systemId, internalSubset));
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002CDB6 File Offset: 0x0002AFB6
		public IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new XProcessingInstructionWrapper(new XProcessingInstruction(target, data));
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002CDC4 File Offset: 0x0002AFC4
		public IXmlElement CreateElement(string elementName)
		{
			return new XElementWrapper(new XElement(elementName));
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002CDD6 File Offset: 0x0002AFD6
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XElementWrapper(new XElement(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri)));
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002CDEE File Offset: 0x0002AFEE
		public IXmlNode CreateAttribute(string name, string value)
		{
			return new XAttributeWrapper(new XAttribute(name, value));
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002CE01 File Offset: 0x0002B001
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value)
		{
			return new XAttributeWrapper(new XAttribute(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri), value));
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0002CE1A File Offset: 0x0002B01A
		public IXmlElement DocumentElement
		{
			get
			{
				if (this.Document.Root == null)
				{
					return null;
				}
				return new XElementWrapper(this.Document.Root);
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002CE3C File Offset: 0x0002B03C
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			XDeclarationWrapper xdeclarationWrapper = newChild as XDeclarationWrapper;
			if (xdeclarationWrapper != null)
			{
				this.Document.Declaration = xdeclarationWrapper.Declaration;
				return xdeclarationWrapper;
			}
			return base.AppendChild(newChild);
		}
	}
}
