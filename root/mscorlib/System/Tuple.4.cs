using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000171 RID: 369
	[Serializable]
	public class Tuple<T1, T2, T3> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00043540 File Offset: 0x00041740
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x00043548 File Offset: 0x00041748
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x00043550 File Offset: 0x00041750
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00043558 File Offset: 0x00041758
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00043246 File Offset: 0x00041446
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00043578 File Offset: 0x00041778
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2)) && comparer.Equals(this.m_Item3, tuple.m_Item3);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0004328E File Offset: 0x0004148E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000435F0 File Offset: 0x000417F0
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
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
			return comparer.Compare(this.m_Item3, tuple.m_Item3);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000432F4 File Offset: 0x000414F4
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0004368E File Offset: 0x0004188E
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3));
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00043314 File Offset: 0x00041514
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x000436C8 File Offset: 0x000418C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000436EC File Offset: 0x000418EC
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x00019B62 File Offset: 0x00017D62
		int ITuple.Length
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700013C RID: 316
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
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x04001204 RID: 4612
		private readonly T1 m_Item1;

		// Token: 0x04001205 RID: 4613
		private readonly T2 m_Item2;

		// Token: 0x04001206 RID: 4614
		private readonly T3 m_Item3;
	}
}
