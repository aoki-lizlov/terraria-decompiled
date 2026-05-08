using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200061A RID: 1562
	public interface IObjectReference
	{
		// Token: 0x06003BE5 RID: 15333
		object GetRealObject(StreamingContext context);
	}
}
