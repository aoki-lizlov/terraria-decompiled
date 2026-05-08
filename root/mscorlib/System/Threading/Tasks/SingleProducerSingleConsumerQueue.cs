using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200031F RID: 799
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SingleProducerSingleConsumerQueue<>.SingleProducerSingleConsumerQueue_DebugView))]
	internal sealed class SingleProducerSingleConsumerQueue<T> : IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600231B RID: 8987 RVA: 0x0007EAA0 File Offset: 0x0007CCA0
		internal SingleProducerSingleConsumerQueue()
		{
			this.m_head = (this.m_tail = new SingleProducerSingleConsumerQueue<T>.Segment(32));
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x0007EAD0 File Offset: 0x0007CCD0
		public void Enqueue(T item)
		{
			SingleProducerSingleConsumerQueue<T>.Segment tail = this.m_tail;
			T[] array = tail.m_array;
			int last = tail.m_state.m_last;
			int num = (last + 1) & (array.Length - 1);
			if (num != tail.m_state.m_firstCopy)
			{
				array[last] = item;
				tail.m_state.m_last = num;
				return;
			}
			this.EnqueueSlow(item, ref tail);
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x0007EB34 File Offset: 0x0007CD34
		private void EnqueueSlow(T item, ref SingleProducerSingleConsumerQueue<T>.Segment segment)
		{
			if (segment.m_state.m_firstCopy != segment.m_state.m_first)
			{
				segment.m_state.m_firstCopy = segment.m_state.m_first;
				this.Enqueue(item);
				return;
			}
			int num = this.m_tail.m_array.Length << 1;
			if (num > 16777216)
			{
				num = 16777216;
			}
			SingleProducerSingleConsumerQueue<T>.Segment segment2 = new SingleProducerSingleConsumerQueue<T>.Segment(num);
			segment2.m_array[0] = item;
			segment2.m_state.m_last = 1;
			segment2.m_state.m_lastCopy = 1;
			try
			{
			}
			finally
			{
				Volatile.Write<SingleProducerSingleConsumerQueue<T>.Segment>(ref this.m_tail.m_next, segment2);
				this.m_tail = segment2;
			}
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x0007EBFC File Offset: 0x0007CDFC
		public bool TryDequeue(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				array[first] = default(T);
				head.m_state.m_first = (first + 1) & (array.Length - 1);
				return true;
			}
			return this.TryDequeueSlow(ref head, ref array, out result);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x0007EC78 File Offset: 0x0007CE78
		private bool TryDequeueSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeue(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			array[first] = default(T);
			segment.m_state.m_first = (first + 1) & (segment.m_array.Length - 1);
			segment.m_state.m_lastCopy = segment.m_state.m_last;
			return true;
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x0007ED88 File Offset: 0x0007CF88
		public bool TryPeek(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				return true;
			}
			return this.TryPeekSlow(ref head, ref array, out result);
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x0007EDDC File Offset: 0x0007CFDC
		private bool TryPeekSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryPeek(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			return true;
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x0007EEA4 File Offset: 0x0007D0A4
		public bool TryDequeueIf(Predicate<T> predicate, out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first == head.m_state.m_lastCopy)
			{
				return this.TryDequeueIfSlow(predicate, ref head, ref array, out result);
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				head.m_state.m_first = (first + 1) & (array.Length - 1);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x0007EF38 File Offset: 0x0007D138
		private bool TryDequeueIfSlow(Predicate<T> predicate, ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeueIf(predicate, out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				segment.m_state.m_first = (first + 1) & (segment.m_array.Length - 1);
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0007F068 File Offset: 0x0007D268
		public void Clear()
		{
			T t;
			while (this.TryDequeue(out t))
			{
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x0007F080 File Offset: 0x0007D280
		public bool IsEmpty
		{
			get
			{
				SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
				return head.m_state.m_first == head.m_state.m_lastCopy && head.m_state.m_first == head.m_state.m_last && head.m_next == null;
			}
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x0007F0D9 File Offset: 0x0007D2D9
		public IEnumerator<T> GetEnumerator()
		{
			SingleProducerSingleConsumerQueue<T>.Segment segment;
			for (segment = this.m_head; segment != null; segment = segment.m_next)
			{
				for (int pt = segment.m_state.m_first; pt != segment.m_state.m_last; pt = (pt + 1) & (segment.m_array.Length - 1))
				{
					yield return segment.m_array[pt];
				}
			}
			segment = null;
			yield break;
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0007F0E8 File Offset: 0x0007D2E8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x0007F0F0 File Offset: 0x0007D2F0
		public int Count
		{
			get
			{
				int num = 0;
				for (SingleProducerSingleConsumerQueue<T>.Segment segment = this.m_head; segment != null; segment = segment.m_next)
				{
					int num2 = segment.m_array.Length;
					int first;
					int last;
					do
					{
						first = segment.m_state.m_first;
						last = segment.m_state.m_last;
					}
					while (first != segment.m_state.m_first);
					num += (last - first) & (num2 - 1);
				}
				return num;
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0007F158 File Offset: 0x0007D358
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			int count;
			lock (syncObj)
			{
				count = this.Count;
			}
			return count;
		}

		// Token: 0x04001B76 RID: 7030
		private const int INIT_SEGMENT_SIZE = 32;

		// Token: 0x04001B77 RID: 7031
		private const int MAX_SEGMENT_SIZE = 16777216;

		// Token: 0x04001B78 RID: 7032
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_head;

		// Token: 0x04001B79 RID: 7033
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_tail;

		// Token: 0x02000320 RID: 800
		[StructLayout(LayoutKind.Sequential)]
		private sealed class Segment
		{
			// Token: 0x0600232A RID: 9002 RVA: 0x0007F198 File Offset: 0x0007D398
			internal Segment(int size)
			{
				this.m_array = new T[size];
			}

			// Token: 0x04001B7A RID: 7034
			internal SingleProducerSingleConsumerQueue<T>.Segment m_next;

			// Token: 0x04001B7B RID: 7035
			internal readonly T[] m_array;

			// Token: 0x04001B7C RID: 7036
			internal SingleProducerSingleConsumerQueue<T>.SegmentState m_state;
		}

		// Token: 0x02000321 RID: 801
		private struct SegmentState
		{
			// Token: 0x04001B7D RID: 7037
			internal PaddingFor32 m_pad0;

			// Token: 0x04001B7E RID: 7038
			internal volatile int m_first;

			// Token: 0x04001B7F RID: 7039
			internal int m_lastCopy;

			// Token: 0x04001B80 RID: 7040
			internal PaddingFor32 m_pad1;

			// Token: 0x04001B81 RID: 7041
			internal int m_firstCopy;

			// Token: 0x04001B82 RID: 7042
			internal volatile int m_last;

			// Token: 0x04001B83 RID: 7043
			internal PaddingFor32 m_pad2;
		}

		// Token: 0x02000322 RID: 802
		private sealed class SingleProducerSingleConsumerQueue_DebugView
		{
			// Token: 0x0600232B RID: 9003 RVA: 0x0007F1AC File Offset: 0x0007D3AC
			public SingleProducerSingleConsumerQueue_DebugView(SingleProducerSingleConsumerQueue<T> queue)
			{
				this.m_queue = queue;
			}

			// Token: 0x17000438 RID: 1080
			// (get) Token: 0x0600232C RID: 9004 RVA: 0x0007F1BC File Offset: 0x0007D3BC
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public T[] Items
			{
				get
				{
					LowLevelList<T> lowLevelList = new LowLevelList<T>();
					foreach (T t in this.m_queue)
					{
						lowLevelList.Add(t);
					}
					return lowLevelList.ToArray();
				}
			}

			// Token: 0x04001B84 RID: 7044
			private readonly SingleProducerSingleConsumerQueue<T> m_queue;
		}

		// Token: 0x02000323 RID: 803
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__16 : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600232D RID: 9005 RVA: 0x0007F218 File Offset: 0x0007D418
			[DebuggerHidden]
			public <GetEnumerator>d__16(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600232E RID: 9006 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600232F RID: 9007 RVA: 0x0007F228 File Offset: 0x0007D428
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				SingleProducerSingleConsumerQueue<T> singleProducerSingleConsumerQueue = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					segment = singleProducerSingleConsumerQueue.m_head;
					goto IL_00C0;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				pt = (pt + 1) & (segment.m_array.Length - 1);
				IL_0095:
				if (pt != segment.m_state.m_last)
				{
					this.<>2__current = segment.m_array[pt];
					this.<>1__state = 1;
					return true;
				}
				segment = segment.m_next;
				IL_00C0:
				if (segment == null)
				{
					segment = null;
					return false;
				}
				pt = segment.m_state.m_first;
				goto IL_0095;
			}

			// Token: 0x17000439 RID: 1081
			// (get) Token: 0x06002330 RID: 9008 RVA: 0x0007F308 File Offset: 0x0007D508
			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002331 RID: 9009 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700043A RID: 1082
			// (get) Token: 0x06002332 RID: 9010 RVA: 0x0007F310 File Offset: 0x0007D510
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04001B85 RID: 7045
			private int <>1__state;

			// Token: 0x04001B86 RID: 7046
			private T <>2__current;

			// Token: 0x04001B87 RID: 7047
			public SingleProducerSingleConsumerQueue<T> <>4__this;

			// Token: 0x04001B88 RID: 7048
			private SingleProducerSingleConsumerQueue<T>.Segment <segment>5__2;

			// Token: 0x04001B89 RID: 7049
			private int <pt>5__3;
		}
	}
}
