using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200059B RID: 1435
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class TransportHeaders : ITransportHeaders
	{
		// Token: 0x06003850 RID: 14416 RVA: 0x000CA2B9 File Offset: 0x000C84B9
		public TransportHeaders()
		{
			this.hash_table = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
		}

		// Token: 0x17000808 RID: 2056
		public object this[object key]
		{
			get
			{
				return this.hash_table[key];
			}
			set
			{
				this.hash_table[key] = value;
			}
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000CA2F3 File Offset: 0x000C84F3
		public IEnumerator GetEnumerator()
		{
			return this.hash_table.GetEnumerator();
		}

		// Token: 0x04002575 RID: 9589
		private Hashtable hash_table;
	}
}
