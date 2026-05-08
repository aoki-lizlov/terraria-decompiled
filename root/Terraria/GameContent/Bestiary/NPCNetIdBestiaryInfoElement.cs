using System;
using System.Runtime.CompilerServices;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200035D RID: 861
	public class NPCNetIdBestiaryInfoElement : IBestiaryInfoElement, IBestiaryEntryDisplayIndex
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x0057404B File Offset: 0x0057224B
		// (set) Token: 0x060028BC RID: 10428 RVA: 0x00574053 File Offset: 0x00572253
		public int NetId
		{
			[CompilerGenerated]
			get
			{
				return this.<NetId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NetId>k__BackingField = value;
			}
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0057405C File Offset: 0x0057225C
		public NPCNetIdBestiaryInfoElement(int npcNetId)
		{
			this.NetId = npcNetId;
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x0057406B File Offset: 0x0057226B
		public int BestiaryDisplayIndex
		{
			get
			{
				return ContentSamples.NpcBestiarySortingId[this.NetId];
			}
		}

		// Token: 0x04005166 RID: 20838
		[CompilerGenerated]
		private int <NetId>k__BackingField;
	}
}
