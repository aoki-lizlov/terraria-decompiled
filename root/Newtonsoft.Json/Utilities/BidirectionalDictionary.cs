using System;
using System.Collections.Generic;
using System.Globalization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200005C RID: 92
	internal class BidirectionalDictionary<TFirst, TSecond>
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x000125B4 File Offset: 0x000107B4
		public BidirectionalDictionary()
			: this(EqualityComparer<TFirst>.Default, EqualityComparer<TSecond>.Default)
		{
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000125C6 File Offset: 0x000107C6
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer)
			: this(firstEqualityComparer, secondEqualityComparer, "Duplicate item already exists for '{0}'.", "Duplicate item already exists for '{0}'.")
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000125DA File Offset: 0x000107DA
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer, string duplicateFirstErrorMessage, string duplicateSecondErrorMessage)
		{
			this._firstToSecond = new Dictionary<TFirst, TSecond>(firstEqualityComparer);
			this._secondToFirst = new Dictionary<TSecond, TFirst>(secondEqualityComparer);
			this._duplicateFirstErrorMessage = duplicateFirstErrorMessage;
			this._duplicateSecondErrorMessage = duplicateSecondErrorMessage;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001260C File Offset: 0x0001080C
		public void Set(TFirst first, TSecond second)
		{
			TSecond tsecond;
			if (this._firstToSecond.TryGetValue(first, ref tsecond) && !tsecond.Equals(second))
			{
				throw new ArgumentException(this._duplicateFirstErrorMessage.FormatWith(CultureInfo.InvariantCulture, first));
			}
			TFirst tfirst;
			if (this._secondToFirst.TryGetValue(second, ref tfirst) && !tfirst.Equals(first))
			{
				throw new ArgumentException(this._duplicateSecondErrorMessage.FormatWith(CultureInfo.InvariantCulture, second));
			}
			this._firstToSecond.Add(first, second);
			this._secondToFirst.Add(second, first);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000126B5 File Offset: 0x000108B5
		public bool TryGetByFirst(TFirst first, out TSecond second)
		{
			return this._firstToSecond.TryGetValue(first, ref second);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000126C4 File Offset: 0x000108C4
		public bool TryGetBySecond(TSecond second, out TFirst first)
		{
			return this._secondToFirst.TryGetValue(second, ref first);
		}

		// Token: 0x0400020E RID: 526
		private readonly IDictionary<TFirst, TSecond> _firstToSecond;

		// Token: 0x0400020F RID: 527
		private readonly IDictionary<TSecond, TFirst> _secondToFirst;

		// Token: 0x04000210 RID: 528
		private readonly string _duplicateFirstErrorMessage;

		// Token: 0x04000211 RID: 529
		private readonly string _duplicateSecondErrorMessage;
	}
}
