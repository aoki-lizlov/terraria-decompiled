using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005CB RID: 1483
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapPositiveInteger : ISoapXsd
	{
		// Token: 0x0600398B RID: 14731 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapPositiveInteger()
		{
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000CBA25 File Offset: 0x000C9C25
		public SoapPositiveInteger(decimal value)
		{
			if (value <= 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value.ToString());
			}
			this._value = value;
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000CBA59 File Offset: 0x000C9C59
		// (set) Token: 0x0600398E RID: 14734 RVA: 0x000CBA61 File Offset: 0x000C9C61
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

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000CBA6A File Offset: 0x000C9C6A
		public static string XsdType
		{
			get
			{
				return "positiveInteger";
			}
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000CBA71 File Offset: 0x000C9C71
		public string GetXsdType()
		{
			return SoapPositiveInteger.XsdType;
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000CBA78 File Offset: 0x000C9C78
		public static SoapPositiveInteger Parse(string value)
		{
			return new SoapPositiveInteger(decimal.Parse(value));
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000CBA85 File Offset: 0x000C9C85
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040025C1 RID: 9665
		private decimal _value;
	}
}
