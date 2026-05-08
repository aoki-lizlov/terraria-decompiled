using System;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x0200045C RID: 1116
	public class FireflyLeashedCritter : FlyLeashedCritter
	{
		// Token: 0x0600328A RID: 12938 RVA: 0x005F07EF File Offset: 0x005EE9EF
		protected override void CopyToDummy()
		{
			base.CopyToDummy();
			LeashedCritter._dummy.localAI[2] = (float)(this.lightOn ? 1 : 0);
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x005F0810 File Offset: 0x005EEA10
		protected override void VisualEffects()
		{
			base.VisualEffects();
			this.UpdateTimer();
			if (this.lightOn && this.timer > 3)
			{
				this.AddLight();
			}
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x005F0838 File Offset: 0x005EEA38
		private void AddLight()
		{
			int num = (int)base.Center.X / 16;
			int num2 = (int)base.Center.Y / 16;
			float scale = LeashedCritter._dummy.scale;
			int npcType = this.npcType;
			if (npcType == 355)
			{
				Lighting.AddLight(num, num2, 0.109500006f * scale, 0.15f * scale, 0.0615f * scale);
				return;
			}
			if (npcType == 358)
			{
				Lighting.AddLight(num, num2, 0.10124999f * scale, 0.21374999f * scale, 0.225f * scale);
				return;
			}
			if (npcType != 654)
			{
				return;
			}
			Lighting.AddLight(num, num2, 0.225f * scale, 0.105000004f * scale, 0.060000002f * scale);
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x005F08E8 File Offset: 0x005EEAE8
		private void UpdateTimer()
		{
			int num = this.timer - 1;
			this.timer = num;
			if (num > 0)
			{
				return;
			}
			this.timer = 0;
			if (!this.lightOn && Main.dayTime && (double)(this.position.Y / 16f) < Main.worldSurface + 10.0)
			{
				return;
			}
			this.lightOn = !this.lightOn;
			this.timer = (this.lightOn ? Main.rand.Next(10, 30) : Main.rand.Next(30, 180));
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x005F0429 File Offset: 0x005EE629
		public FireflyLeashedCritter()
		{
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x005F0982 File Offset: 0x005EEB82
		// Note: this type is marked as 'beforefieldinit'.
		static FireflyLeashedCritter()
		{
		}

		// Token: 0x0400582F RID: 22575
		public new static FireflyLeashedCritter Prototype = new FireflyLeashedCritter();

		// Token: 0x04005830 RID: 22576
		private bool lightOn;

		// Token: 0x04005831 RID: 22577
		private int timer;
	}
}
