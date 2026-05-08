using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x02000AB6 RID: 2742
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IProducerConsumerCollectionDebugView<>))]
	[Serializable]
	public class ConcurrentStack<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x06006559 RID: 25945 RVA: 0x000025BE File Offset: 0x000007BE
		public ConcurrentStack()
		{
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x001599C8 File Offset: 0x00157BC8
		public ConcurrentStack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x001599E8 File Offset: 0x00157BE8
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			ConcurrentStack<T>.Node node = null;
			foreach (T t in collection)
			{
				node = new ConcurrentStack<T>.Node(t)
				{
					_next = node
				};
			}
			this._head = node;
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x0600655C RID: 25948 RVA: 0x00159A40 File Offset: 0x00157C40
		public bool IsEmpty
		{
			get
			{
				return this._head == null;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x0600655D RID: 25949 RVA: 0x00159A50 File Offset: 0x00157C50
		public int Count
		{
			get
			{
				int num = 0;
				for (ConcurrentStack<T>.Node node = this._head; node != null; node = node._next)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x0600655E RID: 25950 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x0600655F RID: 25951 RVA: 0x001572CF File Offset: 0x001554CF
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		// Token: 0x06006560 RID: 25952 RVA: 0x00159A79 File Offset: 0x00157C79
		public void Clear()
		{
			this._head = null;
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x00159A84 File Offset: 0x00157C84
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this.ToList()).CopyTo(array, index);
		}

		// Token: 0x06006562 RID: 25954 RVA: 0x00159AA1 File Offset: 0x00157CA1
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		// Token: 0x06006563 RID: 25955 RVA: 0x00159AC0 File Offset: 0x00157CC0
		public void Push(T item)
		{
			ConcurrentStack<T>.Node node = new ConcurrentStack<T>.Node(item);
			node._next = this._head;
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, node, node._next) == node._next)
			{
				return;
			}
			this.PushCore(node, node);
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x00159B05 File Offset: 0x00157D05
		public void PushRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.PushRange(items, 0, items.Length);
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x00159B20 File Offset: 0x00157D20
		public void PushRange(T[] items, int startIndex, int count)
		{
			ConcurrentStack<T>.ValidatePushPopRangeInput(items, startIndex, count);
			if (count == 0)
			{
				return;
			}
			ConcurrentStack<T>.Node node2;
			ConcurrentStack<T>.Node node = (node2 = new ConcurrentStack<T>.Node(items[startIndex]));
			for (int i = startIndex + 1; i < startIndex + count; i++)
			{
				node2 = new ConcurrentStack<T>.Node(items[i])
				{
					_next = node2
				};
			}
			node._next = this._head;
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, node2, node._next) == node._next)
			{
				return;
			}
			this.PushCore(node2, node);
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x00159BA0 File Offset: 0x00157DA0
		private void PushCore(ConcurrentStack<T>.Node head, ConcurrentStack<T>.Node tail)
		{
			SpinWait spinWait = default(SpinWait);
			do
			{
				spinWait.SpinOnce();
				tail._next = this._head;
			}
			while (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, head, tail._next) != tail._next);
			if (CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPushFailed(spinWait.Count);
			}
		}

		// Token: 0x06006567 RID: 25959 RVA: 0x00159C04 File Offset: 0x00157E04
		private static void ValidatePushPopRangeInput(T[] items, int startIndex, int count)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "The count argument must be greater than or equal to zero.");
			}
			int num = items.Length;
			if (startIndex >= num || startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "The startIndex argument must be greater than or equal to zero.");
			}
			if (num - count < startIndex)
			{
				throw new ArgumentException("The sum of the startIndex and count arguments must be less than or equal to the collection's Count.");
			}
		}

		// Token: 0x06006568 RID: 25960 RVA: 0x00159C60 File Offset: 0x00157E60
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Push(item);
			return true;
		}

		// Token: 0x06006569 RID: 25961 RVA: 0x00159C6C File Offset: 0x00157E6C
		public bool TryPeek(out T result)
		{
			ConcurrentStack<T>.Node head = this._head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			result = head._value;
			return true;
		}

		// Token: 0x0600656A RID: 25962 RVA: 0x00159C9C File Offset: 0x00157E9C
		public bool TryPop(out T result)
		{
			ConcurrentStack<T>.Node head = this._head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, head._next, head) == head)
			{
				result = head._value;
				return true;
			}
			return this.TryPopCore(out result);
		}

		// Token: 0x0600656B RID: 25963 RVA: 0x00159CE8 File Offset: 0x00157EE8
		public int TryPopRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			return this.TryPopRange(items, 0, items.Length);
		}

		// Token: 0x0600656C RID: 25964 RVA: 0x00159D04 File Offset: 0x00157F04
		public int TryPopRange(T[] items, int startIndex, int count)
		{
			ConcurrentStack<T>.ValidatePushPopRangeInput(items, startIndex, count);
			if (count == 0)
			{
				return 0;
			}
			ConcurrentStack<T>.Node node;
			int num = this.TryPopCore(count, out node);
			if (num > 0)
			{
				ConcurrentStack<T>.CopyRemovedItems(node, items, startIndex, num);
			}
			return num;
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x00159D38 File Offset: 0x00157F38
		private bool TryPopCore(out T result)
		{
			ConcurrentStack<T>.Node node;
			if (this.TryPopCore(1, out node) == 1)
			{
				result = node._value;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600656E RID: 25966 RVA: 0x00159D68 File Offset: 0x00157F68
		private int TryPopCore(int count, out ConcurrentStack<T>.Node poppedHead)
		{
			SpinWait spinWait = default(SpinWait);
			int num = 1;
			Random random = null;
			ConcurrentStack<T>.Node head;
			int num2;
			for (;;)
			{
				head = this._head;
				if (head == null)
				{
					break;
				}
				ConcurrentStack<T>.Node node = head;
				num2 = 1;
				while (num2 < count && node._next != null)
				{
					node = node._next;
					num2++;
				}
				if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, node._next, head) == head)
				{
					goto Block_5;
				}
				for (int i = 0; i < num; i++)
				{
					spinWait.SpinOnce();
				}
				if (spinWait.NextSpinWillYield)
				{
					if (random == null)
					{
						random = new Random();
					}
					num = random.Next(1, 8);
				}
				else
				{
					num *= 2;
				}
			}
			if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
			}
			poppedHead = null;
			return 0;
			Block_5:
			if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
			}
			poppedHead = head;
			return num2;
		}

		// Token: 0x0600656F RID: 25967 RVA: 0x00159E54 File Offset: 0x00158054
		private static void CopyRemovedItems(ConcurrentStack<T>.Node head, T[] collection, int startIndex, int nodesCount)
		{
			ConcurrentStack<T>.Node node = head;
			for (int i = startIndex; i < startIndex + nodesCount; i++)
			{
				collection[i] = node._value;
				node = node._next;
			}
		}

		// Token: 0x06006570 RID: 25968 RVA: 0x00159E85 File Offset: 0x00158085
		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryPop(out item);
		}

		// Token: 0x06006571 RID: 25969 RVA: 0x00159E90 File Offset: 0x00158090
		public T[] ToArray()
		{
			ConcurrentStack<T>.Node head = this._head;
			if (head != null)
			{
				return this.ToList(head).ToArray();
			}
			return Array.Empty<T>();
		}

		// Token: 0x06006572 RID: 25970 RVA: 0x00159EBB File Offset: 0x001580BB
		private List<T> ToList()
		{
			return this.ToList(this._head);
		}

		// Token: 0x06006573 RID: 25971 RVA: 0x00159ECC File Offset: 0x001580CC
		private List<T> ToList(ConcurrentStack<T>.Node curr)
		{
			List<T> list = new List<T>();
			while (curr != null)
			{
				list.Add(curr._value);
				curr = curr._next;
			}
			return list;
		}

		// Token: 0x06006574 RID: 25972 RVA: 0x00159EF9 File Offset: 0x001580F9
		public IEnumerator<T> GetEnumerator()
		{
			return this.GetEnumerator(this._head);
		}

		// Token: 0x06006575 RID: 25973 RVA: 0x00159F09 File Offset: 0x00158109
		private IEnumerator<T> GetEnumerator(ConcurrentStack<T>.Node head)
		{
			for (ConcurrentStack<T>.Node current = head; current != null; current = current._next)
			{
				yield return current._value;
			}
			yield break;
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x001572DB File Offset: 0x001554DB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x04003B83 RID: 15235
		private volatile ConcurrentStack<T>.Node _head;

		// Token: 0x04003B84 RID: 15236
		private const int BACKOFF_MAX_YIELDS = 8;

		// Token: 0x02000AB7 RID: 2743
		[Serializable]
		private class Node
		{
			// Token: 0x06006577 RID: 25975 RVA: 0x00159F18 File Offset: 0x00158118
			internal Node(T value)
			{
				this._value = value;
				this._next = null;
			}

			// Token: 0x04003B85 RID: 15237
			internal readonly T _value;

			// Token: 0x04003B86 RID: 15238
			internal ConcurrentStack<T>.Node _next;
		}

		// Token: 0x02000AB8 RID: 2744
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__35 : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006578 RID: 25976 RVA: 0x00159F2E File Offset: 0x0015812E
			[DebuggerHidden]
			public <GetEnumerator>d__35(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06006579 RID: 25977 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600657A RID: 25978 RVA: 0x00159F40 File Offset: 0x00158140
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					current = current._next;
				}
				else
				{
					this.<>1__state = -1;
					current = head;
				}
				if (current == null)
				{
					return false;
				}
				this.<>2__current = current._value;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700118D RID: 4493
			// (get) Token: 0x0600657B RID: 25979 RVA: 0x00159FAD File Offset: 0x001581AD
			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600657C RID: 25980 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700118E RID: 4494
			// (get) Token: 0x0600657D RID: 25981 RVA: 0x00159FB5 File Offset: 0x001581B5
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04003B87 RID: 15239
			private int <>1__state;

			// Token: 0x04003B88 RID: 15240
			private T <>2__current;

			// Token: 0x04003B89 RID: 15241
			public ConcurrentStack<T>.Node head;

			// Token: 0x04003B8A RID: 15242
			private ConcurrentStack<T>.Node <current>5__2;
		}
	}
}
