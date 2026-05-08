using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000174 RID: 372
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00043E1E File Offset: 0x0004201E
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00043E26 File Offset: 0x00042026
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00043E2E File Offset: 0x0004202E
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00043E36 File Offset: 0x00042036
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00043E3E File Offset: 0x0004203E
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00043E46 File Offset: 0x00042046
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00043E4E File Offset: 0x0004204E
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00043246 File Offset: 0x00041446
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00043E84 File Offset: 0x00042084
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5)) && comparer.Equals(this.m_Item6, tuple.m_Item6);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0004328E File Offset: 0x0004148E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00043F5C File Offset: 0x0004215C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
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
			num = comparer.Compare(this.m_Item5, tuple.m_Item5);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item6, tuple.m_Item6);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000432F4 File Offset: 0x000414F4
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00044060 File Offset: 0x00042260
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6));
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00043314 File Offset: 0x00041514
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000440D8 File Offset: 0x000422D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000440FC File Offset: 0x000422FC
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
			sb.Append(", ");
			sb.Append(this.m_Item6);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00019E33 File Offset: 0x00018033
		int ITuple.Length
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000151 RID: 337
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
				case 5:
					return this.Item6;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x04001210 RID: 4624
		private readonly T1 m_Item1;

		// Token: 0x04001211 RID: 4625
		private readonly T2 m_Item2;

		// Token: 0x04001212 RID: 4626
		private readonly T3 m_Item3;

		// Token: 0x04001213 RID: 4627
		private readonly T4 m_Item4;

		// Token: 0x04001214 RID: 4628
		private readonly T5 m_Item5;

		// Token: 0x04001215 RID: 4629
		private readonly T6 m_Item6;
	}
}
