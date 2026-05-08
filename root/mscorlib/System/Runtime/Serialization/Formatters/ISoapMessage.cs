using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x0200064D RID: 1613
	[ComVisible(true)]
	public interface ISoapMessage
	{
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06003D60 RID: 15712
		// (set) Token: 0x06003D61 RID: 15713
		string[] ParamNames { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06003D62 RID: 15714
		// (set) Token: 0x06003D63 RID: 15715
		object[] ParamValues { get; set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06003D64 RID: 15716
		// (set) Token: 0x06003D65 RID: 15717
		Type[] ParamTypes { get; set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06003D66 RID: 15718
		// (set) Token: 0x06003D67 RID: 15719
		string MethodName { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003D68 RID: 15720
		// (set) Token: 0x06003D69 RID: 15721
		string XmlNameSpace { get; set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06003D6A RID: 15722
		// (set) Token: 0x06003D6B RID: 15723
		Header[] Headers { get; set; }
	}
}
