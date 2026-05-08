using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005CA RID: 1482
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNotation : ISoapXsd
	{
		// Token: 0x06003983 RID: 14723 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapNotation()
		{
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000CB9EF File Offset: 0x000C9BEF
		public SoapNotation(string value)
		{
			this._value = value;
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000CB9FE File Offset: 0x000C9BFE
		// (set) Token: 0x06003986 RID: 14726 RVA: 0x000CBA06 File Offset: 0x000C9C06
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

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000CBA0F File Offset: 0x000C9C0F
		public static string XsdType
		{
			get
			{
				return "NOTATION";
			}
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000CBA16 File Offset: 0x000C9C16
		public string GetXsdType()
		{
			return SoapNotation.XsdType;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000CBA1D File Offset: 0x000C9C1D
		public static SoapNotation Parse(string value)
		{
			return new SoapNotation(value);
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000CB9FE File Offset: 0x000C9BFE
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025C0 RID: 9664
		private string _value;
	}
}
