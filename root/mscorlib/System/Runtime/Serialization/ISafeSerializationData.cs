using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200063B RID: 1595
	public interface ISafeSerializationData
	{
		// Token: 0x06003CEF RID: 15599
		void CompleteDeserialization(object deserialized);
	}
}
