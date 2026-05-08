using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F0 RID: 240
	internal class XElementWrapper : XContainerWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0002D16D File Offset: 0x0002B36D
		private XElement Element
		{
			get
			{
				return (XElement)base.WrappedNode;
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002CCFA File Offset: 0x0002AEFA
		public XElementWrapper(XElement element)
			: base(element)
		{
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002D17C File Offset: 0x0002B37C
		public void SetAttributeNode(IXmlNode attribute)
		{
			XObjectWrapper xobjectWrapper = (XObjectWrapper)attribute;
			this.Element.Add(xobjectWrapper.WrappedNode);
			this._attributes = null;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0002D1A8 File Offset: 0x0002B3A8
		public override List<IXmlNode> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (!this.Element.HasAttributes && !this.HasImplicitNamespaceAttribute(this.NamespaceUri))
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>();
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							this._attributes.Add(new XAttributeWrapper(xattribute));
						}
						string namespaceUri = this.NamespaceUri;
						if (this.HasImplicitNamespaceAttribute(namespaceUri))
						{
							this._attributes.Insert(0, new XAttributeWrapper(new XAttribute("xmlns", namespaceUri)));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002D27C File Offset: 0x0002B47C
		private bool HasImplicitNamespaceAttribute(string namespaceUri)
		{
			if (!string.IsNullOrEmpty(namespaceUri))
			{
				IXmlNode parentNode = this.ParentNode;
				if (namespaceUri != ((parentNode != null) ? parentNode.NamespaceUri : null) && string.IsNullOrEmpty(this.GetPrefixOfNamespace(namespaceUri)))
				{
					bool flag = false;
					if (this.Element.HasAttributes)
					{
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							if (xattribute.Name.LocalName == "xmlns" && string.IsNullOrEmpty(xattribute.Name.NamespaceName) && xattribute.Value == namespaceUri)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002D34C File Offset: 0x0002B54C
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			IXmlNode xmlNode = base.AppendChild(newChild);
			this._attributes = null;
			return xmlNode;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0002D35C File Offset: 0x0002B55C
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x0002D369 File Offset: 0x0002B569
		public override string Value
		{
			get
			{
				return this.Element.Value;
			}
			set
			{
				this.Element.Value = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002D377 File Offset: 0x0002B577
		public override string LocalName
		{
			get
			{
				return this.Element.Name.LocalName;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002D389 File Offset: 0x0002B589
		public override string NamespaceUri
		{
			get
			{
				return this.Element.Name.NamespaceName;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002D39B File Offset: 0x0002B59B
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this.Element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002D3AE File Offset: 0x0002B5AE
		public bool IsEmpty
		{
			get
			{
				return this.Element.IsEmpty;
			}
		}

		// Token: 0x040003B6 RID: 950
		private List<IXmlNode> _attributes;
	}
}
