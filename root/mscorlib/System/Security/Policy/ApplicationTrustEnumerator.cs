using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003D5 RID: 981
	[ComVisible(true)]
	public sealed class ApplicationTrustEnumerator : IEnumerator
	{
		// Token: 0x060029C2 RID: 10690 RVA: 0x00098A89 File Offset: 0x00096C89
		internal ApplicationTrustEnumerator(ApplicationTrustCollection atc)
		{
			this.trusts = atc;
			this.current = -1;
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x00098A9F File Offset: 0x00096C9F
		public ApplicationTrust Current
		{
			get
			{
				return this.trusts[this.current];
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x00098A9F File Offset: 0x00096C9F
		object IEnumerator.Current
		{
			get
			{
				return this.trusts[this.current];
			}
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x00098AB2 File Offset: 0x00096CB2
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00098ABB File Offset: 0x00096CBB
		public bool MoveNext()
		{
			if (this.current == this.trusts.Count - 1)
			{
				return false;
			}
			this.current++;
			return true;
		}

		// Token: 0x04001E2D RID: 7725
		private ApplicationTrustCollection trusts;

		// Token: 0x04001E2E RID: 7726
		private int current;
	}
}
