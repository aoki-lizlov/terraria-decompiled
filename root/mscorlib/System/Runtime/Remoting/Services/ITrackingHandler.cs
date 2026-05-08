using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x0200054D RID: 1357
	[ComVisible(true)]
	public interface ITrackingHandler
	{
		// Token: 0x060036A4 RID: 13988
		void DisconnectedObject(object obj);

		// Token: 0x060036A5 RID: 13989
		void MarshaledObject(object obj, ObjRef or);

		// Token: 0x060036A6 RID: 13990
		void UnmarshaledObject(object obj, ObjRef or);
	}
}
