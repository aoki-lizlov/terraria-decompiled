using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005CD RID: 1485
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapTime : ISoapXsd
	{
		// Token: 0x060039A1 RID: 14753 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapTime()
		{
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000CBB98 File Offset: 0x000C9D98
		public SoapTime(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x000CBBA7 File Offset: 0x000C9DA7
		// (set) Token: 0x060039A4 RID: 14756 RVA: 0x000CBBAF File Offset: 0x000C9DAF
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

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060039A5 RID: 14757 RVA: 0x000CBBB8 File Offset: 0x000C9DB8
		public static string XsdType
		{
			get
			{
				return "time";
			}
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000CBBBF File Offset: 0x000C9DBF
		public string GetXsdType()
		{
			return SoapTime.XsdType;
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000CBBC6 File Offset: 0x000C9DC6
		public static SoapTime Parse(string value)
		{
			return new SoapTime(DateTime.ParseExact(value, SoapTime._datetimeFormats, null, DateTimeStyles.None));
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000CBBDA File Offset: 0x000C9DDA
		public override string ToString()
		{
			return this._value.ToString("HH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000CBBF4 File Offset: 0x000C9DF4
		// Note: this type is marked as 'beforefieldinit'.
		static SoapTime()
		{
		}

		// Token: 0x040025C5 RID: 9669
		private static readonly string[] _datetimeFormats = new string[]
		{
			"HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:sszzz", "HH:mm:ss.fzzz",
			"HH:mm:ss.ffzzz", "HH:mm:ss.fffzzz", "HH:mm:ss.ffffzzz", "HH:mm:ss.fffffzzz", "HH:mm:ss.ffffffzzz", "HH:mm:ss.fffffffzzz", "HH:mm:ssZ", "HH:mm:ss.fZ", "HH:mm:ss.ffZ", "HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ", "HH:mm:ss.fffffZ", "HH:mm:ss.ffffffZ", "HH:mm:ss.fffffffZ"
		};

		// Token: 0x040025C6 RID: 9670
		private DateTime _value;
	}
}
