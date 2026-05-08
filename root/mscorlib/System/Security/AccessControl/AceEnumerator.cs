using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x020004D3 RID: 1235
	public sealed class AceEnumerator : IEnumerator
	{
		// Token: 0x060032C7 RID: 12999 RVA: 0x000BC152 File Offset: 0x000BA352
		internal AceEnumerator(GenericAcl owner)
		{
			this.owner = owner;
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000BC168 File Offset: 0x000BA368
		public GenericAce Current
		{
			get
			{
				if (this.current >= 0)
				{
					return this.owner[this.current];
				}
				return null;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x000BC186 File Offset: 0x000BA386
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000BC18E File Offset: 0x000BA38E
		public bool MoveNext()
		{
			if (this.current + 1 == this.owner.Count)
			{
				return false;
			}
			this.current++;
			return true;
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000BC1B6 File Offset: 0x000BA3B6
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x04002394 RID: 9108
		private GenericAcl owner;

		// Token: 0x04002395 RID: 9109
		private int current = -1;
	}
}
