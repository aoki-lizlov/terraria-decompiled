using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C9 RID: 1481
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNormalizedString : ISoapXsd
	{
		// Token: 0x0600397B RID: 14715 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapNormalizedString()
		{
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000CB9B4 File Offset: 0x000C9BB4
		public SoapNormalizedString(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000CB9C8 File Offset: 0x000C9BC8
		// (set) Token: 0x0600397E RID: 14718 RVA: 0x000CB9D0 File Offset: 0x000C9BD0
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000CB9D9 File Offset: 0x000C9BD9
		public static string XsdType
		{
			get
			{
				return "normalizedString";
			}
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000CB9E0 File Offset: 0x000C9BE0
		public string GetXsdType()
		{
			return SoapNormalizedString.XsdType;
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000CB9E7 File Offset: 0x000C9BE7
		public static SoapNormalizedString Parse(string value)
		{
			return new SoapNormalizedString(value);
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000CB9C8 File Offset: 0x000C9BC8
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025BF RID: 9663
		private string _value;
	}
}
