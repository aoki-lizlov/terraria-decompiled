using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000170 RID: 368
	[Serializable]
	public class Tuple<T1, T2> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0004337C File Offset: 0x0004157C
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x00043384 File Offset: 0x00041584
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0004338C File Offset: 0x0004158C
		public Tuple(T1 item1, T2 item2)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00043246 File Offset: 0x00041446
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000433A4 File Offset: 0x000415A4
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004328E File Offset: 0x0004148E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00043400 File Offset: 0x00041600
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item2, tuple.m_Item2);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000432F4 File Offset: 0x000414F4
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004347C File Offset: 0x0004167C
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2));
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00043314 File Offset: 0x00041514
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000434A8 File Offset: 0x000416A8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000434CC File Offset: 0x000416CC
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00015289 File Offset: 0x00013489
		int ITuple.Length
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000137 RID: 311
		object ITuple.this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.Item1;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.Item2;
			}
		}

		// Token: 0x04001202 RID: 4610
		private readonly T1 m_Item1;

		// Token: 0x04001203 RID: 4611
		private readonly T2 m_Item2;
	}
}
