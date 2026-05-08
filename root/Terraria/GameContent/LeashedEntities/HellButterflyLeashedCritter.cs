using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x0200045B RID: 1115
	public class HellButterflyLeashedCritter : FlyLeashedCritter
	{
		// Token: 0x06003287 RID: 12935 RVA: 0x005F0710 File Offset: 0x005EE910
		protected override void VisualEffects()
		{
			base.VisualEffects();
			this.position += this.netOffset;
			Lighting.AddLight((int)base.Center.X / 16, (int)base.Center.Y / 16, 0.6f, 0.3f, 0.1f);
			if (Main.rand.Next(60) == 0)
			{
				int num = Dust.NewDust(this.position, this.width, this.height, 6, 0f, 0f, 254, default(Color), 1f);
				Main.dust[num].velocity *= 0f;
			}
			this.position -= this.netOffset;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x005F0429 File Offset: 0x005EE629
		public HellButterflyLeashedCritter()
		{
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x005F07E3 File Offset: 0x005EE9E3
		// Note: this type is marked as 'beforefieldinit'.
		static HellButterflyLeashedCritter()
		{
		}

		// Token: 0x0400582E RID: 22574
		public new static HellButterflyLeashedCritter Prototype = new HellButterflyLeashedCritter();
	}
}
