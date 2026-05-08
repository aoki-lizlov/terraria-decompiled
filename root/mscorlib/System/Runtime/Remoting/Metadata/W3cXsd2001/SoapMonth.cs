using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C0 RID: 1472
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonth : ISoapXsd
	{
		// Token: 0x06003931 RID: 14641 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapMonth()
		{
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000CB695 File Offset: 0x000C9895
		public SoapMonth(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06003933 RID: 14643 RVA: 0x000CB6A4 File Offset: 0x000C98A4
		// (set) Token: 0x06003934 RID: 14644 RVA: 0x000CB6AC File Offset: 0x000C98AC
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

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x000CB6B5 File Offset: 0x000C98B5
		public static string XsdType
		{
			get
			{
				return "gMonth";
			}
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000CB6BC File Offset: 0x000C98BC
		public string GetXsdType()
		{
			return SoapMonth.XsdType;
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000CB6C3 File Offset: 0x000C98C3
		public static SoapMonth Parse(string value)
		{
			return new SoapMonth(DateTime.ParseExact(value, SoapMonth._datetimeFormats, null, DateTimeStyles.None));
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000CB6D7 File Offset: 0x000C98D7
		public override string ToString()
		{
			return this._value.ToString("--MM--", CultureInfo.InvariantCulture);
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000CB6EE File Offset: 0x000C98EE
		// Note: this type is marked as 'beforefieldinit'.
		static SoapMonth()
		{
		}

		// Token: 0x040025B4 RID: 9652
		private static readonly string[] _datetimeFormats = new string[] { "--MM--", "--MM--zzz" };

		// Token: 0x040025B5 RID: 9653
		private DateTime _value;
	}
}
