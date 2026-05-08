using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000173 RID: 371
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00043A92 File Offset: 0x00041C92
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00043A9A File Offset: 0x00041C9A
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00043AA2 File Offset: 0x00041CA2
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00043AAA File Offset: 0x00041CAA
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00043AB2 File Offset: 0x00041CB2
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00043ABA File Offset: 0x00041CBA
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00043246 File Offset: 0x00041446
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00043AE8 File Offset: 0x00041CE8
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4)) && comparer.Equals(this.m_Item5, tuple.m_Item5);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0004328E File Offset: 0x0004148E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00043B9C File Offset: 0x00041D9C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item2, tuple.m_Item2);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item3, tuple.m_Item3);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item4, tuple.m_Item4);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item5, tuple.m_Item5);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000432F4 File Offset: 0x000414F4
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00043C80 File Offset: 0x00041E80
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5));
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00043314 File Offset: 0x00041514
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00043CE8 File Offset: 0x00041EE8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00043D0C File Offset: 0x00041F0C
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(", ");
			sb.Append(this.m_Item5);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x000348A8 File Offset: 0x00032AA8
		int ITuple.Length
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000149 RID: 329
		object ITuple.this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.Item1;
				case 1:
					return this.Item2;
				case 2:
					return this.Item3;
				case 3:
					return this.Item4;
				case 4:
					return this.Item5;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x0400120B RID: 4619
		private readonly T1 m_Item1;

		// Token: 0x0400120C RID: 4620
		private readonly T2 m_Item2;

		// Token: 0x0400120D RID: 4621
		private readonly T3 m_Item3;

		// Token: 0x0400120E RID: 4622
		private readonly T4 m_Item4;

		// Token: 0x0400120F RID: 4623
		private readonly T5 m_Item5;
	}
}
