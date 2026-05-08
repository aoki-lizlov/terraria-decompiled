using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005D0 RID: 1488
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYearMonth : ISoapXsd
	{
		// Token: 0x060039BE RID: 14782 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapYearMonth()
		{
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000CBE1D File Offset: 0x000CA01D
		public SoapYearMonth(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000CBE2C File Offset: 0x000CA02C
		public SoapYearMonth(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000CBE42 File Offset: 0x000CA042
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000CBE4A File Offset: 0x000CA04A
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

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000CBE53 File Offset: 0x000CA053
		// (set) Token: 0x060039C4 RID: 14788 RVA: 0x000CBE5B File Offset: 0x000CA05B
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

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000CBE64 File Offset: 0x000CA064
		public static string XsdType
		{
			get
			{
				return "gYearMonth";
			}
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000CBE6B File Offset: 0x000CA06B
		public string GetXsdType()
		{
			return SoapYearMonth.XsdType;
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000CBE74 File Offset: 0x000CA074
		public static SoapYearMonth Parse(string value)
		{
			SoapYearMonth soapYearMonth = new SoapYearMonth(DateTime.ParseExact(value, SoapYearMonth._datetimeFormats, null, DateTimeStyles.None));
			if (value.StartsWith("-"))
			{
				soapYearMonth.Sign = -1;
			}
			else
			{
				soapYearMonth.Sign = 0;
			}
			return soapYearMonth;
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000CBEB2 File Offset: 0x000CA0B2
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy-MM", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy-MM", CultureInfo.InvariantCulture);
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000CBEE8 File Offset: 0x000CA0E8
		// Note: this type is marked as 'beforefieldinit'.
		static SoapYearMonth()
		{
		}

		// Token: 0x040025CB RID: 9675
		private static readonly string[] _datetimeFormats = new string[] { "yyyy-MM", "'+'yyyy-MM", "'-'yyyy-MM", "yyyy-MMzzz", "'+'yyyy-MMzzz", "'-'yyyy-MMzzz" };

		// Token: 0x040025CC RID: 9676
		private int _sign;

		// Token: 0x040025CD RID: 9677
		private DateTime _value;
	}
}
