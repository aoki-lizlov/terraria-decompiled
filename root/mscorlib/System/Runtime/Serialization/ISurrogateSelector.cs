using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000624 RID: 1572
	public interface ISurrogateSelector
	{
		// Token: 0x06003C33 RID: 15411
		void ChainSelector(ISurrogateSelector selector);

		// Token: 0x06003C34 RID: 15412
		ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector);

		// Token: 0x06003C35 RID: 15413
		ISurrogateSelector GetNextSelector();
	}
}
