using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000639 RID: 1593
	internal class TypeLoadExceptionHolder
	{
		// Token: 0x06003CE9 RID: 15593 RVA: 0x000D4127 File Offset: 0x000D2327
		internal TypeLoadExceptionHolder(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x000D4136 File Offset: 0x000D2336
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x040026FC RID: 9980
		private string m_typeName;
	}
}
