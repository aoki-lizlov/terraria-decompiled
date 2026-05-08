using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C7 RID: 1479
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonNegativeInteger : ISoapXsd
	{
		// Token: 0x0600396B RID: 14699 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapNonNegativeInteger()
		{
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000CB8DA File Offset: 0x000C9ADA
		public SoapNonNegativeInteger(decimal value)
		{
			if (value < 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value.ToString());
			}
			this._value = value;
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600396D RID: 14701 RVA: 0x000CB90E File Offset: 0x000C9B0E
		// (set) Token: 0x0600396E RID: 14702 RVA: 0x000CB916 File Offset: 0x000C9B16
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

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600396F RID: 14703 RVA: 0x000CB91F File Offset: 0x000C9B1F
		public static string XsdType
		{
			get
			{
				return "nonNegativeInteger";
			}
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000CB926 File Offset: 0x000C9B26
		public string GetXsdType()
		{
			return SoapNonNegativeInteger.XsdType;
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000CB92D File Offset: 0x000C9B2D
		public static SoapNonNegativeInteger Parse(string value)
		{
			return new SoapNonNegativeInteger(decimal.Parse(value));
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000CB93A File Offset: 0x000C9B3A
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040025BD RID: 9661
		private decimal _value;
	}
}
