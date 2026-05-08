using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AFA RID: 2810
	[Serializable]
	public readonly struct KeyValuePair<TKey, TValue>
	{
		// Token: 0x06006744 RID: 26436 RVA: 0x0015E297 File Offset: 0x0015C497
		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06006745 RID: 26437 RVA: 0x0015E2A7 File Offset: 0x0015C4A7
		public TKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06006746 RID: 26438 RVA: 0x0015E2AF File Offset: 0x0015C4AF
		public TValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x0015E2B7 File Offset: 0x0015C4B7
		public override string ToString()
		{
			return KeyValuePair.PairToString(this.Key, this.Value);
		}

		// Token: 0x06006748 RID: 26440 RVA: 0x0015E2D4 File Offset: 0x0015C4D4
		public void Deconstruct(out TKey key, out TValue value)
		{
			key = this.Key;
			value = this.Value;
		}

		// Token: 0x04003C13 RID: 15379
		private readonly TKey key;

		// Token: 0x04003C14 RID: 15380
		private readonly TValue value;
	}
}
