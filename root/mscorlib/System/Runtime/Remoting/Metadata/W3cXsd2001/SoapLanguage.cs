using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005BF RID: 1471
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapLanguage : ISoapXsd
	{
		// Token: 0x06003929 RID: 14633 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapLanguage()
		{
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000CB65A File Offset: 0x000C985A
		public SoapLanguage(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600392B RID: 14635 RVA: 0x000CB66E File Offset: 0x000C986E
		// (set) Token: 0x0600392C RID: 14636 RVA: 0x000CB676 File Offset: 0x000C9876
		public string Value
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

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600392D RID: 14637 RVA: 0x000CB67F File Offset: 0x000C987F
		public static string XsdType
		{
			get
			{
				return "language";
			}
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000CB686 File Offset: 0x000C9886
		public string GetXsdType()
		{
			return SoapLanguage.XsdType;
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000CB68D File Offset: 0x000C988D
		public static SoapLanguage Parse(string value)
		{
			return new SoapLanguage(value);
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000CB66E File Offset: 0x000C986E
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025B3 RID: 9651
		private string _value;
	}
}
