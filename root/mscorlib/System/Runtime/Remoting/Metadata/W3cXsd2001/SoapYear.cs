using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005CF RID: 1487
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYear : ISoapXsd
	{
		// Token: 0x060039B2 RID: 14770 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapYear()
		{
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000CBD17 File Offset: 0x000C9F17
		public SoapYear(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000CBD26 File Offset: 0x000C9F26
		public SoapYear(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x000CBD3C File Offset: 0x000C9F3C
		// (set) Token: 0x060039B6 RID: 14774 RVA: 0x000CBD44 File Offset: 0x000C9F44
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

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060039B7 RID: 14775 RVA: 0x000CBD4D File Offset: 0x000C9F4D
		// (set) Token: 0x060039B8 RID: 14776 RVA: 0x000CBD55 File Offset: 0x000C9F55
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

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060039B9 RID: 14777 RVA: 0x000CBD5E File Offset: 0x000C9F5E
		public static string XsdType
		{
			get
			{
				return "gYear";
			}
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000CBD65 File Offset: 0x000C9F65
		public string GetXsdType()
		{
			return SoapYear.XsdType;
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000CBD6C File Offset: 0x000C9F6C
		public static SoapYear Parse(string value)
		{
			SoapYear soapYear = new SoapYear(DateTime.ParseExact(value, SoapYear._datetimeFormats, null, DateTimeStyles.None));
			if (value.StartsWith("-"))
			{
				soapYear.Sign = -1;
			}
			else
			{
				soapYear.Sign = 0;
			}
			return soapYear;
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000CBDAA File Offset: 0x000C9FAA
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy", CultureInfo.InvariantCulture);
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000CBDE0 File Offset: 0x000C9FE0
		// Note: this type is marked as 'beforefieldinit'.
		static SoapYear()
		{
		}

		// Token: 0x040025C8 RID: 9672
		private static readonly string[] _datetimeFormats = new string[] { "yyyy", "'+'yyyy", "'-'yyyy", "yyyyzzz", "'+'yyyyzzz", "'-'yyyyzzz" };

		// Token: 0x040025C9 RID: 9673
		private int _sign;

		// Token: 0x040025CA RID: 9674
		private DateTime _value;
	}
}
