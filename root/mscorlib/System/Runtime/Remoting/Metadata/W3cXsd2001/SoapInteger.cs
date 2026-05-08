using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005BE RID: 1470
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapInteger : ISoapXsd
	{
		// Token: 0x06003921 RID: 14625 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapInteger()
		{
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000CB612 File Offset: 0x000C9812
		public SoapInteger(decimal value)
		{
			this._value = value;
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x000CB621 File Offset: 0x000C9821
		// (set) Token: 0x06003924 RID: 14628 RVA: 0x000CB629 File Offset: 0x000C9829
		public decimal Value
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

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x000CB632 File Offset: 0x000C9832
		public static string XsdType
		{
			get
			{
				return "integer";
			}
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000CB639 File Offset: 0x000C9839
		public string GetXsdType()
		{
			return SoapInteger.XsdType;
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000CB640 File Offset: 0x000C9840
		public static SoapInteger Parse(string value)
		{
			return new SoapInteger(decimal.Parse(value));
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000CB64D File Offset: 0x000C984D
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040025B2 RID: 9650
		private decimal _value;
	}
}
