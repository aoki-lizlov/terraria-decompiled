using System;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x0200046A RID: 1130
	public class SnailLeashedCritter : CrawlerLeashedCritter
	{
		// Token: 0x060032DE RID: 13022 RVA: 0x005F2C97 File Offset: 0x005F0E97
		protected override void SetDefaults(Item sample)
		{
			base.SetDefaults(sample);
			if (this.npcType == 359)
			{
				this.scale = (float)Main.rand.Next(80, 111) * 0.01f;
			}
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x005F2CC8 File Offset: 0x005F0EC8
		protected override void VisualEffects()
		{
			base.VisualEffects();
			int npcType = this.npcType;
			if (npcType == 360)
			{
				Lighting.AddLight((int)base.Center.X / 16, (int)base.Center.Y / 16, 0.1f, 0.2f, 0.7f);
				return;
			}
			if (npcType != 655)
			{
				return;
			}
			Lighting.AddLight((int)base.Center.X / 16, (int)base.Center.Y / 16, 0.6f, 0.3f, 0.1f);
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x005F2D58 File Offset: 0x005F0F58
		public SnailLeashedCritter()
		{
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x005F2D60 File Offset: 0x005F0F60
		// Note: this type is marked as 'beforefieldinit'.
		static SnailLeashedCritter()
		{
		}

		// Token: 0x04005873 RID: 22643
		public new static SnailLeashedCritter Prototype = new SnailLeashedCritter();
	}
}
