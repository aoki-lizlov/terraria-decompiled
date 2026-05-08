using System;
using Terraria.Localization;

namespace Terraria.GameContent.Items
{
	// Token: 0x02000476 RID: 1142
	public class ItemVariantCondition
	{
		// Token: 0x0600331A RID: 13082 RVA: 0x005F3645 File Offset: 0x005F1845
		public ItemVariantCondition(NetworkText description, ItemVariantCondition.Condition condition)
		{
			this.Description = description;
			this.IsMet = condition;
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x005F365B File Offset: 0x005F185B
		public override string ToString()
		{
			return this.Description.ToString();
		}

		// Token: 0x04005885 RID: 22661
		public readonly NetworkText Description;

		// Token: 0x04005886 RID: 22662
		public readonly ItemVariantCondition.Condition IsMet;

		// Token: 0x02000970 RID: 2416
		// (Invoke) Token: 0x060048FD RID: 18685
		public delegate bool Condition();
	}
}
