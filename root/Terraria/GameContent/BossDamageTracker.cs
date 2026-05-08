using System;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x0200022E RID: 558
	public class BossDamageTracker : NPCDamageTracker
	{
		// Token: 0x060021FE RID: 8702 RVA: 0x00533839 File Offset: 0x00531A39
		public BossDamageTracker(int type, NPCDamageTracker.CustomDefinition definition)
		{
			if (definition != null && definition.NPCTypes != null)
			{
				type = definition.NPCTypes[0];
			}
			this._type = type;
			this._overrides = definition;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x00533868 File Offset: 0x00531A68
		public override LocalizedText Name
		{
			get
			{
				if (this._overrides == null || this._overrides.Name == null)
				{
					return Lang.GetNPCName(this._type);
				}
				return this._overrides.Name;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x00533896 File Offset: 0x00531A96
		public override LocalizedText KillTimeMessage
		{
			get
			{
				return Language.GetText(this._killed ? "BossDamageCommand.KillTime" : "BossDamageCommand.KillTimeEscaped");
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x005338B4 File Offset: 0x00531AB4
		protected override bool IncludeDamageFor(NPC npc)
		{
			if (NPCDamageTracker.BossTypeForMob[npc.type] == this._type)
			{
				return true;
			}
			if (this._overrides == null || this._overrides.NPCTypes == null)
			{
				return npc.type == this._type;
			}
			return this._overrides.NPCTypes.Contains(npc.type);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00533911 File Offset: 0x00531B11
		protected override void CheckActive()
		{
			if (!this.IsActive())
			{
				base.Stop();
			}
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x00533924 File Offset: 0x00531B24
		private bool IsActive()
		{
			if (this._overrides != null && this._overrides.NPCTypes != null)
			{
				foreach (int num in this._overrides.NPCTypes)
				{
					if (NPC.npcsFoundForCheckActive[num])
					{
						return true;
					}
				}
				return false;
			}
			return NPC.npcsFoundForCheckActive[this._type];
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x005339A8 File Offset: 0x00531BA8
		protected override void OnBossKilled(NPC npc)
		{
			this._killed = true;
		}

		// Token: 0x04004CBE RID: 19646
		private readonly int _type;

		// Token: 0x04004CBF RID: 19647
		private readonly NPCDamageTracker.CustomDefinition _overrides;

		// Token: 0x04004CC0 RID: 19648
		private bool _killed;
	}
}
