using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005F2 RID: 1522
	[ComVisible(true)]
	public interface IRemotingFormatter : IFormatter
	{
		// Token: 0x06003AAA RID: 15018
		object Deserialize(Stream serializationStream, HeaderHandler handler);

		// Token: 0x06003AAB RID: 15019
		void Serialize(Stream serializationStream, object graph, Header[] headers);
	}
}
