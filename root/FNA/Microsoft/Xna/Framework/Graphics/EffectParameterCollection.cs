using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000086 RID: 134
	public sealed class EffectParameterCollection : IEnumerable<EffectParameter>, IEnumerable
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00028A46 File Offset: 0x00026C46
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		// Token: 0x1700020C RID: 524
		public EffectParameter this[int index]
		{
			get
			{
				return this.elements[index];
			}
		}

		// Token: 0x1700020D RID: 525
		public EffectParameter this[string name]
		{
			get
			{
				foreach (EffectParameter effectParameter in this.elements)
				{
					if (name.Equals(effectParameter.Name))
					{
						return effectParameter;
					}
				}
				return null;
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00028AC8 File Offset: 0x00026CC8
		internal EffectParameterCollection(List<EffectParameter> value)
		{
			this.elements = value;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00028AD7 File Offset: 0x00026CD7
		public List<EffectParameter>.Enumerator GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00028AE4 File Offset: 0x00026CE4
		public EffectParameter GetParameterBySemantic(string semantic)
		{
			foreach (EffectParameter effectParameter in this.elements)
			{
				if (semantic.Equals(effectParameter.Semantic))
				{
					return effectParameter;
				}
			}
			return null;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00028B48 File Offset: 0x00026D48
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00028B48 File Offset: 0x00026D48
		IEnumerator<EffectParameter> IEnumerable<EffectParameter>.GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x040007FD RID: 2045
		private List<EffectParameter> elements;
	}
}
