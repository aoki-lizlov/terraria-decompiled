using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E8 RID: 232
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x0002CCA9 File Offset: 0x0002AEA9
		public XDocumentTypeWrapper(XDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002CCB9 File Offset: 0x0002AEB9
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0002CCC6 File Offset: 0x0002AEC6
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002CCD3 File Offset: 0x0002AED3
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002C981 File Offset: 0x0002AB81
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x040003B3 RID: 947
		private readonly XDocumentType _documentType;
	}
}
