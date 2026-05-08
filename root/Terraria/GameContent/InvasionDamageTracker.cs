using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x0200022F RID: 559
	public class InvasionDamageTracker : NPCDamageTracker
	{
		// Token: 0x06002205 RID: 8709 RVA: 0x005339B1 File Offset: 0x00531BB1
		public InvasionDamageTracker(int invasionGroup, LocalizedText name = null)
		{
			this._invasionGroup = invasionGroup;
			this._name = ((name != null) ? name : Language.GetText(InvasionDamageTracker.VanillaInvasionNameKeys[invasionGroup]));
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x005339DC File Offset: 0x00531BDC
		public override LocalizedText Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x00076333 File Offset: 0x00074533
		public override LocalizedText KillTimeMessage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x005339E4 File Offset: 0x00531BE4
		protected override bool IncludeDamageFor(NPC npc)
		{
			return NPC.GetNPCInvasionGroup(npc.type) == this._invasionGroup;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x005339F9 File Offset: 0x00531BF9
		protected override void CheckActive()
		{
			if (!this.IsActive())
			{
				base.Stop();
			}
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00533A09 File Offset: 0x00531C09
		private bool IsActive()
		{
			if (this._invasionGroup == -2)
			{
				return Main.pumpkinMoon;
			}
			if (this._invasionGroup == -1)
			{
				return Main.snowMoon;
			}
			return Main.invasionType == this._invasionGroup;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x00533A38 File Offset: 0x00531C38
		// Note: this type is marked as 'beforefieldinit'.
		static InvasionDamageTracker()
		{
		}

		// Token: 0x04004CC1 RID: 19649
		private static Dictionary<int, string> VanillaInvasionNameKeys = new Dictionary<int, string>
		{
			{ 1, "Bestiary_Invasions.Goblins" },
			{ 2, "Bestiary_Invasions.FrostLegion" },
			{ 3, "Bestiary_Invasions.Pirates" },
			{ 4, "Bestiary_Invasions.Martian" },
			{ -2, "Bestiary_Invasions.PumpkinMoon" },
			{ -1, "Bestiary_Invasions.FrostMoon" }
		};

		// Token: 0x04004CC2 RID: 19650
		private readonly int _invasionGroup;

		// Token: 0x04004CC3 RID: 19651
		private readonly LocalizedText _name;
	}
}
