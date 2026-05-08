using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C5 RID: 1477
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtoken : ISoapXsd
	{
		// Token: 0x0600395B RID: 14683 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapNmtoken()
		{
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000CB864 File Offset: 0x000C9A64
		public SoapNmtoken(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x000CB878 File Offset: 0x000C9A78
		// (set) Token: 0x0600395E RID: 14686 RVA: 0x000CB880 File Offset: 0x000C9A80
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

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000CB889 File Offset: 0x000C9A89
		public static string XsdType
		{
			get
			{
				return "NMTOKEN";
			}
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000CB890 File Offset: 0x000C9A90
		public string GetXsdType()
		{
			return SoapNmtoken.XsdType;
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000CB897 File Offset: 0x000C9A97
		public static SoapNmtoken Parse(string value)
		{
			return new SoapNmtoken(value);
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000CB878 File Offset: 0x000C9A78
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025BB RID: 9659
		private string _value;
	}
}
