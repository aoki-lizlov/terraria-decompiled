using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000172 RID: 370
	[Serializable]
	public class Tuple<T1, T2, T3, T4> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00043795 File Offset: 0x00041995
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0004379D File Offset: 0x0004199D
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x000437A5 File Offset: 0x000419A5
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x000437AD File Offset: 0x000419AD
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000437B5 File Offset: 0x000419B5
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00043246 File Offset: 0x00041446
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000437DC File Offset: 0x000419DC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3)) && comparer.Equals(this.m_Item4, tuple.m_Item4);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004328E File Offset: 0x0004148E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00043874 File Offset: 0x00041A74
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
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
			return comparer.Compare(this.m_Item4, tuple.m_Item4);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x000432F4 File Offset: 0x000414F4
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00043934 File Offset: 0x00041B34
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4));
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00043314 File Offset: 0x00041514
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0004398C File Offset: 0x00041B8C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000439B0 File Offset: 0x00041BB0
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0001A197 File Offset: 0x00018397
		int ITuple.Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000142 RID: 322
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
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x04001207 RID: 4615
		private readonly T1 m_Item1;

		// Token: 0x04001208 RID: 4616
		private readonly T2 m_Item2;

		// Token: 0x04001209 RID: 4617
		private readonly T3 m_Item3;

		// Token: 0x0400120A RID: 4618
		private readonly T4 m_Item4;
	}
}
