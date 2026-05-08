using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200008B RID: 139
	public sealed class EffectTechniqueCollection : IEnumerable<EffectTechnique>, IEnumerable
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00028DA3 File Offset: 0x00026FA3
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		// Token: 0x17000218 RID: 536
		public EffectTechnique this[int index]
		{
			get
			{
				return this.elements[index];
			}
		}

		// Token: 0x17000219 RID: 537
		public EffectTechnique this[string name]
		{
			get
			{
				foreach (EffectTechnique effectTechnique in this.elements)
				{
					if (name.Equals(effectTechnique.Name))
					{
						return effectTechnique;
					}
				}
				return null;
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00028E24 File Offset: 0x00027024
		internal EffectTechniqueCollection(List<EffectTechnique> value)
		{
			this.elements = value;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00028E33 File Offset: 0x00027033
		public List<EffectTechnique>.Enumerator GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00028E40 File Offset: 0x00027040
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00028E40 File Offset: 0x00027040
		IEnumerator<EffectTechnique> IEnumerable<EffectTechnique>.GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x04000814 RID: 2068
		private List<EffectTechnique> elements;
	}
}
