using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000463 RID: 1123
	public class FairyLeashedCritter : FlyerLeashedCritter
	{
		// Token: 0x060032A5 RID: 12965 RVA: 0x005F0C68 File Offset: 0x005EEE68
		public FairyLeashedCritter()
		{
			this.minWaitTime = 30;
			this.maxWaitTime = 90;
			this.maxFlySpeed = 1.1f;
			this.acceleration = 0.05f;
			this.rotationScalar = 0.25f;
			this.brakeDuration = 30;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x005F0CB4 File Offset: 0x005EEEB4
		protected override void VisualEffects()
		{
			base.VisualEffects();
			Color color = Color.HotPink;
			Color color2 = Color.LightPink;
			int num = 4;
			if (this.npcType == 584)
			{
				color = Color.LimeGreen;
				color2 = Color.LightSeaGreen;
			}
			if (this.npcType == 585)
			{
				color = Color.RoyalBlue;
				color2 = Color.LightBlue;
			}
			if ((int)Main.timeForVisualEffects % 4 == 0 && Main.rand.Next(4) != 0)
			{
				this.position += this.netOffset;
				Dust dust = Dust.NewDustDirect(base.Center - new Vector2(4f) + Main.rand.NextVector2Circular(2f, 2f), num, num, 278, 0f, 0f, 200, Color.Lerp(color, color2, Main.rand.NextFloat()), 0.65f);
				dust.velocity *= 0f;
				dust.velocity += this.velocity * 0.3f;
				dust.noGravity = true;
				dust.noLight = true;
				this.position -= this.netOffset;
			}
			Lighting.AddLight(base.Center, color.ToVector3() * 0.7f);
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x005F0E11 File Offset: 0x005EF011
		// Note: this type is marked as 'beforefieldinit'.
		static FairyLeashedCritter()
		{
		}

		// Token: 0x0400583A RID: 22586
		public new static FairyLeashedCritter Prototype = new FairyLeashedCritter();
	}
}
