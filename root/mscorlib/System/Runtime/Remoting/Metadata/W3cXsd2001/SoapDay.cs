using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005B5 RID: 1461
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDay : ISoapXsd
	{
		// Token: 0x060038DE RID: 14558 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapDay()
		{
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000CAF38 File Offset: 0x000C9138
		public SoapDay(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000CAF47 File Offset: 0x000C9147
		// (set) Token: 0x060038E1 RID: 14561 RVA: 0x000CAF4F File Offset: 0x000C914F
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

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x000CAF58 File Offset: 0x000C9158
		public static string XsdType
		{
			get
			{
				return "gDay";
			}
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000CAF5F File Offset: 0x000C915F
		public string GetXsdType()
		{
			return SoapDay.XsdType;
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000CAF66 File Offset: 0x000C9166
		public static SoapDay Parse(string value)
		{
			return new SoapDay(DateTime.ParseExact(value, SoapDay._datetimeFormats, null, DateTimeStyles.None));
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000CAF7A File Offset: 0x000C917A
		public override string ToString()
		{
			return this._value.ToString("---dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000CAF91 File Offset: 0x000C9191
		// Note: this type is marked as 'beforefieldinit'.
		static SoapDay()
		{
		}

		// Token: 0x040025A9 RID: 9641
		private static readonly string[] _datetimeFormats = new string[] { "---dd", "---ddzzz" };

		// Token: 0x040025AA RID: 9642
		private DateTime _value;
	}
}
