using System;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200062E RID: 1582
	internal sealed class SurrogateForCyclicalReference : ISerializationSurrogate
	{
		// Token: 0x06003C6E RID: 15470 RVA: 0x000D2275 File Offset: 0x000D0475
		internal SurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
		{
			if (innerSurrogate == null)
			{
				throw new ArgumentNullException("innerSurrogate");
			}
			this.innerSurrogate = innerSurrogate;
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x000D2292 File Offset: 0x000D0492
		[SecurityCritical]
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			this.innerSurrogate.GetObjectData(obj, info, context);
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x000D22A2 File Offset: 0x000D04A2
		[SecurityCritical]
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			return this.innerSurrogate.SetObjectData(obj, info, context, selector);
		}

		// Token: 0x040026BB RID: 9915
		private ISerializationSurrogate innerSurrogate;
	}
}
