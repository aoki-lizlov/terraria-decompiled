using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EE RID: 238
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x0002D0CE File Offset: 0x0002B2CE
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002D0DD File Offset: 0x0002B2DD
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002D0E5 File Offset: 0x0002B2E5
		public virtual XmlNodeType NodeType
		{
			get
			{
				return this._xmlObject.NodeType;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00024290 File Offset: 0x00022490
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002D0F2 File Offset: 0x0002B2F2
		public virtual List<IXmlNode> ChildNodes
		{
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0002D0F2 File Offset: 0x0002B2F2
		public virtual List<IXmlNode> Attributes
		{
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00024290 File Offset: 0x00022490
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00024290 File Offset: 0x00022490
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x0002D0F9 File Offset: 0x0002B2F9
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002D0F9 File Offset: 0x0002B2F9
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00024290 File Offset: 0x00022490
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040003B5 RID: 949
		private readonly XObject _xmlObject;
	}
}
