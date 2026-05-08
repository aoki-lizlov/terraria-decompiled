using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005CC RID: 1484
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapQName : ISoapXsd
	{
		// Token: 0x06003993 RID: 14739 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapQName()
		{
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000CBA92 File Offset: 0x000C9C92
		public SoapQName(string value)
		{
			this._name = value;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000CBAA1 File Offset: 0x000C9CA1
		public SoapQName(string key, string name)
		{
			this._key = key;
			this._name = name;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000CBAB7 File Offset: 0x000C9CB7
		public SoapQName(string key, string name, string namespaceValue)
		{
			this._key = key;
			this._name = name;
			this._namespace = namespaceValue;
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x000CBAD4 File Offset: 0x000C9CD4
		// (set) Token: 0x06003998 RID: 14744 RVA: 0x000CBADC File Offset: 0x000C9CDC
		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x000CBAE5 File Offset: 0x000C9CE5
		// (set) Token: 0x0600399A RID: 14746 RVA: 0x000CBAED File Offset: 0x000C9CED
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x000CBAF6 File Offset: 0x000C9CF6
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x000CBAFE File Offset: 0x000C9CFE
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000CBB07 File Offset: 0x000C9D07
		public static string XsdType
		{
			get
			{
				return "QName";
			}
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000CBB0E File Offset: 0x000C9D0E
		public string GetXsdType()
		{
			return SoapQName.XsdType;
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000CBB18 File Offset: 0x000C9D18
		public static SoapQName Parse(string value)
		{
			SoapQName soapQName = new SoapQName();
			int num = value.IndexOf(':');
			if (num != -1)
			{
				soapQName.Key = value.Substring(0, num);
				soapQName.Name = value.Substring(num + 1);
			}
			else
			{
				soapQName.Name = value;
			}
			return soapQName;
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000CBB5F File Offset: 0x000C9D5F
		public override string ToString()
		{
			if (this._key == null || this._key == "")
			{
				return this._name;
			}
			return this._key + ":" + this._name;
		}

		// Token: 0x040025C2 RID: 9666
		private string _name;

		// Token: 0x040025C3 RID: 9667
		private string _key;

		// Token: 0x040025C4 RID: 9668
		private string _namespace;
	}
}
