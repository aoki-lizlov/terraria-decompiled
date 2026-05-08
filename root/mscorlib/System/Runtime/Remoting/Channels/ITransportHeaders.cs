using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000595 RID: 1429
	[ComVisible(true)]
	public interface ITransportHeaders
	{
		// Token: 0x17000801 RID: 2049
		object this[object key] { get; set; }

		// Token: 0x06003837 RID: 14391
		IEnumerator GetEnumerator();
	}
}
