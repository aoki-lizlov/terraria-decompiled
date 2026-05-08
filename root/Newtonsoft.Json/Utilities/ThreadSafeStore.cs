using System;
using System.Collections.Generic;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200005B RID: 91
	internal class ThreadSafeStore<TKey, TValue>
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x000124AF File Offset: 0x000106AF
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			if (creator == null)
			{
				throw new ArgumentNullException("creator");
			}
			this._creator = creator;
			this._store = new Dictionary<TKey, TValue>();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000124E4 File Offset: 0x000106E4
		public TValue Get(TKey key)
		{
			TValue tvalue;
			if (!this._store.TryGetValue(key, ref tvalue))
			{
				return this.AddValue(key);
			}
			return tvalue;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001250C File Offset: 0x0001070C
		private TValue AddValue(TKey key)
		{
			TValue tvalue = this._creator.Invoke(key);
			object @lock = this._lock;
			TValue tvalue3;
			lock (@lock)
			{
				if (this._store == null)
				{
					this._store = new Dictionary<TKey, TValue>();
					this._store[key] = tvalue;
				}
				else
				{
					TValue tvalue2;
					if (this._store.TryGetValue(key, ref tvalue2))
					{
						return tvalue2;
					}
					Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(this._store);
					dictionary[key] = tvalue;
					Thread.MemoryBarrier();
					this._store = dictionary;
				}
				tvalue3 = tvalue;
			}
			return tvalue3;
		}

		// Token: 0x0400020B RID: 523
		private readonly object _lock = new object();

		// Token: 0x0400020C RID: 524
		private Dictionary<TKey, TValue> _store;

		// Token: 0x0400020D RID: 525
		private readonly Func<TKey, TValue> _creator;
	}
}
