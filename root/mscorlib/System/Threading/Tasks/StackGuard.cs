using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000334 RID: 820
	internal class StackGuard
	{
		// Token: 0x0600241B RID: 9243 RVA: 0x000826BD File Offset: 0x000808BD
		internal bool TryBeginInliningScope()
		{
			if (this.m_inliningDepth < 20 || RuntimeHelpers.TryEnsureSufficientExecutionStack())
			{
				this.m_inliningDepth++;
				return true;
			}
			return false;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000826E1 File Offset: 0x000808E1
		internal void EndInliningScope()
		{
			this.m_inliningDepth--;
			if (this.m_inliningDepth < 0)
			{
				this.m_inliningDepth = 0;
			}
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000025BE File Offset: 0x000007BE
		public StackGuard()
		{
		}

		// Token: 0x04001BF0 RID: 7152
		private int m_inliningDepth;

		// Token: 0x04001BF1 RID: 7153
		private const int MAX_UNCHECKED_INLINING_DEPTH = 20;
	}
}
