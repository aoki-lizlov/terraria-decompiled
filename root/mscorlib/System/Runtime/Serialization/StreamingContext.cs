using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000644 RID: 1604
	[ComVisible(true)]
	[Serializable]
	public readonly struct StreamingContext
	{
		// Token: 0x06003D4B RID: 15691 RVA: 0x000D5096 File Offset: 0x000D3296
		public StreamingContext(StreamingContextStates state)
		{
			this = new StreamingContext(state, null);
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x000D50A0 File Offset: 0x000D32A0
		public StreamingContext(StreamingContextStates state, object additional)
		{
			this.m_state = state;
			this.m_additionalContext = additional;
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003D4D RID: 15693 RVA: 0x000D50B0 File Offset: 0x000D32B0
		public object Context
		{
			get
			{
				return this.m_additionalContext;
			}
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x000D50B8 File Offset: 0x000D32B8
		public override bool Equals(object obj)
		{
			return obj is StreamingContext && (((StreamingContext)obj).m_additionalContext == this.m_additionalContext && ((StreamingContext)obj).m_state == this.m_state);
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x000D50ED File Offset: 0x000D32ED
		public override int GetHashCode()
		{
			return (int)this.m_state;
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000D50ED File Offset: 0x000D32ED
		public StreamingContextStates State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x04002719 RID: 10009
		internal readonly object m_additionalContext;

		// Token: 0x0400271A RID: 10010
		internal readonly StreamingContextStates m_state;
	}
}
