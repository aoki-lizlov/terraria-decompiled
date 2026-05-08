using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000CD RID: 205
	public sealed class TextureCollection
	{
		// Token: 0x17000328 RID: 808
		public Texture this[int index]
		{
			get
			{
				return this.textures[index];
			}
			set
			{
				this.textures[index] = value;
				this.modifiedSamplers[index] = true;
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00033584 File Offset: 0x00031784
		internal TextureCollection(int slots, bool[] modSamplers)
		{
			this.textures = new Texture[slots];
			this.modifiedSamplers = modSamplers;
			for (int i = 0; i < this.textures.Length; i++)
			{
				this.textures[i] = null;
			}
			this.ignoreTargets = false;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x000335D0 File Offset: 0x000317D0
		internal void RemoveDisposedTexture(Texture tex)
		{
			for (int i = 0; i < this.textures.Length; i++)
			{
				if (tex == this.textures[i])
				{
					this[i] = null;
				}
			}
		}

		// Token: 0x04000A3D RID: 2621
		internal bool ignoreTargets;

		// Token: 0x04000A3E RID: 2622
		private readonly Texture[] textures;

		// Token: 0x04000A3F RID: 2623
		private readonly bool[] modifiedSamplers;
	}
}
