using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EA RID: 234
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0002CE6D File Offset: 0x0002B06D
		private XText Text
		{
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002CE7A File Offset: 0x0002B07A
		public XTextWrapper(XText text)
			: base(text)
		{
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0002CE83 File Offset: 0x0002B083
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x0002CE90 File Offset: 0x0002B090
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0002CE9E File Offset: 0x0002B09E
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Text.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Text.Parent);
			}
		}
	}
}
