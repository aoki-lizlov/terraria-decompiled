using System;

namespace System.Text
{
	// Token: 0x02000376 RID: 886
	[Serializable]
	public sealed class EncodingInfo
	{
		// Token: 0x060025FF RID: 9727 RVA: 0x00087C88 File Offset: 0x00085E88
		internal EncodingInfo(int codePage, string name, string displayName)
		{
			this.iCodePage = codePage;
			this.strEncodingName = name;
			this.strDisplayName = displayName;
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x00087CA5 File Offset: 0x00085EA5
		public int CodePage
		{
			get
			{
				return this.iCodePage;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x00087CAD File Offset: 0x00085EAD
		public string Name
		{
			get
			{
				return this.strEncodingName;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x00087CB5 File Offset: 0x00085EB5
		public string DisplayName
		{
			get
			{
				return this.strDisplayName;
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00087CBD File Offset: 0x00085EBD
		public Encoding GetEncoding()
		{
			return Encoding.GetEncoding(this.iCodePage);
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00087CCC File Offset: 0x00085ECC
		public override bool Equals(object value)
		{
			EncodingInfo encodingInfo = value as EncodingInfo;
			return encodingInfo != null && this.CodePage == encodingInfo.CodePage;
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00087CF3 File Offset: 0x00085EF3
		public override int GetHashCode()
		{
			return this.CodePage;
		}

		// Token: 0x04001C8F RID: 7311
		private int iCodePage;

		// Token: 0x04001C90 RID: 7312
		private string strEncodingName;

		// Token: 0x04001C91 RID: 7313
		private string strDisplayName;
	}
}
