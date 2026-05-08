using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005CE RID: 1486
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapToken : ISoapXsd
	{
		// Token: 0x060039AA RID: 14762 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapToken()
		{
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000CBCDC File Offset: 0x000C9EDC
		public SoapToken(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x000CBCF0 File Offset: 0x000C9EF0
		// (set) Token: 0x060039AD RID: 14765 RVA: 0x000CBCF8 File Offset: 0x000C9EF8
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

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x000CBD01 File Offset: 0x000C9F01
		public static string XsdType
		{
			get
			{
				return "token";
			}
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x000CBD08 File Offset: 0x000C9F08
		public string GetXsdType()
		{
			return SoapToken.XsdType;
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000CBD0F File Offset: 0x000C9F0F
		public static SoapToken Parse(string value)
		{
			return new SoapToken(value);
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000CBCF0 File Offset: 0x000C9EF0
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025C7 RID: 9671
		private string _value;
	}
}
