using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005BB RID: 1467
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapId : ISoapXsd
	{
		// Token: 0x06003909 RID: 14601 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapId()
		{
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000CB561 File Offset: 0x000C9761
		public SoapId(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600390B RID: 14603 RVA: 0x000CB575 File Offset: 0x000C9775
		// (set) Token: 0x0600390C RID: 14604 RVA: 0x000CB57D File Offset: 0x000C977D
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

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000CB586 File Offset: 0x000C9786
		public static string XsdType
		{
			get
			{
				return "ID";
			}
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000CB58D File Offset: 0x000C978D
		public string GetXsdType()
		{
			return SoapId.XsdType;
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000CB594 File Offset: 0x000C9794
		public static SoapId Parse(string value)
		{
			return new SoapId(value);
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000CB575 File Offset: 0x000C9775
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025AF RID: 9647
		private string _value;
	}
}
