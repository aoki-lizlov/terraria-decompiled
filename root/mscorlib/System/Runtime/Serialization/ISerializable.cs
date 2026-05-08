using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200061B RID: 1563
	public interface ISerializable
	{
		// Token: 0x06003BE6 RID: 15334
		void GetObjectData(SerializationInfo info, StreamingContext context);
	}
}
