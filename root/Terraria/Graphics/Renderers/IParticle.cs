using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x020001FF RID: 511
	public interface IParticle
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06002123 RID: 8483
		bool ShouldBeRemovedFromRenderer { get; }

		// Token: 0x06002124 RID: 8484
		void Update(ref ParticleRendererSettings settings);

		// Token: 0x06002125 RID: 8485
		void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch);
	}
}
