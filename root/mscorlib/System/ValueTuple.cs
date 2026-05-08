using System;
using System.Collections;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000184 RID: 388
	[Serializable]
	public struct ValueTuple : IEquatable<ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple>, IValueTupleInternal, ITuple
	{
		// Token: 0x06001259 RID: 4697 RVA: 0x0004904B File Offset: 0x0004724B
		public override bool Equals(object obj)
		{
			return obj is ValueTuple;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool Equals(ValueTuple other)
		{
			return true;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004904B File Offset: 0x0004724B
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			return other is ValueTuple;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00049056 File Offset: 0x00047256
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return 0;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0000408A File Offset: 0x0000228A
		public int CompareTo(ValueTuple other)
		{
			return 0;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00049056 File Offset: 0x00047256
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return 0;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0000408A File Offset: 0x0000228A
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0000408A File Offset: 0x0000228A
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0000408A File Offset: 0x0000228A
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00049090 File Offset: 0x00047290
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00049097 File Offset: 0x00047297
		string IValueTupleInternal.ToStringEnd()
		{
			return ")";
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0000408A File Offset: 0x0000228A
		int ITuple.Length
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001B5 RID: 437
		object ITuple.this[int index]
		{
			get
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000490A8 File Offset: 0x000472A8
		public static ValueTuple Create()
		{
			return default(ValueTuple);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000490BE File Offset: 0x000472BE
		public static ValueTuple<T1> Create<T1>(T1 item1)
		{
			return new ValueTuple<T1>(item1);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000490C6 File Offset: 0x000472C6
		public static ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new ValueTuple<T1, T2>(item1, item2);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000490CF File Offset: 0x000472CF
		public static ValueTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new ValueTuple<T1, T2, T3>(item1, item2, item3);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000490D9 File Offset: 0x000472D9
		public static ValueTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000490E4 File Offset: 0x000472E4
		public static ValueTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000490F1 File Offset: 0x000472F1
		public static ValueTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00049100 File Offset: 0x00047300
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00049111 File Offset: 0x00047311
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(item1, item2, item3, item4, item5, item6, item7, ValueTuple.Create<T8>(item8));
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00049129 File Offset: 0x00047329
		internal static int CombineHashCodes(int h1, int h2)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(global::System.Numerics.Hashing.HashHelpers.Combine(global::System.Numerics.Hashing.HashHelpers.RandomSeed, h1), h2);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0004913C File Offset: 0x0004733C
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004914B File Offset: 0x0004734B
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3), h4);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0004915B File Offset: 0x0004735B
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0004916D File Offset: 0x0004736D
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5), h6);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00049181 File Offset: 0x00047381
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00049197 File Offset: 0x00047397
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
		}
	}
}
