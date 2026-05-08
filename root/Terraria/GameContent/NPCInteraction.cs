using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x0200023F RID: 575
	public abstract class NPCInteraction
	{
		// Token: 0x0600229C RID: 8860
		public abstract bool Condition();

		// Token: 0x0600229D RID: 8861
		public abstract string GetText();

		// Token: 0x0600229E RID: 8862
		public abstract void Interact();

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public virtual bool ShowExcalmation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x005396F1 File Offset: 0x005378F1
		public virtual bool TryAddCoins(ref Color chatColor, out int coinValue)
		{
			coinValue = 0;
			return false;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x005396F7 File Offset: 0x005378F7
		public Player LocalPlayer
		{
			get
			{
				return Main.LocalPlayer;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x005396FE File Offset: 0x005378FE
		public NPC TalkNPC
		{
			get
			{
				return Main.npc[this.LocalPlayer.talkNPC];
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x00539711 File Offset: 0x00537911
		public int TalkNPCType
		{
			get
			{
				if (this.LocalPlayer.talkNPC == -1)
				{
					return 0;
				}
				return this.TalkNPC.type;
			}
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0000357B File Offset: 0x0000177B
		protected NPCInteraction()
		{
		}
	}
}
