using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000ED RID: 237
	internal class XContainerWrapper : XObjectWrapper
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002CF3D File Offset: 0x0002B13D
		private XContainer Container
		{
			get
			{
				return (XContainer)base.WrappedNode;
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002CE7A File Offset: 0x0002B07A
		public XContainerWrapper(XContainer container)
			: base(container)
		{
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002CF4C File Offset: 0x0002B14C
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				if (this._childNodes == null)
				{
					if (!this.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>();
						foreach (XNode xnode in this.Container.Nodes())
						{
							this._childNodes.Add(XContainerWrapper.WrapNode(xnode));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002CFD8 File Offset: 0x0002B1D8
		protected virtual bool HasChildNodes
		{
			get
			{
				return this.Container.LastNode != null;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002CFE8 File Offset: 0x0002B1E8
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Container.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Container.Parent);
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002D00C File Offset: 0x0002B20C
		internal static IXmlNode WrapNode(XObject node)
		{
			XDocument xdocument = node as XDocument;
			if (xdocument != null)
			{
				return new XDocumentWrapper(xdocument);
			}
			XElement xelement = node as XElement;
			if (xelement != null)
			{
				return new XElementWrapper(xelement);
			}
			XContainer xcontainer = node as XContainer;
			if (xcontainer != null)
			{
				return new XContainerWrapper(xcontainer);
			}
			XProcessingInstruction xprocessingInstruction = node as XProcessingInstruction;
			if (xprocessingInstruction != null)
			{
				return new XProcessingInstructionWrapper(xprocessingInstruction);
			}
			XText xtext = node as XText;
			if (xtext != null)
			{
				return new XTextWrapper(xtext);
			}
			XComment xcomment = node as XComment;
			if (xcomment != null)
			{
				return new XCommentWrapper(xcomment);
			}
			XAttribute xattribute = node as XAttribute;
			if (xattribute != null)
			{
				return new XAttributeWrapper(xattribute);
			}
			XDocumentType xdocumentType = node as XDocumentType;
			if (xdocumentType != null)
			{
				return new XDocumentTypeWrapper(xdocumentType);
			}
			return new XObjectWrapper(node);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002D0B3 File Offset: 0x0002B2B3
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			this.Container.Add(newChild.WrappedNode);
			this._childNodes = null;
			return newChild;
		}

		// Token: 0x040003B4 RID: 948
		private List<IXmlNode> _childNodes;
	}
}
