using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005F3 RID: 1523
	internal interface ISerializationRootObject
	{
		// Token: 0x06003AAC RID: 15020
		void RootSetObjectData(SerializationInfo info, StreamingContext context);
	}
}
