using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C8 RID: 1480
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonPositiveInteger : ISoapXsd
	{
		// Token: 0x06003973 RID: 14707 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapNonPositiveInteger()
		{
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000CB947 File Offset: 0x000C9B47
		public SoapNonPositiveInteger(decimal value)
		{
			if (value > 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value.ToString());
			}
			this._value = value;
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x000CB97B File Offset: 0x000C9B7B
		// (set) Token: 0x06003976 RID: 14710 RVA: 0x000CB983 File Offset: 0x000C9B83
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

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06003977 RID: 14711 RVA: 0x000CB98C File Offset: 0x000C9B8C
		public static string XsdType
		{
			get
			{
				return "nonPositiveInteger";
			}
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000CB993 File Offset: 0x000C9B93
		public string GetXsdType()
		{
			return SoapNonPositiveInteger.XsdType;
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000CB99A File Offset: 0x000C9B9A
		public static SoapNonPositiveInteger Parse(string value)
		{
			return new SoapNonPositiveInteger(decimal.Parse(value));
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000CB9A7 File Offset: 0x000C9BA7
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040025BE RID: 9662
		private decimal _value;
	}
}
