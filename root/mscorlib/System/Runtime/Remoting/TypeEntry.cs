using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x02000547 RID: 1351
	[ComVisible(true)]
	public class TypeEntry
	{
		// Token: 0x06003688 RID: 13960 RVA: 0x000025BE File Offset: 0x000007BE
		protected TypeEntry()
		{
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x000C6028 File Offset: 0x000C4228
		// (set) Token: 0x0600368A RID: 13962 RVA: 0x000C6030 File Offset: 0x000C4230
		public string AssemblyName
		{
			get
			{
				return this.assembly_name;
			}
			set
			{
				this.assembly_name = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x000C6039 File Offset: 0x000C4239
		// (set) Token: 0x0600368C RID: 13964 RVA: 0x000C6041 File Offset: 0x000C4241
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x040024ED RID: 9453
		private string assembly_name;

		// Token: 0x040024EE RID: 9454
		private string type_name;
	}
}
