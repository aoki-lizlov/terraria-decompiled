using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000B7 RID: 183
	public sealed class SamplerStateCollection
	{
		// Token: 0x170002ED RID: 749
		public SamplerState this[int index]
		{
			get
			{
				return this.samplers[index];
			}
			set
			{
				this.samplers[index] = value;
				this.modifiedSamplers[index] = true;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0002FC2C File Offset: 0x0002DE2C
		internal SamplerStateCollection(int slots, bool[] modSamplers)
		{
			this.samplers = new SamplerState[slots];
			this.modifiedSamplers = modSamplers;
			for (int i = 0; i < this.samplers.Length; i++)
			{
				this.samplers[i] = SamplerState.LinearWrap;
			}
		}

		// Token: 0x0400098D RID: 2445
		private readonly SamplerState[] samplers;

		// Token: 0x0400098E RID: 2446
		private readonly bool[] modifiedSamplers;
	}
}
