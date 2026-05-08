using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005B7 RID: 1463
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntities : ISoapXsd
	{
		// Token: 0x060038EB RID: 14571 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapEntities()
		{
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000CB341 File Offset: 0x000C9541
		public SoapEntities(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060038ED RID: 14573 RVA: 0x000CB355 File Offset: 0x000C9555
		// (set) Token: 0x060038EE RID: 14574 RVA: 0x000CB35D File Offset: 0x000C955D
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

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060038EF RID: 14575 RVA: 0x000CB366 File Offset: 0x000C9566
		public static string XsdType
		{
			get
			{
				return "ENTITIES";
			}
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000CB36D File Offset: 0x000C956D
		public string GetXsdType()
		{
			return SoapEntities.XsdType;
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000CB374 File Offset: 0x000C9574
		public static SoapEntities Parse(string value)
		{
			return new SoapEntities(value);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000CB355 File Offset: 0x000C9555
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025AB RID: 9643
		private string _value;
	}
}
