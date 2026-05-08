using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000089 RID: 137
	public sealed class EffectPassCollection : IEnumerable<EffectPass>, IEnumerable
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00028BFD File Offset: 0x00026DFD
		public int Count
		{
			get
			{
				if (this.elements == null)
				{
					return (this.singleItem != null) ? 1 : 0;
				}
				return this.elements.Count;
			}
		}

		// Token: 0x17000211 RID: 529
		public EffectPass this[int index]
		{
			get
			{
				if (this.elements != null)
				{
					return this.elements[index];
				}
				if (index != 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.singleItem;
			}
		}

		// Token: 0x17000212 RID: 530
		public EffectPass this[string name]
		{
			get
			{
				if (this.elements != null)
				{
					foreach (EffectPass effectPass in this.elements)
					{
						if (name.Equals(effectPass.Name))
						{
							return effectPass;
						}
					}
					return null;
				}
				if (this.singleItem.Name.Equals(name))
				{
					return this.singleItem;
				}
				return null;
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00028CD0 File Offset: 0x00026ED0
		internal EffectPassCollection(List<EffectPass> value)
		{
			this.elements = value;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00028CDF File Offset: 0x00026EDF
		internal EffectPassCollection(EffectPass pass)
		{
			this.singleItem = pass;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00028CEE File Offset: 0x00026EEE
		internal List<EffectPass> GetList()
		{
			if (this.elements == null)
			{
				this.elements = new List<EffectPass>(1);
				this.elements.Add(this.singleItem);
			}
			return this.elements;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00028D1B File Offset: 0x00026F1B
		public List<EffectPass>.Enumerator GetEnumerator()
		{
			return this.GetList().GetEnumerator();
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00028D28 File Offset: 0x00026F28
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetList().GetEnumerator();
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00028D28 File Offset: 0x00026F28
		IEnumerator<EffectPass> IEnumerable<EffectPass>.GetEnumerator()
		{
			return this.GetList().GetEnumerator();
		}

		// Token: 0x0400080E RID: 2062
		private List<EffectPass> elements;

		// Token: 0x0400080F RID: 2063
		private EffectPass singleItem;
	}
}
