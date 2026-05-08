using System;

namespace Mono
{
	// Token: 0x0200002A RID: 42
	internal struct RuntimeRemoteClassHandle
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x000041BF File Offset: 0x000023BF
		internal unsafe RuntimeRemoteClassHandle(RuntimeStructs.RemoteClass* value)
		{
			this.value = value;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000041C8 File Offset: 0x000023C8
		internal unsafe RuntimeClassHandle ProxyClass
		{
			get
			{
				return new RuntimeClassHandle(this.value->proxy_class);
			}
		}

		// Token: 0x04000CDF RID: 3295
		private unsafe RuntimeStructs.RemoteClass* value;
	}
}
