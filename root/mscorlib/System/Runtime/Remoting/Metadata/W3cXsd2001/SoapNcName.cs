using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C3 RID: 1475
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNcName : ISoapXsd
	{
		// Token: 0x0600394B RID: 14667 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapNcName()
		{
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000CB7BC File Offset: 0x000C99BC
		public SoapNcName(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x000CB7D0 File Offset: 0x000C99D0
		// (set) Token: 0x0600394E RID: 14670 RVA: 0x000CB7D8 File Offset: 0x000C99D8
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

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000CB7E1 File Offset: 0x000C99E1
		public static string XsdType
		{
			get
			{
				return "NCName";
			}
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000CB7E8 File Offset: 0x000C99E8
		public string GetXsdType()
		{
			return SoapNcName.XsdType;
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000CB7EF File Offset: 0x000C99EF
		public static SoapNcName Parse(string value)
		{
			return new SoapNcName(value);
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000CB7D0 File Offset: 0x000C99D0
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025B9 RID: 9657
		private string _value;
	}
}
