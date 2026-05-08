using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000653 RID: 1619
	[ComVisible(true)]
	[Serializable]
	public class SoapMessage : ISoapMessage
	{
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x000D56EC File Offset: 0x000D38EC
		// (set) Token: 0x06003D8E RID: 15758 RVA: 0x000D56F4 File Offset: 0x000D38F4
		public string[] ParamNames
		{
			get
			{
				return this.paramNames;
			}
			set
			{
				this.paramNames = value;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003D8F RID: 15759 RVA: 0x000D56FD File Offset: 0x000D38FD
		// (set) Token: 0x06003D90 RID: 15760 RVA: 0x000D5705 File Offset: 0x000D3905
		public object[] ParamValues
		{
			get
			{
				return this.paramValues;
			}
			set
			{
				this.paramValues = value;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003D91 RID: 15761 RVA: 0x000D570E File Offset: 0x000D390E
		// (set) Token: 0x06003D92 RID: 15762 RVA: 0x000D5716 File Offset: 0x000D3916
		public Type[] ParamTypes
		{
			get
			{
				return this.paramTypes;
			}
			set
			{
				this.paramTypes = value;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x000D571F File Offset: 0x000D391F
		// (set) Token: 0x06003D94 RID: 15764 RVA: 0x000D5727 File Offset: 0x000D3927
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
			set
			{
				this.methodName = value;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003D95 RID: 15765 RVA: 0x000D5730 File Offset: 0x000D3930
		// (set) Token: 0x06003D96 RID: 15766 RVA: 0x000D5738 File Offset: 0x000D3938
		public string XmlNameSpace
		{
			get
			{
				return this.xmlNameSpace;
			}
			set
			{
				this.xmlNameSpace = value;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003D97 RID: 15767 RVA: 0x000D5741 File Offset: 0x000D3941
		// (set) Token: 0x06003D98 RID: 15768 RVA: 0x000D5749 File Offset: 0x000D3949
		public Header[] Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapMessage()
		{
		}

		// Token: 0x0400273B RID: 10043
		internal string[] paramNames;

		// Token: 0x0400273C RID: 10044
		internal object[] paramValues;

		// Token: 0x0400273D RID: 10045
		internal Type[] paramTypes;

		// Token: 0x0400273E RID: 10046
		internal string methodName;

		// Token: 0x0400273F RID: 10047
		internal string xmlNameSpace;

		// Token: 0x04002740 RID: 10048
		internal Header[] headers;
	}
}
