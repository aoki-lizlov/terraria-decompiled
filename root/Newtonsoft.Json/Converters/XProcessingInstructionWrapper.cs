using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EC RID: 236
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0002CF08 File Offset: 0x0002B108
		private XProcessingInstruction ProcessingInstruction
		{
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002CE7A File Offset: 0x0002B07A
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction)
			: base(processingInstruction)
		{
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0002CF15 File Offset: 0x0002B115
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002CF22 File Offset: 0x0002B122
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x0002CF2F File Offset: 0x0002B12F
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value;
			}
		}
	}
}
