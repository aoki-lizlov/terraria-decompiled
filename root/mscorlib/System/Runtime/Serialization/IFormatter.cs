using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200062F RID: 1583
	[ComVisible(true)]
	public interface IFormatter
	{
		// Token: 0x06003C71 RID: 15473
		object Deserialize(Stream serializationStream);

		// Token: 0x06003C72 RID: 15474
		void Serialize(Stream serializationStream, object graph);

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003C73 RID: 15475
		// (set) Token: 0x06003C74 RID: 15476
		ISurrogateSelector SurrogateSelector { get; set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003C75 RID: 15477
		// (set) Token: 0x06003C76 RID: 15478
		SerializationBinder Binder { get; set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06003C77 RID: 15479
		// (set) Token: 0x06003C78 RID: 15480
		StreamingContext Context { get; set; }
	}
}
