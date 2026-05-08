using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E0 RID: 224
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x0002C93D File Offset: 0x0002AB3D
		public XmlDocumentTypeWrapper(XmlDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002C94D File Offset: 0x0002AB4D
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0002C95A File Offset: 0x0002AB5A
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0002C967 File Offset: 0x0002AB67
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002C974 File Offset: 0x0002AB74
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0002C981 File Offset: 0x0002AB81
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x040003AE RID: 942
		private readonly XmlDocumentType _documentType;
	}
}
