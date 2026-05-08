using System;
using System.Collections.Generic;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E1 RID: 225
	internal class XmlNodeWrapper : IXmlNode
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x0002C988 File Offset: 0x0002AB88
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002C997 File Offset: 0x0002AB97
		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002C99F File Offset: 0x0002AB9F
		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0002C9AC File Offset: 0x0002ABAC
		public virtual string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0002C9BC File Offset: 0x0002ABBC
		public List<IXmlNode> ChildNodes
		{
			get
			{
				if (this._childNodes == null)
				{
					if (!this._node.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>(this._node.ChildNodes.Count);
						foreach (object obj in this._node.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							this._childNodes.Add(XmlNodeWrapper.WrapNode(xmlNode));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002CA6C File Offset: 0x0002AC6C
		protected virtual bool HasChildNodes
		{
			get
			{
				return this._node.HasChildNodes;
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002CA7C File Offset: 0x0002AC7C
		internal static IXmlNode WrapNode(XmlNode node)
		{
			XmlNodeType nodeType = node.NodeType;
			if (nodeType == 1)
			{
				return new XmlElementWrapper((XmlElement)node);
			}
			if (nodeType == 10)
			{
				return new XmlDocumentTypeWrapper((XmlDocumentType)node);
			}
			if (nodeType != 17)
			{
				return new XmlNodeWrapper(node);
			}
			return new XmlDeclarationWrapper((XmlDeclaration)node);
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002CACC File Offset: 0x0002ACCC
		public List<IXmlNode> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (!this.HasAttributes)
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>(this._node.Attributes.Count);
						foreach (object obj in this._node.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							this._attributes.Add(XmlNodeWrapper.WrapNode(xmlAttribute));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002CB74 File Offset: 0x0002AD74
		private bool HasAttributes
		{
			get
			{
				XmlElement xmlElement = this._node as XmlElement;
				if (xmlElement != null)
				{
					return xmlElement.HasAttributes;
				}
				XmlAttributeCollection attributes = this._node.Attributes;
				return attributes != null && attributes.Count > 0;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		public IXmlNode ParentNode
		{
			get
			{
				XmlAttribute xmlAttribute = this._node as XmlAttribute;
				XmlNode xmlNode = ((xmlAttribute != null) ? xmlAttribute.OwnerElement : this._node.ParentNode);
				if (xmlNode == null)
				{
					return null;
				}
				return XmlNodeWrapper.WrapNode(xmlNode);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002CBEB File Offset: 0x0002ADEB
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		public string Value
		{
			get
			{
				return this._node.Value;
			}
			set
			{
				this._node.Value = value;
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			this._childNodes = null;
			this._attributes = null;
			return newChild;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002CC3D File Offset: 0x0002AE3D
		public string NamespaceUri
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		// Token: 0x040003AF RID: 943
		private readonly XmlNode _node;

		// Token: 0x040003B0 RID: 944
		private List<IXmlNode> _childNodes;

		// Token: 0x040003B1 RID: 945
		private List<IXmlNode> _attributes;
	}
}
