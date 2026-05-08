using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005C2 RID: 1474
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapName : ISoapXsd
	{
		// Token: 0x06003943 RID: 14659 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapName()
		{
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000CB781 File Offset: 0x000C9981
		public SoapName(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x000CB795 File Offset: 0x000C9995
		// (set) Token: 0x06003946 RID: 14662 RVA: 0x000CB79D File Offset: 0x000C999D
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

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06003947 RID: 14663 RVA: 0x000CB7A6 File Offset: 0x000C99A6
		public static string XsdType
		{
			get
			{
				return "Name";
			}
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000CB7AD File Offset: 0x000C99AD
		public string GetXsdType()
		{
			return SoapName.XsdType;
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000CB7B4 File Offset: 0x000C99B4
		public static SoapName Parse(string value)
		{
			return new SoapName(value);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000CB795 File Offset: 0x000C9995
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025B8 RID: 9656
		private string _value;
	}
}
