using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000012 RID: 18
	public class CurveKeyCollection : ICollection<CurveKey>, IEnumerable<CurveKey>, IEnumerable
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00009A81 File Offset: 0x00007C81
		public int Count
		{
			get
			{
				return this.innerlist.Count;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00009A8E File Offset: 0x00007C8E
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x170000A9 RID: 169
		public CurveKey this[int index]
		{
			get
			{
				return this.innerlist[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (index >= this.innerlist.Count)
				{
					throw new IndexOutOfRangeException();
				}
				if (MathHelper.WithinEpsilon(this.innerlist[index].Position, value.Position))
				{
					this.innerlist[index] = value;
					return;
				}
				this.innerlist.RemoveAt(index);
				this.innerlist.Add(value);
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00009B1D File Offset: 0x00007D1D
		public CurveKeyCollection()
		{
			this.innerlist = new List<CurveKey>();
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00009B30 File Offset: 0x00007D30
		public void Add(CurveKey item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.innerlist.Count == 0)
			{
				this.innerlist.Add(item);
				return;
			}
			for (int i = 0; i < this.innerlist.Count; i++)
			{
				if (item.Position < this.innerlist[i].Position)
				{
					this.innerlist.Insert(i, item);
					return;
				}
			}
			this.innerlist.Add(item);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00009BB4 File Offset: 0x00007DB4
		public void Clear()
		{
			this.innerlist.Clear();
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00009BC4 File Offset: 0x00007DC4
		public CurveKeyCollection Clone()
		{
			CurveKeyCollection curveKeyCollection = new CurveKeyCollection();
			foreach (CurveKey curveKey in this.innerlist)
			{
				curveKeyCollection.Add(curveKey);
			}
			return curveKeyCollection;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00009C20 File Offset: 0x00007E20
		public bool Contains(CurveKey item)
		{
			return this.innerlist.Contains(item);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00009C2E File Offset: 0x00007E2E
		public void CopyTo(CurveKey[] array, int arrayIndex)
		{
			this.innerlist.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00009C3D File Offset: 0x00007E3D
		public IEnumerator<CurveKey> GetEnumerator()
		{
			return this.innerlist.GetEnumerator();
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00009C4F File Offset: 0x00007E4F
		public int IndexOf(CurveKey item)
		{
			return this.innerlist.IndexOf(item);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00009C5D File Offset: 0x00007E5D
		public bool Remove(CurveKey item)
		{
			return this.innerlist.Remove(item);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00009C6B File Offset: 0x00007E6B
		public void RemoveAt(int index)
		{
			this.innerlist.RemoveAt(index);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00009C3D File Offset: 0x00007E3D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.innerlist.GetEnumerator();
		}

		// Token: 0x040004B0 RID: 1200
		private bool isReadOnly;

		// Token: 0x040004B1 RID: 1201
		private List<CurveKey> innerlist;
	}
}
