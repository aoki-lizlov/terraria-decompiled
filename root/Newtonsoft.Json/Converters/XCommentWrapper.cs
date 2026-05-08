using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EB RID: 235
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002CEBF File Offset: 0x0002B0BF
		private XComment Text
		{
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002CE7A File Offset: 0x0002B07A
		public XCommentWrapper(XComment text)
			: base(text)
		{
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0002CECC File Offset: 0x0002B0CC
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0002CED9 File Offset: 0x0002B0D9
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

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002CEE7 File Offset: 0x0002B0E7
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
