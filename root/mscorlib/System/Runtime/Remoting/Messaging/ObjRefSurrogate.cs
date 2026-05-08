using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000604 RID: 1540
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x06003B7E RID: 15230 RVA: 0x000D03BE File Offset: 0x000CE5BE
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			((ObjRef)obj).GetObjectData(si, sc);
			si.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000D03E5 File Offset: 0x000CE5E5
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException("Do not use RemotingSurrogateSelector when deserializating");
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000025BE File Offset: 0x000007BE
		public ObjRefSurrogate()
		{
		}
	}
}
