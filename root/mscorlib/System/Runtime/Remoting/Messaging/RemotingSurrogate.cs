using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000603 RID: 1539
	internal class RemotingSurrogate : ISerializationSurrogate
	{
		// Token: 0x06003B7B RID: 15227 RVA: 0x000D0392 File Offset: 0x000CE592
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RemotingServices.GetRealProxy(obj).GetObjectData(si, sc);
				return;
			}
			RemotingServices.GetObjectData(obj, si, sc);
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000025BE File Offset: 0x000007BE
		public RemotingSurrogate()
		{
		}
	}
}
