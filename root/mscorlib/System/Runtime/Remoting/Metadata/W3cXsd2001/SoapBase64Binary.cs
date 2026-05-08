using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005B2 RID: 1458
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapBase64Binary : ISoapXsd
	{
		// Token: 0x060038C5 RID: 14533 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapBase64Binary()
		{
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000CACD5 File Offset: 0x000C8ED5
		public SoapBase64Binary(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060038C7 RID: 14535 RVA: 0x000CACE4 File Offset: 0x000C8EE4
		// (set) Token: 0x060038C8 RID: 14536 RVA: 0x000CACEC File Offset: 0x000C8EEC
		public byte[] Value
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

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060038C9 RID: 14537 RVA: 0x000CACF5 File Offset: 0x000C8EF5
		public static string XsdType
		{
			get
			{
				return "base64Binary";
			}
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000CACFC File Offset: 0x000C8EFC
		public string GetXsdType()
		{
			return SoapBase64Binary.XsdType;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000CAD03 File Offset: 0x000C8F03
		public static SoapBase64Binary Parse(string value)
		{
			return new SoapBase64Binary(Convert.FromBase64String(value));
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000CAD10 File Offset: 0x000C8F10
		public override string ToString()
		{
			return Convert.ToBase64String(this._value);
		}

		// Token: 0x040025A4 RID: 9636
		private byte[] _value;
	}
}
