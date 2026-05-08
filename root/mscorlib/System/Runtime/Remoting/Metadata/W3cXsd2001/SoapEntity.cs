using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005B8 RID: 1464
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntity : ISoapXsd
	{
		// Token: 0x060038F3 RID: 14579 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapEntity()
		{
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000CB37C File Offset: 0x000C957C
		public SoapEntity(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000CB390 File Offset: 0x000C9590
		// (set) Token: 0x060038F6 RID: 14582 RVA: 0x000CB398 File Offset: 0x000C9598
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

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000CB3A1 File Offset: 0x000C95A1
		public static string XsdType
		{
			get
			{
				return "ENTITY";
			}
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000CB3A8 File Offset: 0x000C95A8
		public string GetXsdType()
		{
			return SoapEntity.XsdType;
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000CB3AF File Offset: 0x000C95AF
		public static SoapEntity Parse(string value)
		{
			return new SoapEntity(value);
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000CB390 File Offset: 0x000C9590
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025AC RID: 9644
		private string _value;
	}
}
