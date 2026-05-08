using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x0200016F RID: 367
	[Serializable]
	public class Tuple<T1> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0004322F File Offset: 0x0004142F
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00043237 File Offset: 0x00041437
		public Tuple(T1 item1)
		{
			this.m_Item1 = item1;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00043246 File Offset: 0x00041446
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00043254 File Offset: 0x00041454
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004328E File Offset: 0x0004148E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004329C File Offset: 0x0004149C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return comparer.Compare(this.m_Item1, tuple.m_Item1);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x000432F4 File Offset: 0x000414F4
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00043301 File Offset: 0x00041501
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.m_Item1);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00043314 File Offset: 0x00041514
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00043320 File Offset: 0x00041520
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00043343 File Offset: 0x00041543
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x00003FB7 File Offset: 0x000021B7
		int ITuple.Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000133 RID: 307
		object ITuple.this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this.Item1;
			}
		}

		// Token: 0x04001201 RID: 4609
		private readonly T1 m_Item1;
	}
}
