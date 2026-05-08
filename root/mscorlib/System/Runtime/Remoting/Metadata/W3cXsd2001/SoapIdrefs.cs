using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005BD RID: 1469
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdrefs : ISoapXsd
	{
		// Token: 0x06003919 RID: 14617 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapIdrefs()
		{
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000CB5D7 File Offset: 0x000C97D7
		public SoapIdrefs(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600391B RID: 14619 RVA: 0x000CB5EB File Offset: 0x000C97EB
		// (set) Token: 0x0600391C RID: 14620 RVA: 0x000CB5F3 File Offset: 0x000C97F3
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

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000CB5FC File Offset: 0x000C97FC
		public static string XsdType
		{
			get
			{
				return "IDREFS";
			}
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000CB603 File Offset: 0x000C9803
		public string GetXsdType()
		{
			return SoapIdrefs.XsdType;
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000CB60A File Offset: 0x000C980A
		public static SoapIdrefs Parse(string value)
		{
			return new SoapIdrefs(value);
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000CB5EB File Offset: 0x000C97EB
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025B1 RID: 9649
		private string _value;
	}
}
