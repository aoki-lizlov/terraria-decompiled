using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000623 RID: 1571
	public interface ISerializationSurrogate
	{
		// Token: 0x06003C31 RID: 15409
		void GetObjectData(object obj, SerializationInfo info, StreamingContext context);

		// Token: 0x06003C32 RID: 15410
		object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
	}
}
