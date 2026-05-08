using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C1 RID: 1473
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonthDay : ISoapXsd
	{
		// Token: 0x0600393A RID: 14650 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapMonthDay()
		{
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000CB70B File Offset: 0x000C990B
		public SoapMonthDay(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600393C RID: 14652 RVA: 0x000CB71A File Offset: 0x000C991A
		// (set) Token: 0x0600393D RID: 14653 RVA: 0x000CB722 File Offset: 0x000C9922
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

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600393E RID: 14654 RVA: 0x000CB72B File Offset: 0x000C992B
		public static string XsdType
		{
			get
			{
				return "gMonthDay";
			}
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000CB732 File Offset: 0x000C9932
		public string GetXsdType()
		{
			return SoapMonthDay.XsdType;
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000CB739 File Offset: 0x000C9939
		public static SoapMonthDay Parse(string value)
		{
			return new SoapMonthDay(DateTime.ParseExact(value, SoapMonthDay._datetimeFormats, null, DateTimeStyles.None));
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000CB74D File Offset: 0x000C994D
		public override string ToString()
		{
			return this._value.ToString("--MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000CB764 File Offset: 0x000C9964
		// Note: this type is marked as 'beforefieldinit'.
		static SoapMonthDay()
		{
		}

		// Token: 0x040025B6 RID: 9654
		private static readonly string[] _datetimeFormats = new string[] { "--MM-dd", "--MM-ddzzz" };

		// Token: 0x040025B7 RID: 9655
		private DateTime _value;
	}
}
