using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000082 RID: 130
	public sealed class EffectAnnotationCollection : IEnumerable<EffectAnnotation>, IEnumerable
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00026AFA File Offset: 0x00024CFA
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		// Token: 0x17000200 RID: 512
		public EffectAnnotation this[int index]
		{
			get
			{
				return this.elements[index];
			}
		}

		// Token: 0x17000201 RID: 513
		public EffectAnnotation this[string name]
		{
			get
			{
				foreach (EffectAnnotation effectAnnotation in this.elements)
				{
					if (name.Equals(effectAnnotation.Name))
					{
						return effectAnnotation;
					}
				}
				return null;
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00026B7C File Offset: 0x00024D7C
		internal EffectAnnotationCollection(List<EffectAnnotation> value)
		{
			this.elements = value;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00026B8B File Offset: 0x00024D8B
		public List<EffectAnnotation>.Enumerator GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00026B98 File Offset: 0x00024D98
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00026B98 File Offset: 0x00024D98
		IEnumerator<EffectAnnotation> IEnumerable<EffectAnnotation>.GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00026BAA File Offset: 0x00024DAA
		// Note: this type is marked as 'beforefieldinit'.
		static EffectAnnotationCollection()
		{
		}

		// Token: 0x040007E5 RID: 2021
		internal static readonly EffectAnnotationCollection Empty = new EffectAnnotationCollection(new List<EffectAnnotation>());

		// Token: 0x040007E6 RID: 2022
		private List<EffectAnnotation> elements;
	}
}
