using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C4 RID: 1476
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNegativeInteger : ISoapXsd
	{
		// Token: 0x06003953 RID: 14675 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapNegativeInteger()
		{
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000CB7F7 File Offset: 0x000C99F7
		public SoapNegativeInteger(decimal value)
		{
			if (value >= 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value.ToString());
			}
			this._value = value;
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000CB82B File Offset: 0x000C9A2B
		// (set) Token: 0x06003956 RID: 14678 RVA: 0x000CB833 File Offset: 0x000C9A33
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

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000CB83C File Offset: 0x000C9A3C
		public static string XsdType
		{
			get
			{
				return "negativeInteger";
			}
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000CB843 File Offset: 0x000C9A43
		public string GetXsdType()
		{
			return SoapNegativeInteger.XsdType;
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000CB84A File Offset: 0x000C9A4A
		public static SoapNegativeInteger Parse(string value)
		{
			return new SoapNegativeInteger(decimal.Parse(value));
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000CB857 File Offset: 0x000C9A57
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040025BA RID: 9658
		private decimal _value;
	}
}
