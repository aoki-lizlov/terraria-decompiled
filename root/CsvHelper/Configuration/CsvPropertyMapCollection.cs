using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CsvHelper.Configuration
{
	// Token: 0x0200003A RID: 58
	[DebuggerDisplay("Count = {list.Count}")]
	public class CsvPropertyMapCollection : IList<CsvPropertyMap>, ICollection<CsvPropertyMap>, IEnumerable<CsvPropertyMap>, IEnumerable
	{
		// Token: 0x060001FD RID: 509 RVA: 0x000078B3 File Offset: 0x00005AB3
		public CsvPropertyMapCollection()
			: this(new CsvPropertyMapComparer())
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000078C0 File Offset: 0x00005AC0
		public CsvPropertyMapCollection(IComparer<CsvPropertyMap> comparer)
		{
			this.comparer = comparer;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000078DA File Offset: 0x00005ADA
		public virtual IEnumerator<CsvPropertyMap> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000078EC File Offset: 0x00005AEC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000078F4 File Offset: 0x00005AF4
		public virtual void Add(CsvPropertyMap item)
		{
			this.list.Add(item);
			this.list.Sort(this.comparer);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007913 File Offset: 0x00005B13
		public virtual void AddRange(ICollection<CsvPropertyMap> collection)
		{
			this.list.AddRange(collection);
			this.list.Sort(this.comparer);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007932 File Offset: 0x00005B32
		public virtual void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000793F File Offset: 0x00005B3F
		public virtual bool Contains(CsvPropertyMap item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000794D File Offset: 0x00005B4D
		public virtual void CopyTo(CsvPropertyMap[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000795C File Offset: 0x00005B5C
		public virtual bool Remove(CsvPropertyMap item)
		{
			return this.list.Remove(item);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000796A File Offset: 0x00005B6A
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00005DED File Offset: 0x00003FED
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007977 File Offset: 0x00005B77
		public virtual int IndexOf(CsvPropertyMap item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007985 File Offset: 0x00005B85
		public virtual void Insert(int index, CsvPropertyMap item)
		{
			this.list.Insert(index, item);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007994 File Offset: 0x00005B94
		public virtual void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x1700005E RID: 94
		public virtual CsvPropertyMap this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x04000063 RID: 99
		private readonly List<CsvPropertyMap> list = new List<CsvPropertyMap>();

		// Token: 0x04000064 RID: 100
		private readonly IComparer<CsvPropertyMap> comparer;
	}
}
