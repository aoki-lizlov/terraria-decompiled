using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000647 RID: 1607
	[Serializable]
	internal class SurrogateKey
	{
		// Token: 0x06003D58 RID: 15704 RVA: 0x000D533D File Offset: 0x000D353D
		internal SurrogateKey(Type type, StreamingContext context)
		{
			this.m_type = type;
			this.m_context = context;
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000D5353 File Offset: 0x000D3553
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		// Token: 0x04002727 RID: 10023
		internal Type m_type;

		// Token: 0x04002728 RID: 10024
		internal StreamingContext m_context;
	}
}
