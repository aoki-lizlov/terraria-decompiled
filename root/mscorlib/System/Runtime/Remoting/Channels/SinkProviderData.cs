using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200059A RID: 1434
	[ComVisible(true)]
	public class SinkProviderData
	{
		// Token: 0x0600384C RID: 14412 RVA: 0x000CA27C File Offset: 0x000C847C
		public SinkProviderData(string name)
		{
			this.sinkName = name;
			this.children = new ArrayList();
			this.properties = new Hashtable();
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000CA2A1 File Offset: 0x000C84A1
		public IList Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600384E RID: 14414 RVA: 0x000CA2A9 File Offset: 0x000C84A9
		public string Name
		{
			get
			{
				return this.sinkName;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000CA2B1 File Offset: 0x000C84B1
		public IDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002572 RID: 9586
		private string sinkName;

		// Token: 0x04002573 RID: 9587
		private ArrayList children;

		// Token: 0x04002574 RID: 9588
		private Hashtable properties;
	}
}
