using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EF RID: 239
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002D100 File Offset: 0x0002B300
		private XAttribute Attribute
		{
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002CE7A File Offset: 0x0002B07A
		public XAttributeWrapper(XAttribute attribute)
			: base(attribute)
		{
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002D10D File Offset: 0x0002B30D
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0002D11A File Offset: 0x0002B31A
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002D128 File Offset: 0x0002B328
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0002D13A File Offset: 0x0002B33A
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002D14C File Offset: 0x0002B34C
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Attribute.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Attribute.Parent);
			}
		}
	}
}
