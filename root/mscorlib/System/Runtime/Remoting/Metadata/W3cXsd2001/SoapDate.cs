using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005B3 RID: 1459
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDate : ISoapXsd
	{
		// Token: 0x060038CD RID: 14541 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapDate()
		{
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000CAD1D File Offset: 0x000C8F1D
		public SoapDate(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000CAD2C File Offset: 0x000C8F2C
		public SoapDate(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000CAD42 File Offset: 0x000C8F42
		// (set) Token: 0x060038D1 RID: 14545 RVA: 0x000CAD4A File Offset: 0x000C8F4A
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000CAD53 File Offset: 0x000C8F53
		// (set) Token: 0x060038D3 RID: 14547 RVA: 0x000CAD5B File Offset: 0x000C8F5B
		public DateTime Value
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

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000CAD64 File Offset: 0x000C8F64
		public static string XsdType
		{
			get
			{
				return "date";
			}
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000CAD6B File Offset: 0x000C8F6B
		public string GetXsdType()
		{
			return SoapDate.XsdType;
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000CAD74 File Offset: 0x000C8F74
		public static SoapDate Parse(string value)
		{
			SoapDate soapDate = new SoapDate(DateTime.ParseExact(value, SoapDate._datetimeFormats, null, DateTimeStyles.None));
			if (value.StartsWith("-"))
			{
				soapDate.Sign = -1;
			}
			else
			{
				soapDate.Sign = 0;
			}
			return soapDate;
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000CADB2 File Offset: 0x000C8FB2
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000CADE8 File Offset: 0x000C8FE8
		// Note: this type is marked as 'beforefieldinit'.
		static SoapDate()
		{
		}

		// Token: 0x040025A5 RID: 9637
		private static readonly string[] _datetimeFormats = new string[] { "yyyy-MM-dd", "'+'yyyy-MM-dd", "'-'yyyy-MM-dd", "yyyy-MM-ddzzz", "'+'yyyy-MM-ddzzz", "'-'yyyy-MM-ddzzz" };

		// Token: 0x040025A6 RID: 9638
		private int _sign;

		// Token: 0x040025A7 RID: 9639
		private DateTime _value;
	}
}
