using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000626 RID: 1574
	[Serializable]
	public abstract class SerializationBinder
	{
		// Token: 0x06003C39 RID: 15417 RVA: 0x000D1527 File Offset: 0x000CF727
		public virtual void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = null;
		}

		// Token: 0x06003C3A RID: 15418
		public abstract Type BindToType(string assemblyName, string typeName);

		// Token: 0x06003C3B RID: 15419 RVA: 0x000025BE File Offset: 0x000007BE
		protected SerializationBinder()
		{
		}
	}
}
