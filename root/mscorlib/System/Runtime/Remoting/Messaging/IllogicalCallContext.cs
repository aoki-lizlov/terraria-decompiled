using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005D3 RID: 1491
	internal class IllogicalCallContext
	{
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000CC134 File Offset: 0x000CA334
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x000CC14F File Offset: 0x000CA34F
		// (set) Token: 0x060039DB RID: 14811 RVA: 0x000CC157 File Offset: 0x000CA357
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000CC160 File Offset: 0x000CA360
		internal bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000CC17A File Offset: 0x000CA37A
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000CC188 File Offset: 0x000CA388
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000CC196 File Offset: 0x000CA396
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000CC1A8 File Offset: 0x000CA3A8
		public IllogicalCallContext CreateCopy()
		{
			IllogicalCallContext illogicalCallContext = new IllogicalCallContext();
			illogicalCallContext.HostContext = this.HostContext;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					illogicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
			return illogicalCallContext;
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000025BE File Offset: 0x000007BE
		public IllogicalCallContext()
		{
		}

		// Token: 0x040025CE RID: 9678
		private Hashtable m_Datastore;

		// Token: 0x040025CF RID: 9679
		private object m_HostContext;

		// Token: 0x020005D4 RID: 1492
		internal struct Reader
		{
			// Token: 0x060039E2 RID: 14818 RVA: 0x000CC202 File Offset: 0x000CA402
			public Reader(IllogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000CC20B File Offset: 0x000CA40B
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x060039E4 RID: 14820 RVA: 0x000CC216 File Offset: 0x000CA416
			[SecurityCritical]
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000CC22E File Offset: 0x000CA42E
			public object HostContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.HostContext;
					}
					return null;
				}
			}

			// Token: 0x040025D0 RID: 9680
			private IllogicalCallContext m_ctx;
		}
	}
}
