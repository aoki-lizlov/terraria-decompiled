using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C6 RID: 1478
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtokens : ISoapXsd
	{
		// Token: 0x06003963 RID: 14691 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapNmtokens()
		{
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000CB89F File Offset: 0x000C9A9F
		public SoapNmtokens(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06003965 RID: 14693 RVA: 0x000CB8B3 File Offset: 0x000C9AB3
		// (set) Token: 0x06003966 RID: 14694 RVA: 0x000CB8BB File Offset: 0x000C9ABB
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

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x000CB8C4 File Offset: 0x000C9AC4
		public static string XsdType
		{
			get
			{
				return "NMTOKENS";
			}
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000CB8CB File Offset: 0x000C9ACB
		public string GetXsdType()
		{
			return SoapNmtokens.XsdType;
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000CB8D2 File Offset: 0x000C9AD2
		public static SoapNmtokens Parse(string value)
		{
			return new SoapNmtokens(value);
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000CB8B3 File Offset: 0x000C9AB3
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025BC RID: 9660
		private string _value;
	}
}
