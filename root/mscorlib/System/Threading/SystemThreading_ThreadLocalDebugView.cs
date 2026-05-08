using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x02000293 RID: 659
	internal sealed class SystemThreading_ThreadLocalDebugView<T>
	{
		// Token: 0x06001E7D RID: 7805 RVA: 0x0007295C File Offset: 0x00070B5C
		public SystemThreading_ThreadLocalDebugView(ThreadLocal<T> tlocal)
		{
			this.m_tlocal = tlocal;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0007296B File Offset: 0x00070B6B
		public bool IsValueCreated
		{
			get
			{
				return this.m_tlocal.IsValueCreated;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x00072978 File Offset: 0x00070B78
		public T Value
		{
			get
			{
				return this.m_tlocal.ValueForDebugDisplay;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00072985 File Offset: 0x00070B85
		public List<T> Values
		{
			get
			{
				return this.m_tlocal.ValuesForDebugDisplay;
			}
		}

		// Token: 0x040019B8 RID: 6584
		private readonly ThreadLocal<T> m_tlocal;
	}
}
