using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Renderers;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003CF RID: 975
	public class UIParticleLayer : UIElement
	{
		// Token: 0x06002DA1 RID: 11681 RVA: 0x005A50F1 File Offset: 0x005A32F1
		public UIParticleLayer()
		{
			this.IgnoresMouseInteraction = true;
			this.ParticleSystem = new ParticleRenderer();
			base.OnUpdate += this.ParticleSystemUpdate;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x005A511D File Offset: 0x005A331D
		private void ParticleSystemUpdate(UIElement affectedElement)
		{
			this.ParticleSystem.Update();
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x005A512C File Offset: 0x005A332C
		public override void Recalculate()
		{
			base.Recalculate();
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			this.ParticleSystem.Settings.AnchorPosition = rectangle.TopLeft() + this.AnchorPositionOffsetByPercents * rectangle.Size() + this.AnchorPositionOffsetByPixels;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x005A5185 File Offset: 0x005A3385
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this.ParticleSystem.Draw(spriteBatch);
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x005A5193 File Offset: 0x005A3393
		public void AddParticle(IParticle particle)
		{
			this.ParticleSystem.Add(particle);
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x005A51A1 File Offset: 0x005A33A1
		public void ClearParticles()
		{
			this.ParticleSystem.Clear();
		}

		// Token: 0x040054E9 RID: 21737
		public ParticleRenderer ParticleSystem;

		// Token: 0x040054EA RID: 21738
		public Vector2 AnchorPositionOffsetByPercents;

		// Token: 0x040054EB RID: 21739
		public Vector2 AnchorPositionOffsetByPixels;
	}
}
