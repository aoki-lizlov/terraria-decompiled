using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005B1 RID: 1457
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapAnyUri : ISoapXsd
	{
		// Token: 0x060038BD RID: 14525 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapAnyUri()
		{
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x000CAC9F File Offset: 0x000C8E9F
		public SoapAnyUri(string value)
		{
			this._value = value;
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060038BF RID: 14527 RVA: 0x000CACAE File Offset: 0x000C8EAE
		// (set) Token: 0x060038C0 RID: 14528 RVA: 0x000CACB6 File Offset: 0x000C8EB6
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

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060038C1 RID: 14529 RVA: 0x000CACBF File Offset: 0x000C8EBF
		public static string XsdType
		{
			get
			{
				return "anyUri";
			}
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000CACC6 File Offset: 0x000C8EC6
		public string GetXsdType()
		{
			return SoapAnyUri.XsdType;
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000CACCD File Offset: 0x000C8ECD
		public static SoapAnyUri Parse(string value)
		{
			return new SoapAnyUri(value);
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000CACAE File Offset: 0x000C8EAE
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025A3 RID: 9635
		private string _value;
	}
}
