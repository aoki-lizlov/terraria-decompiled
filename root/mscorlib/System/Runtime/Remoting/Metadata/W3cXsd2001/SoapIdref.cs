using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005BC RID: 1468
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdref : ISoapXsd
	{
		// Token: 0x06003911 RID: 14609 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapIdref()
		{
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000CB59C File Offset: 0x000C979C
		public SoapIdref(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x000CB5B0 File Offset: 0x000C97B0
		// (set) Token: 0x06003914 RID: 14612 RVA: 0x000CB5B8 File Offset: 0x000C97B8
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

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000CB5C1 File Offset: 0x000C97C1
		public static string XsdType
		{
			get
			{
				return "IDREF";
			}
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000CB5C8 File Offset: 0x000C97C8
		public string GetXsdType()
		{
			return SoapIdref.XsdType;
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000CB5CF File Offset: 0x000C97CF
		public static SoapIdref Parse(string value)
		{
			return new SoapIdref(value);
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000CB5B0 File Offset: 0x000C97B0
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040025B0 RID: 9648
		private string _value;
	}
}
